Class TvEpisodeFrame
    Public NfoFile As New Nfo.TvEpisode
    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        NfoFile.NfoFilePath = NfoPath.Text
        NfoFile.Load()
        DisplayGrid.DataContext = NfoFile
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        NfoFile.Save()
    End Sub
End Class
