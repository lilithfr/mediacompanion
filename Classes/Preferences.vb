Imports System.IO
Imports System.Xml
Imports System.Threading


Public Class Preferences


    Public Sub saveconfig()
        Monitor.Enter(Me)
        Try
            Dim doc As New XmlDocument

            Dim thispref As XmlNode = Nothing
            Dim xmlproc As XmlDeclaration

            xmlproc = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
            doc.AppendChild(xmlproc)
            Dim root As XmlElement
            Dim child As XmlElement
            root = doc.CreateElement("xbmc_media_companion_config_v1.0")


            For Each path In Form1.tvFolders
                child = doc.CreateElement("tvfolder")
                child.InnerText = path
                root.AppendChild(child)
            Next

            For Each path In Form1.tvRootFolders
                child = doc.CreateElement("tvrootfolder")
                child.InnerText = path
                root.AppendChild(child)
            Next

            For Each path In Form1.movieFolders
                child = doc.CreateElement("nfofolder")
                child.InnerText = path
                root.AppendChild(child)
            Next
            Dim list As New List(Of String)
            For Each path In Form1.userPrefs.offlinefolders
                If Not list.Contains(path) Then
                    child = doc.CreateElement("offlinefolder")
                    child.InnerText = path
                    root.AppendChild(child)
                    list.Add(path)
                End If
            Next


            child = doc.CreateElement("gettrailer")
            child.InnerText = Form1.userPrefs.gettrailer.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("keepfoldername")
            child.InnerText = Form1.userPrefs.keepfoldername.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("startupcache")
            child.InnerText = Form1.userPrefs.startupCache.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("ignoretrailers")
            child.InnerText = Form1.userPrefs.ignoretrailers.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviescraper")
            child.InnerText = Form1.userPrefs.moviescraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("nfoposterscraper")
            child.InnerText = Form1.userPrefs.nfoposterscraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("alwaysuseimdbid")
            child.InnerText = Form1.userPrefs.alwaysuseimdbid.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("ignoreactorthumbs")
            child.InnerText = Form1.userPrefs.ignoreactorthumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("maxactors")
            child.InnerText = Form1.userPrefs.maxactors.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("maxmoviegenre")
            child.InnerText = Form1.userPrefs.maxmoviegenre.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("enablehdtags")
            child.InnerText = Form1.userPrefs.enablehdtags.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("renamenfofiles")
            child.InnerText = Form1.userPrefs.renamenfofiles.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("checkinfofiles")
            child.InnerText = Form1.userPrefs.checkinfofiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("tvshowautoquick")
            child.InnerText = Form1.userPrefs.tvshowautoquick.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("disablelogfiles")
            child.InnerText = Form1.userPrefs.disablelogfiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("actorseasy")
            child.InnerText = Form1.userPrefs.actorseasy.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("copytvactorthumbs")
            child.InnerText = Form1.userPrefs.copytvactorthumbs.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("fanartnotstacked")
            child.InnerText = Form1.userPrefs.fanartnotstacked.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("posternotstacked")
            child.InnerText = Form1.userPrefs.posternotstacked.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("lastpath")
            child.InnerText = Form1.userPrefs.lastpath
            root.AppendChild(child)

            child = doc.CreateElement("downloadfanart")
            child.InnerText = Form1.userPrefs.savefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("scrapemovieposters")
            child.InnerText = Form1.userPrefs.scrapemovieposters.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("usefanart")
            child.InnerText = Form1.userPrefs.usefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("dontdisplayposter")
            child.InnerText = Form1.userPrefs.dontdisplayposter.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("usefoldernames")
            child.InnerText = Form1.userPrefs.usefoldernames.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("rarsize")
            child.InnerText = Form1.userPrefs.rarsize.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("actorsave")
            child.InnerText = Form1.userPrefs.actorsave.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("actorsavepath")
            child.InnerText = Form1.userPrefs.actorsavepath
            root.AppendChild(child)


            child = doc.CreateElement("actornetworkpath")
            child.InnerText = Form1.userPrefs.actornetworkpath
            root.AppendChild(child)


            child = doc.CreateElement("resizefanart")
            child.InnerText = Form1.userPrefs.resizefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("overwritethumbs")
            child.InnerText = Form1.userPrefs.overwritethumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("defaulttvthumb")
            child.InnerText = Form1.userPrefs.defaulttvthumb
            root.AppendChild(child)


            child = doc.CreateElement("imdbmirror")
            child.InnerText = Form1.userPrefs.imdbmirror
            root.AppendChild(child)


            child = doc.CreateElement("backgroundcolour")
            child.InnerText = Form1.userPrefs.backgroundcolour
            root.AppendChild(child)


            child = doc.CreateElement("forgroundcolour")
            child.InnerText = Form1.userPrefs.forgroundcolour
            root.AppendChild(child)


            child = doc.CreateElement("remembersize")
            child.InnerText = Form1.userPrefs.remembersize.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("locx")
            child.InnerText = Form1.userPrefs.locx.ToString
            root.AppendChild(child)

            child = doc.CreateElement("locy")
            child.InnerText = Form1.userPrefs.locy.ToString
            root.AppendChild(child)


            child = doc.CreateElement("formheight")
            child.InnerText = Form1.userPrefs.formheight.ToString
            root.AppendChild(child)


            child = doc.CreateElement("formwidth")
            child.InnerText = Form1.userPrefs.formwidth.ToString
            root.AppendChild(child)


            child = doc.CreateElement("videoplaybackmode")
            child.InnerText = Form1.userPrefs.videoplaybackmode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("createfolderjpg")
            child.InnerText = Form1.userPrefs.createfolderjpg.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("basicsavemode")
            child.InnerText = Form1.userPrefs.basicsavemode.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("startupdisplaynamemode")
            child.InnerText = Form1.userPrefs.startupdisplaynamemode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("namemode")
            child.InnerText = Form1.userPrefs.namemode
            root.AppendChild(child)


            child = doc.CreateElement("tvdbmode")
            child.InnerText = Form1.userPrefs.sortorder
            root.AppendChild(child)


            child = doc.CreateElement("tvdbactorscrape")
            child.InnerText = Form1.userPrefs.tvdbactorscrape.ToString
            root.AppendChild(child)


            child = doc.CreateElement("usetransparency")
            child.InnerText = Form1.userPrefs.usetransparency.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("transparencyvalue")
            child.InnerText = Form1.userPrefs.transparencyvalue.ToString
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvfanart")
            child.InnerText = Form1.userPrefs.tvfanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvposter")
            child.InnerText = Form1.userPrefs.tvposter.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("keepfoldername")
            child.InnerText = Form1.userPrefs.keepfoldername.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvseasonthumbs")
            child.InnerText = Form1.userPrefs.downloadtvseasonthumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("maximumthumbs")
            child.InnerText = Form1.userPrefs.maximumthumbs.ToString
            root.AppendChild(child)


            child = doc.CreateElement("startupmode")
            child.InnerText = Form1.userPrefs.startupmode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("hdtvtags")
            child.InnerText = Form1.userPrefs.enabletvhdtags.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("disablelogs")
            child.InnerText = Form1.userPrefs.disablelogfiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("disabletvlogs")
            child.InnerText = Form1.userPrefs.disabletvlogs.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("overwritethumb")
            child.InnerText = Form1.userPrefs.overwritethumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("savefanart")
            child.InnerText = Form1.userPrefs.savefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("postertype")
            child.InnerText = Form1.userPrefs.postertype
            root.AppendChild(child)

            child = doc.CreateElement("roundminutes")
            child.InnerText = Form1.userPrefs.roundminutes.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("tvactorscrape")
            child.InnerText = Form1.userPrefs.tvdbactorscrape.ToString
            root.AppendChild(child)


            child = doc.CreateElement("videomode")
            child.InnerText = Form1.userPrefs.videomode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("selectedvideoplayer")
            child.InnerText = Form1.userPrefs.selectedvideoplayer
            root.AppendChild(child)

            child = doc.CreateElement("externalbrowser")
            child.InnerText = Form1.userPrefs.externalbrowser.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviethumbpriority")
            Dim tempstring As String
            tempstring = Form1.userPrefs.moviethumbpriority(0) & "|" & Form1.userPrefs.moviethumbpriority(1) & "|" & Form1.userPrefs.moviethumbpriority(2) & "|" & Form1.userPrefs.moviethumbpriority(3)
            child.InnerText = tempstring
            root.AppendChild(child)


            child = doc.CreateElement("tvdblanguage")
            tempstring = ""
            tempstring = Form1.userPrefs.tvdblanguagecode & "|" & Form1.userPrefs.tvdblanguage
            child.InnerText = tempstring
            root.AppendChild(child)



            child = doc.CreateElement("certificatepriority")
            tempstring = ""
            For f = 0 To 32
                tempstring = tempstring & Form1.userPrefs.certificatepriority(f) & "|"
            Next
            tempstring = tempstring & Form1.userPrefs.certificatepriority(33)
            child.InnerText = tempstring
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer1")
            child.InnerText = Form1.userPrefs.splt1.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer2")
            child.InnerText = Form1.userPrefs.splt2.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer3")
            child.InnerText = Form1.userPrefs.splt3.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer4")
            child.InnerText = Form1.userPrefs.splt4.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer5")
            child.InnerText = Form1.userPrefs.splt5.ToString
            root.AppendChild(child)


            child = doc.CreateElement("seasonall")
            child.InnerText = Form1.userPrefs.seasonall
            root.AppendChild(child)

            child = doc.CreateElement("maximised")
            If Form1.userPrefs.maximised = True Then
                child.InnerText = "true"
            Else
                child.InnerText = "false"
            End If
            root.AppendChild(child)

            child = doc.CreateElement("tvrename")
            child.InnerText = Form1.userPrefs.tvrename.ToString
            root.AppendChild(child)

            child = doc.CreateElement("eprenamelowercase")
            child.InnerText = Form1.userPrefs.eprenamelowercase.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("tvshowrebuildlog")
            child.InnerText = Form1.userPrefs.tvshowrebuildlog.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesortorder")
            child.InnerText = Form1.userPrefs.moviesortorder.ToString
            root.AppendChild(child)

            child = doc.CreateElement("moviedefaultlist")
            child.InnerText = Form1.userPrefs.moviedefaultlist.ToString
            root.AppendChild(child)

            child = doc.CreateElement("autoepisodescreenshot")
            child.InnerText = Form1.userPrefs.autoepisodescreenshot.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("ignorearticle")
            child.InnerText = Form1.userPrefs.ignorearticle.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("TVShowUseXBMCScraper")
            child.InnerText = Form1.userPrefs.tvshow_useXBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesUseXBMCScraper")
            child.InnerText = Form1.userPrefs.movies_useXBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("whatXBMCScraper")
            child.InnerText = Form1.userPrefs.XBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("startuptab")
            child.InnerText = Form1.userPrefs.startuptab.ToString
            root.AppendChild(child)

            child = doc.CreateElement("intruntime")
            child.InnerText = Form1.userPrefs.intruntime.ToString.ToLower
            root.AppendChild(child)

            If Form1.userPrefs.font <> Nothing Then
                If Form1.userPrefs.font <> "" Then
                    child = doc.CreateElement("font")
                    child.InnerText = Form1.userPrefs.font
                    root.AppendChild(child)
                End If
            End If

            child = doc.CreateElement("autorenameepisodes")
            child.InnerText = Form1.userPrefs.autorenameepisodes.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("scrapefullcert")
            child.InnerText = Form1.userPrefs.scrapefullcert.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesets")
            Dim childchild As XmlElement
            For Each movieset In Form1.userPrefs.moviesets
                If movieset <> "None" Then
                    childchild = doc.CreateElement("set")
                    childchild.InnerText = movieset
                    child.AppendChild(childchild)
                End If
            Next
            root.AppendChild(child)

            child = doc.CreateElement("table")
            Dim childchild2 As XmlElement
            childchild2 = doc.CreateElement("sort")
            childchild2.InnerText = Form1.userPrefs.tablesortorder
            child.AppendChild(childchild2)
            For Each tabs In Form1.userPrefs.tableview
                childchild2 = doc.CreateElement("tab")
                childchild2.InnerText = tabs
                child.AppendChild(childchild2)
            Next
            root.AppendChild(child)



            For Each com In Form1.userPrefs.commandlist
                If com.command <> "" And com.title <> "" Then
                    child = doc.CreateElement("comms")
                    childchild = doc.CreateElement("title")
                    childchild.InnerText = com.title
                    child.AppendChild(childchild)
                    childchild = doc.CreateElement("command")
                    childchild.InnerText = com.command
                    child.AppendChild(childchild)
                    root.AppendChild(child)
                End If
            Next




            doc.AppendChild(root)
            Dim tempstring2 As String = Form1.workingProfile.config
            Dim output As New XmlTextWriter(Form1.workingProfile.config, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()
        Catch

        Finally
            Monitor.Exit(Me)
        End Try


    End Sub

    Public Sub loadconfig()
        Form1.userPrefs.commandlist.Clear()
        Form1.userPrefs.moviesets.Clear()
        Form1.userPrefs.moviesets.Add("None")
        Form1.movieFolders.Clear()
        Form1.tvFolders.Clear()
        Form1.tvRootFolders.Clear()
        Form1.userPrefs.tableview.Clear()
        Dim tempstring As String = Form1.workingProfile.config
        If Not IO.File.Exists(Form1.workingProfile.config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(Form1.workingProfile.config)
        Catch ex As Exception
            MsgBox("Error : pr24")
        End Try

        Dim thisresult As XmlNode = Nothing

        For Each thisresult In prefs("xbmc_media_companion_config_v1.0")
            'Try
            Select Case thisresult.Name
                Case "moviesets"
                    Dim thisset As XmlNode = Nothing
                    For Each thisset In thisresult.ChildNodes
                        Select Case thisset.Name
                            Case "set"
                                Form1.userPrefs.moviesets.Add(thisset.InnerText)
                        End Select
                    Next
                Case "table"
                    Dim thistable As XmlNode = Nothing
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "tab"
                                Form1.userPrefs.tableview.Add(thistable.InnerText)
                            Case "sort"
                                Form1.userPrefs.tablesortorder = thistable.InnerText
                        End Select
                    Next
                Case "comms"
                    Dim thistable As XmlNode = Nothing
                    Dim newcom As New ListOfCommands
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "title"
                                newcom.title = thistable.InnerText
                            Case "command"
                                newcom.command = thistable.InnerText
                        End Select
                    Next
                    If newcom.command <> "" And newcom.title <> "" Then
                        Form1.userPrefs.commandlist.Add(newcom)
                    End If
                Case "seasonall"
                    Form1.userPrefs.seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    Form1.userPrefs.splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    Form1.userPrefs.splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    Form1.userPrefs.splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    Form1.userPrefs.splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    Form1.userPrefs.splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        Form1.userPrefs.maximised = True
                    Else
                        Form1.userPrefs.maximised = False
                    End If
                Case "locx"
                    Form1.userPrefs.locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    Form1.userPrefs.locy = Convert.ToInt32(thisresult.InnerText)
                Case "nfofolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.movieFolders.Add(decodestring)
                Case "offlinefolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.userPrefs.offlinefolders.Add(decodestring)
                Case "tvfolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.tvFolders.Add(decodestring)
                Case "tvrootfolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.tvRootFolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.gettrailer = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.tvshowautoquick = False
                    End If
                Case "intruntime"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.intruntime = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.intruntime = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.startupCache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.startupCache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            Form1.userPrefs.font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    Form1.userPrefs.maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    Form1.userPrefs.maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.enablehdtags = False
                    End If

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.renamenfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.renamenfofiles = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.disablelogfiles = False
                    End If

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.dontdisplayposter = False
                    End If

                Case "rarsize"
                    Form1.userPrefs.rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.copytvactorthumbs = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.userPrefs.actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.userPrefs.actornetworkpath = decodestring

                Case "resizefanart"
                    Form1.userPrefs.resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    Form1.userPrefs.defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    Form1.userPrefs.imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim Form1.userPrefs.moviethumbpriority(3)
                    Form1.userPrefs.moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim Form1.userPrefs.certificatepriority(33)
                    Form1.userPrefs.certificatepriority = thisresult.InnerXml.Split("|")

                Case "backgroundcolour"
                    Form1.userPrefs.backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    Form1.userPrefs.forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.remembersize = False
                    End If

                Case "formheight"
                    Form1.userPrefs.formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    Form1.userPrefs.formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    Form1.userPrefs.videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.usefoldernames = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    Form1.userPrefs.startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    Form1.userPrefs.namemode = thisresult.InnerXml

                Case "tvdblanguage"
                    Dim partone() As String
                    partone = thisresult.InnerXml.Split("|")
                    For f = 0 To 1
                        If partone(0).Length = 2 Then
                            Form1.userPrefs.tvdblanguagecode = partone(0)
                            Form1.userPrefs.tvdblanguage = partone(1)
                            Exit For
                        Else
                            Form1.userPrefs.tvdblanguagecode = partone(1)
                            Form1.userPrefs.tvdblanguage = partone(0)
                        End If
                    Next

                Case "tvdbmode"
                    Form1.userPrefs.sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    Form1.userPrefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.usetransparency = False
                    End If

                Case "transparencyvalue"
                    Form1.userPrefs.transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.roundminutes = False
                    End If

                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.autoepisodescreenshot = False
                    End If

                Case "ignorearticle"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.ignorearticle = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.ignorearticle = False
                    End If

                Case "TVShowUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.tvshow_useXBMC_Scraper = True
                        Form1.GroupBox22.Visible = False
                        Form1.GroupBox22.SendToBack()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = True
                        Form1.GroupBox_TVDB_Scraper_Preferences.BringToFront()
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.tvshow_useXBMC_Scraper = False
                        Form1.GroupBox22.Visible = True
                        Form1.GroupBox22.BringToFront()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = False
                        Form1.GroupBox_TVDB_Scraper_Preferences.SendToBack()
                    End If
                    Read_XBMC_TVDB_Scraper_Config()

                Case "moviesUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.movies_useXBMC_Scraper = True
                        Form1.RadioButton51.Visible = True
                        Form1.RadioButton52.Visible = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.movies_useXBMC_Scraper = False
                        Form1.RadioButton51.Visible = False
                        Form1.RadioButton52.Visible = False
                    End If


                Case "whatXBMCScraper"
                    Form1.userPrefs.XBMC_Scraper = thisresult.InnerXml
                    If thisresult.InnerXml = "imdb" Then
                        Form1.RadioButton51.Checked = True
                        Read_XBMC_IMDB_Scraper_Config()
                    ElseIf thisresult.InnerXml = "tmdb" Then
                        Form1.RadioButton52.Checked = True
                        Read_XBMC_TMDB_Scraper_Config()
                    End If


                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    Form1.userPrefs.maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    Form1.userPrefs.startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.disabletvlogs = False
                    End If

                Case "overwritethumb"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.overwritethumbs = False
                    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.savefanart = False
                    End If

                Case "postertype"
                    Form1.userPrefs.postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    Form1.userPrefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    Form1.userPrefs.videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    Form1.userPrefs.selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    Form1.userPrefs.maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    Form1.userPrefs.lastpath = thisresult.InnerXml

                Case "moviescraper"
                    Form1.userPrefs.moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    Form1.userPrefs.nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.externalbrowser = False
                    End If
                Case "tvrename"
                    Form1.userPrefs.tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrebuildlog"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.tvshowrebuildlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.tvshowrebuildlog = False
                    End If
                    'Public moviesortorder As Byte
                    'Public moviedefaultlist As Byte
                    'Public lasttab As Byte
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.autorenameepisodes = False
                    End If
                Case "eprenamelowercase"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.eprenamelowercase = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.eprenamelowercase = False
                    End If
                Case "moviesortorder"
                    Form1.userPrefs.moviesortorder = Convert.ToByte(thisresult.InnerText)
                Case "moviedefaultlist"
                    Form1.userPrefs.moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    Form1.userPrefs.startuptab = Convert.ToByte(thisresult.InnerText)
                Case "scrapefullcert"
                    If thisresult.InnerXml = "true" Then
                        Form1.userPrefs.scrapefullcert = True
                        Form1.ScrapeFullCertCheckBox.Checked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userPrefs.scrapefullcert = False
                    End If
            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
        Next

    End Sub




End Class
