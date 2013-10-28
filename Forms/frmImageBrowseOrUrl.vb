Public Class frmImageBrowseOrUrl

    Public Property Cancelled As Boolean

    Private Sub LoadfrmImageBrowseOrUrl(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_Browse_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Browse.Click
        Try
            'Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            'browse
            'Form1.openFD.InitialDirectory = WorkingTvShow.NfoFilePath.Replace(IO.Path.GetFileName(WorkingTvShow.NfoFilePath), "")
            Form1.openFD.Title = "Select a jpeg image File"
            Form1.openFD.FileName = ""
            Form1.openFD.Filter = "Media Companion Image Files|*.jpg;*.tbn;*.png;*.bmp|All Files|*.*"
            Form1.openFD.FilterIndex = 0
            Form1.openFD.ShowDialog()
            tb_PathorUrl.Text = Form1.openFD.FileName
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_SetThumb_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_SetThumb.Click

    End Sub

    Private Sub btn_Cancel_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Cancel.Click

    End Sub

    Private Sub frmMessageBox_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If  e.KeyCode=Keys.Escape Then
            Cancelled = True
        End If
    End Sub

End Class