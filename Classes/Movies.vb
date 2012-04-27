Imports Media_Companion.Form1
Imports System.Text.RegularExpressions


Public Class Movies
    Public Shared Sub listMovieFiles(ByVal dir_info As IO.DirectoryInfo, ByVal moviePattern As String)
        'scraperLog &= lst & " " & pattern & " " & dir_info.ToString & vbCrLf
        Try
            Dim fs_infos() As IO.FileInfo = dir_info.GetFiles(moviePattern)
            For Each fs_info As IO.FileInfo In fs_infos
                Dim titleFull As String = fs_info.FullName
                Dim titleDir As String = IO.Path.GetDirectoryName(fs_info.FullName) & "\"
                Dim titleExt As String = IO.Path.GetExtension(fs_info.FullName)
                Dim doNotAdd As Boolean = False
                Dim newmoviedetails As New str_NewMovie(SetDefaults)

                If Preferences.usefoldernames = True Then
                    scraperLog &= ": '" & fs_info.Directory.Name.ToString & "'"     'log directory name as Title due to use FOLDERNAMES
                Else
                    scraperLog &= ": '" & fs_info.ToString & "'"                    'log title name
                End If

                Dim movieNfoFile As String = titleFull
                If Utilities.findFileOfType(movieNfoFile, ".nfo") Then
                    Try
                        Dim filechck As IO.StreamReader = IO.File.OpenText(movieNfoFile)
                        Dim tempstring As String
                        Do
                            tempstring = filechck.ReadLine
                            If tempstring = Nothing Then Exit Do
                            If tempstring.IndexOf("<movie>") <> -1 Then
                                doNotAdd = True
                                scraperLog &= " - valid MC .nfo found ('" & movieNfoFile & "') - scrape skipped!"
                                Exit Do
                            End If
                        Loop Until filechck.EndOfStream
                        filechck.Close()
                    Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                    End Try
                End If

                If moviePattern = "*.vob" AndAlso IO.File.Exists(titleDir & "video_ts.ifo") Then
                    scraperLog &= "VOB Pattern Found! -" & titleDir & "VIDEO_TS.IFO - DVD File Structure Found!"
                    newmoviedetails.mediapathandfilename = titleFull
                    newmoviedetails.nfopathandfilename = newmoviedetails.mediapathandfilename.Replace(titleExt, ".nfo")
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
                Else
                    Dim movieStackName As String = titleFull
                    Dim firstPart As Boolean
                    If Utilities.isMultiPartMedia(movieStackName, firstPart) Then
                        If Not firstPart Then doNotAdd = True
                        If Preferences.namemode <> "1" Then titleFull = titleDir & movieStackName & titleExt
                    End If

                    'ignore trailers
                    Dim M As Match
                    M = Regex.Match(titleFull, "[.-_]trailer")
                    If M.Success Then doNotAdd = True

                    'ignore whatever this is meant to be!
                    If titleFull.ToLower.IndexOf("sample") <> -1 And titleFull.ToLower.IndexOf("people") = -1 Then doNotAdd = True

                    If Not doNotAdd And titleExt <> "ttt" Then
                        newmoviedetails.mediapathandfilename = fs_info.FullName
                        newmoviedetails.nfopath = titleDir
                        newmoviedetails.title = IO.Path.GetFileNameWithoutExtension(titleFull) '<--- could be movieStackName?
                        newmoviedetails.nfopathandfilename = titleFull.Replace(titleExt, ".nfo")

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

    Public Shared Function getExtraIdFromNFO(ByVal fullPath As String) As String
        Dim extrapossibleID As String = Nothing
        Dim fileNFO As String = fullPath
        If Utilities.findFileOfType(fileNFO, ".nfo") Then
            Dim objReader As New System.IO.StreamReader(fileNFO)
            Dim tempInfo As String = objReader.ReadToEnd
            objReader.Close()
            Dim M As Match = Regex.Match(tempInfo, "(tt\d{7})")
            If M.Success = True Then
                extrapossibleID = M.Value
                scraperLog = scraperLog & "IMDB ID found in nfo file:- " & extrapossibleID & vbCrLf
            Else
                scraperLog = scraperLog & "No IMDB ID found in NFO" & vbCrLf
            End If
            If Preferences.renamenfofiles = True Then   'reenabled choice as per user preference
                Try
                    If Not IO.File.Exists(fileNFO.Replace(".nfo", ".info")) Then
                        IO.File.Move(fileNFO, fileNFO.Replace(".nfo", ".info"))
                        scraperLog = scraperLog & "renaming nfo file to:- " & fileNFO.Replace(".nfo", ".info") & vbCrLf
                    Else
                        scraperLog = scraperLog & "!!! Unable to rename file, """ & fileNFO & """ already exists" & vbCrLf
                    End If
                Catch
                    scraperLog = scraperLog & "!!! Unable to rename file, """ & fileNFO & """ already exists" & vbCrLf
                End Try
            Else
                scraperLog = scraperLog & "Current nfo file will be overwritten" & vbCrLf
            End If
        End If
        Return extrapossibleID
    End Function

End Class
