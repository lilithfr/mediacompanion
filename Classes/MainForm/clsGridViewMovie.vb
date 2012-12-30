﻿Imports System.Linq
Imports System.Threading


Public Class clsGridViewMovie

    Public GridFieldToDisplay As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(ByVal DataGridViewMovies As DataGridView)
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
            If GridFieldToDisplay = "FileName" Then
                DataGridViewMovies.Columns(2).Visible = True
            Else
                DataGridViewMovies.Columns(2).Visible = False
            End If
            If GridFieldToDisplay = "Folder" Then
                DataGridViewMovies.Columns(3).Visible = True
            Else
                DataGridViewMovies.Columns(3).Visible = False
            End If
            DataGridViewMovies.Columns(4).Visible = False
            DataGridViewMovies.Columns(5).Visible = False 'title
            If GridFieldToDisplay = "TiteAndYear" Then
                DataGridViewMovies.Columns(6).Visible = True
            Else
                DataGridViewMovies.Columns(6).Visible = False
            End If
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

        DataGridViewMovies.Columns(2).Width = 276
        DataGridViewMovies.Columns(2).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(2).ReadOnly = True
        DataGridViewMovies.Columns(2).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(2).ToolTipText = "File Name"
        DataGridViewMovies.Columns(2).HeaderText = "File Name"

        DataGridViewMovies.Columns(3).Width = 276
        DataGridViewMovies.Columns(3).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(3).ReadOnly = True
        DataGridViewMovies.Columns(3).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(3).ToolTipText = "Folder Name"
        DataGridViewMovies.Columns(3).HeaderText = "Folder Name"

        DataGridViewMovies.Columns(6).Width = 276
        DataGridViewMovies.Columns(6).Resizable = DataGridViewTriState.True
        DataGridViewMovies.Columns(6).ReadOnly = True
        DataGridViewMovies.Columns(6).SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewMovies.Columns(6).ToolTipText = "Movies titles"
        DataGridViewMovies.Columns(6).HeaderText = "Movies"

        DataGridViewMovies.Columns(22).Width = 20
        DataGridViewMovies.Columns(22).ToolTipText = "Watched Status"
        DataGridViewMovies.Columns(22).HeaderText = "W"
        DataGridViewMovies.Columns(23).Width = 20
        DataGridViewMovies.Columns(23).ToolTipText = "Plot"
        DataGridViewMovies.Columns(23).HeaderText = "P"
    End Sub


    Public Sub mov_FiltersAndSortApply()
        Monitor.Enter(Me)
        'Try
        'Dim tempint2 As Integer = filteredList.Count - 1
        'For f = tempint2 To 0 Step -1
        '    filteredList.RemoveAt(f)
        'Next
        Dim dupes2 As Boolean = False
        Dim top250 As Boolean = False
        Dim offline As Boolean = False
        Dim watched As Boolean = False
        Dim unwatched As Boolean = False
        Dim missposters As Boolean = False
        Dim missfanart As Boolean = False
        Dim oktoadd As Boolean = True
        Dim tempint As Integer = Form1.fullMovieList.Count - 1

        If tempint < 0 Then
            Exit Sub
            Monitor.Exit(Me)
        End If

        Dim b = From f In Form1.filteredListObj Where f.title Like "*" & Form1.txt_titlesearch.Text & "*"

        b = From f In b Order By f.filename Ascending


        If Form1.RadioButtonAll.Checked = True Then
            b = From f In b
        End If

        If Form1.RadioButtonWatched.Checked = True Then
            b = From f In b Where f.playcount = "1"
        End If

        If Form1.RadioButtonUnWatched.Checked = True Then
            b = From f In b Where f.playcount = "0"
        End If

        If Form1.RadioButtonMissingPosters.Checked = True Then
            b = From f In b Where f.missingdata1 <> "2" And f.missingdata1 <> "3"
        End If

        If Form1.RadioButtonMissingFanart.Checked = True Then
            b = From f In b Where f.missingdata1 <> "1" And f.missingdata1 <> "3"
        End If

        If Form1.RadioButtonDuplicates.Checked = True Then
            'Not Emplemented
        End If



        'lst.Sort();  //O(nlogn) part.

        'var duplicatedItems = lst.Skip(1).Where((x,index)=>x.ID == lst[index].ID);

        Select Case Form1.cbSort.Text
            Case "a(-Z)"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.filename Ascending
                Else
                    b = From f In b Order By f.filename Descending
                End If
            Case "Movie(Year)"
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
                    b = From f In b Order By f.createdate Ascending
                Else
                    b = From f In b Order By f.createdate Descending
                End If
            Case "Votes"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.votes Ascending
                Else
                    b = From f In b Order By f.votes Descending
                End If
        End Select


        'Convert query to list
        'Dim Clist As List(Of Data_GridViewMovie) = b.ToList()
        'filteredListObj = Clist

        Form1.DataGridViewMovies.Columns.Clear()
        Form1.DataGridViewBindingSource.DataSource = b
        GridviewMovieDesign(Form1.DataGridViewMovies)

        Form1.LabelCountFilter.Text = "Displaying " & b.Count.ToString & " of  " & Form1.fullMovieList.Count & " movies"

        Return



        ' Added this section because ApplyFilter is often called as ApplyFilter() & doesn't take into account that a filter choice may have been set.... 
        'If RadioButton46.Checked = True Then Filter = "watched"
        'If RadioButton47.Checked = True Then Filter = "unwatched"
        'If RadioButton48.Checked = True Then Filter = "duplicates"
        'If RadioButton49.Checked = True Then Filter = "missing posters"
        'If RadioButtonMissingFanart.Checked = True Then Filter = "missing fanart"


        'If Filter = "blabla" Then           'i.e. applyFilters() {NONE}
        'RadioButton45.Checked = True    'reset filter radio buttons indcation to ALL
        'ComboBox11.SelectedIndex = 0    'reset filename video type filter to ALL       
        'End If


        'For f = 0 To tempint
        '    dupes2 = False
        '    offline = False
        '    top250 = False
        '    watched = False
        '    unwatched = False
        '    missposters = False
        '    missfanart = False
        '    oktoadd = True
        '    'If Filter.ToLower = "duplicates" Then dupes2 = True
        '    'If Filter.ToLower = "offline movies" Then offline = True
        '    'If Filter.ToLower = "watched" Then watched = True
        '    'If Filter.ToLower = "unwatched" Then unwatched = True
        '    'If Filter.ToLower = "missing posters" Then missposters = True
        '    'If Filter.ToLower = "missing fanart" Then missfanart = True
        '    If oktoadd = True And top250 = True Then
        '        If fullMovieList(f).top250 = "0" Then
        '            oktoadd = False
        '        End If
        '    End If
        '    If oktoadd = True And missposters = True Then
        '        If fullMovieList(f).missingdata1 <> 2 And fullMovieList(f).missingdata1 <> 3 Then
        '            oktoadd = False
        '        End If
        '    End If
        '    If oktoadd = True And missfanart = True Then
        '        If fullMovieList(f).missingdata1 <> 1 And fullMovieList(f).missingdata1 <> 3 Then
        '            oktoadd = False
        '        End If
        '    End If
        '    If oktoadd = True And watched = True Then
        '        If fullMovieList(f).playcount <> Nothing Then
        '            If Convert.ToInt32(fullMovieList(f).playcount) = 0 Then
        '                oktoadd = False
        '            End If
        '        Else
        '            oktoadd = False
        '        End If
        '    Else
        '    End If
        '    If oktoadd = True And unwatched = True Then
        '        If fullMovieList(f).playcount <> Nothing Then
        '            If Convert.ToInt32(fullMovieList(f).playcount) <> 0 Then
        '                oktoadd = False
        '            End If
        '        End If
        '    End If
        '    If oktoadd = True And offline = True Then
        '        For Each paths In Preferences.offlinefolders
        '            If fullMovieList(f).fullpathandfilename.IndexOf(paths) = -1 Then
        '                oktoadd = False
        '                Exit For
        '            End If
        '        Next
        '    End If
        '    If oktoadd = True Then
        '        filteredList.Add(fullMovieList(f))
        '    End If
        'Next
        'Dim add As Boolean = False
        'Dim newlist As New List(Of str_ComboList)
        'For Each movie In filteredList
        '    Dim tempstring As String = String.Empty
        '    If RadioButton1.Checked = True And ComboBox10.SelectedItem = "List" Then tempstring = movie.titleandyear.ToLower
        '    If RadioButton2.Checked = True And ComboBox10.SelectedItem = "List" Then tempstring = movie.filename.ToLower
        '    If RadioButton6.Checked = True And ComboBox10.SelectedItem = "List" Then tempstring = movie.foldername.ToLower
        '    If cbSort.SelectedIndex = 5 And ComboBox10.SelectedItem = "List" Then tempstring = movie.sortorder.ToLower
        '    If ComboBox10.SelectedItem = "Outline" Then tempstring = movie.outline
        '    If ComboBox10.SelectedItem = "Year" Then tempstring = movie.year
        '    If ComboBox10.SelectedItem = "IMDB ID" Then tempstring = movie.id
        '    If ComboBox10.SelectedItem = "Filename" Then tempstring = movie.filename
        '    If ComboBox10.SelectedItem = "Foldername" Then tempstring = movie.foldername
        '    If ComboBox10.SelectedItem = "Genre" Then tempstring = movie.genre
        '    If ComboBox10.SelectedItem = "Rating" Then tempstring = movie.rating
        '    If ComboBox10.SelectedItem = "Runtime" Then tempstring = movie.runtime
        '    If TextBox1.Text = "" And txt_titlesearch.Text = "" Then
        '        add = True
        '    ElseIf TextBox1.Text <> "" And txt_titlesearch.Text = "" Then
        '        If tempstring.ToLower.IndexOf(TextBox1.Text.ToLower) = 0 Then
        '            add = True
        '        End If
        '    ElseIf TextBox1.Text = "" And txt_titlesearch.Text <> "" Then
        '        If tempstring.ToLower.IndexOf(txt_titlesearch.Text.ToLower) <> -1 Then
        '            add = True
        '        End If
        '    ElseIf TextBox1.Text <> "" And txt_titlesearch.Text <> "" Then
        '        If tempstring.ToLower.IndexOf(TextBox1.Text.ToLower) = 0 And tempstring.ToLower.IndexOf(txt_titlesearch.Text.ToLower) <> -1 Then
        '            add = True
        '        End If
        '    End If
        '    If add = True Then
        '        add = False
        '        newlist.Add(movie)
        '    End If
        'Next
        'filteredList = newlist
        ''----------------------------------------------------------------------------------------------------
        'Dim ValuetoSearch As String = ComboBox11.SelectedItem.ToString.ToLower
        'Dim newlist1 As New List(Of str_ComboList)
        'For Each movie In filteredList
        '    Select Case ValuetoSearch.ToLower
        '        Case "all"
        '            TextBox_GenreFilter.Enabled = True
        '            newlist1.Add(movie)
        '        Case "dvdrip"
        '            If movie.filename.ToLower.IndexOf("dvdrip") <> -1 Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "dvdr5"
        '            If (movie.filename.ToLower.IndexOf("dvdr5") <> -1) Or (movie.filename.ToLower.IndexOf(".r5") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "dvdscreener"
        '            If (movie.filename.ToLower.IndexOf("dvdscreener") <> -1) Or (movie.filename.ToLower.IndexOf("dvdscr") <> -1) Or _
        '            (movie.filename.ToLower.IndexOf("screener") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "bluray"
        '            If (movie.filename.ToLower.IndexOf("bluray") <> -1) Or (movie.filename.ToLower.IndexOf("brrip") <> -1) Or _
        '            (movie.filename.ToLower.IndexOf("bdrip") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "telesync"
        '            If (movie.filename.ToLower.IndexOf("telesync") <> -1) Or (movie.filename.ToLower.IndexOf(".ts") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "cam"
        '            If (movie.filename.ToLower.IndexOf("cam") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '        Case "pdtv"
        '            If (movie.filename.ToLower.IndexOf("pdtv") <> -1) Or (movie.filename.ToLower.IndexOf("ppvrip") <> -1) Then
        '                newlist1.Add(movie)
        '            End If
        '    End Select
        'Next
        'filteredList = newlist1

        ''----------------------------------------------------------------------------------------------------


        'Dim genres As New List(Of String)
        'Dim newlist2 As New List(Of str_ComboList)
        'TextBox_GenreFilter.Text = ""
        'For Each CheckBox In CheckedListBox1.CheckedItems
        '    genres.Add(CheckBox.ToString.ToLower)
        '    If Len(TextBox_GenreFilter.Text) = 0 Then
        '        TextBox_GenreFilter.Text = CheckBox.ToString
        '    Else
        '        TextBox_GenreFilter.Text = TextBox_GenreFilter.Text & ", " & CheckBox.ToString
        '    End If
        '    If CheckBox.ToString.ToLower = "duplicates" Then dupes2 = True
        'Next
        'If genres.Count = 0 Then TextBox_GenreFilter.Text = "Genre Filter (AND)"
        'oktoadd = True
        'tempint = filteredList.Count - 1
        'For f = 0 To tempint
        '    top250 = False
        '    watched = False
        '    unwatched = False
        '    oktoadd = True
        '    For Each gen In genres
        '        If gen <> Nothing Then
        '            If filteredList(f).genre.ToLower.IndexOf(gen) = -1 Then
        '                oktoadd = False
        '            End If
        '        End If
        '    Next
        '    If oktoadd = True Then
        '        newlist2.Add(filteredList(f))
        '    End If
        'Next
        'filteredList = newlist2
        ''----------------------------------------------------------------------------------------------------
        'If dupes2 = True Then
        '    Dim dupelist As New List(Of str_ComboList)
        '    For f = 0 To filteredList.Count - 1
        '        For g = 0 To filteredList.Count - 1
        '            If g <> f Then
        '                If filteredList(f).id = filteredList(g).id And filteredList(g).id <> "" Then
        '                    Dim exists As Boolean = False
        '                    For Each movie In filteredList
        '                        If movie.fullpathandfilename = filteredList(f).fullpathandfilename Then exists = True
        '                        If movie.fullpathandfilename = filteredList(g).fullpathandfilename Then exists = True
        '                    Next
        '                    If exists = True Then
        '                        If Not dupelist.Contains(filteredList(f)) Then
        '                            dupelist.Add(filteredList(f))
        '                        End If
        '                    End If
        '                End If
        '            End If
        '        Next
        '    Next
        '    filteredList = dupelist
        'End If
        ''Call applyotherfilters()
        'Call mov_FiltersAndSortApply()
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'Finally
        '    Monitor.Exit(Me)
        'End Try
    End Sub

End Class
