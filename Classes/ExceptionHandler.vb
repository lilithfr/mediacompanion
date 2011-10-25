Friend NotInheritable Class ExceptionHandler
    Friend Shared Sub LogError(ByVal ex As Exception)
        Dim ofrmExcept As New frmExceptions
        ofrmExcept.txtExceptionTrace.Text = ex.ToString
        ofrmExcept.ShowDialog()
    End Sub
End Class
