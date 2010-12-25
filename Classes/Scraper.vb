
Imports System.Threading
Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions


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
            title = imdbmirror & "find?s=tt&q=" & title
            Dim urllinecount As Integer
            Dim GOT_IMDBID As String
            Dim allok As Boolean = False
            Dim websource(2000)
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
                        temps = exacttotal
                        If temps.IndexOf(movieyear) <> -1 Then
                            Dim count As Integer
                            count = CharCount(temps, movieyear)
                            If count = 1 Then
                                temps = exacttotal.Substring(0, exacttotal.IndexOf(movieyear) + 6)
                                Dim first As Integer
                                Dim length As Integer
                                Dim calc As String
                                calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                                If temps.IndexOf("&#34;") = -1 Then
                                    first = temps.LastIndexOf("href=""/title/tt") + 13
                                    length = 9
                                    GOT_IMDBID = temps.Substring(first, length)
                                End If
                            End If
                        End If

                        If GOT_IMDBID = "" And populartotal <> "" Then
                            temps = populartotal
                            If temps.IndexOf(movieyear) <> -1 Then
                                Dim count As Integer
                                count = CharCount(temps, movieyear)
                                If count = 1 Then
                                    temps = populartotal.Substring(0, exacttotal.IndexOf(movieyear) + 6)
                                    Dim first As Integer
                                    Dim length As Integer
                                    Dim calc As String
                                    calc = temps.Substring(temps.LastIndexOf("href=""/title/tt"), temps.Length - temps.LastIndexOf("href=""/title/tt"))
                                    If temps.IndexOf("&#34;") = -1 Then
                                        first = temps.LastIndexOf("href=""/title/tt") + 13
                                        length = 9
                                        GOT_IMDBID = temps.Substring(first, length)
                                    End If
                                End If
                            End If
                        End If
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

    Public Function CharCount(ByVal OrigString As String, _
  ByVal Chars As String, Optional ByVal CaseSensitive As Boolean = False) _
  As Long

        Dim lLen As Long
        Dim lCharLen As Long
        Dim lAns As Long
        Dim sInput As String
        Dim sChar As String
        Dim lCtr As Long
        Dim lEndOfLoop As Long
        Dim bytCompareType As Byte

        sInput = OrigString
        If sInput = "" Then Exit Function
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
    Public Function getimdbbody(Optional ByVal title As String = "", Optional ByVal year As String = "", Optional ByVal imdbid As String = "", Optional ByVal imdbmirror As String = "", Optional ByVal imdbcounter As Integer = 0)
        Monitor.Enter(Me)

        Dim totalinfo As String = ""
        Dim webcounter As Integer

        Try
          
            Dim first As Integer
            Dim tempstring As String
            Dim tempstring2 As String
            Dim actors(10000, 3)
            Dim actorcount As Integer = 0
            Dim filterstring As String
            Dim last As Integer
            Dim length As Integer
            Dim thumburl As String
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

            Dim movienfoarray As String

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
                For f = 0 To webpage.Count - 1
                    webcounter = f
                    If webcounter > webpage.Count - 10 Then Exit For
                    If (webpage(f).IndexOf("<title>") <> -1) And (OriginalTitle = False) Then
                        Try
                            Dim movieyear As String = ""
                            movienfoarray = webpage(f)
                            filterstring = movienfoarray
                            movienfoarray = movienfoarray.Replace("<title>", "")
                            movienfoarray = movienfoarray.Replace("</title>", "")
                            If movienfoarray.IndexOf("(TV)") <> -1 Then
                                movienfoarray = movienfoarray.Replace("(TV)", "")

                            End If
                            If movienfoarray.IndexOf("(VG)") <> -1 Then
                                movienfoarray = movienfoarray.Replace("(VG)", "")
                            End If

                            first = movienfoarray.LastIndexOf("(")
                            If first <> -1 Then
                                If movienfoarray.Substring(first + 2, 1) = ")" Then
                                    tempstring = movienfoarray.Substring(first, 3)
                                    movienfoarray = movienfoarray.Replace(tempstring, "")
                                    first = movienfoarray.LastIndexOf(")")
                                End If
                                If first <> -1 Then
                                    first = movienfoarray.LastIndexOf(")")
                                    movieyear = movienfoarray.Substring(first - 4, 4)
                                    first = movienfoarray.LastIndexOf("(")
                                    movienfoarray = movienfoarray.Substring(0, first)
                                    movienfoarray = movienfoarray.Trim()
                                End If
                            End If

                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            movieyear = encodespecialchrs(movieyear)
                            totalinfo = totalinfo & "<title>" & movienfoarray & "</title>" & vbCrLf
                            totalinfo = totalinfo & "<year>" & movieyear & "</year>" & vbCrLf
                            movienfoarray = ""
                            FoundTitle = True
                        Catch
                            totalinfo = totalinfo & "<title>scraper error</title>" & vbCrLf
                            totalinfo = totalinfo & "<year>scraper error</year>" & vbCrLf
                        End Try
                    End If


                    ' Original Title

                    If (webpage(f).IndexOf("title-extra") <> -1) Then
                        Try
                            Dim movieyear As String = ""
                            movienfoarray = webpage(f + 1)
                            filterstring = movienfoarray
                            movienfoarray = movienfoarray.Replace("<title>", "")
                            movienfoarray = movienfoarray.Replace("</title>", "")
                            If movienfoarray.IndexOf("(TV)") <> -1 Then
                                movienfoarray = movienfoarray.Replace("(TV)", "")

                            End If
                            If movienfoarray.IndexOf("(VG)") <> -1 Then
                                movienfoarray = movienfoarray.Replace("(VG)", "")
                            End If

                            first = movienfoarray.LastIndexOf("(")
                            If first <> -1 Then
                                If movienfoarray.Substring(first + 2, 1) = ")" Then
                                    tempstring = movienfoarray.Substring(first, 3)
                                    movienfoarray = movienfoarray.Replace(tempstring, "")
                                    first = movienfoarray.LastIndexOf(")")
                                End If
                                If first <> -1 Then
                                    first = movienfoarray.LastIndexOf(")")
                                    movieyear = movienfoarray.Substring(first - 4, 4)
                                    first = movienfoarray.LastIndexOf("(")
                                    movienfoarray = movienfoarray.Substring(0, first)
                                    movienfoarray = movienfoarray.Trim()
                                End If
                            End If

                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            movieyear = encodespecialchrs(movieyear)
                            If FoundTitle = False Then
                                totalinfo = totalinfo & "<title>" & movienfoarray & "</title>" & vbCrLf
                                totalinfo = totalinfo & "<year>" & movieyear & "</year>" & vbCrLf
                            Else
                                Dim FirstOcurrence As Integer = totalinfo.IndexOf("<title>")
                                Dim SecondOcurrence As Integer = totalinfo.IndexOf("</title>")
                                Dim OldTitle As String = totalinfo.Substring(FirstOcurrence, (SecondOcurrence + 8) - FirstOcurrence)
                                totalinfo = totalinfo.Replace(OldTitle, "<title>" & movienfoarray & "</title>")
                            End If
                            OriginalTitle = True
                            movienfoarray = ""
                        Catch
                            totalinfo = totalinfo & "<title>scraper error</title>" & vbCrLf
                            totalinfo = totalinfo & "<year>scraper error</year>" & vbCrLf
                        End Try
                    End If




                    'rating
                    If webpage(f).IndexOf("<span>/10</span>") <> -1 Then
                        Try
                            movienfoarray = webpage(f)
                            webpage(f) = webpage(f).Substring(webpage(f).IndexOf(">") + 1, webpage(f).Length - webpage(f).IndexOf(">") - 1)
                            movienfoarray = webpage(f).Substring(0, 3)
                            movienfoarray = movienfoarray.Replace("<b>", "")
                            movienfoarray = movienfoarray.Replace("/<b>", "")
                            movienfoarray = movienfoarray.Replace(",", ".")
                            movienfoarray = movienfoarray.Replace(" ", "")
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<rating>" & movienfoarray & "</rating>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<rating>scraper error</rating>" & vbCrLf
                        End Try
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

                    'director
                    If webpage(f).IndexOf("<h4 class=""inline"">") <> -1 And webpage(f + 1).IndexOf("Director") <> -1 Then
                        Try
                            movienfoarray = ""
                            Dim listofdirectors As New List(Of String)
                            listofdirectors.Clear()
                            For g = 1 To 10
                                If webpage(f + g).IndexOf("<div") <> -1 Then Exit For
                                If webpage(f + g).IndexOf("Writer") <> -1 Then Exit For
                                If webpage(f + g).IndexOf("href=""/name/nm") <> -1 Then
                                    If webpage(f + g).IndexOf("/name/nm") <> webpage(f + g).LastIndexOf("/name/nm") Then
                                        webpage(f + g + 1) = webpage(f + g).Replace(webpage(f + g).Substring(0, webpage(f + g).IndexOf("</a>") + 4), "")
                                        webpage(f + g) = webpage(f + g).Replace(webpage(f + g + 1), "")
                                    End If
                                    webpage(f + g) = webpage(f + g).Substring(0, webpage(f + g).IndexOf("</a>"))
                                    webpage(f + g) = webpage(f + g).Substring(webpage(f + g).LastIndexOf(">") + 1, webpage(f + g).Length - webpage(f + g).LastIndexOf(">") - 1)
                                    webpage(f + g) = webpage(f + g).TrimEnd(" ")
                                    webpage(f + g) = webpage(f + g).TrimEnd(",")
                                    If webpage(f + g) <> "" Then
                                        listofdirectors.Add(webpage(f + g))
                                    End If
                                End If
                            Next
                            For g = 0 To listofdirectors.Count - 1
                                If g = 0 Then
                                    movienfoarray = listofdirectors(g)
                                Else
                                    movienfoarray = movienfoarray & " / " & listofdirectors(g)
                                End If
                            Next
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<director>" & movienfoarray & "</director>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<director>scraper error</director>" & vbCrLf
                        End Try
                    End If

                    'credits
                    If webpage(f).IndexOf("<h4 class=""inline"">") <> -1 And webpage(f + 1).IndexOf("Writer") <> -1 Then
                        Try
                            movienfoarray = ""
                            Dim listofwriters As New List(Of String)
                            listofwriters.Clear()
                            For g = 1 To 10
                                If webpage(f + 1).IndexOf("<div") <> -1 Then Exit For
                                If webpage(f + g).IndexOf("href=""/name/nm") <> -1 Then

                                    webpage(f + g) = webpage(f + g).Substring(0, webpage(f + g).IndexOf("</a>"))
                                    webpage(f + g) = webpage(f + g).Substring(webpage(f + g).LastIndexOf(">") + 1, webpage(f + g).Length - webpage(f + g).LastIndexOf(">") - 1)
                                    webpage(f + g) = webpage(f + g).TrimEnd(" ")
                                    webpage(f + g) = webpage(f + g).TrimEnd(",")
                                    If webpage(f + g) <> "" Then
                                        listofwriters.Add(webpage(f + g))
                                    End If
                                End If
                            Next
                            For g = 0 To listofwriters.Count - 1
                                If g = 0 Then
                                    movienfoarray = listofwriters(g)
                                Else
                                    movienfoarray = movienfoarray & " / " & listofwriters(g)
                                End If
                            Next
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<credits>" & movienfoarray & "</credits>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<credits>scraper error</credits>" & vbCrLf
                        End Try
                    End If



                    'genre
                    If f > 5 Then
                        If webpage(f).IndexOf("<h4 class=""inline"">Genre") <> -1 Then
                            Dim listofgenre As New List(Of String)
                            Try
                                If webpage(f + 1).IndexOf("<a href=""/genre/") <> -1 Then
                                    Do While webpage(f + 1).IndexOf("<a href=""/genre/") <> webpage(f + 1).LastIndexOf("<a href=""/genre/")
                                        Try
                                            tempstring = webpage(f + 1).Replace(webpage(f + 1).Substring(0, webpage(f + 1).IndexOf("</a>") + 4), "")
                                            listofgenre.Add(webpage(f + 1).Replace(tempstring, ""))
                                            webpage(f + 1) = tempstring
                                        Catch ex As Exception

                                        End Try
                                    Loop
                                    listofgenre.Add(webpage(f + 1))
                                    For g = 0 To listofgenre.Count - 1
                                        listofgenre(g) = listofgenre(g).Replace("</a>", "")
                                        listofgenre(g) = listofgenre(g).Substring(listofgenre(g).LastIndexOf(">") + 1, listofgenre(g).Length - listofgenre(g).LastIndexOf(">") - 1)
                                    Next
                                End If
                                For g = 0 To listofgenre.Count - 1
                                    If g = 0 Then
                                        movienfoarray = listofgenre(g)
                                    Else
                                        movienfoarray = movienfoarray & " / " & listofgenre(g)
                                    End If
                                Next
                                movienfoarray = specchars(movienfoarray)
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<genre>" & movienfoarray & "</genre>" & vbCrLf
                            Catch
                                totalinfo = totalinfo & "<genre>scraper error</genre>" & vbCrLf
                            End Try
                        End If
                    End If


                    'tagline
                    If webpage(f).IndexOf("<h4 class=""inline"">Tagline") <> -1 Then
                        Try
                            movienfoarray = webpage(f + 1)
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            movienfoarray = movienfoarray.Trim()
                            totalinfo = totalinfo & "<tagline>" & movienfoarray & "</tagline>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<tagline>scraper error</tagline>" & vbCrLf
                        End Try
                    End If

                    If webpage(f).IndexOf("<h4 class=""inline"">Runtime") <> -1 Then
                        movienfoarray = ""
                        Try
                            For g = 1 To 5
                                If webpage(f + g).IndexOf("min") <> -1 Then
                                    movienfoarray = webpage(f + g)
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
                                    Exit For
                                End If
                            Next
                            If movienfoarray <> "" Then
                                movienfoarray = movienfoarray & " min"
                            End If
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<runtime>" & movienfoarray & "</runtime>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<runtime>scraper error</runtime>" & vbCrLf
                        End Try
                    End If
                    If webpage(f).IndexOf("<div class=""infobar"">") <> -1 Then
                        Try
                            If webpage(f + 1).IndexOf("<img width=") <> 0 Then
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
                                movienfoarray = specchars(movienfoarray)
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<runtime>" & movienfoarray & "</runtime>" & vbCrLf
                            End If
                        Catch
                        End Try
                    End If

                    'votes
                    If webpage(f).IndexOf("votes</a>") <> -1 Then
                        Try
                            webpage(f) = webpage(f).Replace(" votes</a>)", "")
                            movienfoarray = webpage(f).Substring(webpage(f).LastIndexOf(">") + 1, webpage(f).Length - webpage(f).LastIndexOf(">") - 1)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<votes>" & movienfoarray & "</votes>" & vbCrLf
                        Catch
                            totalinfo = totalinfo & "<votes>scraper error</votes>" & vbCrLf
                        End Try
                    End If

                    'outline
                    If webpage(f).IndexOf("<p>") <> -1 Then
                        Try
                            If webpage(f + 1).IndexOf("<p>") <> -1 And webpage(f + 2).IndexOf("</p>") <> -1 And webpage(f + 3).IndexOf("</p>") <> -1 Then
                                movienfoarray = webpage(f + 1)
                                movienfoarray = movienfoarray.Replace("<p>", "")
                                movienfoarray = specchars(movienfoarray)
                                movienfoarray = encodespecialchrs(movienfoarray)
                                totalinfo = totalinfo & "<outline>" & movienfoarray & "</outline><plot></plot>" & vbCrLf
                            End If
                        Catch
                            totalinfo = totalinfo & "<outline>scraper error</outline>" & vbCrLf
                        End Try
                    End If

                    'premiered
                    If webpage(f).IndexOf("<h4 class=""inline"">Release Date") <> -1 Then
                        Try
                            movienfoarray = webpage(f + 1)
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<premiered>" & movienfoarray & "</premiered>" & vbCrLf
                        Catch
                        End Try
                    End If


                    'studio
                    If webpage(f).IndexOf("<h4 class=""inline"">Production") <> -1 Then
                        Try
                            movienfoarray = ""
                            For g = 1 To 5
                                If webpage(f + g).IndexOf("<a  href=""/company/") <> -1 Then
                                    webpage(f + g) = webpage(f + g).Replace("<a  href=""/company/", "")
                                    webpage(f + g) = webpage(f + g).Substring(webpage(f + g).IndexOf(">") + 1, webpage(f + g).IndexOf("</a>") - webpage(f + g).IndexOf(">") - 1)
                                    movienfoarray = webpage(f + g)
                                End If
                            Next
                            movienfoarray = movienfoarray.Trim()
                            movienfoarray = specchars(movienfoarray)
                            movienfoarray = encodespecialchrs(movienfoarray)
                            totalinfo = totalinfo & "<studio>" & movienfoarray & "</studio>" & vbCrLf
                            'Exit For
                        Catch
                            totalinfo = totalinfo & "<studio>scraper error</studio>" & vbCrLf
                        End Try
                    End If
                    '<div class="txt-block">
                    '<h4 class="inline">Country:</h4> 

                    '<a href="/country/us">USA</a>

                    '</div>
                    'country
                    If webpage(f).IndexOf("class=""inline"">Countr") <> -1 Then
                        Try
                            For g = 1 To 5
                                If webpage(f + g).IndexOf("</div>") <> -1 Then Exit For
                                If webpage(f + g).IndexOf("</a>") <> -1 Then
                                    movienfoarray = webpage(f + g)
                                    movienfoarray = movienfoarray.Substring(movienfoarray.IndexOf(">") + 1, movienfoarray.LastIndexOf("<") - movienfoarray.IndexOf(">") - 1)
                                    movienfoarray = specchars(movienfoarray)
                                    movienfoarray = encodespecialchrs(movienfoarray)
                                    totalinfo = totalinfo & "<country>" & movienfoarray & "</country>" & vbCrLf
                                    Exit For
                                End If
                            Next
                        Catch
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
                'If imdbmirror <> "http://www.imdb.de/" Then
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
                        movienfoarray = plots(biggest)
                        If movienfoarray.IndexOf("<a href=") <> -1 Then
                            Do Until movienfoarray.IndexOf("<a href=") = -1
                                first = movienfoarray.LastIndexOf("<a href=")
                                last = movienfoarray.LastIndexOf("/"">")
                                tempstring = movienfoarray.Substring(first, last - first + 3)
                                movienfoarray = movienfoarray.Replace(tempstring, "")
                            Loop
                            movienfoarray = movienfoarray.Replace("</a>", "")
                        End If
                        movienfoarray = specchars(movienfoarray)
                        movienfoarray = encodespecialchrs(movienfoarray)
                        totalinfo = totalinfo.Replace("<plot></plot>", "<plot>" & movienfoarray & "</plot>")
                    End If
                Catch
                    totalinfo = totalinfo & "<plot>scraper error</plot>"
                End Try
                'End If

                'certs & mpaa
                Try
                    tempstring = imdbmirror & "title/" & imdbid & "/parentalguide#certification"
                    webpage.Clear()
                    webpage = loadwebpage(tempstring, False)
                    For f = 0 To webpage.Count - 1
                        'mpaa
                        If webpage(f).IndexOf("<a href=""/mpaa"">MPAA") <> -1 Then
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
                                        mpaaresults(g, 1) = mpaaresults(g, 1).Substring(mpaaresults(g, 1).IndexOf(":") + 1, mpaaresults(g, 1).Length - mpaaresults(g, 1).IndexOf(":") - 1)
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
                        If webpage(f).IndexOf("<h5><a name=""akas"">Also Known As") <> -1 Then
                            Dim loc As Integer = f
                            Dim ignore As Boolean = False
                            For g = loc To loc + 500
                                If webpage(g).IndexOf("</table>") <> -1 Then
                                    Exit For
                                End If
                                Dim skip As Boolean = ignore
                                If webpage(g).IndexOf("<td>") <> -1 And ignore = True Then
                                    ignore = False
                                End If
                                If webpage(g).IndexOf("<td>") <> -1 And skip = False Then
                                    If webpage(g + 2).IndexOf("Greece") = -1 And webpage(g + 2).IndexOf("Russia") = -1 Then
                                        tempstring = webpage(g)
                                        tempstring = tempstring.Replace("<td>", "")
                                        tempstring = tempstring.Replace("</td>", "")
                                        tempstring = specchars(tempstring)
                                        tempstring = encodespecialchrs(tempstring)
                                        totalinfo = totalinfo & "<alternativetitle>" & tempstring & "</alternativetitle>" & vbCrLf
                                    End If
                                    ignore = True
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
                        mpaaresults(f, 0) = specchars(mpaaresults(f, 0))
                        mpaaresults(f, 1) = specchars(mpaaresults(f, 1))
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
    Public Function getimdbactors(ByVal imdbmirror As String, Optional ByVal imdbid As String = "", Optional ByVal title As String = "", Optional ByVal maxactors As Integer = 9999)
        Dim webpage As New List(Of String)
        Dim actors(5000, 3)
        Dim tempstring As String
        Dim filterstring As String
        Dim first As Integer
        Dim last As Integer
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
                    scrapertempint = scrapertempint + 1
                    actors(scrapertempint, 0) = scrapertempstring
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
                    actors(f, 0) = specchars(actors(f, 0))
                    actors(f, 0) = encodespecialchrs(actors(f, 0))
                    totalinfo = totalinfo & "<name>" & actors(f, 0) & "</name>" & vbCrLf
                    If actors(f, 1) <> Nothing Then
                        actors(f, 1) = specchars(actors(f, 1))
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
    End Function
    Public Function gettrailerurl(ByVal imdbid As String, ByVal imdbmirror As String)
        Monitor.Enter(Me)
        Dim allok As Boolean = False
        Dim first As Integer
        Dim last As Integer

        Dim tempstring As String
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
    End Function


    Private Function encodespecialchrs(ByVal text As String)
        If text.IndexOf("&") <> -1 Then text = text.Replace("&", "&amp;")
        If text.IndexOf("<") <> -1 Then text = text.Replace("", "&lt;")
        If text.IndexOf(">") <> -1 Then text = text.Replace("", "&gt;")
        If text.IndexOf(Chr(34)) <> -1 Then text = text.Replace(Chr(34), "&quot;")
        If text.IndexOf("'") <> -1 Then text = text.Replace("'", "&apos;")
        Return text
    End Function
    Private Function loadwebpage(ByVal url As String, ByVal method As Boolean)

        Dim webpage As New List(Of String)
        Monitor.Enter(Me)

        Try
            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(url)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream, System.Text.UTF8Encoding.UTF7)
            Dim sLine As String = ""

            If method = False Then
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

            If method = False Then
                Return webpage
            Else
                Return sLine
            End If

        Catch ex As WebException
            'MsgBox("Unable to load webpage " & url & vbCrLf & vbCrLf & ex.ToString)
            If webpage.Count > 0 Then
                Return webpage
            Else
                webpage.Add("error")
                Return webpage
            End If
        Finally
            Monitor.Exit(Me)
        End Try



    End Function
    Private Function specchars(ByVal filterstring As String)
        Monitor.Enter(Me)
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
            Monitor.Exit(Me)
        End Try
    End Function ' Replace special IMDB chrs with ascii




End Class


