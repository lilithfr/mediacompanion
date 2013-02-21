Imports System.Linq
Imports System.Runtime.CompilerServices

Module ModGlobals2

    <Extension()> _
    Function RemoveAfterMatch(ByVal s As String, Optional match As String=" (") As String
        Dim i As Integer = s.IndexOf(match)

        If i=-1 Then Return s
        
        Return s.Substring(0,i)
    End Function

End Module




Public Class clsGridViewMovie

    Public GridFieldToDisplay1 As String
    Public GridFieldToDisplay2 As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(Form1 As Form1)

        Dim dgv As DataGridView = Form1.DataGridViewMovies

        If dgv.Columns.Count < 26 Then Return  '24
        If dgv.Rows   .Count <  1 Then Return

        Cursor.Current = Cursors.WaitCursor

        Dim imgWatched As New DataGridViewImageColumn()
        Dim inImgWatched As Image = Global.Media_Companion.My.Resources.Resources.DotGray

        While dgv.Columns(0).CellType.Name="DataGridViewImageCell"
            dgv.Columns.Remove(dgv.Columns(0))
        End While


        imgWatched.Image = inImgWatched
        If dgv.Columns.Count < 28 Then         '26
            dgv.Columns.Add(imgWatched)

        End If
        imgWatched.HeaderText = "Watched"
        imgWatched.Name       = "Watched"


        Dim imgPlot As New DataGridViewImageColumn()
        Dim inImgPlot As Image = Global.Media_Companion.My.Resources.Resources.DotGray
        imgPlot.Image = inImgPlot
        If dgv.Columns.Count < 28 Then     '26
            dgv.Columns.Add(imgPlot)
        End If
        imgPlot.HeaderText = "Plot"
        imgPlot.Name       = "ImgPlot"


        Dim header_style As New DataGridViewCellStyle

        header_style.ForeColor = Color.White
        header_style.BackColor = Color.ForestGreen
        header_style.Font      = new Font(dgv.Font, FontStyle.Bold)

        For Each col As DataGridViewcolumn in dgv.Columns
            col.HeaderCell.Style = header_style
        Next

        dgv.EnableHeadersVisualStyles = False


        'Performance tweak:
        Dim x = Global.Media_Companion.My.Resources.Movie

        'Watched icon
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("playcount").Value <> "0" Then
                row.Cells("Watched").Value = x  
            End If
        Next


        'Performance tweak:
        x = Global.Media_Companion.My.Resources.Page

        'plot icon
        For Each row As DataGridViewRow In dgv.Rows
            If row.Cells("plot").Value <> "" Then  
                row.Cells("ImgPlot").Value = x 
            End If
        Next


        For Each column As DataGridViewColumn In dgv.Columns
            column.Visible = False
        Next

          
        dgv.Columns("filename"  ).Visible = GridFieldToDisplay1 = "FileName"
        dgv.Columns("foldername").Visible = GridFieldToDisplay1 = "Folder" 

             
        If GridFieldToDisplay1="TitleAndYear" Then
            dgv.Columns("DisplayTitle"       ).Visible = GridFieldToDisplay2 =  "Movie Year"
            dgv.Columns("DisplayTitleAndYear").Visible = GridFieldToDisplay2 <> "Movie Year"
        End If

        dgv.Columns("year"      ).Visible = GridFieldToDisplay2 = "Movie Year"
        dgv.Columns("filedate"  ).Visible = GridFieldToDisplay2 = "Modified"
        dgv.Columns("rating"    ).Visible = GridFieldToDisplay2 = "Rating" 
        dgv.Columns("runtime"   ).Visible = GridFieldToDisplay2 = "Runtime"
        dgv.Columns("createdate").Visible = GridFieldToDisplay2 = "Date Added" 
        dgv.Columns("votes"     ).Visible = GridFieldToDisplay2 = "Votes" 
        dgv.Columns("Watched"   ).Visible = True
        dgv.Columns("ImgPlot"   ).Visible = True


        dgv.RowHeadersVisible = False

        dgv.Columns("filename").Width = 226
        dgv.Columns("filename").Resizable = DataGridViewTriState.True
        dgv.Columns("filename").ReadOnly = True
        dgv.Columns("filename").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("filename").ToolTipText = "File name"
        dgv.Columns("filename").HeaderText = "File name"

        dgv.Columns("foldername").Width = 226
        dgv.Columns("foldername").Resizable = DataGridViewTriState.True
        dgv.Columns("foldername").ReadOnly = True
        dgv.Columns("foldername").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("foldername").ToolTipText = "Folder name"
        dgv.Columns("foldername").HeaderText = "Folder name"

        dgv.Columns("DisplayTitle").Width = 226
        dgv.Columns("DisplayTitle").Resizable = DataGridViewTriState.True
        dgv.Columns("DisplayTitle").ReadOnly = True
        dgv.Columns("DisplayTitle").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("DisplayTitle").ToolTipText = "Title"
        dgv.Columns("DisplayTitle").HeaderText = "Title"

        dgv.Columns("DisplayTitleAndYear").Width = 226
        dgv.Columns("DisplayTitleAndYear").Resizable = DataGridViewTriState.True
        dgv.Columns("DisplayTitleAndYear").ReadOnly = True
        dgv.Columns("DisplayTitleAndYear").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("DisplayTitleAndYear").ToolTipText = "Title & Year"
        dgv.Columns("DisplayTitleAndYear").HeaderText = "Title & Year"

        dgv.Columns("year").Width = 35
        dgv.Columns("year").Resizable = DataGridViewTriState.True
        dgv.Columns("year").ReadOnly = True
        dgv.Columns("year").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("year").ToolTipText = "Movie year"
        dgv.Columns("year").HeaderText = "Year"

        dgv.Columns("filedate").Width = 95
        dgv.Columns("filedate").Resizable = DataGridViewTriState.True
        dgv.Columns("filedate").ReadOnly = True
        dgv.Columns("filedate").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("filedate").ToolTipText = "Modified"
        dgv.Columns("filedate").HeaderText = "Date"

        dgv.Columns("rating").Width = 50
        dgv.Columns("rating").Resizable = DataGridViewTriState.True
        dgv.Columns("rating").ReadOnly = True
        dgv.Columns("rating").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("rating").ToolTipText = "Rating"
        dgv.Columns("rating").HeaderText = "Rating"

        dgv.Columns("runtime").Width = 58
        dgv.Columns("runtime").Resizable = DataGridViewTriState.True
        dgv.Columns("runtime").ReadOnly = True
        dgv.Columns("runtime").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("runtime").ToolTipText = "Runtime"
        dgv.Columns("runtime").HeaderText = "Runtime"

        dgv.Columns("createdate").Width = 95
        dgv.Columns("createdate").Resizable = DataGridViewTriState.True
        dgv.Columns("createdate").ReadOnly = True
        dgv.Columns("createdate").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("createdate").ToolTipText = "Date Added"
        dgv.Columns("createdate").HeaderText = "Date Added"

        dgv.Columns("votes").Width = 50
        dgv.Columns("votes").Resizable = DataGridViewTriState.True
        dgv.Columns("votes").ReadOnly = True
        dgv.Columns("votes").SortMode = DataGridViewColumnSortMode.Automatic
        dgv.Columns("votes").ToolTipText = "Votes"
        dgv.Columns("votes").HeaderText = "Votes"

        dgv.Columns("Watched").Width = 20                        '24
        dgv.Columns("Watched").ToolTipText = "Watched Status"
        dgv.Columns("Watched").HeaderText = "W"

        dgv.Columns("ImgPlot").Width = 20                        '25
        dgv.Columns("ImgPlot").ToolTipText = "Plot"
        dgv.Columns("ImgPlot").HeaderText = "P"

        SetDataGridViewMoviesFirstColumnWidth(dgv)

        Cursor.Current = Cursors.Default
        'dgv.Refresh()
    End Sub

    Sub SetDataGridViewMoviesFirstColumnWidth(dgvMovies As DataGridView)

        Const w As Integer = 58

        Dim FirstColumnSize As Integer

        If GridFieldToDisplay2 = "A - Z"      Then FirstColumnSize = dgvMovies.Width - w
        If GridFieldToDisplay2 = "Sort Order" Then FirstColumnSize = dgvMovies.Width - w
        If GridFieldToDisplay2 = "Movie Year" Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("year"      ).Width - w
        If GridFieldToDisplay2 = "Modified"   Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("filedate"  ).Width - w
        If GridFieldToDisplay2 = "Rating"     Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("rating"    ).Width - w
        If GridFieldToDisplay2 = "Runtime"    Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("runtime"   ).Width - w
        If GridFieldToDisplay2 = "Date Added" Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("createdate").Width - w
        If GridFieldToDisplay2 = "Votes"      Then FirstColumnSize = dgvMovies.Width - dgvMovies.Columns("votes"     ).Width - w

        Try
            If FirstColumnSize>0 Then
                dgvMovies.Columns("filename"           ).Width = FirstColumnSize
                dgvMovies.Columns("foldername"         ).Width = FirstColumnSize
                dgvMovies.Columns("DisplayTitle"       ).Width = FirstColumnSize
                dgvMovies.Columns("DisplayTitleAndYear").Width = FirstColumnSize
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

        Dim b = From f In Form1.oMovies.Data_GridViewMovieCache Where f.TitleUcase.Contains(Form1.txt_titlesearch.Text.ToUpper)


        'General
        Select Form1.cbFilterGeneral.Text

            Case "Watched"        : b = From f In b Where f.playcount <> "0"

            Case "Unwatched"      : b = From f In b Where f.playcount  = "0"

            Case "Duplicates"     : Dim sort = b.GroupBy(Function(f) f.id) : b = sort.Where(Function(x) x.Count>1).SelectMany(Function(x) x).ToList

            Case "Missing Poster" : b = From f In b Where f.missingdata1 = "2" Or f.missingdata1 = "3"

            Case "Missing Fanart" : b = From f In b Where f.missingdata1 = "1" Or f.missingdata1 = "3"

            Case "Missing Plot"   : b = From f In b Where f.plot.ToString.Trim = ""
        End Select

        If Yield Then Return


        'Genre
        If Form1.cbFilterGenre.Text <> "All" Then
            b = From f In b Where f.genre.Contains(Form1.cbFilterGenre.Text.RemoveAfterMatch)
            If Yield Then Return
        End If
        

        'Set
        If Form1.SetFilter<>"" Then
            b = From f In b Where f.movieset.Contains(Form1.SetFilter)
            If Yield Then Return
        End If


        'Actor
        If Form1.ActorFilter<>"" then
            Dim movie_ids As New List(Of String) 

            For Each actor In Form1.oMovies.ActorDb
                If actor.actorname = Form1.ActorFilter Then
                    movie_ids.Add(actor.movieid)
                End If
            Next

            b = (From f In b).Where( Function(c) movie_ids.Contains(c.id) )

            If Yield Then Return
        End If


        'Source
        If Form1.cbFilterSource.Text <> "All" Then
            b = From f In b Where f.source.Contains(Form1.cbFilterSource.Text)
            If Yield Then Return
        End If
        


        Select Case Form1.cbSort.Text
            Case "A - Z"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.DisplayTitle Ascending            'DisplayTitleAndYear
                Else
                    b = From f In b Order By f.DisplayTitle Descending           'DisplayTitleAndYear
                End If
            Case "Movie Year"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.year Ascending
                Else
                    b = From f In b Order By f.year Descending
                End If
            Case "Modified"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.filedate Ascending
                Else
                    b = From f In b Order By f.filedate Descending
                End If
            Case "Runtime"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.IntRuntime Ascending        
                Else
                    b = From f In b Order By f.IntRuntime Descending
                End If
            Case "Rating"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.rating Ascending
                Else
                    b = From f In b Order By f.rating Descending
                End If
            Case "Sort(Order)"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.createdate Descending
                Else
                    b = From f In b Order By f.createdate Ascending
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

        'Form1.DataGridViewMovies.DataSource = lst

        Form1.DataGridViewBindingSource.DataSource = lst
        Form1.DataGridViewMovies       .DataSource = Form1.DataGridViewBindingSource


        If Yield Then Return

        GridviewMovieDesign(Form1)

        Form1.LabelCountFilter.Text = "Displaying " & lst.Count.ToString & " of " & Form1.oMovies.MovieCache.Count
    End Sub

End Class
