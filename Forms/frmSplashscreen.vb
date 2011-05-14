Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports Media_Companion.ScraperFunctions
Imports System.Management


Public Class frmSplashscreen

    Dim tempstring As String = ""

    Private Sub splashscreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.TopMost = True
        CheckForIllegalCrossThreadCalls = False

        Dim sAssemblyVersion As String = Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1))
        Label2.Text = sAssemblyVersion

        'BackgroundWorker1.RunWorkerAsync()


        'Dim t As System.Threading.Thread
        't = New Thread(AddressOf MyMethod)
        't.IsBackground = True
        't.SetApartmentState(ApartmentState.STA)
        't.Start()











    End Sub


    'Private Sub BackgroundWorker1_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


    '    Try

    '        Do While tempstring <> "done"
    '            BackgroundWorker1.ReportProgress(100)

    '            Threading.Thread.Sleep(500)
    '            tempstring = Form1.loadinginfo
    '        Loop
    '        BackgroundWorker1.CancelAsync()
    '    Catch ex As Exception
    '        MsgBox(ex.Message.ToString)
    '    End Try


    'End Sub










    'Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged
    '    Me.Refresh()
    '    Application.DoEvents()
    'End Sub


    'Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
    '    Dim scraperlog As String
    '    If Not (e.Error Is Nothing) Then
    '        scraperlog = scraperlog & vbCrLf
    '        scraperlog = scraperlog & "Error, exiting movie scraper" & vbCrLf
    '        scraperlog = scraperlog & "Error:-" & vbCrLf
    '        scraperlog = scraperlog & e.Error.ToString & vbCrLf
    '    End If









    '    Me.Close()
    'End Sub
End Class