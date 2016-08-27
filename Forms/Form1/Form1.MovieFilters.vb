'
' Form1 Movie filters stuff should go here
' Form1 is way too big - If you're reading this and you're v. bored, please feel free to split out From1 code 
' by context, into manageable chucks like this. 
'

Partial Public Class Form1


    Friend WithEvents lblFilterLockedMode As Label
    Friend WithEvents lblFilterLocked     As Label
    Friend WithEvents cbFilterLocked      As MC_UserControls.TriStateCheckedComboBox
    
      
    Sub CreateDynamicMovieFilters
        CreateTriStateFilter("Locked", lblFilterLockedMode, lblFilterLocked, cbFilterLocked)
    End Sub
   

    Sub CreateTriStateFilter(name As String, ByRef lblFilterMode As Label, ByRef lblFilter As Label, ByRef cbFilter As MC_UserControls.TriStateCheckedComboBox)

        Dim panel = SplitContainer5.Panel2

		lblFilterMode = CreateFilterLabel            ("lblFilter" & name & "Mode", "M"  , panel, 129,  17)
		lblFilter     = CreateFilterLabel            ("lblFilter" & name         , name , panel,   4, 124)
      cbFilter      = CreateTriStateCheckedComboBox("cbFilter"  & name                , panel, 147     )
    End Sub


    Function CreateFilterLabel(name As String, text As String, panel As Panel, x As Integer, width As Integer)

        Dim lbl      As New Label
        Dim y        = 100
        Dim tabIndex = 100

        panel.Controls.Add(lbl)

        lbl.Anchor    = CType((Windows.Forms.AnchorStyles.Bottom Or Windows.Forms.AnchorStyles.Left), Windows.Forms.AnchorStyles)
        lbl.BackColor = Drawing.Color.Gray
        lbl.Font      = New Drawing.Font("Microsoft Sans Serif", 9.0!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
        lbl.ForeColor = Drawing.Color.White
        lbl.Location  = New Drawing.Point(x, y)
        lbl.Margin    = New Windows.Forms.Padding(4, 0, 4, 0)
        lbl.Name      = name
        lbl.Size      = New Drawing.Size(width, 21)
        lbl.TabIndex  = tabIndex
        lbl.Text      = text
        lbl.TextAlign = Drawing.ContentAlignment.MiddleLeft

        Return lbl
    End Function

	
    Function CreateTriStateCheckedComboBox(name As String, panel As Panel, x As Integer)

        Dim cb  As New MC_UserControls.TriStateCheckedComboBox()
        Dim y        = 100
        Dim tabIndex = 100
        Dim tag      = "100"
	
        panel.Controls.Add(cb)
           
        cb.Anchor                     = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        cb.BackColor                  = Drawing.SystemColors.Control
        cb.Font                       = New Drawing.Font("Microsoft Sans Serif", 9.0!, Drawing.FontStyle.Bold, Drawing.GraphicsUnit.Point, CType(0, Byte))
        cb.CheckOnClick               = true
        cb.DisplayWhenNothingSelected = "All"
        cb.DrawMode                   = Windows.Forms.DrawMode.OwnerDrawVariable
        cb.DropDownHeight             = 1
        cb.DropDownStyle              = Windows.Forms.ComboBoxStyle.DropDownList
        cb.FormattingEnabled          = true
        cb.IntegralHeight             = false
        cb.Location                   = New Drawing.Point(x,y)
        cb.Mode                       = MC_UserControls.TriStateCheckedComboBox.OperationMode.Follow
        cb.Name                       = name
        cb.QuickSelect                = false
        cb.Size                       = New Drawing.Size(0, 22)
        cb.TabIndex                   = 228
        cb.Tag                        = tag
        cb.ValueSeparator             = " "  
  
        Return cb
    End Function



    Private Sub UpdateFilteredList

        oMovies.UpdateTmdbSetMissingMovies()

        Dim lastSelectedRow As Integer = 0
        If DataGridViewMovies.SelectedRows.Count = 1 Then 
            lastSelectedRow = DataGridViewMovies.SelectedRows(0).Index 
        End If
        ProgState = ProgramState.UpdatingFilteredList
        Dim lastSelectedMovie = workingMovie.fullpathandfilename
        filteredList.Clear
        filteredList.AddRange(oMovies.MovieCache)
        Assign_FilterGeneral()
        UpdateMinMaxMovieFilters

        If cbFilterCountries            .Visible Then cbFilterCountries             .UpdateItems( oMovies.CountriesFilter               )
        If cbFilterStudios              .Visible Then cbFilterStudios               .UpdateItems (oMovies.StudiosFilter                 )
        If cbFilterGenre                .Visible Then cbFilterGenre                 .UpdateItems( oMovies.GenresFilter                  )
        If cbFilterCertificate          .Visible Then cbFilterCertificate           .UpdateItems( oMovies.CertificatesFilter            )
        If cbFilterSet                  .Visible Then cbFilterSet                   .UpdateItems( oMovies.SetsFilter                    )
        If cbFilterTag                  .Visible Then cbFilterTag                   .UpdateItems( oMovies.TagFilter                     )
        If cbFilterResolution           .Visible Then cbFilterResolution            .UpdateItems( oMovies.ResolutionFilter              )
        If cbFilterVideoCodec           .Visible Then cbFilterVideoCodec            .UpdateItems( oMovies.VideoCodecFilter              )
        If cbFilterAudioCodecs          .Visible Then cbFilterAudioCodecs           .UpdateItems( oMovies.AudioCodecsFilter             )
        If cbFilterAudioChannels        .Visible Then cbFilterAudioChannels         .UpdateItems( oMovies.AudioChannelsFilter           )
        If cbFilterAudioBitrates        .Visible Then cbFilterAudioBitrates         .UpdateItems( oMovies.AudioBitratesFilter           )
        If cbFilterNumAudioTracks       .Visible Then cbFilterNumAudioTracks        .UpdateItems( oMovies.NumAudioTracksFilter          )
        If cbFilterAudioLanguages       .Visible Then cbFilterAudioLanguages        .UpdateItems( oMovies.AudioLanguagesFilter          )
        If cbFilterAudioDefaultLanguages.Visible Then cbFilterAudioDefaultLanguages .UpdateItems( oMovies.AudioDefaultLanguagesFilter   )
        If cbFilterActor                .Visible Then cbFilterActor                 .UpdateItems( oMovies.ActorsFilter                  )
        If cbFilterDirector             .Visible Then cbFilterDirector              .UpdateItems( oMovies.DirectorsFilter               )
        If cbFilterTag                  .Visible Then cbFilterTag                   .UpdateItems( oMovies.TagsFilter                    )
        If cbFilterSubTitleLang         .Visible Then cbFilterSubTitleLang          .UpdateItems( oMovies.SubTitleLangFilter            )
        If cbFilterRootFolder           .Visible Then cbFilterRootFolder            .UpdateItems( oMovies.RootFolderFilter              )
        If cbFilterUserRated            .Visible Then cbFilterUserRated             .UpdateItems( oMovies.UserRatedFilter               )
        If cbFilterLocked               .Visible Then cbFilterLocked                .UpdateItems( oMovies.LockedFilter                  )
                   
                 
                   
                                          
        Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
        Try
            If DataGridViewMovies.SelectedRows.Count = 0 Then
                For Each row As DataGridViewRow In DataGridViewMovies.Rows
                    row.Selected = (row.Cells("fullpathandfilename").Value.ToString = lastSelectedMovie)
                Next
            ElseIf DataGridViewMovies.SelectedRows.Count = 1 Then
                DataGridViewMovies.ClearSelection()
                DataGridViewMovies.Rows(lastSelectedRow).Selected = True
            End If
        Catch
        End Try
        
        If DataGridViewMovies.SelectedRows.Count=0 And DataGridViewMovies.Rows.Count>0 Then
            DataGridViewMovies.Rows(0).Selected=True
        End If
        DisplayMovie()
        ProgState = ProgramState.Other
    End Sub

    Sub Assign_FilterGeneral
        If cbFilterGeneral.Visible Then
            Dim selected = cbFilterGeneral.Text

            cbFilterGeneral.Items.Clear
            cbFilterGeneral.Items.AddRange( oMovies.GeneralFilters.ToArray )

            If cbFilterGeneral.Text = "" Then cbFilterGeneral.Text = "All"

            If selected<>"" Then
                For Each item As String In cbFilterGeneral.Items
                    If item.RemoveAfterMatch=selected.RemoveAfterMatch Then
                        cbFilterGeneral.SelectedItem=item    
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub




    Private Function TriStateFilter_OnFormatItem(ByVal item As String) As String Handles cbFilterGenre.OnFormatItem, cbFilterCertificate.OnFormatItem,
                                                                                    cbFilterSet.OnFormatItem, cbFilterResolution.OnFormatItem,
                                                                                    cbFilterAudioCodecs.OnFormatItem, cbFilterAudioChannels.OnFormatItem,
                                                                                    cbFilterAudioBitrates.OnFormatItem, cbFilterNumAudioTracks.OnFormatItem,
                                                                                    cbFilterAudioLanguages.OnFormatItem, cbFilterActor.OnFormatItem,
                                                                                    cbFilterSource.OnFormatItem, cbFilterTag.OnFormatItem, cbFilterTag.OnFormatItem,
                                                                                    cbFilterDirector.OnFormatItem, cbFilterVideoCodec.OnFormatItem, cbFilterSubTitleLang.OnFormatItem,
                                                                                    cbFilterAudioDefaultLanguages.OnFormatItem, cbFilterCountries.OnFormatItem, 
                                                                                    cbFilterStudios.OnFormatItem, cbFilterRootFolder.OnFormatItem, cbFilterUserRated.OnFormatItem, cbFilterLocked.OnFormatItem
        Return item.RemoveAfterMatch
    End Function


    Private Sub cbFilterChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterGeneral.SelectedValueChanged, cbFilterSource.TextChanged,
                                                                                                    cbFilterGenre.TextChanged, cbFilterCertificate.TextChanged,
                                                                                                    cbFilterSet.TextChanged, cbFilterResolution.TextChanged,
                                                                                                    cbFilterAudioCodecs.TextChanged, cbFilterAudioChannels.TextChanged,
                                                                                                    cbFilterAudioBitrates.TextChanged, cbFilterNumAudioTracks.TextChanged,
                                                                                                    cbFilterAudioLanguages.TextChanged, cbFilterActor.TextChanged, cbFilterTag.TextChanged,
                                                                                                    cbFilterDirector.TextChanged, cbFilterVideoCodec.TextChanged, cbFilterSubTitleLang.TextChanged,
                                                                                                    cbFilterAudioDefaultLanguages.TextChanged, cbFilterCountries.TextChanged, 
                                                                                                    cbFilterStudios.TextChanged, cbFilterRootFolder.TextChanged,
                                                                                                    cbFilterUserRated.TextChanged, cbFilterLocked.TextChanged

        If TypeName(sender) = "TriStateCheckedComboBox" Then
            Dim x As MC_UserControls.TriStateCheckedComboBox = sender
            If x.opState <> 0 Then
                Return
            End If
        End If
        ApplyMovieFilters()
    End Sub
   

    Private Sub ResetFilter(sender As Object, e As EventArgs) Handles lblFilterSet.Click, lblFilterVotes.Click, lblFilterRating.Click,
                                                                        lblFilterCertificate.Click, lblFilterGenre.Click, lblFilterYear.Click,
                                                                        lblFilterResolution.Click, lblFilterAudioCodecs.Click, lblFilterAudioChannels.Click,
                                                                        lblFilterAudioBitrates.Click, lblFilterNumAudioTracks.Click, lblFilterAudioLanguages.Click,
                                                                        lblFilterActor.Click, lblFilterSource.Click, lblFilterTag.Click,
                                                                        lblFilterDirector.Click, lblFilterVideoCodec.Click, lblFilterSubTitleLang.Click,
                                                                        lblFilterFolderSizes.Click, lblFilterRuntime.Click, lblFilterAudioDefaultLanguages.Click,
                                                                        lblFilterCountries.Click, lblFilterStudios.Click, lblFilterRootFolder.Click,
                                                                        lblFilterUserRated.Click, lblFilterLocked.Click
  
        Dim filter As Object = GetFilterFromLabel(sender)

        ProgState = ProgramState.ResettingFilters
        filter.Reset()
        ProgState = ProgramState.Other

        UpdateFilteredList()
    End Sub   
   
     
    Private Sub cbFilterRatingChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterRating.SelectionChanged, cbFilterVotes.SelectionChanged,
                                                                                                          cbFilterRuntime.SelectionChanged, cbFilterFolderSizes.SelectionChanged,
                                                                                                          cbFilterYear.SelectionChanged
        ApplyMovieFilters
    End Sub
 

    Private Sub cbFilterBeginSliding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterRuntime.BeginSliding, cbFilterYear.BeginSliding, cbFilterVotes.BeginSliding, cbFilterRating.BeginSliding, cbFilterFolderSizes.BeginSliding
        SplitContainer5.Panel2.ContextMenuStrip = Nothing
    End Sub


    Private Sub cbFilterEndSliding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFilterRuntime.EndSliding, cbFilterYear.EndSliding, cbFilterVotes.EndSliding, cbFilterRating.EndSliding, cbFilterFolderSizes.EndSliding
        SplitContainer5.Panel2.ContextMenuStrip = cmsConfigureMovieFilters
    End Sub


    Private Sub ApplyMovieFilters
        tsmiMov_ConvertToFrodo.Enabled = (cbFilterGeneral.Text.RemoveAfterMatch="Pre-Frodo poster only") or (cbFilterGeneral.Text.RemoveAfterMatch="Both poster formats")
        If ProgState = ProgramState.Other Then
            Mc.clsGridViewMovie.mov_FiltersAndSortApply(Me)
            DisplayMovie
        End If
    End Sub


    Sub HandleMovieFilter_SelectedValueChanged(cbFilter As ComboBox, ByRef filterValue As String, Optional replaceUnknown As Boolean = False)
        If ProgState = ProgramState.Other Then
            If cbFilter.Text = "All" Then
                filterValue = ""
            Else
                filterValue = cbFilter.Text.RemoveAfterMatch
                If replaceUnknown Then filterValue = filterValue.Replace("Unknown","-1")
            End If
            ApplyMovieFilters
        End If
    End Sub


    Private Sub ConfigureMovieFiltersToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ConfigureMovieFiltersToolStripMenuItem1.Click
        Dim frm As New frmConfigureMovieFilters

        frm.Init(SplitContainer5.Panel2)

        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            UpdateMovieFiltersPanel()
            Pref.ConfigSave()
            UpdateFilteredList()
        End If
    End Sub


    Sub UpdateMovieFiltersPanel()
        'ResizeBottomLHSPanel
        Pref.movie_filters.UpdateFromPanel()
        ResizeBottomLHSPanel(MovieFiltersPanelMaxHeight)
        Pref.movie_filters.PositionMovieFilters()
    End Sub


    ReadOnly Property MovieFiltersPanelMaxHeight As Integer
        Get
            Return Pref.movie_filters.CalculatedFilterPanelHeight
        End Get
    End Property


    Private Sub SplitContainer5_DoubleClick(sender As Object, e As EventArgs) Handles SplitContainer5.DoubleClick

        If SplitContainer5.Panel2.Height = MovieFiltersPanelMaxHeight - 5 Then
            ResizeBottomLHSPanel(0)
        Else
            ResizeBottomLHSPanel(MovieFiltersPanelMaxHeight)
        End If
    End Sub


    Private Sub ResizeBottomLHSPanel(height As Integer, Optional ByVal MaxHeight As Integer = 0)
        ProgState = ProgramState.ResizingSplitterPanel

        SplitContainer5.SplitterDistance = If((SplitContainer5.Height - height) < 0, 0, SplitContainer5.Height - height)

        DataGridViewMovies.Height = SplitContainer5.SplitterDistance - 140

        If MaxHeight = 0 Then
            SplitContainer5.Panel2.AutoScrollMinSize = New Size(SplitContainer5.Panel2.AutoScrollMinSize.Width, height - 10)
        Else
            SplitContainer5.Panel2.AutoScrollMinSize = New Size(SplitContainer5.Panel2.AutoScrollMinSize.Width, MaxHeight - 10)
        End If

        ProgState = ProgramState.Other
    End Sub


    Private Sub ResizeBottomLHSPanel()
        If ProgState = ProgramState.ResizingSplitterPanel Then Return

        If Not MainFormLoadedStatus Then Return
        If Not SplitContainer5.Panel2.Visible Then Return

        Dim maxSize = MovieFiltersPanelMaxHeight
        Dim minSize = 2

        If SplitContainer5.Height - SplitContainer5.SplitterDistance > maxSize Then
            SplitContainer5.SplitterDistance = SplitContainer5.Height - maxSize
        End If

        If SplitContainer5.Height - SplitContainer5.SplitterDistance < minSize Then
            SplitContainer5.SplitterDistance = SplitContainer5.Height - minSize
        End If

        'Needed as workaround for splitter panel framework bug:
        Dim h = SplitContainer5.SplitterDistance - 140
        If h < minSize Then h = minSize
        DataGridViewMovies.Height = h
    End Sub


    Private Function GetFilterFromLabel(ctl As Control)

        Dim name As String = ctl.Name.RemoveAfterMatch("Mode")

        Return ctl.Parent.Controls("cb" + name.Substring(3, name.Length - 3))
    End Function


    Private Sub ResetCbGeneralFilter(sender As Control, e As EventArgs) Handles lblFilterGeneral.Click
        cbFilterGeneral.SelectedIndex = 0
    End Sub


    Private Sub cbFilterGeneral_DropDown(sender As Object, e As EventArgs) Handles cbFilterGeneral.DropDown

        Dim maxWidth As Integer = cbFilterGeneral.DropDownWidth
        Dim g As Graphics = cbFilterGeneral.CreateGraphics
        Dim vertScrollBarWidth As Integer = If(cbFilterGeneral.Items.Count > cbFilterGeneral.MaxDropDownItems, SystemInformation.VerticalScrollBarWidth, 0)
        Dim renderedWidth As Integer
        Dim lbl As Label = New Label()

        lbl.AutoSize = True
        lbl.Font = cbFilterGeneral.Font

        For Each item As String In cbFilterGeneral.Items
            lbl.Text = item
            renderedWidth = lbl.PreferredSize.Width + vertScrollBarWidth
            maxWidth = Math.Max(maxWidth, renderedWidth)
        Next

        cbFilterGeneral.DropDownWidth = maxWidth
    End Sub


End Class
