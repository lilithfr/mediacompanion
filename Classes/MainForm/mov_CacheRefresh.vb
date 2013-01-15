Imports Media_Companion


Public Class mov_RebuildMovieCaches

    Public Sub ex(ByVal filteredList As List(Of ComboList), oMovies As Movies)

        Form1.Enabled = False

        Form1.ProgressAndStatus1.Display()
        Form1.ProgressAndStatus1.Status("Rebuilding Movie caches...")
        Form1.ProgressAndStatus1.ReportProgress(0, "Processing....")
        Application.DoEvents()

        oMovies.RebuildCaches

        filteredList.Clear
        filteredList.AddRange(oMovies.MovieCache)
        Form1.filteredListObj.Clear
        Form1.filteredListObj.AddRange(oMovies.Data_GridViewMovieCache)


        Form1.ProgressAndStatus1.ReportProgress(0, "Apply Filters...")
        Mc.clsGridViewMovie.mov_FiltersAndSortApply

        Form1.ProgressAndStatus1.ReportProgress(0, "Reload Main Page...")
        Form1.mov_FormPopulate

        If Form1.DataGridViewMovies.Rows.Count>0 Then
            Form1.DataGridViewMovies.Rows(0).Selected = True
        End If

        Form1.Activate()
        Form1.Enabled = True
        Form1.ProgressAndStatus1.Visible = False
    End Sub


End Class
