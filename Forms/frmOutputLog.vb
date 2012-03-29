Imports System.IO

Public Class frmoutputlog
    Public output As String = ""
    Private Sub frmoutputlog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ShowLog() 'call the subroutine to actually show the log when the form is created.
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Public Sub New(ByVal displaystring As String, Optional ByVal forceoverride As Boolean = False)
        Try
            Me.InitializeComponent()
            If forceoverride = False Then
                Me.Close()
            End If
            output = displaystring
            ComboBoxLogViewType.Items.Add("Full")   'index 0
            ComboBoxLogViewType.Items.Add("Brief")  'index 1 
            ComboBoxLogViewType.SelectedIndex = Preferences.logview 'set the combobox entry as per the preferences
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try 'this is the save button, it will save the displayed text (full or brief)
            Dim strFileName As String

            With SaveFileDialog1
                .DefaultExt = "txt"
                .Filter = "Text Documents (*.txt)|*.txt|All Files(*.*)|*.*"
                .FilterIndex = 1
                .OverwritePrompt = True
                .Title = "Save Scraper Log Dialogue"
            End With

            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Try
                    strFileName = SaveFileDialog1.FileName
                    Dim file As StreamWriter
                    file = New StreamWriter(strFileName, False)
                    file.WriteLine(Now())
                    file.WriteLine()
                    file.Write(TextBox1.Text)
                    file.WriteLine()
                    file.Close()
                    MsgBox(strFileName & " Saved")
                Catch ex As Exception
                    MsgBox("Error" & vbCrLf & vbCrLf & ex.Message.ToString)
                End Try
            End If


        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.GotFocus
        Try
            TextBox1.Select(TextBox1.Text.Length, 0) 'this removes the whole text being selected when the form is created
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub ComboBoxLogViewType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBoxLogViewType.SelectedIndexChanged
        ShowLog() 'if we change the combobox selection display the relavent log
    End Sub
    Private Sub ShowLog()
        'DISPLAY FULL LOG
        If ComboBoxLogViewType.SelectedIndex = 0 Then 'full log display
            TextBox1.Text = output  'full means we show all of the log (output)
            Preferences.logview = 0 'set the new preference, it will be saved when MC exits.
        End If

        'DISPLAY BREIF LOG
        If ComboBoxLogViewType.SelectedIndex = 1 Then 'brief we only show lines that contain "!!!" - this is a quick hack.....a better system would be required if more log view types were added.
            TextBox1.Text = ""
            Dim briefoutput() As String = output.Split(vbCrLf) 'split the lines out of output so we can check each one below. second & rest of lines have extra char at front so we test first 4 chars.
            For Each line In briefoutput 'if each line is at least 4 chars then test if first 4 chars has "!!!". If true then add the line directly to the textbox minus the "!!!" on the front. 
                line = Strings.Replace(line, Chr(10), "") 'strips out the leading 'new line'
                If line = "!!!" Then
                    TextBox1.Text &= vbCrLf
                ElseIf line.Contains("!!! ") Then
                    TextBox1.Text &= Strings.Right(line, Strings.Len(line) - 4) & vbCrLf
                End If
            Next
            Preferences.logview = 1 'set the new preference, it will be saved when MC exits.
        End If
    End Sub
End Class