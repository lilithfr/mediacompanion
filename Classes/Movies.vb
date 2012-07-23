'Imports Media_Companion.Form1
Imports System.Text.RegularExpressions


Public Class Movies
    Public Shared newMovieList As New List(Of str_NewMovie)

    Public Shared Sub listMovieFiles(ByVal dir_info As IO.DirectoryInfo, ByVal moviePattern As String, Optional ByRef scraperLog As String = "")
        Try
            Dim fs_infos() As IO.FileInfo = dir_info.GetFiles(moviePattern)
            For Each fs_info As IO.FileInfo In fs_infos
                Dim titleFull As String = fs_info.FullName
                Dim titleDir As String = fs_info.Directory.ToString & IO.Path.DirectorySeparatorChar
                Dim titleExt As String = fs_info.Extension
                Dim doNotAdd As Boolean = False
                Dim newmoviedetails As New str_NewMovie(Preferences.SetDefaults)

                If Preferences.usefoldernames = True Then
                    scraperLog &= "  '" & fs_info.Directory.Name.ToString & "'"     'log directory name as Title due to use FOLDERNAMES
                Else
                    scraperLog &= "  '" & fs_info.ToString & "'"                    'log title name
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
                                scraperLog &= " - valid MC .nfo found - scrape skipped!"
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

                If moviePattern = "*.vob" Then
                    If IO.File.Exists(titleDir & "video_ts.ifo") Then
                        scraperLog &= " VOB Pattern Found! DVD File Structure Found!"
                    Else
                        scraperLog &= " WARNING: No DVD File Structure Found - (VIDEO_TS.IFO missing)"
                    End If
                    scraperLog &= vbCrLf
                    Exit For
                Else
                    Dim movieStackName As String = titleFull
                    Dim firstPart As Boolean
                    If Utilities.isMultiPartMedia(movieStackName, False, firstPart) Then
                        If Not firstPart Then doNotAdd = True
                        If Preferences.namemode <> "1" Then titleFull = titleDir & movieStackName & titleExt
                    End If

                    'ignore trailers
                    Dim M As Match
                    M = Regex.Match(titleFull, "[-_.]trailer")
                    If M.Success Then
                        scraperLog &= " - ignore trailer"
                        doNotAdd = True
                    End If

                    'ignore whatever this is meant to be!
                    If titleFull.ToLower.IndexOf("sample") <> -1 And titleFull.ToLower.IndexOf("people") = -1 Then doNotAdd = True

                    If Not doNotAdd And titleExt <> "ttt" Then
                        newmoviedetails.mediapathandfilename = fs_info.FullName
                        newmoviedetails.nfopath = titleDir
                        newmoviedetails.nfopathandfilename = titleFull.Replace(titleExt, ".nfo")

                        If Preferences.usefoldernames = True Or titleExt.ToLower = ".ifo" Then
                            newmoviedetails.title = Utilities.GetLastFolder(newmoviedetails.nfopathandfilename)
                        Else
                            newmoviedetails.title = IO.Path.GetFileNameWithoutExtension(titleFull) '<--- could be movieStackName?
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

    Public Shared Function getExtraIdFromNFO(ByVal fullPath As String, Optional ByRef scraperLog As String = "") As String
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

    Public Shared Function fileRename(ByVal movieDetails As str_BasicMovieNFO, ByRef movieFileInfo As str_NewMovie) As String
        Dim log As String = ""
        Dim newpath As String = movieFileInfo.nfopath
        Dim mediaFile As String = movieFileInfo.mediapathandfilename
        Dim movieStackList As New List(Of String)(New String() {mediaFile})
        Dim stackName As String = mediaFile
        Dim isStack As Boolean = False
        Dim isFirstPart As Boolean = True
        Dim nextStackPart As String = ""
        Dim stackdesignator As String = ""
        Dim newextension As String = IO.Path.GetExtension(mediaFile)
        Dim newfilename As String = Preferences.MovieRenameTemplate
        Dim targetMovieFile As String = ""
        Dim targetNfoFile As String = ""
        Dim aFileExists As Boolean = False
        Try
            'create new filename (hopefully removing invalid chars first else Move (rename) will fail)
            newfilename = newfilename.Replace("%T", movieDetails.title)       'replaces %T with movie title
            newfilename = newfilename.Replace("%Y", movieDetails.year)        'replaces %Y with year   
            newfilename = newfilename.Replace("%I", movieDetails.imdbid)      'replaces %I with imdid 
            newfilename = newfilename.Replace("%P", movieDetails.premiered)   'replaces %P with premiered date 
            newfilename = newfilename.Replace("%R", movieDetails.rating)      'replaces %R with rating 
            newfilename = newfilename.Replace("%L", movieDetails.runtime)     'replaces %L with runtime (length)
            newfilename = Utilities.cleanFilenameIllegalChars(newfilename)                  'removes chars that can't be in a filename

            'designate the new main movie file (without extension) and test the new filenames do not already exist
            targetMovieFile = newpath & newfilename
            targetNfoFile = targetMovieFile
            If Utilities.testForFileByName(targetMovieFile, newextension) Then
                aFileExists = True
            Else
                'determine if any 'part' names are in the original title - if so, compile a list of stacked media files for renaming
                Do While Utilities.isMultiPartMedia(stackName, False, isFirstPart, stackdesignator, nextStackPart)
                    If isFirstPart Then
                        isStack = True                    'this media file has already been added to the list, but check for existing file with new name
                        Dim i As Integer                  'sacrificial variable to appease the TryParseosaurus Checks
                        targetMovieFile = newpath & newfilename & stackdesignator & If(Integer.TryParse(nextStackPart, i), "1".PadLeft(nextStackPart.Length, "0"), "A")
                        If Utilities.testForFileByName(targetMovieFile, newextension) Then
                            aFileExists = True
                            Exit Do
                        End If
                        If Preferences.namemode = "1" Then targetNfoFile = targetMovieFile
                    Else
                        movieStackList.Add(mediaFile)
                    End If
                    stackName = newpath & stackName & stackdesignator & nextStackPart & newextension
                    mediaFile = stackName
                Loop
            End If

            If aFileExists = False Then         'if none of the possible renamed files already exist then we rename found media files
                Dim logRename As String = ""    'used to build up a string of the renamed files for the log
                movieStackList.Sort()           'we're sure hoping the originals were labelled correctly, ie only incremental numbers changing!
                For i = 0 To movieStackList.Count - 1
                    Dim changename As String = String.Format("{0}{1}{2}{3}", newfilename, stackdesignator, If(isStack, i + 1, ""), newextension)
                    IO.File.Move(movieStackList(i), newpath & changename)
                    logRename &= If(i, " and ", "") & changename
                Next
                log &= "Renamed Movie File to " & logRename & vbCrLf

                For Each subtitle As String In {".sub", ".srt", ".smi", ".idx"} 'rename any subtitle files with the same name as the movie
                    If System.IO.File.Exists(movieFileInfo.mediapathandfilename.Replace(newextension, subtitle)) Then
                        System.IO.File.Move(movieFileInfo.mediapathandfilename.Replace(newextension, subtitle), targetMovieFile & subtitle) ' subtitles file with .sub extension
                        log &= "Renamed '" & subtitle & "' subtitle File" & vbCrLf
                    End If
                Next

                'update the new movie structure with the new data
                movieFileInfo.mediapathandfilename = targetMovieFile & newextension 'this is the new full path & filname to the rename media file
                movieFileInfo.nfopathandfilename = targetNfoFile & ".nfo"           'this is the new nfo path (yet to be created)
                movieFileInfo.title = newfilename                                   'new title
            Else
                log &= String.Format("A file exists with the target filename of '{0}' - RENAME SKIPPED{1}", newfilename, vbCrLf)
            End If
        Catch ex As Exception
            log &= "!!!Rename Movie File FAILED !!!" & vbCrLf
        End Try
        Return log
    End Function
End Class
