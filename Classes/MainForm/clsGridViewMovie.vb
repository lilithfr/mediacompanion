Public Class clsGridViewMovie
    Public Sub GridviewMovieDesign(ByVal DataGridViewMovies As DataGridView)
        'Public Sub GridviewMovieDesign(ByRef DataGridViewMovies As DataGridView)



        Dim imgWatched As New DataGridViewImageColumn()
        Dim inImgWatched As Image = Global.Media_Companion.My.Resources.Resources.DotGray
        imgWatched.Image = inImgWatched
        Form1.DataGridViewMovies.Columns.Add(imgWatched)
        imgWatched.HeaderText = "Watched"
        imgWatched.Name = "Watched"

        Dim imgPlot As New DataGridViewImageColumn()
        Dim inImgPlot As Image = Global.Media_Companion.My.Resources.Resources.DotGray
        imgPlot.Image = inImgPlot
        DataGridViewMovies.Columns.Add(imgPlot)
        imgPlot.HeaderText = "Plot"
        imgPlot.Name = "Plot"

        'Watched icon
        For Each row As DataGridViewRow In Form1.DataGridViewMovies.Rows
            If row.Cells(13).Value = "1" Then
                row.Cells(22).Value = Global.Media_Companion.My.Resources.Movie
            End If
        Next

        'plot icon
        For Each row As DataGridViewRow In Form1.DataGridViewMovies.Rows
            If row.Cells(14).Value.trim <> "" Then
                row.Cells(23).Value = Global.Media_Companion.My.Resources.Page
            End If
        Next


        Dim debug As Boolean = False
        If debug = False Then
            DataGridViewMovies.Columns(0).Visible = False
            DataGridViewMovies.Columns(1).Visible = False
            DataGridViewMovies.Columns(2).Visible = False
            DataGridViewMovies.Columns(3).Visible = False
            DataGridViewMovies.Columns(4).Visible = False
            DataGridViewMovies.Columns(5).Visible = False 'title
            'DataGridViewMovies.Columns(6).Visible = False 'title and year
            DataGridViewMovies.Columns(7).Visible = False
            DataGridViewMovies.Columns(8).Visible = False
            DataGridViewMovies.Columns(9).Visible = False
            DataGridViewMovies.Columns(10).Visible = False
            DataGridViewMovies.Columns(11).Visible = False
            DataGridViewMovies.Columns(12).Visible = False 'type
            DataGridViewMovies.Columns(13).Visible = False 'playcount
            DataGridViewMovies.Columns(14).Visible = False 'plot
            DataGridViewMovies.Columns(15).Visible = False
            DataGridViewMovies.Columns(16).Visible = False 'durée
            DataGridViewMovies.Columns(17).Visible = False
            DataGridViewMovies.Columns(18).Visible = False
            DataGridViewMovies.Columns(19).Visible = False
            DataGridViewMovies.Columns(20).Visible = False
            DataGridViewMovies.Columns(21).Visible = False
            'DataGridViewMovies.Columns(22).Visible = False
        End If

        DataGridViewMovies.RowHeadersVisible = False


        DataGridViewMovies.Columns(6).Width = 276
        DataGridViewMovies.Columns(6).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(6).ReadOnly = True
        DataGridViewMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(6).ToolTipText = "Texte tooltip"
        DataGridViewMovies.Columns(6).HeaderText = "ColomnName"
        DataGridViewMovies.Columns(22).Width = 20
        DataGridViewMovies.Columns(23).Width = 20




    End Sub


End Class
