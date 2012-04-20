Imports Media_Companion.Form1
Imports System.Text.RegularExpressions


Public Class Movies
    Public Shared Sub listMovieFiles(ByVal lst As String, ByVal pattern As String, ByVal dir_info As System.IO.DirectoryInfo)
        'scraperLog &= lst & " " & pattern & " " & dir_info.ToString & vbCrLf
        Dim moviepattern As String = pattern
        Dim tempint2 As Integer = 0
        Dim tempstring As String
        Try
            Dim fs_infos() As System.IO.FileInfo = dir_info.GetFiles(moviepattern)
            Dim workingMovieName As String
            Dim movieStackName As String
            Dim dofilter As Boolean = False
            Dim dvdfiles As Boolean
            For Each fs_info As System.IO.FileInfo In fs_infos
                If Preferences.usefoldernames = True Then
                    scraperLog &= ": '" & fs_info.Directory.Name.ToString & "'"                          'log directory name as Title due to use FOLDERNAMES
                Else
                    scraperLog &= ": '" & fs_info.ToString & "'"                                  'log title name
                End If

                Dim newmoviedetails As New str_NewMovie(SetDefaults)
                Dim title As String = String.Empty
                Dim remove As Boolean = False
                dvdfiles = False
                dofilter = False
                movieStackName = Utilities.GetStackName(fs_info.FullName)
                workingMovieName = fs_info.FullName.Replace(System.IO.Path.GetFileName(fs_info.FullName), movieStackName & ".nfo")
                newmoviedetails.mediapathandfilename = fs_info.FullName
                newmoviedetails.nfopathandfilename = workingMovieName
                Dim basicmoviename As String = workingMovieName.Replace(IO.Path.GetFileName(workingMovieName), "movie.nfo")
                If IO.File.Exists(basicmoviename) Then   'this removes this movie from the to scrape list if the folder contains a movie.nfo
                    remove = True
                    scraperLog &= " - 'movie.nfo' found - scrape skipped!"
                End If
                Dim otherformat As String = fs_info.FullName.Replace(System.IO.Path.GetExtension(fs_info.FullName), ".nfo")
                If IO.File.Exists(otherformat) Then
                    Dim allok2 As Boolean = False
                    Try
                        Dim filechck As IO.StreamReader = IO.File.OpenText(otherformat)
                        Do

                            tempstring = filechck.ReadLine
                            If tempstring = Nothing Then Exit Do

                            If tempstring.IndexOf("<movie>") <> -1 Then
                                allok2 = True
                                Exit Do
                            End If
                        Loop Until tempstring.IndexOf("</movie>") <> -1
                        filechck.Close()
                    Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                    End Try
                    If allok2 = True Then
                        remove = True
                        scraperLog &= " - valid MC .nfo found ('" & otherformat & "') type other - scrape skipped!"
                    End If
                End If

                If moviepattern = "*.vob" Then
                    scraperLog &= "VOB Pattern Found!"
                    Dim lonevobfile As String = workingMovieName.Replace(System.IO.Path.GetFileName(workingMovieName), "VIDEO_TS.IFO")
                    scraperLog &= " -" & lonevobfile
                    dvdfiles = System.IO.File.Exists(lonevobfile)
                End If

                If dvdfiles = False Then

                    If IO.File.Exists(workingMovieName) = True Then


                        Dim allok As Boolean = False
                        Try
                            Dim filechck As IO.StreamReader = IO.File.OpenText(workingMovieName)
                            Do

                                tempstring = filechck.ReadLine
                                If tempstring = Nothing Then Exit Do

                                If tempstring.IndexOf("<movie>") <> -1 Then
                                    allok = True
                                    Exit Do
                                End If
                            Loop Until tempstring.IndexOf("</movie>") <> -1
                            filechck.Close()
                        Catch ex As Exception
#If SilentErrorScream Then
                            Throw ex
#End If
                        Finally
                        End Try

                        If allok = False Then
                            dofilter = True
                            title = fs_info.FullName
                        Else
                            remove = True
                            scraperLog &= " - valid MC .nfo found ('" & fs_info.Name.Replace(System.IO.Path.GetExtension(fs_info.Name), ".nfo") & "') - scrape skipped!"
                        End If
                    Else
                        dofilter = True
                        title = fs_info.FullName
                    End If


                    If dofilter = True Then
                        If title <> Nothing Then
                            Dim M As Match = Regex.Match(title.ToLower, "((" & Join(Utilities.cleanMultipart, "|") & ")([" & Utilities.cleanSeparators & "]?)([0-9]+))")
                            Try
                                If M.Success Then
                                    Dim stackdesignator As String = M.Groups(2).Value   'use the existing 'part'-type
                                    Dim partNumber As Integer = Integer.Parse(M.Groups(4).Value)    'if not integer, will catch exception
                                    If Preferences.movieignorepart And (stackdesignator = "part" Or stackdesignator = "pt") Then
                                        'if this preference is set, we do want to add 'Part' movies, eg. 'Twilight Breaking Dawn Part 2'
                                    ElseIf stackdesignator = "dvd" Then
                                        'check if dvd5 or dvd9 are source designators, or are in an actual stack
                                        If partNumber = 5 Or partNumber = 9 Then
                                            Dim checkDVD As String = title
                                            Mid(checkDVD, M.Groups(4).Index + 1, 1) = "1"
                                            If IO.File.Exists(checkDVD) Then
                                                remove = True
                                            End If
                                        End If
                                    ElseIf partNumber > 1 Then
                                        remove = True
                                    End If
                                End If
                            Catch ex As Exception
                                'remove' is still false, so carry on
                            End Try

                            'ignore trailers
                            M = Regex.Match(title, "[.-_]trailer")
                            If M.Success Then remove = True

                            'ignore whatever this is meant to be!
                            If title.ToLower.IndexOf("sample") <> -1 And title.ToLower.IndexOf("people") = -1 Then remove = True

                            Dim tempname As String
                            Dim extension As String
                            extension = System.IO.Path.GetExtension(title.ToLower)
                            tempname = title.ToLower.Replace(extension, "")
                            If tempname.Substring(tempname.Length - 1) = "b" Or tempname.Substring(tempname.Length - 1) = "c" Or tempname.Substring(tempname.Length - 1) = "d" Or tempname.Substring(tempname.Length - 1) = "e" Or tempname.Substring(tempname.Length - 1) = "B" Or tempname.Substring(tempname.Length - 1) = "C" Or tempname.Substring(tempname.Length - 1) = "D" Or tempname.Substring(tempname.Length - 1) = "E" Then
                                tempname = newmoviedetails.nfopathandfilename.Substring(0, newmoviedetails.nfopathandfilename.Length - (1 + extension.Length)) & "a" & extension
                                If System.IO.File.Exists(tempname) Then remove = True
                            End If
                        End If
                    End If
                Else
                    scraperLog &= "- DVD File Structure Found!"
                End If


                Dim tempmovie2 As String = fs_info.FullName
                If IO.Path.GetExtension(tempmovie2).ToLower = ".rar" Then
                    If IO.File.Exists(tempmovie2) = True Then
                        If IO.File.Exists(workingMovieName) = False Then
                            Dim rarname As String = tempmovie2
                            Dim SizeOfFile As Integer = FileLen(rarname)
                            tempint2 = Convert.ToInt32(Preferences.rarsize) * 1048576
                            If SizeOfFile > tempint2 Then
                                Dim mat As Match
                                mat = Regex.Match(rarname, "\.part[0-9][0-9]?[0-9]?[0-9]?.rar")
                                If mat.Success = True Then
                                    rarname = mat.Value
                                    If rarname.ToLower.IndexOf(".part1.rar") <> -1 Or rarname.ToLower.IndexOf(".part01.rar") <> -1 Or rarname.ToLower.IndexOf(".part001.rar") <> -1 Or rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                        Dim stackrarexists As Boolean = False
                                        rarname = workingMovieName.Replace(".nfo", ".rar")
                                        If rarname.ToLower.IndexOf(".part1.rar") <> -1 Then
                                            rarname = rarname.Replace(".part1.rar", ".nfo")
                                            If IO.File.Exists(rarname) Then
                                                stackrarexists = True
                                                workingMovieName = rarname
                                            Else
                                                stackrarexists = False
                                                workingMovieName = rarname
                                            End If
                                        End If
                                        If rarname.ToLower.IndexOf(".part01.rar") <> -1 Then
                                            rarname = rarname.Replace(".part01.rar", ".nfo")
                                            If IO.File.Exists(rarname) Then
                                                stackrarexists = True
                                                workingMovieName = rarname
                                            Else
                                                stackrarexists = False
                                                workingMovieName = rarname
                                            End If
                                        End If
                                        If rarname.ToLower.IndexOf(".part001.rar") <> -1 Then
                                            rarname = rarname.Replace(".part001.rar", ".nfo")
                                            If IO.File.Exists(rarname) Then
                                                workingMovieName = rarname
                                                stackrarexists = True
                                            Else
                                                stackrarexists = False
                                                workingMovieName = rarname
                                            End If
                                        End If
                                        If rarname.ToLower.IndexOf(".part0001.rar") <> -1 Then
                                            rarname = rarname.Replace(".part0001.rar", ".nfo")
                                            If IO.File.Exists(rarname) Then
                                                workingMovieName = rarname
                                                stackrarexists = True
                                            Else
                                                stackrarexists = False
                                                workingMovieName = rarname
                                            End If
                                        End If
                                        If stackrarexists = True Then
                                            Dim allok As Boolean = False
                                            Try
                                                Dim filechck As IO.StreamReader = IO.File.OpenText(workingMovieName)
                                                Do

                                                    tempstring = filechck.ReadLine
                                                    If tempstring = Nothing Then Exit Do

                                                    If tempstring.IndexOf("<movie>") <> -1 Then
                                                        allok = True
                                                        Exit Do
                                                    End If
                                                Loop Until tempstring.IndexOf("</movie>") <> -1
                                                filechck.Close()
                                            Catch ex As Exception
#If SilentErrorScream Then
                                                Throw ex
#End If
                                            Finally
                                            End Try
                                            If allok = True Then
                                                remove = True
                                            Else
                                                title = workingMovieName
                                            End If
                                        Else
                                            title = workingMovieName
                                        End If
                                    Else
                                        remove = True
                                    End If
                                Else
                                    'remove = True
                                End If
                            Else
                                remove = True
                            End If
                        End If
                    End If
                End If
                If remove = True Then
                    remove = False
                    title = Nothing
                    newmoviedetails.mediapathandfilename = Nothing
                    newmoviedetails.nfopath = Nothing
                    newmoviedetails.nfopathandfilename = Nothing
                    newmoviedetails.title = Nothing

                Else
                    If title <> Nothing Then
                        'scraperLog &= " - TITLE: " & title
                        Dim extension As String
                        Dim filename2 As String
                        'newmoviedetails.nfopathandfilename = title
                        extension = System.IO.Path.GetExtension(title)
                        filename2 = System.IO.Path.GetFileName(title)
                        newmoviedetails.nfopath = title.Replace(filename2, "")
                        newmoviedetails.title = filename2.Replace(extension, "")
                        If extension <> ".IFO" And extension <> "ttt" Then
                            'newmoviedetails.mediapathandfilename = title
                            newmoviedetails.nfopathandfilename = newmoviedetails.nfopathandfilename.Replace(extension, ".nfo")
                        End If
                        'If dvdfolder = True Then
                        If extension.ToLower = ".ifo" Then
                            newmoviedetails.mediapathandfilename = title
                            newmoviedetails.nfopathandfilename = newmoviedetails.mediapathandfilename.Replace(extension, ".nfo")
                            If IO.File.Exists(newmoviedetails.nfopathandfilename) = False Then
                                Dim paths() As String = Nothing
                                If newmoviedetails.nfopathandfilename.IndexOf("\") <> -1 Then
                                    paths = newmoviedetails.nfopathandfilename.Split("\")
                                ElseIf newmoviedetails.nfopathandfilename.IndexOf("/") <> -1 Then
                                    paths = newmoviedetails.nfopathandfilename.Split("/")
                                End If
                                Dim depthecount As Integer = paths.GetUpperBound(0)
                                newmoviedetails.title = Nothing

                                For h = depthecount To 0 Step -1
                                    Dim temppath As String
                                    temppath = paths(h)
                                    paths(h) = paths(h).ToLower
                                    If paths(h).IndexOf("video_ts") = -1 Then
                                        newmoviedetails.title = temppath
                                    End If
                                    If newmoviedetails.title <> Nothing Then Exit For
                                Next
                            Else
                                newmoviedetails.nfopathandfilename = Nothing
                                newmoviedetails.title = Nothing
                            End If
                        End If

                        If Preferences.usefoldernames = True And newmoviedetails.title <> Nothing Then
                            Dim tempstring4 As String
                            tempstring4 = newmoviedetails.nfopathandfilename.ToLower
                            If tempstring4.IndexOf("video_ts") = -1 Then
                                newmoviedetails.title = newmoviedetails.nfopath.Substring(0, newmoviedetails.nfopath.Length - 1)
                                newmoviedetails.title = newmoviedetails.title.Substring(newmoviedetails.title.LastIndexOf("\") + 1, newmoviedetails.title.Length - newmoviedetails.title.LastIndexOf("\") - 1)
                                newmoviedetails.title = Utilities.GetLastFolder(newmoviedetails.nfopathandfilename)
                            End If
                        End If
                        Dim alreadyadded As Boolean = False
                        For Each newmovie In newMovieList
                            If newmovie.nfopathandfilename = newmoviedetails.nfopathandfilename Then
                                alreadyadded = True
                                scraperLog &= " - Already Added!"
                                Exit For
                            End If
                        Next
                        If alreadyadded = False Then
                            scraperLog &= " - NEW!"
                            newMovieList.Add(newmoviedetails)
                        Else
                            alreadyadded = False
                        End If
                    End If
                End If
                Application.DoEvents()
                scraperLog &= vbCrLf
            Next fs_info
            fs_infos = Nothing

        Catch ex As Exception
#If SilentErrorScream Then
            Throw ex
#End If
        Finally

        End Try
    End Sub

End Class
