Imports System.ComponentModel
Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Xml
Imports System.Linq

Public Class frmXbmcExport
    Private MovieList As New List(Of FullMovieDetails)
    Private TVSeries As New List(Of xbmctvseries)
    Dim xbmcexportfolder As String = "\xbmc_videodb_" & DateTime.Now.ToString("yyyy_MM_dd")
    Private OutputFolder As String = Nothing
    Private _outputfolderchecked As Boolean = False
    Private epcount As Integer = 0
    Public Property Bw  As BackgroundWorker = Nothing

    Property outputfolderchecked As Boolean
      Get
        Return _outputfolderchecked
      End Get
      Set(ByVal value as Boolean)
        If value <> _outputfolderchecked Then
            _outputfolderchecked = value
            pbCheck.Image = If(_outputfolderchecked, Global.Media_Companion.My.Resources.Resources.correct, Global.Media_Companion.My.Resources.Resources.incorrect)
        End If
      End Set
    End Property

    Private Sub frmXbmcExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        GetTally()
        UpdateMediaTally()
        ShowMCPaths()
    End Sub

    Private Sub GetTally()
        Dim data = From c In Form1.oMovies.Data_GridViewMovieCache Select New With {Key .Title = c.DisplayTitleAndYear, Key .NFOpath = c.fullpathandfilename}
        For each row in data
            Dim movie As New FullMovieDetails 
            movie.fileinfo.fullpathandfilename = row.NFOpath
            MovieList.Add(movie)
        Next
        For Each Sh In Cache.TvCache.Shows
            Dim tvshow As New xbmctvseries 
            tvshow.Series.Title = Sh.Title.Value
            tvshow.Series.Filenameandpath = Sh.NfoFilePath
            For Each ep In Sh.Episodes
                Dim epdata As New xbmctv
                epdata.Title = ep.Title.Value
                epdata.Season = ep.Season.Value
                epdata.Episode = ep.Episode.Value
                epdata.Filenameandpath = ep.NfoFilePath
                tvshow.episodes.Add(epdata)
            Next
            tvshow.series.Episode = tvshow.episodes.Count.ToString
            TVSeries.Add(tvshow)
        Next
    End Sub

    Private Sub ShowMCPaths()
        If MovieList.Count > 0 Then
            For Each ph In Preferences.movieFolders
                Dim n As Integer = MCExportdgv.Rows.Add()
                MCExportdgv.Rows(n).Cells(0).Value = "Movie"
                MCExportdgv.Rows(n).Cells(1).Value = ph
                MCExportdgv.Rows(n).Cells(2).Value = ph
            Next
        End If
        If TVSeries.Count > 0 Then
            For Each ph In Preferences.tvRootFolders
                Dim n As Integer = MCExportdgv.Rows.Add()
                MCExportdgv.Rows(n).Cells(0).Value = "TV"
                MCExportdgv.Rows(n).Cells(1).Value = ph
                MCExportdgv.Rows(n).Cells(2).Value = ph
            Next
        End If
    End Sub

    Private Sub UpdateMediaTally()
        For Each sh In TVSeries
            epcount += CType(sh.episodes.Count, Integer)
        Next
        lblMovieCount.Text = MovieList.Count.ToString
        lblTVCount.Text = TVSeries.Count.ToString & "  (" & epcount.ToString & " episodes)"
    End Sub

    Public Sub RunExport()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = MovieList.Count + TVSeries.Count + epcount
        'SetpathMapping()
        'SetOutputFolders()

        ''Create Document
        Dim doc As New XmlDocument
        Dim xmlproc As XmlDeclaration
        xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        doc.AppendChild(xmlproc)
        Dim root As XmlElement = Nothing
        root = doc.CreateElement("videodb")
        Dim version As XmlElement = Nothing
        version = doc.CreateElement("version")
        version.InnerText = "1"
        root.AppendChild(version)
        Dim child As XmlElement = Nothing
        ''Run Movie Output
        For Each Moviefound In MovieList
            Me.ProgressBar1.Value +=1
            If Not File.Exists(Moviefound.fileinfo.fullpathandfilename) Then Continue For
            Dim currentmovie As New XmlDocument
            currentmovie.Load(Moviefound.fileinfo.fullpathandfilename)
            If currentmovie.FirstChild.NodeType = XmlNodeType.XmlDeclaration Then currentmovie.RemoveChild(currentmovie.FirstChild)
            For Each no In currentmovie
                child = no
            Next
            root.AppendChild(child)
        Next
        doc.AppendChild(root)
        ''Run Tv Output


    End Sub

    Public Function Validatefolder(ByVal outputfolder As String) As String
        If Not Directory.Exists(outputfolder) Then
            MsgBox("Directory does not exist")
            Return Nothing
        End If
        'Dim xbmcexportfolder As String = "xbmc_videodb_" & DateTime.Now.ToString("yyyy_MM_dd")
       ' outputfolder = outputfolder & "\" & "xbmc_videodb_" & DateTime.Now.ToString("yyyy_MM_dd") 
        Try 'create new output folder
            If Not Directory.Exists(outputfolder & xbmcexportfolder) Then Directory.CreateDirectory(outputfolder & xbmcexportfolder)
        Catch
            MsgBox("Unable to create export folder")
            Return Nothing
        End Try
        'test path is writeable
        Try
            Dim fs As FileStream = File.Create(outputfolder & xbmcexportfolder & "\mc_dmmy.txt")

            ' Add text to the file. 
            Dim info As Byte() = New UTF8Encoding(True).GetBytes("This is some text in the file.")
            fs.Write(info, 0, info.Length)
            fs.Close()
        Catch
            MsgBox("Unable to create file in folder")
            Return Nothing
        End Try
        Try
            File.Delete(outputfolder & xbmcexportfolder & "\mc_dmmy.txt")
            Directory.Delete(outputfolder & xbmcexportfolder, True)
        Catch 
            MsgBox("Unable to Remove file in folder" &vbCrLf & "Check this folder's permissions")
            Return Nothing 
        End Try
        Return outputfolder 
    End Function

    'Private Function Transposemovie(ByVal nfopath) As xbmcmovies 
    '    Dim mov As FullMovieDetails = WorkingWithNfoFiles.mov_NfoLoadFull(nfopath)
        

    '    Return Transposemovie 
    'End Function
    Private Sub frmMediaInfoEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btn_Start_Click( sender As Object,  e As EventArgs) Handles btn_Start.Click
        'RunExport()
    End Sub

    Private Sub btn_Cancel_Click( sender As Object,  e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_FolderBrowse_Click( sender As Object,  e As EventArgs) Handles btn_FolderBrowse.Click
        Dim dialog As New FolderBrowserDialog()
        dialog.RootFolder = Environment.SpecialFolder.Desktop
        dialog.ShowNewFolderButton = True
        dialog.SelectedPath = "C:\"
        dialog.Description = "Select Path to save Exported data""
        If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            OutputFolder = dialog.SelectedPath
        End If
        If IsNothing(OutputFolder) Then
            MsgBox("No folder selected, export aborted")
            Exit Sub
        End If
        OutputFolder = Validatefolder(OutputFolder)
        outputfolderchecked = If(IsNothing(OutputFolder), False, True)
        TextBox1.Text = OutputFolder
    End Sub

    Private Sub btn_Validate_Click( sender As Object,  e As EventArgs) Handles btn_Validate.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then Exit Sub
        OutputFolder = Validatefolder(TextBox1.Text)
        outputfolderchecked = If(IsNothing(OutputFolder), False, True)
        TextBox1.Text = OutputFolder
    End Sub

    Private Sub TextBox1_TextChanged( sender As Object,  e As EventArgs) Handles TextBox1.TextChanged
        If outputfolderchecked Then Exit Sub

    End Sub

End Class