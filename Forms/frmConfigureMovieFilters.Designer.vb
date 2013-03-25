<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigureMovieFilters
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
        Me.tbMovieFilters = New System.Windows.Forms.TextBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.clbMovieFilters = New System.Windows.Forms.CheckedListBox()
        Me.SuspendLayout
        '
        'tbMovieFilters
        '
        Me.tbMovieFilters.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.tbMovieFilters.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbMovieFilters.Location = New System.Drawing.Point(0, 0)
        Me.tbMovieFilters.Multiline = true
        Me.tbMovieFilters.Name = "tbMovieFilters"
        Me.tbMovieFilters.Size = New System.Drawing.Size(213, 232)
        Me.tbMovieFilters.TabIndex = 2
        '
        'btnApply
        '
        Me.btnApply.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnApply.Location = New System.Drawing.Point(155, 202)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(49, 23)
        Me.btnApply.TabIndex = 6
        Me.btnApply.Text = "Done"
        Me.btnApply.UseVisualStyleBackColor = true
        '
        'clbMovieFilters
        '
        Me.clbMovieFilters.CheckOnClick = true
        Me.clbMovieFilters.FormattingEnabled = true
        Me.clbMovieFilters.Location = New System.Drawing.Point(12, 12)
        Me.clbMovieFilters.Name = "clbMovieFilters"
        Me.clbMovieFilters.Size = New System.Drawing.Size(192, 184)
        Me.clbMovieFilters.TabIndex = 9
        '
        'frmConfigureMovieFilters
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(213, 232)
        Me.Controls.Add(Me.clbMovieFilters)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.tbMovieFilters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmConfigureMovieFilters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configure Movie Filters"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents tbMovieFilters As System.Windows.Forms.TextBox
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents clbMovieFilters As System.Windows.Forms.CheckedListBox
End Class
