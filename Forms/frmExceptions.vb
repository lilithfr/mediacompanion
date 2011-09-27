Imports System.Threading

Public Class frmExceptions

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        For Each frmOpen As Form In My.Application.OpenForms
            frmOpen.Close() 'Close form instead of existing application so background threads are properly terminated.
        Next
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim thCopy As New Thread(AddressOf CopyText)
        thCopy.SetApartmentState(ApartmentState.STA)
        thCopy.Start()
    End Sub

    Private Sub lnkCodeplex_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCodeplex.LinkClicked
        System.Diagnostics.Process.Start(lnkCodeplex.Text)
    End Sub

    Private Sub frmExceptions_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If (e.CloseReason = CloseReason.UserClosing) Then
            e.Cancel = True
        End If
    End Sub

    Private Sub CopyText()
        Clipboard.SetText(Me.txtExceptionTrace.Text)
    End Sub
End Class