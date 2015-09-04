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
    Private Pathlist As New List(Of XBMCPaths)
    Private ExportPathList As New Dictionary(Of String, String)
    Dim xbmcexportfolder As String = "\xbmc_videodb_" & DateTime.Now.ToString("yyyy_MM_dd")
    Private OutputFolder As String = Nothing
    Private FullPathOut As String = Nothing
    Private opActors As String = Nothing
    Private opMovies As String = Nothing
    Private opTvshows As String = Nothing
    Private opMusicvideos As String = Nothing
    Private _outputfolderchecked As Boolean = False
    Private epcount As Integer = 0
    Public Property Bw  As BackgroundWorker = Nothing
    Public nfoFunction As New WorkingWithNfoFiles

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
        TextBox1.Text = "E:\_test publish"
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
                MCExportdgv.Rows(n).Cells(1).Value = ph.rpath & "\"
                MCExportdgv.Rows(n).Cells(2).Value = ph.rpath & "\"
            Next
        End If
        If TVSeries.Count > 0 Then
            For Each ph In Preferences.tvRootFolders
                Dim n As Integer = MCExportdgv.Rows.Add()
                MCExportdgv.Rows(n).Cells(0).Value = "TV"
                MCExportdgv.Rows(n).Cells(1).Value = ph.rpath & "\"
                MCExportdgv.Rows(n).Cells(2).Value = ph.rpath & "\"
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
        ProgressBar1.BackColor = Color.red
        SetpathMapping()
        SetOutputFolders()

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
        
        ''Run Movie Output
        For Each Moviefound In MovieList
            Me.ProgressBar1.Value +=1
            If Not File.Exists(Moviefound.fileinfo.fullpathandfilename) Then Continue For
            Dim mov As FullMovieDetails = WorkingWithNfoFiles.mov_NfoLoadFull(Moviefound.fileinfo.fullpathandfilename)
            Dim currentmovie As New XmlDocument
            currentmovie = Transposemovie(mov)
            Dim oDoc As XMLNode = root.OwnerDocument.ImportNode(currentmovie.DocumentElement, True)
            root.AppendChild(oDoc)
            TransMovArtWork(mov)
            TransActorImages(mov)
            Me.ProgressBar1.Refresh()
        Next
        If MovieList.Count > 0 Then
            For Each ph In Preferences.movieFolders
                Dim t As New XBMCPaths
                t.rootpath = ph.rpath
                t.pathsource = "movies"
                Pathlist.Add(t)
            Next
        End If
        doc.AppendChild(root)

        ''Run Tv Output
        For Each sh In TVSeries
            Me.ProgressBar1.Value +=1
            If Not File.Exists(sh.series.Filenameandpath) Then Continue For
            Dim tvsh As TvShow = nfoFunction.tv_NfoLoadFull(sh.series.Filenameandpath)
            Dim CurrentSh As New XmlDocument
            CurrentSh = TransposetvShows(tvsh, sh.episodes.count)
            TransTvActorImages(tvsh.ListActors, sh.series.Filenameandpath)
            For Each ep In sh.episodes
                If Not File.Exists(ep.Filenameandpath) Then Continue For
                Dim tvep As List(Of TvEpisode) = WorkingWithNfoFiles.ep_NfoLoad(ep.Filenameandpath)
                For each subep In tvep
                    If Not ep.Episode = subep.Episode.Value Then Continue For
                    Dim CurrentEp As New XmlDocument
                    CurrentEp = TransposeTvEp(subep, tvsh)
                    Dim oDoc2 As XMLNode = CurrentSh.ImportNode(CurrentEp.DocumentElement, True)
                    CurrentSh.DocumentElement.AppendChild(oDoc2)
                Next
                Me.ProgressBar1.Value +=1
                Me.ProgressBar1.Refresh()
            Next
            Dim oDoc As XMLNode = root.OwnerDocument.ImportNode(CurrentSh.DocumentElement, True)
            root.AppendChild(oDoc)
            Me.ProgressBar1.Refresh()
        Next
        If TVSeries.Count > 0 Then
            For Each ph In Preferences.tvRootFolders 
                Dim t As New XBMCPaths
                t.rootpath = ph.rpath
                t.pathsource = "tvshows"
                Pathlist.Add(t)
            Next
        End If

        Dim xbpaths As New XmlDocument
        xbpaths = SetPaths()
        Dim oDoc3 As XMLNode = root.OwnerDocument.ImportNode(xbpaths.DocumentElement, True)
        root.AppendChild(oDoc3)
        doc.AppendChild(root)
        Try
            Dim output As New XmlTextWriter(FullPathOut & "\videodb.xml", System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            output.Indentation = 4
            doc.WriteTo(output)
            output.Close()
        Catch
        End Try
        If ProgressBar1.Value = ProgressBar1.Maximum Then
            ProgressBar1.ForeColor = Color.Green
            ProgressBar1.BackColor = Color.Green
            btn_Cancel.Text = "Finished"
        End If
    End Sub

    Private Sub TransMovArtWork(ByVal mov As FullMovieDetails)
        Dim filenm As String = Utilities.SpacesToCharacter(mov.fullmoviebody.title & " " & mov.fullmoviebody.year, "_")
        filenm = filenm.Replace(":", "_")
        If File.Exists(mov.fileinfo.fanartpath) Then
            File.Copy(mov.fileinfo.fanartpath, opMovies & filenm & "-fanart.jpg", True)
        End If
        If File.Exists(mov.fileinfo.posterpath) Then
            File.Copy(mov.fileinfo.posterpath, opMovies & filenm & "-poster.jpg", True)
        End If
    End Sub

    Private Sub TransActorImages(ByVal mov As FullMovieDetails)
        Try
            For Each actr In mov.listactors
                Dim temppath = Preferences.GetActorPath(mov.fileinfo.fullpathandfilename, actr.actorname, actr.actorid)
                If Not String.IsNullOrEmpty(temppath) AndAlso IO.File.Exists(temppath) Then
                    Dim actfilename As String = Utilities.GetFileNameFromPath(temppath)
                    File.Copy(temppath, opActors & actfilename, True)
                End If
            Next
        Catch

        End Try
    End Sub
    
    Private Sub TransTvActorImages(ByVal TvActor As ActorList, ByVal nfopath As String)
        Try
            For Each actr In TvActor
                Dim temppath = Preferences.GetActorPath(nfopath, actr.actorname, actr.actorid)
                If Not String.IsNullOrEmpty(temppath) AndAlso IO.File.Exists(temppath) Then
                    Dim actfilename As String = Utilities.GetFileNameFromPath(temppath)
                    File.Copy(temppath, opActors & actfilename, True)
                End If
            Next
        Catch

        End Try
    End Sub
    
    Private Sub SetOutputFolders()
        FullPathOut = OutputFolder & xbmcexportfolder
        If Not Directory.Exists(FullPathOut) Then Directory.CreateDirectory(FullPathOut)
        opActors = FullPathOut & "\actors\"
        If Not Directory.Exists(opActors) Then Directory.CreateDirectory(opActors)
        opMovies = FullPathOut & "\movies\"
        If Not Directory.Exists(opMovies) Then Directory.CreateDirectory(opMovies)
        opTvshows = FullPathOut & "\tvshows\"
        If Not Directory.Exists(opTvshows) Then Directory.CreateDirectory(opTvshows)
        opMusicvideos = FullPathOut & "\musicvideos\"
        If Not Directory.Exists(opMusicvideos) Then Directory.CreateDirectory(opMusicvideos)
    End Sub

    Private Sub SetpathMapping()
        For Each row In MCExportdgv.Rows
            Dim src As String = row.cells(1).Value
            Dim xport As String = row.Cells(2).Value
            'If Not (src.EndsWith("\\") OrElse src.EndsWith("//")) Then   'AndAlso (src.EndsWith("\") OrElse src.EndsWith("/")) 
            '    src = src.Substring(0, src.Length-2)
            'End If
            'If Not (xport.EndsWith("\\") OrElse xport.EndsWith("//")) Then     'AndAlso (xport.EndsWith("\") OrElse xport.EndsWith("/")) 
            '    xport = xport.Substring(0, xport.Length-2)
            'End If
            If src = xport Then Continue For                         ' no need to convert path if source and export paths are the same.
            If ExportPathList.ContainsKey(src) Then Continue For
            ExportPathList.Add(src, row.cells(2).Value)
        Next
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

    Private Function Transposemovie(ByVal mov As FullMovieDetails) As XmlDocument
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
            child = thismovie.CreateElement("title") : child.InnerText = mov.fullmoviebody.title : root.AppendChild(child)

            child = thismovie.CreateElement("originaltitle")
            child.InnerText = If(String.IsNullOrEmpty(mov.fullmoviebody.originaltitle), mov.fullmoviebody.title, mov.fullmoviebody.originaltitle)
            root.AppendChild(child)

            If String.IsNullOrEmpty(mov.fullmoviebody.sortorder) Then mov.fullmoviebody.sortorder = mov.fullmoviebody.title
            child = thismovie.CreateElement("sorttitle") : child.InnerText = mov.fullmoviebody.sortorder : root.AppendChild(child)
            child = thismovie.CreateElement("rating") : child.InnerText = mov.fullmoviebody.rating.ToRating.ToString("0.0", Form1.MyCulture) : root.AppendChild(child)
            child = thismovie.CreateElement("epbookmark") : child.InnerText = "0.000000" : root.AppendChild(child)
            child = thismovie.CreateElement("year") : child.InnerText = mov.fullmoviebody.year : root.AppendChild(child)
            child = thismovie.CreateElement("top250") : child.InnerText = mov.fullmoviebody.top250 : root.AppendChild(child)

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

            child = thismovie.CreateElement("outline") : child.InnerText = mov.fullmoviebody.outline : root.AppendChild(child)
            child = thismovie.CreateElement("plot") : child.InnerText = mov.fullmoviebody.plot : root.AppendChild(child)
            child = thismovie.CreateElement("tagline") : child.InnerText = mov.fullmoviebody.tagline : root.AppendChild(child)

            child = thismovie.CreateElement("runtime")
            If mov.fullmoviebody.runtime <> Nothing Then
                Dim minutes As String = mov.fullmoviebody.runtime
                minutes = minutes.Replace("minutes", "")
                minutes = minutes.Replace("mins", "")
                minutes = minutes.Replace("min", "")
                minutes = minutes.Replace(" ", "")
                child.InnerText = minutes
            Else
                child.InnerText = "0"
            End If
            root.AppendChild(child)

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

            child = thismovie.CreateElement("mpaa") : child.InnerText = mov.fullmoviebody.mpaa : root.AppendChild(child)
            child = thismovie.CreateElement("showlink") : child.InnerText = mov.fullmoviebody.showlink : root.AppendChild(child)
            child = thismovie.CreateElement("playcount") : child.InnerText = mov.fullmoviebody.playcount : root.AppendChild(child)
            child = thismovie.CreateElement("lastplayed") : child.InnerText = mov.fullmoviebody.lastplayed : root.AppendChild(child)
            child = thismovie.CreateElement("path") : child.InnerText = TransposePath(mov.fileinfo.path) : root.AppendChild(child)
            child = thismovie.CreateElement("filenameandpath") : child.InnerText = TransposePath(mov.fileinfo.filenameandpath) : root.AppendChild(child)
            child = thismovie.CreateElement("basepath")
            Dim basepath As String = GetBasePath(mov) : child.InnerText = TransposePath(basepath) : root.AppendChild(child)
            child = thismovie.CreateElement("id") : child.InnerText = mov.fullmoviebody.imdbid : root.AppendChild(child)

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

            If mov.fullmoviebody.country <> "" Then
                Dim strArr() As String
                strArr = mov.fullmoviebody.country.Split(",")
                For count = 0 To strArr.Length - 1
                    child = thismovie.CreateElement("country")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If
            
            child = thismovie.CreateElement("set")
            child.InnerText = If(mov.fullmoviebody.movieset.MovieSetName = "-None-", "", mov.fullmoviebody.movieset.MovieSetName)
            root.AppendChild(child)

            If mov.fullmoviebody.tag.Count <> 0 Then
                For Each tags In mov.fullmoviebody.tag
                    child = thismovie.CreateElement("tag")
                    child.InnerText = tags
                    root.AppendChild(child)
                Next
            End If
            child = thismovie.CreateElement("credits") : child.InnerText = mov.fullmoviebody.credits : root.AppendChild(child)
            
            If mov.fullmoviebody.director <> "" Then
                Dim strArr() As String
                strArr = mov.fullmoviebody.director.Split("/")
                For count = 0 To strArr.Length - 1
                    child = thismovie.CreateElement("director")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If

            If mov.fullmoviebody.studio <> "" Then
                Dim strArr() As String
                strArr = mov.fullmoviebody.studio.Split(",")
                For count = 0 To strArr.Length - 1
                    child = thismovie.CreateElement("studio")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If
            child = thismovie.CreateElement("trailer")
            child.InnerText = If(String.IsNullOrEmpty(mov.fullmoviebody.trailer), "", If(mov.fullmoviebody.trailer.Contains("http"), mov.fullmoviebody.trailer, TransposePath(mov.fullmoviebody.trailer)))
            root.AppendChild(child)

            child = thismovie.CreateElement("fileinfo")
            anotherchild = thismovie.CreateElement("streamdetails")
            filedetailschild = thismovie.CreateElement("video")

            filedetailschildchild = thismovie.CreateElement("codec")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Codec.Value), "", mov.filedetails.filedetails_video.Codec.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = thismovie.CreateElement("aspect")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Aspect.Value), "", mov.filedetails.filedetails_video.Aspect.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            
            filedetailschildchild = thismovie.CreateElement("width")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Width.Value), "", mov.filedetails.filedetails_video.Width.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = thismovie.CreateElement("height")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(mov.filedetails.filedetails_video.Height.Value), "", mov.filedetails.filedetails_video.Height.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = thismovie.CreateElement("durationinseconds")
            filedetailschildchild.InnerText = If(mov.filedetails.filedetails_video.DurationInSeconds.Value = "-1", "0", mov.filedetails.filedetails_video.DurationInSeconds.Value)
            filedetailschild.AppendChild(filedetailschildchild)
            anotherchild.AppendChild(filedetailschild)

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

            filedetailschild = thismovie.CreateElement("subtitle")
            For Each entry In mov.filedetails.filedetails_subtitles
                filedetailschildchild = thismovie.CreateElement("language")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(entry.Language.Value), "", entry.Language.Value)
                filedetailschild.AppendChild(filedetailschildchild)
            Next
            anotherchild.AppendChild(filedetailschild)

            stage = 18
            child.AppendChild(anotherchild)
            root.AppendChild(child)

            Dim actorstosave As Integer = mov.listactors.Count
            If actorstosave > Preferences.maxactors Then actorstosave = Preferences.maxactors
            For f = 0 To actorstosave - 1
                child = thismovie.CreateElement("actor")
                actorchild = thismovie.CreateElement("name")
                actorchild.InnerText = mov.listactors(f).actorname
                child.AppendChild(actorchild)
                actorchild = thismovie.CreateElement("role")
                actorchild.InnerText = mov.listactors(f).actorrole
                child.AppendChild(actorchild)
                actorchild = thismovie.CreateElement("order")
                actorchild.InnerText = mov.listactors(f).order
                child.AppendChild(actorchild)
                actorchild = thismovie.CreateElement("thumb")
                actorchild.InnerText = mov.listactors(f).actorthumb
                child.AppendChild(actorchild)
                root.AppendChild(child)
            Next

            child = thismovie.CreateElement("resume")
                anotherchild = thismovie.CreateElement("position")
                anotherchild.InnerText = "0.000000"
            child.AppendChild(anotherchild)
                anotherchild = thismovie.CreateElement("total")
                anotherchild.InnerText = "0.000000"
            child.AppendChild(anotherchild)
            root.AppendChild(child)

            child = thismovie.CreateElement("dateadded") : child.InnerText = FormatDate(mov.fileinfo.createdate) : root.AppendChild(child)

            child = thismovie.CreateElement("art")
            anotherchild = thismovie.CreateElement("fanart")
            anotherchild.InnerText = TransposePath(mov.fileinfo.fanartpath)
            child.AppendChild(anotherchild)
            anotherchild = thismovie.CreateElement("poster")
            anotherchild.InnerText = TransposePath(mov.fileinfo.posterpath)
            child.AppendChild(anotherchild)
            root.AppendChild(child)

            thismovie.AppendChild(root)
        Catch ex As Exception

        End Try
        Return thismovie 
    End Function

    Private Function TransposetvShows(ByVal tvsh As TvShow, ByVal epcount As Integer) As XmlDocument
        Dim ThisTvShow As New XmlDocument
        Try
            Dim stage As Integer = 0
            Dim root As XmlElement = Nothing
            Dim child As XmlElement = Nothing
            Dim childchild As XmlElement = Nothing
            Dim childchildchild As XmlElement = Nothing
            Dim Attr As XmlAttribute = Nothing
            Dim tempppp As String = ""
            Dim actorchild As XmlElement = Nothing
            Dim filedetailschild As XmlElement = Nothing
            Dim filedetailschildchild As XmlElement = Nothing
            Dim anotherchild As XmlElement = Nothing
            root = ThisTvShow.CreateElement("tvshow")
            child = ThisTvShow.CreateElement("title") : child.InnerText = tvsh.Title.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("showtitle") : child.InnerText = tvsh.Title.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("rating") : child.InnerText = tvsh.Rating.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("epbookmark") : child.InnerText = "0.000000" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("year") : child.InnerText = tvsh.Year.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("top250") : child.InnerText = If(String.IsNullOrEmpty(tvsh.Top250.Value), "0", tvsh.Top250.Value) : root.AppendChild(child)
            child = ThisTvShow.CreateElement("season") : child.InnerText = "-1" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("episode") : child.InnerText = epcount : root.AppendChild(child)
            child = ThisTvShow.CreateElement("uniqueid") : child.InnerText = "" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("displayseason") : child.InnerText = "-1" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("displayepisode") : child.InnerText = "-1" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("votes") : child.InnerText = tvsh.Votes.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("plot") : child.InnerText = tvsh.Plot.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("tagline") : child.InnerText = tvsh.TagLine.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("runtime") : child.InnerText = tvsh.Runtime.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("mpaa") : child.InnerText = tvsh.Mpaa.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("playcount") : child.InnerText = tvsh.Playcount.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("lastplayed") : child.InnerText = tvsh.LastPlayed.Value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("file") : child.InnerText = "" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("path") : child.InnerText = TransposePath(tvsh.FolderPath) : root.AppendChild(child)
            child = ThisTvShow.CreateElement("filenameandpath") : child.InnerText = "" : root.AppendChild(child)
            child = ThisTvShow.CreateElement("basepath") : child.InnerText = TransposePath(tvsh.FolderPath) : root.AppendChild(child)

            child = ThisTvShow.CreateElement("episodeguide")
            childchild = ThisTvShow.CreateElement("url")
            tempppp = tvsh.TvdbId.value
            Attr = ThisTvShow.CreateAttribute("cache")
            Attr.Value = tempppp
            childchild.Attributes.Append(Attr)
            If Not IsNothing(tvsh.episodeguideurl) Then
                '"http://www.thetvdb.com/api/6E82FED600783400/series/" & tvdbid & "/all/" & language & ".zip"
                If tvsh.tvdbid.Value <> Nothing Then
                    If IsNumeric(tvsh.tvdbid.Value) Then
                        If tvsh.language.Value <> Nothing Then
                            If tvsh.language.Value <> "" Then
                                childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvsh.tvdbid.Value & "/all/" & tvsh.language.Value & ".zip"
                                child.AppendChild(childchild)
                            Else
                                childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvsh.tvdbid.Value & "/all/en.zip"
                                child.AppendChild(childchild)
                            End If
                        Else
                            childchild.InnerText = "http://www.thetvdb.com/api/6E82FED600783400/series/" & tvsh.tvdbid.Value & "/all/en.zip"
                            child.AppendChild(childchild)
                        End If
                    End If
                End If
            End If
            root.AppendChild(child)
            child = ThisTvShow.CreateElement("id") : child.InnerText = tvsh.TvdbId.Value : root.AppendChild(child)
            If tvsh.genre.Value <> "" Then
                Dim strArr() As String
                strArr = tvsh.genre.Value.Split("/")
                For count = 0 To strArr.Length - 1
                    child = ThisTvShow.CreateElement("genre")
                    strArr(count) = strArr(count).Trim
                    child.InnerText = strArr(count)
                    root.AppendChild(child)
                Next
            End If
            child = ThisTvShow.CreateElement("premiered") : child.InnerText = tvsh.Premiered.value : root.AppendChild(child)
            child = ThisTvShow.CreateElement("studio") : child.InnerText = tvsh.Studio.value : root.AppendChild(child)
            For each act As Actor In tvsh.ListActors
                child = ThisTvShow.CreateElement("actor")
                actorchild = ThisTvShow.CreateElement("actorid")
                actorchild.InnerText = act.actorid 
                child.AppendChild(actorchild)
                actorchild = ThisTvShow.CreateElement("name")
                actorchild.InnerText = act.actorname
                child.AppendChild(actorchild)
                actorchild = ThisTvShow.CreateElement("role")
                actorchild.InnerText = act.actorrole
                child.AppendChild(actorchild)
                actorchild = ThisTvShow.CreateElement("order")
                actorchild.InnerText = act.order
                child.AppendChild(actorchild)
                root.AppendChild(child)
                If Not String.IsNullOrEmpty(act.actorthumb) Then
                    actorchild = ThisTvShow.CreateElement("thumb")
                    actorchild.InnerText = act.actorthumb
                    child.AppendChild(actorchild)
                End If
            Next
            root.AppendChild(child)

            child = ThisTvShow.CreateElement("art")
            Dim titlepath As String = opTvshows & tvsh.Title.Value.Replace(" ", "_") & "\"
            If Not Directory.Exists(titlepath) Then Directory.CreateDirectory(titlepath)
            If File.Exists(tvsh.ImageBanner.Path) Then
                File.Copy(tvsh.ImageBanner.Path, titlepath & tvsh.ImageBanner.FileName.ToLower)
                childchild = ThisTvShow.CreateElement("banner")
                childchild.InnerText = TransposePath(tvsh.ImageBanner.Path)
                child.AppendChild(childchild)
            End If
            If File.Exists(tvsh.ImageFanart.Path) Then
                File.Copy(tvsh.ImageFanart.Path, titlepath & tvsh.ImageFanart.FileName.ToLower)
                childchild = ThisTvShow.CreateElement("fanart")
                childchild.InnerText = TransposePath(tvsh.ImageFanart.Path)
                child.AppendChild(childchild)
            End If
            If File.Exists(tvsh.ImagePoster.Path) Then
                File.Copy(tvsh.ImagePoster.Path, titlepath & tvsh.ImagePoster.FileName.ToLower)
                childchild = ThisTvShow.CreateElement("poster")
                childchild.InnerText = TransposePath(tvsh.ImagePoster.Path)
                child.AppendChild(childchild)
            End If
            childchild = ThisTvShow.CreateElement("season")
            tempppp = "-1"
            Attr = ThisTvShow.CreateAttribute("num")
            Attr.Value = tempppp
            childchild.Attributes.Append(Attr)
            If File.Exists(tvsh.ImageAllSeasons.Path.Replace("-poster", "-banner")) Then
                File.Copy(tvsh.ImageFanart.Path, titlepath & tvsh.ImageAllSeasons.FileName.Replace("-poster", "-banner").ToLower)
                childchildchild = ThisTvShow.CreateElement("banner")
                childchildchild.InnerText = TransposePath(tvsh.ImageAllSeasons.Path.Replace("-poster", "-banner"))
                childchild.AppendChild(childchildchild)
            End If
            If File.Exists(tvsh.ImageAllSeasons.Path.Replace("-poster", "-fanart")) Then
                File.Copy(tvsh.ImageFanart.Path, titlepath & tvsh.ImageAllSeasons.FileName.Replace("-poster", "-fanart").ToLower)
                childchildchild = ThisTvShow.CreateElement("fanart")
                childchildchild.InnerText = TransposePath(tvsh.ImageAllSeasons.Path.Replace("-poster", "-fanart"))
                childchild.AppendChild(childchildchild)
            End If
            If File.Exists(tvsh.ImageAllSeasons.Path) Then
                File.Copy(tvsh.ImageFanart.Path, titlepath & tvsh.ImageAllSeasons.Filename.ToLower)
                childchildchild = ThisTvShow.CreateElement("poster")
                childchildchild.InnerText = TransposePath(tvsh.ImageAllSeasons.Path)
                childchild.AppendChild(childchildchild)
            End If
            child.AppendChild(childchild)
            childchild = ThisTvShow.CreateElement("season")
            tempppp = "0"
            Attr = ThisTvShow.CreateAttribute("num")
            Attr.Value = tempppp
            childchild.Attributes.Append(Attr)
            If File.Exists(tvsh.FolderPath & "season-specials-banner.jpg") Then
                File.Copy(tvsh.FolderPath & "season-specials-banner.jpg", titlepath & "season-specials-banner.jpg")
                childchildchild = ThisTvShow.CreateElement("banner")
                childchildchild.InnerText = TransposePath(tvsh.FolderPath) & "season-specials-banner.jpg"
                childchild.AppendChild(childchildchild)
            End If
            If File.Exists(tvsh.FolderPath & "season-specials-poster.jpg") Then
                File.Copy(tvsh.FolderPath & "season-specials-poster.jpg", titlepath & "season-specials-poster.jpg")
                childchildchild = ThisTvShow.CreateElement("poster")
                childchildchild.InnerText = TransposePath(tvsh.FolderPath) & "season-specials-poster.jpg"
                childchild.AppendChild(childchildchild)
            End If
            child.AppendChild(childchild)
            For i = 1 To 200
                Dim s As String = i.ToString
                If 1 < 10 Then s = "0" & s
                If File.Exists(tvsh.FolderPath & "season" & s & "-banner.jpg") OrElse File.Exists(tvsh.FolderPath & "season" & s & "-poster.jpg") Then
                    childchild = ThisTvShow.CreateElement("season")
                    'tempppp = "0"
                    Attr = ThisTvShow.CreateAttribute("num")
                    Attr.Value = i.tostring
                    childchild.Attributes.Append(Attr)
                    Dim SeasonArt As String = "season" & s & "-banner.jpg"
                    If File.Exists(tvsh.FolderPath & SeasonArt) Then
                        File.Copy(tvsh.FolderPath & SeasonArt, titlepath & SeasonArt)
                        childchildchild = ThisTvShow.CreateElement("banner")
                        childchildchild.InnerText = TransposePath(tvsh.FolderPath) & SeasonArt
                        childchild.AppendChild(childchildchild)
                    End If
                    SeasonArt = "season" & s & "-poster.jpg"
                    If File.Exists(tvsh.FolderPath & SeasonArt) Then
                        File.Copy(tvsh.FolderPath & SeasonArt, titlepath & SeasonArt)
                        childchildchild = ThisTvShow.CreateElement("poster")
                        childchildchild.InnerText = TransposePath(tvsh.FolderPath) & SeasonArt
                        childchild.AppendChild(childchildchild)
                    End If
                    child.AppendChild(childchild)
                End If
            Next
            'child.AppendChild(childchild)
            root.AppendChild(child)
            ThisTvShow.AppendChild(root)
        Catch
        End Try
        Return ThisTvShow 
    End Function

    Private Function TransposeTvEp(ByVal tvep As TvEpisode, ByVal sh As tvshow) As XmlDocument
        Dim ThisTvEp As New XmlDocument
        Try
            Dim stage As Integer = 0
            Dim root As XmlElement = Nothing
            Dim child As XmlElement = Nothing
            Dim childchild As XmlElement = Nothing
            Dim actorchild As XmlElement = Nothing
            Dim filedetailschild As XmlElement = Nothing
            Dim filedetailschildchild As XmlElement = Nothing
            Dim anotherchild As XmlElement = Nothing
            root = ThisTvEp.CreateElement("episodedetails")
            child = ThisTvEp.CreateElement("title") : child.InnerText = tvep.Title.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("showtitle") : child.InnerText = sh.Title.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("rating") : child.InnerText = tvep.Rating.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("epbookmark") : child.InnerText = "0.000000" : root.AppendChild(child)
            child = ThisTvEp.CreateElement("year") : child.InnerText = "0" : root.AppendChild(child)
            child = ThisTvEp.CreateElement("top250") : child.InnerText = If(String.IsNullOrEmpty(sh.Top250.Value), "0", sh.Top250.Value) : root.AppendChild(child)
            child = ThisTvEp.CreateElement("season") : child.InnerText = tvep.Season.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("episode") : child.InnerText = tvep.Episode.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("uniqueid") : child.InnerText = tvep.UniqueId.value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("displayseason")
            child.InnerText = If(tvep.DisplaySeason.Value = "", "-1", tvep.DisplaySeason.Value) : root.AppendChild(child)
            child = ThisTvEp.CreateElement("displayepisode")
            child.InnerText = If(tvep.DisplayEpisode.Value = "", "-1", tvep.DisplayEpisode.Value) : root.AppendChild(child)
            child = ThisTvEp.CreateElement("votes") : child.InnerText = tvep.Votes.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("plot") : child.InnerText = tvep.Plot.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("tagline") : child.InnerText = tvep.TagLine.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("runtime")
            Dim runtime As Integer = tvep.Details.StreamDetails.Video.DurationInSeconds.Value.ToInt
            child.InnerText = If(runtime > 0, Math.Floor(runtime/60).ToString, "0")
            root.AppendChild(child)
            child = ThisTvEp.CreateElement("mpaa") : child.InnerText = sh.Mpaa.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("playcount") : child.InnerText = tvep.Playcount.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("lastplayed") : child.InnerText = tvep.LastPlayed.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("file") : child.InnerText = "" : root.AppendChild(child)
            child = ThisTvEp.CreateElement("path") : child.InnerText = tvep.FolderPath : root.AppendChild(child)
            child = ThisTvEp.CreateElement("filenameandpath") : child.InnerText = tvep.VideoFilePath : root.AppendChild(child)
            child = ThisTvEp.CreateElement("basepath") : child.InnerText = tvep.VideoFilePath : root.AppendChild(child)
            child = ThisTvEp.CreateElement("id") : child.InnerText = tvep.TvdbId.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("premiered") : child.InnerText = sh.Premiered.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("aired") : child.InnerText = tvep.Aired.Value : root.AppendChild(child)
            child = ThisTvEp.CreateElement("studio") : child.InnerText = sh.Studio.Value : root.AppendChild(child)

            child = ThisTvEp.CreateElement("fileinfo")
            anotherchild = ThisTvEp.CreateElement("streamdetails")
            filedetailschild = ThisTvEp.CreateElement("video")

            filedetailschildchild = ThisTvEp.CreateElement("codec")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(tvep.Details.StreamDetails.Video.Codec.Value), "", tvep.Details.StreamDetails.Video.Codec.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = ThisTvEp.CreateElement("aspect")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(tvep.Details.StreamDetails.Video.Aspect.Value), "", tvep.Details.StreamDetails.Video.Aspect.Value)
            filedetailschild.AppendChild(filedetailschildchild)
            
            filedetailschildchild = ThisTvEp.CreateElement("width")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(tvep.Details.StreamDetails.Video.Width.Value), "", tvep.Details.StreamDetails.Video.Width.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = ThisTvEp.CreateElement("height")
            filedetailschildchild.InnerText = If(String.IsNullOrEmpty(tvep.Details.StreamDetails.Video.Height.Value), "", tvep.Details.StreamDetails.Video.Height.Value)
            filedetailschild.AppendChild(filedetailschildchild)

            filedetailschildchild = ThisTvEp.CreateElement("durationinseconds")
            filedetailschildchild.InnerText = If(tvep.Details.StreamDetails.Video.DurationInSeconds.Value = "-1", "0", tvep.Details.StreamDetails.Video.DurationInSeconds.Value)
            filedetailschild.AppendChild(filedetailschildchild)
            anotherchild.AppendChild(filedetailschild)

            For Each item In tvep.Details.StreamDetails.Audio
                filedetailschild = ThisTvEp.CreateElement("audio")
                filedetailschildchild = ThisTvEp.CreateElement("codec")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Codec.Value), "", item.Codec.Value)
                filedetailschild.AppendChild(filedetailschildchild)

                filedetailschildchild = ThisTvEp.CreateElement("language")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Language.Value), "", item.Language.Value)
                filedetailschild.AppendChild(filedetailschildchild)

                filedetailschildchild = ThisTvEp.CreateElement("channels")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(item.Channels.Value), "", item.Channels.Value)
                filedetailschild.AppendChild(filedetailschildchild)
                        
                anotherchild.AppendChild(filedetailschild)
            Next

            filedetailschild = ThisTvEp.CreateElement("subtitle")
            For Each entry In tvep.Details.StreamDetails.Subtitles
                filedetailschildchild = ThisTvEp.CreateElement("language")
                filedetailschildchild.InnerText = If(String.IsNullOrEmpty(entry.Language.Value), "", entry.Language.Value)
                filedetailschild.AppendChild(filedetailschildchild)
            Next
            anotherchild.AppendChild(filedetailschild)
            child.AppendChild(anotherchild)
            root.AppendChild(child)

            For each act As Actor In sh.ListActors
                child = ThisTvEp.CreateElement("actor")
                actorchild = ThisTvEp.CreateElement("actorid")
                actorchild.InnerText = act.actorid 
                child.AppendChild(actorchild)
                actorchild = ThisTvEp.CreateElement("name")
                actorchild.InnerText = act.actorname
                child.AppendChild(actorchild)
                actorchild = ThisTvEp.CreateElement("role")
                actorchild.InnerText = act.actorrole
                child.AppendChild(actorchild)
                actorchild = ThisTvEp.CreateElement("order")
                actorchild.InnerText = act.order
                child.AppendChild(actorchild)
                root.AppendChild(child)
                If Not String.IsNullOrEmpty(act.actorthumb) Then
                    actorchild = ThisTvEp.CreateElement("thumb")
                    actorchild.InnerText = act.actorthumb
                    child.AppendChild(actorchild)
                End If
            Next
            root.AppendChild(child)

            child = ThisTvEp.CreateElement("art")
            childchild = ThisTvEp.CreateElement("thumb")
            Dim titlepath As String = opTvshows & sh.Title.Value.Replace(" ", "_") & "\"
            'If Not Directory.Exists(titlepath) Then Directory.CreateDirectory(titlepath)
            If File.Exists(tvep.Thumbnail.Path) Then
                Dim epimg As String = "s" & If(tvep.Season.Value.Length = 1, "0", "") & tvep.Season.Value & "e" & If(tvep.Episode.Value.Length = 1, "0", "") & tvep.Episode.Value & "-thumb.jpg"
                File.Copy(tvep.Thumbnail.Path, titlepath & epimg)
                childchild.InnerText = TransposePath(tvep.Thumbnail.Path)
            End If
            child.AppendChild(childchild)
            root.AppendChild(child)
            ThisTvEp.AppendChild(root)
        Catch
        End Try
        Return ThisTvEP 
    End Function

    Private Function SetPaths() As XmlDocument
        Dim SetXBPaths As New XmlDocument
        Try
            'Dim stage As Integer = 0
            Dim root As XmlElement = Nothing
            Dim child As XmlElement = Nothing
            Dim actorchild As XmlElement = Nothing
            Dim filedetailschild As XmlElement = Nothing
            Dim filedetailschildchild As XmlElement = Nothing
            Dim anotherchild As XmlElement = Nothing
            root = SetXBPaths.CreateElement("paths")
            For Each xbpath In Pathlist 
                child = SetXBPaths.CreateElement("path")
                anotherchild = SetXBPaths.CreateElement("url")
                anotherchild.InnerText = xbpath.rootpath & "\"
                child.AppendChild(anotherchild)
                anotherchild = SetXBPaths.CreateElement("scanrecursive")
                Dim scanrec As String = "2147483647"
                If xbpath.pathsource = "tv" Then scanrec = "0"
                anotherchild.InnerText = scanrec
                child.AppendChild(anotherchild)
                anotherchild = SetXBPaths.CreateElement("usefoldernames")
                anotherchild.InnerText = "false"
                child.AppendChild(anotherchild)
                anotherchild = SetXBPaths.CreateElement("content")
                anotherchild.InnerText = xbpath.pathsource
                child.AppendChild(anotherchild)
                anotherchild = SetXBPaths.CreateElement("scraperpath")
                anotherchild.InnerText = "metadata.local"
                child.AppendChild(anotherchild)
                root.AppendChild(child)
            Next
            SetXBPaths.AppendChild(root)
        Catch ex As Exception
            
        End Try
        Return SetXBPaths
    End Function

    Private Function Formatdate(ByVal thisdate As String) As String
        Try
            Return DateTime.ParseExact(thisdate, Preferences.datePattern, Nothing).ToString(Preferences.DateFormat2)
        Catch
            Return "2001-01-01 01:01:00"
        End Try
    End Function

    Private Function GetBasePath(ByVal thismov As FullMovieDetails) As String
        Dim ThisBasePath As String = thismov.fileinfo.fullpathandfilename
        If IO.Path.GetFileName(ThisBasePath).ToLower = "video_ts.nfo" Or IO.Path.GetFileName(ThisBasePath).ToLower = "index.nfo" Then
            Return Utilities.RootVideoTsFolder(ThisBasePath)
        Else
            Return thismov.fileinfo.filenameandpath 
        End If
    End Function

    Private Function TransposePath(ByVal checkpath As String) As String
        For Each item In ExportPathList
            If checkpath.Contains(item.Key) Then
                checkpath = checkpath.Replace(item.Key, item.Value)
                If item.Value.Contains("/") Then
                    checkpath = checkpath.Replace("\", "/")
                ElseIf item.Value.Contains("\") Then
                    checkpath = checkpath.Replace("/", "\")
                End If
            End If
        Next
        Return checkpath 
    End Function
    
    Private Sub frmMediaInfoEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Me.Close()
    End Sub

    Private Sub btn_Start_Click( sender As Object,  e As EventArgs) Handles btn_Start.Click
        btn_Cancel.Text = "Cancel"
        RunExport()
    End Sub

    Private Sub btn_Cancel_Click( sender As Object,  e As EventArgs) Handles btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub btn_FolderBrowse_Click( sender As Object,  e As EventArgs) Handles btn_FolderBrowse.Click
        Dim dialog As New FolderBrowserDialog()
        dialog.RootFolder = Environment.SpecialFolder.Desktop
        dialog.ShowNewFolderButton = True
        dialog.SelectedPath = "C:\"
        dialog.Description = "Select Path to save Exported data"
        If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
            OutputFolder = dialog.SelectedPath
        Else
            Exit Sub
        End If
        'If IsNothing(OutputFolder) Then
        '    'MsgBox("No folder selected, export aborted")
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