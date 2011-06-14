Imports System.IO

Public Class frmoutputlog
    Public output As String = ""
    Private Sub frmoutputlog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Text = output
    End Sub

    Public Sub New(ByVal displaystring As String, Optional ByVal forceoverride As Boolean = False)
       

        Me.InitializeComponent()
        If forceoverride = False Then
            Me.Close()
        End If
        output = displaystring
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
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


    End Sub
End Class