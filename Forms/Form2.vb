Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Linq

Public Class Form2
    Const SetDefaults = True
    Dim editsmade As Boolean = False
    Dim thumbeditsmade As Boolean = False
    'Dim actorlist As Integer = Nothing
    'Dim actors(1000, 3)
    'Dim actorcount As Integer

    Dim oldactors(9999, 2)
    Dim actorcount As Integer = 0
    Dim workingmovieedit As New FullMovieDetails
    Dim posterpath As String = ""
    Dim cropstring As String
    Dim datechanged As Boolean = False




    Private Sub Form2_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            Call checkforedits()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub setupdisplay()
        actorcb.Items.Clear()
        If workingmovieedit.fullmoviebody.title <> Nothing Then titletxt.Text = workingmovieedit.fullmoviebody.title
        If workingmovieedit.fullmoviebody.director <> Nothing Then directortxt.Text = workingmovieedit.fullmoviebody.director
        If workingmovieedit.fullmoviebody.stars <> Nothing Then starstxt.Text = workingmovieedit.fullmoviebody.stars
        If workingmovieedit.fullmoviebody.runtime <> Nothing Then runtimetxt.Text = workingmovieedit.fullmoviebody.runtime
        If workingmovieedit.fullmoviebody.credits <> Nothing Then creditstxt.Text = workingmovieedit.fullmoviebody.credits
        If workingmovieedit.fullmoviebody.mpaa <> Nothing Then mpaatxt.Text = workingmovieedit.fullmoviebody.mpaa
        If workingmovieedit.fullmoviebody.studio <> Nothing Then studiotxt.Text = workingmovieedit.fullmoviebody.studio
        If workingmovieedit.fullmoviebody.genre <> Nothing Then genretxt.Text = workingmovieedit.fullmoviebody.genre
        If workingmovieedit.fullmoviebody.year <> Nothing Then yeartxt.Text = workingmovieedit.fullmoviebody.year
        If workingmovieedit.fullmoviebody.rating <> Nothing Then ratingtxt.Text = workingmovieedit.fullmoviebody.rating
        If workingmovieedit.fullmoviebody.imdbid <> Nothing Then idtxt.Text = workingmovieedit.fullmoviebody.imdbid
        If workingmovieedit.fullmoviebody.votes <> Nothing Then votestxt.Text = workingmovieedit.fullmoviebody.votes
        If workingmovieedit.fullmoviebody.outline <> Nothing Then outlinetxt.Text = workingmovieedit.fullmoviebody.outline
        If workingmovieedit.fullmoviebody.plot <> Nothing Then plottxt.Text = workingmovieedit.fullmoviebody.plot
        If workingmovieedit.fullmoviebody.tagline <> Nothing Then taglinetxt.Text = workingmovieedit.fullmoviebody.tagline
        If workingmovieedit.fullmoviebody.top250 <> Nothing Then top250txt.Text = workingmovieedit.fullmoviebody.top250 
        If workingmovieedit.fullmoviebody.trailer <> Nothing Then tb_TrailerURL.Text = workingmovieedit.fullmoviebody.trailer
        Try
            If workingmovieedit.fileinfo.createdate <> Nothing Then Createdatepicker.Value = workingmovieedit.fileinfo.createdate
        Catch
        End Try
        If workingmovieedit.fileinfo.fullpathandfilename <> Nothing Then filenametxt.Text = workingmovieedit.fileinfo.fullpathandfilename

        For Each actor In workingmovieedit.listactors
            actorcb.Items.Add(actor.actorname)
        Next

        Try
            actorcb.SelectedItem = workingmovieedit.listactors(0).actorname
        Catch ex As Exception
            'MsgBox(ex.ToString)
        End Try

        If actorcb.Text <> "" Then
            roletxt.Text = workingmovieedit.listactors(0).actorrole
        End If
        posterpath = Form1.workingMovieDetails.fileinfo.posterpath
        If posterpath <> Nothing Then
            If posterpath <> "" Then
                If posterpath.ToLower.IndexOf(".jpg") <> -1 Or posterpath.ToLower.IndexOf(".tbn") <> -1 Then
                    moviethumb.ImageLocation = posterpath
                Else
                    moviethumb.Image = Nothing
                End If
            Else
                moviethumb.Image = Nothing
            End If
        Else
            moviethumb.Image = Nothing
        End If
        editsmade = False
    End Sub
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            workingmovieedit = Form1.workingMovieDetails
            For Each actor In workingmovieedit.listactors
                If actor.actorname <> Nothing Then
                    actorcount += 1
                    oldactors(actorcount, 0) = actor.actorname
                    If actor.actorname <> Nothing Then
                        oldactors(actorcount, 1) = actor.actorrole
                    End If
                    If actor.actorthumb <> Nothing Then
                        oldactors(actorcount, 2) = actor.actorthumb
                    End If
                End If
            Next
            Createdatepicker.CustomFormat = "yyyyMMddhhmmss"   'Preferences.DateFormat
            Createdatepicker.Format = DateTimePickerFormat.Custom 
             
            Panel2.Dock = DockStyle.Fill
            Call setupdisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub checkforedits()
        If editsmade Then
            editsmade = False
            Dim t As Integer = MsgBox("Edits were made" & vbCrLf & "Do you wish to Save changes before this form closes", MsgBoxStyle.YesNo)
            If t = DialogResult.Yes Then
                btnsavechanges.PerformClick()

            End If
        End If


    End Sub


    Private Sub btnexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexit.Click
        Try
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub actorcb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles actorcb.SelectedIndexChanged
        Try
            roletxt.Text = workingmovieedit.listactors(actorcb.SelectedIndex).actorrole
            If workingmovieedit.listactors(actorcb.SelectedIndex).actorthumb <> Nothing Then
                Try
                    PictureBox1.ImageLocation = Preferences.GetActorThumbPath(workingmovieedit.listactors(actorcb.SelectedIndex).actorthumb)
                Catch ex As Exception
                    PictureBox1.Image = Nothing
                End Try
            Else
                PictureBox1.Image = Nothing
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btneditactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btneditactor.Click
        Try

        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Edit Actor Button
    Private Sub btndeleteactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndeleteactor.Click
        Try
            If actorcb.Items.Count <> 0 Then
                Dim tempint As Integer
                tempint = actorcb.SelectedIndex
                workingmovieedit.listactors.RemoveAt(actorcb.SelectedIndex)
                actorcb.Items.RemoveAt(actorcb.SelectedIndex)
                If workingmovieedit.listactors.Count <= tempint Then
                    tempint = workingmovieedit.listactors.Count - 1
                End If
                If workingmovieedit.listactors.Count <> 0 Then
                    actorcb.SelectedIndex = tempint
                Else
                    actorcb.Text = ""
                    roletxt.Text = ""
                End If
            Else
                MsgBox("No Actors to delete")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Delete Actor Button
    Private Sub btnaddactor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddactor.Click
        Try
            Dim newactor As New Actor
            For Each currentactor In actorcb.items
                If TextBox4.Text = currentactor Then
                    MsgBox("Actor exists in this movie")
                    Exit sub
                End If
            Next
            newactor.ActorId.value = ""
            newactor.actorname = TextBox3.Text
            newactor.actorrole = TextBox4.Text
            newactor.actorthumb = ""
            workingmovieedit.listactors.Add(newactor)
            actorcb.Items.Add(newactor)
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Add Actor Button



    Private Sub btnchangemovie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnchangemovie.Click
        Try
            MsgBox("Use 'Change Movie' on main Media Companion",vbExclamation)
            'Dim tempstring As String
            'Dim url As String
            'If Preferences.usefoldernames = True Then
            '    tempstring = Form1.workingMovie.foldername
            'Else
            '    tempstring = Utilities.CleanFileName(Utilities.RemoveFilenameExtension(IO.Path.GetFileName(Form1.workingMovieDetails.fileinfo.fullpathandfilename)))
            'End If

            'tempstring = tempstring.Replace(" ", "+")
            'tempstring = tempstring.Replace("&", "%26")


            'url = Preferences.imdbmirror & "find?s=tt&q=" & tempstring
            'WebBrowser2.Stop()
            'WebBrowser2.ScriptErrorsSuppressed = True
            'WebBrowser2.Navigate(url)
            'WebBrowser2.Refresh()
            'Panel2.Visible = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnAltPosterBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAltPosterBrowser.Click
        Try
            Dim t As New frmCoverArt()
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    '_______________________________________________________
    '_________________Crop Thumbnail Code___________________
    '_______________________________________________________

    Private Function CropImage(ByVal SrcBmp As Bitmap, ByVal NewSize As Size, ByVal StartPoint As Point) As Bitmap
        Dim SrcRect As New Rectangle(StartPoint.X, StartPoint.Y, NewSize.Width, NewSize.Height)
        Dim DestRect As New Rectangle(0, 0, NewSize.Width, NewSize.Height)
        Dim DestBmp As New Bitmap(NewSize.Width, NewSize.Height, Imaging.PixelFormat.Format32bppArgb)
        Dim g As Graphics = Graphics.FromImage(DestBmp)
        g.DrawImage(SrcBmp, DestRect, SrcRect, GraphicsUnit.Pixel)
        Return DestBmp
    End Function ' Crop Image Function

    ' Below sets part of image to crop, top, bottom, left, right
    Private Sub croptop()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 1)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropbottom()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth, imageheight - 1), New Point(0, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropleft()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(1, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub
    Private Sub cropright()
        If moviethumb.Image Is Nothing Then Exit Sub
        Dim imagewidth As Integer
        Dim imageheight As Integer
        thumbeditsmade = True
        imagewidth = moviethumb.Image.Width
        imageheight = moviethumb.Image.Height
        PictureBox2.Image = moviethumb.Image
        moviethumb.Image = CropImage(PictureBox2.Image, New Size(imagewidth - 1, imageheight), New Point(0, 0)).Clone
        moviethumb.SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    'crop while button is down, and stop when button is up
    Private Sub btncroptop_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call croptop()
            cropstring = "top"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncroptop_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncroptop.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropbottom_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropbottom()
            cropstring = "bottom"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropbottom_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropbottom.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropleft_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            thumbeditsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropleft()
            cropstring = "left"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropleft_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropleft.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropright_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseDown
        Try
            If moviethumb.Image Is Nothing Then Exit Sub
            editsmade = True
            btnresetimage.Enabled = True
            btnSaveCropped.Enabled = True
            Call cropright()
            cropstring = "right"
            Timer1.Enabled = True
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
    Private Sub btncropright_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btncropright.MouseUp
        Try
            Timer1.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If cropstring = "top" Then Call croptop()
            If cropstring = "bottom" Then Call cropbottom()
            If cropstring = "left" Then Call cropleft()
            If cropstring = "right" Then Call cropright()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub ' Set auto repeat for crop



    Private Sub btnsavechanges_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavechanges.Click
        Try
            'workingmovieedit.listactors.Clear()
            'Form1.workingmoviedetails.listactors.Clear()

            'For f = 1 To actorcount
            '    If oldactors(f, 0) <> Nothing Then
            '        Dim newactor As New movieactors
            '        newactor.actorname = oldactors(f, 0)
            '        If oldactors(f, 1) <> Nothing Then
            '            newactor.actorrole = oldactors(f, 1)
            '        End If
            '        If oldactors(f, 2) <> Nothing Then
            '            newactor.actorthumb = oldactors(f, 2)
            '        End If
            '        workingmovieedit.listactors.Add(newactor)
            '    End If
            'Next

            workingmovieedit.fullmoviebody.plot = plottxt.Text
            workingmovieedit.fullmoviebody.title = titletxt.Text
            workingmovieedit.fullmoviebody.director = directortxt.Text
            workingmovieedit.fullmoviebody.stars = starstxt.Text
            workingmovieedit.fullmoviebody.runtime = runtimetxt.Text
            workingmovieedit.fullmoviebody.credits = creditstxt.Text
            workingmovieedit.fullmoviebody.mpaa = mpaatxt.Text
            workingmovieedit.fullmoviebody.studio = studiotxt.Text
            workingmovieedit.fullmoviebody.genre = genretxt.Text
            workingmovieedit.fullmoviebody.year = yeartxt.Text
            workingmovieedit.fullmoviebody.rating = ratingtxt.Text
            workingmovieedit.fullmoviebody.votes = votestxt.Text
            workingmovieedit.fullmoviebody.outline = outlinetxt.Text
            workingmovieedit.fullmoviebody.tagline = taglinetxt.Text
            Form1.workingMovieDetails.fullmoviebody = workingmovieedit.fullmoviebody
            Form1.workingMovieDetails.listactors = workingmovieedit.listactors
            Form1.workingMovieDetails.listthumbs = workingmovieedit.listthumbs
            Dim credate As date = Createdatepicker.Value
            If datechanged Then
                workingmovieedit.fileinfo.createdate = Format(credate, Preferences.datePattern).ToString
            End If
            'Call WorkingWithNfoFiles.mov_NfoSave(Form1.workingMovieDetails.fileinfo.fullpathandfilename, Form1.workingMovieDetails)
            Movie.SaveNFO(Form1.workingMovieDetails.fileinfo.fullpathandfilename, Form1.workingMovieDetails)
            editsmade = False
            Me.Close()
            'Dim oldactors(9999, 2)
            'Dim actorcount As Integer = 0
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub saved()

    End Sub

    Private Sub moviethumb_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles moviethumb.LoadCompleted
        Try
            ' Label13.Text = moviethumb.Image.Width & " X " & moviethumb.Image.Height
            Dim exists As Boolean = False
            Label16.Text = moviethumb.Image.Width
            Label17.Text = moviethumb.Image.Height
            exists = System.IO.File.Exists(Form1.workingMovieDetails.fileinfo.posterpath)
            If exists = True Then
                Dim lngSizeOfFile As Decimal
                lngSizeOfFile = FileLen(Form1.workingMovieDetails.fileinfo.posterpath)
                lngSizeOfFile = lngSizeOfFile / 1024
                lngSizeOfFile = Decimal.Round(lngSizeOfFile, 2)
                Label18.Text = lngSizeOfFile & "kB"
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub zoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles zoom.Click
        Try
            If moviethumb.Height = 213 Then
                moviethumb.Location = New Point(371, 12)
                moviethumb.Height = 518
                moviethumb.Width = 362
            ElseIf moviethumb.Height = 518 Then
                moviethumb.Height = 213
                moviethumb.Width = 163
                moviethumb.Location = New Point(570, 12)
            End If
            Application.DoEvents()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnfanart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfanart.Click
        Try
            Dim t As New frmMovieFanart
            t.ShowDialog()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub btnresetimage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnresetimage.Click
        Try
            thumbeditsmade = False
            moviethumb.Image = Form1.moviethumb.Image
            btnresetimage.Enabled = False
            btnSaveCropped.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub btnsavecropped_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsavecropped.Click
        Try
            thumbeditsmade = False
            Dim tempstring As String
            tempstring = Form1.workingMovieDetails.fileinfo.posterpath
            Dim stream As New System.IO.MemoryStream
            moviethumb.Image.Save(tempstring, System.Drawing.Imaging.ImageFormat.Jpeg)
            Form1.moviethumb.Image = moviethumb.Image
            btnresetimage.Enabled = False
            btnSaveCropped.Enabled = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            titletxt.Text = ""
            directortxt.Text = ""
            creditstxt.Text = ""
            studiotxt.Text = ""
            yeartxt.Text = ""
            outlinetxt.Text = ""
            plottxt.Text = ""
            taglinetxt.Text = ""
            runtimetxt.Text = ""
            mpaatxt.Text = ""
            genretxt.Text = ""
            ratingtxt.Text = ""
            votestxt.Text = ""
            idtxt.Text = ""
            workingmovieedit.listactors.Clear()
            workingmovieedit.listthumbs.Clear()
            workingmovieedit.filedetails.filedetails_audio.Clear()
            workingmovieedit.filedetails.filedetails_subtitles.Clear()
            workingmovieedit.filedetails.filedetails_video.Bitrate = Nothing
            workingmovieedit.filedetails.filedetails_video.BitrateMax = Nothing
            workingmovieedit.filedetails.filedetails_video.BitrateMode = Nothing
            workingmovieedit.filedetails.filedetails_video.Codec = Nothing
            workingmovieedit.filedetails.filedetails_video.CodecId = Nothing
            workingmovieedit.filedetails.filedetails_video.CodecInfo = Nothing
            workingmovieedit.filedetails.filedetails_video.Container = Nothing
            workingmovieedit.filedetails.filedetails_video.DurationInSeconds.Value = Nothing
            workingmovieedit.filedetails.filedetails_video.FormatInfo = Nothing
            workingmovieedit.filedetails.filedetails_video.Height = Nothing
            workingmovieedit.filedetails.filedetails_video.ScanType = Nothing
            workingmovieedit.filedetails.filedetails_video.Width = Nothing
            workingmovieedit.fullmoviebody.title = Nothing
            workingmovieedit.fullmoviebody.director = Nothing
            workingmovieedit.fullmoviebody.stars = Nothing
            workingmovieedit.fullmoviebody.credits = Nothing
            workingmovieedit.fullmoviebody.studio = Nothing
            workingmovieedit.fullmoviebody.outline = Nothing
            workingmovieedit.fullmoviebody.plot = Nothing
            workingmovieedit.fullmoviebody.tagline = Nothing
            workingmovieedit.fullmoviebody.runtime = Nothing
            workingmovieedit.fullmoviebody.mpaa = Nothing
            workingmovieedit.fullmoviebody.genre = Nothing
            workingmovieedit.fullmoviebody.year = Nothing
            workingmovieedit.fullmoviebody.year = Nothing
            workingmovieedit.fullmoviebody.rating = Nothing
            workingmovieedit.fullmoviebody.votes = Nothing
            workingmovieedit.fullmoviebody.imdbid = Nothing
            actorcb.Text = ""
            roletxt.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) 
        Try
            workingmovieedit = Nothing
            actorcb.Text = ""
            actorcb.Items.Clear()
            roletxt.Text = ""
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            titletxt.Text = ""
            directortxt.Text = ""
            creditstxt.Text = ""
            studiotxt.Text = ""
            yeartxt.Text = ""
            outlinetxt.Text = ""
            plottxt.Text = ""
            taglinetxt.Text = ""
            runtimetxt.Text = ""
            mpaatxt.Text = ""
            genretxt.Text = ""
            ratingtxt.Text = ""
            votestxt.Text = ""
            idtxt.Text = ""
            workingmovieedit = Form1.workingMovieDetails
            Dim newworkingmovieedit As FullMovieDetails = Form1.workingMovieDetails
            newworkingmovieedit.listactors.Clear()
            For f = 1 To actorcount
                Dim actor As New str_MovieActors(SetDefaults)
                If oldactors(f, 0) <> Nothing Then
                    actor.actorname = oldactors(f, 0)
                    If oldactors(f, 1) <> Nothing Then
                        actor.actorrole = oldactors(f, 1)
                    End If
                    If oldactors(f, 2) <> Nothing Then
                        actor.actorthumb = oldactors(f, 2)
                    End If
                    newworkingmovieedit.listactors.Add(actor)
                End If
            Next
            Call setupdisplay()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub

    Private Sub Createdatepicker_ValueChanged( sender As System.Object,  e As System.EventArgs) Handles Createdatepicker.ValueChanged
            datechanged = True
    End Sub

    Private Sub Createdatepicker_DropDown(ByVal sender As Object, ByVal e As EventArgs) Handles Createdatepicker.DropDown
      RemoveHandler Createdatepicker.ValueChanged, AddressOf Createdatepicker_ValueChanged
    End Sub

    Private Sub Createdatepicker_CloseUp(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Createdatepicker.CloseUp
      AddHandler Createdatepicker.ValueChanged, AddressOf Createdatepicker_ValueChanged
      Call Createdatepicker_ValueChanged(sender, EventArgs.Empty)
    End Sub

    Private Sub mpaatxt_TextChanged(sender As Object, e As System.EventArgs) Handles mpaatxt.TextChanged, titletxt.TextChanged, directortxt.TextChanged
        editsmade = True
    End Sub
End Class