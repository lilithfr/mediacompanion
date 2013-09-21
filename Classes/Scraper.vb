Imports System
Imports System.Threading
Imports System.Net
Imports System.IO
Imports System.Web
Imports System.Data
Imports System.Text.RegularExpressions
Imports Media_Companion
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.IO.Compression
Imports System.Xml.Serialization
Imports System.Xml.XPath
Imports System.Linq


Public Class MovieRegExs
    Public Const REGEX_TAGLINE             = ">Tagline.*?:</h4>[ \t\r\n]+(?<tagline>.*?)[ \t\r\n]+<span"
    Public Const REGEX_HREF_PATTERN        = "<a.*?href=[""'](?<url>.*?)[""'].*?>(?<name>.*?)</a>"
    'Const REGEX_MOVIE_TITLE_PATTERN = "<h1 class=""header"" itemprop=""name"">(.*?)<span class=""nobr"">"
    'Const REGEX_MOVIE_YEAR_PATTERN  = "<img alt="".*?\((.*?)\).*?"" title="""
    Public Const REGEX_RELEASE_DATE        = ">Release Date:</h4>(?<date>.*?)<span"
    Public Const REGEX_STARS               = "Stars:</h4>(.*?)</div>"

    Public Const REGEX_TITLE_AND_YEAR      = "<title>(.*?)</title>"
    Public Const REGEX_TITLE               = "<title>(.*?) \("
    Public Const REGEX_YEAR                = "\(.*?(\d{4}).*?\)" 
    Public Const REGEX_NAME                = "itemprop=""name"">(?<name>.*?)</span>"                '"<span.*?>(?<name>.*?)</span>"
    Public Const REGEX_STUDIO              = "<h4 class=""inline"">Production.*?/h4>(.*?)</div>"
    Public Const REGEX_CREDITS             = "<h4 class=""inline"">Writers?:</h4>(.*?)</div>"
    Public Const REGEX_ORIGINAL_TITLE      = "<span class=""title-extra"" itemprop=""name"">(.*?)<i>\(original title\)</i>"
    Public Const REGEX_OUTLINE                = "itemprop=""description"">(?<outline>.*?)<"
End Class


Module ModGlobals

    <Extension()> _
    Function CleanTitle(ByVal sString As String) As String
        Dim s As String = sString

        If s.StartsWith("""") Then s = s.Remove(0, 1)
        If s.EndsWith  ("""") Then s = s.Remove(s.Length - 1, 1)

        Return s
    End Function


    <Extension()> _
    Sub AppendValue(ByRef s As String, value As String, Optional separator As String=", ")

        value = value.Trim

        If s.IndexOf(value) > -1 Then Exit Sub

        If s="" Then
            s  = value
        Else
            s &= separator & value
        End If
    End Sub


    <Extension()> _
    Sub AppendList(ByRef s As String, lst As IEnumerable(Of String) , Optional separator As String=", ")
        For Each m In lst
            m.ExtractName
            s.AppendValue(m, separator)
        Next
    End Sub


    <Extension()> _
    Sub AppendTag(ByRef s As String, name As String, value As String)

        s &= "<" & name & ">" & value.Trim.EncodeSpecialChrs & "</" & name & ">"& vbCrLf
        
    End Sub


    <Extension()> _
    Sub ExtractName(ByRef s As String)
        
        If s.IndexOf("itemprop=""name"">")>-1 Then s=Net.WebUtility.HtmlDecode( Regex.Match(s,MovieRegExs.REGEX_NAME, RegexOptions.Singleline).Groups("name").Value )
                          
    End Sub

    <Extension()> _
    Function StripTagsLeaveContent(ByRef s As String) As String
        
        Return Net.WebUtility.HtmlDecode( Regex.Replace(s, "<(.|\n)+?>", String.Empty) )
                          
    End Function  

     <Extension()> _
    Function EncodeSpecialChrs(ByRef s As String) As String
        s = s.Replace("&", "&amp;")
        s = s.Replace("<", "&lt;")
        s = s.Replace(">", "&gt;")
        s = s.Replace(Chr(34), "&quot;")
        s = s.Replace("'", "&apos;")

        Return s
    End Function


End Module


Public Class Classimdb


    Public Function getimdbID_fromimdb(ByVal title As String, ByVal imdbmirror As String, Optional ByVal movieyear As String = "")
        Monitor.Enter(Me)
        Try
            Dim popularreturn As String = ""
            Dim exactreturn As String = ""
            Dim M As Match
            title = title.Replace(".", "+")
            title = title.Replace(" ", "+")
            title = title.Replace("&", "%26")
            title = title.Replace("À", "%c0")
            title = title.Replace("Á", "%c1")
            title = title.Replace("Â", "%c2")
            title = title.Replace("Ã", "%c3")
            title = title.Replace("Ä", "%c4")
            title = title.Replace("Å", "%c5")
            title = title.Replace("Æ", "%c6")
            title = title.Replace("Ç", "%c7")
            title = title.Replace("È", "%c8")
            title = title.Replace("É", "%c9")
            title = title.Replace("Ê", "%ca")
            title = title.Replace("Ë", "%cb")
            title = title.Replace("Ì", "%cc")
            title = title.Replace("Í", "%cd")
            title = title.Replace("Î", "%ce")
            title = title.Replace("Ï", "%cf")
            title = title.Replace("Ð", "%d0")
            title = title.Replace("Ñ", "%d1")
            title = title.Replace("Ò", "%d2")
            title = title.Replace("Ó", "%d3")
            title = title.Replace("Ô", "%d4")
            title = title.Replace("Õ", "%d5")
            title = title.Replace("Ö", "%d6")
            title = title.Replace("Ø", "%d8")
            title = title.Replace("Ù", "%d9")
            title = title.Replace("Ú", "%da")
            title = title.Replace("Û", "%db")
            title = title.Replace("Ü", "%dc")
            title = title.Replace("Ý", "%dd")
            title = title.Replace("Þ", "%de")
            title = title.Replace("ß", "%df")
            title = title.Replace("à", "%e0")
            title = title.Replace("á", "%e1")
            title = title.Replace("â", "%e2")
            title = title.Replace("ã", "%e3")
            title = title.Replace("ä", "%e4")
            title = title.Replace("å", "%e5")
            title = title.Replace("æ", "%e6")
            title = title.Replace("ç", "%e7")
            title = title.Replace("è", "%e8")
            title = title.Replace("é", "%e9")
            title = title.Replace("ê", "%ea")
            title = title.Replace("ë", "%eb")
            title = title.Replace("ì", "%ec")
            title = title.Replace("í", "%ed")
            title = title.Replace("î", "%ee")
            title = title.Replace("ï", "%ef")
            title = title.Replace("ð", "%f0")
            title = title.Replace("ñ", "%f1")
            title = title.Replace("ò", "%f2")
            title = title.Replace("ó", "%f3")
            title = title.Replace("ô", "%f4")
            title = title.Replace("õ", "%f5")
            title = title.Replace("ö", "%f6")
            title = title.Replace("÷", "%f7")
            title = title.Replace("ø", "%f8")
            title = title.Replace("ù", "%f9")
            title = title.Replace("ú", "%fa")
            title = title.Replace("û", "%fb")
            title = title.Replace("ü", "%fc")
            title = title.Replace("ý", "%fd")
            title = title.Replace("þ", "%fe")
            title = title.Replace("ÿ", "%ff")

            title = title.Replace("&", "%26")
            title = title.Replace(",", "")
            title = title.Replace("++", "+")
            title = imdbmirror & "search/title?title=" & title & "&title_type=feature"
            'title = imdbmirror & "find?s=tt&q=" & title
            Dim urllinecount As Integer
            Dim GOT_IMDBID As String
            Dim allok As Boolean = False
            Dim websource(4000)
            For f = 1 To 10
                Try
                    Dim wrGETURL As WebRequest
                    wrGETURL = WebRequest.Create(title)
                    Dim myProxy As New WebProxy("myproxy", 80)
                    myProxy.BypassProxyOnLocal = True
                    Dim objStream As Stream
                    objStream = wrGETURL.GetResponse.GetResponseStream()
                    Dim objReader As New StreamReader(objStream)
                    Dim sLine As String = ""
                    urllinecount = 0
                    Do While Not sLine Is Nothing
                        urllinecount += 1
                        sLine = objReader.ReadLine
                        If Not sLine Is Nothing Then
                            websource(urllinecount) = sLine
                        End If
                    Loop
                    urllinecount -= 1
                    allok = True
                    Exit For
                Catch
                End Try
            Next
            GOT_IMDBID = ""
            Dim popular(1000) As String
            Dim atpopular As Boolean = False
            Dim popularcount As Integer = 0
            Dim exact(1000) As String
            Dim atexact As Boolean = False
            Dim exactcount As Integer = 0

            Dim yearcounter As Integer = 0
            Dim year(1000) As Integer
            Dim backup As String = ""


            For f = 1 To urllinecount
                If backup = "" And websource(f).IndexOf("href=""/title/tt") <> -1 Then
                    If websource(f).IndexOf("&#34;") = -1 Then
                        Dim first As Integer
                        Dim length As Integer
                        first = websource(f).LastIndexOf("href=""/title/tt") + 13
                        length = 9
                        backup = websource(f).Substring(first, length)
                    End If
                End If
                If movieyear <> "" Then
                    If websource(f).IndexOf("Media from") <> -1 Then
                        If websource(f).IndexOf("&#34;") = -1 Then
                            If websource(f).IndexOf(movieyear) <> -1 Then
                                Dim first As Integer
                                Dim length As Integer
                                first = websource(f).LastIndexOf("href=""/title/tt") + 13
                                length = 9
                                GOT_IMDBID = websource(f).Substring(first, length)
                                Exit For
                            End If
                        End If
                    End If
                    If websource(f).IndexOf(movieyear) <> -1 Then
                        yearcounter += 1
                        year(yearcounter) = f
                    End If
                End If
            Next
            If movieyear <> Nothing Then
                If yearcounter = 1 And GOT_IMDBID = "" Then
                    Dim onlyid As String = websource(year(yearcounter)).Substring(0, websource(year(yearcounter)).IndexOf(movieyear) + 6)
                    Dim first As Integer
                    Dim length As Integer
                    first = onlyid.LastIndexOf("href=""/title/tt") + 13
                    length = 9
                    GOT_IMDBID = onlyid.Substring(first, length)
                End If
                Dim populartotal As String = ""
                Dim exacttotal As String = ""
                If GOT_IMDBID = "" Then
                    For f = 1 To urllinecount
                        If websource(f).IndexOf("Popular Titles") <> -1 Or websource(f).IndexOf("Exact Matches") <> -1 Then

                            If websource(f).IndexOf("Popular Titles") <> -1 And websource(f).IndexOf("Exact Matches") <> -1 Then
                                populartotal = websource(f).Substring(0, websource(f).IndexOf("Exact Matches") - 13)
                                exacttotal = websource(f).Replace(populartotal, "")
                                Exit For
                            ElseIf websource(f).IndexOf("Popular Titles") <> -1 And websource(f).IndexOf("Exact Matches") = -1 Then
                                populartotal = websource(f)
                                exacttotal = ""
                                Exit For
                            ElseIf websource(f).IndexOf("Popular Titles") = -1 And websource(f).IndexOf("Exact Matches") <> -1 Then
                                populartotal = ""
                                exacttotal = websource(f)
                                Exit For
                            End If
                        End If
                    Next
                    Dim temps As String
                    If GOT_IMDBID = "" And exacttotal <> "" Then
                        If exacttotal.IndexOf(movieyear) <> -1 Then
                            If CharCount(exacttotal, movieyear) = 1 Then
                                temps = exacttotal.Substring(0, exacttotal.IndexOf(movieyear) + 6)
                                Dim calc As String
                                calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                                Dim regMatch As New Regex("tt(\d){6,8}")
                                GOT_IMDBID = regMatch.Matches(calc).Item(0).Value
                            End If
                        End If
                        'temps = exacttotal
                        'If temps.IndexOf(movieyear) <> -1 Then
                        '    Dim count As Integer
                        '    count = CharCount(temps, movieyear)
                        '    If count = 1 Then
                        '        temps = exacttotal.Substring(0, exacttotal.IndexOf(movieyear) + 6)
                        '        Dim first As Integer
                        '        Dim length As Integer
                        '        Dim calc As String
                        '        calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                        '        If temps.IndexOf("&#34;") = -1 Then
                        '            first = temps.LastIndexOf("href=""/title/tt") + 13
                        '            length = 9
                        '            GOT_IMDBID = temps.Substring(first, length)
                        '        End If
                        '    End If
                        'End If

                        If GOT_IMDBID = "" And populartotal <> "" Then
                            If populartotal.IndexOf(movieyear) <> -1 Then
                                If CharCount(populartotal, movieyear) = 1 Then
                                    temps = populartotal.Substring(0, populartotal.IndexOf(movieyear) + 6)
                                    Dim calc As String
                                    calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                                    Dim regMatch As New Regex("tt(\d){6,8}")
                                    GOT_IMDBID = regMatch.Matches(calc).Item(0).Value
                                End If
                            End If
                        End If
                        'If GOT_IMDBID = "" And populartotal <> "" Then
                        '    temps = populartotal
                        '    If temps.IndexOf(movieyear) <> -1 Then
                        '        Dim count As Integer
                        '        count = CharCount(temps, movieyear)
                        '        If count = 1 Then
                        '            temps = populartotal.Substring(0, populartotal.IndexOf(movieyear) + 6)
                        '            Dim first As Integer
                        '            Dim length As Integer
                        '            Dim calc As String
                        '            calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                        '            If temps.IndexOf("&#34;") = -1 Then
                        '                first = temps.LastIndexOf("href=""/title/tt") + 13
                        '                length = 9
                        '                GOT_IMDBID = temps.Substring(first, length)
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    End If
                End If
            ElseIf movieyear = Nothing Then
                Dim exactmatch As Boolean = False
                Dim popularmatch As Boolean = False

                For f = 1 To urllinecount
                    If websource(f).IndexOf("Titles (Exact Matches)") <> -1 Then
                        exactmatch = True
                    End If
                    If websource(f).IndexOf("Popular Titles") <> -1 Then
                        popularmatch = True
                    End If
                Next
                If popularmatch = True Then
                    popularreturn = ""
                    For f = 1 To urllinecount
                        If websource(f).IndexOf("Popular Titles") <> -1 Then
                            Dim type As String
                            type = websource(f).Substring(websource(f).IndexOf("Popular Titles"), websource(f).Length - websource(f).IndexOf("Popular Titles"))
                            If type.IndexOf("Exact Matches") <> -1 Then
                                type = type.Substring(0, type.IndexOf("Exact Matches"))
                            End If
                            If type.IndexOf("Partial Matches") <> -1 Then
                                type = type.Substring(0, type.IndexOf("Partial Matches"))
                            End If
                            If type.IndexOf("Approx Matches") <> -1 Then
                                type = type.Substring(0, type.IndexOf("Approx Matches"))
                            End If
                            Do Until type.IndexOf("</td></tr>") = -1
                                Dim pyte As String = ""
                                If type.IndexOf("</td></tr>") <> -1 Then pyte = type.Substring(0, type.IndexOf("</td></tr>"))
                                If pyte <> "" Then
                                    If pyte.IndexOf("&#34;") = -1 And pyte.IndexOf("<small>(TV") = -1 And pyte.IndexOf("(VG)") = -1 Then
                                        type = pyte
                                        Exit Do
                                    Else
                                        type = type.Replace(pyte, "")
                                    End If
                                Else
                                    If type.Length = 0 Then
                                        Exit Do
                                    Else
                                        type = type.Substring(5, type.Length - 5)
                                    End If
                                End If
                            Loop
                            M = Regex.Match(type, "(tt\d{7})")
                            If M.Success = True Then
                                popularreturn = M.Value
                            End If
                        End If
                    Next
                End If
                If exactmatch = True Then
                    exactreturn = ""
                    For f = 1 To urllinecount
                        If websource(f).IndexOf("Titles (Exact Matches)") <> -1 Then
                            Dim type As String
                            type = websource(f).Substring(websource(f).IndexOf("Titles (Exact Matches)"), websource(f).Length - websource(f).IndexOf("Titles (Exact Matches)"))
                            Do Until type.IndexOf("</td></tr>") = -1
                                Dim pyte As String = ""
                                If type.IndexOf("</td></tr>") <> -1 Then pyte = type.Substring(0, type.IndexOf("</td></tr>"))
                                If type.IndexOf("Partial Matches") <> -1 Then
                                    type = type.Substring(0, type.IndexOf("Partial Matches"))
                                End If
                                If type.IndexOf("Approx Matches") <> -1 Then
                                    type = type.Substring(0, type.IndexOf("Approx Matches"))
                                End If
                                If pyte <> "" Then
                                    If pyte.IndexOf("&#34;") = -1 And pyte.IndexOf("<small>(TV") = -1 And pyte.IndexOf("(VG)") = -1 Then
                                        type = pyte
                                        Exit Do
                                    Else
                                        type = type.Replace(pyte, "")
                                    End If
                                Else
                                    If type.Length = 0 Then
                                        Exit Do
                                    Else
                                        type = type.Substring(5, type.Length - 5)
                                    End If
                                End If
                            Loop
                            M = Regex.Match(type, "(tt\d{7})")
                            If M.Success = True Then
                                exactreturn = M.Value
                            End If
                        End If
                    Next
                End If
                If popularreturn <> "" Then GOT_IMDBID = popularreturn
                If exactreturn <> "" And popularreturn = "" Then GOT_IMDBID = exactreturn
                If exactreturn = "" And popularreturn = "" And backup <> "" Then GOT_IMDBID = backup
            End If






            ' If GOT_IMDBID = "" And backup <> "" Then GOT_IMDBID = backup
            If GOT_IMDBID = "" Then
                Dim matc As Match

                For f = 1 To urllinecount
                    matc = Regex.Match(websource(f), "tt\d{7}")
                    If matc.Success Then
                        GOT_IMDBID = matc.Value
                        Exit For
                    End If
                Next
            End If
            Return GOT_IMDBID


        Catch
            Return "Error"
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function CharCount(ByVal OrigString As String, ByVal Chars As String, Optional ByVal CaseSensitive As Boolean = False) As Long

        Dim lLen As Long
        Dim lCharLen As Long
        Dim lAns As Long
        Dim sInput As String
        Dim sChar As String
        Dim lCtr As Long
        Dim lEndOfLoop As Long
        Dim bytCompareType As Byte

        sInput = OrigString
        If sInput = "" Then 
            Return 0
        End If
        
        lLen = Len(sInput)
        lCharLen = Len(Chars)
        lEndOfLoop = (lLen - lCharLen) + 1
        bytCompareType = IIf(CaseSensitive, vbBinaryCompare, _
           vbTextCompare)

        For lCtr = 1 To lEndOfLoop
            sChar = Mid(sInput, lCtr, lCharLen)
            If StrComp(sChar, Chars, bytCompareType) = 0 Then _
                lAns = lAns + 1
        Next

        CharCount = lAns

    End Function

    Public Function getimdbID(ByVal title As String, Optional ByVal year As String = "", Optional ByVal imdbmirror As String = "")
        If imdbmirror = "" Then
            imdbmirror = "http://www.imdb.com/"
        End If
        Monitor.Enter(Me)
        Try
            Dim newimdbid As String = ""
            Dim allok As Boolean = False
            Dim goodyear As Boolean = False
            If IsNumeric(year) Then
                If year.Length = 4 Then
                    goodyear = True
                End If
            End If

            Dim url As String = "http://www.google.co.uk/search?hl=en&q=%3C"
            Dim titlesearch As String = title
            titlesearch = titlesearch.Replace(".", "+")
            titlesearch = titlesearch.Replace(" ", "+")
            titlesearch = titlesearch.Replace("_", "+")
            titlesearch = titlesearch.Replace("·", "%C2%B7")
            titlesearch = titlesearch.Replace("À", "%c0")
            titlesearch = titlesearch.Replace("Á", "%c1")
            titlesearch = titlesearch.Replace("Â", "%c2")
            titlesearch = titlesearch.Replace("Ã", "%c3")
            titlesearch = titlesearch.Replace("Ä", "%c4")
            titlesearch = titlesearch.Replace("Å", "%c5")
            titlesearch = titlesearch.Replace("Æ", "%c6")
            titlesearch = titlesearch.Replace("Ç", "%c7")
            titlesearch = titlesearch.Replace("È", "%c8")
            titlesearch = titlesearch.Replace("É", "%c9")
            titlesearch = titlesearch.Replace("Ê", "%ca")
            titlesearch = titlesearch.Replace("Ë", "%cb")
            titlesearch = titlesearch.Replace("Ì", "%cc")
            titlesearch = titlesearch.Replace("Í", "%cd")
            titlesearch = titlesearch.Replace("Î", "%ce")
            titlesearch = titlesearch.Replace("Ï", "%cf")
            titlesearch = titlesearch.Replace("Ð", "%d0")
            titlesearch = titlesearch.Replace("Ñ", "%d1")
            titlesearch = titlesearch.Replace("Ò", "%d2")
            titlesearch = titlesearch.Replace("Ó", "%d3")
            titlesearch = titlesearch.Replace("Ô", "%d4")
            titlesearch = titlesearch.Replace("Õ", "%d5")
            titlesearch = titlesearch.Replace("Ö", "%d6")
            titlesearch = titlesearch.Replace("Ø", "%d8")
            titlesearch = titlesearch.Replace("Ù", "%d9")
            titlesearch = titlesearch.Replace("Ú", "%da")
            titlesearch = titlesearch.Replace("Û", "%db")
            titlesearch = titlesearch.Replace("Ü", "%dc")
            titlesearch = titlesearch.Replace("Ý", "%dd")
            titlesearch = titlesearch.Replace("Þ", "%de")
            titlesearch = titlesearch.Replace("ß", "%df")
            titlesearch = titlesearch.Replace("à", "%e0")
            titlesearch = titlesearch.Replace("á", "%e1")
            titlesearch = titlesearch.Replace("â", "%e2")
            titlesearch = titlesearch.Replace("ã", "%e3")
            titlesearch = titlesearch.Replace("ä", "%e4")
            titlesearch = titlesearch.Replace("å", "%e5")
            titlesearch = titlesearch.Replace("æ", "%e6")
            titlesearch = titlesearch.Replace("ç", "%e7")
            titlesearch = titlesearch.Replace("è", "%e8")
            titlesearch = titlesearch.Replace("é", "%e9")
            titlesearch = titlesearch.Replace("ê", "%ea")
            titlesearch = titlesearch.Replace("ë", "%eb")
            titlesearch = titlesearch.Replace("ì", "%ec")
            titlesearch = titlesearch.Replace("í", "%ed")
            titlesearch = titlesearch.Replace("î", "%ee")
            titlesearch = titlesearch.Replace("ï", "%ef")
            titlesearch = titlesearch.Replace("ð", "%f0")
            titlesearch = titlesearch.Replace("ñ", "%f1")
            titlesearch = titlesearch.Replace("ò", "%f2")
            titlesearch = titlesearch.Replace("ó", "%f3")
            titlesearch = titlesearch.Replace("ô", "%f4")
            titlesearch = titlesearch.Replace("õ", "%f5")
            titlesearch = titlesearch.Replace("ö", "%f6")
            titlesearch = titlesearch.Replace("÷", "%f7")
            titlesearch = titlesearch.Replace("ø", "%f8")
            titlesearch = titlesearch.Replace("ù", "%f9")
            titlesearch = titlesearch.Replace("ú", "%fa")
            titlesearch = titlesearch.Replace("û", "%fb")
            titlesearch = titlesearch.Replace("ü", "%fc")
            titlesearch = titlesearch.Replace("ý", "%fd")
            titlesearch = titlesearch.Replace("þ", "%fe")
            titlesearch = titlesearch.Replace("ÿ", "%ff")
            titlesearch = titlesearch.Replace("&", "%26")
            titlesearch = titlesearch.Replace("++", "+")
            If goodyear = True Then
                titlesearch = titlesearch & "+" & year
            End If
            url = url & titlesearch & "%3E+site%3Aimdb.com&meta="

            Dim webpage As String = loadwebpage(url, True)


            'www.imdb.com/title/tt0402022
            If webpage.IndexOf("www.imdb.com/title/tt") <> -1 Then
                newimdbid = webpage.Substring(webpage.IndexOf("www.imdb.com/title/tt") + 19, 9)
            End If

            If newimdbid <> "" And newimdbid.IndexOf("tt") = 0 And newimdbid.Length = 9 Then
                allok = True
            Else
                newimdbid = getimdbID_fromimdb(title, imdbmirror, year)
                If newimdbid <> "" And newimdbid.IndexOf("tt") = 0 And newimdbid.Length = 9 Then
                    allok = True
                End If
            End If

            If allok = True Then
                Return newimdbid
            Else
                Return "NA"
            End If
        Catch
            Return "Error"
        Finally
            Monitor.Exit(Me)
        End Try

    End Function


    Property Html As String=""


    'ReadOnly Property Stars_Old As String
    '    Get
    '        Dim s As String=""
    '        Dim context = Regex.Match(Html,MovieRegExs.REGEX_STARS, RegexOptions.Singleline).ToString

    '        If context = "" Then Return ""
            
    '        Dim star=""

    '        For Each m As Match In Regex.Matches(context, MovieRegExs.REGEX_HREF_PATTERN, RegexOptions.Singleline) 

    '            star=Net.WebUtility.HtmlDecode(m.Groups("name").Value)

    '            star.ExtractName

    '            If star.ToLower.IndexOf("see full cast and crew")>-1 Then Continue For

    '            s.AppendValue(star)
    '        Next   

    '        Return s
    '    End Get
    'End Property
   

    ReadOnly Property Stars As String
        Get
            Dim s       As String = ""
            Dim context As String = Regex.Match(Html,MovieRegExs.REGEX_STARS, RegexOptions.Singleline).ToString

            Dim lst = From M As Match In Regex.Matches(context, MovieRegExs.REGEX_HREF_PATTERN, RegexOptions.Singleline) 
                        Where Not M.Groups("name").ToString.ToLower.Contains("see full cast and crew") 
                        Select Net.WebUtility.HtmlDecode(M.Groups("name").ToString)

            s.AppendList(lst)

            Return s
        End Get
    End Property



    ReadOnly Property Outline As String
        Get
            Return Regex.Match(Html,MovieRegExs.REGEX_OUTLINE, RegexOptions.Singleline).Groups(1).Value.Trim
        End Get
    End Property


    ReadOnly Property TitleAndYear As String
        Get
            Return Regex.Match(Html,MovieRegExs.REGEX_TITLE_AND_YEAR, RegexOptions.Singleline).ToString.Trim
        End Get
    End Property
   

    ReadOnly Property Title As String
        Get
            Dim s As String = ""

            If Preferences.Original_Title Then
                s=Original_Title
            End If
            
            s = s.Replace("""", "")

            If s="" Then  
                s=Regex.Match(TitleAndYear,MovieRegExs.REGEX_TITLE, RegexOptions.Singleline).Groups(1).Value
            End If

            Return s
        End Get

    End Property
   

    ReadOnly Property Year As String
        Get
            Return Regex.Match(TitleAndYear,MovieRegExs.REGEX_YEAR, RegexOptions.Singleline).Groups(1).Value
        End Get
    End Property
   

    ReadOnly Property Original_Title As String
        Get
            Return Regex.Match(Html,MovieRegExs.REGEX_ORIGINAL_TITLE, RegexOptions.Singleline).Groups(1).Value.Trim
        End Get
    End Property
   

   

    ReadOnly Property Genres As String
        Get
            Dim s As String=""
            Dim D = 0
            Dim W = 0

            D = Html.IndexOf("<h4 class=""inline"">Genres:</h4>")

            If Not D <= 0 Then
                W = Html.IndexOf("</div>", D)

                Dim rGenres As MatchCollection = Regex.Matches(Html.Substring(D, W - D), MovieRegExs.REGEX_HREF_PATTERN, RegexOptions.Singleline)

                Dim lst = From M As Match In rGenres Select N = M.Groups("name").ToString Where Not N.Contains("more")

                s.AppendList(lst, " / ")

                Return s
            End If

            Return s
        End Get
    End Property


    ReadOnly Property Directors As String
        Get
            Dim s As String=""

            Dim D = Html.IndexOf("itemprop=""director""")

            Dim W = If(D > 0, Html.IndexOf("</div>", D), 0)

            If Not D <= 0 And Not W <= 0 Then
                Dim rDir As MatchCollection = Regex.Matches(Html.Substring(D, W - D), MovieRegExs.REGEX_HREF_PATTERN)
                Dim lst = From M As Match In rDir Where Not M.Groups("name").ToString.Contains("more") _
                             Select Net.WebUtility.HtmlDecode(M.Groups("name").ToString)

                s.AppendList(lst, " / ")
            End If

            Return s
        End Get
    End Property


    'Studio = Production
    ReadOnly Property Studio As String
        Get
            Return GetNames(MovieRegExs.REGEX_STUDIO,Preferences.MovieScraper_MaxStudios)
        End Get
    End Property


    'NB Credits = Writer
    ReadOnly Property Credits As String
        Get
            Return GetNames(MovieRegExs.REGEX_CREDITS)
        End Get
    End Property


    'ReadOnly Property Plot As String
    '    Get
    '        Return Regex.Match(Html,MovieRegExs.REGEX_PLOT, RegexOptions.Singleline).ToString.Trim.StripHRef
    '    End Get
    'End Property


    ReadOnly Property ReleaseDate As String
        Get
            Dim s=""

            Dim RelDate As Date
            Dim sRelDate As String = Regex.Match(Regex.Match(Html, MovieRegExs.REGEX_RELEASE_DATE, RegexOptions.Singleline).Groups("date").ToString.Replace("&nbsp;"," "), "\d+\s\w+\s\d\d\d\d\s").ToString

            If Not sRelDate = "" Then
                If Date.TryParse(sRelDate, RelDate) Then
                    s = RelDate.ToString("yyyy-MM-dd")
                End If
            End If

            Return s
        End Get
    End Property


    Function GetNames(RegExPattern As String, Optional ByVal Max As Integer=-1) As String
        Dim s As String=""
        Dim context = Regex.Match(Html,RegExPattern, RegexOptions.Singleline).ToString

        If context = "" Then Return ""
            
        Dim name=""

        If Max=-1 Then Max=999 
        Dim i As Integer=0

        For Each m As Match In Regex.Matches(context, MovieRegExs.REGEX_NAME, RegexOptions.Singleline) 

            name=Net.WebUtility.HtmlDecode(m.Groups("name").Value)

            s.AppendValue(name)
            i += 1
            If i=Max Then Exit For
        Next   

        Return s
    End Function
    

    Public Function getimdbbody(Optional ByVal title As String = "", Optional ByVal year As String = "", Optional ByVal imdbid As String = "", Optional ByVal imdbmirror As String = "", Optional ByVal imdbcounter As Integer = 0)
        Monitor.Enter(Me)

        Dim totalinfo As String = ""
        Dim webcounter As Integer

        Try
          
            Dim first As Integer
            Dim tempstring As String
            Dim actors(10000, 3)
            Dim actorcount As Integer = 0
            Dim last As Integer
            Dim length As Integer
            Dim tempint As Integer
            Dim mpaacount As Integer = -1
            Dim webpage As New List(Of String)
            Dim mpaaresults(33, 1) As String
            Dim OriginalTitle As Boolean = False
            Dim FoundTitle As Boolean = False
            mpaaresults(0, 0) = "MPAA"
            mpaaresults(1, 0) = "UK"
            mpaaresults(2, 0) = "USA"
            mpaaresults(3, 0) = "Ireland"
            mpaaresults(4, 0) = "Australia"
            mpaaresults(5, 0) = "New Zealand"
            mpaaresults(6, 0) = "Norway"
            mpaaresults(7, 0) = "Singapore"
            mpaaresults(8, 0) = "South Korea"
            mpaaresults(9, 0) = "Philippines"
            mpaaresults(10, 0) = "Brazil"
            mpaaresults(11, 0) = "Netherlands"
            mpaaresults(12, 0) = "Malaysia"
            mpaaresults(13, 0) = "Argentina"
            mpaaresults(14, 0) = "Iceland"
            mpaaresults(15, 0) = "Quebec"
            mpaaresults(16, 0) = "British Columbia"
            mpaaresults(17, 0) = "Nova Scotia"
            mpaaresults(18, 0) = "Peru"
            mpaaresults(19, 0) = "Sweden"
            mpaaresults(20, 0) = "Portugal"
            mpaaresults(21, 0) = "South Africa"
            mpaaresults(22, 0) = "Denmark"
            mpaaresults(23, 0) = "Hong Kong"
            mpaaresults(24, 0) = "Finland"
            mpaaresults(25, 0) = "India"
            mpaaresults(26, 0) = "Mexico"
            mpaaresults(27, 0) = "France"
            mpaaresults(28, 0) = "Italy"
            mpaaresults(29, 0) = "canton of Vaud"
            mpaaresults(30, 0) = "canton of Geneva"
            mpaaresults(31, 0) = "Germany"
            mpaaresults(32, 0) = "Greece"
            mpaaresults(33, 0) = "Austria"

            Dim movienfoarray As String = String.Empty

            Dim genre(20)
            Dim thumbs(500)

            Dim allok As Boolean = False
            If imdbid <> Nothing Then
                If imdbid.Length = 9 And imdbid.IndexOf("tt") <> -1 Then
                    allok = True
                End If
            End If
            totalinfo = "<movie>" & vbCrLf
            If allok = False Then
                If imdbcounter < 50 Then
                    imdbid = getimdbID(title, year)
                Else
                    imdbid = getimdbID_fromimdb(title, imdbmirror, year)
                End If
                If imdbid <> "" And imdbid.IndexOf("tt") = 0 And imdbid.Length = 9 Then
                    allok = True
                End If
            End If
            If allok = False Then
                Return "MIC"
                Exit Function
            End If
            If allok = True Then
                tempstring = imdbmirror & "title/" & imdbid
                webpage.Clear()
                webpage = loadwebpage(tempstring, False)

                Dim webPg As String = String.Join( "" , webpage.ToArray() )
                Html = webPg
            

                totalinfo.AppendTag( "genre"     , Genres      )
                totalinfo.AppendTag( "director"  , Directors   )
                totalinfo.AppendTag( "credits"   , Credits     )
                totalinfo.AppendTag( "premiered" , ReleaseDate )
                totalinfo.AppendTag( "stars"     , Stars       )
                totalinfo.AppendTag( "title"     , Me.Title    )
                totalinfo.AppendTag( "year"      , Me.Year     )
                totalinfo.AppendTag( "studio"    , Studio      )
                totalinfo.AppendTag( "outline"   , Outline     )


                For f = 0 To webpage.Count - 1
                    webcounter = f
                    If webcounter > webpage.Count - 10 Then Exit For

                    'rating
                    If totalinfo.IndexOf("<rating>") = -1 Then
                        If webpage(f).IndexOf("<span itemprop=""ratingValue") <> -1 Then
                            Try
                                Dim M As Match = Regex.Match(webpage(f), "<span itemprop=""ratingValue"">(\d.\d)</span>")
                                If M.Success = True Then
                                    movienfoarray = M.Groups(1).Value
                                Else
                                    movienfoarray = "scraper error"
                                End If
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<rating>" & movienfoarray & "</rating>" & vbCrLf
                            Catch
                                totalinfo = totalinfo & "<rating>scraper error</rating>" & vbCrLf
                            End Try
                        End If
                    End If

                    If webpage(f).IndexOf("<strong>Top 250 #") <> -1 Then
                        Try
                            first = webpage(f).IndexOf("Top 250 #")
                            last = webpage(f).IndexOf("</strong></a>")
                            length = last - first
                            movienfoarray = webpage(f).Substring(first + 9, webpage(f).LastIndexOf("</strong>") - (first + 9))
                            totalinfo = totalinfo & "<top250>" & movienfoarray & "</top250>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<top250>scraper error</top250>" & vbCrLf
                        End Try
                    End If


                    'tagline
                    If webpage(f).IndexOf("<h4 class=""inline"">Tagline") <> -1 Then
                        Try
                            movienfoarray = webpage(f + 1)

                            movienfoarray = Regex.Replace(movienfoarray, "<.*?>", "").Trim

                            movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)

                '            movienfoarray = movienfoarray.Trim()
                            totalinfo = totalinfo & "<tagline>" & movienfoarray & "</tagline>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<tagline>scraper error</tagline>" & vbCrLf
                        End Try
                    End If

                    'runtime
                    'If webpage(f).IndexOf("itemprop=""duration") <> -1 Then
                    '    movienfoarray = ""
                    '    Try
                    '        Dim M As Match = Regex.Match(webpage(f), ">(\d+ min)</time>")
                    '        If M.Success = True Then
                    '            movienfoarray = M.Groups(1).Value
                    '        Else
                    '            movienfoarray = "scraper error"
                    '        End If

                    '        movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                    '        movienfoarray = encodespecialchrs(movienfoarray)
                    '        totalinfo = totalinfo & "<runtime>" & movienfoarray & "</runtime>" & vbCrLf
                    '    Catch
                    '        totalinfo = totalinfo & "<runtime>scraper error</runtime>" & vbCrLf
                    '    End Try
                    'End If

                    If webpage(f).IndexOf("itemprop=""duration") <> -1 Then
                        movienfoarray = ""
                        Try
                            Dim M As Match = Regex.Match(webpage(f), ">(\d+ min)</time>")

                            If M.Success Then
                                movienfoarray = M.Groups(1).Value
                                movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<runtime>" & movienfoarray & "</runtime>" & vbCrLf
                            End If
                        Catch
                        End Try
                    End If

                    If webpage(f).IndexOf("<div class=""infobar"">") <> -1 Then
                        Try
                            If webpage(f + 1) <> "" and webpage(f + 1).IndexOf("<img width=") <> 0 Then
                                movienfoarray = webpage(f + 1).Substring(0, webpage(f + 1).IndexOf("min") + 3)
                                movienfoarray = movienfoarray.Replace("min", "")
                                movienfoarray = movienfoarray.Trim(" ")
                                If Not IsNumeric(movienfoarray) Then
                                    For h = 0 To movienfoarray.Length - 1
                                        If IsNumeric(movienfoarray.Substring(h, 1)) Then
                                            movienfoarray = movienfoarray.Substring(h, movienfoarray.Length - h)
                                            Exit For
                                        End If
                                    Next
                                End If
                                If movienfoarray <> "" Then
                                    movienfoarray = movienfoarray & " min"
                                End If
                                movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<runtime>" & movienfoarray & "</runtime>" & vbCrLf
                            End If
                        Catch
                        End Try
                    End If

                    'votes
                    If webpage(f).IndexOf("itemprop=""ratingCount""") <> -1 Then
                        Try
                            Dim M As Match = Regex.Match(webpage(f), "<span itemprop=""ratingCount"">([\d{1,3},?]*[0-9]?)</span>")
                            If M.Success = True Then
                                movienfoarray = M.Groups(1).Value
                            Else
                                movienfoarray = "scraper error"
                            End If
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<votes>" & movienfoarray & "</votes>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<votes>scraper error</votes>" & vbCrLf
                        End Try
                    End If

                    'outline
                    ''If webpage(f).IndexOf("<p>") <> -1 Then
'                    If totalinfo.IndexOf("<outline>") = -1 Then
'                        If webpage(f).IndexOf("itemprop=""description""") <> -1 Then
'                            Try
'                                movienfoarray = ""
'                                Dim endofoutline = f
'                                For endofoutline = (f) To webpage.Count - 2
'                                    movienfoarray = movienfoarray & webpage(endofoutline)
'                                    If webpage(endofoutline).IndexOf("</p>") <> -1 Then
'                                        Exit For
'                                    End If
'                                Next
'                                If movienfoarray.Length > 0 Then

''                                   Dim M As Match = Regex.Match(movienfoarray, "<p itemprop=""description"">(.+?)(<a|</p)")
'                                    Dim M As Match = Regex.Match(movienfoarray, "<p itemprop=""description"">(.+?)(</p)")
'                                    If M.Success = True Then
'                                        movienfoarray = M.Groups(1).Value.StripTagsLeaveContent.Replace("See full summary »","").Trim
'                                    Else
'                                        movienfoarray = "scraper error"
'                                    End If
'                           '        movienfoarray = Regex.Replace(movienfoarray, "<.*?>", "").Trim
'                                    movienfoarray = Utilities.cleanSpecChars(movienfoarray)
'                                    movienfoarray = encodespecialchrs(movienfoarray)
'                                    totalinfo = totalinfo & "<outline>" & movienfoarray & "</outline>" & vbCrLf
'                                Else
'                                    totalinfo = totalinfo & "<outline>scaper error: possible format change</outline>" & vbCrLf
'                                End If
'                            Catch
'                                totalinfo = totalinfo & "<outline>scraper error</outline>" & vbCrLf
'                            End Try

'                            If totalinfo.IndexOf("<plot>") = -1 Then totalinfo = totalinfo & "<plot></plot>" & vbCrLf
'                        End If
'                    End If



                    ''studio
                    'If webpage(f).IndexOf("<h4 class=""inline"">Production") <> -1 Then
                    '    Try
                    '        movienfoarray = ""
                    '        For g = 1 To 5
                    '            If webpage(f + g).IndexOf("<a") <> -1 Then
                    '                movienfoarray = webpage(f + g).Substring(webpage(f + g).IndexOf(">") + 1, webpage(f + g).IndexOf("</a>") - webpage(f + g).IndexOf(">") - 1)
                    '                Exit For
                    '            End If
                    '        Next
                    '        movienfoarray = movienfoarray.Trim()
                    '        movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                    '        movienfoarray = encodespecialchrs(movienfoarray)
                    '        totalinfo = totalinfo & "<studio>" & movienfoarray & "</studio>" & vbCrLf
                    '        'Exit For
                    '    Catch
                    '        totalinfo = totalinfo & "<studio>scraper error</studio>" & vbCrLf
                    '    End Try
                    'End If

                    'country
                    If webpage(f).IndexOf("class=""inline"">Countr") <> -1 Then
                        Try
                            tempstring = ""
                            For g = 1 To 5
                                If webpage(f + g).IndexOf("</div>") <> -1 Then Exit For
                                Dim M As Match = Regex.Match(webpage(f + g), ">(.+)</a>")
                                If M.Success = True Then
                                    movienfoarray = M.Groups(1).Value
                                    movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                                    movienfoarray = encodespecialchrs(movienfoarray)
                                    tempstring = tempstring & "<country>" & movienfoarray & "</country>" & vbCrLf
                                    Exit For
                                End If
                            Next
                            totalinfo = totalinfo & tempstring
                        Catch
                            totalinfo = totalinfo & "<country>scraper error</country>" & vbCrLf
                        End Try
                    End If
                Next

                totalinfo = totalinfo & "<id>" & imdbid & "</id>" & vbCrLf
                'insert imdbid 

                For f = 0 To 33
                    If mpaaresults(f, 1) <> Nothing Then
                        Try
                            mpaaresults(f, 0) = encodespecialchrs(mpaaresults(f, 0))
                            mpaaresults(f, 1) = encodespecialchrs(mpaaresults(f, 1))
                            totalinfo = totalinfo & "<cert>" & mpaaresults(f, 0) & "|" & mpaaresults(f, 1) & "</cert>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<cert>scraper error|scraper error</cert>"
                        End Try
                    End If
                Next

                
                Try
                    tempstring = imdbmirror & "title/" & imdbid & "/plotsummary"
                    Dim plots(20) As String
                    webpage.Clear()
                    webpage = loadwebpage(tempstring, False)
                    tempint = 0
                    Dim doo As Boolean = False
                    For Each line In webpage
                        If doo = True Then
                            plots(tempint) = line
                            doo = False
                        End If
                        If line.IndexOf("plotpar") <> -1 Then
                            tempint = tempint + 1
                            doo = True
                        End If
                    Next
                    Dim sizes(tempint) As Integer
                    Dim biggest As Integer = 1
                    For f = 1 To tempint
                        If plots(f).Length > plots(biggest).Length Then
                            biggest = f
                        End If
                    Next
                    If plots(biggest) <> Nothing Then

                        movienfoarray = plots(biggest).StripTagsLeaveContent

                        'If movienfoarray.IndexOf("<a href=") <> -1 Then
                        '    Do Until movienfoarray.IndexOf("<a href=") = -1
                        '        first = movienfoarray.LastIndexOf("<a href=")
                        '        last = movienfoarray.LastIndexOf("/"">")
                        '        tempstring = movienfoarray.Substring(first, last - first + 3)
                        '        movienfoarray = movienfoarray.Replace(tempstring, "")
                        '    Loop
                        '    movienfoarray = movienfoarray.Replace("</a>", "")
                        'End If

                        movienfoarray = Regex.Replace(movienfoarray, "<.*?>", "").Trim
                        movienfoarray = Utilities.cleanSpecChars(movienfoarray)
                        movienfoarray = encodespecialchrs(movienfoarray)
'                        totalinfo = totalinfo.Replace("<plot></plot>", "<plot>" & movienfoarray & "</plot>")
                        totalinfo &= "<plot>" & movienfoarray & "</plot>"
                    End If
                Catch
                    totalinfo = totalinfo & "<plot>scraper error</plot>"
                End Try


                'certs & mpaa
                Try
                    tempstring = imdbmirror & "title/" & imdbid & "/parentalguide#certification"
                    webpage.Clear()
                    webpage = loadwebpage(tempstring, False)
                    For f = 0 To webpage.Count - 1
                        'mpaa
                        If webpage(f).IndexOf("<a href=""/mpaa") <> -1 Then
                            tempstring = webpage(f + 2)
                            If tempstring.IndexOf("<") = -1 Then
                                For g = 0 To 33
                                    If mpaaresults(g, 0) = "MPAA" Then
                                        mpaaresults(g, 1) = tempstring
                                        Exit For
                                    End If
                                Next
                            End If
                        End If
                        'cert
                        If f > 1 Then
                            For g = 0 To 33
                                tempstring = """>" & mpaaresults(g, 0) & ":"
                                If webpage(f).IndexOf("certificates=") <> -1 And webpage(f).IndexOf(tempstring) <> -1 Then
                                    tempstring = webpage(f).Substring(webpage(f).IndexOf(tempstring), webpage(f).Length - webpage(f).IndexOf(tempstring))
                                    tempstring = tempstring.Substring(tempstring.IndexOf(">") + 1, tempstring.IndexOf("</a>") - tempstring.IndexOf(">") - 1)
                                    mpaaresults(g, 1) = tempstring
                                    Try
                                        'line below determines if cert is full or short as e.g. UK:15 becomes 15
                                        If Not Preferences.scrapefullcert Then
                                            mpaaresults(g, 1) = mpaaresults(g, 1).Substring(mpaaresults(g, 1).IndexOf(":") + 1, mpaaresults(g, 1).Length - mpaaresults(g, 1).IndexOf(":") - 1)
                                        End If

                                        mpaaresults(g, 1) = encodespecialchrs(mpaaresults(g, 1))
                                    Catch
                                        mpaaresults(g, 1) = "error"
                                    End Try
                                End If
                            Next
                        End If
                    Next
                Catch
                End Try


                Try
                    'releaseinfo#akas
                    tempstring = imdbmirror & "title/" & imdbid & "/releaseinfo#akas"
                    webpage.Clear()
                    webpage = loadwebpage(tempstring, False)
                    For f = 0 To webpage.Count - 1
                        If webpage(f).IndexOf("<h4 class=""li_group"">Also Known As (AKA)") <> -1 Then    '"<h5><a name=""akas"">Also Known As"
                            Dim loc As Integer = f
                            Dim ignore As Boolean = False
                            For g = loc To loc + 500
                                If webpage(g).IndexOf("</table>") <> -1 Then
                                    Exit For
                                End If
                                Dim skip As Boolean = Not ignore
                                If webpage(g).IndexOf("<td>") <> -1 Then    'And ignore = True Then
                                    ignore = Not ignore
                                End If

                                If webpage(g).IndexOf("<td>") <> -1 And skip = False Then
                                    If webpage(g - 1).IndexOf("Greece") = -1 And webpage(g - 1).IndexOf("Russia") = -1 Then
                                        tempstring = webpage(g)
                                        tempstring = LTrim(tempstring)
                                        tempstring = tempstring.Replace("<td>", "")
                                        tempstring = tempstring.Replace("</td>", "")
                                        tempstring = Utilities.cleanSpecChars(tempstring)
                                        tempstring = encodespecialchrs(tempstring)
                                        totalinfo = totalinfo & "<alternativetitle>" & tempstring & "</alternativetitle>" & vbCrLf
                                    Else
                                        g = g + 1
                                    End If
                                    ignore = False
                                End If
                            Next
                        End If
                    Next
                Catch ex As Exception

                End Try


            End If
            For f = 0 To 33
                If mpaaresults(f, 1) <> Nothing Then
                    Try
                        mpaaresults(f, 0) = Utilities.cleanSpecChars(mpaaresults(f, 0))
                        mpaaresults(f, 1) = Utilities.cleanSpecChars(mpaaresults(f, 1))
                        mpaaresults(f, 0) = encodespecialchrs(mpaaresults(f, 0))
                        mpaaresults(f, 1) = encodespecialchrs(mpaaresults(f, 1))
                        totalinfo = totalinfo & "<cert>" & mpaaresults(f, 0) & "|" & mpaaresults(f, 1) & "</cert>" & vbCrLf
                    Catch
                        totalinfo = totalinfo & "<cert>scraper error|scraper error</cert>"
                    End Try
                End If
            Next
            totalinfo = totalinfo & "</movie>" & vbCrLf
            Return totalinfo
        Catch ex As Exception
            totalinfo = totalinfo & "</movie>" & vbCrLf
            Return totalinfo
            'MsgBox(ex.ToString & vbCrLf & vbCrLf & "error 354")
        Finally
            Monitor.Exit(Me)
        End Try
    End Function



    Public Function getimdbactors(ByVal imdbmirror As String, Optional ByVal imdbid As String = "", Optional ByVal title As String = "", Optional ByVal maxactors As Integer = 9999) As String
        Dim webpage As New List(Of String)
        Dim actors(5000, 3)
        Dim tempstring As String
        Dim filterstring As String
        Dim actorcount As Integer
        Dim totalinfo As String = "<actorlist>"

        Try
            Monitor.Enter(Me)

            tempstring = imdbmirror & "title/" & imdbid & "/fullcredits#cast"
            webpage.Clear()
            webpage = loadwebpage(tempstring, False)


            Dim scrapertempint As Integer
            Dim scrapertempstring As String



            For Each line In webpage
                If line.IndexOf("Cast</a>") <> -1 Then
                    line = line.Substring(line.IndexOf("Cast</a>"), line.Length - line.IndexOf("Cast</a>"))
                    If line.IndexOf("<tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>") <> -1 Then
                        line = line.Replace("</td></tr> <tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>", "</td></tr><tr class")
                    End If
                    line = line.Substring(0, line.IndexOf("</table>"))
                    scrapertempint = 0
                    scrapertempstring = line
                    Do Until scrapertempstring.IndexOf("<tr class=""") = -1
                                
                        '22Feb12 - AnotherPhil - Bug fix actor scraping
                        scrapertempstring = Right( scrapertempstring, scrapertempstring.Length - scrapertempstring.IndexOf("<tr class=""") )							
                            
                        scrapertempint = scrapertempint + 1
                        actors(scrapertempint, 0) = scrapertempstring.Substring(0, scrapertempstring.IndexOf("</td></tr>") + 10)
                        scrapertempstring = scrapertempstring.Replace(actors(scrapertempint, 0), "")
                        filterstring = actors(scrapertempint, 0)
                        If filterstring <> actors(scrapertempint, 0) Then actors(scrapertempint, 0) = filterstring
                        If actors(scrapertempint, 0).IndexOf("other cast") <> -1 Then
                            actors(scrapertempint, 0) = Nothing
                            scrapertempint -= 1
                        End If
                    Loop
                    If scrapertempstring <> "" Then
                        scrapertempint = scrapertempint + 1
                        actors(scrapertempint, 0) = scrapertempstring
                    End If
                    actorcount = scrapertempint
                    If actorcount > maxactors Then
                        actorcount = maxactors
                    End If
                    For g = 1 To actorcount
                        Try
                            actors(g, 3) = actors(g, 0).substring(actors(g, 0).indexof("<a href=""/name/nm") + 15, 9)
                            If actors(g, 0).IndexOf("http://resume.imdb.com") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://resume.imdb.com", "")
                            If actors(g, 0).IndexOf("http://i.media-imdb.com/images/tn15/addtiny.gif") <> -1 Then actors(g, 0) = actors(g, 0).Replace("http://i.media-imdb.com/images/tn15/addtiny.gif", "")
                            If actors(g, 0).indexof("http://ia.media-imdb.com/images/") <> -1 Then
                                Dim tempint6 As Integer
                                Dim tempint7 As Integer
                                tempint6 = actors(g, 0).indexof("http://ia.media-imdb.com/images/")
                                tempint7 = actors(g, 0).indexof("._V1._")
                                actors(g, 2) = actors(g, 0).substring(tempint6, tempint7 - tempint6 + 3)
                                actors(g, 2) = actors(g, 2) & "._V1._SY400_SX300_.jpg"
                            End If

                            If actors(g, 0).IndexOf("</td></tr></table>") <> -1 Then
                                scrapertempint = actors(g, 0).IndexOf("</td></tr></table>")
                                scrapertempstring = actors(g, 0).Substring(scrapertempint, actors(g, 0).Length - scrapertempint)
                                actors(g, 0) = actors(g, 0).Replace(scrapertempstring, "</td></tr><tr class")
                            End If

                            If actors(g, 0).IndexOf("a href=""/character") <> -1 Then
                                actors(g, 1) = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character") + 19, actors(g, 0).lastIndexOf("</td></tr>") - actors(g, 0).IndexOf("a href=""/character") - 19)
                                If actors(g, 1).IndexOf("</a>") <> -1 Then
                                    actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).IndexOf("</a>") - 12)
                                ElseIf actors(g, 1).IndexOf("</a>") = -1 Then
                                    actors(g, 1) = actors(g, 1).Substring(12, actors(g, 1).Length - 12)
                                End If
                                scrapertempstring = actors(g, 0).Substring(actors(g, 0).IndexOf("a href=""/character"), actors(g, 0).Length - actors(g, 0).IndexOf("a href=""/character"))
                                actors(g, 0) = actors(g, 0).Replace(scrapertempstring, "")
                                actors(g, 0) = actors(g, 0).Substring(0, actors(g, 0).lastindexof("</a>"))
                                actors(g, 0) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
                            ElseIf actors(g, 0).IndexOf("a href=""/character") = -1 Then
                                actors(g, 0) = actors(g, 0).substring(0, actors(g, 0).length - 10)
                                actors(g, 1) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
                                actors(g, 0) = actors(g, 0).Substring(0, actors(g, 0).lastindexof("</a>"))
                                actors(g, 0) = actors(g, 0).substring(actors(g, 0).lastindexof(">") + 1, actors(g, 0).length - actors(g, 0).lastindexof(">") - 1)
                            End If
                        Catch
                            Exit For
                        End Try
                    Next
                End If

            Next
            For f = 1 To actorcount
                If actors(f, 0) <> Nothing Then
                    totalinfo = totalinfo & "<actor>" & vbCrLf
                    actors(f, 0) = Utilities.cleanSpecChars(actors(f, 0))
                    actors(f, 0) = encodespecialchrs(actors(f, 0))
                    totalinfo = totalinfo & "<name>" & actors(f, 0) & "</name>" & vbCrLf
                    If actors(f, 1) <> Nothing Then
                        actors(f, 1) = Utilities.cleanSpecChars(actors(f, 1))
                        actors(f, 1) = encodespecialchrs(actors(f, 1))
                        totalinfo = totalinfo & "<role>" & actors(f, 1) & "</role>" & vbCrLf
                    End If
                    If actors(f, 2) <> Nothing Then
                        actors(f, 2) = encodespecialchrs(actors(f, 2))
                        totalinfo = totalinfo & "<thumb>" & actors(f, 2) & "</thumb>" & vbCrLf
                    End If
                    If actors(f, 3) <> Nothing Then
                        totalinfo = totalinfo & "<actorid>" & actors(f, 3) & "</actorid>" & vbCrLf
                    End If
                    totalinfo = totalinfo & "</actor>" & vbCrLf
                End If
            Next
            totalinfo = totalinfo & "</actorlist>"
            Return totalinfo
        Catch ex As Exception

        Finally
            Monitor.Exit(Me)
        End Try


        Return "Error"
    End Function
    Public Function gettrailerurl(ByVal imdbid As String, ByVal imdbmirror As String) As String
        Monitor.Enter(Me)
        Dim allok As Boolean = False
        Dim first As Integer
        Dim last As Integer

        Dim tempstring As String = ""
        Try
            Dim webpage As List(Of String)
            tempstring = imdbmirror & "title/" & imdbid & "/trailers"
            webpage = loadwebpage(tempstring, False)
            For f = 0 To webpage.Count - 1
                If webpage(f).IndexOf("/screenplay/") <> -1 Then
                    first = webpage(f).IndexOf("")
                    Dim S As String = webpage(f)
                    Dim M As Match
                    M = Regex.Match(S, "\d{12}")
                    If M.Success = True Then
                        tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                    Else
                        M = Regex.Match(S, "\d{11}")
                        If M.Success = True Then
                            tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                        Else
                            M = Regex.Match(S, "\d{10}")
                            If M.Success = True Then
                                tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                            Else
                                M = Regex.Match(S, "\d{9}")
                                If M.Success = True Then
                                    tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                Else
                                    M = Regex.Match(S, "\d{8}")
                                    If M.Success = True Then
                                        tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                    Else
                                        M = Regex.Match(S, "\d{7}")
                                        If M.Success = True Then
                                            tempstring = imdbmirror & "rg/VIDEO_TITLE/GALLERY/video/screenplay/vi" & M.Value & "/player"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                    webpage(f) = webpage(f).Substring(webpage(f).IndexOf("screenplay-") + 11, 12)

                    allok = True
                    Exit For
                End If
            Next
            If allok = True Then
                allok = False
                webpage.Clear()
                webpage = loadwebpage(tempstring, False)
                For f = 0 To webpage.Count - 1
                    If webpage(f).IndexOf("www.totaleclips.com") <> -1 Then
                        first = webpage(f).IndexOf("www.totaleclips.com")
                        last = webpage(f).IndexOf(""");")
                        webpage(f) = webpage(f).Substring(first, last - first)
                        allok = True
                        webpage(f) = webpage(f).Replace("%3A", ":")
                        webpage(f) = webpage(f).Replace("%2F", "/")
                        webpage(f) = webpage(f).Replace("%3F", "?")
                        webpage(f) = webpage(f).Replace("%3D", "=")
                        webpage(f) = webpage(f).Replace("%26", "&")
                        tempstring = webpage(f)
                        tempstring = "http://" & tempstring
                        Dim totalinfo As String = "<trailer>" & tempstring & "</trailer>" & vbCrLf
                        Return tempstring
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
        Finally
            Monitor.Exit(Me)
        End Try
        Return ""
    End Function


    Private Function encodespecialchrs(ByVal text As String)
        If text.IndexOf("&") <> -1 Then text = text.Replace("&", "&amp;")
        If text.IndexOf("<") <> -1 Then text = text.Replace("<", "&lt;")
        If text.IndexOf(">") <> -1 Then text = text.Replace(">", "&gt;")
        If text.IndexOf(Chr(34)) <> -1 Then text = text.Replace(Chr(34), "&quot;")
        If text.IndexOf("'") <> -1 Then text = text.Replace("'", "&apos;")
        Return text
    End Function

    Public Function loadwebpage(ByVal Url As String, ByVal IntoSingleString As Boolean, Optional TimeoutInSecs As Integer=-1)

        Dim webpage As New List(Of String)
        Monitor.Enter(Me)

        Try
            Dim wrGETURL As WebRequest = WebRequest.Create(Url)

            If TimeoutInSecs > -1 Then wrGETURL.Timeout = TimeoutInSecs * 1000

            wrGETURL.Headers.Add("Accept-Language", TMDb.LanguageCodes(0))

            Dim myProxy As New WebProxy("myproxy", 80)


            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
'            Dim objReader As New StreamReader(objStream, System.Text.UTF8Encoding.UTF7)
            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""

            If IntoSingleString = False Then
                Do While Not sLine Is Nothing

                    sLine = objReader.ReadLine
                    If Not sLine Is Nothing Then
                        webpage.Add(sLine)
                    End If
                Loop
            Else
                sLine = objReader.ReadToEnd
            End If
            objReader.Close()

            If IntoSingleString = False Then
                Return webpage
            Else
                Return sLine
            End If

        Catch ex As WebException
            'MsgBox("Unable to load webpage " & url & vbCrLf & vbCrLf & ex.ToString)
            If IntoSingleString = False Then
                If webpage.Count > 0 Then
                    Return webpage
                Else
                    webpage.Add("error")
                    Return "error"   'webpage
                End If
            Else
                Return "error"
            End If
        Finally
            Monitor.Exit(Me)
        End Try
    End Function


    Function GetGenres( ByVal webPage As String )

        Dim genres As New List(Of String)
        Dim genre As String

        For Each m As Match In Regex.Matches( webPage, Preferences.MovieImdbGenreRegEx )

            genre = m.Groups("genre").Value

            If Not genres.Contains( genre ) then
                genres.Add( genre )
            End if

        Next   
         
        Return genres
    End Function

End Class


