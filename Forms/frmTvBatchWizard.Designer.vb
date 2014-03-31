<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class tv_batch_wizard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cbshXtraFanart = New System.Windows.Forms.CheckBox()
        Me.cbshSeason = New System.Windows.Forms.CheckBox()
        Me.cbshFanart = New System.Windows.Forms.CheckBox()
        Me.cbshPosters = New System.Windows.Forms.CheckBox()
        Me.cbshActor = New System.Windows.Forms.CheckBox()
        Me.cbshStudio = New System.Windows.Forms.CheckBox()
        Me.cbshGenre = New System.Windows.Forms.CheckBox()
        Me.cbshMpaa = New System.Windows.Forms.CheckBox()
        Me.cbshRuntime = New System.Windows.Forms.CheckBox()
        Me.cbshPlot = New System.Windows.Forms.CheckBox()
        Me.cbshRating = New System.Windows.Forms.CheckBox()
        Me.cbshYear = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbepStreamDetails = New System.Windows.Forms.CheckBox()
        Me.cbepCreateScreenshot = New System.Windows.Forms.CheckBox()
        Me.cbepScreenshot = New System.Windows.Forms.CheckBox()
        Me.cbepRuntime = New System.Windows.Forms.CheckBox()
        Me.cbepActor = New System.Windows.Forms.CheckBox()
        Me.cbepCredits = New System.Windows.Forms.CheckBox()
        Me.cbepDirector = New System.Windows.Forms.CheckBox()
        Me.cbepRating = New System.Windows.Forms.CheckBox()
        Me.cbepAired = New System.Windows.Forms.CheckBox()
        Me.cbepPlot = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.cbRewiteAllNfo = New System.Windows.Forms.CheckBox()
        Me.btn_TvBatchCancel = New System.Windows.Forms.Button()
        Me.btnTvBatchStart = New System.Windows.Forms.Button()
        Me.cbincludeLocked = New System.Windows.Forms.CheckBox()
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.GroupBox1.SuspendLayout
        Me.GroupBox2.SuspendLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer2.Panel1.SuspendLayout
        Me.SplitContainer2.Panel2.SuspendLayout
        Me.SplitContainer2.SuspendLayout
        Me.SuspendLayout
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = true
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Size = New System.Drawing.Size(416, 359)
        Me.SplitContainer1.SplitterDistance = 197
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cbshXtraFanart)
        Me.GroupBox1.Controls.Add(Me.cbshSeason)
        Me.GroupBox1.Controls.Add(Me.cbshFanart)
        Me.GroupBox1.Controls.Add(Me.cbshPosters)
        Me.GroupBox1.Controls.Add(Me.cbshActor)
        Me.GroupBox1.Controls.Add(Me.cbshStudio)
        Me.GroupBox1.Controls.Add(Me.cbshGenre)
        Me.GroupBox1.Controls.Add(Me.cbshMpaa)
        Me.GroupBox1.Controls.Add(Me.cbshRuntime)
        Me.GroupBox1.Controls.Add(Me.cbshPlot)
        Me.GroupBox1.Controls.Add(Me.cbshRating)
        Me.GroupBox1.Controls.Add(Me.cbshYear)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(364, 162)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = false
        Me.GroupBox1.Text = "Select Tags to Rescrape"
        '
        'cbshXtraFanart
        '
        Me.cbshXtraFanart.AutoSize = true
        Me.cbshXtraFanart.Location = New System.Drawing.Point(7, 136)
        Me.cbshXtraFanart.Name = "cbshXtraFanart"
        Me.cbshXtraFanart.Size = New System.Drawing.Size(300, 17)
        Me.cbshXtraFanart.TabIndex = 11
        Me.cbshXtraFanart.Text = "Search for and Download missing Extra Fanart, if available"
        Me.cbshXtraFanart.UseVisualStyleBackColor = true
        '
        'cbshSeason
        '
        Me.cbshSeason.AutoSize = true
        Me.cbshSeason.Location = New System.Drawing.Point(7, 90)
        Me.cbshSeason.Name = "cbshSeason"
        Me.cbshSeason.Size = New System.Drawing.Size(274, 17)
        Me.cbshSeason.TabIndex = 10
        Me.cbshSeason.Text = "Search and download missing Season art if available"
        Me.cbshSeason.UseVisualStyleBackColor = true
        '
        'cbshFanart
        '
        Me.cbshFanart.AutoSize = true
        Me.cbshFanart.Location = New System.Drawing.Point(7, 113)
        Me.cbshFanart.Name = "cbshFanart"
        Me.cbshFanart.Size = New System.Drawing.Size(265, 17)
        Me.cbshFanart.TabIndex = 9
        Me.cbshFanart.Text = "Search for and download missing fanart if available"
        Me.cbshFanart.UseVisualStyleBackColor = true
        '
        'cbshPosters
        '
        Me.cbshPosters.AutoSize = true
        Me.cbshPosters.Location = New System.Drawing.Point(7, 67)
        Me.cbshPosters.Name = "cbshPosters"
        Me.cbshPosters.Size = New System.Drawing.Size(277, 17)
        Me.cbshPosters.TabIndex = 8
        Me.cbshPosters.Text = "Search for and download missing poster/s if available"
        Me.cbshPosters.UseVisualStyleBackColor = true
        '
        'cbshActor
        '
        Me.cbshActor.AutoSize = true
        Me.cbshActor.Location = New System.Drawing.Point(198, 44)
        Me.cbshActor.Name = "cbshActor"
        Me.cbshActor.Size = New System.Drawing.Size(56, 17)
        Me.cbshActor.TabIndex = 7
        Me.cbshActor.Text = "Actors"
        Me.cbshActor.UseVisualStyleBackColor = true
        '
        'cbshStudio
        '
        Me.cbshStudio.AutoSize = true
        Me.cbshStudio.Location = New System.Drawing.Point(198, 21)
        Me.cbshStudio.Name = "cbshStudio"
        Me.cbshStudio.Size = New System.Drawing.Size(56, 17)
        Me.cbshStudio.TabIndex = 6
        Me.cbshStudio.Text = "Studio"
        Me.cbshStudio.UseVisualStyleBackColor = true
        '
        'cbshGenre
        '
        Me.cbshGenre.AutoSize = true
        Me.cbshGenre.Location = New System.Drawing.Point(284, 20)
        Me.cbshGenre.Name = "cbshGenre"
        Me.cbshGenre.Size = New System.Drawing.Size(55, 17)
        Me.cbshGenre.TabIndex = 5
        Me.cbshGenre.Text = "Genre"
        Me.cbshGenre.UseVisualStyleBackColor = true
        '
        'cbshMpaa
        '
        Me.cbshMpaa.AutoSize = true
        Me.cbshMpaa.Location = New System.Drawing.Point(105, 44)
        Me.cbshMpaa.Name = "cbshMpaa"
        Me.cbshMpaa.Size = New System.Drawing.Size(56, 17)
        Me.cbshMpaa.TabIndex = 4
        Me.cbshMpaa.Text = "MPAA"
        Me.cbshMpaa.UseVisualStyleBackColor = true
        '
        'cbshRuntime
        '
        Me.cbshRuntime.AutoSize = true
        Me.cbshRuntime.Location = New System.Drawing.Point(105, 21)
        Me.cbshRuntime.Name = "cbshRuntime"
        Me.cbshRuntime.Size = New System.Drawing.Size(65, 17)
        Me.cbshRuntime.TabIndex = 3
        Me.cbshRuntime.Text = "Runtime"
        Me.cbshRuntime.UseVisualStyleBackColor = true
        '
        'cbshPlot
        '
        Me.cbshPlot.AutoSize = true
        Me.cbshPlot.Location = New System.Drawing.Point(284, 44)
        Me.cbshPlot.Name = "cbshPlot"
        Me.cbshPlot.Size = New System.Drawing.Size(44, 17)
        Me.cbshPlot.TabIndex = 2
        Me.cbshPlot.Text = "Plot"
        Me.cbshPlot.UseVisualStyleBackColor = true
        '
        'cbshRating
        '
        Me.cbshRating.AutoSize = true
        Me.cbshRating.Location = New System.Drawing.Point(7, 44)
        Me.cbshRating.Name = "cbshRating"
        Me.cbshRating.Size = New System.Drawing.Size(57, 17)
        Me.cbshRating.TabIndex = 1
        Me.cbshRating.Text = "Rating"
        Me.cbshRating.UseVisualStyleBackColor = true
        '
        'cbshYear
        '
        Me.cbshYear.AutoSize = true
        Me.cbshYear.Location = New System.Drawing.Point(7, 20)
        Me.cbshYear.Name = "cbshYear"
        Me.cbshYear.Size = New System.Drawing.Size(73, 17)
        Me.cbshYear.TabIndex = 0
        Me.cbshYear.Text = "Premiered"
        Me.cbshYear.UseVisualStyleBackColor = true
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TV Show batch Wizard"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbepStreamDetails)
        Me.GroupBox2.Controls.Add(Me.cbepCreateScreenshot)
        Me.GroupBox2.Controls.Add(Me.cbepScreenshot)
        Me.GroupBox2.Controls.Add(Me.cbepRuntime)
        Me.GroupBox2.Controls.Add(Me.cbepActor)
        Me.GroupBox2.Controls.Add(Me.cbepCredits)
        Me.GroupBox2.Controls.Add(Me.cbepDirector)
        Me.GroupBox2.Controls.Add(Me.cbepRating)
        Me.GroupBox2.Controls.Add(Me.cbepAired)
        Me.GroupBox2.Controls.Add(Me.cbepPlot)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 29)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(364, 109)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = false
        Me.GroupBox2.Text = "Select Tags to Rescrape"
        '
        'cbepStreamDetails
        '
        Me.cbepStreamDetails.AutoSize = true
        Me.cbepStreamDetails.Location = New System.Drawing.Point(283, 44)
        Me.cbepStreamDetails.Name = "cbepStreamDetails"
        Me.cbepStreamDetails.Size = New System.Drawing.Size(82, 17)
        Me.cbepStreamDetails.TabIndex = 9
        Me.cbepStreamDetails.Text = "Media Tags"
        Me.cbepStreamDetails.UseVisualStyleBackColor = true
        '
        'cbepCreateScreenshot
        '
        Me.cbepCreateScreenshot.AutoSize = true
        Me.cbepCreateScreenshot.Enabled = false
        Me.cbepCreateScreenshot.Location = New System.Drawing.Point(6, 90)
        Me.cbepCreateScreenshot.Name = "cbepCreateScreenshot"
        Me.cbepCreateScreenshot.Size = New System.Drawing.Size(265, 17)
        Me.cbepCreateScreenshot.TabIndex = 8
        Me.cbepCreateScreenshot.Text = "Create Screenshot if none is available to download"
        Me.cbepCreateScreenshot.UseVisualStyleBackColor = true
        '
        'cbepScreenshot
        '
        Me.cbepScreenshot.AutoSize = true
        Me.cbepScreenshot.Location = New System.Drawing.Point(6, 67)
        Me.cbepScreenshot.Name = "cbepScreenshot"
        Me.cbepScreenshot.Size = New System.Drawing.Size(220, 17)
        Me.cbepScreenshot.TabIndex = 7
        Me.cbepScreenshot.Text = "Attempt to download missing screenshots"
        Me.cbepScreenshot.UseVisualStyleBackColor = true
        '
        'cbepRuntime
        '
        Me.cbepRuntime.AutoSize = true
        Me.cbepRuntime.Location = New System.Drawing.Point(283, 20)
        Me.cbepRuntime.Name = "cbepRuntime"
        Me.cbepRuntime.Size = New System.Drawing.Size(65, 17)
        Me.cbepRuntime.TabIndex = 6
        Me.cbepRuntime.Text = "Runtime"
        Me.cbepRuntime.UseVisualStyleBackColor = true
        '
        'cbepActor
        '
        Me.cbepActor.AutoSize = true
        Me.cbepActor.Location = New System.Drawing.Point(197, 44)
        Me.cbepActor.Name = "cbepActor"
        Me.cbepActor.Size = New System.Drawing.Size(56, 17)
        Me.cbepActor.TabIndex = 5
        Me.cbepActor.Text = "Actors"
        Me.cbepActor.UseVisualStyleBackColor = true
        '
        'cbepCredits
        '
        Me.cbepCredits.AutoSize = true
        Me.cbepCredits.Location = New System.Drawing.Point(104, 44)
        Me.cbepCredits.Name = "cbepCredits"
        Me.cbepCredits.Size = New System.Drawing.Size(58, 17)
        Me.cbepCredits.TabIndex = 4
        Me.cbepCredits.Text = "Credits"
        Me.cbepCredits.UseVisualStyleBackColor = true
        '
        'cbepDirector
        '
        Me.cbepDirector.AutoSize = true
        Me.cbepDirector.Location = New System.Drawing.Point(104, 20)
        Me.cbepDirector.Name = "cbepDirector"
        Me.cbepDirector.Size = New System.Drawing.Size(63, 17)
        Me.cbepDirector.TabIndex = 3
        Me.cbepDirector.Text = "Director"
        Me.cbepDirector.UseVisualStyleBackColor = true
        '
        'cbepRating
        '
        Me.cbepRating.AutoSize = true
        Me.cbepRating.Location = New System.Drawing.Point(197, 20)
        Me.cbepRating.Name = "cbepRating"
        Me.cbepRating.Size = New System.Drawing.Size(57, 17)
        Me.cbepRating.TabIndex = 2
        Me.cbepRating.Text = "Rating"
        Me.cbepRating.UseVisualStyleBackColor = true
        '
        'cbepAired
        '
        Me.cbepAired.AutoSize = true
        Me.cbepAired.Location = New System.Drawing.Point(6, 44)
        Me.cbepAired.Name = "cbepAired"
        Me.cbepAired.Size = New System.Drawing.Size(64, 17)
        Me.cbepAired.TabIndex = 1
        Me.cbepAired.Text = "Air Date"
        Me.cbepAired.UseVisualStyleBackColor = true
        '
        'cbepPlot
        '
        Me.cbepPlot.AutoSize = true
        Me.cbepPlot.Location = New System.Drawing.Point(6, 20)
        Me.cbepPlot.Name = "cbepPlot"
        Me.cbepPlot.Size = New System.Drawing.Size(44, 17)
        Me.cbepPlot.TabIndex = 0
        Me.cbepPlot.Text = "Plot"
        Me.cbepPlot.UseVisualStyleBackColor = true
        '
        'Label2
        '
        Me.Label2.AutoSize = true
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Episode Batch Wizard"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.IsSplitterFixed = true
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.cbRewiteAllNfo)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btn_TvBatchCancel)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnTvBatchStart)
        Me.SplitContainer2.Panel2.Controls.Add(Me.cbincludeLocked)
        Me.SplitContainer2.Size = New System.Drawing.Size(416, 417)
        Me.SplitContainer2.SplitterDistance = 359
        Me.SplitContainer2.TabIndex = 1
        '
        'cbRewiteAllNfo
        '
        Me.cbRewiteAllNfo.AutoSize = true
        Me.cbRewiteAllNfo.Location = New System.Drawing.Point(18, 25)
        Me.cbRewiteAllNfo.Name = "cbRewiteAllNfo"
        Me.cbRewiteAllNfo.Size = New System.Drawing.Size(108, 17)
        Me.cbRewiteAllNfo.TabIndex = 3
        Me.cbRewiteAllNfo.Text = "Rewrite All NFO's"
        Me.cbRewiteAllNfo.UseVisualStyleBackColor = true
        '
        'btn_TvBatchCancel
        '
        Me.btn_TvBatchCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_TvBatchCancel.Location = New System.Drawing.Point(240, 11)
        Me.btn_TvBatchCancel.Name = "btn_TvBatchCancel"
        Me.btn_TvBatchCancel.Size = New System.Drawing.Size(75, 23)
        Me.btn_TvBatchCancel.TabIndex = 2
        Me.btn_TvBatchCancel.Text = "Cancel"
        Me.btn_TvBatchCancel.UseVisualStyleBackColor = true
        '
        'btnTvBatchStart
        '
        Me.btnTvBatchStart.Location = New System.Drawing.Point(321, 11)
        Me.btnTvBatchStart.Name = "btnTvBatchStart"
        Me.btnTvBatchStart.Size = New System.Drawing.Size(75, 23)
        Me.btnTvBatchStart.TabIndex = 1
        Me.btnTvBatchStart.Text = "Start Update"
        Me.btnTvBatchStart.UseVisualStyleBackColor = true
        '
        'cbincludeLocked
        '
        Me.cbincludeLocked.AutoSize = true
        Me.cbincludeLocked.Location = New System.Drawing.Point(18, 8)
        Me.cbincludeLocked.Name = "cbincludeLocked"
        Me.cbincludeLocked.Size = New System.Drawing.Size(129, 17)
        Me.cbincludeLocked.TabIndex = 0
        Me.cbincludeLocked.Text = "Include locked shows"
        Me.cbincludeLocked.UseVisualStyleBackColor = true
        '
        'tv_batch_wizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_TvBatchCancel
        Me.ClientSize = New System.Drawing.Size(416, 417)
        Me.ControlBox = false
        Me.Controls.Add(Me.SplitContainer2)
        Me.KeyPreview = true
        Me.MaximumSize = New System.Drawing.Size(424, 444)
        Me.MinimumSize = New System.Drawing.Size(424, 444)
        Me.Name = "tv_batch_wizard"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "TV Batch Wizard"
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel1.PerformLayout
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        Me.SplitContainer1.Panel2.PerformLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.GroupBox1.ResumeLayout(false)
        Me.GroupBox1.PerformLayout
        Me.GroupBox2.ResumeLayout(false)
        Me.GroupBox2.PerformLayout
        Me.SplitContainer2.Panel1.ResumeLayout(false)
        Me.SplitContainer2.Panel2.ResumeLayout(false)
        Me.SplitContainer2.Panel2.PerformLayout
        CType(Me.SplitContainer2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer2.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbshPosters As System.Windows.Forms.CheckBox
    Friend WithEvents cbshActor As System.Windows.Forms.CheckBox
    Friend WithEvents cbshStudio As System.Windows.Forms.CheckBox
    Friend WithEvents cbshGenre As System.Windows.Forms.CheckBox
    Friend WithEvents cbshMpaa As System.Windows.Forms.CheckBox
    Friend WithEvents cbshRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents cbshPlot As System.Windows.Forms.CheckBox
    Friend WithEvents cbshRating As System.Windows.Forms.CheckBox
    Friend WithEvents cbshYear As System.Windows.Forms.CheckBox
    Friend WithEvents cbshFanart As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbepCreateScreenshot As System.Windows.Forms.CheckBox
    Friend WithEvents cbepScreenshot As System.Windows.Forms.CheckBox
    Friend WithEvents cbepRuntime As System.Windows.Forms.CheckBox
    Friend WithEvents cbepActor As System.Windows.Forms.CheckBox
    Friend WithEvents cbepCredits As System.Windows.Forms.CheckBox
    Friend WithEvents cbepDirector As System.Windows.Forms.CheckBox
    Friend WithEvents cbepRating As System.Windows.Forms.CheckBox
    Friend WithEvents cbepAired As System.Windows.Forms.CheckBox
    Friend WithEvents cbepPlot As System.Windows.Forms.CheckBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents cbincludeLocked As System.Windows.Forms.CheckBox
    Friend WithEvents btnTvBatchStart As System.Windows.Forms.Button
    Friend WithEvents btn_TvBatchCancel As System.Windows.Forms.Button
    Friend WithEvents cbepStreamDetails As System.Windows.Forms.CheckBox
    Friend WithEvents cbRewiteAllNfo As System.Windows.Forms.CheckBox
    Friend WithEvents cbshSeason As System.Windows.Forms.CheckBox
    Friend WithEvents cbshXtraFanart As System.Windows.Forms.CheckBox
End Class
