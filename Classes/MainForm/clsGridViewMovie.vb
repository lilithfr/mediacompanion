Imports System.Linq

Public Class clsGridViewMovie

    Public GridFieldToDisplay1 As String
    Public GridFieldToDisplay2 As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(Form1 As Form1)


        If Form1.DataGridViewMovies.Columns.Count < 24 Then Return
        If Form1.DataGridViewMovies.Rows   .Count <  1 Then Return


        Cursor.Current = Cursors.WaitCursor

        Dim imgWatched As New DataGridViewImageColumn()
        Dim inImgWatched As Image = Global.Media_Companion.My.Resources.Resources.DotGray


        While Form1.DataGridViewMovies.Columns(0).CellType.Name="DataGridViewImageCell"
            Form1.DataGridViewMovies.Columns.Remove(Form1.DataGridViewMovies.Columns(0))
        End While


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
        header_style.Font      = new Font(Form1.DataGridViewMovies.Font, FontStyle.Bold)

        For Each col As DataGridViewcolumn in Form1.DataGridViewMovies.Columns
	        col.HeaderCell.Style = header_style
        Next

        Form1.DataGridViewMovies.EnableHeadersVisualStyles = False


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
            Form1.DataGridViewMovies.Columns(0).Visible = False
            Form1.DataGridViewMovies.Columns(1).Visible = False
            If GridFieldToDisplay1 = "FileName" Then
                Form1.DataGridViewMovies.Columns(2).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(2).Visible = False
            End If
            If GridFieldToDisplay1 = "Folder" Then
                Form1.DataGridViewMovies.Columns(3).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(3).Visible = False
            End If
            Form1.DataGridViewMovies.Columns(4).Visible = False
            Form1.DataGridViewMovies.Columns(5).Visible = False 
            Form1.DataGridViewMovies.Columns(6).Visible = False

            If GridFieldToDisplay1 = "TiteAndYear" Then
                Form1.DataGridViewMovies.Columns(4).Visible = GridFieldToDisplay2="Movie Year"
                Form1.DataGridViewMovies.Columns(6).Visible = GridFieldToDisplay2<>"Movie Year"
            End If

            If GridFieldToDisplay2 = "Movie Year" Then
                Form1.DataGridViewMovies.Columns(7).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(7).Visible = False
            End If


            If GridFieldToDisplay2 = "Modified" Then
                Form1.DataGridViewMovies.Columns(8).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(8).Visible = False
            End If
            Form1.DataGridViewMovies.Columns(9).Visible = False

            If GridFieldToDisplay2 = "Rating" Then
                Form1.DataGridViewMovies.Columns(10).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(10).Visible = False
            End If

            Form1.DataGridViewMovies.Columns(11).Visible = False
            Form1.DataGridViewMovies.Columns(12).Visible = False 'genre
            Form1.DataGridViewMovies.Columns(13).Visible = False 'playcount
            Form1.DataGridViewMovies.Columns(14).Visible = False '
            Form1.DataGridViewMovies.Columns(15).Visible = False
            If GridFieldToDisplay2 = "Runtime" Then
                Form1.DataGridViewMovies.Columns(16).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(16).Visible = False
            End If
            If GridFieldToDisplay2 = "Date Added" Then
                Form1.DataGridViewMovies.Columns(17).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(17).Visible = False
            End If
            Form1.DataGridViewMovies.Columns(18).Visible = False
            Form1.DataGridViewMovies.Columns(19).Visible = False
            Form1.DataGridViewMovies.Columns(20).Visible = False
   '         Form1.DataGridViewMovies.Columns(21).Visible = False
            Form1.DataGridViewMovies.Columns(22).Visible = False

            If GridFieldToDisplay2 = "Votes" Then
                Form1.DataGridViewMovies.Columns(21).Visible = True
            Else
                Form1.DataGridViewMovies.Columns(21).Visible = False
            End If

            Form1.DataGridViewMovies.Columns(23).Visible = False
            Form1.DataGridViewMovies.Columns(24).Visible = True
            Form1.DataGridViewMovies.Columns(25).Visible = True
        End If

        Form1.DataGridViewMovies.RowHeadersVisible = False

        Form1.DataGridViewMovies.Columns(2).Width = 226
        Form1.DataGridViewMovies.Columns(2).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(2).ReadOnly = True
        Form1.DataGridViewMovies.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(2).ToolTipText = "File name"
        Form1.DataGridViewMovies.Columns(2).HeaderText = "File name"

        Form1.DataGridViewMovies.Columns(3).Width = 226
        Form1.DataGridViewMovies.Columns(3).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(3).ReadOnly = True
        Form1.DataGridViewMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(3).ToolTipText = "Folder name"
        Form1.DataGridViewMovies.Columns(3).HeaderText = "Folder name"

        Form1.DataGridViewMovies.Columns(4).Width = 226
        Form1.DataGridViewMovies.Columns(4).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(4).ReadOnly = True
        Form1.DataGridViewMovies.Columns(4).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(4).ToolTipText = "Movie titles"
        Form1.DataGridViewMovies.Columns(4).HeaderText = "Movies"

        Form1.DataGridViewMovies.Columns(6).Width = 226
        Form1.DataGridViewMovies.Columns(6).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(6).ReadOnly = True
        Form1.DataGridViewMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(6).ToolTipText = "Movie titles"
        Form1.DataGridViewMovies.Columns(6).HeaderText = "Movies"

        Form1.DataGridViewMovies.Columns(7).Width = 35
        Form1.DataGridViewMovies.Columns(7).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(7).ReadOnly = True
        Form1.DataGridViewMovies.Columns(7).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(7).ToolTipText = "Movie year"
        Form1.DataGridViewMovies.Columns(7).HeaderText = "Year"

        Form1.DataGridViewMovies.Columns(8).Width = 95
        Form1.DataGridViewMovies.Columns(8).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(8).ReadOnly = True
        Form1.DataGridViewMovies.Columns(8).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(8).ToolTipText = "Modified"
        Form1.DataGridViewMovies.Columns(8).HeaderText = "Date"

        Form1.DataGridViewMovies.Columns(10).Width = 50
        Form1.DataGridViewMovies.Columns(10).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(10).ReadOnly = True
        Form1.DataGridViewMovies.Columns(10).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(10).ToolTipText = "Rating"
        Form1.DataGridViewMovies.Columns(10).HeaderText = "Rating"

        Form1.DataGridViewMovies.Columns(16).Width = 58
        Form1.DataGridViewMovies.Columns(16).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(16).ReadOnly = True
        Form1.DataGridViewMovies.Columns(16).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(16).ToolTipText = "Runtime"
        Form1.DataGridViewMovies.Columns(16).HeaderText = "Runtime"

        Form1.DataGridViewMovies.Columns(17).Width = 95
        Form1.DataGridViewMovies.Columns(17).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(17).ReadOnly = True
        Form1.DataGridViewMovies.Columns(17).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(17).ToolTipText = "Date Added"
        Form1.DataGridViewMovies.Columns(17).HeaderText = "Date Added"

        Form1.DataGridViewMovies.Columns(21).Width = 50
        Form1.DataGridViewMovies.Columns(21).Resizable = DataGridViewTriState.True
        Form1.DataGridViewMovies.Columns(21).ReadOnly = True
        Form1.DataGridViewMovies.Columns(21).SortMode = DataGridViewColumnSortMode.Automatic
        Form1.DataGridViewMovies.Columns(21).ToolTipText = "Votes"
        Form1.DataGridViewMovies.Columns(21).HeaderText = "Votes"

        Form1.DataGridViewMovies.Columns(24).Width = 20
        Form1.DataGridViewMovies.Columns(24).ToolTipText = "Watched Status"
        Form1.DataGridViewMovies.Columns(24).HeaderText = "W"

        Form1.DataGridViewMovies.Columns(25).Width = 20
        Form1.DataGridViewMovies.Columns(25).ToolTipText = "Plot"
        Form1.DataGridViewMovies.Columns(25).HeaderText = "P"

        SetDataGridViewMoviesFirstColumnWidth(Form1.DataGridViewMovies)

        Cursor.Current = Cursors.Default
        'Form1.DataGridViewMovies.Refresh()
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

        'Form1.DataGridViewMovies.Columns.Clear()
        'Form1.DataGridViewBindingSource.DataSource = Nothing

        If Yield Then Return

        Dim b = From f In Form1.filteredListObj Where f.TitleUcase.Contains(Form1.txt_titlesearch.Text.ToUpper)

        If Yield Then Return

        Dim movie_ids As New List(Of String) 

        If Form1.ActorFilter<>"" then

            For Each actor In Form1.oMovies.ActorDb
                If actor.actorname = Form1.ActorFilter Then
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

        Dim lst = b.ToList

        Form1.DataGridViewBindingSource.DataSource = lst

        If Yield Then Return

        GridviewMovieDesign(Form1)


        Form1.LabelCountFilter.Text = "Displaying " & lst.Count.ToString & " of  " & Form1.oMovies.MovieCache.Count & " movies"

        Return

    End Sub

End Class
