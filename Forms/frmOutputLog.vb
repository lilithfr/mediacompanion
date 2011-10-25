Imports System.IO

Public Class frmoutputlog
    Public output As String = ""
    Private Sub frmoutputlog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            TextBox1.Text = output
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
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
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
            TextBox1.Select(TextBox1.Text.Length, 0)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
End Class