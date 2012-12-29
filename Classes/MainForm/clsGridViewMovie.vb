Public Class clsGridViewMovie
    Public Sub GridviewMovieDesign()
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
        Form1.DataGridViewMovies.Columns.Add(imgPlot)
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
            Form1.DataGridViewMovies.Columns(0).Visible = False
            Form1.DataGridViewMovies.Columns(1).Visible = False
            Form1.DataGridViewMovies.Columns(2).Visible = False
            Form1.DataGridViewMovies.Columns(3).Visible = False
            Form1.DataGridViewMovies.Columns(4).Visible = False
            Form1.DataGridViewMovies.Columns(5).Visible = False 'title
            'DataGridViewMovies.Columns(6).Visible = False 'title and year
            Form1.DataGridViewMovies.Columns(7).Visible = False
            Form1.DataGridViewMovies.Columns(8).Visible = False
            Form1.DataGridViewMovies.Columns(9).Visible = False
            Form1.DataGridViewMovies.Columns(10).Visible = False
            Form1.DataGridViewMovies.Columns(11).Visible = False
            Form1.DataGridViewMovies.Columns(12).Visible = False 'type
            Form1.DataGridViewMovies.Columns(13).Visible = False 'playcount
            Form1.DataGridViewMovies.Columns(14).Visible = False 'plot
            Form1.DataGridViewMovies.Columns(15).Visible = False
            Form1.DataGridViewMovies.Columns(16).Visible = False 'durée
            Form1.DataGridViewMovies.Columns(17).Visible = False
            Form1.DataGridViewMovies.Columns(18).Visible = False
            Form1.DataGridViewMovies.Columns(19).Visible = False
            Form1.DataGridViewMovies.Columns(20).Visible = False
            Form1.DataGridViewMovies.Columns(21).Visible = False
            'DataGridViewMovies.Columns(22).Visible = False
        End If

        Form1.DataGridViewMovies.RowHeadersVisible = False


        Form1.DataGridViewMovies.Columns(6).Width = 290
        Form1.DataGridViewMovies.Columns(6).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(6).ReadOnly = True
        Form1.DataGridViewMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(6).ToolTipText = "Texte tooltip"
        Form1.DataGridViewMovies.Columns(6).HeaderText = "ColomnName"
        Form1.DataGridViewMovies.Columns(22).Width = 20
        Form1.DataGridViewMovies.Columns(23).Width = 20




    End Sub


End Class
