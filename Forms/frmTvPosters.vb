
Imports System.Net
Imports System.IO



Public Class frmTvPosters


    Dim totalpages As Integer = 0

    Dim bigpanel As Panel
    Dim WithEvents picboxes As PictureBox
    Dim WithEvents checkboxes As RadioButton
    Dim WithEvents labels As Label
    Dim WithEvents reslabel As Label
    Dim resolutionlbl As Label
    Dim panel2 As Panel
    Dim posterurls(1000, 1) As String
    'Dim posterpath As String = Form1.fullposterpath
    Dim WithEvents mainposter As PictureBox
    Dim WithEvents bigpicbox As PictureBox
    Dim count As Integer = 0
    Dim title As String = Form1.titletxt.Text
    Dim itemnumber As Integer
    Dim rememberint As Integer
    Dim maxthumbs As Integer = Form1.userPrefs.maximumthumbs
    Dim pagecount As Integer = 0
    Dim currentpage As Integer = 1
    Dim downloadthumb(3000, 1) As String
    Dim workingthumb As Integer
    Dim downloadthumbcount As Integer
    Dim url As String
    Dim websource(3000) As String
    Dim urllinecount As Integer
    Dim series As Integer
    Dim savethumbpath As String = ""












    'Private Sub tvposters_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    '    'Public tvpath As String
    '    'Public imdbidd As String
    '    'Public tvdbidd As String

    '    'posterpath = Form1.tvpath & "folder.jpg"


    '    TextBox1.Text = maxthumbs.ToString
    '    Me.Refresh()
    '    Application.DoEvents()
    '    Dim exists As Boolean = System.IO.File.Exists(posterpath)
    '    If exists = True Then

    '        Dim tempstring As String
    '        mainposter = New PictureBox
    '        Try
    '            Dim OriginalImage As New Bitmap(posterpath)
    '            Dim Image2 As New Bitmap(OriginalImage)
    '            OriginalImage.Dispose()

    '            With mainposter
    '                .Location = New Point(0, 0)
    '                .Width = 250
    '                .Height = 240
    '                .SizeMode = PictureBoxSizeMode.Zoom
    '                .Image = Image2
    '                .Visible = True
    '                .BorderStyle = BorderStyle.Fixed3D
    '            End With
    '            Me.Panel1.Controls.Add(mainposter)
    '            tempstring = mainposter.Image.Width.ToString & " x " & mainposter.Image.Height.ToString
    '            Label6.Text = tempstring
    '        Catch
    '            mainposter = New PictureBox
    '            With mainposter
    '                .Location = New Point(0, 0)
    '                .Width = 250
    '                .Height = 240
    '                .SizeMode = PictureBoxSizeMode.Zoom
    '                .Visible = False
    '                .BorderStyle = BorderStyle.Fixed3D
    '            End With
    '            Me.Panel1.Controls.Add(mainposter)
    '            Dim mainlabel As Label
    '            mainlabel = New Label
    '            With mainlabel
    '                .Location = New Point(0, 100)
    '                .Width = 423
    '                .Height = 100
    '                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
    '                .Text = "No Local Poster Is Available For This Movie"
    '                .BringToFront()
    '            End With
    '            Me.Panel1.Controls.Add(mainlabel)
    '            Label6.Visible = False
    '        End Try
    '    Else
    '        mainposter = New PictureBox
    '        With mainposter
    '            .Location = New Point(0, 0)
    '            .Width = 250
    '            .Height = 240
    '            .SizeMode = PictureBoxSizeMode.Zoom
    '            .Visible = False
    '            .BorderStyle = BorderStyle.Fixed3D
    '        End With
    '        Me.Panel1.Controls.Add(mainposter)
    '        Dim mainlabel As Label
    '        mainlabel = New Label
    '        With mainlabel
    '            .Location = New Point(0, 100)
    '            .Width = 423
    '            .Height = 100
    '            .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
    '            .Text = "No Local Poster Is Available For This Movie"
    '            .BringToFront()
    '        End With
    '        Me.Panel1.Controls.Add(mainlabel)
    '        Label6.Visible = False
    '    End If

    '    panel2 = New Panel
    '    With panel2
    '        .Width = 772
    '        .Height = 237
    '        .Location = New Point(2, 2)
    '        .AutoScroll = True
    '    End With
    '    Me.Controls.Add(panel2)

    '    ComboBox2.Items.Add("Main TV Show Thumb")
    '    For f = 1 To Form1.availableseasoncount
    '        If Form1.availableseasons(f) <> "0" Then
    '            ComboBox2.Items.Add(Form1.availableseasons(f))
    '        Else
    '            ComboBox2.Items.Add("Specials")
    '        End If
    '    Next
    '    Try
    '        ComboBox2.SelectedIndex = 0
    '    Catch ex As Exception

    '    End Try



    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        'tmdb posters
        ReDim posterurls(1000, 1)
        count = 0
        pagecount = 0
        Call initialise()
        If ComboBox2.Text.IndexOf("Main") <> -1 Then
            Call downloadmain()
        End If

        If ComboBox2.Text.IndexOf("Season") <> -1 Or ComboBox2.Text.IndexOf("Spec") <> -1 Then
            Call downloadseason()
        End If


        count = downloadthumbcount
        Call displayselection()







    End Sub


    Private Sub downloadseason()
        ReDim downloadthumb(2000, 1)
        downloadthumbcount = 0
        workingthumb = 0
        Dim tempstring As String
        Dim tempstring2 As String
        downloadthumbcount = 0
        'url = "http://thetvdb.com/api/6E82FED600783400/series/" & Form1.tvdbidd & "/banners.xml"
        Call loadwebpage()
        Dim tempstring3 As String
        Dim line1 As String = "<BannerType>poster</BannerType>"
        Dim line2 As String = "<BannerType>series</BannerType>"
        tempstring3 = "<Season>" & series.ToString & "</Season>"
        For f = 1 To urllinecount
            If websource(f).IndexOf(tempstring3) <> -1 Then
                If websource(f - 4).IndexOf("<BannerPath>") <> -1 Then
                    tempstring2 = websource(f - 4)
                    tempstring2 = tempstring2.Replace("<BannerPath>", "")
                    tempstring2 = tempstring2.Replace("</BannerPath>", "")
                    tempstring2 = tempstring2.Replace("  ", "")
                    '_cache/posters/73739-4.jpg
                    tempstring = tempstring2
                    tempstring2 = "http://images.thetvdb.com/banners/" & tempstring2
                    tempstring = "http://images.thetvdb.com/banners/_cache/" & tempstring
                    downloadthumbcount += 1
                    downloadthumb(downloadthumbcount, 0) = tempstring
                    downloadthumb(downloadthumbcount, 1) = tempstring2
                End If
            End If
        Next
    End Sub












    Private Sub downloadmain()
        ReDim downloadthumb(2000, 1)
        downloadthumbcount = 0
        workingthumb = 0
        Dim tempstring As String
        Dim tempstring2 As String
        downloadthumbcount = 0
        'url = "http://thetvdb.com/api/6E82FED600783400/series/" & Form1.tvdbidd & "/banners.xml"
        Call loadwebpage()

        Dim line1 As String = "<BannerType>poster</BannerType>"
        Dim line2 As String = "<BannerType>series</BannerType>"

        For f = 1 To urllinecount
            If websource(f).IndexOf(line1) <> -1 Or websource(f).IndexOf(line2) <> -1 Then
                If websource(f - 1).IndexOf("<BannerPath>") <> -1 Then
                    tempstring2 = websource(f - 1)
                    tempstring2 = tempstring2.Replace("<BannerPath>", "")
                    tempstring2 = tempstring2.Replace("</BannerPath>", "")
                    tempstring2 = tempstring2.Replace("  ", "")
                    '_cache/posters/73739-4.jpg
                    tempstring = tempstring2
                    tempstring2 = "http://thetvdb.com/banners/" & tempstring2
                    tempstring = "http://thetvdb.com/banners/_cache/" & tempstring
                    downloadthumbcount += 1
                    downloadthumb(downloadthumbcount, 0) = tempstring
                    downloadthumb(downloadthumbcount, 1) = tempstring2
                End If
            End If
        Next
    End Sub


    Private Sub displayselection()

        If TextBox1.Text <> "" Then
            If IsNumeric(TextBox1.Text) Then
                Dim tempint As Integer
                tempint = Convert.ToInt16(TextBox1.Text)
                If tempint > 5 Then
                    maxthumbs = tempint
                End If
            End If
        End If

        Dim names As New List(Of String)()

        If count > 0 Then

            If count > maxthumbs Then
                Dim tempmaxthumbs As Integer = count

                Do Until tempmaxthumbs < 1
                    pagecount += 1
                    tempmaxthumbs -= maxthumbs
                Loop
            End If


            If count > maxthumbs Then
                For f = 1 To maxthumbs
                    names.Add(downloadthumb(f, 0))
                Next
            Else
                For f = 1 To count
                    names.Add(downloadthumb(f, 0))
                Next
            End If

            Label7.Visible = True
            If pagecount > 1 Then
                Button7.Visible = True
                Button8.Visible = True
                If count >= maxthumbs Then
                    Label7.Text = "Displaying 1 to " & maxthumbs.ToString & " of " & count.ToString & " Images"
                Else
                    Label7.Text = "Displaying 1 to " & count.ToString & " of " & count.ToString & " Images"
                End If
                currentpage = 1
                Button7.Enabled = False
                Button8.Enabled = True
            Else
                Button7.Visible = False
                Button8.Visible = False
                If count >= maxthumbs Then
                    Label7.Text = "Displaying 1 to " & maxthumbs.ToString & " of " & count.ToString & " Images"
                Else
                    Label7.Text = "Displaying 1 to " & count.ToString & " of " & count.ToString & " Images"
                End If
            End If

            Dim location As Integer = 0
            Dim itemcounter As Integer = 0
            For Each item As String In names


                picboxes() = New PictureBox()
                With picboxes
                    .Location = New Point(location, 0)
                    .Width = 280
                    .Height = 180
                    .SizeMode = PictureBoxSizeMode.Zoom
                    .ImageLocation = item
                    .Visible = True
                    .BorderStyle = BorderStyle.Fixed3D
                    .Name = "picture" & itemcounter.ToString
                    AddHandler picboxes.DoubleClick, AddressOf zoomimage
                    AddHandler picboxes.LoadCompleted, AddressOf imageres
                End With

                checkboxes() = New RadioButton()
                With checkboxes
                    .Location = New Point(location + 135, 195)
                    .Name = "checkbox" & itemcounter.ToString
                    .SendToBack()
                    .Text = " "
                    AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
                End With

                itemcounter += 1
                location += 280

                Me.panel2.Controls.Add(picboxes())
                Me.panel2.Controls.Add(checkboxes())
                Me.Refresh()
                Application.DoEvents()
            Next
        Else
            Dim mainlabel2 As Label
            mainlabel2 = New Label
            With mainlabel2
                .Location = New Point(0, 100)
                .Width = 700
                .Height = 100
                .Font = New System.Drawing.Font("Arial", 15, FontStyle.Bold)
                .Text = "No Posters Were Found For This Movie"
            End With
            Me.panel2.Controls.Add(mainlabel2)
        End If
    End Sub

    Private Sub imageres(ByVal sender As Object, ByVal e As EventArgs)


        reslabel = New Label
        Dim tempstring As String

        tempstring = sender.image.width.ToString
        tempstring = tempstring & " x "
        tempstring = tempstring & sender.image.height.ToString
        Dim locx As Integer = sender.location.x
        Dim locy As Integer = sender.location.y
        locy = locy + sender.height

        With reslabel
            .Location = New Point(locx + 100, locy)
            .Text = tempstring
            .BringToFront()
        End With

        Me.panel2.Controls.Add(reslabel)


        Me.Refresh()
        Application.DoEvents()
    End Sub

    Private Sub radiochanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim tempstring As String = sender.name
        Dim tempint As Integer
        Dim tempstring2 As String = tempstring
        Dim allok As Boolean = False
        tempstring = tempstring.Replace("checkbox", "")
        tempint = Convert.ToDecimal(tempstring)
        For Each button As Control In Me.panel2.Controls
            If button.Name.IndexOf("checkbox") <> -1 Then
                Dim b1 As RadioButton = CType(button, RadioButton)
                If b1.Checked = True Then
                    allok = True
                    Exit For
                End If
            End If
        Next
        If allok = True Then
            Button5.Visible = True
            Button6.Visible = True
        Else
            Button5.Visible = False
            Button6.Visible = False
        End If

    End Sub


    Private Sub closeimage()
        Me.Controls.Remove(bigpanel)
        bigpanel = Nothing
        Me.Controls.Remove(bigpicbox)
        bigpicbox = Nothing
    End Sub





    Private Sub zoomimage(ByVal sender As Object, ByVal e As EventArgs)

        Dim tempstring As String = sender.name
        Dim tempint As Integer
        Dim tempstring2 As String = tempstring
        tempstring = tempstring.Replace("picture", "")
        tempint = Convert.ToDecimal(tempstring)
        tempint = tempint + ((currentpage - 1) * maxthumbs)
        rememberint = tempint
        Dim buffer(4000000) As Byte
        Dim size As Integer = 0
        Dim bytesRead As Integer = 0

        bigpanel = New Panel
        With bigpanel
            .Width = Me.Width
            .Height = Me.Height
            .BringToFront()
            .Dock = DockStyle.Fill
        End With

        bigpicbox = New PictureBox()

        With bigpicbox
            .Location = New Point(0, 0)
            .Width = bigpanel.Width
            .Height = bigpanel.Height
            .SizeMode = PictureBoxSizeMode.Zoom
            '.Image = sender.image
            .ImageLocation = downloadthumb(tempint + 1, 1)
            .Visible = True
            .BorderStyle = BorderStyle.Fixed3D
            AddHandler bigpicbox.DoubleClick, AddressOf closeimage
            .Dock = DockStyle.Fill
        End With

        Dim sizex As Integer = bigpicbox.Width
        Dim sizey As Integer = bigpicbox.Height


        Me.Controls.Add(bigpanel)
        bigpanel.BringToFront()
        Me.bigpanel.Controls.Add(bigpicbox)
        Me.Refresh()

    End Sub












    Private Sub initialise()
        If TextBox1.Text <> "" Then
            If IsNumeric(TextBox1.Text) And Convert.ToDecimal(TextBox1.Text) <> 0 Then
                maxthumbs = Convert.ToDecimal(TextBox1.Text)
                Form1.userPrefs.maximumthumbs = maxthumbs
            Else
                MsgBox("Invalid Maximum Thumb Value" & vbCrLf & "Setting to default Value of 10")
                maxthumbs = 10
                TextBox1.Text = "10"
                Form1.userPrefs.maximumthumbs = 10
            End If
        Else
            MsgBox("Invalid Maximum Thumb Value" & vbCrLf & "Setting to default Value of 10")
            maxthumbs = 10
            TextBox1.Text = "10"
            Form1.userPrefs.maximumthumbs = 10
        End If

        Button5.Visible = False
        Button6.Visible = False
        Me.Controls.Remove(panel2)
        panel2 = Nothing
        picboxes = Nothing
        checkboxes = Nothing
        reslabel = Nothing

        panel2 = New Panel
        With panel2
            .Width = Me.Width - 10
            .Height = 237
            .Location = New Point(2, 2)
            .AutoScroll = True
        End With
        Me.Controls.Add(panel2)

        panel2.Refresh()
        Application.DoEvents()

        ReDim posterurls(1000, 1)
        count = 0
        pagecount = 0
    End Sub
    Private Sub loadwebpage()
        urllinecount = 0



        Try
            Dim wrGETURL As WebRequest
            wrGETURL = WebRequest.Create(url)
            Dim myProxy As New WebProxy("myproxy", 80)
            myProxy.BypassProxyOnLocal = True
            Dim objStream As Stream
            objStream = wrGETURL.GetResponse.GetResponseStream()
            Dim objReader As New StreamReader(objStream)
            Dim sLine As String = ""
            urllinecount = 0

            Do While Not sLine Is Nothing
                urllinecount += 1
                sLine = objReader.ReadLine
                If Not sLine Is Nothing Then
                    websource(urllinecount) = sLine
                End If
            Loop
            objReader.Close()
            urllinecount -= 1
            'Dim tempboolean As Boolean
            'tempboolean = Form1.forceexit
            'If Form1.forceexit = True Then
            '    objStream.Dispose()
            '    Form1.completeclose = True
            '    Application.Exit()
            'End If


        Catch ex As WebException
            'MsgBox("Unable to load webpage " & url & vbCrLf & vbCrLf & ex.ToString)
        End Try






    End Sub



    Private Sub bigpicbox_LoadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs) Handles bigpicbox.LoadCompleted
        Dim bigpanellabel As Label
        bigpanellabel = New Label
        With bigpanellabel
            .Location = New Point(20, 20)
            .Width = 150
            .Height = 50
            .Visible = True
            .Text = "Double Click Image To" & vbCrLf & "Return To Browser"
            '   .BringToFront()
        End With

        Me.bigpanel.Controls.Add(bigpanellabel)
        bigpanellabel.BringToFront()
        Application.DoEvents()



        If Not bigpicbox.Image Is Nothing And bigpicbox.Image.Width > 20 Then

            Dim sizey As Integer = bigpicbox.Image.Height
            Dim sizex As Integer = bigpicbox.Image.Width
            Dim tempstring As String
            tempstring = "Full Image Resolution :- " & sizex.ToString & " x " & sizey.ToString
            resolutionlbl = New Label
            With resolutionlbl
                .Location = New Point(Me.Width - 200, 20)
                .Anchor = AnchorStyles.Right Or AnchorStyles.Top
                .Width = 180
                .Text = tempstring
                .BackColor = Color.Transparent
            End With

            Me.bigpanel.Controls.Add(resolutionlbl)
            resolutionlbl.BringToFront()
            Me.Refresh()
            Application.DoEvents()
            Dim tempstring2 As String = resolutionlbl.Text
        Else
            bigpicbox.ImageLocation = posterurls(rememberint + 1, 1)
        End If
    End Sub

    Private Sub tvposters_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Try
            panel2.Width = Me.Width - 10
            mainposter.Width = Panel1.Width
            mainposter.Height = Panel1.Height
        Catch
        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Button5.Visible = False
        Me.Controls.Remove(panel2)
        panel2 = Nothing
        picboxes = Nothing
        checkboxes = Nothing

        panel2 = New Panel
        With panel2
            .Width = 782
            .Height = 232
            .Location = New Point(2, 2)
            .AutoScroll = True
        End With
        Me.Controls.Add(panel2)

        currentpage += 1
        Button7.Enabled = True

        If currentpage = pagecount Then
            Button8.Enabled = False
        Else
            Button8.Enabled = True
        End If


        Dim tempint As Integer = (currentpage * (maxthumbs) + 1) - maxthumbs
        Dim tempint2 As Integer = currentpage * maxthumbs

        If tempint2 > count Then
            tempint2 = count
        End If

        Dim names As New List(Of String)()

        For f = tempint To tempint2
            names.Add(downloadthumb(f, 0))
        Next
        Label7.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & count.ToString & " Images"

        Dim location As Integer = 0
        Dim itemcounter As Integer = 0
        For Each item As String In names


            picboxes() = New PictureBox()
            With picboxes
                .Location = New Point(location, 0)
                .Width = 140
                .Height = 180
                .SizeMode = PictureBoxSizeMode.Zoom
                .ImageLocation = item
                .Visible = True
                .BorderStyle = BorderStyle.Fixed3D
                .Name = "picture" & itemcounter.ToString
                AddHandler picboxes.DoubleClick, AddressOf zoomimage
                AddHandler picboxes.LoadCompleted, AddressOf imageres
            End With

            checkboxes() = New RadioButton()
            With checkboxes
                .Location = New Point(location + 60, 195)
                .Name = "checkbox" & itemcounter.ToString
                .SendToBack()
                .Text = " "
                AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
            End With

            itemcounter += 1
            location += 160

            Me.panel2.Controls.Add(picboxes())
            Me.panel2.Controls.Add(checkboxes())
            Me.Refresh()
            Application.DoEvents()
        Next
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Button5.Visible = False
        Me.Controls.Remove(panel2)
        panel2 = Nothing
        picboxes = Nothing
        checkboxes = Nothing

        panel2 = New Panel
        With panel2
            .Width = 782
            .Height = 232
            .Location = New Point(2, 2)
            .AutoScroll = True
        End With
        Me.Controls.Add(panel2)

        currentpage -= 1
        Button7.Enabled = True
        If currentpage <= 1 Then
            Button7.Enabled = False
        Else
            Button7.Enabled = True
        End If
        If currentpage = pagecount Then
            Button8.Enabled = False
        Else
            Button8.Enabled = True
        End If
        Dim tempint As Integer = (currentpage * (maxthumbs) + 1) - maxthumbs
        Dim tempint2 As Integer = currentpage * maxthumbs
        If tempint2 > count Then
            tempint2 = count
        End If

        Dim names As New List(Of String)()

        For f = tempint To tempint2
            names.Add(downloadthumb(f, 0))
        Next
        Label7.Text = "Displaying " & tempint.ToString & " to " & tempint2 & " of " & count.ToString & " Images"

        Dim location As Integer = 0
        Dim itemcounter As Integer = 0
        For Each item As String In names
            picboxes() = New PictureBox()
            With picboxes
                .Location = New Point(location, 0)
                .Width = 140
                .Height = 180
                .SizeMode = PictureBoxSizeMode.Zoom
                .ImageLocation = item
                .Visible = True
                .BorderStyle = BorderStyle.Fixed3D
                .Name = "picture" & itemcounter.ToString
                AddHandler picboxes.DoubleClick, AddressOf zoomimage
                AddHandler picboxes.LoadCompleted, AddressOf imageres
            End With

            checkboxes() = New RadioButton()
            With checkboxes
                .Location = New Point(location + 60, 195)
                .Name = "checkbox" & itemcounter.ToString
                .SendToBack()
                .Text = " "
                AddHandler checkboxes.CheckedChanged, AddressOf radiochanged
            End With

            itemcounter += 1
            location += 160

            Me.panel2.Controls.Add(picboxes())
            Me.panel2.Controls.Add(checkboxes())
            Me.Refresh()
            Application.DoEvents()
        Next
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        'Call initialise()

        Dim tempstring As String
        If ComboBox2.Text.IndexOf("Season") <> -1 Or ComboBox2.Text.IndexOf("Spec") <> -1 Then

            If ComboBox2.SelectedItem.indexof("Season") <> -1 Then
                tempstring = ComboBox2.SelectedItem
                tempstring = tempstring.Replace("Season ", "")
                series = CInt(tempstring)
            ElseIf ComboBox2.SelectedItem.indexof("Specials") <> -1 Then
                series = 0
            End If

        End If

        Dim savepath As String

        If ComboBox2.Text.IndexOf("Main") <> -1 Then
            savepath = savethumbpath & "folder.jpg"

            If IO.File.Exists(savepath) Then
                Dim b1 As New Bitmap(savepath)
                Dim Image2 As New Bitmap(b1)
                mainposter.Image = Image2
                Label6.Visible = True
                tempstring = Image2.Width.ToString & " x " & Image2.Height.ToString
                Label6.Text = tempstring
                b1.Dispose()
                mainposter.Visible = True
            Else


            End If

        Else

            If series > 9 Then
                savepath = savethumbpath & "season" & series.ToString & ".tbn"
            Else
                savepath = savethumbpath & "season0" & series.ToString & ".tbn"
            End If

            If IO.File.Exists(savepath) Then

                Dim b1 As New Bitmap(savepath)
                Dim Image2 As New Bitmap(b1)
                mainposter.Image = Image2
                Label6.Visible = True
                tempstring = Image2.Width.ToString & " x " & Image2.Height.ToString
                Label6.Text = tempstring
                b1.Dispose()
                mainposter.Visible = True


            Else


            End If

        End If



    End Sub

    Private Sub btngetthumb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btngetthumb.Click
        Dim savepath As String

        If ComboBox2.Text.IndexOf("Main") <> -1 Then
            savepath = savethumbpath & "folder.jpg"
        End If
        If ComboBox2.Text.IndexOf("Season") <> -1 Or ComboBox2.Text.IndexOf("Spec") <> -1 Then
            If series > 9 Then
                savepath = savethumbpath & "season" & series.ToString & ".tbn"
            Else
                savepath = savethumbpath & "season0" & series.ToString & ".tbn"
            End If
        End If

        Dim MyWebClient As New System.Net.WebClient
        Try
            Dim ImageInBytes() As Byte = MyWebClient.DownloadData(TextBox5.Text)
            Dim ImageStream As New IO.MemoryStream(ImageInBytes)

            mainposter.Image = New System.Drawing.Bitmap(ImageStream)
            mainposter.Image.Save(savepath)

        Catch ex As Exception
            MsgBox("Unable To Download Image")
        End Try
        Panel3.Visible = False




    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim savepath As String

        If ComboBox2.Text.IndexOf("Main") <> -1 Then
            savepath = savethumbpath & "folder.jpg"
        End If
        If ComboBox2.Text.IndexOf("Season") <> -1 Or ComboBox2.Text.IndexOf("Spec") <> -1 Then
            If series > 9 Then
                savepath = savethumbpath & "season" & series.ToString & ".tbn"
            Else
                savepath = savethumbpath & "season0" & series.ToString & ".tbn"
            End If
        End If


        Dim tempstring As String
        Dim realnumber As Integer = 0
        Dim tempint As Integer = 0
        Dim tempstring2 As String = ""
        Dim allok As Boolean = False
        For Each button As Control In Me.panel2.Controls
            If button.Name.IndexOf("checkbox") <> -1 Then
                Dim b1 As RadioButton = CType(button, RadioButton)
                If b1.Checked = True Then
                    tempstring = b1.Name
                    tempstring = tempstring.Replace("checkbox", "")
                    tempint = Convert.ToDecimal(tempstring)
                    realnumber = tempint + ((currentpage - 1) * maxthumbs)
                    tempstring2 = downloadthumb(realnumber, 0)
                    allok = True
                    Exit For
                End If
            End If
        Next
        If allok = False Then
            MsgBox("No Fanart Is Selected")
        End If
        If allok = True Then
            For Each PictureBox2 As Control In Me.panel2.Controls
                If PictureBox2.Name.IndexOf("picture") <> -1 And PictureBox2.Name.IndexOf(tempint) <> -1 Then
                    Dim b1 As PictureBox = CType(PictureBox2, PictureBox)
                    If Not b1.Image Is Nothing Then
                        If b1.Image.Width > 20 Then
                            b1.Image.Save(savepath)
                            mainposter.Image = b1.Image
                            Label6.Visible = True
                            tempstring = b1.Image.Width.ToString & " x " & b1.Image.Height.ToString
                            Label6.Text = tempstring
                            mainposter.Visible = True
                            Exit For
                        Else
                            Label6.Visible = False
                        End If
                    Else
                        Label6.Visible = False
                    End If
                End If
            Next
        End If



















    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click

        'Call initialise()

        Dim savepath As String

        If ComboBox2.Text.IndexOf("Main") <> -1 Then
            savepath = savethumbpath & "folder.jpg"
        End If
        If ComboBox2.Text.IndexOf("Season") <> -1 Or ComboBox2.Text.IndexOf("Spec") <> -1 Then
            If series > 9 Then
                savepath = savethumbpath & "season" & series.ToString & ".tbn"
            Else
                savepath = savethumbpath & "season0" & series.ToString & ".tbn"
            End If
        End If



        Dim tempstring As String
        Dim tempint As Integer = 0
        Dim realnumber As Integer = 0
        Dim tempstring2 As String = ""
        Dim allok As Boolean = False
        For Each button As Control In Me.panel2.Controls
            If button.Name.IndexOf("checkbox") <> -1 Then
                Dim b1 As RadioButton = CType(button, RadioButton)
                If b1.Checked = True Then
                    tempstring = b1.Name
                    tempstring = tempstring.Replace("checkbox", "")
                    tempint = Convert.ToDecimal(tempstring)
                    realnumber = tempint + ((currentpage - 1) * maxthumbs)
                    tempstring2 = downloadthumb(realnumber, 1)
                    allok = True
                    Exit For
                End If
            End If
        Next
        If allok = False Then
            MsgBox("No Fanart Is Selected")
        End If
        If allok = True Then
            For Each PictureBox2 As Control In Me.panel2.Controls
                If PictureBox2.Name.IndexOf("picture") <> -1 And PictureBox2.Name.IndexOf(tempint.ToString) <> -1 Then
                    Dim b1 As PictureBox = CType(PictureBox2, PictureBox)
                    If Not b1.Image Is Nothing Then
                        If b1.Image.Width > 20 Then
                            With b1
                                .WaitOnLoad = True
                                Try
                                    .ImageLocation = (downloadthumb(realnumber + 1, 1))
                                Catch
                                    .ImageLocation = (downloadthumb(realnumber + 1, 0))
                                End Try
                            End With
                            b1.Image.Save(savepath)


                            mainposter.Image = b1.Image
                            Label6.Visible = True
                            tempstring = b1.Image.Width.ToString & " x " & b1.Image.Height.ToString
                            Label6.Text = tempstring
                            mainposter.Visible = True
                            With b1
                                .WaitOnLoad = True
                                .ImageLocation = (downloadthumb(realnumber + 1, 0))
                            End With

                            Exit For
                        Else
                            Label6.Visible = False
                        End If
                    Else
                        Label6.Visible = False
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        downloadthumbcount = 0
        ReDim posterurls(1000, 1)
        count = 0
        pagecount = 0
        Call initialise()
        'url = "http://www.imdb.com/title/" & Form1.imdbidd & "/mediaindex"
        Call loadwebpage()
        Dim tempint As Integer
        Dim imdbsmall(2000) As String
        Dim reached As Boolean = False
        Dim counter As Integer = 0
        For f = 1 To urllinecount
            If websource(f).IndexOf("<a href=""?page=") <> -1 Then
                websource(f) = websource(f).Replace("<a href=""?page=", "")
                websource(f) = websource(f).Substring(0, 1)
                tempint = Convert.ToString(websource(f))
                If tempint > totalpages Then totalpages = tempint
            End If
            '<div class="thumb_list" 


            If websource(f).IndexOf("<div class=""thumb_list""") <> -1 Then
                reached = True
            End If
            If reached = True Then
                If websource(f).IndexOf("</div>") <> -1 Then
                    'reached = False
                    'Exit For
                End If
                If websource(f).IndexOf("src=""http://") <> -1 Then
                    websource(f) = websource(f).Substring(websource(f).IndexOf("src=""") - 1, websource(f).Length - websource(f).IndexOf("src=""") - 1)
                    websource(f).TrimStart()
                    websource(f) = websource(f).Replace("src=""", "")
                    If websource(f).IndexOf("._V1._") <> -1 Then
                        counter = counter + 1
                        imdbsmall(counter) = websource(f).Substring(1, websource(f).IndexOf("._V1._"))
                    End If
                End If
            End If
        Next
        For g = 2 To totalpages
            'url = "http://www.imdb.com/title/" & Form1.imdbidd & "/mediaindex?page=" & g.ToString
            Call loadwebpage()
            For f = 1 To urllinecount
                If websource(f).IndexOf("<div class=""thumb_list""") <> -1 Then
                    reached = True
                End If
                If reached = True Then
                    If websource(f).IndexOf("</div>") <> -1 Then
                        'reached = False
                        'Exit For
                    End If
                    If websource(f).IndexOf("src=""http://") <> -1 Then
                        websource(f) = websource(f).Substring(websource(f).IndexOf("src=""") - 1, websource(f).Length - websource(f).IndexOf("src=""") - 1)
                        websource(f).TrimStart()
                        websource(f) = websource(f).Replace("src=""", "")
                        If websource(f).IndexOf("._V1._") <> -1 Then
                            counter = counter + 1
                            imdbsmall(counter) = websource(f).Substring(1, websource(f).IndexOf("._V1._"))
                        End If
                    End If
                End If
            Next
        Next

        For f = 1 To counter
            imdbsmall(f) = imdbsmall(f) & "_V1._SX1000_SY1000_.jpg"

        Next



        'ReDim downloadthumb(2000, 1)
        'downloadthumbcount = 0





        For f = counter To 1 Step -1
            downloadthumbcount = downloadthumbcount + 1
            downloadthumb(downloadthumbcount, 0) = imdbsmall(f)
            downloadthumb(downloadthumbcount, 1) = imdbsmall(f)
        Next


        'If ComboBox2.Text = "Main TV Show Thumb" Then
        '    For f = 1 To counter
        '        downloadthumbcount = downloadthumbcount + 1
        '        downloadthumb(downloadthumbcount) = imdbsmall(f)
        '    Next
        'End If


        count = downloadthumbcount
        Call displayselection()


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Call initialise()

        ReDim downloadthumb(2000, 1)
        downloadthumbcount = 0
        workingthumb = 0
        Dim tempstring As String
        Dim tempstring2 As String
        downloadthumbcount = 0
        'url = "http://thetvdb.com/api/6E82FED600783400/series/" & Form1.tvdbidd & "/banners.xml"
        Call loadwebpage()
        Dim tempstring3 As String

        tempstring3 = "<BannerPath>fanart"
        For f = 1 To urllinecount
            If websource(f).IndexOf(tempstring3) = -1 Then
                If websource(f).IndexOf("<BannerPath>") <> -1 Then
                    tempstring2 = websource(f)
                    tempstring2 = tempstring2.Replace("<BannerPath>", "")
                    tempstring2 = tempstring2.Replace("</BannerPath>", "")
                    tempstring2 = tempstring2.Replace("  ", "")
                    '_cache/posters/73739-4.jpg
                    tempstring = tempstring2
                    tempstring2 = "http://thetvdb.com/banners/" & tempstring2
                    tempstring = "http://thetvdb.com/banners/_cache/" & tempstring
                    downloadthumbcount += 1
                    downloadthumb(downloadthumbcount, 0) = tempstring
                    downloadthumb(downloadthumbcount, 1) = tempstring2
                End If
            End If
        Next

        count = downloadthumbcount
        Call displayselection()



    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Panel3.Visible = True
    End Sub

    Private Sub btncancelgetthumburl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancelgetthumburl.Click
        Panel3.Visible = False
    End Sub

    Private Sub btnthumbbrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnthumbbrowse.Click
        openFD.InitialDirectory = Form1.applicationPath
        openFD.Title = "Select a jpeg image file File"
        openFD.FileName = ""
        openFD.Filter = "Media Companion Image Files|*.jpg;*.tbn|All Files|*.*"
        openFD.FilterIndex = 0
        openFD.ShowDialog()
        TextBox5.Text = openFD.FileName
    End Sub
End Class