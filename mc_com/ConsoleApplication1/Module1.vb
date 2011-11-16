Imports class_mpdb_thumbs
Imports ConsoleApplication1.My
Imports imdb
Imports imdb_thumbs
Imports IMPA
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.VisualBasic.FileIO
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Xml
Imports tmdb_posters
Imports TVDB
Imports Vbaccelerator.Components.Algorithms

Namespace ConsoleApplication1
    <StandardModule> _
    Friend NotInheritable Class Module1
        ' Methods
        Private Shared Sub addepisode(ByVal alleps As List(Of episodeinfo), ByVal sPath As String)
            Console.WriteLine("Saving episode")
            Module1.saveepisodenfo(alleps, sPath, "-2", "-2")
            Dim str As String = sPath.Replace(Path.GetExtension(sPath), ".tbn")
            If Not (File.Exists(str) Or (alleps.Item(0).thumb = Nothing)) Then
                Dim buffer As Byte() = New Byte(&H61A81 - 1) {}
                Dim num2 As Integer = 0
                Dim offset As Integer = 0
                Dim thumb As String = alleps.Item(0).thumb
                If ((thumb <> Nothing) AndAlso ((thumb.IndexOf("http") = 0) And (thumb.IndexOf(".jpg") <> -1))) Then
                    Try
                        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(thumb), HttpWebRequest)
                        Dim responseStream As Stream = DirectCast(request.GetResponse, HttpWebResponse).GetResponseStream
                        Dim length As Integer = buffer.Length
                        Do While (length > 0)
                            num2 = responseStream.Read(buffer, offset, length)
                            If (num2 = 0) Then
                                Exit Do
                            End If
                            length = (length - num2)
                            offset = (offset + num2)
                        Loop
                        Try
                            Console.WriteLine(("Saving Thumbnail To :- " & str))
                            Dim stream2 As New FileStream(str, FileMode.OpenOrCreate, FileAccess.Write)
                            stream2.Write(buffer, 0, offset)
                            responseStream.Close()
                            stream2.Close()
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            Console.WriteLine(("Unable to Save Thumb, Error: " & exception.Message.ToString))
                            ProjectData.ClearProjectError()
                            Return
                        End Try
                    Catch exception2 As Exception
                        ProjectData.SetProjectError(exception2)
                        ProjectData.ClearProjectError()
                    End Try
                End If
            End If
        End Sub

        Private Shared Sub addhtmltemplates()
            Module1.templatelist.Clear
            Dim info As New DirectoryInfo(Path.Combine(Module1.applicationpath, "html_templates\"))
            Dim info2 As FileInfo
            For Each info2 In info.GetFiles("*.txt", IO.SearchOption.TopDirectoryOnly)
                Try
                    Dim str2 As String = File.OpenText(info2.FullName).ReadToEnd
                    If ((str2.ToLower.IndexOf("<<mc html page>>") <> -1) And (str2.ToLower.IndexOf("<</mc html page>>") <> -1)) Then
                        Dim str3 As String = str2.Substring((str2.IndexOf("<title>") + 7), (str2.IndexOf("</title>") - 7))
                        Dim flag As Boolean = True
                        Dim htmltemplate2 As htmltemplate
                        For Each htmltemplate2 In Module1.templatelist
                            If (htmltemplate2.title = str3) Then
                                flag = False
                                Exit For
                            End If
                        Next
                        If flag Then
                            Dim htmltemplate As htmltemplate
                            htmltemplate.title = str3
                            htmltemplate.path = info2.FullName
                            htmltemplate.body = str2
                            Module1.templatelist.Add(htmltemplate)
                        End If
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    ProjectData.ClearProjectError()
                End Try
            Next
        End Sub

        Public Shared Function cleanfilename(ByVal filename As String, ByVal Optional withextension As Boolean = True) As Object
            Dim obj2 As Object
            Dim sPath As String = filename
            Try
                Dim str2 As String
                If withextension Then
                    Try
                        sPath = filename.Replace(Path.GetExtension(sPath), "")
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        ProjectData.ClearProjectError()
                    End Try
                End If
                Dim input As String = sPath
                Dim match As Match = Regex.Match(input, "(\([\d]{4}\))")
                If match.Success Then
                    str2 = match.Value
                Else
                    str2 = Nothing
                End If
                If (str2 = Nothing) Then
                    match = Regex.Match(input, "(\[[\d]{4}\])")
                    If match.Success Then
                        str2 = match.Value
                    Else
                        str2 = Nothing
                    End If
                End If
                filename = sPath
                Dim strArray As String() = New String(&H49 - 1) {}
                strArray(1) = "cd1"
                strArray(2) = "cd.1"
                strArray(3) = "cd_1"
                strArray(4) = "cd 1"
                strArray(5) = "cd-1"
                strArray(6) = "dvd1"
                strArray(7) = "dvd.1"
                strArray(8) = "dvd_1"
                strArray(9) = "dvd 1"
                strArray(10) = "dvd-1"
                strArray(11) = "part1"
                strArray(12) = "part.1"
                strArray(13) = "part_1"
                strArray(14) = "part 1"
                strArray(15) = "part-1"
                strArray(&H10) = "disk1"
                strArray(&H11) = "disk.1"
                strArray(&H12) = "disk_1"
                strArray(&H13) = "disk 1"
                strArray(20) = "disk-1"
                strArray(&H15) = "pt1"
                strArray(&H16) = "pt.1"
                strArray(&H17) = "pt_1"
                strArray(&H18) = "pt 1"
                strArray(&H19) = "pt-1"
                strArray(&H1A) = "ac3"
                strArray(&H1B) = "divx"
                strArray(&H1C) = "xvid"
                strArray(&H1D) = "dvdrip"
                strArray(30) = "directors cut"
                strArray(&H1F) = "special edition"
                strArray(&H20) = "screener"
                strArray(&H21) = "telesync"
                strArray(&H22) = "telecine"
                strArray(&H23) = "director's cut"
                strArray(&H24) = " r5"
                strArray(&H25) = " scr"
                strArray(&H26) = ".scr"
                strArray(&H27) = "_scr"
                strArray(40) = "-scr"
                strArray(&H29) = " ts"
                strArray(&H2A) = "_ts"
                strArray(&H2B) = ".ts"
                strArray(&H2C) = "-ts"
                strArray(&H2D) = " fs"
                strArray(&H2E) = ".fs"
                strArray(&H2F) = "_fs"
                strArray(&H30) = "-fs"
                strArray(&H31) = " ws"
                strArray(50) = ".ws"
                strArray(&H33) = "_ws"
                strArray(&H34) = "-ws"
                strArray(&H35) = "-r5"
                strArray(&H36) = "_r5"
                strArray(&H37) = ".r5"
                strArray(&H38) = "bluray"
                strArray(&H39) = "720"
                strArray(&H3A) = "1024"
                strArray(&H3B) = "fullscreen"
                strArray(60) = "widescreen"
                strArray(&H3D) = "dvdscr"
                strArray(&H3E) = "part01"
                strArray(&H3F) = "dvd5"
                strArray(&H40) = "dvd9"
                strArray(&H41) = "dvd 5"
                strArray(&H42) = "dvd 9"
                strArray(&H43) = "dvd-5"
                strArray(&H44) = "dvd-9"
                strArray(&H45) = "dvd_5"
                strArray(70) = "dvd_9"
                strArray(&H47) = "dvd.5"
                strArray(&H48) = "dvd.9"
                Dim length As Integer = filename.Length
                Dim num2 As Integer = filename.Length
                Dim index As Integer = 1
                Do
                    If (filename.ToLower.IndexOf(strArray(index)) <> -1) Then
                        num2 = filename.ToLower.IndexOf(strArray(index))
                        If (num2 < length) Then
                            length = num2
                        End If
                    End If
                    index += 1
                Loop While (index <= &H48)
                If ((str2 <> Nothing) AndAlso (filename.IndexOf(str2) <> -1)) Then
                    num2 = filename.IndexOf(str2)
                    If (num2 < length) Then
                        length = num2
                    End If
                End If
                If ((length < filename.Length) And (length > 0)) Then
                    filename = filename.Substring(0, length)
                    If ((((filename.Substring((filename.Length - 1), 1) = "-") Or (filename.Substring((filename.Length - 1), 1) = "_")) Or (filename.Substring((filename.Length - 1), 1) = ".")) Or (filename.Substring((filename.Length - 1), 1) = " ")) Then
                        filename = filename.Substring(0, (filename.Length - 1))
                    End If
                End If
                If (filename <> "") Then
                    sPath = filename
                End If
                obj2 = Strings.Trim(sPath)
            Catch exception2 As Exception
                ProjectData.SetProjectError(exception2)
                Dim exception As Exception = exception2
                sPath = "error"
                obj2 = sPath
                ProjectData.ClearProjectError()
            End Try
            Return obj2
        End Function

        Public Shared Function decxmlchars(ByVal line As String) As Object
            line = line.Replace("&amp;", "&")
            line = line.Replace("&lt;", "<")
            line = line.Replace("&gt;", ">")
            line = line.Replace("&quot;", "Chr(34)")
            line = line.Replace("&apos;", "'")
            line = line.Replace("&#xA;", ChrW(13) & ChrW(10))
            Return line
        End Function

        Public Shared Function EnumerateDirectory2(ByVal RootDirectory As String, ByVal Optional log As Boolean = False) As Object
            Dim obj2 As Object
            Dim list As New List(Of String)
            Try 
                Dim str As String
                For Each str In Directory.GetDirectories(RootDirectory)
                    If (((File.GetAttributes(str) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str)) Then
                        Dim flag As Boolean = False
                        Dim str2 As String
                        For Each str2 In list
                            If (str2 = str) Then
                                flag = True
                            End If
                        Next
                        If Not flag Then
                            list.Add(str)
                            Dim str3 As String
                            For Each str3 In Directory.GetDirectories(str)
                                If (((File.GetAttributes(str3) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str3)) Then
                                    Dim flag2 As Boolean = False
                                    Dim str4 As String
                                    For Each str4 In list
                                        If (str4 = str3) Then
                                            flag2 = True
                                        End If
                                    Next
                                    If Not flag Then
                                        list.Add(str3)
                                        Dim str5 As String
                                        For Each str5 In Directory.GetDirectories(str3)
                                            If (((File.GetAttributes(str5) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str5)) Then
                                                Dim flag3 As Boolean = False
                                                Dim str6 As String
                                                For Each str6 In list
                                                    If (str6 = str) Then
                                                        flag3 = True
                                                    End If
                                                Next
                                                If Not flag Then
                                                    list.Add(str5)
                                                    Dim str7 As String
                                                    For Each str7 In Directory.GetDirectories(str5)
                                                        If (((File.GetAttributes(str7) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str7)) Then
                                                            Dim flag4 As Boolean = False
                                                            Dim str8 As String
                                                            For Each str8 In list
                                                                If (str8 = str7) Then
                                                                    flag4 = True
                                                                End If
                                                            Next
                                                            If Not flag4 Then
                                                                list.Add(str7)
                                                                Dim str9 As String
                                                                For Each str9 In Directory.GetDirectories(str7)
                                                                    If (((File.GetAttributes(str9) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str9)) Then
                                                                        Dim flag5 As Boolean = False
                                                                        Dim str10 As String
                                                                        For Each str10 In list
                                                                            If (str10 = str9) Then
                                                                                flag5 = True
                                                                            End If
                                                                        Next
                                                                        If Not flag5 Then
                                                                            list.Add(str9)
                                                                            Dim str11 As String
                                                                            For Each str11 In Directory.GetDirectories(str9)
                                                                                If (((File.GetAttributes(str11) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str11)) Then
                                                                                    Dim flag6 As Boolean = False
                                                                                    Dim str12 As String
                                                                                    For Each str12 In list
                                                                                        If (str12 = str11) Then
                                                                                            flag6 = True
                                                                                        End If
                                                                                    Next
                                                                                    If Not flag6 Then
                                                                                        list.Add(str11)
                                                                                        Dim str13 As String
                                                                                        For Each str13 In Directory.GetDirectories(str11)
                                                                                            If (((File.GetAttributes(str13) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str13)) Then
                                                                                                Dim flag7 As Boolean = False
                                                                                                Dim str14 As String
                                                                                                For Each str14 In list
                                                                                                    If (str14 = str13) Then
                                                                                                        flag7 = True
                                                                                                    End If
                                                                                                Next
                                                                                                If Not flag7 Then
                                                                                                    list.Add(str13)
                                                                                                End If
                                                                                            End If
                                                                                        Next
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        End If
                                                                    End If
                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    End If
                Next
                obj2 = list
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim str15 As String = exception1.ToString
                obj2 = list
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Public Shared Function EnumerateDirectory3(ByVal RootDirectory As String) As Object
            Dim obj2 As Object
            Dim list As New List(Of String)
            Try 
                Dim str As String
                For Each str In Directory.GetDirectories(RootDirectory)
                    If (((File.GetAttributes(str) And FileAttributes.ReparsePoint) <> FileAttributes.ReparsePoint) AndAlso Module1.validmoviedir(str)) Then
                        Dim flag As Boolean = False
                        Dim str2 As String
                        For Each str2 In list
                            If (str2 = str) Then
                                flag = True
                            End If
                        Next
                        If Not flag Then
                            list.Add(str)
                            Module1.EnumerateDirectory3(str)
                        End If
                    End If
                Next
                obj2 = list
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim str3 As String = exception1.ToString
                obj2 = list
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Private Shared Sub episodescraper(ByVal listofshowfolders As List(Of String), ByVal manual As Boolean)
            Dim str As String
            Dim num5 As Integer
            Dim sPath As String = ""
            Module1.newepisodelist.Clear()
            Dim list As New List(Of String)
            Console.WriteLine("Starting TV Folder Scan")
            Dim strArray As String() = New String(&H65 - 1) {}
            strArray(1) = "*.avi"
            strArray(2) = "*.xvid"
            strArray(3) = "*.divx"
            strArray(4) = "*.img"
            strArray(5) = "*.mpg"
            strArray(6) = "*.mpeg"
            strArray(7) = "*.mov"
            strArray(8) = "*.rm"
            strArray(9) = "*.3gp"
            strArray(10) = "*.m4v"
            strArray(11) = "*.wmv"
            strArray(12) = "*.asf"
            strArray(13) = "*.mp4"
            strArray(14) = "*.mkv"
            strArray(15) = "*.nrg"
            strArray(&H10) = "*.iso"
            strArray(&H11) = "*.rmvb"
            strArray(&H12) = "*.ogm"
            strArray(&H13) = "*.bin"
            strArray(20) = "*.ts"
            strArray(&H15) = "*.vob"
            strArray(&H16) = "*.m2ts"
            strArray(&H17) = "*.rar"
            strArray(&H18) = "*.flv"
            strArray(&H19) = "VIDEO_TS.IFO"
            strArray(&H1A) = "*.strm"
            Dim num2 As Integer = &H1A
            Dim directoryName As String
            For Each directoryName In listofshowfolders
                Dim flag2 As Boolean = True
                Dim basictvshownfo As basictvshownfo
                For Each basictvshownfo In Module1.basictvlist
                    If (((basictvshownfo.fullpath.IndexOf(directoryName) <> -1) AndAlso ((basictvshownfo.locked = -1) Or (basictvshownfo.locked = 2))) AndAlso Not manual) Then
                        flag2 = False
                        Exit For
                    End If
                Next
                If flag2 Then
                    directoryName = Path.GetDirectoryName(directoryName)
                    sPath = ""
                    Dim info As New DirectoryInfo(directoryName)
                    If info.Exists Then
                        Console.WriteLine(("found " & info.FullName.ToString))
                        list.Add(directoryName)
                        Try
                            Dim enumerator As IEnumerator(Of String)
                            Try
                                enumerator = MyProject.Computer.FileSystem.GetDirectories(directoryName).GetEnumerator
                                Do While enumerator.MoveNext
                                    Dim current As String = enumerator.Current
                                    Try
                                        If (current.IndexOf("System Volume Information") = -1) Then
                                            list.Add(current)
                                            Dim str8 As String
                                            For Each str8 In MyProject.Computer.FileSystem.GetDirectories(current, FileIO.SearchOption.SearchAllSubDirectories, New String(0 - 1) {})
                                                Try
                                                    If (str8.IndexOf("System Volume Information") = -1) Then
                                                        list.Add(str8)
                                                    End If
                                                    Continue For
                                                Catch exception1 As Exception
                                                    ProjectData.SetProjectError(exception1)
                                                    Dim exception As Exception = exception1
                                                    Interaction.MsgBox(exception.Message, MsgBoxStyle.OkOnly, Nothing)
                                                    ProjectData.ClearProjectError()
                                                    Continue For
                                                End Try
                                            Next
                                        End If
                                        Continue Do
                                    Catch exception8 As Exception
                                        ProjectData.SetProjectError(exception8)
                                        Dim exception2 As Exception = exception8
                                        Interaction.MsgBox(exception2.Message, MsgBoxStyle.OkOnly, Nothing)
                                        ProjectData.ClearProjectError()
                                        Continue Do
                                    End Try
                                Loop
                            Finally
                                If (Not enumerator Is Nothing) Then
                                    enumerator.Dispose()
                                End If
                            End Try
                        Catch exception9 As Exception
                            ProjectData.SetProjectError(exception9)
                            Interaction.MsgBox(exception9.ToString, MsgBoxStyle.OkOnly, Nothing)
                            ProjectData.ClearProjectError()
                        End Try
                    End If
                    Continue For
                End If
                Console.WriteLine((ChrW(13) & ChrW(10) & "Show Locked, Ignoring: " & directoryName))
            Next
            Dim count As Integer = Module1.newepisodelist.Count
            Dim num21 As Integer = (list.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num21)
                Dim num22 As Integer = num2
                Dim j As Integer = 1
                Do While (j <= num22)
                    Dim pattern As String = strArray(j)
                    str = list.Item(i)
                    Dim info2 As New DirectoryInfo(str)
                    Module1.findnewepisodes(str, pattern)
                    j += 1
                Loop
                num5 = (Module1.newepisodelist.Count - count)
                If (num5 > 0) Then
                    Console.WriteLine((num5.ToString & " New episodes found in directory:- " & str))
                End If
                count = Module1.newepisodelist.Count
                i += 1
            Loop
            If (Module1.newepisodelist.Count <= 0) Then
                Console.WriteLine((num5.ToString & "No new episodes found, exiting scraper" & str))
            Else
                Dim enumerator7 As IEnumerator(Of episodeinfo)
                Dim input As String = ""
                Dim episodeinfo2 As episodeinfo
                For Each episodeinfo2 In Module1.newepisodelist
                    input = ""
                    Module1.newepisodetoadd.episodeno = ""
                    Module1.newepisodetoadd.episodepath = ""
                    Module1.newepisodetoadd.imdbid = ""
                    Module1.newepisodetoadd.playcount = ""
                    Module1.newepisodetoadd.rating = ""
                    Module1.newepisodetoadd.seasonno = ""
                    Module1.newepisodetoadd.title = ""
                    Module1.newepisodetoadd.tvdbid = ""
                    Dim episodeinfo As New episodeinfo
                    Dim str9 As String
                    For Each str9 In Module1.tvregex
                        input = episodeinfo2.episodepath.Replace("x264", "").Replace("720p", "").Replace("720i", "").Replace("1080p", "").Replace("1080i", "").Replace("X264", "").Replace("720P", "").Replace("720I", "").Replace("1080P", "").Replace("1080I", "")
                        Dim match As Match = Regex.Match(input, str9)
                        If match.Success Then
                            Try
                                episodeinfo2.seasonno = match.Groups.Item(1).Value.ToString
                                episodeinfo2.episodeno = match.Groups.Item(2).Value.ToString
                                If ((episodeinfo2.seasonno <> "-1") And (episodeinfo2.episodeno <> "-1")) Then
                                    Console.WriteLine(String.Concat(New String() {"Season and Episode information found for : ", episodeinfo2.episodepath, episodeinfo2.seasonno, "x", episodeinfo2.episodeno}))
                                Else
                                    Console.WriteLine(("Cant extract Season and Episode deatails from filename: " & episodeinfo2.seasonno & "x" & episodeinfo2.episodeno))
                                End If
                                Try
                                    episodeinfo2.fanartpath = input.Substring((match.Groups.Item(2).Index + match.Groups.Item(2).Value.Length), (input.Length - (match.Groups.Item(2).Index + match.Groups.Item(2).Value.Length)))
                                    Exit For
                                Catch exception10 As Exception
                                    ProjectData.SetProjectError(exception10)
                                    ProjectData.ClearProjectError()
                                    Exit For
                                End Try
                            Catch exception11 As Exception
                                ProjectData.SetProjectError(exception11)
                                episodeinfo2.seasonno = "-1"
                                episodeinfo2.episodeno = "-1"
                                ProjectData.ClearProjectError()
                                Continue For
                            End Try
                        End If
                    Next
                    If (episodeinfo2.seasonno = Nothing) Then
                        episodeinfo2.seasonno = "-1"
                    End If
                    If (episodeinfo2.episodeno = Nothing) Then
                        episodeinfo2.episodeno = "-1"
                    End If
                Next
                Dim episodepath As String = ""
                Try
                    enumerator7 = Module1.newepisodelist.GetEnumerator
                    Do While enumerator7.MoveNext
                        Dim flag As Boolean
                        Dim item As episodeinfo = enumerator7.Current
                        Dim alleps As New List(Of episodeinfo)
                        alleps.Clear()
                        Dim episodeinfo4 As New episodeinfo With { _
                            .seasonno = item.seasonno, _
                            .episodeno = item.episodeno, _
                            .episodepath = item.episodepath, _
                            .mediaextension = item.mediaextension _
                        }
                        alleps.Add(episodeinfo4)
                        Console.WriteLine((ChrW(13) & ChrW(10) & "Working on episode: " & item.episodepath))
                        If ((item.seasonno = "-1") Or (item.episodeno = "-1")) Then
                            item.title = Conversions.ToString(Module1.getfilename(item.episodepath))
                            item.rating = "0"
                            item.playcount = "0"
                            item.genre = "Unknown Episode Season and/or Episode Number"
                            item.filedetails = DirectCast(Module1.get_hdtags(item.mediaextension), fullfiledetails)
                            alleps.Add(item)
                            episodepath = alleps.Item(0).episodepath
                        Else
                            Dim match2 As Match
                            Dim str16 As String = item.episodepath
                            Dim index As Integer = 0
                            Dim numArray As Integer() = New Integer(&H65 - 1) {}
                            input = item.fanartpath
                            item.fanartpath = ""
                            Do
                                match2 = Regex.Match(input, "(([EeXx])([\d]{1,4}))")
                                If match2.Success Then
                                    Dim flag4 As Boolean = False
                                    Dim episodeinfo5 As episodeinfo
                                    For Each episodeinfo5 In alleps
                                        If (episodeinfo5.episodeno = match2.Groups.Item(3).Value) Then
                                            flag4 = True
                                        End If
                                    Next
                                    If Not flag4 Then
                                        Dim episodeinfo6 As New episodeinfo With { _
                                            .seasonno = item.seasonno, _
                                            .episodeno = match2.Groups.Item(3).Value, _
                                            .episodepath = item.episodepath, _
                                            .mediaextension = item.mediaextension _
                                        }
                                        alleps.Add(episodeinfo6)
                                        numArray(index) = Convert.ToInt32(Convert.ToDecimal(match2.Groups.Item(3).Value))
                                    End If
                                    Try
                                        input = input.Substring((match2.Groups.Item(3).Index + match2.Groups.Item(3).Value.Length), (input.Length - (match2.Groups.Item(3).Index + match2.Groups.Item(3).Value.Length)))
                                    Catch exception12 As Exception
                                        ProjectData.SetProjectError(exception12)
                                        ProjectData.ClearProjectError()
                                    End Try
                                End If
                            Loop While match2.Success
                            Dim language As String = ""
                            Dim sortorder As String = ""
                            Dim tvdbid As String = ""
                            Dim imdbid As String = ""
                            Dim episodeactorsource As String = ""
                            Dim fullpath As String = ""
                            episodepath = alleps.Item(0).episodepath
                            Dim basictvshownfo2 As basictvshownfo
                            For Each basictvshownfo2 In Module1.basictvlist
                                If (alleps.Item(0).episodepath.IndexOf(basictvshownfo2.fullpath.Replace("tvshow.nfo", "")) <> -1) Then
                                    language = basictvshownfo2.language
                                    sortorder = basictvshownfo2.sortorder
                                    tvdbid = basictvshownfo2.tvdbid
                                    imdbid = basictvshownfo2.imdbid
                                    fullpath = basictvshownfo2.fullpath
                                    episodeactorsource = basictvshownfo2.episodeactorsource
                                End If
                            Next
                            If (alleps.Count > 1) Then
                                Console.WriteLine("Multipart episode found: ")
                                Console.WriteLine(("Season: " & alleps.Item(0).seasonno & " Episodes, "))
                                Dim episodeinfo7 As episodeinfo
                                For Each episodeinfo7 In alleps
                                    Console.Write((episodeinfo7.episodeno & ", "))
                                Next
                            End If
                            Console.WriteLine("Looking up scraper options from tvshow.nfo")
                            Dim episodeinfo8 As episodeinfo
                            For Each episodeinfo8 In alleps
                                If ((episodeinfo8.seasonno.Length > 0) Or (episodeinfo8.seasonno.IndexOf("0") = 0)) Then
                                    Do While Not ((episodeinfo8.seasonno.IndexOf("0") <> 0) Or (episodeinfo8.seasonno.Length = 1))
                                        episodeinfo8.seasonno = episodeinfo8.seasonno.Substring(1, (episodeinfo8.seasonno.Length - 1))
                                    Loop
                                    If (episodeinfo8.episodeno = "00") Then
                                        episodeinfo8.episodeno = "0"
                                    End If
                                    If (episodeinfo8.episodeno <> "0") Then
                                        Do While (episodeinfo8.episodeno.IndexOf("0") = 0)
                                            episodeinfo8.episodeno = episodeinfo8.episodeno.Substring(1, (episodeinfo8.episodeno.Length - 1))
                                        Loop
                                    End If
                                End If
                                Dim tvdbscraper As New tvdbscraper
                                If (sortorder = "") Then
                                    sortorder = "default"
                                End If
                                Dim str18 As String = sortorder
                                If (language = "") Then
                                    language = "en"
                                End If
                                If (episodeactorsource = "") Then
                                    episodeactorsource = "tvdb"
                                End If
                                If (tvdbid <> "") Then
                                    Dim url As String = String.Concat(New String() {"http://thetvdb.com/api/6E82FED600783400/series/", tvdbid, "/", sortorder, "/", episodeinfo8.seasonno, "/", episodeinfo8.episodeno, "/", language, ".xml"})
                                    If (Not Module1.UrlIsValid(url) AndAlso (sortorder.ToLower = "dvd")) Then
                                        str18 = "default"
                                        Console.WriteLine("This episode could not be found on TVDB using DVD sort order")
                                        Console.WriteLine("Attempting to find using default sort order")
                                        url = String.Concat(New String() {"http://thetvdb.com/api/6E82FED600783400/series/", tvdbid, "/default/", episodeinfo8.seasonno, "/", episodeinfo8.episodeno, "/", language, ".xml"})
                                    End If
                                    If Module1.UrlIsValid(url) Then
                                        Dim xml As String = Conversions.ToString(tvdbscraper.getepisode(tvdbid, str18, episodeinfo8.seasonno, episodeinfo8.episodeno, language))
                                        flag = True
                                        If (xml = Nothing) Then
                                            flag = False
                                            Console.WriteLine("This episode could not be found on TVDB")
                                        End If
                                        If flag Then
                                            Dim document As New XmlDocument
                                            Try
                                                Dim enumerator12 As IEnumerator
                                                Console.WriteLine(("Scraping body of episode: " & episodeinfo8.episodeno))
                                                document.LoadXml(xml)
                                                Dim node As XmlNode = Nothing
                                                Try
                                                    enumerator12 = document.Item("episodedetails").GetEnumerator
Label_10B8:
                                                    Do While enumerator12.MoveNext
                                                        node = DirectCast(enumerator12.Current, XmlNode)
                                                        Dim name As String = node.Name
                                                        If (name = "title") Then
                                                            episodeinfo8.title = node.InnerText
                                                        Else
                                                            If (name = "premiered") Then
                                                                episodeinfo8.aired = node.InnerText
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "plot") Then
                                                                episodeinfo8.plot = node.InnerText
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "director") Then
                                                                Dim str21 As String = node.InnerText.TrimEnd(New Char() {"|"c}).TrimStart(New Char() {"|"c}).Replace("|", " / ")
                                                                episodeinfo8.director = str21
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "credits") Then
                                                                Dim str22 As String = node.InnerText.TrimEnd(New Char() {"|"c}).TrimStart(New Char() {"|"c}).Replace("|", " / ")
                                                                episodeinfo8.credits = str22
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "rating") Then
                                                                episodeinfo8.rating = node.InnerText
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "thumb") Then
                                                                episodeinfo8.thumb = node.InnerText
                                                                GoTo Label_10B8
                                                            End If
                                                            If (name = "actor") Then
                                                                Dim enumerator13 As IEnumerator
                                                                Try
                                                                    enumerator13 = node.ChildNodes.GetEnumerator
                                                                    Do While enumerator13.MoveNext
                                                                        Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator13.Current)
                                                                        If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, Nothing, "name", New Object(0 - 1) {}, Nothing, Nothing, Nothing), "name", False) Then
                                                                            Dim movieactors As New movieactors With { _
                                                                                .actorname = Conversions.ToString(NewLateBinding.LateGet(objectValue, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing)) _
                                                                            }
                                                                            episodeinfo8.listactors.Add(movieactors)
                                                                        End If
                                                                    Loop
                                                                    GoTo Label_10B8
                                                                Finally
                                                                    If TypeOf enumerator13 Is IDisposable Then
                                                                        TryCast(enumerator13, IDisposable).Dispose()
                                                                    End If
                                                                End Try
                                                            End If
                                                        End If
                                                    Loop
                                                Finally
                                                    If TypeOf enumerator12 Is IDisposable Then
                                                        TryCast(enumerator12, IDisposable).Dispose()
                                                    End If
                                                End Try
                                                episodeinfo8.playcount = "0"
                                            Catch exception13 As Exception
                                                ProjectData.SetProjectError(exception13)
                                                Dim exception4 As Exception = exception13
                                                Console.WriteLine(("Error scraping episode body, " & exception4.Message.ToString))
                                                ProjectData.ClearProjectError()
                                            End Try
                                            If (episodeactorsource = "imdb") Then
                                                Console.WriteLine("Scraping actors from IMDB")
                                                Dim requestUriString As String = ("http://www.imdb.com/title/" & imdbid & "/episodes")
                                                Dim num9 As Integer = 0
                                                Dim objArray As Object() = New Object(&H2711 - 1) {}
                                                num9 = 0
                                                Try
                                                    Dim request As WebRequest = WebRequest.Create(requestUriString)
                                                    Dim proxy As New WebProxy("myproxy", 80) With { _
                                                        .BypassProxyOnLocal = True _
                                                    }
                                                    Dim reader As New StreamReader(request.GetResponse.GetResponseStream)
                                                    Dim str24 As String = ""
                                                    num9 = 0
                                                    Do While (Not str24 Is Nothing)
                                                        num9 += 1
                                                        str24 = reader.ReadLine
                                                        If (Not str24 Is Nothing) Then
                                                            objArray(num9) = str24
                                                        End If
                                                    Loop
                                                    reader.Close()
                                                    num9 -= 1
                                                Catch exception14 As WebException
                                                    ProjectData.SetProjectError(exception14)
                                                    Dim exception5 As WebException = exception14
                                                    objArray(0) = "404"
                                                    ProjectData.ClearProjectError()
                                                End Try
                                                If (num9 <> 0) Then
                                                    Dim str25 As String = String.Concat(New String() {"Season ", episodeinfo8.seasonno, ", Episode ", episodeinfo8.episodeno, ":"})
                                                    Dim num23 As Integer = num9
                                                    Dim k As Integer = 1
                                                    Do While (k <= num23)
                                                        Dim arguments As Object() = New Object() {str25}
                                                        Dim copyBack As Boolean() = New Boolean() {True}
                                                        If copyBack(0) Then
                                                            str25 = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(arguments(0)), GetType(String)))
                                                        End If
                                                        If Not Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(k), Nothing, "indexof", arguments, Nothing, Nothing, copyBack), -1, False) Then
                                                            GoTo Label_1A0B
                                                        End If
                                                        Dim num11 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(k), Nothing, "indexof", New Object() {"<a href=""/title/"}, Nothing, Nothing, Nothing))
                                                        If (num11 = -1) Then
                                                            GoTo Label_1A0B
                                                        End If
                                                        str25 = Conversions.ToString(NewLateBinding.LateGet(objArray(k), Nothing, "substring", New Object() {(num11 + &H10), 9}, Nothing, Nothing, Nothing))
                                                        Dim classimdbscraper As New Classimdbscraper
                                                        Dim str26 As String = ""
                                                        str26 = Conversions.ToString(classimdbscraper.getimdbactors(Module1.userprefs.imdbmirror, str25, "", Module1.userprefs.maxactors))
                                                        Dim list4 As New List(Of movieactors)
                                                        Dim document2 As New XmlDocument
                                                        Dim node3 As XmlNode = Nothing
                                                        Try
                                                            Dim enumerator14 As IEnumerator
                                                            document2.LoadXml(str26)
                                                            node3 = Nothing
                                                            Dim num12 As Integer = 0
                                                            Try
                                                                enumerator14 = document2.Item("actorlist").GetEnumerator
                                                                Do While enumerator14.MoveNext
                                                                    node3 = DirectCast(enumerator14.Current, XmlNode)
                                                                    If (node3.Name = "actor") Then
                                                                        Dim enumerator15 As IEnumerator
                                                                        If (num12 >= Module1.userprefs.maxactors) Then
                                                                            GoTo Label_1976
                                                                        End If
                                                                        num12 += 1
                                                                        Dim movieactors2 As New movieactors
                                                                        Dim node4 As XmlNode = Nothing
                                                                        Try
                                                                            enumerator15 = node3.ChildNodes.GetEnumerator
                                                                            Do While enumerator15.MoveNext
                                                                                Dim str28 As String
                                                                                Dim buffer As Byte()
                                                                                Dim num13 As Integer
                                                                                Dim responseStream As Stream
                                                                                node4 = DirectCast(enumerator15.Current, XmlNode)
                                                                                Dim str36 As String = node4.Name
                                                                                Select Case str36
                                                                                    Case "name"
                                                                                        movieactors2.actorname = node4.InnerText
                                                                                        Continue Do
                                                                                    Case "role"
                                                                                        movieactors2.actorrole = node4.InnerText
                                                                                        Continue Do
                                                                                    Case "thumb"
                                                                                        movieactors2.actorthumb = node4.InnerText
                                                                                        Continue Do
                                                                                    Case Else
                                                                                        If ((str36 <> "actorid") OrElse (movieactors2.actorthumb = Nothing)) Then
                                                                                            Continue Do
                                                                                        End If
                                                                                        If Not (Module1.userprefs.actorseasy And (node4.InnerText <> "")) Then
                                                                                            GoTo Label_1647
                                                                                        End If
                                                                                        Dim str27 As String = (alleps.Item(0).episodepath.Replace(Path.GetFileName(alleps.Item(0).episodepath), "") & ".actors\")
                                                                                        Dim info3 As New DirectoryInfo(str27)
                                                                                        Dim flag5 As Boolean = False
                                                                                        If Not info3.Exists Then
                                                                                            Try
                                                                                                Directory.CreateDirectory(str27)
                                                                                                flag5 = True
                                                                                            Catch exception15 As Exception
                                                                                                ProjectData.SetProjectError(exception15)
                                                                                                Dim exception6 As Exception = exception15
                                                                                                ProjectData.ClearProjectError()
                                                                                            End Try
                                                                                        Else
                                                                                            flag5 = True
                                                                                        End If
                                                                                        If Not flag5 Then
                                                                                            GoTo Label_1647
                                                                                        End If
                                                                                        str28 = (movieactors2.actorname.Replace(" ", "_") & ".tbn")
                                                                                        Dim str29 As String = fullpath
                                                                                        str29 = Path.Combine(Path.Combine(str29.Replace(Path.GetFileName(str29), ""), ".actors\"), str28)
                                                                                        str28 = Path.Combine(str27, str28)
                                                                                        If File.Exists(str29) Then
                                                                                            Try
                                                                                                File.Copy(str29, str28, True)
                                                                                            Catch exception16 As Exception
                                                                                                ProjectData.SetProjectError(exception16)
                                                                                                ProjectData.ClearProjectError()
                                                                                            End Try
                                                                                        End If
                                                                                        If File.Exists(str28) Then
                                                                                            GoTo Label_1647
                                                                                        End If
                                                                                        buffer = New Byte(&H3D0901 - 1) {}
                                                                                        Dim num15 As Integer = 0
                                                                                        num13 = 0
                                                                                        Dim request2 As HttpWebRequest = DirectCast(WebRequest.Create(movieactors2.actorthumb), HttpWebRequest)
                                                                                        responseStream = DirectCast(request2.GetResponse, HttpWebResponse).GetResponseStream
                                                                                        Dim length As Integer = buffer.Length
                                                                                        Do While (length > 0)
                                                                                            num15 = responseStream.Read(buffer, num13, length)
                                                                                            If (num15 = 0) Then
                                                                                                Exit Do
                                                                                            End If
                                                                                            length = (length - num15)
                                                                                            num13 = (num13 + num15)
                                                                                        Loop
                                                                                        Exit Select
                                                                                End Select
                                                                                Dim stream3 As New FileStream(str28, FileMode.OpenOrCreate, FileAccess.Write)
                                                                                stream3.Write(buffer, 0, num13)
                                                                                responseStream.Close()
                                                                                stream3.Close()
Label_1647:
                                                                                If ((Module1.userprefs.actorsave And (node4.InnerText <> "")) And Not Module1.userprefs.actorseasy) Then
                                                                                    Dim str32 As String = ""
                                                                                    Dim actorsavepath As String = Module1.userprefs.actorsavepath
                                                                                    Try
                                                                                        sPath = (actorsavepath & "\" & node4.InnerText.Substring((node4.InnerText.Length - 2), 2))
                                                                                        Dim info4 As New DirectoryInfo(sPath)
                                                                                        If Not info4.Exists Then
                                                                                            Directory.CreateDirectory(sPath)
                                                                                        End If
                                                                                        str32 = String.Concat(New String() {actorsavepath, "\", node4.InnerText.Substring((node4.InnerText.Length - 2), 2), "\", node4.InnerText, ".jpg"})
                                                                                        If Not File.Exists(str32) Then
                                                                                            Dim buffer2 As Byte() = New Byte(&H3D0901 - 1) {}
                                                                                            Dim num18 As Integer = 0
                                                                                            Dim offset As Integer = 0
                                                                                            Dim request3 As HttpWebRequest = DirectCast(WebRequest.Create(movieactors2.actorthumb), HttpWebRequest)
                                                                                            Dim stream4 As Stream = DirectCast(request3.GetResponse, HttpWebResponse).GetResponseStream
                                                                                            Dim num17 As Integer = buffer2.Length
                                                                                            Do While (num17 > 0)
                                                                                                num18 = stream4.Read(buffer2, offset, num17)
                                                                                                If (num18 = 0) Then
                                                                                                    Exit Do
                                                                                                End If
                                                                                                num17 = (num17 - num18)
                                                                                                offset = (offset + num18)
                                                                                            Loop
                                                                                            Dim stream5 As New FileStream(str32, FileMode.OpenOrCreate, FileAccess.Write)
                                                                                            stream5.Write(buffer2, 0, offset)
                                                                                            stream4.Close()
                                                                                            stream5.Close()
                                                                                        End If
                                                                                        movieactors2.actorthumb = Path.Combine(Module1.userprefs.actornetworkpath, node4.InnerText.Substring((node4.InnerText.Length - 2), 2))
                                                                                        If (Module1.userprefs.actornetworkpath.IndexOf("/") <> -1) Then
                                                                                            movieactors2.actorthumb = String.Concat(New String() {Module1.userprefs.actornetworkpath, "/", node4.InnerText.Substring((node4.InnerText.Length - 2), 2), "/", node4.InnerText, ".jpg"})
                                                                                        Else
                                                                                            movieactors2.actorthumb = String.Concat(New String() {Module1.userprefs.actornetworkpath, "\", node4.InnerText.Substring((node4.InnerText.Length - 2), 2), "\", node4.InnerText, ".jpg"})
                                                                                        End If
                                                                                        Continue Do
                                                                                    Catch exception17 As Exception
                                                                                        ProjectData.SetProjectError(exception17)
                                                                                        ProjectData.ClearProjectError()
                                                                                        Continue Do
                                                                                    End Try
                                                                                End If
                                                                            Loop
                                                                        Finally
                                                                            If TypeOf enumerator15 Is IDisposable Then
                                                                                TryCast(enumerator15, IDisposable).Dispose()
                                                                            End If
                                                                        End Try
                                                                        list4.Add(movieactors2)
                                                                    End If
                                                                Loop
                                                            Finally
                                                                If TypeOf enumerator14 Is IDisposable Then
                                                                    TryCast(enumerator14, IDisposable).Dispose()
                                                                End If
                                                            End Try
                                                        Catch exception18 As Exception
                                                            ProjectData.SetProjectError(exception18)
                                                            Dim exception7 As Exception = exception18
                                                            Console.WriteLine(("Error scraping episode actors from IMDB, " & exception7.Message.ToString))
                                                            ProjectData.ClearProjectError()
                                                        End Try
Label_1976:
                                                        If (list4.Count > 0) Then
                                                            Console.WriteLine("Actors scraped from IMDB OK")
                                                            Do While (list4.Count > Module1.userprefs.maxactors)
                                                                list4.RemoveAt((list4.Count - 1))
                                                            Loop
                                                            episodeinfo8.listactors.Clear()
                                                            Dim movieactors3 As movieactors
                                                            For Each movieactors3 In list4
                                                                episodeinfo8.listactors.Add(movieactors3)
                                                            Next
                                                            list4.Clear()
                                                        Else
                                                            Console.WriteLine("Actors not scraped from IMDB, reverting to TVDB actorlist")
                                                        End If
                                                        Exit Do
Label_1A0B:
                                                        k += 1
                                                    Loop
                                                End If
                                            End If
                                        End If
                                        If Module1.userprefs.enablehdtags Then
                                            Try
                                                episodeinfo8.filedetails = DirectCast(Module1.get_hdtags(Conversions.ToString(Module1.getfilename(episodeinfo8.episodepath))), fullfiledetails)
                                                If (Not episodeinfo8.filedetails.filedetails_video.duration Is Nothing) Then
                                                    Dim num19 As Integer
                                                    Dim num20 As Integer
                                                    sPath = episodeinfo8.filedetails.filedetails_video.duration
                                                    num5 = sPath.IndexOf("h")
                                                    If (num5 <> -1) Then
                                                        num19 = Convert.ToInt32(sPath.Substring(0, num5))
                                                        sPath = Strings.Trim(sPath.Substring((num5 + 1), (sPath.Length - (num5 + 1))))
                                                    End If
                                                    num5 = sPath.IndexOf("mn")
                                                    If (num5 <> -1) Then
                                                        num20 = Convert.ToInt32(sPath.Substring(0, num5))
                                                    End If
                                                    If (num19 <> 0) Then
                                                        num19 = (num19 * 60)
                                                    End If
                                                    episodeinfo8.runtime = ((num20 + num19).ToString & " min")
                                                End If
                                            Catch exception19 As Exception
                                                ProjectData.SetProjectError(exception19)
                                                ProjectData.ClearProjectError()
                                            End Try
                                        End If
                                        Continue For
                                    End If
                                    Console.WriteLine("Could not locate this episode on TVDB, or TVDB may be unavailable")
                                    Continue For
                                End If
                                Console.WriteLine("No TVDB ID is available for this show, please scrape the show using the ""TV Show Selector"" TAB")
                            Next
                        End If
                        If ((episodepath <> "") And flag) Then
                            Dim enumerator17 As IEnumerator(Of basictvshownfo)
                            Module1.addepisode(alleps, episodepath)
                            Try
                                enumerator17 = Module1.basictvlist.GetEnumerator
                                Do While enumerator17.MoveNext
                                    Dim basictvshownfo3 As basictvshownfo = enumerator17.Current
                                    If (alleps.Item(0).episodepath.IndexOf(basictvshownfo3.fullpath.Replace("\tvshow.nfo", "")) <> -1) Then
                                        Dim enumerator18 As IEnumerator(Of episodeinfo)
                                        Try
                                            enumerator18 = alleps.GetEnumerator
                                            Do While enumerator18.MoveNext
                                                Dim episodeinfo9 As episodeinfo = enumerator18.Current
                                                Dim basicepisodenfo As New basicepisodenfo With { _
                                                    .episodeno = episodeinfo9.episodeno, _
                                                    .episodepath = episodeinfo9.episodepath, _
                                                    .playcount = "0", _
                                                    .rating = episodeinfo9.rating, _
                                                    .seasonno = episodeinfo9.seasonno, _
                                                    .title = episodeinfo9.title _
                                                }
                                                basictvshownfo3.allepisodes.Add(basicepisodenfo)
                                            Loop
                                            Continue Do
                                        Finally
                                            enumerator18.Dispose()
                                        End Try
                                    End If
                                Loop
                                Continue Do
                            Finally
                                enumerator17.Dispose()
                            End Try
                        End If
                    Loop
                Finally
                    enumerator7.Dispose()
                End Try
            End If
        End Sub

        Private Shared Sub findnewepisodes(ByVal sPath As String, ByVal pattern As String)
            Dim list As New List(Of basicepisodenfo)
            Dim files As FileInfo() = New DirectoryInfo(sPath).GetFiles(pattern, IO.SearchOption.TopDirectoryOnly)
            Dim info2 As FileInfo
            For Each info2 In files
                Try
                    Dim str As String = Path.Combine(sPath, info2.Name)
                    Dim str2 As String = str.Replace(path.GetExtension(str), ".nfo")
                    If Not File.Exists(str2) Then
                        Dim flag3 As Boolean = True
                        If (pattern = "*.vob") Then
                            Dim str3 As String = str2
                            If File.Exists(str3.Replace(path.GetFileName(str3), "VIDEO_TS.IFO")) Then
                                flag3 = False
                            End If
                        End If
                        If (pattern = "*.rar") Then
                            Dim str4 As String
                            Dim fullName As String = info2.FullName
                            If (((path.GetExtension(fullName).ToLower = ".rar") AndAlso File.Exists(fullName)) AndAlso Not File.Exists(str4)) Then
                                Dim pathName As String = fullName
                                Dim num3 As Integer = (Convert.ToInt32(Module1.userprefs.rarsize) * &H100000)
                                Dim num4 As Integer = CInt(Microsoft.VisualBasic.FileSystem.FileLen(pathName))
                                If (num4 > num3) Then
                                    Dim match As Match = Regex.Match(pathName, "\.part[0-9][0-9]?[0-9]?[0-9]?.rar")
                                    If match.Success Then
                                        pathName = match.Value
                                        If ((((pathName.ToLower.IndexOf(".part1.rar") <> -1) Or (pathName.ToLower.IndexOf(".part01.rar") <> -1)) Or (pathName.ToLower.IndexOf(".part001.rar") <> -1)) Or (pathName.ToLower.IndexOf(".part0001.rar") <> -1)) Then
                                            Dim flag4 As Boolean = False
                                            pathName = str4.Replace(".nfo", ".rar")
                                            If (pathName.ToLower.IndexOf(".part1.rar") <> -1) Then
                                                pathName = pathName.Replace(".part1.rar", ".nfo")
                                                If File.Exists(pathName) Then
                                                    flag4 = True
                                                    str4 = pathName
                                                Else
                                                    flag4 = False
                                                    str4 = pathName
                                                End If
                                            End If
                                            If (pathName.ToLower.IndexOf(".part01.rar") <> -1) Then
                                                pathName = pathName.Replace(".part01.rar", ".nfo")
                                                If File.Exists(pathName) Then
                                                    flag4 = True
                                                    str4 = pathName
                                                Else
                                                    flag4 = False
                                                    str4 = pathName
                                                End If
                                            End If
                                            If (pathName.ToLower.IndexOf(".part001.rar") <> -1) Then
                                                pathName = pathName.Replace(".part001.rar", ".nfo")
                                                If File.Exists(pathName) Then
                                                    str4 = pathName
                                                    flag4 = True
                                                Else
                                                    flag4 = False
                                                    str4 = pathName
                                                End If
                                            End If
                                            If (pathName.ToLower.IndexOf(".part0001.rar") <> -1) Then
                                                pathName = pathName.Replace(".part0001.rar", ".nfo")
                                                If File.Exists(pathName) Then
                                                    str4 = pathName
                                                    flag4 = True
                                                Else
                                                    flag4 = False
                                                    str4 = pathName
                                                End If
                                            End If
                                        Else
                                            flag3 = False
                                        End If
                                    End If
                                Else
                                    flag3 = False
                                End If
                            End If
                        End If
                        If flag3 Then
                            Dim item As New episodeinfo With { _
                                .episodepath = str2, _
                                .mediaextension = str _
                            }
                            Module1.newepisodelist.Add(item)
                        End If
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    ProjectData.ClearProjectError()
                End Try
            Next
            files = Nothing
        End Sub

        Public Shared Function get_hdtags(ByVal filename As String) As Object
            'Dim obj2 As Object
            'Try 
            '    Dim str2 As String
            '    If (Path.GetFileName(filename).ToLower = "video_ts.ifo") Then
            '        Dim sPath As String = filename.Replace(Path.GetFileName(filename), "VTS_01_0.IFO")
            '        If File.Exists(sPath) Then
            '            filename = sPath
            '        End If
            '    End If
            '    Dim list As New List(Of String)
            '    list = DirectCast(Module1.getmedialist(Conversions.ToString(Module1.getfilename(filename))), List(Of String))
            '    If Not File.Exists(filename) Then
            '        Return obj2
            '    End If
            '    Dim fullfiledetails As New fullfiledetails
            '    Dim mediainfo As New mediainfo
            '    mediainfo.Open(filename)
            '    Dim streamNumber As Integer = 0
            '    Dim num6 As Integer = mediainfo.Count_Get(StreamKind.Visual, UInt32.MaxValue)
            '    fullfiledetails.filedetails_video.width = mediainfo.Get_(StreamKind.Visual, streamNumber, "Width", InfoKind.Text, InfoKind.Name)
            '    fullfiledetails.filedetails_video.height = mediainfo.Get_(StreamKind.Visual, streamNumber, "Height", InfoKind.Text, InfoKind.Name)
            '    If (((fullfiledetails.filedetails_video.width <> Nothing) AndAlso Versioned.IsNumeric(fullfiledetails.filedetails_video.width)) AndAlso ((fullfiledetails.filedetails_video.height <> Nothing) AndAlso Versioned.IsNumeric(fullfiledetails.filedetails_video.height))) Then
            '        Dim num9 As Integer = Convert.ToInt32(fullfiledetails.filedetails_video.width)
            '        Dim num8 As Integer = Convert.ToInt32(fullfiledetails.filedetails_video.height)
            '        Try 
            '            Dim expression As New Decimal((CDbl(num9) / CDbl(num8)))
            '            expression = Conversions.ToDecimal(Strings.FormatNumber(expression, 3, TriState.UseDefault, TriState.UseDefault, TriState.UseDefault))
            '            If (Decimal.Compare(expression, Decimal.Zero) > 0) Then
            '                fullfiledetails.filedetails_video.aspect = expression.ToString
            '            End If
            '        Catch exception1 As Exception
            '            ProjectData.SetProjectError(exception1)
            '            Dim exception As Exception = exception1
            '            ProjectData.ClearProjectError
            '        End Try
            '    End If
            '    Dim extension As String = mediainfo.Get_(StreamKind.Visual, streamNumber, "Format", InfoKind.Text, InfoKind.Name)
            '    If (extension.ToLower = "avc") Then
            '        str2 = "h264"
            '    Else
            '        str2 = extension
            '    End If
            '    fullfiledetails.filedetails_video.codec = mediainfo.Get_(StreamKind.Visual, streamNumber, "CodecID", InfoKind.Text, InfoKind.Name)
            '    If (fullfiledetails.filedetails_video.codec = "DX50") Then
            '        fullfiledetails.filedetails_video.codec = "DIVX"
            '    End If
            '    If (fullfiledetails.filedetails_video.codec.ToLower.IndexOf("mpeg4/iso/avc") <> -1) Then
            '        fullfiledetails.filedetails_video.codec = "h264"
            '    End If
            '    fullfiledetails.filedetails_video.formatinfo = mediainfo.Get_(StreamKind.Visual, streamNumber, "CodecID", InfoKind.Text, InfoKind.Name)
            '    Dim strArray As String() = New String(&H65  - 1) {}
            '    Dim index As Integer = 1
            '    Do
            '        strArray(index) = mediainfo.Get_(StreamKind.Visual, 0, index, InfoKind.Text)
            '        index += 1
            '    Loop While (index <= 100)
            '    Try 
            '        If (list.Count = 1) Then
            '            fullfiledetails.filedetails_video.duration = mediainfo.Get_(StreamKind.Visual, 0, &H3D, InfoKind.Text)
            '        ElseIf (list.Count > 1) Then
            '            Dim num11 As Integer = 0
            '            Dim num16 As Integer = (list.Count - 1)
            '            Dim i As Integer = 0
            '            Do While (i <= num16)
            '                Dim mediainfo2 As New mediainfo
            '                mediainfo2.Open(list.Item(i))
            '                Dim str5 As String = mediainfo2.Get_(StreamKind.Visual, 0, &H3D, InfoKind.Text)
            '                If (str5 <> Nothing) Then
            '                    Try 
            '                        Dim num14 As Integer = 0
            '                        Dim num15 As Integer = 0
            '                        Dim str6 As String = str5
            '                        Dim length As Integer = str6.IndexOf("h")
            '                        If (length <> -1) Then
            '                            num14 = Convert.ToInt32(str6.Substring(0, length))
            '                            str6 = Strings.Trim(str6.Substring((length + 1), (str6.Length - (length + 1))))
            '                        End If
            '                        length = str6.IndexOf("mn")
            '                        If (length <> -1) Then
            '                            num15 = Convert.ToInt32(str6.Substring(0, length))
            '                        End If
            '                        If (num14 <> 0) Then
            '                            num14 = (num14 * 60)
            '                        End If
            '                        num15 = (num15 + num14)
            '                        num11 = (num11 + num15)
            '                    Catch exception3 As Exception
            '                        ProjectData.SetProjectError(exception3)
            '                        ProjectData.ClearProjectError
            '                    End Try
            '                End If
            '                i += 1
            '            Loop
            '            fullfiledetails.filedetails_video.duration = (Conversions.ToString(num11) & " min")
            '        End If
            '    Catch exception4 As Exception
            '        ProjectData.SetProjectError(exception4)
            '        fullfiledetails.filedetails_video.duration = mediainfo.Get_(StreamKind.Visual, 0, &H39, InfoKind.Text)
            '        ProjectData.ClearProjectError
            '    End Try
            '    fullfiledetails.filedetails_video.bitrate = mediainfo.Get_(StreamKind.Visual, streamNumber, "BitRate/String", InfoKind.Text, InfoKind.Name)
            '    fullfiledetails.filedetails_video.bitratemode = mediainfo.Get_(StreamKind.Visual, streamNumber, "BitRate_Mode/String", InfoKind.Text, InfoKind.Name)
            '    fullfiledetails.filedetails_video.bitratemax = mediainfo.Get_(StreamKind.Visual, streamNumber, "BitRate_Maximum/String", InfoKind.Text, InfoKind.Name)
            '    extension = Path.GetExtension(filename)
            '    fullfiledetails.filedetails_video.container = extension
            '    fullfiledetails.filedetails_video.codecinfo = mediainfo.Get_(StreamKind.Visual, streamNumber, "CodecID/Info", InfoKind.Text, InfoKind.Name)
            '    fullfiledetails.filedetails_video.scantype = mediainfo.Get_(StreamKind.Visual, streamNumber, &H66, InfoKind.Text)
            '    Dim num4 As Integer = mediainfo.Count_Get(StreamKind.Audio, UInt32.MaxValue)
            '    Dim num As Integer = 0
            '    If (num4 > 0) Then
            '        Do While (num < num4)
            '            Dim item As New medianfo_audio With { _
            '                .language = Module1.getlangcode(mediainfo.Get_(StreamKind.Audio, num, "Language/String", InfoKind.Text, InfoKind.Name)) _
            '            }
            '            If (mediainfo.Get_(StreamKind.Audio, num, "Format", InfoKind.Text, InfoKind.Name) = "MPEG Audio") Then
            '                item.codec = "MP3"
            '            Else
            '                item.codec = mediainfo.Get_(StreamKind.Audio, num, "Format", InfoKind.Text, InfoKind.Name)
            '            End If
            '            If (item.codec = "AC-3") Then
            '                item.codec = "AC3"
            '            End If
            '            item.channels = mediainfo.Get_(StreamKind.Audio, num, "Channel(s)", InfoKind.Text, InfoKind.Name)
            '            item.bitrate = mediainfo.Get_(StreamKind.Audio, num, "BitRate/String", InfoKind.Text, InfoKind.Name)
            '            fullfiledetails.filedetails_audio.Add(item)
            '            num += 1
            '        Loop
            '    End If
            '    Dim num5 As Integer = mediainfo.Count_Get(StreamKind.Text, UInt32.MaxValue)
            '    Dim num2 As Integer = 0
            '    If (num5 > 0) Then
            '        Do While (num2 < num5)
            '            Dim _subtitles As New medianfo_subtitles With { _
            '                .language = Module1.getlangcode(mediainfo.Get_(StreamKind.Text, num2, "Language/String", InfoKind.Text, InfoKind.Name)) _
            '            }
            '            fullfiledetails.filedetails_subtitles.Add(_subtitles)
            '            num2 += 1
            '        Loop
            '    End If
            '    obj2 = fullfiledetails
            'Catch exception5 As Exception
            '    ProjectData.SetProjectError(exception5)
            '    Dim exception2 As Exception = exception5
            '    ProjectData.ClearProjectError
            'End Try
            'Return obj2
        End Function

        Public Shared Function getactorthumbpath(ByVal Optional location As String = "") As Object
            Dim obj2 As Object
            Dim sPath As String = ""
            Try
                If (location = Nothing) Then
                    Return "none"
                End If
                If (location = "") Then
                    Return "none"
                End If
                If (location.IndexOf("http") <> -1) Then
                    Return location
                End If
                If (location.IndexOf(Module1.userprefs.actornetworkpath) <> -1) Then
                    If ((Module1.userprefs.actornetworkpath <> Nothing) And (Module1.userprefs.actorsavepath <> Nothing)) Then
                        If ((Module1.userprefs.actornetworkpath <> "") And (Module1.userprefs.actorsavepath <> "")) Then
                            Dim fileName As String = Path.GetFileName(location)
                            sPath = Path.Combine(Module1.userprefs.actorsavepath, fileName)
                            If Not File.Exists(sPath) Then
                                Dim extension As String = Path.GetExtension(location)
                                Dim str4 As String = Path.GetFileName(location).Replace(extension, "")
                                sPath = String.Concat(New String() {Module1.userprefs.actorsavepath, "\", str4.Substring((str4.Length - 2), 2), "\", fileName})
                            End If
                            If Not File.Exists(sPath) Then
                                sPath = "none"
                            End If
                        End If
                    Else
                        sPath = "none"
                    End If
                Else
                    sPath = "none"
                End If
                If (sPath = "") Then
                    sPath = "none"
                End If
                obj2 = sPath
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                obj2 = "none"
                ProjectData.ClearProjectError()
            End Try
            Return obj2
        End Function

        Public Shared Function GetCRC32(ByVal sFileName As String) As String
            Dim crc As New CRC32
            Dim encoding As New UTF7Encoding
            Return Conversions.ToString(crc.GetCrc32(New MemoryStream(encoding.GetBytes(sFileName))))
        End Function

        Public Shared Function getfanartpath(ByVal fullpath As String) As String
            Dim str As String
            Try 
                Dim sPath As String = ""
                sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & "-fanart.jpg")
                If Not File.Exists(sPath) Then
                    Dim newValue As String = Module1.getstackname(Path.GetFileName(fullpath), fullpath.Replace(Path.GetFileName(fullpath), ""))
                    newValue = (fullpath.Replace(Path.GetFileName(fullpath), newValue) & "-fanart.jpg")
                    If ((newValue <> "na") And File.Exists(newValue)) Then
                        sPath = newValue
                    Else
                        sPath = (sPath.Replace(Path.GetFileName(sPath), "") & "fanart.jpg")
                        If Not File.Exists(sPath) Then
                            sPath = ""
                        End If
                    End If
                End If
                If ((sPath = "") AndAlso (fullpath.IndexOf("movie.nfo") <> -1)) Then
                    sPath = fullpath.Replace("movie.nfo", "fanart.jpg")
                End If
                If (sPath = "") Then
                    If Module1.userprefs.fanartnotstacked Then
                        sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & "-fanart.jpg")
                    Else
                        sPath = (Module1.getstackname(Path.GetFileName(fullpath), fullpath) & "-fanart.jpg")
                        If (sPath = "na-fanart.jpg") Then
                            sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & "-fanart.jpg")
                        Else
                            sPath = fullpath.Replace(Path.GetFileName(fullpath), sPath)
                        End If
                    End If
                    If Module1.userprefs.basicsavemode Then
                        sPath = sPath.Replace(Path.GetFileName(sPath), "fanart.jpg")
                    End If
                End If
                str = sPath
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                str = ""
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Public Shared Function getfilename(ByVal sPath As String) As Object
            Dim obj2 As Object
            Dim objectValue As Object = RuntimeHelpers.GetObjectValue(New Object)
            Monitor.Enter(RuntimeHelpers.GetObjectValue(objectValue))
            Try
                Dim str2 As String = sPath
                Dim str As String = ""
                Dim strArray As String() = New String(&H17 - 1) {}
                strArray(1) = ".avi"
                strArray(2) = ".xvid"
                strArray(3) = ".divx"
                strArray(4) = ".img"
                strArray(5) = ".mpg"
                strArray(6) = ".mpeg"
                strArray(7) = ".mov"
                strArray(8) = ".rm"
                strArray(9) = ".3gp"
                strArray(10) = ".m4v"
                strArray(11) = ".wmv"
                strArray(12) = ".asf"
                strArray(13) = ".mp4"
                strArray(14) = ".mkv"
                strArray(15) = ".nrg"
                strArray(&H10) = ".iso"
                strArray(&H11) = ".rmvb"
                strArray(&H12) = ".ogm"
                strArray(&H13) = ".bin"
                strArray(20) = ".ts"
                strArray(&H15) = ".vob"
                strArray(&H16) = ".m2ts"
                strArray(&H17) = ".strm"
                If File.Exists(str2.Replace(Path.GetFileName(str2), "VIDEO_TS.IFO")) Then
                    str = str2.Replace(Path.GetFileName(str2), "VIDEO_TS.IFO")
                End If
                If (str = "") Then
                    Dim index As Integer = 1
                    Do
                        str2 = str2.Replace(Path.GetExtension(str2), strArray(index))
                        If File.Exists(str2) Then
                            str = str2
                            Exit Do
                        End If
                        index += 1
                    Loop While (index <= &H16)
                End If
                If ((str = "") AndAlso (str2.IndexOf("movie.nfo") <> -1)) Then
                    Dim strArray2 As String() = New String(&H3E9 - 1) {}
                    Dim num3 As Integer = 0
                    Dim num4 As Integer = 1
                    Do
                        Dim info As New DirectoryInfo(str2.Replace(Path.GetFileName(str2), ""))
                        Dim searchPattern As String = ("*" & strArray(num4))
                        Dim info2 As FileInfo
                        For Each info2 In info.GetFiles(searchPattern)
                            If File.Exists(info2.FullName) Then
                                Dim str3 As String = info2.FullName.ToLower
                                If ((((str3.IndexOf("-trailer") = -1) And (str3.IndexOf("-sample") = -1)) And (str3.IndexOf(".trailer") = -1)) And (str3.IndexOf(".sample") = -1)) Then
                                    num3 += 1
                                    strArray2(num3) = info2.FullName
                                End If
                            End If
                        Next
                        num4 += 1
                    Loop While (num4 <= &H17)
                    If (num3 = 1) Then
                        str = strArray2(num3)
                    ElseIf (num3 > 1) Then
                        Dim strArray3 As String() = New String(7 - 1) {}
                        strArray3(1) = "cd"
                        strArray3(2) = "dvd"
                        strArray3(3) = "part"
                        strArray3(4) = "pt"
                        strArray3(5) = "disk"
                        strArray3(6) = "disc"
                        Dim strArray4 As String() = New String(6 - 1) {}
                        strArray4(1) = ""
                        strArray4(2) = "-"
                        strArray4(3) = "_"
                        strArray4(4) = " "
                        strArray4(5) = "."
                        Dim num5 As Integer = 1
                        Do
                            Dim num6 As Integer = 1
                            Do
                                Dim num9 As Integer = num3
                                Dim i As Integer = 1
                                Do While (i <= num9)
                                    Dim str6 As String = (strArray3(num5) & strArray4(i) & "1")
                                    If (strArray2(i).ToLower.IndexOf(str6) <> -1) Then
                                        str = strArray2(i)
                                    End If
                                    i += 1
                                Loop
                                num6 += 1
                            Loop While (num6 <= 5)
                            num5 += 1
                        Loop While (num5 <= 6)
                    End If
                End If
                If (str = "") Then
                    str = "none"
                End If
                obj2 = str
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError()
            Finally
                Monitor.Exit(RuntimeHelpers.GetObjectValue(objectValue))
            End Try
            Return obj2
        End Function

        Public Shared Function getimdbactors(ByVal imdbid As String, ByVal mirror As String) As Object
            Dim objArray(,) As Object = New Object(&H3E9 - 1, 3 - 1) {}
            Dim num As Integer = 0
            Module1.tvdburl = (mirror & "title/" & imdbid & "/fullcredits#cast")
            Module1.loadwebpage(Module1.tvdburl)
            Try 
                If (Module1.tvdbwebsource(1) <> "404") Then
                    Dim tvfblinecount As Integer = Module1.tvfblinecount
                    Dim i As Integer = 1
                    Do While (i <= tvfblinecount)
                        If (Module1.tvdbwebsource(i).IndexOf("Episode  Cast</a>") <> -1) Then
                            Module1.tvdbwebsource(i) = Module1.tvdbwebsource(i).Substring(Module1.tvdbwebsource(i).IndexOf("Cast</a>"), (Module1.tvdbwebsource(i).Length - Module1.tvdbwebsource(i).IndexOf("Cast</a>")))
                            If (Module1.tvdbwebsource(i).IndexOf("<tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>") <> -1) Then
                                Module1.tvdbwebsource(i) = Module1.tvdbwebsource(i).Replace("</td></tr> <tr><td align=""center"" colspan=""4""><small>rest of cast listed alphabetically:</small></td></tr>", "</td></tr><tr class")
                            End If
                            Dim right As Integer = 0
                            Dim str As String = Module1.tvdbwebsource(i)
                            Do While (str.IndexOf("</td></tr><tr class") <> -1)
                                right += 1
                                objArray(right, 0) = str.Substring(0, (str.IndexOf("</td></tr><tr class") + &H13))
                                str = str.Replace(Conversions.ToString(objArray(right, 0)), "")
                            Loop
                            right += 1
                            objArray(right, 0) = str
                            num = right
                            Dim num12 As Integer = num
                            Dim j As Integer = 1
                            Do While (j <= num12)
                                Dim objArray2 As Object()
                                Dim flagArray As Boolean()
                                If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "http://resume.imdb.com" }, Nothing, Nothing, Nothing), -1, False) Then
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Replace", New Object() { "http://resume.imdb.com", "" }, Nothing, Nothing, Nothing))
                                End If
                                If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "http://i.media-imdb.com/images/tn15/addtiny.gif" }, Nothing, Nothing, Nothing), -1, False) Then
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Replace", New Object() { "http://i.media-imdb.com/images/tn15/addtiny.gif", "" }, Nothing, Nothing, Nothing))
                                End If
                                If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</td></tr></table>" }, Nothing, Nothing, Nothing), -1, False) Then
                                    right = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</td></tr></table>" }, Nothing, Nothing, Nothing))
                                    objArray2 = New Object() { right, Operators.SubtractObject(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Length", New Object(0  - 1) {}, Nothing, Nothing, Nothing), right) }
                                    flagArray = New Boolean() { True, False }
                                    If flagArray(0) Then
                                        right = CInt(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray2(0)), GetType(Integer)))
                                    End If
                                    str = Conversions.ToString(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", objArray2, Nothing, Nothing, flagArray))
                                    objArray2 = New Object() { str, "</td></tr><tr class" }
                                    flagArray = New Boolean() { True, False }
                                    If flagArray(0) Then
                                        str = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray2(0)), GetType(String)))
                                    End If
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Replace", objArray2, Nothing, Nothing, flagArray))
                                End If
                                If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "indexof", New Object() { "http" }, Nothing, Nothing, Nothing), -1, False) Then
                                    Dim num5 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "http" }, Nothing, Nothing, Nothing))
                                    Dim num6 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "._V1._" }, Nothing, Nothing, Nothing))
                                    objArray2 = New Object() { num5, ((num6 - num5) + 6) }
                                    flagArray = New Boolean() { True, False }
                                    If flagArray(0) Then
                                        num5 = CInt(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(objArray2(0)), GetType(Integer)))
                                    End If
                                    objArray(j, 2) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", objArray2, Nothing, Nothing, flagArray))
                                    objArray(j, 2) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 2), Nothing, "Replace", New Object() { "._V1._", "._V1._SY200_SX300_.jpg" }, Nothing, Nothing, Nothing))
                                End If
                                If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "a href=""/character" }, Nothing, Nothing, Nothing), -1, False) Then
                                    objArray(j, 1) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", New Object() { Operators.AddObject(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "a href=""/character" }, Nothing, Nothing, Nothing), &H13), Operators.SubtractObject(Operators.SubtractObject(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</td></tr><tr class" }, Nothing, Nothing, Nothing), NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "a href=""/character" }, Nothing, Nothing, Nothing)), &H13) }, Nothing, Nothing, Nothing))
                                    If Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(objArray(j, 1), Nothing, "IndexOf", New Object() { "</a>" }, Nothing, Nothing, Nothing), -1, False) Then
                                        objArray(j, 1) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 1), Nothing, "Substring", New Object() { 12, Operators.SubtractObject(NewLateBinding.LateGet(objArray(j, 1), Nothing, "IndexOf", New Object() { "</a>" }, Nothing, Nothing, Nothing), 12) }, Nothing, Nothing, Nothing))
                                    ElseIf Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objArray(j, 1), Nothing, "IndexOf", New Object() { "</a>" }, Nothing, Nothing, Nothing), -1, False) Then
                                        objArray(j, 1) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 1), Nothing, "Substring", New Object() { 12, Operators.SubtractObject(NewLateBinding.LateGet(objArray(j, 1), Nothing, "Length", New Object(0  - 1) {}, Nothing, Nothing, Nothing), 12) }, Nothing, Nothing, Nothing))
                                    End If
                                    Dim objArray4 As Object() = New Object(2  - 1) {}
                                    Dim num13 As Integer = j
                                    Dim num14 As Integer = 0
                                    Dim arguments As Object() = New Object(1  - 1) {}
                                    Dim str2 As String = "a href=""/character"
                                    arguments(0) = str2
                                    objArray4(0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(num13, num14), Nothing, "IndexOf", arguments, Nothing, Nothing, Nothing))
                                    objArray4(1) = Operators.SubtractObject(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Length", New Object(0  - 1) {}, Nothing, Nothing, Nothing), NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "a href=""/character" }, Nothing, Nothing, Nothing))
                                    Dim objArray3 As Object() = objArray4
                                    flagArray = New Boolean() { True, False }
                                    If flagArray(0) Then
                                        NewLateBinding.LateSetComplex(objArray(num13, num14), Nothing, "IndexOf", New Object() { str2, RuntimeHelpers.GetObjectValue(objArray3(0)) }, Nothing, Nothing, True, False)
                                    End If
                                    str = Conversions.ToString(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", objArray3, Nothing, Nothing, flagArray))
                                    arguments = New Object() { str, "" }
                                    flagArray = New Boolean() { True, False }
                                    If flagArray(0) Then
                                        str = CStr(Conversions.ChangeType(RuntimeHelpers.GetObjectValue(arguments(0)), GetType(String)))
                                    End If
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Replace", arguments, Nothing, Nothing, flagArray))
                                    Dim num7 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "/"">" }, Nothing, Nothing, Nothing))
                                    Dim num8 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</a></td>" }, Nothing, Nothing, Nothing))
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", New Object() { (num7 + 3), (num8 - (num7 + 3)) }, Nothing, Nothing, Nothing))
                                ElseIf Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "a href=""/character" }, Nothing, Nothing, Nothing), -1, False) Then
                                    Dim num9 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "<td class=""char"">" }, Nothing, Nothing, Nothing))
                                    Dim num10 As Integer = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</td></tr><tr class" }, Nothing, Nothing, Nothing))
                                    objArray(j, 1) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", New Object() { (num9 + &H11), ((num10 - num9) - &H11) }, Nothing, Nothing, Nothing))
                                    num9 = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "/"">" }, Nothing, Nothing, Nothing))
                                    num10 = Conversions.ToInteger(NewLateBinding.LateGet(objArray(j, 0), Nothing, "IndexOf", New Object() { "</a></td>" }, Nothing, Nothing, Nothing))
                                    objArray(j, 0) = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objArray(j, 0), Nothing, "Substring", New Object() { (num9 + 3), (num10 - (num9 + 3)) }, Nothing, Nothing, Nothing))
                                End If
                                j += 1
                            Loop
                        End If
                        i += 1
                    Loop
                    Return objArray
                End If
                objArray(0, 0) = "404"
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                objArray(0, 0) = "404"
                ProjectData.ClearProjectError
            End Try
            Return objArray
        End Function

        Public Shared Function getlangcode(ByVal strLang As String) As String
            Dim str As String
            Dim objectValue As Object = RuntimeHelpers.GetObjectValue(New Object)
            Monitor.Enter(RuntimeHelpers.GetObjectValue(objectValue))
            Try 
                Dim str2 As String = strLang.ToLower
                Select Case str2
                    Case "english"
                        Return "eng"
                    Case "german"
                        Return "deu"
                    Case ""
                        Return ""
                    Case "afar"
                        Return "aar"
                    Case "abkhazian"
                        Return "abk"
                    Case "achinese"
                        Return "ace"
                    Case "acoli"
                        Return "ach"
                    Case "adangme"
                        Return "ada"
                    Case "adyghe", "adygei"
                        Return "ady"
                    Case "afro-asiatic (other)"
                        Return "afa"
                    Case "afrihili"
                        Return "afh"
                    Case "afrikaans"
                        Return "afr"
                    Case "ainu"
                        Return "ain"
                    Case "akan"
                        Return "aka"
                    Case "akkadian"
                        Return "akk"
                    Case "albanian"
                        Return "alb"
                    Case "aleut"
                        Return "ale"
                    Case "algonquian languages"
                        Return "alg"
                    Case "southern altai"
                        Return "alt"
                    Case "amharic"
                        Return "amh"
                End Select
                Select Case str2
                    Case "english"
                        Return "ang"
                    Case "angika"
                        Return "anp"
                    Case "apache languages"
                        Return "apa"
                    Case "arabic"
                        Return "ara"
                    Case "official aramaic (700-300 bce)", "imperial aramaic (700-300 bce)"
                        Return "arc"
                    Case "aragonese"
                        Return "arg"
                    Case "armenian"
                        Return "arm"
                    Case "mapudungun", "mapuche"
                        Return "arn"
                    Case "arapaho"
                        Return "arp"
                    Case "artificial (other)"
                        Return "art"
                    Case "arawak"
                        Return "arw"
                    Case "assamese"
                        Return "asm"
                    Case "asturian", "bable", "leonese", "asturleonese"
                        Return "ast"
                    Case "athapascan languages"
                        Return "ath"
                    Case "australian languages"
                        Return "aus"
                    Case "avaric"
                        Return "ava"
                    Case "avestan"
                        Return "ave"
                    Case "awadhi"
                        Return "awa"
                    Case "aymara"
                        Return "aym"
                    Case "azerbaijani"
                        Return "aze"
                    Case "banda languages"
                        Return "bad"
                    Case "bamileke languages"
                        Return "bai"
                    Case "bashkir"
                        Return "bak"
                    Case "baluchi"
                        Return "bal"
                    Case "bambara"
                        Return "bam"
                    Case "balinese"
                        Return "ban"
                    Case "basque"
                        Return "baq"
                    Case "basa"
                        Return "bas"
                    Case "baltic (other)"
                        Return "bat"
                    Case "beja", "bedawiyet"
                        Return "bej"
                    Case "belarusian"
                        Return "bel"
                    Case "bemba"
                        Return "bem"
                    Case "bengali"
                        Return "ben"
                    Case "berber (other)"
                        Return "ber"
                    Case "bhojpuri"
                        Return "bho"
                    Case "bihari"
                        Return "bih"
                    Case "bikol"
                        Return "bik"
                    Case "bini", "edo"
                        Return "bin"
                    Case "bislama"
                        Return "bis"
                    Case "siksika"
                        Return "bla"
                    Case "bantu (other)"
                        Return "bnt"
                    Case "bosnian"
                        Return "bos"
                    Case "braj"
                        Return "bra"
                    Case "breton"
                        Return "bre"
                    Case "batak languages"
                        Return "btk"
                    Case "buriat"
                        Return "bua"
                    Case "buginese"
                        Return "bug"
                    Case "bulgarian"
                        Return "bul"
                    Case "burmese"
                        Return "bur"
                    Case "blin", "bilin"
                        Return "byn"
                    Case "caddo"
                        Return "cad"
                    Case "central american indian (other)"
                        Return "cai"
                    Case "galibi carib"
                        Return "car"
                    Case "catalan", "valencian"
                        Return "cat"
                    Case "caucasian (other)"
                        Return "cau"
                    Case "cebuano"
                        Return "ceb"
                    Case "celtic (other)"
                        Return "cel"
                    Case "chamorro"
                        Return "cha"
                    Case "chibcha"
                        Return "chb"
                    Case "chechen"
                        Return "che"
                    Case "chagatai"
                        Return "chg"
                    Case "chinese"
                        Return "chi"
                    Case "chuukese"
                        Return "chk"
                    Case "mari"
                        Return "chm"
                    Case "chinook jargon"
                        Return "chn"
                    Case "choctaw"
                        Return "cho"
                    Case "chipewyan", "dene suline"
                        Return "chp"
                    Case "cherokee"
                        Return "chr"
                    Case "church slavic", "old slavonic", "church slavonic", "old bulgarian", "old church slavonic"
                        Return "chu"
                    Case "chuvash"
                        Return "chv"
                    Case "cheyenne"
                        Return "chy"
                    Case "chamic languages"
                        Return "cmc"
                    Case "coptic"
                        Return "cop"
                    Case "cornish"
                        Return "cor"
                    Case "corsican"
                        Return "cos"
                    Case "creoles and pidgins"
                        Return "cpe"
                End Select
                If (str2 = "creoles and pidgins") Then
                    Return "cpf"
                End If
                If (str2 = "creoles and pidgins") Then
                    Return "cpp"
                End If
                If (str2 = "cree") Then
                    Return "cre"
                End If
                If ((str2 = "crimean tatar") OrElse (str2 = "crimean turkish")) Then
                    Return "crh"
                End If
                If (str2 = "creoles and pidgins (other)") Then
                    Return "crp"
                End If
                If (str2 = "kashubian") Then
                    Return "csb"
                End If
                If (str2 = "cushitic (other)") Then
                    Return "cus"
                End If
                If (str2 = "czech") Then
                    Return "cze"
                End If
                If (str2 = "dakota") Then
                    Return "dak"
                End If
                If (str2 = "danish") Then
                    Return "dan"
                End If
                If (str2 = "dargwa") Then
                    Return "dar"
                End If
                If (str2 = "land dayak languages") Then
                    Return "day"
                End If
                If (str2 = "delaware") Then
                    Return "del"
                End If
                If (str2 = "slave (athapascan)") Then
                    Return "den"
                End If
                If (str2 = "dogrib") Then
                    Return "dgr"
                End If
                If (str2 = "dinka") Then
                    Return "din"
                End If
                If (((str2 = "divehi") OrElse (str2 = "dhivehi")) OrElse (str2 = "maldivian")) Then
                    Return "div"
                End If
                If (str2 = "dogri") Then
                    Return "doi"
                End If
                If (str2 = "dravidian (other)") Then
                    Return "dra"
                End If
                If (str2 = "lower sorbian") Then
                    Return "dsb"
                End If
                If (str2 = "duala") Then
                    Return "dua"
                End If
                If (str2 = "dutch") Then
                    Return "dum"
                End If
                If ((str2 = "dutch") OrElse (str2 = "flemish")) Then
                    Return "dut"
                End If
                If (str2 = "dyula") Then
                    Return "dyu"
                End If
                If (str2 = "dzongkha") Then
                    Return "dzo"
                End If
                If (str2 = "efik") Then
                    Return "efi"
                End If
                If (str2 = "egyptian (ancient)") Then
                    Return "egy"
                End If
                If (str2 = "ekajuk") Then
                    Return "eka"
                End If
                If (str2 = "elamite") Then
                    Return "elx"
                End If
                If (str2 = "english") Then
                    Return "eng"
                End If
                If (str2 = "english") Then
                    Return "enm"
                End If
                If (str2 = "esperanto") Then
                    Return "epo"
                End If
                If (str2 = "estonian") Then
                    Return "est"
                End If
                If (str2 = "ewe") Then
                    Return "ewe"
                End If
                If (str2 = "ewondo") Then
                    Return "ewo"
                End If
                If (str2 = "fang") Then
                    Return "fan"
                End If
                If (str2 = "faroese") Then
                    Return "fao"
                End If
                If (str2 = "fanti") Then
                    Return "fat"
                End If
                If (str2 = "fijian") Then
                    Return "fij"
                End If
                If ((str2 = "filipino") OrElse (str2 = "pilipino")) Then
                    Return "fil"
                End If
                If (str2 = "finnish") Then
                    Return "fin"
                End If
                If (str2 = "finno-ugrian (other)") Then
                    Return "fiu"
                End If
                If (str2 = "fon") Then
                    Return "fon"
                End If
                If (str2 = "french") Then
                    Return "fre"
                End If
                If (str2 = "french") Then
                    Return "frm"
                End If
                If (str2 = "french") Then
                    Return "fro"
                End If
                If (str2 = "northern frisian") Then
                    Return "frr"
                End If
                If (str2 = "eastern frisian") Then
                    Return "frs"
                End If
                If (str2 = "western frisian") Then
                    Return "fry"
                End If
                If (str2 = "fulah") Then
                    Return "ful"
                End If
                If (str2 = "friulian") Then
                    Return "fur"
                End If
                If (str2 = "ga") Then
                    Return "gaa"
                End If
                If (str2 = "gayo") Then
                    Return "gay"
                End If
                If (str2 = "gbaya") Then
                    Return "gba"
                End If
                If (str2 = "germanic (other)") Then
                    Return "gem"
                End If
                If (str2 = "georgian") Then
                    Return "geo"
                End If
                If (str2 = "german") Then
                    Return "ger"
                End If
                If (str2 = "geez") Then
                    Return "gez"
                End If
                If (str2 = "gilbertese") Then
                    Return "gil"
                End If
                If ((str2 = "gaelic") OrElse (str2 = "scottish gaelic")) Then
                    Return "gla"
                End If
                If (str2 = "irish") Then
                    Return "gle"
                End If
                If (str2 = "galician") Then
                    Return "glg"
                End If
                If (str2 = "manx") Then
                    Return "glv"
                End If
                If (str2 = "german") Then
                    Return "gmh"
                End If
                If (str2 = "german") Then
                    Return "goh"
                End If
                If (str2 = "gondi") Then
                    Return "gon"
                End If
                If (str2 = "gorontalo") Then
                    Return "gor"
                End If
                If (str2 = "gothic") Then
                    Return "got"
                End If
                If (str2 = "grebo") Then
                    Return "grb"
                End If
                If (str2 = "greek") Then
                    Return "grc"
                End If
                If (str2 = "greek") Then
                    Return "gre"
                End If
                If (str2 = "guarani") Then
                    Return "grn"
                End If
                If (((str2 = "swiss german") OrElse (str2 = "alemannic")) OrElse (str2 = "alsatian")) Then
                    Return "gsw"
                End If
                If (str2 = "gujarati") Then
                    Return "guj"
                End If
                If (str2 = "gwich'in") Then
                    Return "gwi"
                End If
                If (str2 = "haida") Then
                    Return "hai"
                End If
                If ((str2 = "haitian") OrElse (str2 = "haitian creole")) Then
                    Return "hat"
                End If
                If (str2 = "hausa") Then
                    Return "hau"
                End If
                If (str2 = "hawaiian") Then
                    Return "haw"
                End If
                If (str2 = "hebrew") Then
                    Return "heb"
                End If
                If (str2 = "herero") Then
                    Return "her"
                End If
                If (str2 = "hiligaynon") Then
                    Return "hil"
                End If
                If (str2 = "himachali") Then
                    Return "him"
                End If
                If (str2 = "hindi") Then
                    Return "hin"
                End If
                If (str2 = "hittite") Then
                    Return "hit"
                End If
                If (str2 = "hmong") Then
                    Return "hmn"
                End If
                If (str2 = "hiri motu") Then
                    Return "hmo"
                End If
                If (str2 = "croatian") Then
                    Return "hrv"
                End If
                If (str2 = "upper sorbian") Then
                    Return "hsb"
                End If
                If (str2 = "hungarian") Then
                    Return "hun"
                End If
                If (str2 = "hupa") Then
                    Return "hup"
                End If
                If (str2 = "iban") Then
                    Return "iba"
                End If
                If (str2 = "igbo") Then
                    Return "ibo"
                End If
                If (str2 = "icelandic") Then
                    Return "ice"
                End If
                If (str2 = "ido") Then
                    Return "ido"
                End If
                If ((str2 = "sichuan yi") OrElse (str2 = "nuosu")) Then
                    Return "iii"
                End If
                If (str2 = "ijo languages") Then
                    Return "ijo"
                End If
                If (str2 = "inuktitut") Then
                    Return "iku"
                End If
                If ((str2 = "interlingue") OrElse (str2 = "occidental")) Then
                    Return "ile"
                End If
                If (str2 = "iloko") Then
                    Return "ilo"
                End If
                If (str2 = "interlingua (international auxiliary language association)") Then
                    Return "ina"
                End If
                If (str2 = "indic (other)") Then
                    Return "inc"
                End If
                If (str2 = "indonesian") Then
                    Return "ind"
                End If
                If (str2 = "indo-european (other)") Then
                    Return "ine"
                End If
                If (str2 = "ingush") Then
                    Return "inh"
                End If
                If (str2 = "inupiaq") Then
                    Return "ipk"
                End If
                If (str2 = "iranian (other)") Then
                    Return "ira"
                End If
                If (str2 = "iroquoian languages") Then
                    Return "iro"
                End If
                If (str2 = "italian") Then
                    Return "ita"
                End If
                If (str2 = "javanese") Then
                    Return "jav"
                End If
                If (str2 = "lojban") Then
                    Return "jbo"
                End If
                If (str2 = "japanese") Then
                    Return "jpn"
                End If
                If (str2 = "judeo-persian") Then
                    Return "jpr"
                End If
                If (str2 = "judeo-arabic") Then
                    Return "jrb"
                End If
                If (str2 = "kara-kalpak") Then
                    Return "kaa"
                End If
                If (str2 = "kabyle") Then
                    Return "kab"
                End If
                If ((str2 = "kachin") OrElse (str2 = "jingpho")) Then
                    Return "kac"
                End If
                If ((str2 = "kalaallisut") OrElse (str2 = "greenlandic")) Then
                    Return "kal"
                End If
                If (str2 = "kamba") Then
                    Return "kam"
                End If
                If (str2 = "kannada") Then
                    Return "kan"
                End If
                If (str2 = "karen languages") Then
                    Return "kar"
                End If
                If (str2 = "kashmiri") Then
                    Return "kas"
                End If
                If (str2 = "kanuri") Then
                    Return "kau"
                End If
                If (str2 = "kawi") Then
                    Return "kaw"
                End If
                If (str2 = "kazakh") Then
                    Return "kaz"
                End If
                If (str2 = "kabardian") Then
                    Return "kbd"
                End If
                If (str2 = "khasi") Then
                    Return "kha"
                End If
                If (str2 = "khoisan (other)") Then
                    Return "khi"
                End If
                If (str2 = "central khmer") Then
                    Return "khm"
                End If
                If ((str2 = "khotanese") OrElse (str2 = "sakan")) Then
                    Return "kho"
                End If
                If ((str2 = "kikuyu") OrElse (str2 = "gikuyu")) Then
                    Return "kik"
                End If
                If (str2 = "kinyarwanda") Then
                    Return "kin"
                End If
                If ((str2 = "kirghiz") OrElse (str2 = "kyrgyz")) Then
                    Return "kir"
                End If
                If (str2 = "kimbundu") Then
                    Return "kmb"
                End If
                If (str2 = "konkani") Then
                    Return "kok"
                End If
                If (str2 = "komi") Then
                    Return "kom"
                End If
                If (str2 = "kongo") Then
                    Return "kon"
                End If
                If (str2 = "korean") Then
                    Return "kor"
                End If
                If (str2 = "kosraean") Then
                    Return "kos"
                End If
                If (str2 = "kpelle") Then
                    Return "kpe"
                End If
                If (str2 = "karachay-balkar") Then
                    Return "krc"
                End If
                If (str2 = "karelian") Then
                    Return "krl"
                End If
                If (str2 = "kru languages") Then
                    Return "kro"
                End If
                If (str2 = "kurukh") Then
                    Return "kru"
                End If
                If ((str2 = "kuanyama") OrElse (str2 = "kwanyama")) Then
                    Return "kua"
                End If
                If (str2 = "kumyk") Then
                    Return "kum"
                End If
                If (str2 = "kurdish") Then
                    Return "kur"
                End If
                If (str2 = "kutenai") Then
                    Return "kut"
                End If
                If (str2 = "ladino") Then
                    Return "lad"
                End If
                If (str2 = "lahnda") Then
                    Return "lah"
                End If
                If (str2 = "lamba") Then
                    Return "lam"
                End If
                If (str2 = "lao") Then
                    Return "lao"
                End If
                If (str2 = "latin") Then
                    Return "lat"
                End If
                If (str2 = "latvian") Then
                    Return "lav"
                End If
                If (str2 = "lezghian") Then
                    Return "lez"
                End If
                If (((str2 = "limburgan") OrElse (str2 = "limburger")) OrElse (str2 = "limburgish")) Then
                    Return "lim"
                End If
                If (str2 = "lingala") Then
                    Return "lin"
                End If
                If (str2 = "lithuanian") Then
                    Return "lit"
                End If
                If (str2 = "mongo") Then
                    Return "lol"
                End If
                If (str2 = "lozi") Then
                    Return "loz"
                End If
                If ((str2 = "luxembourgish") OrElse (str2 = "letzeburgesch")) Then
                    Return "ltz"
                End If
                If (str2 = "luba-lulua") Then
                    Return "lua"
                End If
                If (str2 = "luba-katanga") Then
                    Return "lub"
                End If
                If (str2 = "ganda") Then
                    Return "lug"
                End If
                If (str2 = "luiseno") Then
                    Return "lui"
                End If
                If (str2 = "lunda") Then
                    Return "lun"
                End If
                If (str2 = "luo (kenya and tanzania)") Then
                    Return "luo"
                End If
                If (str2 = "lushai") Then
                    Return "lus"
                End If
                If (str2 = "macedonian") Then
                    Return "mac"
                End If
                If (str2 = "madurese") Then
                    Return "mad"
                End If
                If (str2 = "magahi") Then
                    Return "mag"
                End If
                If (str2 = "marshallese") Then
                    Return "mah"
                End If
                If (str2 = "maithili") Then
                    Return "mai"
                End If
                If (str2 = "makasar") Then
                    Return "mak"
                End If
                If (str2 = "malayalam") Then
                    Return "mal"
                End If
                If (str2 = "mandingo") Then
                    Return "man"
                End If
                If (str2 = "maori") Then
                    Return "mao"
                End If
                If (str2 = "austronesian (other)") Then
                    Return "map"
                End If
                If (str2 = "marathi") Then
                    Return "mar"
                End If
                If (str2 = "masai") Then
                    Return "mas"
                End If
                If (str2 = "malay") Then
                    Return "may"
                End If
                If (str2 = "moksha") Then
                    Return "mdf"
                End If
                If (str2 = "mandar") Then
                    Return "mdr"
                End If
                If (str2 = "mende") Then
                    Return "men"
                End If
                If (str2 = "irish") Then
                    Return "mga"
                End If
                If ((str2 = "mi'kmaq") OrElse (str2 = "micmac")) Then
                    Return "mic"
                End If
                If (str2 = "minangkabau") Then
                    Return "min"
                End If
                If (str2 = "uncoded languages") Then
                    Return "mis"
                End If
                If (str2 = "mon-khmer (other)") Then
                    Return "mkh"
                End If
                If (str2 = "malagasy") Then
                    Return "mlg"
                End If
                If (str2 = "maltese") Then
                    Return "mlt"
                End If
                If (str2 = "manchu") Then
                    Return "mnc"
                End If
                If (str2 = "manipuri") Then
                    Return "mni"
                End If
                If (str2 = "manobo languages") Then
                    Return "mno"
                End If
                If (str2 = "mohawk") Then
                    Return "moh"
                End If
                If (str2 = "mongolian") Then
                    Return "mon"
                End If
                If (str2 = "mossi") Then
                    Return "mos"
                End If
                If (str2 = "multiple languages") Then
                    Return "mul"
                End If
                If (str2 = "munda languages") Then
                    Return "mun"
                End If
                If (str2 = "creek") Then
                    Return "mus"
                End If
                If (str2 = "mirandese") Then
                    Return "mwl"
                End If
                If (str2 = "marwari") Then
                    Return "mwr"
                End If
                If (str2 = "mayan languages") Then
                    Return "myn"
                End If
                If (str2 = "erzya") Then
                    Return "myv"
                End If
                If (str2 = "nahuatl languages") Then
                    Return "nah"
                End If
                If (str2 = "north american indian") Then
                    Return "nai"
                End If
                If (str2 = "neapolitan") Then
                    Return "nap"
                End If
                If (str2 = "nauru") Then
                    Return "nau"
                End If
                If ((str2 = "navajo") OrElse (str2 = "navaho")) Then
                    Return "nav"
                End If
                If (str2 = "ndebele") Then
                    Return "nbl"
                End If
                If (str2 = "ndebele") Then
                    Return "nde"
                End If
                If (str2 = "ndonga") Then
                    Return "ndo"
                End If
                If (((str2 = "low german") OrElse (str2 = "low saxon")) OrElse (str2 = "german")) Then
                    Return "nds"
                End If
                If (str2 = "nepali") Then
                    Return "nep"
                End If
                If ((str2 = "nepal bhasa") OrElse (str2 = "newari")) Then
                    Return "new"
                End If
                If (str2 = "nias") Then
                    Return "nia"
                End If
                If (str2 = "niger-kordofanian (other)") Then
                    Return "nic"
                End If
                If (str2 = "niuean") Then
                    Return "niu"
                End If
                If ((str2 = "norwegian nynorsk") OrElse (str2 = "nynorsk")) Then
                    Return "nno"
                End If
                If (str2 = "bokm" & ChrW(229) & "l") Then
                    Return "nob"
                End If
                If (str2 = "nogai") Then
                    Return "nog"
                End If
                If (str2 = "norse") Then
                    Return "non"
                End If
                If (str2 = "norwegian") Then
                    Return "nor"
                End If
                If (str2 = "n'ko") Then
                    Return "nqo"
                End If
                If (((str2 = "pedi") OrElse (str2 = "sepedi")) OrElse (str2 = "northern sotho")) Then
                    Return "nso"
                End If
                If (str2 = "nubian languages") Then
                    Return "nub"
                End If
                If (((str2 = "classical newari") OrElse (str2 = "old newari")) OrElse (str2 = "classical nepal bhasa")) Then
                    Return "nwc"
                End If
                If (((str2 = "chichewa") OrElse (str2 = "chewa")) OrElse (str2 = "nyanja")) Then
                    Return "nya"
                End If
                If (str2 = "nyamwezi") Then
                    Return "nym"
                End If
                If (str2 = "nyankole") Then
                    Return "nyn"
                End If
                If (str2 = "nyoro") Then
                    Return "nyo"
                End If
                If (str2 = "nzima") Then
                    Return "nzi"
                End If
                If ((str2 = "occitan (post 1500)") OrElse (str2 = "proven" & ChrW(231) & "al")) Then
                    Return "oci"
                End If
                If (str2 = "ojibwa") Then
                    Return "oji"
                End If
                If (str2 = "oriya") Then
                    Return "ori"
                End If
                If (str2 = "oromo") Then
                    Return "orm"
                End If
                If (str2 = "osage") Then
                    Return "osa"
                End If
                If ((str2 = "ossetian") OrElse (str2 = "ossetic")) Then
                    Return "oss"
                End If
                If (str2 = "turkish") Then
                    Return "ota"
                End If
                If (str2 = "otomian languages") Then
                    Return "oto"
                End If
                If (str2 = "papuan (other)") Then
                    Return "paa"
                End If
                If (str2 = "pangasinan") Then
                    Return "pag"
                End If
                If (str2 = "pahlavi") Then
                    Return "pal"
                End If
                If ((str2 = "pampanga") OrElse (str2 = "kapampangan")) Then
                    Return "pam"
                End If
                If ((str2 = "panjabi") OrElse (str2 = "punjabi")) Then
                    Return "pan"
                End If
                If (str2 = "papiamento") Then
                    Return "pap"
                End If
                If (str2 = "palauan") Then
                    Return "pau"
                End If
                If (str2 = "persian") Then
                    Return "peo"
                End If
                If (str2 = "persian") Then
                    Return "per"
                End If
                If (str2 = "philippine (other)") Then
                    Return "phi"
                End If
                If (str2 = "phoenician") Then
                    Return "phn"
                End If
                If (str2 = "pali") Then
                    Return "pli"
                End If
                If (str2 = "polish") Then
                    Return "pol"
                End If
                If (str2 = "pohnpeian") Then
                    Return "pon"
                End If
                If (str2 = "portuguese") Then
                    Return "por"
                End If
                If (str2 = "prakrit languages") Then
                    Return "pra"
                End If
                If (str2 = "proven" & ChrW(231) & "al") Then
                    Return "pro"
                End If
                If ((str2 = "pushto") OrElse (str2 = "pashto")) Then
                    Return "pus"
                End If
                If (str2 = "reserved for local use") Then
                    Return "qaa-qtz"
                End If
                If (str2 = "quechua") Then
                    Return "que"
                End If
                If (str2 = "rajasthani") Then
                    Return "raj"
                End If
                If (str2 = "rapanui") Then
                    Return "rap"
                End If
                If ((str2 = "rarotongan") OrElse (str2 = "cook islands maori")) Then
                    Return "rar"
                End If
                If (str2 = "romance (other)") Then
                    Return "roa"
                End If
                If (str2 = "romansh") Then
                    Return "roh"
                End If
                If (str2 = "romany") Then
                    Return "rom"
                End If
                If (((str2 = "romanian") OrElse (str2 = "moldavian")) OrElse (str2 = "moldovan")) Then
                    Return "rum"
                End If
                If (str2 = "rundi") Then
                    Return "run"
                End If
                If (((str2 = "aromanian") OrElse (str2 = "arumanian")) OrElse (str2 = "macedo-romanian")) Then
                    Return "rup"
                End If
                If (str2 = "russian") Then
                    Return "rus"
                End If
                If (str2 = "sandawe") Then
                    Return "sad"
                End If
                If (str2 = "sango") Then
                    Return "sag"
                End If
                If (str2 = "yakut") Then
                    Return "sah"
                End If
                If (str2 = "south american indian (other)") Then
                    Return "sai"
                End If
                If (str2 = "salishan languages") Then
                    Return "sal"
                End If
                If (str2 = "samaritan aramaic") Then
                    Return "sam"
                End If
                If (str2 = "sanskrit") Then
                    Return "san"
                End If
                If (str2 = "sasak") Then
                    Return "sas"
                End If
                If (str2 = "santali") Then
                    Return "sat"
                End If
                If (str2 = "sicilian") Then
                    Return "scn"
                End If
                If (str2 = "scots") Then
                    Return "sco"
                End If
                If (str2 = "selkup") Then
                    Return "sel"
                End If
                If (str2 = "semitic (other)") Then
                    Return "sem"
                End If
                If (str2 = "irish") Then
                    Return "sga"
                End If
                If (str2 = "sign languages") Then
                    Return "sgn"
                End If
                If (str2 = "shan") Then
                    Return "shn"
                End If
                If (str2 = "sidamo") Then
                    Return "sid"
                End If
                If ((str2 = "sinhala") OrElse (str2 = "sinhalese")) Then
                    Return "sin"
                End If
                If (str2 = "siouan languages") Then
                    Return "sio"
                End If
                If (str2 = "sino-tibetan (other)") Then
                    Return "sit"
                End If
                If (str2 = "slavic (other)") Then
                    Return "sla"
                End If
                If (str2 = "slovak") Then
                    Return "slo"
                End If
                If (str2 = "slovenian") Then
                    Return "slv"
                End If
                If (str2 = "southern sami") Then
                    Return "sma"
                End If
                If (str2 = "northern sami") Then
                    Return "sme"
                End If
                If (str2 = "sami languages (other)") Then
                    Return "smi"
                End If
                If (str2 = "lule sami") Then
                    Return "smj"
                End If
                If (str2 = "inari sami") Then
                    Return "smn"
                End If
                If (str2 = "samoan") Then
                    Return "smo"
                End If
                If (str2 = "skolt sami") Then
                    Return "sms"
                End If
                If (str2 = "shona") Then
                    Return "sna"
                End If
                If (str2 = "sindhi") Then
                    Return "snd"
                End If
                If (str2 = "soninke") Then
                    Return "snk"
                End If
                If (str2 = "sogdian") Then
                    Return "sog"
                End If
                If (str2 = "somali") Then
                    Return "som"
                End If
                If (str2 = "songhai languages") Then
                    Return "son"
                End If
                If (str2 = "sotho") Then
                    Return "sot"
                End If
                If ((str2 = "spanish") OrElse (str2 = "castilian")) Then
                    Return "spa"
                End If
                If (str2 = "sardinian") Then
                    Return "srd"
                End If
                If (str2 = "sranan tongo") Then
                    Return "srn"
                End If
                If (str2 = "serbian") Then
                    Return "srp"
                End If
                If (str2 = "serer") Then
                    Return "srr"
                End If
                If (str2 = "nilo-saharan (other)") Then
                    Return "ssa"
                End If
                If (str2 = "swati") Then
                    Return "ssw"
                End If
                If (str2 = "sukuma") Then
                    Return "suk"
                End If
                If (str2 = "sundanese") Then
                    Return "sun"
                End If
                If (str2 = "susu") Then
                    Return "sus"
                End If
                If (str2 = "sumerian") Then
                    Return "sux"
                End If
                If (str2 = "swahili") Then
                    Return "swa"
                End If
                If (str2 = "swedish") Then
                    Return "swe"
                End If
                If (str2 = "classical syriac") Then
                    Return "syc"
                End If
                If (str2 = "syriac") Then
                    Return "syr"
                End If
                If (str2 = "tahitian") Then
                    Return "tah"
                End If
                If (str2 = "tai (other)") Then
                    Return "tai"
                End If
                If (str2 = "tamil") Then
                    Return "tam"
                End If
                If (str2 = "tatar") Then
                    Return "tat"
                End If
                If (str2 = "telugu") Then
                    Return "tel"
                End If
                If (str2 = "timne") Then
                    Return "tem"
                End If
                If (str2 = "tereno") Then
                    Return "ter"
                End If
                If (str2 = "tetum") Then
                    Return "tet"
                End If
                If (str2 = "tajik") Then
                    Return "tgk"
                End If
                If (str2 = "tagalog") Then
                    Return "tgl"
                End If
                If (str2 = "thai") Then
                    Return "tha"
                End If
                If (str2 = "tibetan") Then
                    Return "tib"
                End If
                If (str2 = "tigre") Then
                    Return "tig"
                End If
                If (str2 = "tigrinya") Then
                    Return "tir"
                End If
                If (str2 = "tiv") Then
                    Return "tiv"
                End If
                If (str2 = "tokelau") Then
                    Return "tkl"
                End If
                If ((str2 = "klingon") OrElse (str2 = "tlhingan-hol")) Then
                    Return "tlh"
                End If
                If (str2 = "tlingit") Then
                    Return "tli"
                End If
                If (str2 = "tamashek") Then
                    Return "tmh"
                End If
                If (str2 = "tonga (nyasa)") Then
                    Return "tog"
                End If
                If (str2 = "tonga (tonga islands)") Then
                    Return "ton"
                End If
                If (str2 = "tok pisin") Then
                    Return "tpi"
                End If
                If (str2 = "tsimshian") Then
                    Return "tsi"
                End If
                If (str2 = "tswana") Then
                    Return "tsn"
                End If
                If (str2 = "tsonga") Then
                    Return "tso"
                End If
                If (str2 = "turkmen") Then
                    Return "tuk"
                End If
                If (str2 = "tumbuka") Then
                    Return "tum"
                End If
                If (str2 = "tupi languages") Then
                    Return "tup"
                End If
                If (str2 = "turkish") Then
                    Return "tur"
                End If
                If (str2 = "altaic (other)") Then
                    Return "tut"
                End If
                If (str2 = "tuvalu") Then
                    Return "tvl"
                End If
                If (str2 = "twi") Then
                    Return "twi"
                End If
                If (str2 = "tuvinian") Then
                    Return "tyv"
                End If
                If (str2 = "udmurt") Then
                    Return "udm"
                End If
                If (str2 = "ugaritic") Then
                    Return "uga"
                End If
                If ((str2 = "uighur") OrElse (str2 = "uyghur")) Then
                    Return "uig"
                End If
                If (str2 = "ukrainian") Then
                    Return "ukr"
                End If
                If (str2 = "umbundu") Then
                    Return "umb"
                End If
                If (str2 = "undetermined") Then
                    Return "und"
                End If
                If (str2 = "urdu") Then
                    Return "urd"
                End If
                If (str2 = "uzbek") Then
                    Return "uzb"
                End If
                If (str2 = "vai") Then
                    Return "vai"
                End If
                If (str2 = "venda") Then
                    Return "ven"
                End If
                If (str2 = "vietnamese") Then
                    Return "vie"
                End If
                If (str2 = "volap" & ChrW(252) & "k") Then
                    Return "vol"
                End If
                If (str2 = "votic") Then
                    Return "vot"
                End If
                If (str2 = "wakashan languages") Then
                    Return "wak"
                End If
                If (str2 = "walamo") Then
                    Return "wal"
                End If
                If (str2 = "waray") Then
                    Return "war"
                End If
                If (str2 = "washo") Then
                    Return "was"
                End If
                If (str2 = "welsh") Then
                    Return "wel"
                End If
                If (str2 = "sorbian languages") Then
                    Return "wen"
                End If
                If (str2 = "walloon") Then
                    Return "wln"
                End If
                If (str2 = "wolof") Then
                    Return "wol"
                End If
                If ((str2 = "kalmyk") OrElse (str2 = "oirat")) Then
                    Return "xal"
                End If
                If (str2 = "xhosa") Then
                    Return "xho"
                End If
                If (str2 = "yao") Then
                    Return "yao"
                End If
                If (str2 = "yapese") Then
                    Return "yap"
                End If
                If (str2 = "yiddish") Then
                    Return "yid"
                End If
                If (str2 = "yoruba") Then
                    Return "yor"
                End If
                If (str2 = "yupik languages") Then
                    Return "ypk"
                End If
                If (str2 = "zapotec") Then
                    Return "zap"
                End If
                If (((str2 = "blissymbols") OrElse (str2 = "blissymbolics")) OrElse (str2 = "bliss")) Then
                    Return "zbl"
                End If
                If (str2 = "zenaga") Then
                    Return "zen"
                End If
                If ((str2 = "zhuang") OrElse (str2 = "chuang")) Then
                    Return "zha"
                End If
                If (str2 = "zande languages") Then
                    Return "znd"
                End If
                If (str2 = "zulu") Then
                    Return "zul"
                End If
                If (str2 = "zuni") Then
                    Return "zun"
                End If
                If ((str2 = "no linguistic content") OrElse (str2 = "not applicable")) Then
                    Return "zxx"
                End If
                If ((((str2 <> "zaza") AndAlso (str2 <> "dimili")) AndAlso ((str2 <> "dimli") AndAlso (str2 <> "kirdki"))) AndAlso ((str2 <> "kirmanjki") AndAlso (str2 <> "zazaki"))) Then
                    Return str
                End If
                str = "zza"
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            Finally
                Monitor.Exit(RuntimeHelpers.GetObjectValue(objectValue))
            End Try
            Return str
        End Function

        Public Shared Function getlastfolder(ByVal fullpath As String) As String
            Dim str As String
            Try 
                If ((fullpath.IndexOf("/") <> -1) And (fullpath.IndexOf("\") = -1)) Then
                    fullpath = fullpath.Replace("/", "\")
                End If
                fullpath = fullpath.Replace(Path.GetFileName(fullpath), "")
                Dim str2 As String = ""
                Dim array As String() = fullpath.Split(New Char() { "\"c })
                Dim i As Integer = Information.UBound(array, 1)
                Do While (i >= 0)
                    If ((array(i).ToLower.IndexOf("video_ts") = -1) And (array(i) <> "")) Then
                        str2 = array(i)
                        Exit Do
                    End If
                    i = (i + -1)
                Loop
                str = str2
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                str = ""
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Public Shared Function getmedialist(ByVal pathandfilename As String) As Object
            Dim obj2 As Object
            Try 
                Dim path As String = pathandfilename
                Dim list As New List(Of String)
                If File.Exists(path) Then
                    list.Add(path)
                End If
                path = path.ToLower
                If (path.IndexOf("cd1") <> -1) Then
                    path = path.Replace("cd1", "cd2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd2", "cd3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd3", "cd4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd4", "cd5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("cd_1") <> -1) Then
                    path = path.Replace("cd_1", "cd_2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd_2", "cd_3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd_3", "cd_4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd_4", "cd_5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("cd 1") <> -1) Then
                    path = path.Replace("cd 1", "cd 2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd 2", "cd 3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd 3", "cd 4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd 4", "cd 5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("cd.1") <> -1) Then
                    path = path.Replace("cd.1", "cd.2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd.2", "cd.3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd.3", "cd.4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("cd.4", "cd.5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("dvd1") <> -1) Then
                    path = path.Replace("dvd1", "dvd2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd2", "dvd3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd3", "dvd4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd4", "dvd5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("dvd_1") <> -1) Then
                    path = path.Replace("dvd_1", "dvd_2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd_2", "dvd_3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd_3", "dvd_4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd_4", "dvd_5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("dvd 1") <> -1) Then
                    path = path.Replace("dvd 1", "dvd 2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd 2", "dvd 3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd 3", "dvd 4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd 4", "dvd 5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("dvd.1") <> -1) Then
                    path = path.Replace("dvd.1", "dvd.2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd.2", "dvd.3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd.3", "dvd.4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("dvd.4", "dvd.5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("part1") <> -1) Then
                    path = path.Replace("part1", "part2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part2", "part3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part3", "part4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part4", "part5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("part_1") <> -1) Then
                    path = path.Replace("part_1", "part_2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part_2", "part_3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part_3", "part_4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part_4", "part_5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("part 1") <> -1) Then
                    path = path.Replace("part 1", "part 2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part 2", "part 3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part 3", "part 4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part 4", "part 5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("part.1") <> -1) Then
                    path = path.Replace("part.1", "part.2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part.2", "part.3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part.3", "part.4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("part.4", "part.5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("disk1") <> -1) Then
                    path = path.Replace("disk1", "disk2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk2", "disk3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk3", "disk4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk4", "disk5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("disk_1") <> -1) Then
                    path = path.Replace("disk_1", "disk_2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk_2", "disk_3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk_3", "disk_4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk_4", "disk_5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("disk 1") <> -1) Then
                    path = path.Replace("disk 1", "disk 2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk 2", "disk 3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk 3", "disk 4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk 4", "disk 5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("disk.1") <> -1) Then
                    path = path.Replace("disk.1", "disk.2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk.2", "disk.3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk.3", "disk.4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("disk.4", "disk.5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("pt1") <> -1) Then
                    path = path.Replace("pt1", "pt2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt2", "pt3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt3", "pt4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt4", "pt5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("pt_1") <> -1) Then
                    path = path.Replace("pt_1", "pt_2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt_2", "pt_3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt_3", "pt_4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt_4", "pt_5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("pt 1") <> -1) Then
                    path = path.Replace("pt 1", "pt 2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt 2", "pt 3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt 3", "pt 4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt 4", "pt 5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                If (path.IndexOf("pt.1") <> -1) Then
                    path = path.Replace("pt.1", "pt.2")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt.2", "pt.3")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt.3", "pt.4")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                    path = path.Replace("pt.4", "pt.5")
                    If File.Exists(path) Then
                        list.Add(path)
                    End If
                End If
                obj2 = list
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Private Shared Function getmovietags(ByVal [text] As String, ByVal movie As combolist, ByVal counter As Integer, ByVal Optional thumbpath As String = "") As Object
            If (([text].IndexOf("<<smallimage>>") And -((thumbpath <> "") > False)) <= 0) Then
                goto Label_01C6
            End If
            Dim str As String = Module1.GetCRC32(movie.fullpathandfilename)
            If File.Exists(Path.Combine(Module1.applicationpath, ("settings\postercache\" & str & ".jpg"))) Then
                Try 
                    File.Copy(Path.Combine(Module1.applicationpath, ("settings\postercache\" & str & ".jpg")), Path.Combine(thumbpath, (str & ".jpg")))
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    ProjectData.ClearProjectError
                End Try
                Try 
                    [text] = [text].Replace("<<smallimage>>", ("images/" & str & ".jpg"))
                Catch exception2 As Exception
                    ProjectData.SetProjectError(exception2)
                    Interaction.MsgBox(exception2.ToString, MsgBoxStyle.OkOnly, Nothing)
                    ProjectData.ClearProjectError
                End Try
                Dim str2 As String = [text]
            Else
                If File.Exists(Module1.getposterpath(movie.fullpathandfilename)) Then
                    Try 
                        Dim original As New Bitmap(Module1.getposterpath(movie.fullpathandfilename))
                        Dim bmp As New Bitmap(original)
                        original.Dispose
                        Module1.resizeimage(bmp, 150, 200).Save(Path.Combine(thumbpath, (str & ".jpg")), ImageFormat.Jpeg)
                        goto Label_01A9
                    Catch exception3 As Exception
                        ProjectData.SetProjectError(exception3)
                        ProjectData.ClearProjectError
                        goto Label_01A9
                    End Try
                End If
                Try 
                    Dim bitmap3 As New Bitmap(Module1.defaultposter)
                    Dim bitmap4 As New Bitmap(bitmap3)
                    bitmap3.Dispose
                    Module1.resizeimage(bitmap4, 150, 200).Save(Path.Combine(thumbpath, (str & ".jpg")), ImageFormat.Jpeg)
                Catch exception4 As Exception
                    ProjectData.SetProjectError(exception4)
                    ProjectData.ClearProjectError
                End Try
            End If
        Label_01A9:
            [text] = [text].Replace("<<smallimage>>", ("images/" & str & ".jpg"))
        Label_01C6:
            If (((([text].IndexOf("<<fullplot>>") <> -1) Or ([text].IndexOf("<<director>>") <> -1)) Or ([text].IndexOf("<<writer>>") <> -1)) Or ([text].IndexOf("<<moviegenre>>") <> -1)) Then
                Dim fullmoviedetails As fullmoviedetails = DirectCast(Module1.loadfullmovienfo(movie.fullpathandfilename), fullmoviedetails)
                If ([text].IndexOf("<<fullplot>>") <> -1) Then
                    [text] = [text].Replace("<<fullplot>>", fullmoviedetails.fullmoviebody.plot)
                End If
                If ([text].IndexOf("<<director>>") <> -1) Then
                    [text] = [text].Replace("<<director>>", fullmoviedetails.fullmoviebody.director)
                End If
                If ([text].IndexOf("<<writer>>") <> -1) Then
                    [text] = [text].Replace("<<writer>>", fullmoviedetails.fullmoviebody.credits)
                End If
                If ([text].IndexOf("<<moviegenre>>") <> -1) Then
                    [text] = [text].Replace("<<moviegenre>>", fullmoviedetails.fullmoviebody.genre)
                End If
            End If
            Try 
                [text] = [text].Replace("<<moviecount>>", Module1.basictvlist.Count.ToString)
            Catch exception5 As Exception
                ProjectData.SetProjectError(exception5)
                [text] = [text].Replace("<<moviecount>>", "0000")
                ProjectData.ClearProjectError
            End Try
            Try 
                [text] = [text].Replace("<<counter>>", counter.ToString)
            Catch exception6 As Exception
                ProjectData.SetProjectError(exception6)
                ProjectData.ClearProjectError
            End Try
            If (movie.title <> Nothing) Then
                [text] = [text].Replace("<<title>>", movie.title)
            Else
                [text] = [text].Replace("<<title>>", "")
            End If
            If ((movie.title <> Nothing) And (movie.year <> Nothing)) Then
                [text] = [text].Replace("<<movietitleandyear>>", (movie.title & " (" & movie.year.ToString & ")"))
            ElseIf ((movie.title <> Nothing) And (movie.year = Nothing)) Then
                [text] = [text].Replace("<<movietitleandyear>>", (movie.title & " (0000)"))
            ElseIf ((movie.title = Nothing) And (movie.year <> Nothing)) Then
                [text] = [text].Replace("<<movietitleandyear>>", (" (" & movie.year.ToString & ")"))
            End If
            If (movie.runtime <> Nothing) Then
                [text] = [text].Replace("<<runtime>>", movie.runtime)
            Else
                [text] = [text].Replace("<<runtime>>", "")
            End If
            If (movie.rating <> Nothing) Then
                [text] = [text].Replace("<<rating>>", movie.rating)
            Else
                [text] = [text].Replace("<<rating>>", "")
            End If
            If (movie.outline <> Nothing) Then
                [text] = [text].Replace("<<outline>>", movie.outline)
            Else
                [text] = [text].Replace("<<outline>>", "")
            End If
            If (movie.id <> Nothing) Then
                [text] = [text].Replace("<<imdb_url>>", (Module1.userprefs.imdbmirror & "title/" & movie.id & "/"))
            Else
                [text] = [text].Replace("<<imdb_url>>", Module1.userprefs.imdbmirror)
            End If
            If (movie.fullpathandfilename <> Nothing) Then
                [text] = [text].Replace("<<fullpathandfilename>>", movie.fullpathandfilename)
            Else
                [text] = [text].Replace("<<fullpathandfilename>>", "")
            End If
            If (movie.year <> Nothing) Then
                [text] = [text].Replace("<<movieyear>>", movie.year)
                Return [text]
            End If
            [text] = [text].Replace("<<movieyear>>", "")
            Return [text]
        End Function

        Public Shared Function getposterpath(ByVal fullpath As String) As String
            Dim str As String
            Try 
                Dim sPath As String = ""
                sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & ".tbn")
                Dim newValue As String = Module1.getstackname(Path.GetFileName(fullpath), fullpath.Replace(Path.GetFileName(fullpath), ""))
                newValue = (fullpath.Replace(Path.GetFileName(fullpath), newValue) & ".tbn")
                If ((newValue <> "na") And File.Exists(newValue)) Then
                    sPath = newValue
                Else
                    sPath = (sPath.Replace(Path.GetFileName(sPath), "") & "movie.tbn")
                    If Not File.Exists(sPath) Then
                        sPath = ""
                    End If
                End If
                If ((sPath = "") AndAlso (fullpath.IndexOf("movie.nfo") <> -1)) Then
                    sPath = fullpath.Replace("movie.nfo", "movie.tbn")
                End If
                If (sPath = "") Then
                    If Module1.userprefs.posternotstacked Then
                        sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & ".tbn")
                    Else
                        sPath = (Module1.getstackname(Path.GetFileName(fullpath), sPath.Replace(Path.GetFileName(fullpath), "")) & ".tbn")
                        If (sPath = "na.tbn") Then
                            sPath = (fullpath.Substring(0, (fullpath.Length - 4)) & ".tbn")
                        Else
                            sPath = fullpath.Replace(Path.GetFileName(fullpath), sPath)
                        End If
                    End If
                    If Module1.userprefs.basicsavemode Then
                        sPath = sPath.Replace(Path.GetFileName(fullpath), "movie.tbn")
                    End If
                End If
                If ((sPath = "na") AndAlso File.Exists(fullpath.Replace(Path.GetFileName(fullpath), "folder.jpg"))) Then
                    sPath = fullpath.Replace(Path.GetFileName(fullpath), "folder.jpg")
                End If
                str = sPath
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                str = ""
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Public Shared Function getstackname(ByVal filenames As String, ByVal filepath As String) As String
            Dim str As String
            Try 
                Dim flag As Boolean = False
                Dim flag2 As Boolean = False
                Dim sPath As String = filenames
                If (Path.GetExtension(sPath).ToLower = ".rar") Then
                    flag2 = True
                End If
                filenames = filenames.Replace(Path.GetExtension(filenames), "")
                sPath = sPath.ToLower
                Dim str6 As String = "na"
                If (sPath.IndexOf("cd1") <> -1) Then
                    flag = True
                    str6 = "cd1"
                End If
                If (sPath.IndexOf("cd.1") <> -1) Then
                    flag = True
                    str6 = "cd.1"
                End If
                If (sPath.IndexOf("cd_1") <> -1) Then
                    flag = True
                    str6 = "cd_1"
                End If
                If (sPath.IndexOf("cd 1") <> -1) Then
                    flag = True
                    str6 = "cd 1"
                End If
                If (sPath.IndexOf("cd-1") <> -1) Then
                    flag = True
                    str6 = "cd-1"
                End If
                If (sPath.IndexOf("dvd1") <> -1) Then
                    flag = True
                    str6 = "dvd1"
                End If
                If (sPath.IndexOf("dvd.1") <> -1) Then
                    flag = True
                    str6 = "dvd.1"
                End If
                If (sPath.IndexOf("dvd_1") <> -1) Then
                    flag = True
                    str6 = "dvd_1"
                End If
                If (sPath.IndexOf("dvd 1") <> -1) Then
                    flag = True
                    str6 = "dvd 1"
                End If
                If (sPath.IndexOf("dvd-1") <> -1) Then
                    flag = True
                    str6 = "dvd-1"
                End If
                If (sPath.IndexOf("part1") <> -1) Then
                    flag = True
                    str6 = "part1"
                End If
                If (sPath.IndexOf("part.1") <> -1) Then
                    flag = True
                    str6 = "part.1"
                End If
                If (sPath.IndexOf("part_1") <> -1) Then
                    flag = True
                    str6 = "part_1"
                End If
                If (sPath.IndexOf("part-1") <> -1) Then
                    flag = True
                    str6 = "part-1"
                End If
                If (sPath.IndexOf("part 1") <> -1) Then
                    flag = True
                    str6 = "part 1"
                End If
                If (sPath.IndexOf("disk1") <> -1) Then
                    flag = True
                    str6 = "disk1"
                End If
                If (sPath.IndexOf("disk.1") <> -1) Then
                    flag = True
                    str6 = "disk.1"
                End If
                If (sPath.IndexOf("disk_1") <> -1) Then
                    flag = True
                    str6 = "disk_1"
                End If
                If (sPath.IndexOf("disk 1") <> -1) Then
                    flag = True
                    str6 = "disk 1"
                End If
                If (sPath.IndexOf("disk-1") <> -1) Then
                    flag = True
                    str6 = "disk-1"
                End If
                If (sPath.IndexOf("pt 1") <> -1) Then
                    flag = True
                    str6 = "pt 1"
                End If
                If (sPath.IndexOf("pt-1") <> -1) Then
                    flag = True
                    str6 = "pt-1"
                End If
                If (sPath.IndexOf("pt1") <> -1) Then
                    flag = True
                    str6 = "pt1"
                End If
                If (sPath.IndexOf("pt_1") <> -1) Then
                    flag = True
                    str6 = "pt_1"
                End If
                If (sPath.IndexOf("pt.1") <> -1) Then
                    flag = True
                    str6 = "pt.1"
                End If
                Dim strArray As String() = New String(&H18 - 1) {}
                strArray(1) = ".avi"
                strArray(2) = ".xvid"
                strArray(3) = ".divx"
                strArray(4) = ".img"
                strArray(5) = ".mpg"
                strArray(6) = ".mpeg"
                strArray(7) = ".mov"
                strArray(8) = ".rm"
                strArray(9) = ".3gp"
                strArray(10) = ".m4v"
                strArray(11) = ".wmv"
                strArray(12) = ".asf"
                strArray(13) = ".mp4"
                strArray(14) = ".mkv"
                strArray(15) = ".nrg"
                strArray(&H10) = ".iso"
                strArray(&H11) = ".rmvb"
                strArray(&H12) = ".ogm"
                strArray(&H13) = ".bin"
                strArray(20) = ".ts"
                strArray(&H15) = ".vob"
                strArray(&H16) = ".m2ts"
                strArray(&H17) = ".rar"
                strArray(&H17) = ".strm"
                Dim num As Integer = &H17
                Dim extension As String = Path.GetExtension(sPath)
                Dim str4 As String = sPath.Replace(Path.GetExtension(sPath), "")
                If (str4.Substring((str4.Length - 1)).ToLower = "a") Then
                    Dim num5 As Integer = num
                    Dim i As Integer = 1
                    Do While (i <= num5)
                        If File.Exists((filepath & sPath.Substring(0, (sPath.Length - (1 + extension.Length))) & "b" & strArray(i))) Then
                            str6 = "a"
                            flag = True
                            Exit Do
                        End If
                        i += 1
                    Loop
                End If
                If flag Then
                    Dim flag4 As Boolean = False
                    If (str6 <> "na") Then
                        sPath = sPath.Replace(Path.GetExtension(sPath), "")
                        Dim strArray2 As String() = New String() {".", " ", "-", "_"}
                        Dim index As Integer = 0
                        Do
                            Dim str8 As String = (strArray2(index) & str6)
                            If (sPath.IndexOf(str8) <> -1) Then
                                If (str8.IndexOf(".") <> -1) Then
                                    str8 = str8.Replace(".", "\.")
                                End If
                                sPath = Regex.Replace(filenames, str8, "", RegexOptions.IgnoreCase)
                                flag4 = True
                                Exit Do
                            End If
                            index += 1
                        Loop While (index <= 3)
                    End If
                    If ((str6 = "a") And Not flag4) Then
                        Dim str9 As String = sPath
                        If (str9.Substring((str9.Length - 1), 1) = "a") Then
                            str9 = str9.Substring(0, (str9.Length - 1))
                            Dim num6 As Integer = num
                            Dim j As Integer = 1
                            Do While (j <= num6)
                                If File.Exists((filepath & str9 & "b" & strArray(j))) Then
                                    sPath = filenames.Substring(0, (sPath.Length - 1))
                                    Exit Do
                                End If
                                j += 1
                            Loop
                        End If
                    End If
                End If
                If flag2 Then
                    If (Path.GetExtension(sPath).ToLower = ".rar") Then
                        sPath = sPath.Replace(Path.GetExtension(sPath), "")
                    End If
                    If (sPath.ToLower.IndexOf(".part1") <> -1) Then
                        sPath = sPath.Replace(".part1", "")
                        flag = True
                    End If
                    If (sPath.ToLower.IndexOf(".part01") <> -1) Then
                        sPath = sPath.Replace(".part01", "")
                        flag = True
                    End If
                    If (sPath.ToLower.IndexOf(".part001") <> -1) Then
                        sPath = sPath.Replace(".part001", "")
                        flag = True
                    End If
                    If (sPath.ToLower.IndexOf(".part0001") <> -1) Then
                        sPath = sPath.Replace(".part0001", "")
                        flag = True
                    End If
                End If
                If Not flag Then
                    sPath = "na"
                End If
                Dim objArray As Object() = New Object() {" ", "_", "-", "."}
                sPath = Strings.RTrim(sPath)
                If (sPath.IndexOf("_") = (sPath.Length - 1)) Then
                    sPath = sPath.Substring(0, (sPath.Length - 1))
                End If
                If (sPath.IndexOf("-") = (sPath.Length - 1)) Then
                    sPath = sPath.Substring(0, (sPath.Length - 1))
                End If
                If (sPath.IndexOf(".") = (sPath.Length - 1)) Then
                    sPath = sPath.Substring(0, (sPath.Length - 1))
                End If
                str = Strings.RTrim(sPath)
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                str = "na"
                ProjectData.ClearProjectError
            End Try
            Return str
        End Function

        Private Shared Sub htmloutput()
            If ((Module1.fullhtmlstring <> Nothing) AndAlso (Module1.fullhtmlstring <> "")) Then
                Dim text As String = ""
                Dim htmlfileoutput As String = Module1.htmlfileoutput
                Dim counter As Integer = 0
                htmlfileoutput = Module1.htmlfileoutput
                If ((Module1.fullhtmlstring.IndexOf("<<css>>") <> -1) And (Module1.fullhtmlstring.IndexOf("<</css>>") <> -1)) Then
                    [text] = Module1.fullhtmlstring.Substring((Module1.fullhtmlstring.IndexOf("<<css>>") + 9), ((Module1.fullhtmlstring.IndexOf("<</css>>") - Module1.fullhtmlstring.IndexOf("<<css>>")) - 9))
                    If (([text].IndexOf("<filename>") <> -1) And ([text].IndexOf("</filename>") <> -1)) Then
                        Dim newValue As String = [text].Substring(([text].IndexOf("<filename>") + 10), (([text].IndexOf("</filename>") - [text].IndexOf("<filename>")) - 10))
                        Dim str2 As String = htmlfileoutput.Replace(Path.GetFileName(htmlfileoutput), newValue)
                        Dim str As String = [text].Substring(([text].IndexOf("</filename>") + 13), (([text].Length - [text].IndexOf("</filename>")) - 13))
                        Try 
                            Dim writer As New StreamWriter(str2, False, Encoding.UTF8)
                            writer.Write(str)
                            writer.Close
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            Dim exception As Exception = exception1
                            ProjectData.ClearProjectError
                        End Try
                    End If
                End If
                [text] = Module1.fullhtmlstring.Substring((Module1.fullhtmlstring.IndexOf("<<header>>") + 12), ((Module1.fullhtmlstring.IndexOf("<</header>>") - Module1.fullhtmlstring.IndexOf("<<header>>")) - 12))
                Dim left As String = ("<html><head>" & [text] & "</head>")
                [text] = Module1.fullhtmlstring.Substring((Module1.fullhtmlstring.ToLower.IndexOf("<<body>>") + 8), (Module1.fullhtmlstring.ToLower.IndexOf("<</body>>") - (Module1.fullhtmlstring.ToLower.IndexOf("<<body>>") + 8)))
                left = (left & "<body>")
                Dim sPath As String = ""
                If ([text].IndexOf("<<smallimage>>") <> -1) Then
                    sPath = (htmlfileoutput.Replace(Path.GetFileName(htmlfileoutput), "") & "images\")
                    Dim info As New DirectoryInfo(sPath)
                    If Not info.Exists Then
                        Directory.CreateDirectory(sPath)
                    End If
                End If
                Dim flag As Boolean = False
                Dim combolist As combolist
                For Each combolist In Module1.fullmovielist
                    Dim num2 As Integer = (Module1.fullmovielist.Count - (counter + 1))
                    Console.SetCursorPosition(0, Console.CursorTop)
                    Console.Write((num2.ToString & " Movies Remaining         "))
                    Try 
                        left = Conversions.ToString(Operators.ConcatenateObject(left, Module1.getmovietags([text], combolist, counter, sPath)))
                    Catch exception3 As Exception
                        ProjectData.SetProjectError(exception3)
                        ProjectData.ClearProjectError
                    End Try
                    counter += 1
                Next
                If Not flag Then
                    left = (left & "</body>" & "</html>")
                    Try 
                        Dim writer2 As New StreamWriter(htmlfileoutput, False, Encoding.UTF8)
                        writer2.Write(left)
                        writer2.Close
                    Catch exception4 As Exception
                        ProjectData.SetProjectError(exception4)
                        Dim exception2 As Exception = exception4
                        ProjectData.ClearProjectError
                    End Try
                End If
            End If
        End Sub

        Public Shared Function imdbthumb(ByVal posterimdbid As String) As Object
            Dim obj2 As Object
            Dim str As String = "na"
            Try 
                Dim str4 As String = posterimdbid
                Dim requestUriString As String = ("http://www.imdb.com/title/" & str4)
                Dim strArray As String() = New String(&H7D1  - 1) {}
                Dim index As Integer = 0
                Dim request As WebRequest = WebRequest.Create(requestUriString)
                Dim proxy As New WebProxy("myproxy", 80) With { _
                    .BypassProxyOnLocal = True _
                }
                Dim reader As New StreamReader(request.GetResponse.GetResponseStream)
                Dim str3 As String = ""
                index = 0
                Do While (Not str3 Is Nothing)
                    index += 1
                    strArray(index) = reader.ReadLine
                Loop
                index -= 1
                Dim num3 As Integer = index
                Dim i As Integer = 1
                Do While (i <= num3)
                    If (strArray(i).IndexOf("<div class=""photo"">") <> -1) Then
                        str = strArray((i + 1))
                        str = (str.Substring(str.IndexOf("http"), (str.IndexOf("._V1") - str.IndexOf("http"))) & "._V1._SX1500_SY1000_.jpg")
                    End If
                    i += 1
                Loop
                If ((str.IndexOf("http") = -1) Or (str.IndexOf(".jpg") = -1)) Then
                    str = "na"
                End If
                obj2 = str
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                str = "na"
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Public Shared Function impathumb(ByVal title As String, ByVal year As String) As Object
            Dim obj2 As Object
            Try 
                year = year.Replace("<year>", "")
                year = year.Replace("</year>", "")
                year = year.Replace("    ", "")
                Dim flag As Boolean = False
                Dim str6 As String = "na"
                Dim str4 As String = title
                Dim requestUriString As String = "http://www.google.com/custom?hl=en&client=pub-6811780361519631&cof=FORID%3A1%3BGL%3A1%3BLBGC%3A000000%3BBGC%3A%23000000%3BT%3A%23cccccc%3BLC%3A%2333cc33%3BVLC%3A%2333ff33%3BGALT%3A%2333CC33%3BGFNT%3A%23ffffff%3BGIMP%3A%23ffffff%3B&domains=www.impawards.com&ie=ISO-8859-1&oe=ISO-8859-1&q="
                str4 = str4.ToLower.Replace(" ", "+").Replace("&", "%26").Replace(ChrW(192), "%c0").Replace(ChrW(193), "%c1").Replace(ChrW(194), "%c2").Replace(ChrW(195), "%c3").Replace(ChrW(196), "%c4").Replace(ChrW(197), "%c5").Replace(ChrW(198), "%c6").Replace(ChrW(199), "%c7").Replace(ChrW(200), "%c8").Replace(ChrW(201), "%c9").Replace(ChrW(202), "%ca").Replace(ChrW(203), "%cb").Replace(ChrW(204), "%cc").Replace(ChrW(205), "%cd").Replace(ChrW(206), "%ce").Replace(ChrW(207), "%cf").Replace(ChrW(208), "%d0").Replace(ChrW(209), "%d1").Replace(ChrW(210), "%d2").Replace(ChrW(211), "%d3").Replace(ChrW(212), "%d4").Replace(ChrW(213), "%d5").Replace(ChrW(214), "%d6").Replace(ChrW(216), "%d8").Replace(ChrW(217), "%d9").Replace(ChrW(218), "%da").Replace(ChrW(219), "%db").Replace(ChrW(220), "%dc").Replace(ChrW(221), "%dd").Replace(ChrW(222), "%de").Replace(ChrW(223), "%df").Replace(ChrW(224), "%e0").Replace(ChrW(225), "%e1").Replace(ChrW(226), "%e2").Replace(ChrW(227), "%e3").Replace(ChrW(228), "%e4").Replace(ChrW(229), "%e5").Replace(ChrW(230), "%e6").Replace(ChrW(231), "%e7").Replace(ChrW(232), "%e8").Replace(ChrW(233), "%e9").Replace(ChrW(234), "%ea").Replace(ChrW(235), "%eb").Replace(ChrW(236), "%ec").Replace(ChrW(237), "%ed").Replace(ChrW(238), "%ee").Replace(ChrW(239), "%ef").Replace(ChrW(240), "%f0").Replace(ChrW(241), "%f1").Replace(ChrW(242), "%f2").Replace(ChrW(243), "%f3").Replace(ChrW(244), "%f4").Replace(ChrW(245), "%f5").Replace(ChrW(246), "%f6").Replace(ChrW(247), "%f7").Replace(ChrW(248), "%f8").Replace(ChrW(249), "%f9").Replace(ChrW(250), "%fa").Replace(ChrW(251), "%fb").Replace(ChrW(252), "%fc").Replace(ChrW(253), "%fd").Replace(ChrW(254), "%fe").Replace(ChrW(255), "%ff").Replace(" ", "+").Replace("&", "%26")
                requestUriString = ((requestUriString & str4 & "+" & year) & "&sitesearch=www.impawards.com")
                Dim strArray As String() = New String(&H7D1  - 1) {}
                Dim index As Integer = 0
                Dim request As WebRequest = WebRequest.Create(requestUriString)
                Dim proxy As New WebProxy("myproxy", 80) With { _
                    .BypassProxyOnLocal = True _
                }
                Dim reader As New StreamReader(request.GetResponse.GetResponseStream)
                Dim str2 As String = ""
                index = 0
                Do While (Not str2 Is Nothing)
                    index += 1
                    strArray(index) = reader.ReadLine
                Loop
                index -= 1
                Dim flag2 As Boolean = False
                Dim num7 As Integer = index
                Dim i As Integer = 1
                Do While (i <= num7)
                    If (strArray(i).IndexOf("http://www.impawards.com/") <> -1) Then
                        Dim startIndex As Integer = strArray(i).IndexOf("http://www.impawards.com/")
                        strArray(i) = strArray(i).Substring(startIndex, (strArray(i).Length - startIndex))
                        requestUriString = strArray(i).Substring(0, (strArray(i).IndexOf("html") + 4))
                        Dim str5 As String = requestUriString
                        str5 = str5.Replace("http://", "")
                        If ((str5.LastIndexOf("/") - 5) = str5.IndexOf("/")) Then
                            flag = True
                        Else
                            requestUriString = "http://www.google.com/custom?hl=en&client=pub-6811780361519631&cof=FORID%3A1%3BGL%3A1%3BLBGC%3A000000%3BBGC%3A%23000000%3BT%3A%23cccccc%3BLC%3A%2333cc33%3BVLC%3A%2333ff33%3BGALT%3A%2333CC33%3BGFNT%3A%23ffffff%3BGIMP%3A%23ffffff%3B&domains=www.impawards.com&ie=ISO-8859-1&oe=ISO-8859-1&q="
                            requestUriString = (requestUriString & str4 & "&sitesearch=www.impawards.com")
                            strArray = New String(&H7D1  - 1) {}
                            index = 0
                            Dim request3 As WebRequest = WebRequest.Create(requestUriString)
                            Dim proxy3 As New WebProxy("myproxy", 80) With { _
                                .BypassProxyOnLocal = True _
                            }
                            Dim reader3 As New StreamReader(request3.GetResponse.GetResponseStream)
                            Dim str7 As String = ""
                            index = 0
                            Do While (Not str7 Is Nothing)
                                index += 1
                                strArray(index) = reader3.ReadLine
                            Loop
                            index -= 1
                            flag2 = False
                            Dim num8 As Integer = index
                            Dim k As Integer = 1
                            Do While (k <= num8)
                                If (strArray(i).IndexOf("http://www.impawards.com/") <> -1) Then
                                    startIndex = strArray(i).IndexOf("http://www.impawards.com/")
                                    strArray(i) = strArray(i).Substring(startIndex, (strArray(i).Length - startIndex))
                                    requestUriString = strArray(i).Substring(0, (strArray(i).IndexOf("html") + 4))
                                    str5 = requestUriString
                                    str5 = str5.Replace("http://", "")
                                    If ((str5.LastIndexOf("/") - 5) = str5.IndexOf("/")) Then
                                        flag = True
                                    Else
                                        flag = False
                                    End If
                                End If
                                k += 1
                            Loop
                        End If
                        Exit Do
                    End If
                    If (strArray(i).IndexOf("xlg.html") <> -1) Then
                        flag2 = True
                    End If
                    i += 1
                Loop
                strArray = New String(&H7D1  - 1) {}
                index = 0
                Dim request2 As WebRequest = WebRequest.Create(requestUriString)
                Dim proxy2 As New WebProxy("myproxy", 80) With { _
                    .BypassProxyOnLocal = True _
                }
                Dim reader2 As New StreamReader(request2.GetResponse.GetResponseStream)
                Dim str3 As String = ""
                Do While (Not str3 Is Nothing)
                    index += 1
                    strArray(index) = reader2.ReadLine
                Loop
                index -= 1
                Dim num9 As Integer = index
                Dim j As Integer = 1
                Do While (j <= num9)
                    If (strArray(j).IndexOf("xlg.html") <> -1) Then
                        flag2 = True
                        Exit Do
                    End If
                    j += 1
                Loop
                If flag Then
                    Dim str8 As String = requestUriString.Substring(0, (requestUriString.LastIndexOf("/") + 1))
                    Dim str9 As String = requestUriString.Substring((requestUriString.LastIndexOf("/") + 1), ((requestUriString.Length - requestUriString.LastIndexOf("/")) - 1))
                    requestUriString = (str8 & "posters/" & str9)
                    If Not flag2 Then
                        str6 = requestUriString.Replace(".html", ".jpg")
                    ElseIf flag2 Then
                        str6 = requestUriString.Replace(".html", "_xlg.jpg")
                    End If
                Else
                    str6 = "na"
                End If
                If (str6.IndexOf("art_machine.jpg") <> -1) Then
                    str6 = "na"
                End If
                obj2 = str6
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Private Shared Sub Listmoviefiles(ByVal lst As String, ByVal pattern As String, ByVal dir_info As DirectoryInfo)
            Dim searchPattern As String = pattern
            Try 
                Dim files As FileInfo() = dir_info.GetFiles(searchPattern)
                Dim flag As Boolean = False
                Dim info As FileInfo
                For Each info In files
                    Dim str2 As String
                    Dim newmovie As newmovie
                    Dim str7 As String
                    Dim flag3 As Boolean = False
                    Dim flag2 As Boolean = False
                    flag = False
                    Dim sPath As String = info.FullName.Replace(Path.GetExtension(info.FullName), ".nfo")
                    newmovie.mediapathandfilename = info.FullName
                    newmovie.nfopathandfilename = sPath
                    If File.Exists(sPath.Replace(Path.GetFileName(sPath), "movie.nfo")) Then
                        flag3 = True
                    End If
                    Dim str5 As String = sPath.Replace(Path.GetFileName(sPath), (Module1.getstackname(Path.GetFileName(info.FullName), info.FullName) & ".nfo"))
                    If File.Exists(str5) Then
                        Dim flag4 As Boolean = False
                        Try 
                            Dim reader As StreamReader = File.OpenText(str5)
                            Do
                                str2 = reader.ReadLine
                                If (str2 = Nothing) Then
                                    Exit Do
                                End If
                                If (str2.IndexOf("<movie>") <> -1) Then
                                    flag4 = True
                                    Exit Do
                                End If
                            Loop While (str2.IndexOf("</movie>") = -1)
                            reader.Close
                        Catch exception1 As Exception
                            ProjectData.SetProjectError(exception1)
                            ProjectData.ClearProjectError
                        End Try
                        If flag4 Then
                            flag3 = True
                        End If
                    End If
                    If (searchPattern = "*.vob") Then
                        flag2 = File.Exists(sPath.Replace(Path.GetFileName(sPath), "VIDEO_TS.IFO"))
                    End If
                    If Not flag2 Then
                        If File.Exists(sPath) Then
                            Dim flag5 As Boolean = False
                            Try
                                Dim reader2 As StreamReader = File.OpenText(sPath)
                                Do
                                    str2 = reader2.ReadLine
                                    If (str2 = Nothing) Then
                                        Exit Do
                                    End If
                                    If (str2.IndexOf("<movie>") <> -1) Then
                                        flag5 = True
                                        Exit Do
                                    End If
                                Loop While (str2.IndexOf("</movie>") = -1)
                                reader2.Close()
                            Catch exception2 As Exception
                                ProjectData.SetProjectError(exception2)
                                ProjectData.ClearProjectError()
                            End Try
                            If Not flag5 Then
                                flag = True
                                str7 = info.FullName
                            End If
                        Else
                            flag = True
                            str7 = info.FullName
                        End If
                        If (flag AndAlso (str7 <> Nothing)) Then
                            Dim flag6 As Boolean = False
                            Dim str10 As String = Path.GetFileName(str7).ToLower
                            If (str10.IndexOf("cd2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd.2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd.3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd.4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd.5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd_9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd4") <> -1) Then
                                flag6 = True
                            End If
                            If ((str10.IndexOf("dvd5") <> -1) AndAlso File.Exists(str7.Replace("dvd5", "dvd1"))) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd8") <> -1) Then
                                flag6 = True
                            End If
                            If ((str10.IndexOf("dvd9") <> -1) AndAlso File.Exists(str7.Replace("dvd9", "dvd1"))) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd.2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd.3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd.4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd.5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd_9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part.2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part.3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part.4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part.5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part_9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk.9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk_9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd 9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("cd-9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd 9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("dvd-9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part-9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("part 9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk 9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("disk-9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt 9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt-9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt_9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.2") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.3") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.4") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.5") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.6") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.7") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.8") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("pt.9") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("-trailer") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf(".trailer") <> -1) Then
                                flag6 = True
                            End If
                            If (str10.IndexOf("_trailer") <> -1) Then
                                flag6 = True
                            End If
                            If ((str10.IndexOf("sample") <> -1) And (str10.IndexOf("people") = -1)) Then
                                flag6 = True
                            End If
                            Dim extension As String = Path.GetExtension(str10)
                            Dim str11 As String = str10.Replace(extension, "")
                            If ((((((((str11.Substring((str11.Length - 1)) = "b") Or (str11.Substring((str11.Length - 1)) = "c")) Or (str11.Substring((str11.Length - 1)) = "d")) Or (str11.Substring((str11.Length - 1)) = "e")) Or (str11.Substring((str11.Length - 1)) = "B")) Or (str11.Substring((str11.Length - 1)) = "C")) Or (str11.Substring((str11.Length - 1)) = "D")) Or (str11.Substring((str11.Length - 1)) = "E")) Then
                                str11 = (newmovie.nfopathandfilename.Substring(0, (newmovie.nfopathandfilename.Length - (1 + extension.Length))) & "a" & extension)
                                If File.Exists(str11) Then
                                    flag6 = True
                                End If
                            End If
                            If flag6 Then
                                flag3 = True
                            End If
                        End If
                    End If
                    Dim fullName As String = info.FullName
                    If (((Path.GetExtension(fullName).ToLower = ".rar") AndAlso File.Exists(fullName)) AndAlso Not File.Exists(sPath)) Then
                        Dim pathName As String = fullName
                        Dim num As Integer = (Convert.ToInt32(Module1.userprefs.rarsize) * &H100000)
                        Dim num2 As Integer = CInt(Microsoft.VisualBasic.FileSystem.FileLen(pathName))
                        If (num2 > num) Then
                            Dim match As Match = Regex.Match(pathName, "\.part[0-9][0-9]?[0-9]?[0-9]?.rar")
                            If match.Success Then
                                pathName = match.Value
                                If ((((pathName.ToLower.IndexOf(".part1.rar") <> -1) Or (pathName.ToLower.IndexOf(".part01.rar") <> -1)) Or (pathName.ToLower.IndexOf(".part001.rar") <> -1)) Or (pathName.ToLower.IndexOf(".part0001.rar") <> -1)) Then
                                    Dim flag8 As Boolean = False
                                    pathName = sPath.Replace(".nfo", ".rar")
                                    If (pathName.ToLower.IndexOf(".part1.rar") <> -1) Then
                                        pathName = pathName.Replace(".part1.rar", ".nfo")
                                        If File.Exists(pathName) Then
                                            flag8 = True
                                            sPath = pathName
                                        Else
                                            flag8 = False
                                            sPath = pathName
                                        End If
                                    End If
                                    If (pathName.ToLower.IndexOf(".part01.rar") <> -1) Then
                                        pathName = pathName.Replace(".part01.rar", ".nfo")
                                        If File.Exists(pathName) Then
                                            flag8 = True
                                            sPath = pathName
                                        Else
                                            flag8 = False
                                            sPath = pathName
                                        End If
                                    End If
                                    If (pathName.ToLower.IndexOf(".part001.rar") <> -1) Then
                                        pathName = pathName.Replace(".part001.rar", ".nfo")
                                        If File.Exists(pathName) Then
                                            sPath = pathName
                                            flag8 = True
                                        Else
                                            flag8 = False
                                            sPath = pathName
                                        End If
                                    End If
                                    If (pathName.ToLower.IndexOf(".part0001.rar") <> -1) Then
                                        pathName = pathName.Replace(".part0001.rar", ".nfo")
                                        If File.Exists(pathName) Then
                                            sPath = pathName
                                            flag8 = True
                                        Else
                                            flag8 = False
                                            sPath = pathName
                                        End If
                                    End If
                                    If flag8 Then
                                        Dim flag9 As Boolean = False
                                        Try
                                            Dim reader3 As StreamReader = File.OpenText(sPath)
                                            Do
                                                str2 = reader3.ReadLine
                                                If (str2 = Nothing) Then
                                                    Exit Do
                                                End If
                                                If (str2.IndexOf("<movie>") <> -1) Then
                                                    flag9 = True
                                                    Exit Do
                                                End If
                                            Loop While (str2.IndexOf("</movie>") = -1)
                                            reader3.Close()
                                        Catch exception3 As Exception
                                            ProjectData.SetProjectError(exception3)
                                            ProjectData.ClearProjectError()
                                        End Try
                                        If flag9 Then
                                            flag3 = True
                                        Else
                                            str7 = sPath
                                        End If
                                    Else
                                        str7 = sPath
                                    End If
                                Else
                                    flag3 = True
                                End If
                            End If
                        Else
                            flag3 = True
                        End If
                    End If
                    If flag3 Then
                        flag3 = False
                        str7 = Nothing
                        newmovie.mediapathandfilename = Nothing
                        newmovie.nfopath = Nothing
                        newmovie.nfopathandfilename = Nothing
                        newmovie.title = Nothing
                    ElseIf (str7 <> Nothing) Then
                        newmovie.nfopathandfilename = str7
                        Dim oldValue As String = Path.GetExtension(str7)
                        Dim fileName As String = Path.GetFileName(str7)
                        newmovie.nfopath = str7.Replace(fileName, "")
                        newmovie.title = fileName.Replace(oldValue, "")
                        If ((oldValue <> ".IFO") And (oldValue <> "ttt")) Then
                            newmovie.nfopathandfilename = newmovie.nfopathandfilename.Replace(oldValue, ".nfo")
                        End If
                        If (oldValue.ToLower = ".ifo") Then
                            newmovie.mediapathandfilename = str7
                            newmovie.nfopathandfilename = newmovie.mediapathandfilename.Replace(oldValue, ".nfo")
                            If Not File.Exists(newmovie.nfopathandfilename) Then
                                Dim strArray As String()
                                If (newmovie.nfopathandfilename.IndexOf("\") <> -1) Then
                                    strArray = newmovie.nfopathandfilename.Split(New Char() { "\"c })
                                ElseIf (newmovie.nfopathandfilename.IndexOf("/") <> -1) Then
                                    strArray = newmovie.nfopathandfilename.Split(New Char() { "/"c })
                                End If
                                Dim upperBound As Integer = strArray.GetUpperBound(0)
                                newmovie.title = Nothing
                                Dim i As Integer = upperBound
                                Do While (i >= 0)
                                    Dim str17 As String = strArray(i)
                                    strArray(i) = strArray(i).ToLower
                                    If (strArray(i).IndexOf("video_ts") = -1) Then
                                        newmovie.title = str17
                                    End If
                                    If (newmovie.title <> Nothing) Then
                                        Exit Do
                                    End If
                                    i = (i + -1)
                                Loop
                            Else
                                newmovie.nfopathandfilename = Nothing
                                newmovie.title = Nothing
                            End If
                        End If
                        If ((Module1.userprefs.usefoldernames And (newmovie.title <> Nothing)) AndAlso (newmovie.nfopathandfilename.ToLower.IndexOf("video_ts") = -1)) Then
                            newmovie.title = newmovie.nfopath.Substring(0, (newmovie.nfopath.Length - 1))
                            newmovie.title = newmovie.title.Substring((newmovie.title.LastIndexOf("\") + 1), ((newmovie.title.Length - newmovie.title.LastIndexOf("\")) - 1))
                            newmovie.title = Module1.getlastfolder(newmovie.nfopathandfilename)
                        End If
                        Dim flag10 As Boolean = False
                        Dim newmovie2 As newmovie
                        For Each newmovie2 In Module1.newmovielist
                            If (newmovie2.nfopathandfilename = newmovie.nfopathandfilename) Then
                                flag10 = True
                                Exit For
                            End If
                        Next
                        If Not flag10 Then
                            Module1.newmovielist.Add(newmovie)
                        Else
                            flag10 = False
                        End If
                    End If
                Next
                files = Nothing
            Catch exception4 As Exception
                ProjectData.SetProjectError(exception4)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Shared Sub loadactorcache()
            Dim enumerator As IEnumerator
            Module1.actordb.Clear
            Dim actorcache As String = Module1.workingprofile.actorcache
            Dim document As New XmlDocument
            document.Load(actorcache)
            Dim current As XmlNode = Nothing
            Try 
                enumerator = document.Item("actor_cache").GetEnumerator
                Do While enumerator.MoveNext
                    current = DirectCast(enumerator.Current, XmlNode)
                    If (current.Name = "actor") Then
                        Dim enumerator2 As IEnumerator
                        Dim item As New actordatabase With { _
                            .actorname = "", _
                            .movieid = "" _
                        }
                        Dim node2 As XmlNode = Nothing
                        Try 
                            enumerator2 = current.ChildNodes.GetEnumerator
                            Do While enumerator2.MoveNext
                                node2 = DirectCast(enumerator2.Current, XmlNode)
                                Select Case node2.Name
                                    Case "name"
                                        item.actorname = node2.InnerText
                                        Exit Select
                                    Case "id"
                                        item.movieid = node2.InnerText
                                        Exit Select
                                End Select
                                If ((item.actorname <> "") And (item.movieid <> "")) Then
                                    Module1.actordb.Add(item)
                                End If
                            Loop
                            Continue Do
                        Finally
                            If TypeOf enumerator2 Is IDisposable Then
                                TryCast(enumerator2,IDisposable).Dispose
                            End If
                        End Try
                    End If
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator,IDisposable).Dispose
                End If
            End Try
        End Sub

        Public Shared Sub loadconfig()
            Module1.userprefs.moviesets.Clear
            Module1.userprefs.moviesets.Add("None")
            Module1.moviefolders.Clear
            Module1.tvfolders.Clear
            Module1.tvrootfolders.Clear
            Module1.userprefs.tableview.Clear
            Dim config As String = Module1.workingprofile.config
            If File.Exists(Module1.workingprofile.config) Then
                Dim enumerator As IEnumerator
                Dim document As New XmlDocument
                Try 
                    document.Load(Module1.workingprofile.config)
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Interaction.MsgBox("Error : pr24", MsgBoxStyle.OkOnly, Nothing)
                    ProjectData.ClearProjectError
                End Try
                Dim current As XmlNode = Nothing
                Try 
                    enumerator = document.Item("xbmc_media_companion_config_v1.0").GetEnumerator
                    Do While enumerator.MoveNext
                        current = DirectCast(enumerator.Current, XmlNode)
                        Dim name As String = current.Name
                        Select Case name
                            Case "moviesets"
                                Dim enumerator2 As IEnumerator
                                Dim node2 As XmlNode = Nothing
                                Try 
                                    enumerator2 = current.ChildNodes.GetEnumerator
                                    Do While enumerator2.MoveNext
                                        node2 = DirectCast(enumerator2.Current, XmlNode)
                                        If (node2.Name = "set") Then
                                            Module1.userprefs.moviesets.Add(node2.InnerText)
                                        End If
                                    Loop
                                    Continue Do
                                Finally
                                    If TypeOf enumerator2 Is IDisposable Then
                                        TryCast(enumerator2,IDisposable).Dispose
                                    End If
                                End Try
                                Exit Select
                            Case "table"
                                Dim enumerator3 As IEnumerator
                                Dim node3 As XmlNode = Nothing
                                Try 
                                    enumerator3 = current.ChildNodes.GetEnumerator
                                    Do While enumerator3.MoveNext
                                        node3 = DirectCast(enumerator3.Current, XmlNode)
                                        Select Case node3.Name
                                            Case "tab"
                                                Module1.userprefs.tableview.Add(node3.InnerText)
                                                Exit Select
                                            Case "sort"
                                                Module1.userprefs.tablesortorder = node3.InnerText
                                                Exit Select
                                        End Select
                                    Loop
                                    Continue Do
                                Finally
                                    If TypeOf enumerator3 Is IDisposable Then
                                        TryCast(enumerator3,IDisposable).Dispose
                                    End If
                                End Try
                                Exit Select
                            Case "seasonall"
                                Module1.userprefs.seasonall = current.InnerText
                                Continue Do
                            Case "splitcontainer1"
                                Module1.userprefs.splt1 = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "splitcontainer2"
                                Module1.userprefs.splt2 = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "splitcontainer3"
                                Module1.userprefs.splt3 = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "splitcontainer4"
                                Module1.userprefs.splt4 = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "splitcontainer5"
                                Module1.userprefs.splt5 = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "maximised"
                                If (current.InnerText = "true") Then
                                    Module1.userprefs.maximised = True
                                Else
                                    Module1.userprefs.maximised = False
                                End If
                                Continue Do
                            Case "locx"
                                Module1.userprefs.locx = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "locy"
                                Module1.userprefs.locy = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "nfofolder"
                                Dim item As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.moviefolders.Add(item)
                                Continue Do
                            Case "offlinefolder"
                                Dim str3 As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.userprefs.offlinefolders.Add(str3)
                                Continue Do
                            Case "tvfolder"
                                Dim str4 As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.tvfolders.Add(str4)
                                Continue Do
                            Case "tvrootfolder"
                                Dim str5 As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.tvrootfolders.Add(str5)
                                Continue Do
                            Case "gettrailer"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.gettrailer = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.gettrailer = False
                                End If
                                Continue Do
                            Case "tvshowautoquick"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.tvshowautoquick = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.tvshowautoquick = False
                                End If
                                Continue Do
                            Case "keepfoldername"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.keepfoldername = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.keepfoldername = False
                                End If
                                Continue Do
                            Case "startupcache"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.startupcache = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.startupcache = False
                                End If
                                Continue Do
                            Case "ignoretrailers"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.ignoretrailers = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.ignoretrailers = False
                                End If
                                Continue Do
                            Case "ignoreactorthumbs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.ignoreactorthumbs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.ignoreactorthumbs = False
                                End If
                                Continue Do
                            Case "font"
                                If ((current.InnerXml <> Nothing) AndAlso (current.InnerXml <> "")) Then
                                    Module1.userprefs.font = current.InnerXml
                                End If
                                Continue Do
                            Case "maxactors"
                                Module1.userprefs.maxactors = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "maxmoviegenre"
                                Module1.userprefs.maxmoviegenre = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "enablehdtags"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.enablehdtags = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.enablehdtags = False
                                End If
                                Continue Do
                            Case "hdtvtags"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.enabletvhdtags = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.enabletvhdtags = False
                                End If
                                Continue Do
                            Case "renamenfofiles"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.renamenfofiles = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.renamenfofiles = False
                                End If
                                Continue Do
                            Case "checkinfofiles"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.checkinfofiles = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.checkinfofiles = False
                                End If
                                Continue Do
                            Case "disablelogfiles"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.disablelogfiles = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.disablelogfiles = False
                                End If
                                Continue Do
                            Case "fanartnotstacked"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.fanartnotstacked = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.fanartnotstacked = False
                                End If
                                Continue Do
                            Case "posternotstacked"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.posternotstacked = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.posternotstacked = False
                                End If
                                Continue Do
                            Case "downloadfanart"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.savefanart = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.savefanart = False
                                End If
                                Continue Do
                            Case "scrapemovieposters"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.scrapemovieposters = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.scrapemovieposters = False
                                End If
                                Continue Do
                            Case "usefanart"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.usefanart = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.usefanart = False
                                End If
                                Continue Do
                            Case "dontdisplayposter"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.dontdisplayposter = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.dontdisplayposter = False
                                End If
                                Continue Do
                            Case "rarsize"
                                Module1.userprefs.rarsize = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "actorsave"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.actorsave = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.actorsave = False
                                End If
                                Continue Do
                            Case "actorseasy"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.actorseasy = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.actorseasy = False
                                End If
                                Continue Do
                            Case "copytvactorthumbs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.copytvactorthumbs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.copytvactorthumbs = False
                                End If
                                Continue Do
                            Case "actorsavepath"
                                Dim str6 As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.userprefs.actorsavepath = str6
                                Continue Do
                            Case "actornetworkpath"
                                Dim str7 As String = Conversions.ToString(Module1.decxmlchars(current.InnerText))
                                Module1.userprefs.actornetworkpath = str7
                                Continue Do
                            Case "resizefanart"
                                Module1.userprefs.resizefanart = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "overwritethumbs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.overwritethumbs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.overwritethumbs = False
                                End If
                                Continue Do
                            Case "defaulttvthumb"
                                Module1.userprefs.defaulttvthumb = current.InnerXml
                                Continue Do
                            Case "imdbmirror"
                                Module1.userprefs.imdbmirror = current.InnerXml
                                Continue Do
                            Case "moviethumbpriority"
                                Module1.userprefs.moviethumbpriority = New String(4  - 1) {}
                                Module1.userprefs.moviethumbpriority = current.InnerXml.Split(New Char() { "|"c })
                                Continue Do
                            Case "certificatepriority"
                                Module1.userprefs.certificatepriority = New String(&H22  - 1) {}
                                Module1.userprefs.certificatepriority = current.InnerXml.Split(New Char() { "|"c })
                                Continue Do
                            Case "backgroundcolour"
                                Module1.userprefs.backgroundcolour = current.InnerXml
                                Continue Do
                            Case "forgroundcolour"
                                Module1.userprefs.forgroundcolour = current.InnerXml
                                Continue Do
                            Case "remembersize"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.remembersize = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.remembersize = False
                                End If
                                Continue Do
                            Case "formheight"
                                Module1.userprefs.formheight = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "formwidth"
                                Module1.userprefs.formwidth = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "videoplaybackmode"
                                Module1.userprefs.videoplaybackmode = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "usefoldernames"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.usefoldernames = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.usefoldernames = False
                                End If
                                Continue Do
                            Case "createfolderjpg"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.createfolderjpg = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.createfolderjpg = False
                                End If
                                Continue Do
                            Case "basicsavemode"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.basicsavemode = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.basicsavemode = False
                                End If
                                Continue Do
                            Case "startupdisplaynamemode"
                                Module1.userprefs.startupdisplaynamemode = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "namemode"
                                Module1.userprefs.namemode = current.InnerXml
                                Continue Do
                            Case "tvdblanguage"
                                Dim strArray As String() = current.InnerXml.Split(New Char() { "|"c })
                                Dim num As Integer = 0
                                Do
                                    If (strArray(0).Length = 2) Then
                                        Module1.userprefs.tvdblanguagecode = strArray(0)
                                        Module1.userprefs.tvdblanguage = strArray(1)
                                        Exit Do
                                    End If
                                    Module1.userprefs.tvdblanguagecode = strArray(1)
                                    Module1.userprefs.tvdblanguage = strArray(0)
                                    num += 1
                                Loop While (num <= 1)
                                Continue Do
                            Case "tvdbmode"
                                Module1.userprefs.sortorder = current.InnerXml
                                Continue Do
                            Case "tvdbactorscrape"
                                Module1.userprefs.tvdbactorscrape = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "usetransparency"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.usetransparency = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.usetransparency = False
                                End If
                                Continue Do
                            Case "transparencyvalue"
                                Module1.userprefs.transparencyvalue = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "downloadtvfanart"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.tvfanart = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.tvfanart = False
                                End If
                                Continue Do
                            Case "roundminutes"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.roundminutes = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.roundminutes = False
                                End If
                                Continue Do
                            Case "downloadtvposter"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.tvposter = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.tvposter = False
                                End If
                                Continue Do
                            Case "downloadtvseasonthumbs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.downloadtvseasonthumbs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.downloadtvseasonthumbs = False
                                End If
                                Continue Do
                            Case "maximumthumbs"
                                Module1.userprefs.maximumthumbs = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "startupmode"
                                Module1.userprefs.startupmode = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "hdtags"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.enablehdtags = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.enablehdtags = False
                                End If
                                Continue Do
                            Case "disablelogs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.disablelogfiles = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.disablelogfiles = False
                                End If
                                Continue Do
                            Case "disabletvlogs"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.disabletvlogs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.disabletvlogs = False
                                End If
                                Continue Do
                            Case "overwritethumb"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.overwritethumbs = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.overwritethumbs = False
                                End If
                                Continue Do
                            Case "folderjpg"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.createfolderjpg = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.createfolderjpg = False
                                End If
                                Continue Do
                            Case "savefanart"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.savefanart = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.savefanart = False
                                End If
                                Continue Do
                            Case "postertype"
                                Module1.userprefs.postertype = current.InnerXml
                                Continue Do
                            Case "tvactorscrape"
                                Module1.userprefs.tvdbactorscrape = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "videomode"
                                Module1.userprefs.videomode = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "selectedvideoplayer"
                                Module1.userprefs.selectedvideoplayer = current.InnerXml
                                Continue Do
                            Case "maximagecount"
                                Module1.userprefs.maximagecount = Convert.ToInt32(current.InnerXml)
                                Continue Do
                            Case "lastpath"
                                Module1.userprefs.lastpath = current.InnerXml
                                Continue Do
                            Case "moviescraper"
                                Module1.userprefs.moviescraper = Conversions.ToInteger(current.InnerXml)
                                Continue Do
                            Case "nfoposterscraper"
                                Module1.userprefs.nfoposterscraper = Conversions.ToInteger(current.InnerXml)
                                Continue Do
                            Case "alwaysuseimdbid"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.alwaysuseimdbid = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.alwaysuseimdbid = False
                                End If
                                Continue Do
                            Case "externalbrowser"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.externalbrowser = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.externalbrowser = False
                                End If
                                Continue Do
                            Case "tvrename"
                                Module1.userprefs.tvrename = Convert.ToInt32(current.InnerText)
                                Continue Do
                            Case "tvshowrefreshlog"
                                If (current.InnerXml = "true") Then
                                    Module1.userprefs.tvshowrefreshlog = True
                                ElseIf (current.InnerXml = "false") Then
                                    Module1.userprefs.tvshowrefreshlog = False
                                End If
                                Continue Do
                        End Select
                        If (name = "moviesortorder") Then
                            Module1.userprefs.moviesortorder = Convert.ToByte(current.InnerText)
                        Else
                            If (name = "moviedefaultlist") Then
                                Module1.userprefs.moviedefaultlist = Convert.ToByte(current.InnerText)
                                Continue Do
                            End If
                            If (name = "startuptab") Then
                                Module1.userprefs.startuptab = Convert.ToByte(current.InnerText)
                            End If
                        End If
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator,IDisposable).Dispose
                    End If
                End Try
            End If
        End Sub

        Public Shared Function loadfullmovienfo(ByVal sPath As String) As Object
            Dim obj2 As Object
            Try
                Dim innerText As String
                Dim enumerator As IEnumerator
                Dim fullmoviedetails As New fullmoviedetails
                If Not File.Exists(sPath) Then
                    Return obj2
                End If
                Dim document As New XmlDocument
                Try
                    document.Load(sPath)
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Dim str2 As String = (exception.Message.ToString & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10) & exception.StackTrace.ToString)
                    fullmoviedetails.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(Path.GetFileName(sPath), True))
                    fullmoviedetails.fullmoviebody.year = "0000"
                    fullmoviedetails.fullmoviebody.top250 = "0"
                    fullmoviedetails.fullmoviebody.playcount = "0"
                    fullmoviedetails.fullmoviebody.credits = ""
                    fullmoviedetails.fullmoviebody.director = ""
                    fullmoviedetails.fullmoviebody.filename = ""
                    fullmoviedetails.fullmoviebody.genre = ""
                    fullmoviedetails.fullmoviebody.imdbid = ""
                    fullmoviedetails.fullmoviebody.mpaa = ""
                    fullmoviedetails.fullmoviebody.outline = "This nfo file could not be loaded"
                    fullmoviedetails.fullmoviebody.playcount = "0"
                    fullmoviedetails.fullmoviebody.plot = str2
                    fullmoviedetails.fullmoviebody.premiered = ""
                    fullmoviedetails.fullmoviebody.rating = ""
                    fullmoviedetails.fullmoviebody.runtime = ""
                    fullmoviedetails.fullmoviebody.studio = ""
                    fullmoviedetails.fullmoviebody.tagline = "Rescrapeing the movie should fix the problem"
                    fullmoviedetails.fullmoviebody.trailer = ""
                    fullmoviedetails.fullmoviebody.votes = ""
                    fullmoviedetails.fullmoviebody.sortorder = ""
                    fullmoviedetails.fileinfo.createdate = "99999999"
                    obj2 = fullmoviedetails
                    ProjectData.ClearProjectError()
                    Return obj2
                End Try
                Dim current As XmlNode = Nothing
                Try
                    enumerator = document.Item("movie").GetEnumerator
                    Do While enumerator.MoveNext
                        current = DirectCast(enumerator.Current, XmlNode)
                        Select Case current.Name
                            Case "alternativetitle"
                                fullmoviedetails.alternativetitles.Add(current.InnerText)
                                Continue Do
                            Case "set"
                                fullmoviedetails.fullmoviebody.movieset = current.InnerText
                                Continue Do
                            Case "sortorder"
                                fullmoviedetails.fullmoviebody.sortorder = current.InnerText
                                Continue Do
                            Case "sorttitle"
                                fullmoviedetails.fullmoviebody.sortorder = current.InnerText
                                Continue Do
                            Case "votes"
                                fullmoviedetails.fullmoviebody.votes = current.InnerText
                                Continue Do
                            Case "outline"
                                fullmoviedetails.fullmoviebody.outline = current.InnerText
                                Continue Do
                            Case "plot"
                                fullmoviedetails.fullmoviebody.plot = current.InnerText
                                Continue Do
                            Case "tagline"
                                fullmoviedetails.fullmoviebody.tagline = current.InnerText
                                Continue Do
                            Case "runtime"
                                fullmoviedetails.fullmoviebody.runtime = current.InnerText
                                Continue Do
                            Case "mpaa"
                                fullmoviedetails.fullmoviebody.mpaa = current.InnerText
                                Continue Do
                            Case "credits"
                                fullmoviedetails.fullmoviebody.credits = current.InnerText
                                Continue Do
                            Case "director"
                                fullmoviedetails.fullmoviebody.director = current.InnerText
                                Continue Do
                            Case "thumb"
                                If (current.InnerText.IndexOf("&lt;thumbs&gt;") <> -1) Then
                                    innerText = current.InnerText
                                Else
                                    fullmoviedetails.listthumbs.Add(current.InnerText)
                                End If
                                Continue Do
                            Case "premiered"
                                fullmoviedetails.fullmoviebody.premiered = current.InnerText
                                Continue Do
                            Case "studio"
                                fullmoviedetails.fullmoviebody.studio = current.InnerText
                                Continue Do
                            Case "trailer"
                                fullmoviedetails.fullmoviebody.trailer = current.InnerText
                                Continue Do
                            Case "title"
                                fullmoviedetails.alternativetitles.Add(current.InnerText)
                                fullmoviedetails.fullmoviebody.title = current.InnerText
                                Continue Do
                            Case "year"
                                fullmoviedetails.fullmoviebody.year = current.InnerText
                                Continue Do
                            Case "genre"
                                fullmoviedetails.fullmoviebody.genre = current.InnerText
                                Continue Do
                            Case "id"
                                fullmoviedetails.fullmoviebody.imdbid = current.InnerText
                                Continue Do
                            Case "playcount"
                                fullmoviedetails.fullmoviebody.playcount = current.InnerText
                                Continue Do
                            Case "rating"
                                fullmoviedetails.fullmoviebody.rating = current.InnerText
                                If (fullmoviedetails.fullmoviebody.rating.IndexOf("/10") <> -1) Then
                                    fullmoviedetails.fullmoviebody.rating.Replace("/10", "")
                                End If
                                If (fullmoviedetails.fullmoviebody.rating.IndexOf(" ") <> -1) Then
                                    fullmoviedetails.fullmoviebody.rating.Replace(" ", "")
                                End If
                                Continue Do
                            Case "top250"
                                fullmoviedetails.fullmoviebody.top250 = current.InnerText
                                Continue Do
                            Case "createdate"
                                fullmoviedetails.fileinfo.createdate = current.InnerText
                                Continue Do
                            Case "actor"
                                Dim enumerator2 As IEnumerator
                                Dim item As New movieactors
                                Dim node2 As XmlNode = Nothing
                                Try
                                    enumerator2 = current.ChildNodes.GetEnumerator
                                    Do While enumerator2.MoveNext
                                        node2 = DirectCast(enumerator2.Current, XmlNode)
                                        Dim name As String = node2.Name
                                        If (name = "name") Then
                                            item.actorname = node2.InnerText
                                        Else
                                            If (name = "role") Then
                                                item.actorrole = node2.InnerText
                                                Continue Do
                                            End If
                                            If (name = "thumb") Then
                                                item.actorthumb = node2.InnerText
                                            End If
                                        End If
                                    Loop
                                Finally
                                    If TypeOf enumerator2 Is IDisposable Then
                                        TryCast(enumerator2, IDisposable).Dispose()
                                    End If
                                End Try
                                fullmoviedetails.listactors.Add(item)
                                Continue Do
                            Case "fileinfo"
                                Dim enumerator3 As IEnumerator
                                Try
                                    enumerator3 = current.ChildNodes.GetEnumerator
                                    Do While enumerator3.MoveNext
                                        Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator3.Current)
                                        If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, Nothing, "name", New Object(0 - 1) {}, Nothing, Nothing, Nothing), "streamdetails", False) Then
                                            Dim enumerator4 As IEnumerator
                                            Dim fullfiledetails As New fullfiledetails
                                            Dim node4 As XmlNode = Nothing
                                            Try
                                                enumerator4 = DirectCast(NewLateBinding.LateGet(objectValue, Nothing, "ChildNodes", New Object(0 - 1) {}, Nothing, Nothing, Nothing), IEnumerable).GetEnumerator
                                                Do While enumerator4.MoveNext
                                                    node4 = DirectCast(enumerator4.Current, XmlNode)
                                                    Select Case node4.Name
                                                        Case "video"
                                                            Dim enumerator5 As IEnumerator
                                                            Dim node5 As XmlNode = Nothing
                                                            Try
                                                                enumerator5 = node4.ChildNodes.GetEnumerator
                                                                Do While enumerator5.MoveNext
                                                                    node5 = DirectCast(enumerator5.Current, XmlNode)
                                                                    Dim str7 As String = node5.Name
                                                                    If (str7 = "width") Then
                                                                        fullfiledetails.filedetails_video.width = node5.InnerText
                                                                    Else
                                                                        If (str7 = "height") Then
                                                                            fullfiledetails.filedetails_video.height = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "aspect") Then
                                                                            fullfiledetails.filedetails_video.aspect = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "codec") Then
                                                                            fullfiledetails.filedetails_video.codec = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "formatinfo") Then
                                                                            fullfiledetails.filedetails_video.formatinfo = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "duration") Then
                                                                            fullfiledetails.filedetails_video.duration = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "bitrate") Then
                                                                            fullfiledetails.filedetails_video.bitrate = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "bitratemode") Then
                                                                            fullfiledetails.filedetails_video.bitratemode = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "bitratemax") Then
                                                                            fullfiledetails.filedetails_video.bitratemax = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "container") Then
                                                                            fullfiledetails.filedetails_video.container = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "codecid") Then
                                                                            fullfiledetails.filedetails_video.codecid = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "codecidinfo") Then
                                                                            fullfiledetails.filedetails_video.codecinfo = node5.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str7 = "scantype") Then
                                                                            fullfiledetails.filedetails_video.scantype = node5.InnerText
                                                                        End If
                                                                    End If
                                                                Loop
                                                                Continue Do
                                                            Finally
                                                                If TypeOf enumerator5 Is IDisposable Then
                                                                    TryCast(enumerator5, IDisposable).Dispose()
                                                                End If
                                                            End Try
                                                            Exit Select
                                                        Case "audio"
                                                            Dim enumerator6 As IEnumerator
                                                            Dim node6 As XmlNode = Nothing
                                                            Dim _audio As New medianfo_audio
                                                            Try
                                                                enumerator6 = node4.ChildNodes.GetEnumerator
                                                                Do While enumerator6.MoveNext
                                                                    node6 = DirectCast(enumerator6.Current, XmlNode)
                                                                    Dim str8 As String = node6.Name
                                                                    If (str8 = "language") Then
                                                                        _audio.language = node6.InnerText
                                                                    Else
                                                                        If (str8 = "codec") Then
                                                                            _audio.codec = node6.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str8 = "channels") Then
                                                                            _audio.channels = node6.InnerText
                                                                            Continue Do
                                                                        End If
                                                                        If (str8 = "bitrate") Then
                                                                            _audio.bitrate = node6.InnerText
                                                                        End If
                                                                    End If
                                                                Loop
                                                            Finally
                                                                If TypeOf enumerator6 Is IDisposable Then
                                                                    TryCast(enumerator6, IDisposable).Dispose()
                                                                End If
                                                            End Try
                                                            fullfiledetails.filedetails_audio.Add(_audio)
                                                            Continue Do
                                                        Case "subtitle"
                                                            Dim enumerator7 As IEnumerator
                                                            Dim node7 As XmlNode = Nothing
                                                            Try
                                                                enumerator7 = node4.ChildNodes.GetEnumerator
                                                                Do While enumerator7.MoveNext
                                                                    node7 = DirectCast(enumerator7.Current, XmlNode)
                                                                    If (node7.Name = "language") Then
                                                                        Dim _subtitles As New medianfo_subtitles With { _
                                                                            .language = node7.InnerText _
                                                                        }
                                                                        fullfiledetails.filedetails_subtitles.Add(_subtitles)
                                                                    End If
                                                                Loop
                                                                Continue Do
                                                            Finally
                                                                If TypeOf enumerator7 Is IDisposable Then
                                                                    TryCast(enumerator7, IDisposable).Dispose()
                                                                End If
                                                            End Try
                                                            Exit Select
                                                    End Select
                                                Loop
                                            Finally
                                                If TypeOf enumerator4 Is IDisposable Then
                                                    TryCast(enumerator4, IDisposable).Dispose()
                                                End If
                                            End Try
                                            fullmoviedetails.filedetails = fullfiledetails
                                        End If
                                    Loop
                                    Continue Do
                                Finally
                                    If TypeOf enumerator3 Is IDisposable Then
                                        TryCast(enumerator3, IDisposable).Dispose()
                                    End If
                                End Try
                                Exit Select
                        End Select
                    Loop
                Finally
                    If TypeOf enumerator Is IDisposable Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
                If (innerText <> Nothing) Then
                    Do While (innerText.ToLower.IndexOf("http") <> -1)
                        Try
                            Dim oldValue As String = innerText.ToLower.Substring(innerText.ToLower.LastIndexOf("http"), ((innerText.ToLower.LastIndexOf(".jpg") + 4) - innerText.ToLower.LastIndexOf("http")))
                            innerText = innerText.ToLower.Replace(oldValue, "")
                            fullmoviedetails.listthumbs.Add(oldValue)
                            Continue Do
                        Catch exception3 As Exception
                            ProjectData.SetProjectError(exception3)
                            ProjectData.ClearProjectError()
                            Exit Do
                        End Try
                    Loop
                End If
                fullmoviedetails.fileinfo.fullpathandfilename = sPath
                fullmoviedetails.fileinfo.filename = Path.GetFileName(sPath)
                fullmoviedetails.fileinfo.foldername = Module1.getlastfolder(sPath)
                fullmoviedetails.fileinfo.posterpath = Module1.getposterpath(sPath)
                fullmoviedetails.fileinfo.trailerpath = ""
                fullmoviedetails.fileinfo.fanartpath = Module1.getfanartpath(sPath)
                obj2 = fullmoviedetails
            Catch exception4 As Exception
                ProjectData.SetProjectError(exception4)
                Dim exception2 As Exception = exception4
                ProjectData.ClearProjectError()
            End Try
            Return obj2
        End Function

        Private Shared Sub loadmoviecache()
            Dim enumerator As IEnumerator
            Module1.fullmovielist.Clear
            Dim document As New XmlDocument
            Dim reader As New StreamReader(Module1.workingprofile.moviecache)
            Dim xml As String = reader.ReadToEnd
            reader.Close
            document.LoadXml(xml)
            Dim current As XmlNode = Nothing
            Try 
                enumerator = document.Item("movie_cache").GetEnumerator
                Do While enumerator.MoveNext
                    current = DirectCast(enumerator.Current, XmlNode)
                    If (current.Name = "movie") Then
                        Dim enumerator2 As IEnumerator
                        Dim item As New combolist
                        Dim node2 As XmlNode = Nothing
                        Try 
                            enumerator2 = current.ChildNodes.GetEnumerator
                            Do While enumerator2.MoveNext
                                node2 = DirectCast(enumerator2.Current, XmlNode)
                                Dim name As String = node2.Name
                                If (name = "missingdata1") Then
                                    item.missingdata1 = Convert.ToByte(node2.InnerText)
                                Else
                                    If (name = "set") Then
                                        item.movieset = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "sortorder") Then
                                        item.sortorder = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "filedate") Then
                                        item.filedate = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "filename") Then
                                        item.filename = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "foldername") Then
                                        item.foldername = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "fullpathandfilename") Then
                                        item.fullpathandfilename = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "genre") Then
                                        item.genre = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "id") Then
                                        item.id = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "playcount") Then
                                        item.playcount = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "rating") Then
                                        item.rating = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "title") Then
                                        item.title = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "titleandyear") Then
                                        item.titleandyear = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "top250") Then
                                        item.top250 = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "year") Then
                                        item.year = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "outline") Then
                                        item.outline = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "runtime") Then
                                        item.runtime = node2.InnerText
                                    End If
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator2 Is IDisposable Then
                                TryCast(enumerator2,IDisposable).Dispose
                            End If
                        End Try
                        If (item.movieset = Nothing) Then
                            item.movieset = "None"
                        End If
                        If (item.movieset = "") Then
                            item.movieset = "None"
                        End If
                        Module1.fullmovielist.Add(item)
                    End If
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator,IDisposable).Dispose
                End If
            End Try
        End Sub

        Private Shared Sub loadprofiles()
            Module1.profile_structure.profilelist.Clear
            Dim sPath As String = Path.Combine(Path.Combine(Module1.applicationpath, "settings"), "profile.xml")
            If File.Exists(sPath) Then
                Try
                    Dim document As New XmlDocument
                    document.Load(sPath)
                    If (document.DocumentElement.Name = "profile") Then
                        Dim enumerator As IEnumerator
                        Try
                            enumerator = document.Item("profile").GetEnumerator
                            Do While enumerator.MoveNext
                                Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator.Current)
                                Dim left As Object = NewLateBinding.LateGet(objectValue, Nothing, "Name", New Object(0 - 1) {}, Nothing, Nothing, Nothing)
                                If Operators.ConditionalCompareObjectEqual(left, "default", False) Then
                                    Module1.profile_structure.defaultprofile = Conversions.ToString(NewLateBinding.LateGet(objectValue, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                Else
                                    If Operators.ConditionalCompareObjectEqual(left, "startup", False) Then
                                        Module1.profile_structure.startupprofile = Conversions.ToString(NewLateBinding.LateGet(objectValue, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                        Continue Do
                                    End If
                                    If Operators.ConditionalCompareObjectEqual(left, "profiledetails", False) Then
                                        Dim enumerator2 As IEnumerator
                                        Dim item As New listofprofiles
                                        Try
                                            enumerator2 = DirectCast(NewLateBinding.LateGet(objectValue, Nothing, "childnodes", New Object(0 - 1) {}, Nothing, Nothing, Nothing), IEnumerable).GetEnumerator
                                            Do While enumerator2.MoveNext
                                                Dim instance As Object = RuntimeHelpers.GetObjectValue(enumerator2.Current)
                                                Dim obj5 As Object = NewLateBinding.LateGet(instance, Nothing, "name", New Object(0 - 1) {}, Nothing, Nothing, Nothing)
                                                If Operators.ConditionalCompareObjectEqual(obj5, "actorcache", False) Then
                                                    item.actorcache = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                Else
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "config", False) Then
                                                        item.config = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                        Continue Do
                                                    End If
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "moviecache", False) Then
                                                        item.moviecache = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                        Continue Do
                                                    End If
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "profilename", False) Then
                                                        item.profilename = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                        Continue Do
                                                    End If
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "regex", False) Then
                                                        item.regexlist = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                        Continue Do
                                                    End If
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "filters", False) Then
                                                        item.filters = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                        Continue Do
                                                    End If
                                                    If Operators.ConditionalCompareObjectEqual(obj5, "tvcache", False) Then
                                                        item.tvcache = Conversions.ToString(NewLateBinding.LateGet(instance, Nothing, "innertext", New Object(0 - 1) {}, Nothing, Nothing, Nothing))
                                                    End If
                                                End If
                                            Loop
                                        Finally
                                            If TypeOf enumerator2 Is IDisposable Then
                                                TryCast(enumerator2, IDisposable).Dispose()
                                            End If
                                        End Try
                                        Module1.profile_structure.profilelist.Add(item)
                                    End If
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator Is IDisposable Then
                                TryCast(enumerator, IDisposable).Dispose()
                            End If
                        End Try
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    ProjectData.ClearProjectError()
                End Try
            End If
        End Sub

        Private Shared Sub loadregex()
            Dim regexlist As String = Module1.workingprofile.regexlist
            Module1.tvregex.Clear
            Dim path As String = regexlist
            If File.Exists(path) Then
                Try 
                    Dim document As New XmlDocument
                    document.Load(path)
                    If (document.DocumentElement.Name = "regexlist") Then
                        Dim enumerator As IEnumerator
                        Try 
                            enumerator = document.Item("regexlist").GetEnumerator
                            Do While enumerator.MoveNext
                                Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator.Current)
                                If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, Nothing, "Name", New Object(0  - 1) {}, Nothing, Nothing, Nothing), "tvregex", False) Then
                                    Module1.tvregex.Add(Conversions.ToString(NewLateBinding.LateGet(objectValue, Nothing, "innertext", New Object(0  - 1) {}, Nothing, Nothing, Nothing)))
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator Is IDisposable Then
                                TryCast(enumerator,IDisposable).Dispose
                            End If
                        End Try
                    End If
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    ProjectData.ClearProjectError
                End Try
            End If
        End Sub

        Private Shared Sub loadtvcache()
            Dim enumerator As IEnumerator
            Module1.totalepisodecount = 0
            Module1.totaltvshowcount = 0
            Module1.basictvlist.Clear
            Dim document As New XmlDocument
            document.Load(Module1.workingprofile.tvcache)
            Dim current As XmlNode = Nothing
            Try 
                enumerator = document.Item("tv_cache").GetEnumerator
                Do While enumerator.MoveNext
                    current = DirectCast(enumerator.Current, XmlNode)
                    If (current.Name = "tvshow") Then
                        Dim enumerator2 As IEnumerator
                        Dim item As New basictvshownfo
                        Dim node2 As XmlNode = Nothing
                        Try 
                            enumerator2 = current.ChildNodes.GetEnumerator
                            Do While enumerator2.MoveNext
                                node2 = DirectCast(enumerator2.Current, XmlNode)
                                Dim name As String = node2.Name
                                If (name = "title") Then
                                    Dim innerText As String = ""
                                    innerText = node2.InnerText
                                    If (innerText.ToLower.IndexOf("the ") = 0) Then
                                        innerText = (innerText.Substring(4, (innerText.Length - 4)) & ", The")
                                    End If
                                    item.title = innerText
                                ElseIf (name = "fullpathandfilename") Then
                                    item.fullpath = node2.InnerText
                                Else
                                    If (name = "genre") Then
                                        item.genre = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "locked") Then
                                        item.locked = Conversions.ToInteger(node2.InnerText)
                                        Continue Do
                                    End If
                                    If (name = "imdbid") Then
                                        item.imdbid = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "tvdbid") Then
                                        item.tvdbid = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "rating") Then
                                        item.rating = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "year") Then
                                        item.year = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "language") Then
                                        item.language = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "sortorder") Then
                                        item.sortorder = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "episodeactorsource") Then
                                        item.episodeactorsource = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "status") Then
                                        item.status = node2.InnerText
                                        Continue Do
                                    End If
                                    If (name = "episode") Then
                                        Dim enumerator3 As IEnumerator
                                        Dim basicepisodenfo As New basicepisodenfo
                                        Dim node3 As XmlNode = Nothing
                                        Try 
                                            enumerator3 = node2.ChildNodes.GetEnumerator
                                            Do While enumerator3.MoveNext
                                                node3 = DirectCast(enumerator3.Current, XmlNode)
                                                Dim str4 As String = node3.Name
                                                If (str4 = "title") Then
                                                    basicepisodenfo.title = node3.InnerText
                                                Else
                                                    If (str4 = "episodepath") Then
                                                        basicepisodenfo.episodepath = node3.InnerText
                                                        Continue Do
                                                    End If
                                                    If (str4 = "seasonno") Then
                                                        basicepisodenfo.seasonno = node3.InnerText
                                                        Continue Do
                                                    End If
                                                    If (str4 = "episodeno") Then
                                                        basicepisodenfo.episodeno = node3.InnerText
                                                        Continue Do
                                                    End If
                                                    If (str4 = "playcount") Then
                                                        basicepisodenfo.playcount = node3.InnerText
                                                        Continue Do
                                                    End If
                                                    If (str4 = "rating") Then
                                                        basicepisodenfo.rating = node3.InnerText
                                                        Continue Do
                                                    End If
                                                    If (str4 = "tvdbid") Then
                                                        basicepisodenfo.tvdbid = node3.InnerText
                                                    End If
                                                End If
                                            Loop
                                        Finally
                                            If TypeOf enumerator3 Is IDisposable Then
                                                TryCast(enumerator3,IDisposable).Dispose
                                            End If
                                        End Try
                                        item.allepisodes.Add(basicepisodenfo)
                                    End If
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator2 Is IDisposable Then
                                TryCast(enumerator2,IDisposable).Dispose
                            End If
                        End Try
                        Module1.basictvlist.Add(item)
                    End If
                Loop
            Finally
                If TypeOf enumerator Is IDisposable Then
                    TryCast(enumerator,IDisposable).Dispose
                End If
            End Try
        End Sub

        Public Shared Function loadwebpage(ByVal tvdburl As String) As Object
            Dim tvdbwebsource As Object
            Try 
                Try 
                    Module1.tvdbwebsource = New String(&H2711  - 1) {}
                    Module1.tvfblinecount = 0
                    Dim request As WebRequest = WebRequest.Create(tvdburl)
                    Dim proxy As New WebProxy("myproxy", 80) With { _
                        .BypassProxyOnLocal = True _
                    }
                    Dim reader As New StreamReader(request.GetResponse.GetResponseStream)
                    Dim str As String = ""
                    Module1.tvfblinecount = 0
                    Do While (Not str Is Nothing)
                        Module1.tvfblinecount += 1
                        str = reader.ReadLine
                        If (Not str Is Nothing) Then
                            Module1.tvdbwebsource(Module1.tvfblinecount) = str
                        End If
                    Loop
                    reader.Close
                    Module1.tvfblinecount -= 1
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Module1.tvfblinecount = 1
                    Module1.tvdbwebsource(1) = "404"
                    ProjectData.ClearProjectError
                End Try
                tvdbwebsource = Module1.tvdbwebsource
            Catch exception2 As Exception
                ProjectData.SetProjectError(exception2)
                ProjectData.ClearProjectError
            End Try
            Return tvdbwebsource
        End Function

        <STAThread()> _
        Public Shared Sub Main()
            Dim flag2 As Boolean = False
            Dim flag4 As Boolean = False
            Dim flag As Boolean = False
            Console.WriteLine("****************************************************")
            Dim str3 As String
            For Each str3 In Environment.GetCommandLineArgs
                Module1.arguments.Add(str3)
            Next
            If (Module1.arguments.Count = 1) Then
                Dim item As New arguments With { _
                    .switch = "help" _
                }
                Module1.listofargs.Add(item)
            Else
                Dim num3 As Integer = (Module1.arguments.Count - 1)
                Dim i As Integer = 1
                Do While (i <= num3)
                    If (Module1.arguments.Item(i) = "-m") Then
                        Dim arguments2 As New arguments With { _
                            .switch = Module1.arguments.Item(i) _
                        }
                        Module1.listofargs.Add(arguments2)
                    ElseIf (Module1.arguments.Item(i) = "-e") Then
                        Dim arguments3 As New arguments With { _
                            .switch = Module1.arguments.Item(i) _
                        }
                        Module1.listofargs.Add(arguments3)
                    Else
                        If (Module1.arguments.Item(i) = "-p") Then
                            Dim arguments4 As New arguments With { _
                                .switch = Module1.arguments.Item(i) _
                            }
                            Try
                                arguments4.argu = Module1.arguments.Item((i + 1))
                                Module1.listofargs.Add(arguments4)
                                GoTo Label_028B
                            Catch exception1 As Exception
                                ProjectData.SetProjectError(exception1)
                                Module1.listofargs.Clear()
                                Dim arguments5 As New arguments With { _
                                    .switch = "help" _
                                }
                                Module1.listofargs.Add(arguments5)
                                ProjectData.ClearProjectError()
                                Exit Do
                            End Try
                        End If
                        If (Module1.arguments.Item(i) = "-h") Then
                            Dim arguments6 As New arguments With { _
                                .switch = Module1.arguments.Item(i) _
                            }
                            Try
                                arguments6.argu = Module1.arguments.Item((i + 1))
                                Module1.listofargs.Add(arguments6)
                                Try
                                    Module1.htmlfileoutput = Module1.arguments.Item((i + 2))
                                    flag = True
                                Catch exception2 As Exception
                                    ProjectData.SetProjectError(exception2)
                                    Dim exception As Exception = exception2
                                    Module1.listofargs.Clear()
                                    Dim arguments7 As New arguments With { _
                                        .switch = "help" _
                                    }
                                    Module1.listofargs.Add(arguments7)
                                    ProjectData.ClearProjectError()
                                    Exit Do
                                End Try
                            Catch exception3 As Exception
                                ProjectData.SetProjectError(exception3)
                                Module1.listofargs.Clear()
                                Dim arguments8 As New arguments With { _
                                    .switch = "help" _
                                }
                                Module1.listofargs.Add(arguments8)
                                ProjectData.ClearProjectError()
                                Exit Do
                            End Try
                        End If
Label_028B:
                    End If
                    i += 1
                Loop
            End If
            If (Module1.listofargs.Count = 0) Then
                Dim arguments9 As New arguments With { _
                    .switch = "help" _
                }
                Module1.listofargs.Add(arguments9)
            End If
            Dim arguments12 As arguments = Module1.listofargs.Item(0)
            If (arguments12.switch = "help") Then
                Console.WriteLine("Media Companion Command Line Tool")
                Console.WriteLine()
                Console.WriteLine("Useage")
                Console.WriteLine("mc_com.exe [-m] [-e] [-p ProfileName] [-h templatename outputpath]")
                Console.WriteLine("-m to scrape movies")
                Console.WriteLine("-e to scrape episodes")
                Console.WriteLine("-h [templatename] [outputpath] to output html list ")
                Console.WriteLine()
                Console.WriteLine("Example")
                Console.WriteLine("mc_com.exe -m -e -p billy -h basiclist C:\Movielist\testfile.html")
                Console.WriteLine("will search for and scrape any new movies and episodes")
                Console.WriteLine("using the folders and settings of the 'billy' profile")
                Console.WriteLine("then create a new html list using the named template")
                Console.WriteLine("Without the profile arg the defauld profile will be used")
                Console.WriteLine()
                Console.WriteLine("Tip: When using profile, template, or filenames that contain")
                Console.WriteLine("spaces, enclose with quotes, eg.")
                Console.WriteLine("mc_com.exe -m -p ""my profile"" -h ""new list"" ""C:\Movie list\test.html""")
                Console.WriteLine()
                Console.WriteLine("****************************************************")
                Environment.Exit(0)
            End If
            Dim arguments10 As arguments
            For Each arguments10 In Module1.listofargs
                If (arguments10.switch = "-m") Then
                    flag2 = True
                End If
                If (arguments10.switch = "-e") Then
                    flag4 = True
                End If
                If (arguments10.switch = "-p") Then
                    Module1.profile = arguments10.argu
                End If
                If (arguments10.switch = "-h") Then
                    flag = True
                End If
            Next
            Dim flag3 As Boolean = False
            Module1.defaultposter = Path.Combine(Module1.applicationpath, "Resources\default_poster.jpg")
            Console.WriteLine("Loading Config")
            Module1.setuppreferences()
            If Not File.Exists((Module1.applicationpath & "\settings\profile.xml")) Then
                Console.WriteLine(("Unable to find profile file: " & Module1.applicationpath & "\settings\profile.xml"))
                Console.WriteLine("****************************************************")
                Environment.Exit(1)
            Else
                Module1.loadprofiles()
                If (Module1.profile = "default") Then
                    Module1.profile = Module1.profile_structure.defaultprofile
                End If
                Dim listofprofiles As listofprofiles
                For Each listofprofiles In Module1.profile_structure.profilelist
                    If (listofprofiles.profilename = Module1.profile) Then
                        Module1.workingprofile.actorcache = listofprofiles.actorcache
                        Module1.workingprofile.config = listofprofiles.config
                        Module1.workingprofile.moviecache = listofprofiles.moviecache
                        Module1.workingprofile.profilename = listofprofiles.profilename
                        Module1.workingprofile.regexlist = listofprofiles.regexlist
                        Module1.workingprofile.filters = listofprofiles.filters
                        Module1.workingprofile.tvcache = listofprofiles.tvcache
                        Module1.workingprofile.profilename = listofprofiles.profilename
                        flag3 = True
                        Exit For
                    End If
                Next
                If Not flag3 Then
                    Console.WriteLine(("Unable to find profile name: " & Module1.profile))
                    Console.WriteLine("****************************************************")
                    Environment.Exit(1)
                End If
            End If
            Module1.defaultofflineart = Path.Combine(Module1.applicationpath, "Resources\default_offline.jpg")
            If File.Exists(Module1.workingprofile.config) Then
                Module1.loadconfig()
            End If
            If ((flag2 Or flag) AndAlso File.Exists(Module1.workingprofile.moviecache)) Then
                Module1.loadmoviecache()
            End If
            If flag2 Then
                Module1.startnewmovies()
                Module1.savemoviecache()
                Module1.saveactorcache()
                Console.WriteLine()
                Console.WriteLine("Movies search completed")
                Console.WriteLine()
                Console.WriteLine()
            End If
            If flag4 Then
                If File.Exists(Module1.workingprofile.tvcache) Then
                    Module1.loadtvcache()
                End If
                If File.Exists(Module1.workingprofile.regexlist) Then
                    Module1.loadregex()
                End If
                If (Module1.tvregex.Count = 0) Then
                    Module1.tvregex.Add("[Ss]([\d]{1,4}).?[Ee]([\d]{1,4})")
                    Module1.tvregex.Add("([\d]{1,4}) ?[xX] ?([\d]{1,4})")
                    Module1.tvregex.Add("([0-9]+)([0-9][0-9])")
                End If
                Dim basictvshownfo As basictvshownfo
                For Each basictvshownfo In Module1.basictvlist
                    If (basictvshownfo.fullpath.ToLower.IndexOf("tvshow.nfo") <> -1) Then
                        Module1.showstoscrapelist.Add(basictvshownfo.fullpath)
                    End If
                Next
                If (Module1.showstoscrapelist.Count > 0) Then
                    Module1.episodescraper(Module1.showstoscrapelist, False)
                    Module1.savetvcache()
                End If
            End If
            If flag Then
                Dim arguments11 As arguments
                For Each arguments11 In Module1.listofargs
                    If (arguments11.switch = "-h") Then
                        Console.WriteLine("Starting HTML Output")
                        Console.WriteLine()
                        Module1.addhtmltemplates()
                        Module1.setuphtml(arguments11.argu)
                        Console.WriteLine("HTML output complete")
                        Console.WriteLine()
                    End If
                Next
            End If
            Console.WriteLine()
            Console.WriteLine("Tasks Completed")
            Console.WriteLine("****************************************************")
            Environment.Exit(0)
        End Sub

        Public Shared Function mpdbthumb(ByVal posterimdbid As String) As Object
            Dim obj2 As Object
            Try 
                Dim flag As Boolean = False
                Dim str6 As String = "na"
                Dim str5 As String = posterimdbid
                str5 = str5.Replace("tt", "")
                Dim requestUriString As String = ("http://www.movieposterdb.com/movie/" & str5)
                Dim strArray As String() = New String(&H7D1  - 1) {}
                Dim index As Integer = 0
                Dim request As WebRequest = WebRequest.Create(requestUriString)
                Dim proxy As New WebProxy("myproxy", 80) With { _
                    .BypassProxyOnLocal = True _
                }
                Dim reader As New StreamReader(request.GetResponse.GetResponseStream)
                Dim str4 As String = ""
                index = 0
                Do While (Not str4 Is Nothing)
                    index += 1
                    strArray(index) = reader.ReadLine
                Loop
                index -= 1
                Dim num4 As Integer = index
                Dim i As Integer = 2
                Do While (i <= num4)
                    If (strArray(i).IndexOf("<title>") <> -1) Then
                        If (strArray(i).IndexOf("<title>MoviePosterDB.com - Internet Movie Poster DataBase</title>") <> -1) Then
                            flag = False
                        Else
                            flag = True
                        End If
                        Exit Do
                    End If
                    i += 1
                Loop
                If flag Then
                    flag = False
                    Dim num5 As Integer = index
                    Dim j As Integer = 2
                    Do While (j <= num5)
                        If (strArray(j).IndexOf("<img src=""http://www.movieposterdb.com/posters/") <> -1) Then
                            Dim str2 As String = Conversions.ToString(strArray(j).IndexOf("http"))
                            Dim str3 As String = Conversions.ToString(strArray(j).IndexOf("jpg"))
                            str6 = strArray(j).Substring(Conversions.ToInteger(str2), CInt(Math.Round(CDbl(((Conversions.ToDouble(str3) + 3) - Conversions.ToDouble(str2))))))
                            If (str6.IndexOf("t_") <> -1) Then
                                str6 = str6.Replace("t_", "l_")
                                Exit Do
                            End If
                            If (str6.IndexOf("s_") <> -1) Then
                                str6 = str6.Replace("s_", "l_")
                                Exit Do
                            End If
                        End If
                        j += 1
                    Loop
                End If
                If ((str6.IndexOf("http") = 0) And (str6.IndexOf(".jpg") = (str6.Length - 4))) Then
                    flag = True
                Else
                    str6 = "na"
                End If
                obj2 = str6
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Private Shared Sub offlinedvd(ByVal nfopath As String, ByVal title As String, ByVal mediapath As String)
            Dim num2 As Integer = &H200000
            Dim num As Integer = CInt(Microsoft.VisualBasic.FileSystem.FileLen(mediapath))
            If (num <= num2) Then
                Try 
                    Dim filename As String = ""
                    If File.Exists(Module1.getfanartpath(nfopath)) Then
                        filename = Module1.getfanartpath(nfopath)
                    Else
                        filename = Module1.defaultofflineart
                    End If
                    Dim image As Image = Image.FromFile(filename)
                    Dim str4 As String = ("Please Insert '" & title & "' DVD")
                    Dim graphics As Graphics = Graphics.FromImage(image)
                    Dim brush2 As New SolidBrush(Color.FromArgb(80, 0, 0, 0))
                    Dim text As String = str4
                    Dim font As New Font("Arial", 40!)
                    Dim brush As New SolidBrush(Color.White)
                    Dim ef2 As New SizeF
                    ef2 = graphics.MeasureString([text], font)
                    Dim width As Single = (ef2.Width + 5!)
                    Dim height As Single = (ef2.Height + 5!)
                    If (height < ((CDbl(image.Height) / 100) * 8)) Then
                        Do
                            Dim num7 As Integer = CInt(Math.Round(CDbl((font.Size + 1!))))
                            font = New Font("Arial", CSng(num7))
                            height = graphics.MeasureString([text], font).Height
                        Loop While (height <= ((CDbl(image.Height) / 100) * 8))
                    End If
                    If (height > ((CDbl(image.Height) / 100) * 8)) Then
                        Do
                            Dim num8 As Integer = CInt(Math.Round(CDbl((font.Size - 1!))))
                            font = New Font("Arial", CSng(num8))
                        Loop While (graphics.MeasureString([text], font).Height >= ((CDbl(image.Height) / 100) * 8))
                    End If
                    ef2 = graphics.MeasureString([text], font)
                    width = ef2.Width
                    height = ef2.Height
                    If (width > (image.Width - 30)) Then
                        Do
                            Dim num9 As Integer = CInt(Math.Round(CDbl((font.Size - 1!))))
                            font = New Font("Arial", CSng(num9))
                            width = (graphics.MeasureString([text], font).Width + 20!)
                        Loop While (width >= (image.Width - 30))
                    End If
                    ef2 = graphics.MeasureString([text], font)
                    width = (ef2.Width + 5!)
                    height = (ef2.Height + 5!)
                    Dim x As Integer = CInt(Math.Round(CDbl(((CDbl(image.Width) / 2) - (width / 2!)))))
                    Dim y As Integer = CInt(Math.Round(CDbl(((image.Height - ef2.Height) - ((CDbl(image.Height) / 100) * 2)))))
                    Dim layoutRectangle As New RectangleF(CSng(x), CSng(y), width, height)
                    Dim rect As New Rectangle(x, y, CInt(Math.Round(CDbl(width))), CInt(Math.Round(CDbl(height))))
                    graphics.FillRectangle(brush2, rect)
                    Dim format As New StringFormat With { _
                        .Alignment = StringAlignment.Center _
                    }
                    graphics.DrawString([text], font, brush, layoutRectangle, format)
                    Dim num10 As Integer = 1
                    Do
                        Dim str5 As String
                        If (num10 < 10) Then
                            str5 = (Module1.applicationpath & "\Settings\00" & num10.ToString & ".jpg")
                        Else
                            str5 = (Module1.applicationpath & "\Settings\0" & num10.ToString & ".jpg")
                        End If
                        image.Save(str5, ImageFormat.Jpeg)
                        num10 += 1
                    Loop While (num10 <= &H10)
                    Dim process As New Process
                    With process.StartInfo
                        .WindowStyle = ProcessWindowStyle.Hidden
                        .CreateNoWindow = False
                        .FileName = (Module1.applicationpath & "\ffmpeg.exe")
                    End With
                        Dim str3 As String = String.Concat(New String() {"-r 1 -b 1800 -qmax 6 -i """, Module1.applicationpath, "\Settings\%03d.jpg"" -vcodec msmpeg4v2 """, mediapath, """"})
                        process.StartInfo.Arguments = str3
                        process.Start()
                        process.WaitForExit()
                        Dim num11 As Integer = 1
                        Do
                            Dim str6 As String
                            If (num11 < 10) Then
                                str6 = (Module1.applicationpath & "\Settings\00" & num11.ToString & ".jpg")
                            Else
                                str6 = (Module1.applicationpath & "\Settings\0" & num11.ToString & ".jpg")
                            End If
                            Try
                                File.Delete(str6)
                            Catch exception1 As Exception
                                ProjectData.SetProjectError(exception1)
                                Dim exception As Exception = exception1
                                ProjectData.ClearProjectError()
                            End Try
                            num11 += 1
                        Loop While (num11 <= &H10)
                Catch exception2 As Exception
                    ProjectData.SetProjectError(exception2)
                    ProjectData.ClearProjectError
                End Try
            End If
        End Sub

        Public Shared Function resizeimage(ByVal bmp As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
            Dim image As New Bitmap(bmp)
            Dim bitmap As New Bitmap(width, height)
            Dim graphics As Graphics = Graphics.FromImage(bitmap)
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear
            graphics.DrawImage(image, 0, 0, CInt((width - 1)), CInt((height - 1)))
            Return bitmap
        End Function

        Private Shared Sub saveactorcache()
            Dim actorcache As String = Module1.workingprofile.actorcache
            Dim document As New XmlDocument
            Dim newChild As XmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            document.AppendChild(newChild)
            Dim element3 As XmlElement = document.CreateElement("actor_cache")
            Dim actordatabase As actordatabase
            For Each actordatabase In Module1.actordb
                Dim element As XmlElement = document.CreateElement("actor")
                Dim element2 As XmlElement = document.CreateElement("name")
                element2.InnerText = actordatabase.actorname
                element.AppendChild(element2)
                element2 = document.CreateElement("id")
                element2.InnerText = actordatabase.movieid
                element.AppendChild(element2)
                element3.AppendChild(element)
            Next
            document.AppendChild(element3)
            Dim w As New XmlTextWriter(actorcache, Encoding.UTF8) With { _
                .Formatting = Formatting.Indented _
            }
            document.WriteTo(w)
            w.Close
        End Sub

        Public Shared Sub saveepisodenfo(ByVal listofepisodes As List(Of episodeinfo), ByVal path As String, ByVal Optional seasonno As String = "-2", ByVal Optional episodeno As String = "-2")
            If (Not (Conversions.ToDouble(seasonno) = -2) And Not (Conversions.ToDouble(episodeno) = -2)) Then
                Dim flag As Boolean = False
                Dim basictvshownfo As basictvshownfo
                For Each basictvshownfo In Module1.basictvlist
                    If (basictvshownfo.fullpath = path) Then
                        Dim basicepisodenfo As basicepisodenfo
                        For Each basicepisodenfo In basictvshownfo.allepisodes
                            If ((basicepisodenfo.episodeno = episodeno) And (basicepisodenfo.seasonno = seasonno)) Then
                                Dim episodeinfo As episodeinfo
                                For Each episodeinfo In listofepisodes
                                    If ((episodeinfo.seasonno = seasonno) And (episodeinfo.episodeno = episodeno)) Then
                                        Dim item As New basicepisodenfo With { _
                                            .episodepath = episodeinfo.episodepath, _
                                            .title = episodeinfo.title, _
                                            .seasonno = episodeinfo.seasonno, _
                                            .episodeno = episodeinfo.episodeno, _
                                            .playcount = episodeinfo.playcount, _
                                            .rating = episodeinfo.rating _
                                        }
                                        basictvshownfo.allepisodes.Remove(basicepisodenfo)
                                        basictvshownfo.allepisodes.Add(item)
                                        flag = True
                                        Exit For
                                    End If
                                Next
                            End If
                            If flag Then
                                Exit For
                            End If
                        Next
                    End If
                    If flag Then
                        Exit For
                    End If
                Next
            End If
            If (listofepisodes.Count = 1) Then
                Dim element2 As XmlElement
                Dim document As New XmlDocument
                Dim newChild As XmlElement = document.CreateElement("episodedetails")
                Dim declaration As XmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                document.AppendChild(declaration)
                Dim node As XmlNode = Nothing
                If Module1.userprefs.enabletvhdtags Then
                    Try 
                        Dim element4 As XmlElement
                        element2 = document.CreateElement("fileinfo")
                        node = document.CreateElement("streamdetails")
                        Dim element3 As XmlElement = document.CreateElement("video")
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.width <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.width <> "")) Then
                            element4 = document.CreateElement("width")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.width
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.height <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.height <> "")) Then
                            element4 = document.CreateElement("height")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.height
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.aspect <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.aspect <> "")) Then
                            element4 = document.CreateElement("aspect")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.aspect
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.codec <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.codec <> "")) Then
                            element4 = document.CreateElement("codec")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.codec
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.formatinfo <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.formatinfo <> "")) Then
                            element4 = document.CreateElement("format")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.formatinfo
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.duration <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.duration <> "")) Then
                            element4 = document.CreateElement("duration")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.duration
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.bitrate <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.bitrate <> "")) Then
                            element4 = document.CreateElement("bitrate")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.bitrate
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.bitratemode <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.bitratemode <> "")) Then
                            element4 = document.CreateElement("bitratemode")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.bitratemode
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.bitratemax <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.bitratemax <> "")) Then
                            element4 = document.CreateElement("bitratemax")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.bitratemax
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.container <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.container <> "")) Then
                            element4 = document.CreateElement("container")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.container
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.codecid <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.codecid <> "")) Then
                            element4 = document.CreateElement("codecid")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.codecid
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.codecinfo <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.codecinfo <> "")) Then
                            element4 = document.CreateElement("codecidinfo")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.codecinfo
                            element3.AppendChild(element4)
                        End If
                        If ((listofepisodes.Item(0).filedetails.filedetails_video.scantype <> Nothing) AndAlso (listofepisodes.Item(0).filedetails.filedetails_video.scantype <> "")) Then
                            element4 = document.CreateElement("scantype")
                            element4.InnerText = listofepisodes.Item(0).filedetails.filedetails_video.scantype
                            element3.AppendChild(element4)
                        End If
                        node.AppendChild(element3)
                        If (listofepisodes.Item(0).filedetails.filedetails_audio.Count > 0) Then
                            Dim _audio As medianfo_audio
                            For Each _audio In listofepisodes.Item(0).filedetails.filedetails_audio
                                element3 = document.CreateElement("audio")
                                If ((_audio.language <> Nothing) AndAlso (_audio.language <> "")) Then
                                    element4 = document.CreateElement("language")
                                    element4.InnerText = _audio.language
                                    element3.AppendChild(element4)
                                End If
                                If ((_audio.codec <> Nothing) AndAlso (_audio.codec <> "")) Then
                                    element4 = document.CreateElement("codec")
                                    element4.InnerText = _audio.codec
                                    element3.AppendChild(element4)
                                End If
                                If ((_audio.channels <> Nothing) AndAlso (_audio.channels <> "")) Then
                                    element4 = document.CreateElement("channels")
                                    element4.InnerText = _audio.channels
                                    element3.AppendChild(element4)
                                End If
                                If ((_audio.bitrate <> Nothing) AndAlso (_audio.bitrate <> "")) Then
                                    element4 = document.CreateElement("bitrate")
                                    element4.InnerText = _audio.bitrate
                                    element3.AppendChild(element4)
                                    node.AppendChild(element3)
                                End If
                            Next
                            If (listofepisodes.Item(0).filedetails.filedetails_subtitles.Count > 0) Then
                                element3 = document.CreateElement("subtitle")
                                Dim _subtitles As medianfo_subtitles
                                For Each _subtitles In listofepisodes.Item(0).filedetails.filedetails_subtitles
                                    If ((_subtitles.language <> Nothing) AndAlso (_subtitles.language <> "")) Then
                                        element4 = document.CreateElement("language")
                                        element4.InnerText = _subtitles.language
                                        element3.AppendChild(element4)
                                    End If
                                Next
                                node.AppendChild(element3)
                            End If
                        End If
                        element2.AppendChild(node)
                        newChild.AppendChild(element2)
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        ProjectData.ClearProjectError
                    End Try
                End If
                element2 = document.CreateElement("title")
                element2.InnerText = listofepisodes.Item(0).title
                newChild.AppendChild(element2)
                element2 = document.CreateElement("season")
                element2.InnerText = listofepisodes.Item(0).seasonno
                newChild.AppendChild(element2)
                element2 = document.CreateElement("episode")
                element2.InnerText = listofepisodes.Item(0).episodeno
                newChild.AppendChild(element2)
                element2 = document.CreateElement("aired")
                element2.InnerText = listofepisodes.Item(0).aired
                newChild.AppendChild(element2)
                element2 = document.CreateElement("plot")
                element2.InnerText = listofepisodes.Item(0).plot
                newChild.AppendChild(element2)
                element2 = document.CreateElement("playcount")
                element2.InnerText = listofepisodes.Item(0).playcount
                newChild.AppendChild(element2)
                element2 = document.CreateElement("director")
                element2.InnerText = listofepisodes.Item(0).director
                newChild.AppendChild(element2)
                element2 = document.CreateElement("credits")
                element2.InnerText = listofepisodes.Item(0).credits
                newChild.AppendChild(element2)
                element2 = document.CreateElement("rating")
                element2.InnerText = listofepisodes.Item(0).rating
                newChild.AppendChild(element2)
                element2 = document.CreateElement("runtime")
                element2.InnerText = listofepisodes.Item(0).runtime
                newChild.AppendChild(element2)
                Dim count As Integer = listofepisodes.Item(0).listactors.Count
                If (count > Module1.userprefs.maxactors) Then
                    count = Module1.userprefs.maxactors
                End If
                Dim num3 As Integer = (count - 1)
                Dim i As Integer = 0
                Do While (i <= num3)
                    element2 = document.CreateElement("actor")
                    Dim element As XmlElement = document.CreateElement("name")
                    Dim movieactors2 As movieactors = listofepisodes.Item(0).listactors.Item(i)
                    element.InnerText = movieactors2.actorname
                    element2.AppendChild(element)
                    element = document.CreateElement("role")
                    movieactors2 = listofepisodes.Item(0).listactors.Item(i)
                    element.InnerText = movieactors2.actorrole
                    element2.AppendChild(element)
                    movieactors2 = listofepisodes.Item(0).listactors.Item(i)
                    If (movieactors2.actorthumb <> Nothing) Then
                        Dim movieactors3 As movieactors = listofepisodes.Item(0).listactors.Item(i)
                        If (movieactors3.actorthumb <> "") Then
                            element = document.CreateElement("thumb")
                            movieactors3 = listofepisodes.Item(0).listactors.Item(i)
                            element.InnerText = movieactors3.actorthumb
                            element2.AppendChild(element)
                        End If
                    End If
                    newChild.AppendChild(element2)
                    i += 1
                Loop
                document.AppendChild(newChild)
                Dim w As New XmlTextWriter(path, Encoding.UTF8) With { _
                    .Formatting = Formatting.Indented _
                }
                document.WriteTo(w)
                w.Close
            Else
                Dim enumerator As IEnumerator(Of episodeinfo)
                Dim document2 As New XmlDocument
                Dim declaration2 As XmlDeclaration = document2.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                document2.AppendChild(declaration2)
                Dim element11 As XmlElement = document2.CreateElement("multiepisodenfo")
                Dim flag2 As Boolean = False
                Try 
                    enumerator = listofepisodes.GetEnumerator
                    Do While enumerator.MoveNext
                        Dim element7 As XmlElement
                        Dim element8 As XmlElement
                        Dim current As episodeinfo = enumerator.Current
                        Dim element6 As XmlElement = document2.CreateElement("episodedetails")
                        If (Not flag2 AndAlso Module1.userprefs.enabletvhdtags) Then
                            Try 
                                Dim element9 As XmlElement
                                Dim element10 As XmlElement = document2.CreateElement("streamdetails")
                                element7 = document2.CreateElement("fileinfo")
                                element8 = document2.CreateElement("video")
                                If ((current.filedetails.filedetails_video.width <> Nothing) AndAlso (current.filedetails.filedetails_video.width <> "")) Then
                                    element9 = document2.CreateElement("width")
                                    element9.InnerText = current.filedetails.filedetails_video.width
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.height <> Nothing) AndAlso (current.filedetails.filedetails_video.height <> "")) Then
                                    element9 = document2.CreateElement("height")
                                    element9.InnerText = current.filedetails.filedetails_video.height
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.codec <> Nothing) AndAlso (current.filedetails.filedetails_video.codec <> "")) Then
                                    element9 = document2.CreateElement("codec")
                                    element9.InnerText = current.filedetails.filedetails_video.codec
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.formatinfo <> Nothing) AndAlso (current.filedetails.filedetails_video.formatinfo <> "")) Then
                                    element9 = document2.CreateElement("format")
                                    element9.InnerText = current.filedetails.filedetails_video.formatinfo
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.duration <> Nothing) AndAlso (current.filedetails.filedetails_video.duration <> "")) Then
                                    element9 = document2.CreateElement("duration")
                                    element9.InnerText = current.filedetails.filedetails_video.duration
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.bitrate <> Nothing) AndAlso (current.filedetails.filedetails_video.bitrate <> "")) Then
                                    element9 = document2.CreateElement("width")
                                    element9.InnerText = current.filedetails.filedetails_video.bitrate
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.bitratemode <> Nothing) AndAlso (current.filedetails.filedetails_video.bitratemode <> "")) Then
                                    element9 = document2.CreateElement("bitratemode")
                                    element9.InnerText = current.filedetails.filedetails_video.bitratemode
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.bitratemax <> Nothing) AndAlso (current.filedetails.filedetails_video.bitratemax <> "")) Then
                                    element9 = document2.CreateElement("bitratemax")
                                    element9.InnerText = current.filedetails.filedetails_video.bitratemax
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.container <> Nothing) AndAlso (current.filedetails.filedetails_video.container <> "")) Then
                                    element9 = document2.CreateElement("container")
                                    element9.InnerText = current.filedetails.filedetails_video.container
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.codecid <> Nothing) AndAlso (current.filedetails.filedetails_video.codecid <> "")) Then
                                    element9 = document2.CreateElement("codecid")
                                    element9.InnerText = current.filedetails.filedetails_video.codecid
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.codecinfo <> Nothing) AndAlso (current.filedetails.filedetails_video.codecinfo <> "")) Then
                                    element9 = document2.CreateElement("codecidinfo")
                                    element9.InnerText = current.filedetails.filedetails_video.codecinfo
                                    element8.AppendChild(element9)
                                End If
                                If ((current.filedetails.filedetails_video.scantype <> Nothing) AndAlso (current.filedetails.filedetails_video.scantype <> "")) Then
                                    element9 = document2.CreateElement("scantype")
                                    element9.InnerText = current.filedetails.filedetails_video.scantype
                                    element8.AppendChild(element9)
                                End If
                                element7.AppendChild(element8)
                                If (current.filedetails.filedetails_audio.Count > 0) Then
                                    Dim _audio2 As medianfo_audio
                                    For Each _audio2 In current.filedetails.filedetails_audio
                                        element8 = document2.CreateElement("audio")
                                        If ((_audio2.language <> Nothing) AndAlso (_audio2.language <> "")) Then
                                            element9 = document2.CreateElement("language")
                                            element9.InnerText = _audio2.language
                                            element8.AppendChild(element9)
                                        End If
                                        If ((_audio2.codec <> Nothing) AndAlso (_audio2.codec <> "")) Then
                                            element9 = document2.CreateElement("codec")
                                            element9.InnerText = _audio2.codec
                                            element8.AppendChild(element9)
                                        End If
                                        If ((_audio2.channels <> Nothing) AndAlso (_audio2.channels <> "")) Then
                                            element9 = document2.CreateElement("channels")
                                            element9.InnerText = _audio2.channels
                                            element8.AppendChild(element9)
                                        End If
                                        If ((_audio2.bitrate <> Nothing) AndAlso (_audio2.bitrate <> "")) Then
                                            element9 = document2.CreateElement("bitrate")
                                            element9.InnerText = _audio2.bitrate
                                            element8.AppendChild(element9)
                                        End If
                                        element7.AppendChild(element8)
                                    Next
                                End If
                                If (current.filedetails.filedetails_subtitles.Count > 0) Then
                                    Dim _subtitles2 As medianfo_subtitles
                                    For Each _subtitles2 In current.filedetails.filedetails_subtitles
                                        If ((_subtitles2.language <> Nothing) AndAlso (_subtitles2.language <> "")) Then
                                            element8 = document2.CreateElement("subtitle")
                                            element9 = document2.CreateElement("language")
                                            element9.InnerText = _subtitles2.language
                                            element8.AppendChild(element9)
                                        End If
                                        element7.AppendChild(element8)
                                    Next
                                End If
                                element10.AppendChild(element7)
                                element6.AppendChild(element10)
                            Catch exception2 As Exception
                                ProjectData.SetProjectError(exception2)
                                ProjectData.ClearProjectError
                            End Try
                        End If
                        element7 = document2.CreateElement("title")
                        element7.InnerText = current.title
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("season")
                        element7.InnerText = current.seasonno
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("episode")
                        element7.InnerText = current.episodeno
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("playcount")
                        element7.InnerText = current.playcount
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("credits")
                        element7.InnerText = current.credits
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("director")
                        element7.InnerText = current.director
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("rating")
                        element7.InnerText = current.rating
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("aired")
                        element7.InnerText = current.aired
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("plot")
                        element7.InnerText = current.plot
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("thumb")
                        element7.InnerText = current.thumb
                        element6.AppendChild(element7)
                        element7 = document2.CreateElement("runtime")
                        element7.InnerText = current.runtime
                        element6.AppendChild(element7)
                        Dim movieactors As movieactors
                        For Each movieactors In current.listactors
                            Try 
                                element7 = document2.CreateElement("actor")
                                element8 = document2.CreateElement("name")
                                element8.InnerText = movieactors.actorname
                                element7.AppendChild(element8)
                                element8 = document2.CreateElement("role")
                                element8.InnerText = movieactors.actorrole
                                element7.AppendChild(element8)
                                element8 = document2.CreateElement("thumb")
                                element8.InnerText = movieactors.actorthumb
                                element7.AppendChild(element8)
                                element6.AppendChild(element7)
                                Continue For
                            Catch exception3 As Exception
                                ProjectData.SetProjectError(exception3)
                                ProjectData.ClearProjectError
                                Continue For
                            End Try
                        Next
                        element11.AppendChild(element6)
                        document2.AppendChild(element11)
                    Loop
                Finally
                    enumerator.Dispose
                End Try
                Try 
                    Dim writer2 As New XmlTextWriter(path, Encoding.UTF8) With { _
                        .Formatting = Formatting.Indented _
                    }
                    document2.WriteTo(writer2)
                    writer2.Close
                Catch exception4 As Exception
                    ProjectData.SetProjectError(exception4)
                    ProjectData.ClearProjectError
                End Try
            End If
        End Sub

        Private Shared Sub savemoviecache()
            Dim moviecache As String = Module1.workingprofile.moviecache
            If File.Exists(moviecache) Then
                Dim flag As Boolean = False
                Dim num As Integer = 0
                Do
                    Try 
                        File.Delete(moviecache)
                        flag = True
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
                        ProjectData.ClearProjectError
                    Finally
                        num += 1
                    End Try
                Loop While Not flag
            End If
            Dim document As New XmlDocument
            Dim newChild As XmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            document.AppendChild(newChild)
            Dim element3 As XmlElement = document.CreateElement("movie_cache")
            Dim combolist As combolist
            For Each combolist In Module1.fullmovielist
                Dim element As XmlElement = document.CreateElement("movie")
                Dim element2 As XmlElement = document.CreateElement("filedate")
                element2.InnerText = combolist.filedate
                element.AppendChild(element2)
                element2 = document.CreateElement("missingdata1")
                element2.InnerText = combolist.missingdata1.ToString
                element.AppendChild(element2)
                element2 = document.CreateElement("filename")
                element2.InnerText = combolist.filename
                element.AppendChild(element2)
                element2 = document.CreateElement("foldername")
                element2.InnerText = combolist.foldername
                element.AppendChild(element2)
                element2 = document.CreateElement("fullpathandfilename")
                element2.InnerText = combolist.fullpathandfilename
                element.AppendChild(element2)
                If (combolist.movieset <> Nothing) Then
                    If (combolist.movieset <> "") Then
                        element2 = document.CreateElement("set")
                        element2.InnerText = combolist.movieset
                        element.AppendChild(element2)
                    Else
                        element2 = document.CreateElement("set")
                        element2.InnerText = "None"
                        element.AppendChild(element2)
                    End If
                Else
                    element2 = document.CreateElement("set")
                    element2.InnerText = "None"
                    element.AppendChild(element2)
                End If
                element2 = document.CreateElement("genre")
                element2.InnerText = combolist.genre
                element.AppendChild(element2)
                element2 = document.CreateElement("id")
                element2.InnerText = combolist.id
                element.AppendChild(element2)
                element2 = document.CreateElement("playcount")
                element2.InnerText = combolist.playcount
                element.AppendChild(element2)
                element2 = document.CreateElement("rating")
                element2.InnerText = combolist.rating
                element.AppendChild(element2)
                element2 = document.CreateElement("title")
                element2.InnerText = combolist.title
                element.AppendChild(element2)
                If (combolist.sortorder = Nothing) Then
                    combolist.sortorder = combolist.title
                End If
                If (combolist.sortorder = "") Then
                    combolist.sortorder = combolist.title
                End If
                element2 = document.CreateElement("outline")
                element2.InnerText = combolist.outline
                element.AppendChild(element2)
                element2 = document.CreateElement("sortorder")
                element2.InnerText = combolist.sortorder
                element.AppendChild(element2)
                element2 = document.CreateElement("titleandyear")
                element2.InnerText = combolist.titleandyear
                element.AppendChild(element2)
                element2 = document.CreateElement("runtime")
                element2.InnerText = combolist.runtime
                element.AppendChild(element2)
                element2 = document.CreateElement("top250")
                element2.InnerText = combolist.top250
                element.AppendChild(element2)
                element2 = document.CreateElement("year")
                element2.InnerText = combolist.year
                element.AppendChild(element2)
                element3.AppendChild(element)
            Next
            document.AppendChild(element3)
            Dim num2 As Integer = 1
            Do
                Try 
                    Dim w As New XmlTextWriter(moviecache, Encoding.UTF8) With { _
                        .Formatting = Formatting.Indented _
                    }
                    document.WriteTo(w)
                    w.Close
                    Exit Do
                Catch exception2 As Exception
                    ProjectData.SetProjectError(exception2)
                    ProjectData.ClearProjectError
                End Try
                num2 += 1
            Loop While (num2 <= 100)
        End Sub

        Public Shared Sub savemovienfo(ByVal filenameandpath As String, ByVal movietosave As fullmoviedetails, ByVal Optional overwrite As Boolean = True)
            Dim num As Integer = 1
            Try 
                If (Not movietosave Is Nothing) Then
                    If (Not File.Exists(filenameandpath) Or overwrite) Then
                        Dim element3 As XmlElement
                        Dim document As New XmlDocument
                        Dim str As String = ""
                        num = 2
                        Dim newChild As XmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
                        document.AppendChild(newChild)
                        Dim element6 As XmlElement = document.CreateElement("movie")
                        num = 3
                        If Module1.userprefs.enablehdtags Then
                            Dim element2 As XmlElement
                            Dim element4 As XmlElement
                            Dim element5 As XmlElement
                            Try 
                                element3 = document.CreateElement("fileinfo")
                            Catch exception1 As Exception
                                ProjectData.SetProjectError(exception1)
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                element2 = document.CreateElement("streamdetails")
                            Catch exception9 As Exception
                                ProjectData.SetProjectError(exception9)
                                Dim exception As Exception = exception9
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                element4 = document.CreateElement("video")
                            Catch exception10 As Exception
                                ProjectData.SetProjectError(exception10)
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                If ((movietosave.filedetails.filedetails_video.width <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.width <> "")) Then
                                    element5 = document.CreateElement("width")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.width
                                    element4.AppendChild(element5)
                                End If
                            Catch exception11 As Exception
                                ProjectData.SetProjectError(exception11)
                                ProjectData.ClearProjectError
                            End Try
                            num = 4
                            Try 
                                If ((movietosave.filedetails.filedetails_video.height <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.height <> "")) Then
                                    element5 = document.CreateElement("height")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.height
                                    element4.AppendChild(element5)
                                End If
                            Catch exception12 As Exception
                                ProjectData.SetProjectError(exception12)
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                If ((movietosave.filedetails.filedetails_video.aspect <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.aspect <> "")) Then
                                    element5 = document.CreateElement("aspect")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.aspect
                                    element4.AppendChild(element5)
                                End If
                            Catch exception13 As Exception
                                ProjectData.SetProjectError(exception13)
                                ProjectData.ClearProjectError
                            End Try
                            num = 5
                            Try 
                                If ((movietosave.filedetails.filedetails_video.codec <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.codec <> "")) Then
                                    element5 = document.CreateElement("codec")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.codec
                                    element4.AppendChild(element5)
                                End If
                            Catch exception14 As Exception
                                ProjectData.SetProjectError(exception14)
                                ProjectData.ClearProjectError
                            End Try
                            num = 6
                            Try 
                                If ((movietosave.filedetails.filedetails_video.formatinfo <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.formatinfo <> "")) Then
                                    element5 = document.CreateElement("format")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.formatinfo
                                    element4.AppendChild(element5)
                                End If
                            Catch exception15 As Exception
                                ProjectData.SetProjectError(exception15)
                                ProjectData.ClearProjectError
                            End Try
                            num = 7
                            Try 
                                If ((movietosave.filedetails.filedetails_video.duration <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.duration <> "")) Then
                                    element5 = document.CreateElement("duration")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.duration
                                    element4.AppendChild(element5)
                                End If
                            Catch exception16 As Exception
                                ProjectData.SetProjectError(exception16)
                                ProjectData.ClearProjectError
                            End Try
                            num = 8
                            Try 
                                If ((movietosave.filedetails.filedetails_video.bitrate <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.bitrate <> "")) Then
                                    element5 = document.CreateElement("bitrate")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.bitrate
                                    element4.AppendChild(element5)
                                End If
                            Catch exception17 As Exception
                                ProjectData.SetProjectError(exception17)
                                ProjectData.ClearProjectError
                            End Try
                            num = 9
                            Try 
                                If ((movietosave.filedetails.filedetails_video.bitratemode <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.bitratemode <> "")) Then
                                    element5 = document.CreateElement("bitratemode")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.bitratemode
                                    element4.AppendChild(element5)
                                End If
                            Catch exception18 As Exception
                                ProjectData.SetProjectError(exception18)
                                ProjectData.ClearProjectError
                            End Try
                            num = 10
                            Try 
                                If ((movietosave.filedetails.filedetails_video.bitratemax <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.bitratemax <> "")) Then
                                    element5 = document.CreateElement("bitratemax")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.bitratemax
                                    element4.AppendChild(element5)
                                End If
                            Catch exception19 As Exception
                                ProjectData.SetProjectError(exception19)
                                ProjectData.ClearProjectError
                            End Try
                            num = 11
                            Try 
                                If ((movietosave.filedetails.filedetails_video.container <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.container <> "")) Then
                                    element5 = document.CreateElement("container")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.container
                                    element4.AppendChild(element5)
                                End If
                            Catch exception20 As Exception
                                ProjectData.SetProjectError(exception20)
                                ProjectData.ClearProjectError
                            End Try
                            num = 12
                            Try 
                                If ((movietosave.filedetails.filedetails_video.codecid <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.codecid <> "")) Then
                                    element5 = document.CreateElement("codecid")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.codecid
                                    element4.AppendChild(element5)
                                End If
                            Catch exception21 As Exception
                                ProjectData.SetProjectError(exception21)
                                ProjectData.ClearProjectError
                            End Try
                            num = 13
                            Try 
                                If ((movietosave.filedetails.filedetails_video.codecinfo <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.codecinfo <> "")) Then
                                    element5 = document.CreateElement("codecidinfo")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.codecinfo
                                    element4.AppendChild(element5)
                                End If
                            Catch exception22 As Exception
                                ProjectData.SetProjectError(exception22)
                                ProjectData.ClearProjectError
                            End Try
                            num = 14
                            Try 
                                If ((movietosave.filedetails.filedetails_video.scantype <> Nothing) AndAlso (movietosave.filedetails.filedetails_video.scantype <> "")) Then
                                    element5 = document.CreateElement("scantype")
                                    element5.InnerText = movietosave.filedetails.filedetails_video.scantype
                                    element4.AppendChild(element5)
                                End If
                            Catch exception23 As Exception
                                ProjectData.SetProjectError(exception23)
                                ProjectData.ClearProjectError
                            End Try
                            num = 15
                            Try 
                                element2.AppendChild(element4)
                            Catch exception24 As Exception
                                ProjectData.SetProjectError(exception24)
                                ProjectData.ClearProjectError
                            End Try
                            num = &H10
                            Dim _audio As medianfo_audio
                            For Each _audio In movietosave.filedetails.filedetails_audio
                                Try 
                                    element4 = document.CreateElement("audio")
                                Catch exception25 As Exception
                                    ProjectData.SetProjectError(exception25)
                                    ProjectData.ClearProjectError
                                End Try
                                Try 
                                    If ((_audio.language <> Nothing) AndAlso (_audio.language <> "")) Then
                                        element5 = document.CreateElement("language")
                                        element5.InnerText = _audio.language
                                        element4.AppendChild(element5)
                                    End If
                                Catch exception26 As Exception
                                    ProjectData.SetProjectError(exception26)
                                    ProjectData.ClearProjectError
                                End Try
                                Try 
                                    If ((_audio.codec <> Nothing) AndAlso (_audio.codec <> "")) Then
                                        element5 = document.CreateElement("codec")
                                        element5.InnerText = _audio.codec
                                        element4.AppendChild(element5)
                                    End If
                                Catch exception27 As Exception
                                    ProjectData.SetProjectError(exception27)
                                    ProjectData.ClearProjectError
                                End Try
                                Try 
                                    If ((_audio.channels <> Nothing) AndAlso (_audio.channels <> "")) Then
                                        element5 = document.CreateElement("channels")
                                        element5.InnerText = _audio.channels
                                        element4.AppendChild(element5)
                                    End If
                                Catch exception28 As Exception
                                    ProjectData.SetProjectError(exception28)
                                    ProjectData.ClearProjectError
                                End Try
                                Try 
                                    If ((_audio.bitrate <> Nothing) AndAlso (_audio.bitrate <> "")) Then
                                        element5 = document.CreateElement("bitrate")
                                        element5.InnerText = _audio.bitrate
                                        element4.AppendChild(element5)
                                        element2.AppendChild(element4)
                                    End If
                                    Continue For
                                Catch exception29 As Exception
                                    ProjectData.SetProjectError(exception29)
                                    ProjectData.ClearProjectError
                                    Continue For
                                End Try
                            Next
                            num = &H11
                            Try 
                                element4 = document.CreateElement("subtitle")
                            Catch exception30 As Exception
                                ProjectData.SetProjectError(exception30)
                                ProjectData.ClearProjectError
                            End Try
                            Dim num2 As Integer = 0
                            Dim _subtitles As medianfo_subtitles
                            For Each _subtitles In movietosave.filedetails.filedetails_subtitles
                                Try 
                                    If ((_subtitles.language <> Nothing) AndAlso (_subtitles.language <> "")) Then
                                        num2 += 1
                                        element5 = document.CreateElement("language")
                                        element5.InnerText = _subtitles.language
                                        element4.AppendChild(element5)
                                    End If
                                    Continue For
                                Catch exception31 As Exception
                                    ProjectData.SetProjectError(exception31)
                                    ProjectData.ClearProjectError
                                    Continue For
                                End Try
                            Next
                            num = &H12
                            Try 
                                If (num2 > 0) Then
                                    element2.AppendChild(element4)
                                End If
                            Catch exception32 As Exception
                                ProjectData.SetProjectError(exception32)
                                Dim exception2 As Exception = exception32
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                element3.AppendChild(element2)
                            Catch exception33 As Exception
                                ProjectData.SetProjectError(exception33)
                                ProjectData.ClearProjectError
                            End Try
                            Try 
                                element6.AppendChild(element3)
                            Catch exception34 As Exception
                                ProjectData.SetProjectError(exception34)
                                ProjectData.ClearProjectError
                            End Try
                        End If
                        Try 
                            element3 = document.CreateElement("title")
                            element3.InnerText = movietosave.fullmoviebody.title
                            element6.AppendChild(element3)
                        Catch exception35 As Exception
                            ProjectData.SetProjectError(exception35)
                            ProjectData.ClearProjectError
                        End Try
                        If (movietosave.alternativetitles.Count > 0) Then
                            Try 
                                Dim enumerator As IEnumerator(Of String)
                                Try 
                                    enumerator = movietosave.alternativetitles.GetEnumerator
                                    Do While enumerator.MoveNext
                                        Dim current As String = enumerator.Current
                                        If (current <> movietosave.fullmoviebody.title) Then
                                            Try 
                                                element3 = document.CreateElement("alternativetitle")
                                                element3.InnerText = current
                                                element6.AppendChild(element3)
                                                Continue Do
                                            Catch exception36 As Exception
                                                ProjectData.SetProjectError(exception36)
                                                Dim exception3 As Exception = exception36
                                                ProjectData.ClearProjectError
                                                Continue Do
                                            End Try
                                        End If
                                    Loop
                                Finally
                                    enumerator.Dispose
                                End Try
                            Catch exception37 As Exception
                                ProjectData.SetProjectError(exception37)
                                ProjectData.ClearProjectError
                            End Try
                        End If
                        Try 
                            If ((movietosave.fullmoviebody.movieset <> Nothing) AndAlso (movietosave.fullmoviebody.movieset <> "None")) Then
                                element3 = document.CreateElement("set")
                                element3.InnerText = movietosave.fullmoviebody.movieset
                                element6.AppendChild(element3)
                            End If
                        Catch exception38 As Exception
                            ProjectData.SetProjectError(exception38)
                            Dim exception4 As Exception = exception38
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            If (movietosave.fullmoviebody.sortorder = Nothing) Then
                                movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                            End If
                            If (movietosave.fullmoviebody.sortorder = "") Then
                                movietosave.fullmoviebody.sortorder = movietosave.fullmoviebody.title
                            End If
                            element3 = document.CreateElement("sorttitle")
                            element3.InnerText = movietosave.fullmoviebody.sortorder
                            element6.AppendChild(element3)
                        Catch exception39 As Exception
                            ProjectData.SetProjectError(exception39)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H13
                        Try 
                            element3 = document.CreateElement("year")
                            element3.InnerText = movietosave.fullmoviebody.year
                            element6.AppendChild(element3)
                        Catch exception40 As Exception
                            ProjectData.SetProjectError(exception40)
                            ProjectData.ClearProjectError
                        End Try
                        num = 20
                        Try 
                            element3 = document.CreateElement("rating")
                            element3.InnerText = movietosave.fullmoviebody.rating
                            element6.AppendChild(element3)
                        Catch exception41 As Exception
                            ProjectData.SetProjectError(exception41)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H15
                        Try 
                            element3 = document.CreateElement("votes")
                            element3.InnerText = movietosave.fullmoviebody.votes
                            element6.AppendChild(element3)
                        Catch exception42 As Exception
                            ProjectData.SetProjectError(exception42)
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            element3 = document.CreateElement("top250")
                            element3.InnerText = movietosave.fullmoviebody.top250
                            element6.AppendChild(element3)
                        Catch exception43 As Exception
                            ProjectData.SetProjectError(exception43)
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            element3 = document.CreateElement("outline")
                            element3.InnerText = movietosave.fullmoviebody.outline
                            element6.AppendChild(element3)
                        Catch exception44 As Exception
                            ProjectData.SetProjectError(exception44)
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            element3 = document.CreateElement("plot")
                            element3.InnerText = movietosave.fullmoviebody.plot
                            element6.AppendChild(element3)
                        Catch exception45 As Exception
                            ProjectData.SetProjectError(exception45)
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            element3 = document.CreateElement("tagline")
                            element3.InnerText = movietosave.fullmoviebody.tagline
                            element6.AppendChild(element3)
                        Catch exception46 As Exception
                            ProjectData.SetProjectError(exception46)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H16
                        Try 
                            Dim enumerator4 As IEnumerator(Of String)
                            Try 
                                enumerator4 = movietosave.listthumbs.GetEnumerator
                                Do While enumerator4.MoveNext
                                    Dim str3 As String = enumerator4.Current
                                    Try 
                                        element3 = document.CreateElement("thumb")
                                        element3.InnerText = str3
                                        element6.AppendChild(element3)
                                        Continue Do
                                    Catch exception47 As Exception
                                        ProjectData.SetProjectError(exception47)
                                        ProjectData.ClearProjectError
                                        Continue Do
                                    End Try
                                Loop
                            Finally
                                enumerator4.Dispose
                            End Try
                        Catch exception48 As Exception
                            ProjectData.SetProjectError(exception48)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H17
                        Try 
                            If (str <> "") Then
                                element3 = document.CreateElement("thumb")
                                element3.InnerText = str
                                element6.AppendChild(element3)
                            End If
                        Catch exception49 As Exception
                            ProjectData.SetProjectError(exception49)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H18
                        Try 
                            element3 = document.CreateElement("runtime")
                            If (movietosave.fullmoviebody.runtime <> Nothing) Then
                                Dim runtime As String = movietosave.fullmoviebody.runtime.Replace("minutes", "").Replace("mins", "").Replace("min", "").Replace(" ", "")
                                Try 
                                    Do While (runtime.IndexOf("0") = 0)
                                        runtime = runtime.Substring(1, (runtime.Length - 1))
                                    Loop
                                    If (((Convert.ToInt32(runtime) < 100) And (Convert.ToInt32(runtime) > 10)) And Module1.userprefs.roundminutes) Then
                                        runtime = ("0" & runtime & " min")
                                    ElseIf (((Convert.ToInt32(runtime) < 100) And (Convert.ToInt32(runtime) < 10)) And Module1.userprefs.roundminutes) Then
                                        runtime = ("00" & runtime & " min")
                                    Else
                                        runtime = movietosave.fullmoviebody.runtime
                                    End If
                                Catch exception50 As Exception
                                    ProjectData.SetProjectError(exception50)
                                    Dim exception5 As Exception = exception50
                                    runtime = movietosave.fullmoviebody.runtime
                                    ProjectData.ClearProjectError
                                End Try
                                element3.InnerText = runtime
                            Else
                                element3.InnerText = movietosave.fullmoviebody.runtime
                            End If
                            element6.AppendChild(element3)
                        Catch exception51 As Exception
                            ProjectData.SetProjectError(exception51)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H19
                        Try 
                            element3 = document.CreateElement("mpaa")
                            element3.InnerText = movietosave.fullmoviebody.mpaa
                            element6.AppendChild(element3)
                        Catch exception52 As Exception
                            ProjectData.SetProjectError(exception52)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H1A
                        Try 
                            element3 = document.CreateElement("genre")
                            element3.InnerText = movietosave.fullmoviebody.genre
                            element6.AppendChild(element3)
                        Catch exception53 As Exception
                            ProjectData.SetProjectError(exception53)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H1B
                        Try 
                            element3 = document.CreateElement("credits")
                            element3.InnerText = movietosave.fullmoviebody.credits
                            element6.AppendChild(element3)
                        Catch exception54 As Exception
                            ProjectData.SetProjectError(exception54)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H1C
                        Try 
                            element3 = document.CreateElement("director")
                            element3.InnerText = movietosave.fullmoviebody.director
                            element6.AppendChild(element3)
                        Catch exception55 As Exception
                            ProjectData.SetProjectError(exception55)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H1D
                        Try 
                            element3 = document.CreateElement("studio")
                            element3.InnerText = movietosave.fullmoviebody.studio
                            element6.AppendChild(element3)
                        Catch exception56 As Exception
                            ProjectData.SetProjectError(exception56)
                            ProjectData.ClearProjectError
                        End Try
                        num = 30
                        Try 
                            element3 = document.CreateElement("trailer")
                            element3.InnerText = movietosave.fullmoviebody.trailer
                            element6.AppendChild(element3)
                        Catch exception57 As Exception
                            ProjectData.SetProjectError(exception57)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H1F
                        Try 
                            element3 = document.CreateElement("playcount")
                            element3.InnerText = movietosave.fullmoviebody.playcount
                            element6.AppendChild(element3)
                        Catch exception58 As Exception
                            ProjectData.SetProjectError(exception58)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H20
                        Try 
                            element3 = document.CreateElement("id")
                            element3.InnerText = movietosave.fullmoviebody.imdbid
                            element6.AppendChild(element3)
                        Catch exception59 As Exception
                            ProjectData.SetProjectError(exception59)
                            ProjectData.ClearProjectError
                        End Try
                        Try 
                            element3 = document.CreateElement("createdate")
                            If (movietosave.fileinfo.createdate = Nothing) Then
                                Dim now As DateTime = DateTime.Now
                                Try 
                                    element3.InnerText = Strings.Format(now, "yyyyMMddHHmmss").ToString
                                Catch exception60 As Exception
                                    ProjectData.SetProjectError(exception60)
                                    Dim exception6 As Exception = exception60
                                    ProjectData.ClearProjectError
                                End Try
                            ElseIf (movietosave.fileinfo.createdate = "") Then
                                Dim expression As DateTime = DateTime.Now
                                Try 
                                    element3.InnerText = Strings.Format(expression, "yyyyMMddHHmmss").ToString
                                Catch exception61 As Exception
                                    ProjectData.SetProjectError(exception61)
                                    Dim exception7 As Exception = exception61
                                    ProjectData.ClearProjectError
                                End Try
                            Else
                                element3.InnerText = movietosave.fileinfo.createdate
                            End If
                            element6.AppendChild(element3)
                        Catch exception62 As Exception
                            ProjectData.SetProjectError(exception62)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H21
                        Try 
                            Dim count As Integer = movietosave.listactors.Count
                            If (count > Module1.userprefs.maxactors) Then
                                count = Module1.userprefs.maxactors
                            End If
                            Dim num5 As Integer = (count - 1)
                            Dim i As Integer = 0
                            Do While (i <= num5)
                                element3 = document.CreateElement("actor")
                                Dim element As XmlElement = document.CreateElement("name")
                                Dim movieactors As movieactors = movietosave.listactors.Item(i)
                                element.InnerText = movieactors.actorname
                                element3.AppendChild(element)
                                element = document.CreateElement("role")
                                movieactors = movietosave.listactors.Item(i)
                                element.InnerText = movieactors.actorrole
                                element3.AppendChild(element)
                                movieactors = movietosave.listactors.Item(i)
                                If (movieactors.actorthumb <> Nothing) Then
                                    Dim movieactors2 As movieactors = movietosave.listactors.Item(i)
                                    If (movieactors2.actorthumb <> "") Then
                                        element = document.CreateElement("thumb")
                                        movieactors2 = movietosave.listactors.Item(i)
                                        element.InnerText = movieactors2.actorthumb
                                        element3.AppendChild(element)
                                    End If
                                End If
                                element6.AppendChild(element3)
                                i += 1
                            Loop
                            document.AppendChild(element6)
                        Catch exception63 As Exception
                            ProjectData.SetProjectError(exception63)
                            ProjectData.ClearProjectError
                        End Try
                        num = &H22
                        Try 
                            Dim w As New XmlTextWriter(filenameandpath, Encoding.UTF8) With { _
                                .Formatting = Formatting.Indented _
                            }
                            num = &H23
                            document.WriteTo(w)
                            w.Close
                            Return
                        Catch exception64 As Exception
                            ProjectData.SetProjectError(exception64)
                            ProjectData.ClearProjectError
                            Return
                        End Try
                    End If
                    Interaction.MsgBox("File already exists", MsgBoxStyle.OkOnly, Nothing)
                End If
            Catch exception65 As Exception
                ProjectData.SetProjectError(exception65)
                Interaction.MsgBox(("Error Encountered at stage " & num.ToString & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10) & exception65.ToString), MsgBoxStyle.OkOnly, Nothing)
                ProjectData.ClearProjectError
            End Try
        End Sub

        Private Shared Sub savetvcache()
            Dim tvcache As String = Module1.workingprofile.tvcache
            If File.Exists(tvcache) Then
                File.Delete(tvcache)
            End If
            Dim document As New XmlDocument
            Dim newChild As XmlDeclaration = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            document.AppendChild(newChild)
            Dim element3 As XmlElement = document.CreateElement("tv_cache")
            Dim basictvshownfo As basictvshownfo
            For Each basictvshownfo In Module1.basictvlist
                Dim element As XmlElement = document.CreateElement("tvshow")
                Dim element2 As XmlElement = document.CreateElement("title")
                element2.InnerText = basictvshownfo.title
                element.AppendChild(element2)
                element2 = document.CreateElement("fullpathandfilename")
                element2.InnerText = basictvshownfo.fullpath
                element.AppendChild(element2)
                element2 = document.CreateElement("genre")
                element2.InnerText = basictvshownfo.genre
                element.AppendChild(element2)
                element2 = document.CreateElement("imdbid")
                element2.InnerText = basictvshownfo.imdbid
                element.AppendChild(element2)
                element2 = document.CreateElement("tvdbid")
                element2.InnerText = basictvshownfo.tvdbid
                element.AppendChild(element2)
                element2 = document.CreateElement("rating")
                element2.InnerText = basictvshownfo.rating
                element.AppendChild(element2)
                element.AppendChild(element2)
                element2 = document.CreateElement("year")
                element2.InnerText = basictvshownfo.year
                element.AppendChild(element2)
                element2 = document.CreateElement("language")
                element2.InnerText = basictvshownfo.language
                element.AppendChild(element2)
                element2 = document.CreateElement("status")
                element2.InnerText = basictvshownfo.status
                element.AppendChild(element2)
                element2 = document.CreateElement("sortorder")
                element2.InnerText = basictvshownfo.sortorder
                element.AppendChild(element2)
                element2 = document.CreateElement("episodeactorsource")
                element2.InnerText = basictvshownfo.episodeactorsource
                element.AppendChild(element2)
                element2 = document.CreateElement("locked")
                element2.InnerText = basictvshownfo.locked.ToString.ToLower
                element.AppendChild(element2)
                Dim basicepisodenfo As basicepisodenfo
                For Each basicepisodenfo In basictvshownfo.allepisodes
                    element2 = document.CreateElement("episode")
                    Dim element4 As XmlElement = document.CreateElement("title")
                    element4.InnerText = basicepisodenfo.title
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("episodepath")
                    element4.InnerText = basicepisodenfo.episodepath
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("seasonno")
                    element4.InnerText = basicepisodenfo.seasonno
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("episodeno")
                    element4.InnerText = basicepisodenfo.episodeno
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("playcount")
                    element4.InnerText = basicepisodenfo.playcount
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("rating")
                    element4.InnerText = basicepisodenfo.rating
                    element2.AppendChild(element4)
                    element4 = document.CreateElement("tvdbid")
                    element4.InnerText = basicepisodenfo.tvdbid
                    element2.AppendChild(element4)
                    element.AppendChild(element2)
                Next
                element3.AppendChild(element)
            Next
            document.AppendChild(element3)
            Dim w As New XmlTextWriter(tvcache, Encoding.UTF8) With { _
                .Formatting = Formatting.Indented _
            }
            document.WriteTo(w)
            w.Close
        End Sub

        Private Shared Sub setuphtml(ByVal name As String)
            Dim htmltemplate As htmltemplate
            For Each htmltemplate In Module1.templatelist
                If (htmltemplate.title = name) Then
                    Try 
                        Dim str As String = File.OpenText(htmltemplate.path).ReadToEnd
                        If ((str.ToLower.IndexOf("<<mc html page>>") <> -1) And (str.ToLower.IndexOf("<</mc html page>>") <> -1)) Then
                            Module1.fullhtmlstring = str
                        Else
                            Module1.fullhtmlstring = Nothing
                        End If
                        Exit For
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        Dim exception As Exception = exception1
                        ProjectData.ClearProjectError
                        Exit For
                    End Try
                End If
            Next
            Module1.htmloutput
        End Sub

        Public Shared Sub setuppreferences()
            Module1.userprefs.tvshowautoquick = False
            Module1.userprefs.actorseasy = True
            Module1.userprefs.copytvactorthumbs = True
            Module1.userprefs.startuptab = 0
            Module1.userprefs.font = "Times New Roman, 9pt"
            Module1.userprefs.moviedefaultlist = 0
            Module1.userprefs.moviesortorder = 0
            Module1.userprefs.enabletvhdtags = True
            Module1.userprefs.tvshowrefreshlog = False
            Module1.userprefs.seasonall = "none"
            Module1.userprefs.tvrename = 0
            Module1.userprefs.externalbrowser = False
            Module1.userprefs.videoplaybackmode = Conversions.ToInteger("1")
            Module1.userprefs.backgroundcolour = "Silver"
            Module1.userprefs.forgroundcolour = "#D3D9DC"
            Module1.userprefs.formheight = Conversions.ToInteger("600")
            Module1.userprefs.formwidth = Conversions.ToInteger("800")
            Module1.userprefs.fanartnotstacked = False
            Module1.userprefs.posternotstacked = False
            Module1.userprefs.disablelogfiles = False
            Module1.userprefs.startupcache = True
            Module1.userprefs.rarsize = CInt(-(Conversions.ToBoolean("8") > False))
            Module1.userprefs.ignoreactorthumbs = False
            Module1.userprefs.actorsave = False
            Module1.userprefs.actorsavepath = ""
            Module1.userprefs.actornetworkpath = ""
            Module1.userprefs.tvfanart = True
            Module1.userprefs.tvposter = True
            Module1.userprefs.postertype = "poster"
            Module1.userprefs.downloadtvseasonthumbs = True
            Module1.userprefs.usefanart = True
            Module1.userprefs.ignoretrailers = False
            Module1.userprefs.keepfoldername = False
            Module1.userprefs.enablehdtags = True
            Module1.userprefs.renamenfofiles = False
            Module1.userprefs.checkinfofiles = True
            Module1.userprefs.savefanart = True
            Module1.userprefs.scrapemovieposters = True
            Module1.userprefs.dontdisplayposter = False
            Module1.userprefs.resizefanart = 1
            Module1.userprefs.overwritethumbs = False
            Module1.userprefs.startupmode = 1
            Module1.userprefs.maxactors = &H270F
            Module1.userprefs.maxmoviegenre = &H63
            Module1.userprefs.usetransparency = False
            Module1.userprefs.transparencyvalue = &HFF
            Module1.userprefs.defaulttvthumb = "poster"
            Module1.userprefs.imdbmirror = "http://www.imdb.com/"
            Module1.userprefs.usefoldernames = False
            Module1.userprefs.createfolderjpg = False
            Module1.userprefs.basicsavemode = False
            Module1.userprefs.namemode = "1"
            Module1.userprefs.tvdblanguage = "English"
            Module1.userprefs.tvdblanguagecode = "en"
            Module1.userprefs.sortorder = "default"
            Module1.userprefs.tvdbactorscrape = 0
            Module1.userprefs.maximumthumbs = 10
            Module1.userprefs.gettrailer = False
            Module1.userprefs.certificatepriority = New String() { "MPAA", "UK", "USA", "Ireland", "Australia", "New Zealand", "Norway", "Singapore", "South Korea", "Philippines", "Brazil", "Netherlands", "Malaysia", "Argentina", "Iceland", "Canada (Quebec)", "Canada (British Columbia/Ontario)", "Canada (Alberta/Manitoba/Nova Scotia)", "Peru", "Sweden", "Portugal", "South Africa", "Denmark", "Hong Kong", "Finland", "India", "Mexico", "France", "Italy", "Switzerland (canton of Vaud)", "Switzerland (canton of Geneva)", "Germany", "Greece", "Austria" }
            Module1.userprefs.moviethumbpriority = New String() { "Internet Movie Poster Awards", "themoviedb.org", "Movie Poster DB", "IMDB" }
            Module1.userprefs.maximagecount = 10
            Module1.userprefs.videomode = 1
            Module1.userprefs.locx = 0
            Module1.userprefs.locy = 0
            Module1.userprefs.formheight = &H2D5
            Module1.userprefs.formwidth = &H424
            Module1.moviefolders.Clear
            Module1.tvfolders.Clear
            Module1.userprefs.splt5 = 0
        End Sub

        Private Shared Sub startnewmovies()
            Dim index As Integer
            Dim duration As String
            Dim list As New List(Of String)
            Dim num As Integer = 0
            Module1.newmovielist.Clear
            Dim list2 As New List(Of String)
            Dim num5 As Integer = 0
            Console.WriteLine("Starting Folder Scan")
            Dim strArray As String() = New String(&H65  - 1) {}
            strArray(1) = "*.avi"
            strArray(2) = "*.xvid"
            strArray(3) = "*.divx"
            strArray(4) = "*.img"
            strArray(5) = "*.mpg"
            strArray(6) = "*.mpeg"
            strArray(7) = "*.mov"
            strArray(8) = "*.rm"
            strArray(9) = "*.3gp"
            strArray(10) = "*.m4v"
            strArray(11) = "*.wmv"
            strArray(12) = "*.asf"
            strArray(13) = "*.mp4"
            strArray(14) = "*.mkv"
            strArray(15) = "*.nrg"
            strArray(&H10) = "*.iso"
            strArray(&H11) = "*.rmvb"
            strArray(&H12) = "*.ogm"
            strArray(&H13) = "*.bin"
            strArray(20) = "*.ts"
            strArray(&H15) = "*.vob"
            strArray(&H16) = "*.m2ts"
            strArray(&H17) = "*.rar"
            strArray(&H18) = "VIDEO_TS.IFO"
            strArray(&H17) = ".strm"
            Dim num2 As Integer = &H18
            Dim str10 As String
            For Each str10 In Module1.moviefolders
                Dim info As New DirectoryInfo(str10)
                If info.Exists Then
                    Console.WriteLine(("found" & info.FullName.ToString))
                    list2.Add(str10)
                    Console.WriteLine("Checking for subfolders")
                    Try 
                        Dim enumerator As IEnumerator(Of String)
                        Dim list3 As List(Of String) = DirectCast(Module1.EnumerateDirectory2(str10, False), List(Of String))
                        Try 
                            enumerator = list3.GetEnumerator
                            Do While enumerator.MoveNext
                                Dim current As String = enumerator.Current
                                Console.WriteLine(("Subfolder added :- " & current.ToString))
                                list2.Add(current)
                            Loop
                            Continue For
                        Finally
                            enumerator.Dispose
                        End Try
                    Catch exception1 As Exception
                        ProjectData.SetProjectError(exception1)
                        ProjectData.ClearProjectError
                        Continue For
                    End Try
                End If
            Next
            Dim str12 As String
            For Each str12 In Module1.userprefs.offlinefolders
                Dim info2 As New DirectoryInfo(str12)
                If info2.Exists Then
                    Console.WriteLine(("found" & info2.FullName.ToString))
                    Console.WriteLine("Checking for subfolders")
                    Try 
                        Dim enumerator4 As IEnumerator(Of String)
                        Dim list4 As List(Of String) = DirectCast(Module1.EnumerateDirectory3(str12), List(Of String))
                        Try 
                            enumerator4 = list4.GetEnumerator
                            Do While enumerator4.MoveNext
                                Dim str15 As String = enumerator4.Current
                                Console.WriteLine(("Subfolder added :- " & str15.ToString))
                                Dim str16 As String = (Module1.getlastfolder((str15 & "\whatever")) & ".avi")
                                Dim sPath As String = Path.Combine(str15, str16)
                                If Not File.Exists(sPath.Replace(Path.GetExtension(sPath), ".nfo")) Then
                                    If Not File.Exists(Path.Combine(str15, "tempoffline.ttt")) Then
                                        Using fs As New FileStream(Path.Combine(str15, "tempoffline.ttt"), FileMode.Create)
                                        End Using
                                    End If
                                    If Not File.Exists(sPath) Then
                                        Dim str19 As String = (Module1.getlastfolder((str15 & "\whatever")) & ".avi")
                                        Using fs As New FileStream(Path.Combine(str15, str19), FileMode.Create)

                                        End Using
                                    End If
                                    list2.Add(str15)
                                End If
                            Loop
                            Continue For
                        Finally
                            enumerator4.Dispose
                        End Try
                    Catch exception20 As Exception
                        ProjectData.SetProjectError(exception20)
                        ProjectData.ClearProjectError
                        Continue For
                    End Try
                End If
            Next
            Dim count As Integer = Module1.newmovielist.Count
            Dim num32 As Integer = (list2.Count - 1)
            Dim i As Integer = 0
            Do While (i <= num32)
                Try 
                    Dim str2 As String
                    Dim num33 As Integer = num2
                    Dim k As Integer = 1
                    Do While (k <= num33)
                        Dim str As String
                        Dim pattern As String = strArray(k)
                        str2 = list2.Item(i)
                        Dim info3 As New DirectoryInfo(str2)
                        Module1.Listmoviefiles(str, pattern, info3)
                        k += 1
                    Loop
                    index = (Module1.newmovielist.Count - count)
                    Console.WriteLine((index.ToString & " New movies found in directory:- " & str2))
                    count = Module1.newmovielist.Count
                Catch exception21 As Exception
                    ProjectData.SetProjectError(exception21)
                    Dim exception As Exception = exception21
                    ProjectData.ClearProjectError
                End Try
                i += 1
            Loop
            Console.WriteLine((Conversions.ToString(Module1.newmovielist.Count) & " Movies found in all folders"))
            Console.WriteLine("Obtaining Title for each movie found, from path and filename")
            Dim newmovie As newmovie
            For Each newmovie In Module1.newmovielist
                Try 
                    Dim extension As String = Path.GetExtension(newmovie.nfopathandfilename)
                    Dim fileName As String = Path.GetFileName(newmovie.nfopathandfilename)
                    Console.WriteLine("")
                    newmovie.nfopath = newmovie.nfopathandfilename.Replace(fileName, "")
                    newmovie.title = fileName.Replace(extension, "")
                    If (extension.ToLower <> ".ifo") Then
                        Try 
                            newmovie.nfopathandfilename = newmovie.nfopathandfilename.Replace(extension, ".nfo")
                        Catch exception22 As Exception
                            ProjectData.SetProjectError(exception22)
                            Console.WriteLine("Unable to get movie title, stage1")
                            Console.WriteLine(("Path is: " & newmovie.nfopathandfilename))
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If ((extension.ToLower = ".ifo") Or Module1.userprefs.usefoldernames) Then
                        Try 
                            newmovie.nfopathandfilename = newmovie.nfopathandfilename.Replace(extension, ".nfo")
                            newmovie.title = Module1.getlastfolder(newmovie.nfopathandfilename)
                        Catch exception23 As Exception
                            ProjectData.SetProjectError(exception23)
                            Console.WriteLine("Unable to get movie title, stage2")
                            Console.WriteLine(("Path is: " & newmovie.nfopathandfilename))
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If ((newmovie.title <> Nothing) AndAlso (newmovie.title <> "")) Then
                        duration = Conversions.ToString(Module1.cleanfilename(newmovie.title, False))
                        Select Case duration
                            Case Nothing
                                Console.WriteLine(("Cleaning title returns nothing: " & newmovie.title))
                                goto Label_0671
                            Case ""
                                Console.WriteLine(("Cleaning title returns blank: " & newmovie.title))
                                goto Label_0671
                        End Select
                        If (duration <> "error") Then
                            newmovie.title = duration
                        Else
                            Console.WriteLine(("Unable to clean title: " & newmovie.title))
                        End If
                    End If
                Label_0671:
                    Console.WriteLine(("Filename is: " & newmovie.mediapathandfilename))
                    Console.WriteLine(("Title according to settings is: """ & newmovie.title & """"))
                    Continue For
                Catch exception24 As Exception
                    ProjectData.SetProjectError(exception24)
                    ProjectData.ClearProjectError
                    Continue For
                End Try
            Next
            Dim num4 As Integer = Conversions.ToInteger(Module1.newmovielist.Count.ToString)
            Console.WriteLine("Starting Main Scraper Process")
            Dim num35 As Integer = (Module1.newmovielist.Count - 1)
            Dim j As Integer = 0
            Do While (j <= num35)
                Dim str6 As String
                Dim match As Match
                Dim str28 As String
                Dim newmovie3 As newmovie
                Dim filename As String = ""
                Dim fullpath As String = ""
                Dim str21 As String = ""
                Dim str23 As String = ""
                Dim document As New XmlDocument
                num5 = CInt(Math.Round(CDbl((((100 / CDbl(num4)) * (j + 1)) * 10))))
                Dim str7 As String = String.Concat(New String() { ("Scraping Movie " & Conversions.ToString(CInt((j + 1))) & " of " & Conversions.ToString(num4)) })
                Dim newmovie2 As newmovie = Module1.newmovielist.Item(j)
                If (newmovie2.title = Nothing) Then
                    newmovie3 = Module1.newmovielist.Item(j)
                    Console.WriteLine(("No Filename found for" & newmovie3.nfopathandfilename))
                End If
                Dim imdbid As String = Nothing
                newmovie3 = Module1.newmovielist.Item(j)
                If (newmovie3.title = Nothing) Then
                    goto Label_364C
                End If
                newmovie2 = Module1.newmovielist.Item(j)
                filename = newmovie2.title
                newmovie3 = Module1.newmovielist.Item(j)
                Console.WriteLine(("Scraping Title:- " & newmovie3.title))
                newmovie3 = Module1.newmovielist.Item(j)
                fullpath = newmovie3.nfopathandfilename
                If Module1.userprefs.basicsavemode Then
                    newmovie3 = Module1.newmovielist.Item(j)
                    newmovie2 = Module1.newmovielist.Item(j)
                    fullpath = newmovie3.nfopathandfilename.Replace(Path.GetFileName(newmovie2.nfopathandfilename), "movie.nfo")
                End If
                Console.WriteLine(("Output filename:- " & fullpath))
                str23 = Module1.getposterpath(fullpath)
                str21 = Module1.getfanartpath(fullpath)
                Console.WriteLine(("Poster Path:- " & str23))
                Console.WriteLine(("Fanart Path:- " & str21))
                imdbid = Nothing
                If File.Exists(fullpath) Then
                    Console.WriteLine("nfo file exists, checking for IMDB ID")
                    Dim str29 As String = ""
                    Dim reader As New StreamReader(fullpath)
                    str29 = reader.ReadToEnd
                    reader.Close
                    imdbid = Nothing
                    str28 = str29
                    match = Nothing
                    match = Regex.Match(str28, "(tt\d{7})")
                    If match.Success Then
                        Console.WriteLine(("IMDB ID found in nfo file:- " & match.Value))
                        imdbid = match.Value
                    Else
                        Console.WriteLine("No IMDB ID found")
                        imdbid = Nothing
                    End If
                    Try 
                        If Not File.Exists(fullpath.Replace(".nfo", ".info")) Then
                            File.Move(fullpath, fullpath.Replace(".nfo", ".info"))
                            Console.WriteLine(("renaming nfo file to:- " & fullpath.Replace(".nfo", ".info")))
                        Else
                            Console.WriteLine(("Unable to rename file, """ & fullpath & """ already exists"))
                        End If
                    Catch exception25 As Exception
                        ProjectData.SetProjectError(exception25)
                        Console.WriteLine(("Unable to rename file, """ & fullpath & """ already exists"))
                        ProjectData.ClearProjectError
                    End Try
                Else
                    Dim str30 As String = (Module1.getstackname(fullpath, fullpath.Replace(Path.GetFileName(fullpath), "")) & ".nfo")
                    If File.Exists(str30) Then
                        Console.WriteLine("nfo file exists, checking for IMDB ID")
                        Dim str32 As String = ""
                        Dim reader2 As New StreamReader(str30)
                        str32 = reader2.ReadToEnd
                        reader2.Close
                        imdbid = Nothing
                        str28 = str32
                        match = Nothing
                        match = Regex.Match(str28, "(tt\d{7})")
                        If match.Success Then
                            Console.WriteLine(("IMDB ID found in nfo file:- " & match.Value))
                            imdbid = match.Value
                        Else
                            Console.WriteLine("No IMDB ID found")
                            imdbid = Nothing
                        End If
                    Else
                        Console.WriteLine("NFO does not exist")
                    End If
                End If
                If (imdbid = Nothing) Then
                    Console.WriteLine("Checking filename for IMDB ID")
                    match = Nothing
                    newmovie3 = Module1.newmovielist.Item(j)
                    match = Regex.Match(newmovie3.nfopathandfilename, "(tt\d{7})")
                    If match.Success Then
                        Console.WriteLine(("IMDB ID found in filename:- " & match.Value))
                        imdbid = match.Value
                    Else
                        imdbid = Nothing
                        Console.WriteLine("No IMDB ID found")
                    End If
                End If
                If (imdbid = Nothing) Then
                    Console.WriteLine("Checking for Movie year in filename")
                    If (imdbid = Nothing) Then
                        newmovie3 = Module1.newmovielist.Item(j)
                        Dim match2 As Match = Regex.Match(newmovie3.nfopathandfilename, "(\([\d]{4}\))")
                        If match2.Success Then
                            str6 = match2.Value
                        Else
                            str6 = Nothing
                        End If
                        If (str6 = Nothing) Then
                            newmovie3 = Module1.newmovielist.Item(j)
                            match2 = Regex.Match(newmovie3.nfopathandfilename, "(\[[\d]{4}\])")
                            If match2.Success Then
                                str6 = match2.Value
                            Else
                                str6 = Nothing
                            End If
                        End If
                    End If
                    If (str6 = Nothing) Then
                        Console.WriteLine("No year found in filename")
                    Else
                        str6 = str6.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "")
                        Console.WriteLine(("Year found for movie:- " & str6.ToString))
                    End If
                End If
                filename = Conversions.ToString(Module1.cleanfilename(filename, False))
                Console.WriteLine(("Cleaned Title for search :- " & filename))
                Dim movietosave As New fullmoviedetails
                Dim classimdbscraper As New Classimdbscraper
                Dim list5 As New List(Of String)
                Module1.imdbcounter += 1
                Dim xml As String = Conversions.ToString(classimdbscraper.getimdbbody(filename, str6, imdbid, Module1.userprefs.imdbmirror, Module1.imdbcounter))
                Dim node As XmlNode = Nothing
                If (xml = "MIC") Then
                    Console.WriteLine(String.Concat(New String() { "Unable to scrape body with refs """, filename, """, """, str6, """, """, imdbid, """, """, Module1.userprefs.imdbmirror, """" }))
                    If (Module1.imdbcounter < 50) Then
                        Console.WriteLine("Searching using Google")
                    Else
                        Console.WriteLine("Google session limit reached, Searching using IMDB")
                    End If
                    Console.WriteLine("To add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie")
                    movietosave.fullmoviebody.genre = "problem"
                    If (movietosave.fullmoviebody.title = Nothing) Then
                        movietosave.fullmoviebody.title = "Unknown Title"
                    End If
                    If (movietosave.fullmoviebody.title = "") Then
                        movietosave.fullmoviebody.title = "Unknown Title"
                    End If
                    If (movietosave.fullmoviebody.year = Nothing) Then
                        movietosave.fullmoviebody.year = "0000"
                    End If
                    If (movietosave.fullmoviebody.rating = Nothing) Then
                        movietosave.fullmoviebody.rating = "0"
                    End If
                    If (movietosave.fullmoviebody.top250 = Nothing) Then
                        movietosave.fullmoviebody.top250 = "0"
                    End If
                    If (movietosave.fullmoviebody.playcount = Nothing) Then
                        movietosave.fullmoviebody.playcount = "0"
                    End If
                    If (movietosave.fullmoviebody.title = "Unknown Title") Then
                        movietosave.fullmoviebody.plot = "This Movie has could not be identified by Media Companion, to add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie"
                        If (filename = Nothing) Then
                            filename = "Unknown Title"
                        ElseIf (filename = "") Then
                            filename = "Unknown Title"
                        End If
                        movietosave.fullmoviebody.title = filename
                    End If
                    If (movietosave.fullmoviebody.title = "Unknown Title") Then
                        movietosave.fullmoviebody.genre = "Problem"
                    End If
                    Dim expression As DateTime = DateTime.Now
                    Try 
                        movietosave.fileinfo.createdate = Strings.Format(expression, "yyyyMMddHHmmss").ToString
                    Catch exception26 As Exception
                        ProjectData.SetProjectError(exception26)
                        Dim exception2 As Exception = exception26
                        ProjectData.ClearProjectError
                    End Try
                    Module1.savemovienfo(fullpath, movietosave, True)
                    Dim combolist As New combolist With { _
                        .fullpathandfilename = fullpath _
                    }
                    newmovie3 = Module1.newmovielist.Item(j)
                    combolist.filename = Path.GetFileName(newmovie3.nfopathandfilename)
                    newmovie3 = Module1.newmovielist.Item(j)
                    combolist.foldername = Module1.getlastfolder(newmovie3.nfopathandfilename)
                    combolist.title = movietosave.fullmoviebody.title
                    If (movietosave.fullmoviebody.title <> Nothing) Then
                        If (movietosave.fullmoviebody.year <> Nothing) Then
                            combolist.titleandyear = (movietosave.fullmoviebody.title & " (" & movietosave.fullmoviebody.year & ")")
                        Else
                            combolist.titleandyear = (movietosave.fullmoviebody.title & " (0000)")
                        End If
                    Else
                        combolist.titleandyear = "Unknown (0000)"
                    End If
                    combolist.year = movietosave.fullmoviebody.year
                    newmovie3 = Module1.newmovielist.Item(j)
                    Dim info4 As New FileInfo(newmovie3.nfopathandfilename)
                    Dim time As DateTime = info4.LastWriteTime
                    Try 
                        combolist.filedate = Strings.Format(time, "yyyyMMddHHmmss").ToString
                    Catch exception27 As Exception
                        ProjectData.SetProjectError(exception27)
                        Dim exception3 As Exception = exception27
                        ProjectData.ClearProjectError
                    End Try
                    expression = DateTime.Now
                    Try 
                        combolist.createdate = Strings.Format(expression, "yyyyMMddHHmmss").ToString
                    Catch exception28 As Exception
                        ProjectData.SetProjectError(exception28)
                        Dim exception4 As Exception = exception28
                        ProjectData.ClearProjectError
                    End Try
                    combolist.sortorder = movietosave.fullmoviebody.title
                    combolist.outline = movietosave.fullmoviebody.outline
                    combolist.id = movietosave.fullmoviebody.imdbid
                    combolist.rating = movietosave.fullmoviebody.rating
                    combolist.top250 = movietosave.fullmoviebody.top250
                    combolist.genre = movietosave.fullmoviebody.genre
                    combolist.playcount = movietosave.fullmoviebody.playcount
                    combolist.missingdata1 = 3
                    combolist.runtime = "0"
                    Module1.fullmovielist.Add(combolist)
                    goto Label_3642
                End If
                Try 
                    Dim enumerator6 As IEnumerator
                    Console.WriteLine("Movie Body Scraped OK")
                    document.LoadXml(xml)
                    Try 
                        enumerator6 = document.Item("movie").GetEnumerator
                        Do While enumerator6.MoveNext
                            node = DirectCast(enumerator6.Current, XmlNode)
                            Dim name As String = node.Name
                            Select Case name
                                Case "title"
                                    If Not Module1.userprefs.keepfoldername Then
                                        movietosave.fullmoviebody.title = node.InnerText
                                    ElseIf Not Module1.userprefs.usefoldernames Then
                                        newmovie3 = Module1.newmovielist.Item(j)
                                        duration = Path.GetFileName(newmovie3.nfopathandfilename)
                                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(duration, False))
                                    Else
                                        newmovie3 = Module1.newmovielist.Item(j)
                                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(Module1.getlastfolder(newmovie3.nfopathandfilename), False))
                                    End If
                                    Continue Do
                                Case "alternativetitle"
                                    movietosave.alternativetitles.Add(node.InnerText)
                                    Continue Do
                                Case "credits"
                                    movietosave.fullmoviebody.credits = node.InnerText
                                    Continue Do
                                Case "director"
                                    movietosave.fullmoviebody.director = node.InnerText
                                    Continue Do
                                Case "genre"
                                    Dim strArray2 As String() = node.InnerText.Split(New Char() { "/"c })
                                    Dim num36 As Integer = (strArray2.Length - 1)
                                    Dim n As Integer = 0
                                    Do While (n <= num36)
                                        strArray2(n) = strArray2(n).Replace(" ", "")
                                        n += 1
                                    Loop
                                    If (strArray2.Length <= Module1.userprefs.maxmoviegenre) Then
                                        movietosave.fullmoviebody.genre = node.InnerText
                                    Else
                                        Dim num37 As Integer = (Module1.userprefs.maxmoviegenre - 1)
                                        Dim num13 As Integer = 0
                                        Do While (num13 <= num37)
                                            If (num13 = 0) Then
                                                movietosave.fullmoviebody.genre = strArray2(num13)
                                            Else
                                                Dim fullmoviedetails2 As fullmoviedetails = movietosave
                                                fullmoviedetails2.fullmoviebody.genre = (fullmoviedetails2.fullmoviebody.genre & " / " & strArray2(num13))
                                            End If
                                            num13 += 1
                                        Loop
                                    End If
                                    Continue Do
                            End Select
                            If (name = "mpaa") Then
                                movietosave.fullmoviebody.mpaa = node.InnerText
                            Else
                                If (name = "outline") Then
                                    movietosave.fullmoviebody.outline = node.InnerText
                                    Continue Do
                                End If
                                If (name = "plot") Then
                                    movietosave.fullmoviebody.plot = node.InnerText
                                    Continue Do
                                End If
                                If (name = "premiered") Then
                                    movietosave.fullmoviebody.premiered = node.InnerText
                                    Continue Do
                                End If
                                If (name = "rating") Then
                                    movietosave.fullmoviebody.rating = node.InnerText
                                    Continue Do
                                End If
                                If (name = "runtime") Then
                                    movietosave.fullmoviebody.runtime = node.InnerText
                                    Continue Do
                                End If
                                If (name = "studio") Then
                                    movietosave.fullmoviebody.studio = node.InnerText
                                    Continue Do
                                End If
                                If (name = "tagline") Then
                                    movietosave.fullmoviebody.tagline = node.InnerText
                                    Continue Do
                                End If
                                If (name = "top250") Then
                                    movietosave.fullmoviebody.top250 = node.InnerText
                                    Continue Do
                                End If
                                If (name = "votes") Then
                                    movietosave.fullmoviebody.votes = node.InnerText
                                    Continue Do
                                End If
                                If (name = "year") Then
                                    movietosave.fullmoviebody.year = node.InnerText
                                    Continue Do
                                End If
                                If (name = "id") Then
                                    movietosave.fullmoviebody.imdbid = node.InnerText
                                    Continue Do
                                End If
                                If (name = "cert") Then
                                    list5.Add(node.InnerText)
                                End If
                            End If
                        Loop
                    Finally
                        If TypeOf enumerator6 Is IDisposable Then
                            TryCast(enumerator6,IDisposable).Dispose
                        End If
                    End Try
                Catch exception29 As Exception
                    ProjectData.SetProjectError(exception29)
                    Dim exception5 As Exception = exception29
                    newmovie3 = Module1.newmovielist.Item(j)
                    Console.WriteLine(("Error with " & newmovie3.nfopathandfilename))
                    Console.WriteLine("An error was encountered at stage 1, Downloading Movie Body")
                    Console.WriteLine(exception5.Message.ToString)
                    num += 1
                    If Not Module1.userprefs.usefoldernames Then
                        newmovie3 = Module1.newmovielist.Item(j)
                        duration = Path.GetFileName(newmovie3.nfopathandfilename)
                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(duration, False))
                    Else
                        newmovie3 = Module1.newmovielist.Item(j)
                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(Module1.getlastfolder(newmovie3.nfopathandfilename), False))
                    End If
                    ProjectData.ClearProjectError
                End Try
                If (movietosave.fullmoviebody.playcount = Nothing) Then
                    movietosave.fullmoviebody.playcount = "0"
                End If
                If (movietosave.fullmoviebody.top250 = Nothing) Then
                    movietosave.fullmoviebody.top250 = "0"
                End If
                Dim flag2 As Boolean = False
                Dim num38 As Integer = Information.UBound(Module1.userprefs.certificatepriority, 1)
                Dim m As Integer = 0
                Do While (m <= num38)
                    Dim str35 As String
                    For Each str35 In list5
                        If (str35.IndexOf(Module1.userprefs.certificatepriority(m)) <> -1) Then
                            movietosave.fullmoviebody.mpaa = str35.Substring((str35.IndexOf("|") + 1), ((str35.Length - str35.IndexOf("|")) - 1))
                            flag2 = True
                            Exit For
                        End If
                    Next
                    If flag2 Then
                        Exit Do
                    End If
                    m += 1
                Loop
                If Module1.userprefs.keepfoldername Then
                    If Not Module1.userprefs.usefoldernames Then
                        newmovie3 = Module1.newmovielist.Item(j)
                        duration = Path.GetFileName(newmovie3.nfopathandfilename)
                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(duration, True))
                    Else
                        newmovie3 = Module1.newmovielist.Item(j)
                        movietosave.fullmoviebody.title = Conversions.ToString(Module1.cleanfilename(Module1.getlastfolder(newmovie3.nfopathandfilename), True))
                    End If
                End If
                Dim str26 As String = Conversions.ToString(classimdbscraper.getimdbactors(Module1.userprefs.imdbmirror, movietosave.fullmoviebody.imdbid, movietosave.fullmoviebody.title, Module1.userprefs.maxactors))
                Try 
                    Dim enumerator8 As IEnumerator
                    Dim enumerator10 As IEnumerator(Of movieactors)
                    document.LoadXml(str26)
                    node = Nothing
                    Dim num15 As Integer = 0
                    Try 
                        enumerator8 = document.Item("actorlist").GetEnumerator
                        Do While enumerator8.MoveNext
                            node = DirectCast(enumerator8.Current, XmlNode)
                            If (node.Name = "actor") Then
                                Dim enumerator9 As IEnumerator
                                If (num15 > Module1.userprefs.maxactors) Then
                                    goto Label_1E09
                                End If
                                num15 += 1
                                Dim movieactors As New movieactors
                                Dim node2 As XmlNode = Nothing
                                Try 
                                    enumerator9 = node.ChildNodes.GetEnumerator
                                    Do While enumerator9.MoveNext
                                        node2 = DirectCast(enumerator9.Current, XmlNode)
                                        Dim str63 As String = node2.Name
                                        Select Case str63
                                            Case "name"
                                                movieactors.actorname = node2.InnerText
                                                Continue Do
                                            Case "role"
                                                movieactors.actorrole = node2.InnerText
                                                Continue Do
                                            Case "thumb"
                                                movieactors.actorthumb = node2.InnerText
                                                Continue Do
                                        End Select
                                        If ((str63 = "actorid") AndAlso (movieactors.actorthumb <> Nothing)) Then
                                            If ((node2.InnerText <> "") And Module1.userprefs.actorseasy) Then
                                                newmovie3 = Module1.newmovielist.Item(j)
                                                newmovie2 = Module1.newmovielist.Item(j)
                                                Dim str36 As String = (newmovie3.nfopathandfilename.Replace(Path.GetFileName(newmovie2.nfopathandfilename), "") & ".actors\")
                                                Dim info6 As New DirectoryInfo(str36)
                                                Dim flag5 As Boolean = False
                                                If Not info6.Exists Then
                                                    Try 
                                                        Directory.CreateDirectory(str36)
                                                        flag5 = True
                                                    Catch exception30 As Exception
                                                        ProjectData.SetProjectError(exception30)
                                                        Dim exception6 As Exception = exception30
                                                        ProjectData.ClearProjectError
                                                    End Try
                                                Else
                                                    flag5 = True
                                                End If
                                                If flag5 Then
                                                    Dim str37 As String = (movieactors.actorname.Replace(" ", "_") & ".tbn")
                                                    str37 = Path.Combine(str36, str37)
                                                    If Not File.Exists(str37) Then
                                                        Try 
                                                            Dim buffer As Byte() = New Byte(&H3D0901  - 1) {}
                                                            Dim num18 As Integer = 0
                                                            Dim offset As Integer = 0
                                                            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(movieactors.actorthumb), HttpWebRequest)
                                                            Dim responseStream As Stream = DirectCast(request.GetResponse, HttpWebResponse).GetResponseStream
                                                            Dim length As Integer = buffer.Length
                                                            Do While (length > 0)
                                                                num18 = responseStream.Read(buffer, offset, length)
                                                                If (num18 = 0) Then
                                                                    Exit Do
                                                                End If
                                                                length = (length - num18)
                                                                offset = (offset + num18)
                                                            Loop
                                                            Dim stream4 As New FileStream(str37, FileMode.OpenOrCreate, FileAccess.Write)
                                                            stream4.Write(buffer, 0, offset)
                                                            responseStream.Close
                                                            stream4.Close
                                                        Catch exception31 As Exception
                                                            ProjectData.SetProjectError(exception31)
                                                            ProjectData.ClearProjectError
                                                        End Try
                                                    End If
                                                End If
                                            End If
                                            If ((Module1.userprefs.actorsave And (node2.InnerText <> "")) And Not Module1.userprefs.actorseasy) Then
                                                Dim str40 As String = ""
                                                Dim actorsavepath As String = Module1.userprefs.actorsavepath
                                                Try 
                                                    duration = (actorsavepath & "\" & node2.InnerText.Substring((node2.InnerText.Length - 2), 2))
                                                    Dim info7 As New DirectoryInfo(duration)
                                                    If Not info7.Exists Then
                                                        Directory.CreateDirectory(duration)
                                                    End If
                                                    str40 = String.Concat(New String() { actorsavepath, "\", node2.InnerText.Substring((node2.InnerText.Length - 2), 2), "\", node2.InnerText, ".jpg" })
                                                    If Not File.Exists(str40) Then
                                                        Dim buffer2 As Byte() = New Byte(&H3D0901  - 1) {}
                                                        Dim num21 As Integer = 0
                                                        Dim num19 As Integer = 0
                                                        Dim request2 As HttpWebRequest = DirectCast(WebRequest.Create(movieactors.actorthumb), HttpWebRequest)
                                                        Dim stream5 As Stream = DirectCast(request2.GetResponse, HttpWebResponse).GetResponseStream
                                                        Dim num20 As Integer = buffer2.Length
                                                        Do While (num20 > 0)
                                                            num21 = stream5.Read(buffer2, num19, num20)
                                                            If (num21 = 0) Then
                                                                Exit Do
                                                            End If
                                                            num20 = (num20 - num21)
                                                            num19 = (num19 + num21)
                                                        Loop
                                                        Dim stream6 As New FileStream(str40, FileMode.OpenOrCreate, FileAccess.Write)
                                                        stream6.Write(buffer2, 0, num19)
                                                        stream5.Close
                                                        stream6.Close
                                                    End If
                                                    movieactors.actorthumb = Path.Combine(Module1.userprefs.actornetworkpath, node2.InnerText.Substring((node2.InnerText.Length - 2), 2))
                                                    If (Module1.userprefs.actornetworkpath.IndexOf("/") <> -1) Then
                                                        movieactors.actorthumb = String.Concat(New String() { Module1.userprefs.actornetworkpath, "/", node2.InnerText.Substring((node2.InnerText.Length - 2), 2), "/", node2.InnerText, ".jpg" })
                                                    Else
                                                        movieactors.actorthumb = String.Concat(New String() { Module1.userprefs.actornetworkpath, "\", node2.InnerText.Substring((node2.InnerText.Length - 2), 2), "\", node2.InnerText, ".jpg" })
                                                    End If
                                                    Continue Do
                                                Catch exception32 As Exception
                                                    ProjectData.SetProjectError(exception32)
                                                    ProjectData.ClearProjectError
                                                    Continue Do
                                                End Try
                                            End If
                                        End If
                                    Loop
                                Finally
                                    If TypeOf enumerator9 Is IDisposable Then
                                        TryCast(enumerator9,IDisposable).Dispose
                                    End If
                                End Try
                                movietosave.listactors.Add(movieactors)
                            End If
                        Loop
                    Finally
                        If TypeOf enumerator8 Is IDisposable Then
                            TryCast(enumerator8,IDisposable).Dispose
                        End If
                    End Try
                Label_1E09:
                    Console.WriteLine("Actors scraped OK")
                    Do While (movietosave.listactors.Count > Module1.userprefs.maxactors)
                        movietosave.listactors.RemoveAt((movietosave.listactors.Count - 1))
                    Loop
                    Try 
                        enumerator10 = movietosave.listactors.GetEnumerator
                        Do While enumerator10.MoveNext
                            Dim movieactors2 As movieactors = enumerator10.Current
                            Dim actordatabase As New actordatabase With { _
                                .actorname = movieactors2.actorname, _
                                .movieid = movietosave.fullmoviebody.imdbid _
                            }
                            Module1.actordb.Add(actordatabase)
                        Loop
                    Finally
                        enumerator10.Dispose
                    End Try
                Catch exception33 As Exception
                    ProjectData.SetProjectError(exception33)
                    Dim exception7 As Exception = exception33
                    newmovie3 = Module1.newmovielist.Item(j)
                    Console.WriteLine(("Error with " & newmovie3.nfopathandfilename))
                    Console.WriteLine("An error was encountered at stage 2, Downloading Actors")
                    Console.WriteLine(exception7.Message.ToString)
                    num += 1
                    movietosave.listactors.Clear
                    ProjectData.ClearProjectError
                End Try
                Try 
                    If Module1.userprefs.gettrailer Then
                        Dim str9 As String = Conversions.ToString(classimdbscraper.gettrailerurl(movietosave.fullmoviebody.imdbid, Module1.userprefs.imdbmirror))
                        If (str9 <> Nothing) Then
                            movietosave.fullmoviebody.trailer = str9
                            Console.WriteLine("Trailer URL Scraped OK")
                        End If
                    End If
                Catch exception34 As Exception
                    ProjectData.SetProjectError(exception34)
                    ProjectData.ClearProjectError
                End Try
                If (Module1.userprefs.nfoposterscraper <> 0) Then
                    Dim str42 As String = ""
                    If ((((((((Module1.userprefs.nfoposterscraper = 1) Or (Module1.userprefs.nfoposterscraper = 3)) Or (Module1.userprefs.nfoposterscraper = 5)) Or (Module1.userprefs.nfoposterscraper = 7)) Or (Module1.userprefs.nfoposterscraper = 9)) Or (Module1.userprefs.nfoposterscraper = 11)) Or (Module1.userprefs.nfoposterscraper = 13)) Or (Module1.userprefs.nfoposterscraper = 15)) Then
                        Dim getimpaposters As New getimpaposters
                        Dim document2 As New XmlDocument
                        Try 
                            Dim str43 As String = Conversions.ToString(getimpaposters.getimpathumbs(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year))
                            Dim str44 As String = ("<totalthumbs>" & str43 & "</totalthumbs>")
                            document2.LoadXml(str44)
                            str42 = (str42 & str43.ToString)
                        Catch exception35 As Exception
                            ProjectData.SetProjectError(exception35)
                            Dim exception8 As Exception = exception35
                            Thread.Sleep(1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If ((((((((Module1.userprefs.nfoposterscraper = 2) Or (Module1.userprefs.nfoposterscraper = 3)) Or (Module1.userprefs.nfoposterscraper = 6)) Or (Module1.userprefs.nfoposterscraper = 7)) Or (Module1.userprefs.nfoposterscraper = 10)) Or (Module1.userprefs.nfoposterscraper = 11)) Or (Module1.userprefs.nfoposterscraper = 14)) Or (Module1.userprefs.nfoposterscraper = 15)) Then
                        Dim class2 As New tmdb_posters.Class1
                        Dim document3 As New XmlDocument
                        Try 
                            Dim enumerator11 As IEnumerator
                            Dim str45 As String = Conversions.ToString(class2.gettmdbposters_newapi(movietosave.fullmoviebody.imdbid))
                            Dim document4 As New XmlDocument
                            document4.LoadXml(str45)
                            Dim str46 As String = ""
                            Try 
                                enumerator11 = document4.Item("tmdb_posterlist").GetEnumerator
                                Do While enumerator11.MoveNext
                                    Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator11.Current)
                                    If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, Nothing, "name", New Object(0  - 1) {}, Nothing, Nothing, Nothing), "poster", False) Then
                                        Dim enumerator12 As IEnumerator
                                        Try 
                                            enumerator12 = DirectCast(objectValue, IEnumerable).GetEnumerator
                                            Do While enumerator12.MoveNext
                                                Dim instance As Object = RuntimeHelpers.GetObjectValue(enumerator12.Current)
                                                If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(instance, Nothing, "childnodes", New Object() { 0 }, Nothing, Nothing, Nothing), Nothing, "innertext", New Object(0  - 1) {}, Nothing, Nothing, Nothing), "original", False) Then
                                                    str46 = Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject((str46 & "<thumbs>"), NewLateBinding.LateGet(NewLateBinding.LateGet(instance, Nothing, "childnodes", New Object() { 1 }, Nothing, Nothing, Nothing), Nothing, "innertext", New Object(0  - 1) {}, Nothing, Nothing, Nothing)), "</thumbs>"))
                                                End If
                                            Loop
                                            Continue Do
                                        Finally
                                            If TypeOf enumerator12 Is IDisposable Then
                                                TryCast(enumerator12,IDisposable).Dispose
                                            End If
                                        End Try
                                    End If
                                Loop
                            Finally
                                If TypeOf enumerator11 Is IDisposable Then
                                    TryCast(enumerator11,IDisposable).Dispose
                                End If
                            End Try
                            str42 = (str42 & str46.ToString)
                        Catch exception36 As Exception
                            ProjectData.SetProjectError(exception36)
                            Dim exception9 As Exception = exception36
                            Thread.Sleep(1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If ((((((((Module1.userprefs.nfoposterscraper = 4) Or (Module1.userprefs.nfoposterscraper = 5)) Or (Module1.userprefs.nfoposterscraper = 6)) Or (Module1.userprefs.nfoposterscraper = 7)) Or (Module1.userprefs.nfoposterscraper = 12)) Or (Module1.userprefs.nfoposterscraper = 13)) Or (Module1.userprefs.nfoposterscraper = 14)) Or (Module1.userprefs.nfoposterscraper = 15)) Then
                        Dim class3 As New class_mpdb_thumbs.Class1
                        Dim document5 As New XmlDocument
                        Try 
                            Dim str47 As String = Conversions.ToString(class3.get_mpdb_thumbs(movietosave.fullmoviebody.imdbid))
                            Dim str48 As String = ("<totalthumbs>" & str47 & "</totalthumbs>")
                            document5.LoadXml(str48)
                            str42 = (str42 & str47.ToString)
                        Catch exception37 As Exception
                            ProjectData.SetProjectError(exception37)
                            Dim exception10 As Exception = exception37
                            Thread.Sleep(1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    If ((((((((Module1.userprefs.nfoposterscraper = 8) Or (Module1.userprefs.nfoposterscraper = 9)) Or (Module1.userprefs.nfoposterscraper = 10)) Or (Module1.userprefs.nfoposterscraper = 11)) Or (Module1.userprefs.nfoposterscraper = 12)) Or (Module1.userprefs.nfoposterscraper = 13)) Or (Module1.userprefs.nfoposterscraper = 14)) Or (Module1.userprefs.nfoposterscraper = 15)) Then
                        Dim class4 As New imdb_thumbs.Class1
                        Dim document6 As New XmlDocument
                        Try 
                            Dim str49 As String = Conversions.ToString(class4.getimdbthumbs(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year, movietosave.fullmoviebody.imdbid))
                            Dim str50 As String = ("<totalthumbs>" & str49 & "</totalthumbs>")
                            document6.LoadXml(str50)
                            str42 = (str42 & str49.ToString)
                        Catch exception38 As Exception
                            ProjectData.SetProjectError(exception38)
                            Dim exception11 As Exception = exception38
                            Thread.Sleep(1)
                            ProjectData.ClearProjectError
                        End Try
                    End If
                    str42 = ("<thumblist>" & str42 & "</thumblist>")
                    Try 
                        Dim enumerator13 As IEnumerator
                        document.LoadXml(str42)
                        Try 
                            enumerator13 = document.Item("thumblist").GetEnumerator
                            Do While enumerator13.MoveNext
                                node = DirectCast(enumerator13.Current, XmlNode)
                                If (node.Name = "thumb") Then
                                    movietosave.listthumbs.Add(node.InnerText)
                                End If
                            Loop
                        Finally
                            If TypeOf enumerator13 Is IDisposable Then
                                TryCast(enumerator13,IDisposable).Dispose
                            End If
                        End Try
                        Console.WriteLine("Poster URLs Scraped OK")
                    Catch exception39 As Exception
                        ProjectData.SetProjectError(exception39)
                        Dim exception12 As Exception = exception39
                        newmovie3 = Module1.newmovielist.Item(j)
                        Console.WriteLine(("Error with " & newmovie3.nfopathandfilename))
                        Console.WriteLine("An error was encountered at stage 4, Downloading poster list for nfo file")
                        Console.WriteLine(exception12.Message.ToString)
                        num += 1
                        movietosave.listthumbs.Clear
                        ProjectData.ClearProjectError
                    End Try
                End If
                Try 
                    newmovie3 = Module1.newmovielist.Item(j)
                    Dim str51 As String = Path.GetFileName(newmovie3.mediapathandfilename)
                    newmovie3 = Module1.newmovielist.Item(j)
                    newmovie2 = Module1.newmovielist.Item(j)
                    If Not File.Exists(Path.Combine(newmovie3.mediapathandfilename.Replace(Path.GetFileName(newmovie2.mediapathandfilename), ""), "tempoffline.ttt")) Then
                        newmovie3 = Module1.newmovielist.Item(j)
                        movietosave.filedetails = DirectCast(Module1.get_hdtags(newmovie3.mediapathandfilename), fullfiledetails)
                        If (movietosave.filedetails.filedetails_video.duration <> Nothing) Then
                            Try 
                                Dim num22 As Integer
                                Dim num23 As Integer
                                duration = movietosave.filedetails.filedetails_video.duration
                                index = duration.IndexOf("h")
                                If (index <> -1) Then
                                    num22 = Convert.ToInt32(duration.Substring(0, index))
                                    duration = Strings.Trim(duration.Substring((index + 1), (duration.Length - (index + 1))))
                                End If
                                index = duration.IndexOf("mn")
                                If (index <> -1) Then
                                    num23 = Convert.ToInt32(duration.Substring(0, index))
                                End If
                                If (num22 <> 0) Then
                                    num22 = (num22 * 60)
                                End If
                                num23 = (num23 + num22)
                                If ((num23 = 0) AndAlso (duration.IndexOf("min") <> -1)) Then
                                    duration = duration.Replace("min", "").Replace(" ", "")
                                    If Versioned.IsNumeric(duration) Then
                                        num23 = Convert.ToInt32(duration)
                                    End If
                                End If
                                movietosave.fullmoviebody.runtime = (num23.ToString & " min")
                                Console.WriteLine("HD Tags Added OK")
                            Catch exception40 As Exception
                                ProjectData.SetProjectError(exception40)
                                Dim exception13 As Exception = exception40
                                Console.WriteLine(("Error getting HD Tags:- " & exception13.Message.ToString))
                                ProjectData.ClearProjectError
                            End Try
                        End If
                    End If
                Catch exception41 As Exception
                    ProjectData.SetProjectError(exception41)
                    Dim exception14 As Exception = exception41
                    Console.WriteLine(("Error getting HD Tags:- " & exception14.Message.ToString))
                    ProjectData.ClearProjectError
                End Try
                If (movietosave.fullmoviebody.title = Nothing) Then
                    movietosave.fullmoviebody.title = "Unknown Title"
                End If
                If (movietosave.fullmoviebody.title = "") Then
                    movietosave.fullmoviebody.title = "Unknown Title"
                End If
                If (movietosave.fullmoviebody.year = Nothing) Then
                    movietosave.fullmoviebody.year = "0000"
                End If
                If (movietosave.fullmoviebody.rating = Nothing) Then
                    movietosave.fullmoviebody.rating = "0"
                End If
                If (movietosave.fullmoviebody.top250 = Nothing) Then
                    movietosave.fullmoviebody.top250 = "0"
                End If
                If (movietosave.fullmoviebody.playcount = Nothing) Then
                    movietosave.fullmoviebody.playcount = "0"
                End If
                If (movietosave.fullmoviebody.title = "Unknown Title") Then
                    movietosave.fullmoviebody.plot = "This Movie has could not be identified by Media Companion, to add the movie manually, go to the movie edit page and select ""Change Movie"" to manually select the correct movie"
                    Select Case filename
                        Case Nothing
                            filename = "Unknown Title"
                            Exit Select
                        Case ""
                            filename = "Unknown Title"
                            Exit Select
                    End Select
                    movietosave.fullmoviebody.title = filename
                End If
                If (movietosave.fullmoviebody.title = "Unknown Title") Then
                    movietosave.fullmoviebody.genre = "Problem"
                End If
                Dim now As DateTime = DateTime.Now
                Try 
                    movietosave.fileinfo.createdate = Strings.Format(now, "yyyyMMddHHmmss").ToString
                Catch exception42 As Exception
                    ProjectData.SetProjectError(exception42)
                    Dim exception15 As Exception = exception42
                    ProjectData.ClearProjectError
                End Try
                Module1.savemovienfo(fullpath, movietosave, True)
                Dim item As New combolist With { _
                    .fullpathandfilename = fullpath _
                }
                newmovie3 = Module1.newmovielist.Item(j)
                item.filename = Path.GetFileName(newmovie3.nfopathandfilename)
                newmovie3 = Module1.newmovielist.Item(j)
                item.foldername = Module1.getlastfolder(newmovie3.nfopathandfilename)
                item.title = movietosave.fullmoviebody.title
                item.sortorder = movietosave.fullmoviebody.sortorder
                item.runtime = movietosave.fullmoviebody.runtime
                If (movietosave.fullmoviebody.title <> Nothing) Then
                    If (movietosave.fullmoviebody.year <> Nothing) Then
                        item.titleandyear = (movietosave.fullmoviebody.title & " (" & movietosave.fullmoviebody.year & ")")
                    Else
                        item.titleandyear = (movietosave.fullmoviebody.title & " (0000)")
                    End If
                Else
                    item.titleandyear = "Unknown (0000)"
                End If
                item.outline = movietosave.fullmoviebody.outline
                item.year = movietosave.fullmoviebody.year
                newmovie3 = Module1.newmovielist.Item(j)
                Dim info5 As New FileInfo(newmovie3.nfopathandfilename)
                Dim lastWriteTime As DateTime = info5.LastWriteTime
                Try 
                    item.filedate = Strings.Format(lastWriteTime, "yyyyMMddHHmmss").ToString
                Catch exception43 As Exception
                    ProjectData.SetProjectError(exception43)
                    Dim exception16 As Exception = exception43
                    ProjectData.ClearProjectError
                End Try
                now = DateTime.Now
                Try 
                    item.createdate = Strings.Format(now, "yyyyMMddHHmmss").ToString
                Catch exception44 As Exception
                    ProjectData.SetProjectError(exception44)
                    Dim exception17 As Exception = exception44
                    ProjectData.ClearProjectError
                End Try
                item.id = movietosave.fullmoviebody.imdbid
                item.rating = movietosave.fullmoviebody.rating
                item.top250 = movietosave.fullmoviebody.top250
                item.genre = movietosave.fullmoviebody.genre
                item.playcount = movietosave.fullmoviebody.playcount
                Dim str33 As String = ""
                newmovie3 = Module1.newmovielist.Item(j)
                If Not ((Module1.userprefs.scrapemovieposters And Module1.userprefs.overwritethumbs) Or Not File.Exists(Module1.getposterpath(newmovie3.nfopathandfilename))) Then
                    goto Label_30FF
                End If
                Try 
                    Select Case Module1.userprefs.moviethumbpriority(0)
                        Case "Internet Movie Poster Awards"
                            str33 = Conversions.ToString(Module1.impathumb(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year))
                            goto Label_2C97
                        Case "IMDB"
                            str33 = Conversions.ToString(Module1.imdbthumb(movietosave.fullmoviebody.imdbid))
                            goto Label_2C97
                        Case "Movie Poster DB"
                            str33 = Conversions.ToString(Module1.mpdbthumb(movietosave.fullmoviebody.imdbid))
                            Exit Select
                        Case "themoviedb.org"
                            str33 = Conversions.ToString(Module1.tmdbthumb(movietosave.fullmoviebody.imdbid))
                            Exit Select
                    End Select
                Catch exception45 As Exception
                    ProjectData.SetProjectError(exception45)
                    str33 = "na"
                    ProjectData.ClearProjectError
                End Try
            Label_2C97:
                Try 
                    If (str33 = "na") Then
                        Select Case Module1.userprefs.moviethumbpriority(1)
                            Case "Internet Movie Poster Awards"
                                str33 = Conversions.ToString(Module1.impathumb(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year))
                                goto Label_2D7F
                            Case "IMDB"
                                str33 = Conversions.ToString(Module1.imdbthumb(movietosave.fullmoviebody.imdbid))
                                goto Label_2D7F
                            Case "Movie Poster DB"
                                str33 = Conversions.ToString(Module1.mpdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                            Case "themoviedb.org"
                                str33 = Conversions.ToString(Module1.tmdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                        End Select
                    End If
                Catch exception46 As Exception
                    ProjectData.SetProjectError(exception46)
                    str33 = "na"
                    ProjectData.ClearProjectError
                End Try
            Label_2D7F:
                Try 
                    If (str33 = "na") Then
                        Select Case Module1.userprefs.moviethumbpriority(2)
                            Case "Internet Movie Poster Awards"
                                str33 = Conversions.ToString(Module1.impathumb(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year))
                                goto Label_2E67
                            Case "IMDB"
                                str33 = Conversions.ToString(Module1.imdbthumb(movietosave.fullmoviebody.imdbid))
                                goto Label_2E67
                            Case "Movie Poster DB"
                                str33 = Conversions.ToString(Module1.mpdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                            Case "themoviedb.org"
                                str33 = Conversions.ToString(Module1.tmdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                        End Select
                    End If
                Catch exception47 As Exception
                    ProjectData.SetProjectError(exception47)
                    str33 = "na"
                    ProjectData.ClearProjectError
                End Try
            Label_2E67:
                Try 
                    If (str33 = "na") Then
                        Select Case Module1.userprefs.moviethumbpriority(3)
                            Case "Internet Movie Poster Awards"
                                str33 = Conversions.ToString(Module1.impathumb(movietosave.fullmoviebody.title, movietosave.fullmoviebody.year))
                                goto Label_2F4F
                            Case "IMDB"
                                str33 = Conversions.ToString(Module1.imdbthumb(movietosave.fullmoviebody.imdbid))
                                goto Label_2F4F
                            Case "Movie Poster DB"
                                str33 = Conversions.ToString(Module1.mpdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                            Case "themoviedb.org"
                                str33 = Conversions.ToString(Module1.tmdbthumb(movietosave.fullmoviebody.imdbid))
                                Exit Select
                        End Select
                    End If
                Catch exception48 As Exception
                    ProjectData.SetProjectError(exception48)
                    str33 = "na"
                    ProjectData.ClearProjectError
                End Try
            Label_2F4F:
                Try 
                    If ((str33 <> "") And (str33 <> "na")) Then
                        newmovie3 = Module1.newmovielist.Item(j)
                        Dim str53 As String = Module1.getposterpath(newmovie3.nfopathandfilename)
                        Try 
                            Dim buffer3 As Byte() = New Byte(&H3D0901  - 1) {}
                            Dim num26 As Integer = 0
                            Dim num24 As Integer = 0
                            Dim requestUriString As String = str33
                            Dim request3 As HttpWebRequest = DirectCast(WebRequest.Create(requestUriString), HttpWebRequest)
                            Dim stream7 As Stream = DirectCast(request3.GetResponse, HttpWebResponse).GetResponseStream
                            Dim num25 As Integer = buffer3.Length
                            Do While (num25 > 0)
                                num26 = stream7.Read(buffer3, num24, num25)
                                If (num26 = 0) Then
                                    Exit Do
                                End If
                                num25 = (num25 - num26)
                                num24 = (num24 + num26)
                            Loop
                            Dim stream8 As New FileStream(str23, FileMode.OpenOrCreate, FileAccess.Write)
                            stream8.Write(buffer3, 0, num24)
                            stream7.Close
                            stream8.Close
                            Console.WriteLine("Poster scraped and saved OK")
                            Dim str54 As String = str53.Replace(Path.GetFileName(str53), "folder.jpg")
                            If Module1.userprefs.createfolderjpg Then
                                If (Module1.userprefs.overwritethumbs Or Not File.Exists(str54)) Then
                                    Console.WriteLine(("Saving folder.jpg To Path :- " & str54))
                                    Dim stream9 As New FileStream(str54, FileMode.OpenOrCreate, FileAccess.Write)
                                    stream9.Write(buffer3, 0, num24)
                                    stream7.Close
                                    stream9.Close
                                    Console.WriteLine("Poster also saved as ""folder.jpg"" OK")
                                Else
                                    Console.WriteLine(("folder.jpg Not Saved to :- " & str54 & ", file already exists"))
                                End If
                            End If
                        Catch exception49 As Exception
                            ProjectData.SetProjectError(exception49)
                            Dim exception18 As Exception = exception49
                            Console.WriteLine("Problem Saving Thumbnail")
                            Console.WriteLine(("Error Returned :- " & exception18.ToString))
                            ProjectData.ClearProjectError
                        End Try
                    End If
                Catch exception50 As Exception
                    ProjectData.SetProjectError(exception50)
                    ProjectData.ClearProjectError
                End Try
            Label_30FF:
                newmovie3 = Module1.newmovielist.Item(j)
                If ((Module1.userprefs.overwritethumbs Or (Not Module1.userprefs.overwritethumbs And Not File.Exists(Module1.getfanartpath(newmovie3.nfopathandfilename)))) AndAlso Module1.userprefs.savefanart) Then
                    Try 
                        newmovie3 = Module1.newmovielist.Item(j)
                        Dim str56 As String = Module1.getfanartpath(newmovie3.nfopathandfilename)
                        str33 = ""
                        If (Not File.Exists(str56) Or Module1.userprefs.overwritethumbs) Then
                            Dim str58 As String = movietosave.fullmoviebody.imdbid
                            Dim str57 As String = ("http://api.themoviedb.org/2.0/Movie.imdbLookup?imdb_id=" & str58 & "&api_key=3f026194412846e530a208cf8a39e9cb")
                            Dim strArray3 As String() = New String(&H7D1  - 1) {}
                            Dim num27 As Integer = 0
                            Try 
                                Dim request4 As WebRequest = WebRequest.Create(str57)
                                Dim proxy As New WebProxy("myproxy", 80) With { _
                                    .BypassProxyOnLocal = True _
                                }
                                Dim reader3 As New StreamReader(request4.GetResponse.GetResponseStream)
                                Dim str59 As String = ""
                                num27 = 0
                                Do While (Not str59 Is Nothing)
                                    num27 += 1
                                    strArray3(num27) = reader3.ReadLine
                                Loop
                                num27 -= 1
                                Dim flag7 As Boolean = False
                                Dim num39 As Integer = num27
                                Dim num28 As Integer = 1
                                Do While (num28 <= num39)
                                    If (strArray3(num28).IndexOf("<backdrop size=""original"">") <> -1) Then
                                        strArray3(num28) = strArray3(num28).Replace("<backdrop size=""original"">", "")
                                        strArray3(num28) = strArray3(num28).Replace("</backdrop>", "")
                                        strArray3(num28) = strArray3(num28).Replace("  ", "")
                                        strArray3(num28) = strArray3(num28).Trim
                                        If ((((strArray3(num28).ToLower.IndexOf("http") <> -1) And (strArray3(num28).ToLower.IndexOf(".jpg") <> -1)) Or (strArray3(num28).IndexOf(".jpeg") <> -1)) Or (strArray3(num28).IndexOf(".png") <> -1)) Then
                                            str33 = strArray3(num28)
                                            flag7 = True
                                        End If
                                        Exit Do
                                    End If
                                    num28 += 1
                                Loop
                                If Not flag7 Then
                                    str33 = ""
                                End If
                            Catch exception51 As Exception
                                ProjectData.SetProjectError(exception51)
                                ProjectData.ClearProjectError
                            End Try
                            If (str33 <> "") Then
                                Console.WriteLine(("Saving Fanart As :- " & str56))
                                Try 
                                    Dim buffer4 As Byte() = New Byte(&H7A1201  - 1) {}
                                    Dim num31 As Integer = 0
                                    Dim num29 As Integer = 0
                                    Dim str60 As String = str33
                                    Dim request5 As HttpWebRequest = DirectCast(WebRequest.Create(str60), HttpWebRequest)
                                    Dim stream As Stream = DirectCast(request5.GetResponse, HttpWebResponse).GetResponseStream
                                    Dim num30 As Integer = buffer4.Length
                                    Dim original As New Bitmap(stream)
                                    Do While (num30 > 0)
                                        num31 = stream.Read(buffer4, num29, num30)
                                        If (num31 = 0) Then
                                            Exit Do
                                        End If
                                        num30 = (num30 - num31)
                                        num29 = (num29 + num31)
                                    Loop
                                    If (Module1.userprefs.resizefanart = 1) Then
                                        original.Save(str21, ImageFormat.Jpeg)
                                        Console.WriteLine("Fanart not resized")
                                    ElseIf (Module1.userprefs.resizefanart = 2) Then
                                        If ((original.Width > &H500) Or (original.Height > 720)) Then
                                            Dim image As New Bitmap(original)
                                            Dim bitmap2 As New Bitmap(&H500, 720)
                                            Dim graphics As Graphics = Graphics.FromImage(bitmap2)
                                            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear
                                            graphics.DrawImage(image, 0, 0, &H4FF, &H2CF)
                                            bitmap2.Save(str56, ImageFormat.Jpeg)
                                            Console.WriteLine("Farart Resized to 1280x720")
                                        Else
                                            Console.WriteLine("Fanart not resized, already =< required size")
                                            original.Save(str56, ImageFormat.Jpeg)
                                        End If
                                    ElseIf (Module1.userprefs.resizefanart = 3) Then
                                        If ((original.Width > 960) Or (original.Height > 540)) Then
                                            Dim bitmap5 As New Bitmap(original)
                                            Dim bitmap4 As New Bitmap(960, 540)
                                            Dim graphics2 As Graphics = Graphics.FromImage(bitmap4)
                                            graphics2.InterpolationMode = InterpolationMode.HighQualityBilinear
                                            graphics2.DrawImage(bitmap5, 0, 0, &H3BF, &H21B)
                                            bitmap4.Save(str56, ImageFormat.Jpeg)
                                            Console.WriteLine("Farart Resized to 960x540")
                                        Else
                                            Console.WriteLine("Fanart not resized, already =< required size")
                                            original.Save(str56, ImageFormat.Jpeg)
                                        End If
                                    End If
                                Catch exception52 As Exception
                                    ProjectData.SetProjectError(exception52)
                                    Dim exception19 As Exception = exception52
                                    Try 
                                        Console.WriteLine(("Fanart Not Saved to :- " & str56))
                                        Console.WriteLine(("Error received :- " & exception19.ToString))
                                    Catch exception53 As Exception
                                        ProjectData.SetProjectError(exception53)
                                        ProjectData.ClearProjectError
                                    End Try
                                    ProjectData.ClearProjectError
                                End Try
                            End If
                        End If
                    Catch exception54 As Exception
                        ProjectData.SetProjectError(exception54)
                        ProjectData.ClearProjectError
                    End Try
                End If
                Dim fullpathandfilename As String = item.fullpathandfilename
                fullpathandfilename = fullpathandfilename.Replace(Path.GetFileName(fullpathandfilename), "tempoffline.ttt")
                If File.Exists(fullpathandfilename) Then
                    File.Delete(fullpathandfilename)
                    Module1.offlinedvd(item.fullpathandfilename, item.title, Conversions.ToString(Module1.getfilename(item.fullpathandfilename)))
                End If
                Dim num11 As Byte = 0
                Dim flag3 As Boolean = File.Exists(Module1.getfanartpath(item.fullpathandfilename))
                Dim flag4 As Boolean = File.Exists(Module1.getposterpath(item.fullpathandfilename))
                If Not flag3 Then
                    num11 = CByte((num11 + 1))
                End If
                If Not flag4 Then
                    num11 = CByte((num11 + 2))
                End If
                item.missingdata1 = num11
                Module1.fullmovielist.Add(item)
            Label_3642:
                Console.WriteLine("Movie added to list")
            Label_364C:
                Console.WriteLine
                Console.WriteLine
                Console.WriteLine
                j += 1
            Loop
        End Sub

        Public Shared Function tmdbthumb(ByVal posterimdbid As String) As Object
            Dim obj2 As Object
            Try 
                Dim class2 As New tmdb_posters.Class1
                Dim flag As Boolean = False
                Try 
                    Dim str As String
                    Dim enumerator As IEnumerator
                    Dim xml As String = Conversions.ToString(class2.gettmdbposters_newapi(posterimdbid))
                    Dim document As New XmlDocument
                    document.LoadXml(xml)
                    Try 
                        enumerator = document.Item("tmdb_posterlist").GetEnumerator
                        Do While enumerator.MoveNext
                            Dim enumerator2 As IEnumerator
                            Dim objectValue As Object = RuntimeHelpers.GetObjectValue(enumerator.Current)
                            If Not Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, Nothing, "name", New Object(0  - 1) {}, Nothing, Nothing, Nothing), "poster", False) Then
                                Continue Do
                            End If
                            Try 
                                enumerator2 = DirectCast(objectValue, IEnumerable).GetEnumerator
                                Do While enumerator2.MoveNext
                                    Dim instance As Object = RuntimeHelpers.GetObjectValue(enumerator2.Current)
                                    If Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(instance, Nothing, "childnodes", New Object() { 0 }, Nothing, Nothing, Nothing), Nothing, "innertext", New Object(0  - 1) {}, Nothing, Nothing, Nothing), "original", False) Then
                                        str = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(instance, Nothing, "childnodes", New Object() { 1 }, Nothing, Nothing, Nothing), Nothing, "innertext", New Object(0  - 1) {}, Nothing, Nothing, Nothing))
                                        flag = True
                                        goto Label_0142
                                    End If
                                Loop
                            Finally
                                If TypeOf enumerator2 Is IDisposable Then
                                    TryCast(enumerator2,IDisposable).Dispose
                                End If
                            End Try
                        Label_0142:
                            If flag Then
                                goto Label_016B
                            End If
                        Loop
                    Finally
                        If TypeOf enumerator Is IDisposable Then
                            TryCast(enumerator,IDisposable).Dispose
                        End If
                    End Try
                Label_016B:
                    obj2 = str
                Catch exception1 As Exception
                    ProjectData.SetProjectError(exception1)
                    Dim exception As Exception = exception1
                    Thread.Sleep(1)
                    ProjectData.ClearProjectError
                End Try
            Catch exception3 As Exception
                ProjectData.SetProjectError(exception3)
                Dim exception2 As Exception = exception3
                ProjectData.ClearProjectError
            End Try
            Return obj2
        End Function

        Private Shared Function UrlIsValid(ByVal url As String) As Boolean
            Dim flag2 As Boolean
            If url.ToLower.StartsWith("www.") Then
                url = ("http://" & url)
            End If
            Dim response As HttpWebResponse = Nothing
            Try 
                Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
                request.Timeout = &H1388
                response = DirectCast(request.GetResponse, HttpWebResponse)
                flag2 = True
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                flag2 = False
                ProjectData.ClearProjectError
            Finally
                If (Not response Is Nothing) Then
                    response.Close
                End If
            End Try
            Return flag2
        End Function

        Public Shared Function validmoviedir(ByVal s As String) As Boolean
            Dim flag As Boolean = True
            Try 
                Dim str As String
                For Each str In Directory.GetDirectories(s)
                Next
                Dim flag3 As Boolean = True
                If (flag3 = (Strings.Right(s, 7).ToLower = "trailer")) Then
                    Return False
                End If
                If (flag3 = (Strings.Right(s, 8).ToLower = "(noscan)")) Then
                    Return False
                End If
                If (flag3 = (Strings.Right(s, 6).ToLower = "sample")) Then
                    Return False
                End If
                If (flag3 = (Strings.Right(s, 8).ToLower = "recycler")) Then
                    Return False
                End If
                If (flag3 = s.ToLower.Contains("$recycle.bin")) Then
                    Return False
                End If
                If (flag3 = (Strings.Right(s, 10).ToLower = "lost+found")) Then
                    Return False
                End If
                If (flag3 = s.ToLower.Contains("system volume information")) Then
                    Return False
                End If
                If (flag3 = s.Contains("MSOCache")) Then
                    flag = False
                End If
            Catch exception1 As Exception
                ProjectData.SetProjectError(exception1)
                Dim exception As Exception = exception1
                flag = False
                ProjectData.ClearProjectError
            End Try
            Return flag
        End Function


        ' Fields
        Private Shared actordb As List(Of actordatabase) = New List(Of actordatabase)
        Private Shared applicationpath As String = AppDomain.CurrentDomain.BaseDirectory
        Private Shared arguments As List(Of String) = New List(Of String)
        Private Shared basictvlist As List(Of basictvshownfo) = New List(Of basictvshownfo)
        Private Shared defaultofflineart As String = ""
        Private Shared defaultposter As String = ""
        Private Shared fullhtmlstring As String
        Private Shared fullmovielist As List(Of combolist) = New List(Of combolist)
        Private Shared htmlfileoutput As String = ""
        Private Shared imdbcounter As Integer = 0
        Private Shared listofargs As List(Of arguments) = New List(Of arguments)
        Private Shared moviefolders As List(Of String) = New List(Of String)
        Private Shared newepisodelist As List(Of episodeinfo) = New List(Of episodeinfo)
        Private Shared newepisodetoadd As basicepisodenfo = New basicepisodenfo
        Private Shared newmovielist As List(Of newmovie) = New List(Of newmovie)
        Private Shared profile As String = "default"
        Private Shared profile_structure As profiles = New profiles
        Private Shared showstoscrapelist As List(Of String) = New List(Of String)
        Private Shared templatelist As List(Of htmltemplate) = New List(Of htmltemplate)
        Private Shared totalepisodecount As Integer = 0
        Private Shared totaltvshowcount As Integer = 0
        Private Shared tvdburl As String
        Private Shared tvdbwebsource As String() = New String(&HBB9  - 1) {}
        Private Shared tvfblinecount As Integer
        Private Shared tvfolders As List(Of String) = New List(Of String)
        Private Shared tvregex As List(Of String) = New List(Of String)
        Private Shared tvrootfolders As List(Of String) = New List(Of String)
        Private Shared userprefs As preferences = New preferences
        Private Shared workingprofile As listofprofiles = New listofprofiles

        ' Nested Types
        <StructLayout(LayoutKind.Sequential)> _
        Private Structure htmltemplate
            Public title As String
            Public path As String
            Public body As String
        End Structure
    End Class
End Namespace

