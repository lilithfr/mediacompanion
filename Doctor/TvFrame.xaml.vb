Class TvFrame
    Public NfoFile As New Media_Companion.TvShow

    Private Sub Button1_Click(sender As System.Object, e As System.Windows.RoutedEventArgs) Handles Button1.Click
        NfoFile.NfoFilePath = NfoPath.Text
        NfoFile.Load()
        DisplayGrid.DataContext = NfoFile
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.Windows.RoutedEventArgs)
        NfoFile.Save()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'DisplayGrid.DataContext = NfoFile
    End Sub
End Class
