Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Linq

Public Class frmXbmcExport
    Private MovieList As New List(Of xbmcmovies)
    Private TVSeries As New List(Of xbmctvseries)
    Private OutputFolder As String = Nothing

    Private Sub frmXbmcExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        GetTally()
    End Sub

    Private Sub GetTally()
        Dim data = From c In Form1.oMovies.Data_GridViewMovieCache Select New With {Key .Title = c.DisplayTitleAndYear, Key .NFOpath = c.fullpathandfilename}
        For each row in data
            Dim movie As New xbmcmovies
            movie.fileinfo.fullpathandfilename = row.NFOpath
            MovieList.Add(movie)
        Next

    End Sub


    Private Sub frmMediaInfoEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

End Class