Friend NotInheritable Class ExceptionHandler
    Friend Shared Sub LogError(ByVal ex As Exception)
        Try
            Dim ofrmExcept As New frmExceptions
            ofrmExcept.txtExceptionTrace.Text = ex.ToString
            ofrmExcept.ShowDialog()
        Catch ex1 As Exception
            Dim log As New EventLog
            Dim source As String = "MediaCompanion"
            If Not EventLog.SourceExists(source) Then
                EventLog.CreateEventSource(source, source)
            End If
            log.Source = source
            log.EnableRaisingEvents = True
            log.WriteEntry(ex1.ToString, EventLogEntryType.Error)
            log.WriteEntry(ex.ToString, EventLogEntryType.Error)
        End Try

    End Sub
End Class
