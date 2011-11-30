﻿Imports System.IO

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
            ComboBoxLogViewType.Items.Add("Breif")  'index 1 
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
        If ComboBoxLogViewType.SelectedIndex = 0 Then 'full
            TextBox1.Text = output  'full means we show all of the log (output)
            Preferences.logview = 0 'set the new preference, it will be saved when MC exits.
        End If
        If ComboBoxLogViewType.SelectedIndex = 1 Then 'breif we only show lines that contain "!!!" - this is a quick hack.....a better system would be required if more log view types were added.
            TextBox1.Text = ""
            Dim breifoutput() As String = output.Split(vbCrLf) 'split the lines out of output so we can check each one below
            For Each line In breifoutput
                If line.Contains("!!!") Then TextBox1.Text &= line & vbCrLf 'if logged textline contains this text it will appear in the brief logview 
            Next
            Preferences.logview = 1 'set the new preference, it will be saved when MC exits.
        End If
    End Sub
End Class