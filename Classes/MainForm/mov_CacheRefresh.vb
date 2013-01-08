Imports Media_Companion


Public Class mov_CacheRefresh

    Public Sub ex(ByVal filteredList As List(Of str_ComboList), ByVal dList As List(Of String), ByVal fullMovieList As List(Of str_ComboList), ByVal progressmode As Boolean, ByVal movieFolders As List(Of String))

        Form1.Enabled = False

        Form1.ProgressAndStatus1.Display()
        Form1.ProgressAndStatus1.Status("Refresh Movies...")
        Form1.ProgressAndStatus1.ReportProgress(0, "Searching for Movie Folders.....")
        Application.DoEvents()

        Form1.dList.Clear()
        fullMovieList.Clear()
        filteredList.Clear()
        Form1.filteredListObj.Clear()

        If Preferences.usefoldernames = True Then         'use TRUE if folder contains nfo's, False if folder contains folders which contain nfo's
            progressmode = False
        Else
            progressmode = True
        End If

        Call Form1.mov_NfoLoad(movieFolders, progressmode)

        Form1.ProgressAndStatus1.ReportProgress(0, "Searching for Offline Movie Folders.....")
        Application.DoEvents()
        progressmode = False
        Call Form1.mov_NfoLoad(Preferences.offlinefolders, progressmode)
        Application.DoEvents()


        Form1.ProgressAndStatus1.ReportProgress(0, "Processing....")

        For Each movie In fullMovieList
            Try
                If Preferences.usefoldernames = False Then
                    If movie.filename <> Nothing Then
                        movie.filename = movie.filename.Replace(".nfo", "")
                    End If
                End If
            Catch
                Exit For
            End Try
        Next

        Form1.ProgressAndStatus1.Status("Save Data...")
        Call Mc.mov_CacheSave.ex(fullMovieList)

        Call Form1.mov_FormPopulate()
        Form1.ProgressAndStatus1.ReportProgress(0, "Apply Filters...")


        'Call Mc.clsGridViewMovie.mov_FiltersAndSortApply()

        Form1.ProgressAndStatus1.ReportProgress(0, "Reload Main Page...")

        If Form1.DataGridViewMovies.Rows.Count > 1 Then
            Form1.DataGridViewMovies.Rows(0).Selected = True
        End If

        Form1.Activate()
        Form1.Enabled = True
        Form1.ProgressAndStatus1.Visible = False
    End Sub


End Class
