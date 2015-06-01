Imports System.Linq
Imports System.Runtime.CompilerServices



Public Class clsGridViewMovie

    Public GridFieldToDisplay1 As String
    Public GridFieldToDisplay2 As String
    Public GridSort As String

    Public Sub GridviewMovieDesign(Form1 As Form1)

        If Not Form1.MainFormLoadedStatus Then Exit Sub

        Dim dgv As DataGridView = Form1.DataGridViewMovies

        If dgv.Columns.Count < 27 Then Return

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

        'Highlight titles in datagridview with missing video files.
        If Preferences.incmissingmovies Then
            For Each row As DataGridViewRow In dgv.Rows
                If row.Cells("videomissing").Value = True Then
                    row.DefaultCellStyle.BackColor = Color.Red                
                End If
            Next
        End If

        dgv.RowHeadersVisible = False

             
        If GridFieldToDisplay1="TitleAndYear" Then
            IniColumn(dgv,"DisplayTitle"       ,GridFieldToDisplay2= "Movie Year","Title"       )
            IniColumn(dgv,"DisplayTitleAndYear",GridFieldToDisplay2<>"Movie Year","Title & Year")
        End If

        IniColumn(dgv,"filename"         ,GridFieldToDisplay1="FileName"    ,"File name"                                                                   )
        IniColumn(dgv,"foldername"       ,GridFieldToDisplay1="Folder"      ,"Folder name"                                                                 )
        IniColumn(dgv,"year"             ,GridFieldToDisplay2="Movie Year"  ,"Movie year"       ,"Year"    , -20                                           )
        IniColumn(dgv,"DisplayFileDate"  ,GridFieldToDisplay2="Modified"    ,"Date Modified"    ,"Modified"                                                )
        IniColumn(dgv,"DisplayRating"    ,GridFieldToDisplay2="Rating"      ,"Rating"           ,"Rating"  , -20, DataGridViewContentAlignment.MiddleCenter)
        IniColumn(dgv,"runtime"          ,GridFieldToDisplay2="Runtime"     ,"Runtime"          ,          , -20, DataGridViewContentAlignment.MiddleRight )
        IniColumn(dgv,"DisplayCreateDate",GridFieldToDisplay2="Date Added"  ,"Date Added"       ,"Added"                                                   )
        IniColumn(dgv,"votes"            ,GridFieldToDisplay2="Votes"       ,"Votes"            ,          ,    , DataGridViewContentAlignment.MiddleRight )
        IniColumn(dgv,"DisplayFolderSize",GridFieldToDisplay2="Folder Size" ,"Folder Size (GB)" ,"Size"    , -20, DataGridViewContentAlignment.MiddleRight )
         
        dgv.Columns("DisplayFolderSize").DefaultCellStyle.Format="0.0"
          
        SetFirstColumnWidth(dgv)

        Cursor.Current = Cursors.Default
    End Sub

    Sub IniColumn(dgv As DataGridView, name As String, visible As Boolean, Optional toolTip As String=Nothing, Optional headerText As String=Nothing, Optional widthAdjustment As Integer=0, Optional alignment As DataGridViewContentAlignment=Nothing )

        Dim col As DataGridViewColumn = dgv.Columns(name)

        If IsNothing(toolTip) Then toolTip = Utilities.TitleCase(name)  'CapsFirstLetter(name)

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
        Return Form1.MyCulture.TextInfo.ToTitleCase(words)
    End Function


    Sub SetColWidth(col As DataGridViewColumn, Optional widthAdjustment As Integer=0)

        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells    'Set auto-size mode

        Dim initialAutoSizeWidth As Integer = col.Width                 'Save calculated width after auto-sizing

        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet        'Revert sizing mode to default

        col.Width = initialAutoSizeWidth+widthAdjustment                'Set width to calculated auto-size - adjustment needed because header has excess padding
    End Sub


    Sub SetFirstColumnWidth(dgvMovies As DataGridView)
        Try
            Dim firstColWidth As Integer = dgvMovies.Width - 17

            If Not IsNothing(dgvMovies.Columns("ImgPlot")) AndAlso dgvMovies.Columns("ImgPlot").Visible then firstColWidth -= dgvMovies.Columns("ImgPlot").Width
            If Not IsNothing(dgvMovies.Columns("Watched")) AndAlso dgvMovies.Columns("Watched").Visible then firstColWidth -= dgvMovies.Columns("Watched").Width


            If GridFieldToDisplay2 = "Movie Year" Then firstColWidth -= dgvMovies.Columns("year"             ).Width
            If GridFieldToDisplay2 = "Modified"   Then firstColWidth -= dgvMovies.Columns("DisplayFileDate"  ).Width
            If GridFieldToDisplay2 = "Rating"     Then firstColWidth -= dgvMovies.Columns("DisplayRating"    ).Width
            If GridFieldToDisplay2 = "Runtime"    Then firstColWidth -= dgvMovies.Columns("runtime"          ).Width
            If GridFieldToDisplay2 = "Date Added" Then firstColWidth -= dgvMovies.Columns("DisplayCreateDate").Width
            If GridFieldToDisplay2 = "Votes"      Then firstColWidth -= dgvMovies.Columns("votes"            ).Width
            If GridFieldToDisplay2 = "Folder Size"Then firstColWidth -= dgvMovies.Columns("DisplayFolderSize" ).Width


            If firstColWidth>0 Then
                If Not IsNothing(dgvMovies.Columns("filename"           )) Then dgvMovies.Columns("filename"           ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("foldername"         )) Then dgvMovies.Columns("foldername"         ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("DisplayTitle"       )) Then dgvMovies.Columns("DisplayTitle"       ).Width = firstColWidth
                If Not IsNothing(dgvMovies.Columns("DisplayTitleAndYear")) Then dgvMovies.Columns("DisplayTitleAndYear").Width = firstColWidth
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
 
        Dim b = From f In Form1.oMovies.Data_GridViewMovieCache
       
        If Form1.txt_titlesearch.Text.ToUpper<>"" Then
            b = From f In b Where f.TitleUcase.Contains(Form1.txt_titlesearch.Text.ToUpper)
        End If

        'General
        If Form1.cbFilterGeneral.Visible Then
            Select Form1.cbFilterGeneral.Text.RemoveAfterMatch

                Case "Watched"                     : b = From f In b Where     f.Watched
                Case "Unwatched"                   : b = From f In b Where Not f.Watched
                Case "Scrape Error"                : b = From f In b Where f.genre.ToLower = "problem"

                Case "Duplicates"                  : Dim sort = b.Where(Function(y) y.id<>"0").GroupBy(Function(f) f.id) : b = sort.Where(Function(x) x.Count>1).SelectMany(Function(x) x).ToList

                Case "Missing Poster"              : b = From f In b Where f.MissingPoster
                Case "Missing Fanart"              : b = From f In b Where f.MissingFanart
                Case "Missing Trailer"             : b = From f In b Where f.MissingTrailer
                Case "Missing Local Actors"        : b = From f In b Where f.MissingLocalActors
                Case "Missing Plot"                : b = From f In b Where f.MissingPlot
                Case "Missing Genre"               : b = From f In b Where f.MissingGenre
                Case "Missing Outline"             : b = From f In b Where f.MissingOutline
                Case "Missing Rating"              : b = From f In b Where f.MissingRating
                Case "Missing Runtime"             : b = From f In b Where f.MissingRuntime
                Case "Missing Votes"               : b = From f In b Where f.MissingVotes
                Case "Missing Year"                : b = From f In b Where f.MissingYear
                Case "Missing IMDB"                : b = From f In b Where f.MissingIMDBId
                Case "Missing Certificate"         : b = From f In b Where f.MissingCertificate
                Case "Missing Source"              : b = From f In b Where f.MissingSource
                Case "Missing Director"            : b = From f In b Where f.MissingDirector
                Case "Missing from XBMC"           : b = b.Where( Function(x) Form1.MC_Only_Movies_Nfos.Contains(x.fullpathandfilename) )
                Case "Not matching rename pattern" : b = From f In b Where Not f.ActualNfoFileNameMatchesDesired
                Case "Different titles"            : b = b.Where( Function(x) Form1.oMovies.Xbmc_DifferentTitles.Contains(x.MoviePathAndFileName) )
                Case "Pre-Frodo poster only"       : b = From f In b Where     f.PreFrodoPosterExists And Not f.FrodoPosterExists
                Case "Frodo poster only"           : b = From f In b Where Not f.PreFrodoPosterExists And     f.FrodoPosterExists
                Case "Both poster formats"         : b = From f In b Where     f.PreFrodoPosterExists And     f.FrodoPosterExists
                Case "Imdb in folder name"         : b = From f In b Where     f.ImdbInFolderName
                Case "Imdb in not folder name"     : b = From f In b Where Not f.ImdbInFolderName
                Case "Imdb not in folder name & year mismatch" : b = From f In b Where Not f.ImdbInFolderName And f.year<>f.FolderNameYear
                Case "Plot same as Outline"        : b = (From f In b From m In Form1.oMovies.MovieCache _
                                                          Where m.PlotEqOutline And f.fullpathandfilename=m.fullpathandfilename
                                                          Select f
                                                          )
            End Select
        End If

        If Yield Then Return

        If Form1.cbFilterRating  .Visible Then b = From f In b Where f.Rating     >= Form1.cbFilterRating  .SelectedMin and f.Rating     <= Form1.cbFilterRating  .SelectedMax   
        If Form1.cbFilterVotes   .Visible Then b = From f In b Where f.Votes      >= Form1.cbFilterVotes   .SelectedMin and f.Votes      <= Form1.cbFilterVotes   .SelectedMax   
        If Form1.cbFilterRuntime .Visible Then b = From f In b Where f.IntRuntime >= Form1.cbFilterRuntime .SelectedMin and f.IntRuntime <= Form1.cbFilterRuntime .SelectedMax   

'        If Form1.cbFilterFolderSizes .Visible Then b = From f In b Where CInt( f.FolderSize /(1024*1024*1024) )  >= Form1.cbFilterFolderSizes.SelectedMin and CInt( f.FolderSize /(1024*1024*1024) )  <= Form1.cbFilterFolderSizes.SelectedMax     'Votes
        If Form1.cbFilterFolderSizes .Visible Then b = From f In b Where f.DisplayFolderSize >= Form1.cbFilterFolderSizes.SelectedMin and f.DisplayFolderSize <= Form1.cbFilterFolderSizes.SelectedMax
        If Form1.cbFilterYear  .Visible Then b = From f In b Where f.year   >= Form1.cbFilterYear  .SelectedMin and f.year   <= Form1.cbFilterYear  .SelectedMax     'Year

        If Form1.cbFilterCountries             .Visible Then b = Form1.oMovies.ApplyCountiesFilter              ( b , Form1.cbFilterCountries             )
        If Form1.cbFilterGenre                 .Visible Then b = Form1.oMovies.ApplyGenresFilter                ( b , Form1.cbFilterGenre                 )
        If Form1.cbFilterCertificate           .Visible Then b = Form1.oMovies.ApplyCertificatesFilter          ( b , Form1.cbFilterCertificate           )
        If Form1.cbFilterSet                   .Visible Then b = Form1.oMovies.ApplySetsFilter                  ( b , Form1.cbFilterSet                   )
        If Form1.cbFilterResolution            .Visible Then b = Form1.oMovies.ApplyResolutionsFilter           ( b , Form1.cbFilterResolution            )
        If Form1.cbFilterVideoCodec            .Visible Then b = Form1.oMovies.ApplyVideoCodecFilter            ( b , Form1.cbFilterVideoCodec            )
        If Form1.cbFilterAudioCodecs           .Visible Then b = Form1.oMovies.ApplyAudioCodecsFilter           ( b , Form1.cbFilterAudioCodecs           )
        If Form1.cbFilterAudioChannels         .Visible Then b = Form1.oMovies.ApplyAudioChannelsFilter         ( b , Form1.cbFilterAudioChannels         )
        If Form1.cbFilterAudioBitrates         .Visible Then b = Form1.oMovies.ApplyAudioBitratesFilter         ( b , Form1.cbFilterAudioBitrates         )
        If Form1.cbFilterNumAudioTracks        .Visible Then b = Form1.oMovies.ApplyNumAudioTracksFilter        ( b , Form1.cbFilterNumAudioTracks        )
        If Form1.cbFilterAudioLanguages        .Visible Then b = Form1.oMovies.ApplyAudioLanguagesFilter        ( b , Form1.cbFilterAudioLanguages        )
        If Form1.cbFilterAudioDefaultLanguages .Visible Then b = Form1.oMovies.ApplyAudioDefaultLanguagesFilter ( b , Form1.cbFilterAudioDefaultLanguages )
        If Form1.cbFilterActor                 .Visible Then b = Form1.oMovies.ApplyActorsFilter                ( b , Form1.cbFilterActor                 )
        If Form1.cbFilterDirector              .Visible Then b = Form1.oMovies.ApplyDirectorsFilter             ( b , Form1.cbFilterDirector              )
        If Form1.cbFilterSource                .Visible Then b = Form1.oMovies.ApplySourcesFilter               ( b , Form1.cbFilterSource                )
        If Form1.cbFilterTag                   .Visible Then b = Form1.oMovies.ApplyTagsFilter                  ( b , Form1.cbFilterTag                   )
        If Form1.cbFilterSubTitleLang          .Visible Then b = Form1.oMovies.ApplySubtitleLangFilter          ( b , Form1.cbFilterSubTitleLang          )       
 
        Select Case Form1.cbSort.Text
            Case "A - Z"
                If GridSort = "Asc" Then
                    If GridFieldToDisplay1="FileName" Then
                        b = From f In b Order By f.filename Ascending
                    ElseIf GridFieldToDisplay1="Folder" Then
                        b = From f In b Order By f.foldername Ascending
                    Else
                        b = From f In b Order By f.DisplayTitle Ascending            'DisplayTitleAndYear
                    End If
                Else
                    If GridFieldToDisplay1="FileName" Then
                        b = From f In b Order By f.filename Descending
                    ElseIf GridFieldToDisplay1="Folder" Then
                        b = From f In b Order By f.foldername Descending
                    Else
                        b = From f In b Order By f.DisplayTitle Descending           'DisplayTitleAndYear
                    End If
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
            Case "Folder Size"
                If GridSort = "Asc" Then
                    b = From f In b Order By f.FolderSize Ascending
                Else
                    b = From f In b Order By f.FolderSize Descending
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
