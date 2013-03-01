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
        Try
            'Me.TopMost = True
            CheckForIllegalCrossThreadCalls = False

            Dim sAssemblyVersion As String = Trim(System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(",")(1))
            Label2.Text = String.Format("Version {0}", Microsoft.VisualBasic.Right(sAssemblyVersion, 7))

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

End Class