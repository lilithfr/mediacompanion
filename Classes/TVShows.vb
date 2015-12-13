Public Class SeasonEpisodeComparer
    Implements IComparer(Of String)
    Private Shared Function ParseSeasonEpisode(ByVal item As String, ByVal type As Boolean) As Integer
        Dim hyphenIndex As Integer = item.IndexOf("-")
        ' Normally do some error checking in case hyphenIndex==-1
        Dim firstPart As String
        If type = True Then
            firstPart = item.Substring(0, hyphenIndex)
        Else
            firstPart = item.Substring(hyphenIndex + 1)
        End If
        Return Integer.Parse(firstPart)
    End Function

    Public Function Compare(ByVal first As String, ByVal second As String) _
                As Integer Implements IComparer(Of String).Compare
        ' In real code you would probably add nullity checks
        Dim firstSeason As Integer = ParseSeasonEpisode(first, True)
        Dim secondSeason As Integer = ParseSeasonEpisode(second, True)
        Dim sameSeason As Integer = firstSeason.CompareTo(secondSeason)
        If sameSeason = 0 Then
            Dim firstEpisode As Integer = ParseSeasonEpisode(first, False)
            Dim secondEpisode As Integer = ParseSeasonEpisode(second, False)
            sameSeason = firstEpisode.CompareTo(secondEpisode)
        End If
        Return sameSeason
    End Function
End Class

Public Class TVShows

    Public Shared Function episodeRename(ByVal path As String, ByVal seasonno As String, ByVal episodeno As List(Of String), ByVal showtitle As String, ByVal episodetitle As String)
        showtitle = Pref.RemoveIgnoredArticles(showtitle)
        Dim returnpath As String = "false"

        Dim medianame As String = path.Replace(IO.Path.GetExtension(path), "")
        For Each ext In Utilities.VideoExtensions
            If ext = "VIDEO_TS.IFO" Then Continue For
            Dim actualname As String = medianame & ext
            If IO.File.Exists(actualname) Then
                Dim newfilename As String
                newfilename = ""
                If seasonno.Length = 1 Then
                    seasonno = "0" & seasonno
                End If
                For g = 0 To episodeno.Count - 1
                    If episodeno(g).Length = 1 Then
                        episodeno(g) = "0" & episodeno(g)
                    End If
                Next
                newfilename = Renamer.setTVFilename(showtitle, episodetitle, episodeno, seasonno)

                newfilename = newfilename.Replace("?", "")
                newfilename = newfilename.Replace("/", "")
                newfilename = newfilename.Replace("\", "")
                newfilename = newfilename.Replace("<", "")
                newfilename = newfilename.Replace(">", "")
                newfilename = newfilename.Replace(":", "")
                newfilename = newfilename.Replace("""", "")
                newfilename = newfilename.Replace("*", "")
                newfilename = newfilename.Replace("|", "-")
                Dim listtorename As New List(Of String)
                listtorename.Clear()
                Dim done As String = ""
                Dim temppath As String = path

                listtorename.Add(actualname)

                Dim di As IO.DirectoryInfo = New IO.DirectoryInfo(path.Replace(IO.Path.GetFileName(path), ""))
                Dim filenama As String = IO.Path.GetFileNameWithoutExtension(path)
                Dim fils As IO.FileInfo() = di.GetFiles(filenama & ".*")
                For Each fiNext In fils
                    Dim extn As String = Utilities.GetExtension(fiNext.FullName)
                    Dim tmpname As String = fiNext.FullName.Replace(extn, extn.ToLower)
                    If Not listtorename.Contains(fiNext.FullName) AndAlso Not listtorename.Contains(tmpname) Then
                        listtorename.Add(fiNext.FullName)
                    End If
                Next

                temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".nfo")
                If IO.File.Exists(temppath) Then
                    If Not listtorename.Contains(temppath) Then
                        listtorename.Add(temppath)
                    End If
                End If

                temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".rar")
                If IO.File.Exists(temppath) Then
                    If Not listtorename.Contains(temppath) Then
                        listtorename.Add(temppath)
                    End If
                End If

                temppath = temppath.Replace(IO.Path.GetExtension(temppath), ".jpg")
                If IO.File.Exists(temppath) Then
                    If Not listtorename.Contains(temppath) Then
                        listtorename.Add(temppath)
                    End If
                End If

                temppath = temppath.Replace(IO.Path.GetExtension(temppath), "-thumb.jpg")
                If IO.File.Exists(temppath) Then
                    If Not listtorename.Contains(temppath) Then
                        listtorename.Add(temppath)
                    End If
                End If

                Dim StillOk As Boolean = True   'first we test every file we are going to rename, if they all can be renamed we then rename
                Dim RenameFailedFile As String = ""
                For Each ITEMS In listtorename
                    Dim newname As String = ITEMS.Replace(filenama, newfilename)
                    'newname = newname.Replace("..", ".")
                    Dim fi As New IO.FileInfo(ITEMS)
                    If IO.File.Exists(newname) Then
                        RenameFailedFile = newname
                        StillOk = False
                    End If
                Next

                If StillOk = True Then
                    Dim FirstCount As Boolean = True
                    For Each ITEMS In listtorename
                        Dim newname As String = ITEMS.Replace(filenama, newfilename)
                        'newname = newname.Replace("..", ".")
                        done = newname.Replace(IO.Path.GetExtension(newname), ".nfo")
                        If done.Contains("-thumb.") Then done = done.Replace("-thumb", "")
                        Try
                            Dim fi As New IO.FileInfo(ITEMS)
                            fi.MoveTo(newname)
                            If FirstCount = True Then  'we only want to show the renamed mediafile in the brief view
                                Pref.tvScraperLog &= "!!! Renamed to: " & newname & vbCrLf
                                FirstCount = False
                            Else
                                Pref.tvScraperLog &= "                " & newname & vbCrLf
                            End If

                        Catch ex As Exception
                            done = path
                            Pref.tvScraperLog &= "!!! *** Rename FAILED for : " & newname & vbCrLf
                            Pref.tvScraperLog &= "!!! *** Reported Message  : " & ex.Message & vbCrLf
                            Pref.tvScraperLog &= "!!! *** Resolve the indicated issue, & then try to rename the files again." & vbCrLf
                            Exit For 'we need to stop renaming the rest of the files if we get here. We usually get here if the episode
                            '        ' cannot be renamed because another process has locked the file. i.e. another app has the file open.  
                        End Try

                    Next
                    returnpath = done
                Else
                    Pref.tvScraperLog &= "!!! Rename aborted - file already exists : " & RenameFailedFile & vbCrLf
                End If
                Pref.tvScraperLog &= "!!! " & vbCrLf
            End If
        Next
        Return returnpath
    End Function
End Class
