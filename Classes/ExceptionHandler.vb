Imports System.Windows.Forms
Imports System.Threading


Friend NotInheritable Class ExceptionHandler
    Friend Shared Sub LogError(ByVal ex As Exception)
        If TypeOf (ex) Is UnauthorizedAccessException Then
            Dim ErrorMessage As String
            ErrorMessage = "Unable to write to file.  Please verify file permissions" + vbCrLf + vbCrLf + ex.Message
            ErrorMessage += vbCrLf + vbCrLf + "Now exiting."
            MessageBox.Show(ErrorMessage, "Unable to write Metadata", MessageBoxButtons.OK, MessageBoxIcon.Error)
            For Each frmOpen As Form In My.Application.OpenForms
                frmOpen.Close()
            Next

        Else
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
        End If
    End Sub
End Class
