﻿Imports System.Linq
Imports System.Runtime.CompilerServices



Public Class clsGridViewMovie

    Public GridFieldToDisplay1 As String
    Public GridFieldToDisplay2 As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(Form1 As Form1)

        If Not Form1.MainFormLoadedStatus Then Exit Sub

        Dim dgv As DataGridView = Form1.DataGridViewMovies

        If dgv.Columns.Count < 27 Then Return  '24
   '    If dgv.Rows   .Count <  1 Then Return

        Cursor.Current = Cursors.WaitCursor

        While dgv.Columns(0).CellType.Name="DataGridViewImageCell"
            dgv.Columns.Remove(dgv.Columns(0))
        End While


        If IsNothing(dgv.Columns("Watched")) Then
            Dim imgWatched As New DataGridViewImageColumn

            imgWatched.Image       = Global.Media_Companion.My.Resources.Resources.DotGray
            imgWatched.Name        = "Watched"
            imgWatched.ToolTipText = "Watched Status"
            imgWatched.HeaderText  = "W"

            dgv.Columns.Add(imgWatched)
            SetColWidth(dgv.Columns("Watched"))
        End If


        If IsNothing(dgv.Columns("ImgPlot")) Then
            Dim imgPlot As New DataGridViewImageColumn

            imgPlot.Image       = Global.Media_Companion.My.Resources.Resources.DotGray
            imgPlot.Name        = "ImgPlot"
            imgPlot.ToolTipText = "Plot"
            imgPlot.HeaderText  = "P"   
                    
            dgv.Columns.Add(imgPlot)
            SetColWidth(dgv.Columns("ImgPlot"))
        End If


        Dim header_style As New DataGridViewCellStyle

        header_style.ForeColor = Color.White
        header_style.BackColor = Color.ForestGreen
        header_style.Font      = new Font(dgv.Font, FontStyle.Bold)

        For Each col As DataGridViewcolumn in dgv.Columns
            col.HeaderCell.Style = header_style
        Next

        dgv.EnableHeadersVisualStyles = False


        For Each column As DataGridViewColumn In dgv.Columns
            column.Resizable = DataGridViewTriState.True
            column.ReadOnly  = True
            column.SortMode  = DataGridViewColumnSortMode.Automatic
            column.Visible   = False
        Next


        dgv.Columns("Watched").Visible = Preferences.MovieList_ShowColWatched
        dgv.Columns("ImgPlot").Visible = Preferences.MovieList_ShowColPlot   


        If dgv.Columns("Watched").Visible Then
            Dim x = Global.Media_Companion.My.Resources.Movie        'Performance tweak

            'Watched icon
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("playcount").Value <> "0" Then
                    row.Cells("Watched").Value = x  
                End If
            Next
        End If


        If dgv.Columns("ImgPlot").Visible Then
            Dim x = Global.Media_Companion.My.Resources.Page        'Performance tweak

            Try
                'plot icon
                For Each row As DataGridViewRow In dgv.Rows
                    If row.Cells("plot").Value <> "" Then  
                        row.Cells("ImgPlot").Value = x 
                    End If
                Next
            Catch
                Return
            End Try
        End If


        dgv.RowHeadersVisible = False

             
        If GridFieldToDisplay1="TitleAndYear" Then
            IniColumn(dgv,"DisplayTitle"       ,GridFieldToDisplay2= "Movie Year","Title"       )
            IniColumn(dgv,"DisplayTitleAndYear",GridFieldToDisplay2<>"Movie Year","Title & Year")
        End If

        IniColumn(dgv,"filename"         ,GridFieldToDisplay1="FileName"  ,"File name"                                                               )
        IniColumn(dgv,"foldername"       ,GridFieldToDisplay1="Folder"    ,"Folder name"                                                             )
        IniColumn(dgv,"year"             ,GridFieldToDisplay2="Movie Year","Movie year"   ,"Year"    , -20                                           )
        IniColumn(dgv,"DisplayFileDate"  ,GridFieldToDisplay2="Modified"  ,"Date Modified","Modified"                                                )
        IniColumn(dgv,"DisplayRating"    ,GridFieldToDisplay2="Rating"    ,"Rating"       ,"Rating"  , -20, DataGridViewContentAlignment.MiddleCenter)
        IniColumn(dgv,"runtime"          ,GridFieldToDisplay2="Runtime"   ,"Runtime"      ,          , -20, DataGridViewContentAlignment.MiddleRight )
        IniColumn(dgv,"DisplayCreateDate",GridFieldToDisplay2="Date Added","Date Added"   ,"Added"                                                   )
        IniColumn(dgv,"votes"            ,GridFieldToDisplay2="Votes"     ,"Votes"        ,          ,    , DataGridViewContentAlignment.MiddleRight )
          
        SetFirstColumnWidth(dgv)

        Cursor.Current = Cursors.Default
    End Sub

    Sub IniColumn(dgv As DataGridView, name As String, visible As Boolean, Optional toolTip As String=Nothing, Optional headerText As String=Nothing, Optional widthAdjustment As Integer=0, Optional alignment As DataGridViewContentAlignment=Nothing )

        Dim col As DataGridViewColumn = dgv.Columns(name)

        If IsNothing(toolTip) Then toolTip = CapsFirstLetter(name)

        col.Visible     = visible
        col.ToolTipText = toolTip
        col.HeaderText  = If(IsNothing(headerText),toolTip,headerText)
        SetColWidth(col,widthAdjustment)
       
        If Not IsNothing(alignment) Then

            Dim header_style As New DataGridViewCellStyle

            header_style.ForeColor = Color.White
            header_style.BackColor = Color.ForestGreen
            header_style.Font      = new Font(dgv.Font, FontStyle.Bold)
            header_style.Alignment = alignment

            col.HeaderCell.Style = header_style

            col.DefaultCellStyle.Alignment = alignment
        End If
    End Sub


    Function CapsFirstLetter(words As String)
        Return Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words)
    End Function


    Sub SetColWidth(col As DataGridViewColumn, Optional widthAdjustment As Integer=0)

    '   col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells          'AllCells = slow
        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells    'Set auto-size mode

        Dim initialAutoSizeWidth As Integer = col.Width                 'Save calculated width after auto-sizing

        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet        'Revert sizing mode to default

        col.Width = initialAutoSizeWidth+widthAdjustment                'Set width to calculated auto-size - adjustment needed because header has excess padding
    End Sub


    Sub SetFirstColumnWidth(dgvMovies As DataGridView)
        Try
            Dim firstColWidth As Integer = dgvMovies.Width - 17

            If dgvMovies.Columns("ImgPlot").Visible then firstColWidth -= dgvMovies.Columns("ImgPlot").Width
            If dgvMovies.Columns("Watched").Visible then firstColWidth -= dgvMovies.Columns("Watched").Width


            If GridFieldToDisplay2 = "Movie Year" Then firstColWidth -= dgvMovies.Columns("year"             ).Width
            If GridFieldToDisplay2 = "Modified"   Then firstColWidth -= dgvMovies.Columns("DisplayFileDate"  ).Width
            If GridFieldToDisplay2 = "Rating"     Then firstColWidth -= dgvMovies.Columns("DisplayRating"    ).Width
            If GridFieldToDisplay2 = "Runtime"    Then firstColWidth -= dgvMovies.Columns("runtime"          ).Width
            If GridFieldToDisplay2 = "Date Added" Then firstColWidth -= dgvMovies.Columns("DisplayCreateDate").Width
            If GridFieldToDisplay2 = "Votes"      Then firstColWidth -= dgvMovies.Columns("votes"            ).Width

            If firstColWidth>0 Then
                dgvMovies.Columns("filename"           ).Width = firstColWidth
                dgvMovies.Columns("foldername"         ).Width = firstColWidth
                dgvMovies.Columns("DisplayTitle"       ).Width = firstColWidth
                dgvMovies.Columns("DisplayTitleAndYear").Width = firstColWidth
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

        If Not Form1.MainFormLoadedStatus Then Exit Sub

        Dim b = From f In Form1.oMovies.Data_GridViewMovieCache Where f.TitleUcase.Contains(Form1.txt_titlesearch.Text.ToUpper)


        'General
        Select Form1.cbFilterGeneral.Text.RemoveAfterMatch

            Case "Watched"                     : b = From f In b Where     f.Watched
            Case "Unwatched"                   : b = From f In b Where Not f.Watched

            Case "Duplicates"                  : Dim sort = b.GroupBy(Function(f) f.id) : b = sort.Where(Function(x) x.Count>1).SelectMany(Function(x) x).ToList

            Case "Missing Poster"              : b = From f In b Where f.MissingPoster
            Case "Missing Fanart"              : b = From f In b Where f.MissingFanart
            Case "Missing Trailer"             : b = From f In b Where f.MissingTrailer
            Case "Missing Plot"                : b = From f In b Where f.MissingPlot
            Case "Missing Genre"               : b = From f In b Where f.MissingGenre
            Case "Missing Outline"             : b = From f In b Where f.MissingOutline
            Case "Missing Rating"              : b = From f In b Where f.MissingRating
            Case "Missing Runtime"             : b = From f In b Where f.MissingRuntime
            Case "Missing Votes"               : b = From f In b Where f.MissingVotes
            Case "Missing Year"                : b = From f In b Where f.MissingYear
            Case "Missing Certificate"         : b = From f In b Where f.MissingCertificate

            Case "Not matching rename pattern" : b = From f In b Where Not f.ActualNfoFileNameMatchesDesired
  
        End Select

        If Yield Then Return

        
        b = From f In b Where f.Rating >= Form1.cbFilterRating.SelectedMin and f.Rating <= Form1.cbFilterRating.SelectedMax     'Rating
        b = From f In b Where f.Votes  >= Form1.cbFilterVotes .SelectedMin and f.Votes  <= Form1.cbFilterVotes .SelectedMax     'Votes
        b = From f In b Where f.year   >= Form1.cbFilterYear  .SelectedMin and f.year   <= Form1.cbFilterYear  .SelectedMax     'Year
       
        b = Form1.oMovies.ApplyGenreFilter      ( b , Form1.cbFilterGenre       )
        b = Form1.oMovies.ApplyCertificateFilter( b , Form1.cbFilterCertificate )
        b = Form1.oMovies.ApplySetFilter        ( b , Form1.cbFilterSet         )
        b = Form1.oMovies.ApplyResolutionFilter ( b , Form1.cbFilterResolution  )
        
'        'Set
'        If Form1.SetFilter<>"" Then
''            b = From f In b Where f.movieset.Contains(Form1.SetFilter)
'            b = From f In b Where f.movieset=Form1.SetFilter
'            If Yield Then Return
'        End If

        'Resolution
        'If Form1.ResolutionFilter<>"" Then
        '    b = From f In b Where f.Resolution=Convert.ToInt32(Form1.ResolutionFilter)
        '    If Yield Then Return
        'End If


        If Form1.AudioCodecsFilter    <>"" Then b = Form1.oMovies.ApplyAudioCodecFilter     ( b , Form1.AudioCodecsFilter    )
        If Form1.AudioLanguagesFilter <>"" Then b = Form1.oMovies.ApplyAudioLanguageFilter  ( b , Form1.AudioLanguagesFilter )
        If Form1.AudioChannelsFilter  <>"" Then b = Form1.oMovies.ApplyAudioChannelsFilter  ( b , Form1.AudioChannelsFilter  )
        If Form1.AudioBitratesFilter  <>"" Then b = Form1.oMovies.ApplyAudioBitrateFilter   ( b , Form1.AudioBitratesFilter  )
        If Form1.NumAudioTracksFilter <>"" Then b = Form1.oMovies.ApplyNumAudioTracksFilter ( b , Form1.NumAudioTracksFilter )

        ' , oMovies.NumAudioTracks   

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
                    b = From f In b Order By f.Rating Ascending
                Else
                    b = From f In b Order By f.Rating Descending
                End If
            Case "Sort Order"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.DisplaySortOrder Ascending
                Else
                    b = From f In b Order By f.DisplaySortOrder Descending
                End If
            Case "Date Added"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.createdate Descending
                Else
                    b = From f In b Order By f.createdate Ascending
                End If
            Case "Votes"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.Votes Ascending
                Else
                    b = From f In b Order By f.Votes Descending
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
