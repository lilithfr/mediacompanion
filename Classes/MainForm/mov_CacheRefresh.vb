Imports Media_Companion

Public Class mov_CacheRefresh

    Public Sub ex(ByVal dList As List(Of String), ByVal fullMovieList As List(Of str_ComboList), ByVal progressmode As Boolean, ByVal movieFolders As List(Of String))
        'ByVal folderlist As List(Of String)
        Form1.Enabled = False

        Form1.ProgressAndStatus1.Display()
        Form1.ProgressAndStatus1.Status("Refresh Movies...")
        Form1.ProgressAndStatus1.ReportProgress(0, "Searching for Movie Folders.....")
        Application.DoEvents()

        dList.Clear()
        fullMovieList.Clear()
        Form1.filteredList.Clear()
        '----------------------------------------------Progess Bar Addition
        If Preferences.usefoldernames = True Then         'use TRUE if folder contains nfo's, False if folder contains folders which contain nfo's
            progressmode = False
        Else
            progressmode = True
        End If

        Call Form1.mov_NfoLoad(movieFolders, progressmode)

        Form1.ProgressAndStatus1.ReportProgress(0, "Searching for Offline Movie Folders.....")
        Application.DoEvents()                                  ' If not called previous progress bar is not hidden as requested 
        progressmode = False                                            'offlines folders always are folders of folders that contain nfo's
        Call Form1.mov_NfoLoad(Preferences.offlinefolders, progressmode)
        Application.DoEvents()
        '----------------------------------------------

        Form1.ProgressAndStatus1.ReportProgress(0, "Processing....")

        For Each movie In fullMovieList
            Try
                If Preferences.usefoldernames = False Then
                    If movie.filename <> Nothing Then
                        movie.filename = movie.filename.Replace(".nfo", "")
                        'Dim tempstring4 As String = ""                         'not sure of the purpose of this, tempstring4 is never used.....
                        'tempstring4 = movie.fullpathandfilename.ToLower
                    End If
                End If
            Catch
                Exit For
            End Try
        Next

        Form1.ProgressAndStatus1.Status("Save Data...")
        Call Classes.mov_CacheSave.ex(fullMovieList)
        'Call mov_CacheLoad()

        Call Form1.mov_FormPopulate()

        'Call sortorder()    ApplyFilters calls sortorder()
        Form1.ProgressAndStatus1.ReportProgress(0, "Apply Filters...")
        Call Classes.clsGridViewMovie.mov_FiltersAndSortApply()
        Form1.ProgressAndStatus1.ReportProgress(0, "Reload Main Page...")

        If Form1.DataGridViewMovies.Rows.Count > 1 Then
            Form1.DataGridViewMovies.Rows(0).Selected = True
        End If

        Form1.Activate()
        Form1.Enabled = True
        Form1.ProgressAndStatus1.Visible = False
    End Sub


End Class
