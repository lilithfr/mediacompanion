﻿Imports System.ComponentModel
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
            btn_Start.Enabled = value
        End If
      End Set
    End Property

    Private Sub frmXbmcExport_Load(sender As Object, e As EventArgs) Handles Me.Load
        GetTally()
        UpdateMediaTally()
        ShowMCPaths()
        TextBox1.Text = "F:\_test publish"
        btn_Validate.PerformClick()
    End Sub

    Private Sub GetTally()
        Dim data = From c In Form1.oMovies.Data_GridViewMovieCache Select New With {Key .Title = c.DisplayTitleAndYear, Key .NFOpath = c.fullpathandfilename, Key .path = c.foldername}
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
            currentmovie = Transposemovie(Moviefound.fileinfo.fullpathandfilename)
            'currentmovie.Load(Moviefound.fileinfo.fullpathandfilename)
            'If currentmovie.FirstChild.NodeType = XmlNodeType.XmlDeclaration Then currentmovie.RemoveChild(currentmovie.FirstChild)
            'child = doc.CreateElement("path")
            'child.InnerText = Moviefound.fileinfo.path
            'currentmovie.InsertAfter(child, currentmovie.FirstChild)
            'Dim oDoc As XMLNode = root.OwnerDocument.ImportNode(currentmovie.DocumentElement, True)
            'root.AppendChild(oDoc)
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

    Private Function Transposemovie(ByVal nfopath) As XmlDocument  
        Dim mov As FullMovieDetails = WorkingWithNfoFiles.mov_NfoLoadFull(nfopath)
        Dim thismovie As New XmlDocument
        Try
            Dim stage As Integer = 0
            Dim root As XmlElement = Nothing
            Dim child As XmlElement = Nothing
            Dim actorchild As XmlElement = Nothing
            Dim filedetailschild As XmlElement = Nothing
            Dim filedetailschildchild As XmlElement = Nothing
            Dim anotherchild As XmlElement = Nothing

            root = thismovie.CreateElement("movie")
            stage = 3
            child = thismovie.CreateElement("fileinfo")
            anotherchild = thismovie.CreateElement("streamdetails")
            filedetailschild = thismovie.CreateElement("video")
            filedetailschildchild = thismovie.CreateElement("width")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Width.Value), "", mov.filedetails.filedetails_video.Width.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            stage = 4
            filedetailschildchild = thismovie.CreateElement("height")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Height.Value), "", mov.filedetails.filedetails_video.Height.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = thismovie.CreateElement("aspect")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Aspect.Value), "", mov.filedetails.filedetails_video.Aspect.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            stage = 5
            filedetailschildchild = thismovie.CreateElement("codec")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Codec.Value), "", mov.filedetails.filedetails_video.Codec.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            stage = 7
            filedetailschildchild = thismovie.CreateElement("durationinseconds")
            filedetailschildchild.InnerText = If(mov.filedetails.filedetails_video.DurationInSeconds.Value = "-1", "0", mov.filedetails.filedetails_video.DurationInSeconds.Value)
            filedetailschild.AppendChild(filedetailschildchild)
            
            stage = 15
                anotherchild.AppendChild(filedetailschild)
            stage = 16
            For Each item In mov.filedetails.filedetails_audio
                filedetailschild = thismovie.CreateElement("audio")
                filedetailschildchild = thismovie.CreateElement("codec")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Codec.Value), "", item.Codec.Value)
                filedetailschild.AppendChild(filedetailschildchild)

                filedetailschildchild = thismovie.CreateElement("language")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Language.Value), "", item.Language.Value)
                filedetailschild.AppendChild(filedetailschildchild)

                filedetailschildchild = thismovie.CreateElement("channels")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Channels.Value), "", item.Channels.Value)
                filedetailschild.AppendChild(filedetailschildchild)
                        
                anotherchild.AppendChild(filedetailschild)
            Next

            stage = 17
            filedetailschild = thismovie.CreateElement("subtitle")
            For Each entry In mov.filedetails.filedetails_subtitles
                filedetailschildchild = thismovie.CreateElement("language")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(entry.Language.Value), "", entry.Language.Value)
                filedetailschild.AppendChild(filedetailschildchild)
            Next
            anotherchild.AppendChild(filedetailschild)

            stage = 18
            child.AppendChild(anotherchild) : root.AppendChild(child)
            child = thismovie.CreateElement("title") : child.InnerText = mov.fullmoviebody.title : root.AppendChild(child)

            child = thismovie.CreateElement("originaltitle")
            child.InnerText = If(String.IsNullOrEmpty(mov.fullmoviebody.originaltitle), mov.fullmoviebody.title, mov.fullmoviebody.originaltitle)
            root.AppendChild(child) 
            
            If mov.fullmoviebody.movieset <> "-None-" Then
                Dim strArr() As String
                strArr = mov.fullmoviebody.movieset.Split("/")
                For count = 0 To strArr.Length - 1
                    child = thismovie.CreateElement("set")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If

            If String.IsNullOrEmpty(mov.fullmoviebody.sortorder) Then
                mov.fullmoviebody.sortorder = mov.fullmoviebody.title
            End If
            child = thismovie.CreateElement("sorttitle")
            child.InnerText = mov.fullmoviebody.sortorder
            root.AppendChild(child)
            stage = 19
            child = thismovie.CreateElement("year") : child.InnerText = mov.fullmoviebody.year : root.AppendChild(child)
            stage = 20
            child = thismovie.CreateElement("premiered") : child.InnerText = mov.fullmoviebody.premiered : root.AppendChild(child)
            child = thismovie.CreateElement("rating") : child.InnerText = mov.fullmoviebody.rating.ToRating.ToString("0.0", Form1.MyCulture) : root.AppendChild(child)
            stage = 21
            child = thismovie.CreateElement("votes")
            Dim votes As String = mov.fullmoviebody.votes
            If Not String.IsNullOrEmpty(votes) then
                If Not Preferences.MovThousSeparator Then
                    votes = votes.Replace(",", "")
                Else
                    If Not votes.Contains(",") Then
                        If votes.Length > 3 Then
                            votes = votes.Insert(votes.Length-3, ",")
                        End If
                        If votes.Length > 7 Then
                            votes = votes.Insert(votes.Length-7, ",")
                        End If
                    End If
                End If
            End If
                    
            child.InnerText = votes : root.AppendChild(child)
            child = thismovie.CreateElement("top250") : child.InnerText = mov.fullmoviebody.top250 : root.AppendChild(child)
            child = thismovie.CreateElement("outline") : child.InnerText = mov.fullmoviebody.outline : root.AppendChild(child)
            child = thismovie.CreateElement("plot") : child.InnerText = mov.fullmoviebody.plot : root.AppendChild(child)
            child = thismovie.CreateElement("tagline") : child.InnerText = mov.fullmoviebody.tagline : root.AppendChild(child)
            child = thismovie.CreateElement("country") : child.InnerText = mov.fullmoviebody.country : root.AppendChild(child)

            stage = 22
            If Preferences.XtraFrodoUrls AndAlso Preferences.FrodoEnabled Then
                For Each item In mov.frodoPosterThumbs
                    child = thismovie.CreateElement("thumb")
                    child.SetAttribute("aspect", item.Aspect)
                    child.InnerText = item.Url
                    root.AppendChild(child)
                Next
                root.AppendChild(mov.frodoFanartThumbs.GetChild(thismovie))
                For Each thumbnail In mov.listthumbs
                    Try
                        child = thismovie.CreateElement("thumb")
                        child.InnerText = thumbnail
                        root.AppendChild(child)
                    Catch
                    End Try
                Next
            End If

            stage = 24
                child = thismovie.CreateElement("runtime")
                If mov.fullmoviebody.runtime <> Nothing Then
                    Dim minutes As String = mov.fullmoviebody.runtime
                    minutes = minutes.Replace("minutes", "")
                    minutes = minutes.Replace("mins", "")
                    minutes = minutes.Replace("min", "")
                    minutes = minutes.Replace(" ", "")
                    Try
                        If Not String.IsNullOrEmpty(minutes) AndAlso Convert.ToInt32(minutes) > 0 Then
                            Do While minutes.IndexOf("0") = 0 And minutes.Length > 0
                                minutes = minutes.Substring(1, minutes.Length - 1)
                            Loop
                            If Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) > 10 And Preferences.roundminutes = True Then
                                minutes = "0" & minutes
                            ElseIf Convert.ToInt32(minutes) < 100 And Convert.ToInt32(minutes) < 10 And Preferences.roundminutes = True Then
                                minutes = "00" & minutes
                            End If
                        End If
                        If Preferences.intruntime = False And IsNumeric(minutes) Then
                            If minutes = "0" AndAlso Not String.IsNullOrEmpty(mov.filedetails.filedetails_video.DurationInSeconds.Value) Then
                                Dim seconds As Integer = Convert.ToInt32(mov.filedetails.filedetails_video.DurationInSeconds.Value)
                                If seconds > 0 AndAlso seconds < 60 Then minutes = "1"
                            End If
                            minutes = minutes & " min"
                        End If
                    Catch ex As Exception
                        minutes = mov.fullmoviebody.runtime
                    End Try
                    child.InnerText = minutes
                Else
                    child.InnerText = mov.fullmoviebody.runtime
                End If
                root.AppendChild(child)
            stage = 25
                child = thismovie.CreateElement("mpaa") : child.InnerText = mov.fullmoviebody.mpaa : root.AppendChild(child)
            stage = 26
            If mov.fullmoviebody.genre <> "" Then
                Dim strArr() As String
                strArr = mov.fullmoviebody.genre.Split("/")
                For count = 0 To strArr.Length - 1
                    child = thismovie.CreateElement("genre")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If
            stage = 27
            If mov.fullmoviebody.tag.Count <> 0 Then
                For Each tags In mov.fullmoviebody.tag
                    child = thismovie.CreateElement("tag")
                    child.InnerText = tags
                    root.AppendChild(child)
                Next
            End If
            stage = 28
            child = thismovie.CreateElement("credits") : child.InnerText = mov.fullmoviebody.credits : root.AppendChild(child)
            stage = 29
            child = thismovie.CreateElement("director") : child.InnerText = mov.fullmoviebody.director : root.AppendChild(child)
            stage = 30
            child = thismovie.CreateElement("studio") : child.InnerText = mov.fullmoviebody.studio : root.AppendChild(child)
            stage = 31
            child = thismovie.CreateElement("trailer")
            child.InnerText = If(String.IsNullOrEmpty(mov.fullmoviebody.trailer), "", mov.fullmoviebody.trailer)
            root.AppendChild(child)
            stage = 32
            child = thismovie.CreateElement("playcount") : child.InnerText = mov.fullmoviebody.playcount : root.AppendChild(child)
            stage = 32
            child = thismovie.CreateElement("lastplayed") : child.InnerText = mov.fullmoviebody.lastplayed : root.AppendChild(child)
            stage = 33
                If Not String.IsNullOrEmpty(mov.fullmoviebody.imdbid) Then
                    child = thismovie.CreateElement("id")
                    child.InnerText = mov.fullmoviebody.imdbid
                    root.AppendChild(child)
                End If
                If Not String.IsNullOrEmpty(mov.fullmoviebody.tmdbid) Then
                    child = thismovie.CreateElement("tmdbid")
                    child.InnerText = mov.fullmoviebody.tmdbid
                    root.AppendChild(child)
                End If
                If Not String.IsNullOrEmpty(mov.fullmoviebody.source) Then
                    child = thismovie.CreateElement("videosource")
                    child.InnerText = mov.fullmoviebody.source
                    root.AppendChild(child)
                End If
                child = thismovie.CreateElement("createdate")
                If String.IsNullOrEmpty(mov.fileinfo.createdate) Then
                    Dim myDate2 As Date = System.DateTime.Now
                    Try
                        child.InnerText = Format(myDate2, Preferences.datePattern).ToString
                    Catch ex2 As Exception
                    End Try
                Else
                    child.InnerText = mov.fileinfo.createdate
                End If
                root.AppendChild(child)
            stage = 34
                child = thismovie.CreateElement("stars")
                child.InnerText = mov.fullmoviebody.stars
                root.AppendChild(child)
                Dim actorstosave As Integer = mov.listactors.Count
                If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
                For f = 0 To actorstosave - 1
                    child = thismovie.CreateElement("actor")
                    actorchild = thismovie.CreateElement("id")
                    actorchild.InnerText = mov.listactors(f).actorid
                    child.AppendChild(actorchild)
                    actorchild = thismovie.CreateElement("name")
                    actorchild.InnerText = mov.listactors(f).actorname
                    child.AppendChild(actorchild)
                    actorchild = thismovie.CreateElement("role")
                    actorchild.InnerText = mov.listactors(f).actorrole
                    child.AppendChild(actorchild)
                    actorchild = thismovie.CreateElement("thumb")
                    actorchild.InnerText = mov.listactors(f).actorthumb
                    child.AppendChild(actorchild)
                    actorchild = thismovie.CreateElement("order")
                    actorchild.InnerText = mov.listactors(f).order
                    child.AppendChild(actorchild)
                    root.AppendChild(child)
                Next
        Catch ex As Exception

        End Try
        Return thismovie 
    End Function

    Private Sub frmMediaInfoEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btn_Start_Click( sender As Object,  e As EventArgs) Handles btn_Start.Click
        RunExport()
    End Sub

    Private Sub btn_Cancel_Click( sender As Object,  e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_FolderBrowse_Click( sender As Object,  e As EventArgs) Handles btn_FolderBrowse.Click
        'Dim dialog As New FolderBrowserDialog()
        'dialog.RootFolder = Environment.SpecialFolder.Desktop
        'dialog.ShowNewFolderButton = True
        'dialog.SelectedPath = "C:\"
        'dialog.Description = "Select Path to save Exported data""
        'If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
        '    OutputFolder = dialog.SelectedPath
        'End If
        'If IsNothing(OutputFolder) Then
        '    MsgBox("No folder selected, export aborted")
        '    Exit Sub
        'End If 
        OutputFolder = Validatefolder(OutputFolder)
        outputfolderchecked = If(IsNothing(OutputFolder), False, True)
        TextBox1.Text = OutputFolder
    End Sub

    Private Sub btn_Validate_Click( sender As Object,  e As EventArgs) Handles btn_Validate.Click
        If String.IsNullOrEmpty(TextBox1.Text) Then
            outputfolderchecked = False
            Exit Sub
        End If
        OutputFolder = Validatefolder(TextBox1.Text)
        outputfolderchecked = If(IsNothing(OutputFolder), False, True)
        TextBox1.Text = OutputFolder
    End Sub

    Private Sub TextBox1_TextChanged( sender As Object,  e As EventArgs) Handles TextBox1.TextChanged
        If outputfolderchecked Then Exit Sub

    End Sub

End Class