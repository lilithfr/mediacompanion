Class TvEpisodeFrame
    Public NfoFile As New Nfo.TvEpisode
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        NfoFile.NfoFilePath = NfoPath.Text
        NfoFile.Load()
        DisplayGrid.DataContext = NfoFile
    End Sub
End Class
