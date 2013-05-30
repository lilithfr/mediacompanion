Imports System.Text.RegularExpressions

Public Class HomeMovies

    Public Shared newHomeMovieList As New List(Of str_BasicHomeMovie)
    Public Shared Function listHomeMovieFiles(ByVal dir_info As IO.DirectoryInfo, ByVal moviePattern As String, Optional ByRef scraperLog As String = "")
        newHomeMovieList.Clear()
        Try
            Dim fs_infos() As IO.FileInfo = dir_info.GetFiles(moviePattern)
            For Each fs_info As IO.FileInfo In fs_infos
                Dim titleFull As String = fs_info.FullName
                Dim titleDir As String = fs_info.Directory.ToString & IO.Path.DirectorySeparatorChar
                Dim titleExt As String = fs_info.Extension
                Dim doNotAdd As Boolean = False
                Dim newHomeMovieDetails As New str_BasicHomeMovie(Preferences.SetDefaults)

                If Preferences.usefoldernames = True Then
                    scraperLog &= "  '" & fs_info.Directory.Name.ToString & "'"     'log directory name as Title due to use FOLDERNAMES
                Else
                    scraperLog &= "  '" & fs_info.ToString & "'"                    'log title name
                End If

                Dim movieNfoFile As String = titleFull

                Dim needtorename As Boolean = False

                If Utilities.findFileOfType(movieNfoFile, ".nfo") Then
                    Try
                        Dim filechck As IO.StreamReader = IO.File.OpenText(movieNfoFile)
                        Dim tempstring As String
                        Do
                            tempstring = filechck.ReadToEnd
                            If tempstring = Nothing Then Exit Do
                            If tempstring.IndexOf("<movie>") <> -1 Then
                                Dim existsincache As Boolean = False
                                For Each item In Form1.homemovielist
                                    If item.FullPathAndFilename = movieNfoFile Then
                                        existsincache = True
                                        needtorename = False
                                        Exit For
                                    End If
                                Next
                                If existsincache = True Then
                                    doNotAdd = True
                                    scraperLog &= " - valid MC .nfo found - scrape skipped!"
                                    Exit Do
                                End If
                            Else
                                'not a valid nfo file
                                needtorename = True
                                Exit Do

                            End If
                        Loop Until filechck.EndOfStream
                        filechck.Close()
                        If needtorename = True Then
                            scraperLog &= " - invalid MC .nfo found - Renaming to .info"
                            Dim fi As New IO.FileInfo(movieNfoFile)
                            Dim newname As String = movieNfoFile.Replace(".nfo", ".info")
                            fi.MoveTo(newname)
                        End If
                    Catch ex As Exception
#If SilentErrorScream Then
                        Throw ex
#End If
                    End Try
                End If

                If moviePattern = "*.vob" Then
                    If IO.File.Exists(titleDir & "video_ts.ifo") Then
                        scraperLog &= " VOB Pattern Found! DVD File Structure Found!"
                    Else
                        scraperLog &= " WARNING: No DVD File Structure Found - (VIDEO_TS.IFO missing)"
                    End If
                    scraperLog &= vbCrLf
                    Exit For
                Else

                    If Not doNotAdd And titleExt <> "ttt" Then
                        newHomeMovieDetails.Title = IO.Path.GetFileNameWithoutExtension(titleFull) '<--- could be movieStackName?
                        newHomeMovieDetails.FullPathAndFilename = titleFull
                    End If
                    Dim alreadyadded As Boolean = False
                    For Each movie In Form1.homemovielist
                        If newHomeMovieDetails.FullPathAndFilename = movie.FullPathAndFilename Then
                            alreadyadded = True
                            scraperLog &= " - Already Added!"
                            Exit For
                        End If
                    Next
                    If alreadyadded = False Then
                        If newHomeMovieDetails.FullPathAndFilename <> "" And newHomeMovieDetails.Title <> "" Then
                            scraperLog &= " - NEW!"
                            newHomeMovieList.Add(newHomeMovieDetails)
                        End If
                    Else
                        alreadyadded = False
                    End If
                    'End If
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
        Return newHomeMovieList
    End Function
End Class
