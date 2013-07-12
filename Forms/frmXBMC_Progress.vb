Imports System.IO

Public Class frmXBMC_Progress

    Private Sub panelXBMC_Click( sender As Object,  e As EventArgs) Handles panelXBMC.Click, LinkLabel1.Click

        System.Diagnostics.Process.Start(Path.Combine(My.Application.Info.DirectoryPath,Form1.XBMC_Controller_log_file))
            
    End Sub

    Private Sub lblQueueCount_MouseHover( sender As Object,  e As EventArgs) Handles lblQueueCount.MouseHover
        Dim qCount As Integer = 0

        Try
            qCount = Convert.ToInt32(lblQueueCount.Text)

            Dim s As String = ""

            If qCount = 0 Then 
                s = "No queued events"
            End If

            s  = Form1.XbmcControllerQ      .ToString
            s += Form1.XbmcControllerBufferQ.ToString

            ToolTip1.SetToolTip(lblQueueCount, s) 
        Catch
        End Try
    End Sub

    Private Sub frmXBMC_Progress_FormClosing( sender As Object,  e As FormClosingEventArgs) Handles MyBase.FormClosing
        WindowState = FormWindowState.Minimized
        e.Cancel    = True
    End Sub

    Private Sub frmXBMC_Progress_Shown( sender As Object,  e As EventArgs) Handles MyBase.Shown
        MaximizeBox = False
    End Sub

    Private Sub btnPurgeQ_Click( sender As Object,  e As EventArgs) Handles btnPurgeQ.Click
        Form1.XbmcControllerQ.Write(XbmcController.E.MC_PurgeQ_Req)     
    End Sub

End Class