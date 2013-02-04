Imports System.Linq

Public Class clsGridViewMovie

    Public GridFieldToDisplay1 As String
    Public GridFieldToDisplay2 As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(Form1 As Form1, ByVal DataGridViewMovies As DataGridView)

        Cursor.Current = Cursors.WaitCursor

        Dim imgWatched As New DataGridViewImageColumn()
        Dim inImgWatched As Image = Global.Media_Companion.My.Resources.Resources.DotGray

        If DataGridViewMovies.Rows.Count < 1 Then Return

        imgWatched.Image = inImgWatched
        If Form1.DataGridViewMovies.Columns.Count < 26 Then
            Form1.DataGridViewMovies.Columns.Add(imgWatched)
        End If
        imgWatched.HeaderText = "Watched"
        imgWatched.Name = "Watched"

        Dim imgPlot As New DataGridViewImageColumn()
        Dim inImgPlot As Image = Global.Media_Companion.My.Resources.Resources.DotGray
        imgPlot.Image = inImgPlot
        If Form1.DataGridViewMovies.Columns.Count < 26 Then
            Form1.DataGridViewMovies.Columns.Add(imgPlot)
        End If
        imgPlot.HeaderText = "Plot"
        imgPlot.Name = "Plot"

        Dim header_style As New DataGridViewCellStyle

        header_style.ForeColor = Color.White
        header_style.BackColor = Color.ForestGreen
        header_style.Font      = new Font(DataGridViewMovies.Font, FontStyle.Bold)

        For Each col As DataGridViewcolumn in DataGridViewMovies.Columns
	        col.HeaderCell.Style = header_style
        Next

        DataGridViewMovies.EnableHeadersVisualStyles = False


        'Performance tweak:
        Dim x = Global.Media_Companion.My.Resources.Movie

        'Dim watchedTm As New Times
        'watchedTm.StartTm = DateTime.Now

        'Watched icon
        For Each row As DataGridViewRow In Form1.DataGridViewMovies.Rows
            If row.Cells(13).Value = "1" Then
                row.Cells(24).Value = x
            End If
        Next

        'watchedTm.EndTm = DateTime.Now
        'Dim plotTm As New Times
        'plotTm.StartTm = DateTime.Now

        'Performance tweak:
        x = Global.Media_Companion.My.Resources.Page

        'plot icon
        For Each row As DataGridViewRow In Form1.DataGridViewMovies.Rows
            If row.Cells(19).Value <> "" Then
                row.Cells(25).Value = x
            End If
        Next

        'plotTm.EndTm = DateTime.Now


        Dim debug As Boolean = False
        If debug = False Then
            DataGridViewMovies.Columns(0).Visible = False
            DataGridViewMovies.Columns(1).Visible = False
            If GridFieldToDisplay1 = "FileName" Then
                DataGridViewMovies.Columns(2).Visible = True
            Else
                DataGridViewMovies.Columns(2).Visible = False
            End If
            If GridFieldToDisplay1 = "Folder" Then
                DataGridViewMovies.Columns(3).Visible = True
            Else
                DataGridViewMovies.Columns(3).Visible = False
            End If
            DataGridViewMovies.Columns(4).Visible = False
            DataGridViewMovies.Columns(5).Visible = False 
            DataGridViewMovies.Columns(6).Visible = False

            If GridFieldToDisplay1 = "TiteAndYear" Then
                DataGridViewMovies.Columns(4).Visible = GridFieldToDisplay2="Movie Year"
                DataGridViewMovies.Columns(6).Visible = GridFieldToDisplay2<>"Movie Year"
            End If

            If GridFieldToDisplay2 = "Movie Year" Then
                DataGridViewMovies.Columns(7).Visible = True
            Else
                DataGridViewMovies.Columns(7).Visible = False
            End If


            If GridFieldToDisplay2 = "Modified" Then
                DataGridViewMovies.Columns(8).Visible = True
            Else
                DataGridViewMovies.Columns(8).Visible = False
            End If
            DataGridViewMovies.Columns(9).Visible = False

            If GridFieldToDisplay2 = "Rating" Then
                DataGridViewMovies.Columns(10).Visible = True
            Else
                DataGridViewMovies.Columns(10).Visible = False
            End If

            DataGridViewMovies.Columns(11).Visible = False
            DataGridViewMovies.Columns(12).Visible = False 'genre
            DataGridViewMovies.Columns(13).Visible = False 'playcount
            DataGridViewMovies.Columns(14).Visible = False '
            DataGridViewMovies.Columns(15).Visible = False
            If GridFieldToDisplay2 = "Runtime" Then
                DataGridViewMovies.Columns(16).Visible = True
            Else
                DataGridViewMovies.Columns(16).Visible = False
            End If
            If GridFieldToDisplay2 = "Date Added" Then
                DataGridViewMovies.Columns(17).Visible = True
            Else
                DataGridViewMovies.Columns(17).Visible = False
            End If
            DataGridViewMovies.Columns(18).Visible = False
            DataGridViewMovies.Columns(19).Visible = False
            DataGridViewMovies.Columns(20).Visible = False
   '         DataGridViewMovies.Columns(21).Visible = False
            DataGridViewMovies.Columns(22).Visible = False

            If GridFieldToDisplay2 = "Votes" Then
                DataGridViewMovies.Columns(21).Visible = True
            Else
                DataGridViewMovies.Columns(21).Visible = False
            End If

            DataGridViewMovies.Columns(23).Visible = False
            DataGridViewMovies.Columns(24).Visible = True
            DataGridViewMovies.Columns(25).Visible = True
        End If

        DataGridViewMovies.RowHeadersVisible = False

        DataGridViewMovies.Columns(2).Width = 226
        DataGridViewMovies.Columns(2).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(2).ReadOnly = True
        DataGridViewMovies.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(2).ToolTipText = "File name"
        DataGridViewMovies.Columns(2).HeaderText = "File name"

        DataGridViewMovies.Columns(3).Width = 226
        DataGridViewMovies.Columns(3).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(3).ReadOnly = True
        DataGridViewMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(3).ToolTipText = "Folder name"
        DataGridViewMovies.Columns(3).HeaderText = "Folder name"

        DataGridViewMovies.Columns(4).Width = 226
        DataGridViewMovies.Columns(4).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(4).ReadOnly = True
        DataGridViewMovies.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(4).ToolTipText = "Movie titles"
        DataGridViewMovies.Columns(4).HeaderText = "Movies"

        DataGridViewMovies.Columns(6).Width = 226
        DataGridViewMovies.Columns(6).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(6).ReadOnly = True
        DataGridViewMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(6).ToolTipText = "Movie titles"
        DataGridViewMovies.Columns(6).HeaderText = "Movies"

        DataGridViewMovies.Columns(7).Width = 35
        DataGridViewMovies.Columns(7).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(7).ReadOnly = True
        DataGridViewMovies.Columns(7).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(7).ToolTipText = "Movie year"
        DataGridViewMovies.Columns(7).HeaderText = "Year"

        DataGridViewMovies.Columns(8).Width = 95
        DataGridViewMovies.Columns(8).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(8).ReadOnly = True
        DataGridViewMovies.Columns(8).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(8).ToolTipText = "Modified"
        DataGridViewMovies.Columns(8).HeaderText = "Date"

        DataGridViewMovies.Columns(10).Width = 50
        DataGridViewMovies.Columns(10).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(10).ReadOnly = True
        DataGridViewMovies.Columns(10).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(10).ToolTipText = "Rating"
        DataGridViewMovies.Columns(10).HeaderText = "Rating"

        DataGridViewMovies.Columns(16).Width = 58
        DataGridViewMovies.Columns(16).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(16).ReadOnly = True
        DataGridViewMovies.Columns(16).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(16).ToolTipText = "Runtime"
        DataGridViewMovies.Columns(16).HeaderText = "Runtime"

        DataGridViewMovies.Columns(17).Width = 95
        DataGridViewMovies.Columns(17).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(17).ReadOnly = True
        DataGridViewMovies.Columns(17).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(17).ToolTipText = "Date Added"
        DataGridViewMovies.Columns(17).HeaderText = "Date Added"

        DataGridViewMovies.Columns(21).Width = 50
        DataGridViewMovies.Columns(21).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(21).ReadOnly = True
        DataGridViewMovies.Columns(21).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(21).ToolTipText = "Votes"
        DataGridViewMovies.Columns(21).HeaderText = "Votes"

        DataGridViewMovies.Columns(24).Width = 20
        DataGridViewMovies.Columns(24).ToolTipText = "Watched Status"
        DataGridViewMovies.Columns(24).HeaderText = "W"

        DataGridViewMovies.Columns(25).Width = 20
        DataGridViewMovies.Columns(25).ToolTipText = "Plot"
        DataGridViewMovies.Columns(25).HeaderText = "P"

        SetDataGridViewMoviesFirstColumnWidth(DataGridViewMovies)
        Cursor.Current = Cursors.Default
        'DataGridViewMovies.Refresh()
    End Sub

    Sub SetDataGridViewMoviesFirstColumnWidth(DataGridViewMovies As DataGridView)
        Dim FirstColumnSize As Integer

        If GridFieldToDisplay2 = "A - Z" Then FirstColumnSize = DataGridViewMovies.Width - 58
        If GridFieldToDisplay2 = "Sort Order" Then FirstColumnSize = DataGridViewMovies.Width - 58
        If GridFieldToDisplay2 = "Movie Year" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(7).Width - 58
        If GridFieldToDisplay2 = "Modified" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(8).Width - 58
        If GridFieldToDisplay2 = "Rating" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(10).Width - 58
        If GridFieldToDisplay2 = "Runtime" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(16).Width - 58
        If GridFieldToDisplay2 = "Date Added" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(17).Width - 58
        If GridFieldToDisplay2 = "Votes" Then FirstColumnSize = DataGridViewMovies.Width - DataGridViewMovies.Columns(21).Width - 58

        Try
            If FirstColumnSize>0 Then
                DataGridViewMovies.Columns(2).Width = FirstColumnSize
                DataGridViewMovies.Columns(3).Width = FirstColumnSize
                DataGridViewMovies.Columns(4).Width = FirstColumnSize
                DataGridViewMovies.Columns(6).Width = FirstColumnSize
            End If
        Catch
        End Try
    End Sub

    Public Function Yield As Boolean
        Dim yld As Boolean=False
        Try
            Application.DoEvents
            yld = Form1._yield
        Catch
        End Try
        
        Return yld
    End Function

    Public Sub mov_FiltersAndSortApply(Form1 As Form1)

        If Yield Then Return

        Dim b = From f In Form1.filteredListObj Where f.TitleUcase.Contains(Form1.txt_titlesearch.Text.ToUpper)

        If Yield Then Return

        Dim movie_ids As New List(Of String) 

        If Form1.FilteringByActor then
            Dim topactorname As String = Form1.actorcb.Text

            For Each actor In Form1.oMovies.ActorDb
                If actor.actorname = Form1.actorcb.Text Then
                    movie_ids.Add(actor.movieid)
                End If
            Next

            If Yield Then Return

            b = (From f In b).Where( Function(c) movie_ids.Contains(c.id) )
        End If


        If Form1.FilteringBySet Then
            b = From f In b Where f.movieset.ToLower.Contains(Form1.setsTxt.Text.ToLower)
        End If

        'b = From f In b Order By f.filename Ascending

        If Yield Then Return

        If Form1.RadioButtonAll.Checked = True Then b = From f In b

        If Yield Then Return

        If Form1.RadioButtonWatched.Checked = True Then b = From f In b Where f.playcount = "1"
 
        If Yield Then Return

       If Form1.RadioButtonUnWatched.Checked = True Then b = From f In b Where f.playcount = "0"

        If Yield Then Return

        If Form1.RadioButtonMissingPosters.Checked = True Then b = From f In b Where f.missingdata1 = "2" Or f.missingdata1 = "3"

        If Yield Then Return

        If Form1.RadioButtonMissingFanart.Checked = True Then b = From f In b Where f.missingdata1 = "1" Or f.missingdata1 = "3"
 
        If Yield Then Return

       If Form1.RadioButtonMissingPlot.Checked = True Then b = From f In b Where f.plot.ToString.Trim = ""
 
        If Yield Then Return

       If Form1.RadioButtonDuplicates.Checked = True Then
            Dim sort = b.GroupBy(Function(f) f.id)
            b = sort.Where(Function(x) x.Count > 1).SelectMany(Function(x) x).ToList()
        End If

        If Yield Then Return

        'Genre
        If Form1.ComboBoxFilterGenre.Text <> "All" Then
            b = From f In b Where f.genre.Contains(Form1.ComboBoxFilterGenre.Text)
        End If

        If Yield Then Return

        'Movie Format
        If Form1.ComboBoxFilterMovieFormat.Text <> "All" Then
            b = From f In b Where f.source.Contains(Form1.ComboBoxFilterMovieFormat.Text)
        End If

        If Yield Then Return

        'MessageBox.Show("#" & Form1.cbSort.Text & "#")

        Select Case Form1.cbSort.Text
            Case "A - Z"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.titleandyear Ascending
                Else
                    b = From f In b Order By f.titleandyear Descending
                End If
            Case "Movie Year"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.year Ascending
                Else
                    b = From f In b Order By f.year Descending
                End If
            Case "Modified"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.createdate Ascending
                Else
                    b = From f In b Order By f.createdate Descending
                End If
            Case "Runtime"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.runtime Ascending
                Else
                    b = From f In b Order By f.runtime Descending
                End If
            Case "Rating"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.rating Ascending
                Else
                    b = From f In b Order By f.rating Descending
                End If
            Case "Sort(Order)"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.createdate Ascending
                Else
                    b = From f In b Order By f.createdate Descending
                End If
            Case "Date Added"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.createdate Descending
                Else
                    b = From f In b Order By f.createdate Ascending
                End If
            Case "Votes"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.IntVotes Ascending
                Else
                    b = From f In b Order By f.IntVotes Descending
                End If
        End Select

        If Yield Then Return

        Form1.DataGridViewMovies.Columns.Clear()
        Form1.DataGridViewBindingSource.DataSource = b

        If Yield Then Return

        GridviewMovieDesign(Form1,Form1.DataGridViewMovies)


        Form1.LabelCountFilter.Text = "Displaying " & b.Count.ToString & " of  " & Form1.oMovies.MovieCache.Count & " movies"

        Return

    End Sub

End Class
