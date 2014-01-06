Public Class frmImageBrowseOrUrl

    Public Property Cancelled As Boolean
    Public Property Tbstring As String = ""

    Private Sub LoadfrmImageBrowseOrUrl(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            'Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_Browse_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Browse.Click
        Try
            'Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            'browse
            'Form1.openFD.InitialDirectory = WorkingTvShow.NfoFilePath.Replace(IO.Path.GetFileName(WorkingTvShow.NfoFilePath), "")
            Dim newFD As New OpenFileDialog ()
            newFD.Title = "Select a jpeg image File"
            newFD.FileName = ""
            newFD.Filter = "Media Companion Image Files|*.jpg;*.tbn;*.png;*.bmp|All Files|*.*"
            newFD.FilterIndex = 0
            If newFD.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Tbstring = newFD.FileName.ToString
                tb_PathorUrl.Text = Tbstring 
            End if
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btn_SetThumb_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_SetThumb.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btn_Cancel_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Cancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmMessageBox_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If  e.KeyCode=Keys.Escape Then
            btn_Cancel.PerformClick()
        End If
    End Sub

End Class