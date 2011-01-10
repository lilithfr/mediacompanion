Imports System.IO
Imports System.Xml
Imports System.Threading


Public Class _preferences


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


            For Each path In Form1.tvfolders
                child = doc.CreateElement("tvfolder")
                child.InnerText = path
                root.AppendChild(child)
            Next

            For Each path In Form1.tvrootfolders
                child = doc.CreateElement("tvrootfolder")
                child.InnerText = path
                root.AppendChild(child)
            Next

            For Each path In Form1.moviefolders
                child = doc.CreateElement("nfofolder")
                child.InnerText = path
                root.AppendChild(child)
            Next
            Dim list As New List(Of String)
            For Each path In Form1.userprefs.offlinefolders
                If Not list.Contains(path) Then
                    child = doc.CreateElement("offlinefolder")
                    child.InnerText = path
                    root.AppendChild(child)
                    list.Add(path)
                End If
            Next


            child = doc.CreateElement("gettrailer")
            child.InnerText = Form1.userprefs.gettrailer.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("keepfoldername")
            child.InnerText = Form1.userprefs.keepfoldername.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("startupcache")
            child.InnerText = Form1.userprefs.startupcache.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("ignoretrailers")
            child.InnerText = Form1.userprefs.ignoretrailers.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviescraper")
            child.InnerText = Form1.userprefs.moviescraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("nfoposterscraper")
            child.InnerText = Form1.userprefs.nfoposterscraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("alwaysuseimdbid")
            child.InnerText = Form1.userprefs.alwaysuseimdbid.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("ignoreactorthumbs")
            child.InnerText = Form1.userprefs.ignoreactorthumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("maxactors")
            child.InnerText = Form1.userprefs.maxactors.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("maxmoviegenre")
            child.InnerText = Form1.userprefs.maxmoviegenre.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("enablehdtags")
            child.InnerText = Form1.userprefs.enablehdtags.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("renamenfofiles")
            child.InnerText = Form1.userprefs.renamenfofiles.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("checkinfofiles")
            child.InnerText = Form1.userprefs.checkinfofiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("tvshowautoquick")
            child.InnerText = Form1.userprefs.tvshowautoquick.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("disablelogfiles")
            child.InnerText = Form1.userprefs.disablelogfiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("actorseasy")
            child.InnerText = Form1.userprefs.actorseasy.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("copytvactorthumbs")
            child.InnerText = Form1.userprefs.copytvactorthumbs.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("fanartnotstacked")
            child.InnerText = Form1.userprefs.fanartnotstacked.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("posternotstacked")
            child.InnerText = Form1.userprefs.posternotstacked.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("lastpath")
            child.InnerText = Form1.userprefs.lastpath
            root.AppendChild(child)

            child = doc.CreateElement("downloadfanart")
            child.InnerText = Form1.userprefs.savefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("scrapemovieposters")
            child.InnerText = Form1.userprefs.scrapemovieposters.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("usefanart")
            child.InnerText = Form1.userprefs.usefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("dontdisplayposter")
            child.InnerText = Form1.userprefs.dontdisplayposter.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("usefoldernames")
            child.InnerText = Form1.userprefs.usefoldernames.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("rarsize")
            child.InnerText = Form1.userprefs.rarsize.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("actorsave")
            child.InnerText = Form1.userprefs.actorsave.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("actorsavepath")
            child.InnerText = Form1.userprefs.actorsavepath
            root.AppendChild(child)


            child = doc.CreateElement("actornetworkpath")
            child.InnerText = Form1.userprefs.actornetworkpath
            root.AppendChild(child)


            child = doc.CreateElement("resizefanart")
            child.InnerText = Form1.userprefs.resizefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("overwritethumbs")
            child.InnerText = Form1.userprefs.overwritethumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("defaulttvthumb")
            child.InnerText = Form1.userprefs.defaulttvthumb
            root.AppendChild(child)


            child = doc.CreateElement("imdbmirror")
            child.InnerText = Form1.userprefs.imdbmirror
            root.AppendChild(child)


            child = doc.CreateElement("backgroundcolour")
            child.InnerText = Form1.userprefs.backgroundcolour
            root.AppendChild(child)


            child = doc.CreateElement("forgroundcolour")
            child.InnerText = Form1.userprefs.forgroundcolour
            root.AppendChild(child)


            child = doc.CreateElement("remembersize")
            child.InnerText = Form1.userprefs.remembersize.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("locx")
            child.InnerText = Form1.userprefs.locx.ToString
            root.AppendChild(child)

            child = doc.CreateElement("locy")
            child.InnerText = Form1.userprefs.locy.ToString
            root.AppendChild(child)


            child = doc.CreateElement("formheight")
            child.InnerText = Form1.userprefs.formheight.ToString
            root.AppendChild(child)


            child = doc.CreateElement("formwidth")
            child.InnerText = Form1.userprefs.formwidth.ToString
            root.AppendChild(child)


            child = doc.CreateElement("videoplaybackmode")
            child.InnerText = Form1.userprefs.videoplaybackmode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("createfolderjpg")
            child.InnerText = Form1.userprefs.createfolderjpg.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("basicsavemode")
            child.InnerText = Form1.userprefs.basicsavemode.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("startupdisplaynamemode")
            child.InnerText = Form1.userprefs.startupdisplaynamemode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("namemode")
            child.InnerText = Form1.userprefs.namemode
            root.AppendChild(child)


            child = doc.CreateElement("tvdbmode")
            child.InnerText = Form1.userprefs.sortorder
            root.AppendChild(child)


            child = doc.CreateElement("tvdbactorscrape")
            child.InnerText = Form1.userprefs.tvdbactorscrape.ToString
            root.AppendChild(child)


            child = doc.CreateElement("usetransparency")
            child.InnerText = Form1.userprefs.usetransparency.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("transparencyvalue")
            child.InnerText = Form1.userprefs.transparencyvalue.ToString
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvfanart")
            child.InnerText = Form1.userprefs.tvfanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvposter")
            child.InnerText = Form1.userprefs.tvposter.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("keepfoldername")
            child.InnerText = Form1.userprefs.keepfoldername.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("downloadtvseasonthumbs")
            child.InnerText = Form1.userprefs.downloadtvseasonthumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("maximumthumbs")
            child.InnerText = Form1.userprefs.maximumthumbs.ToString
            root.AppendChild(child)


            child = doc.CreateElement("startupmode")
            child.InnerText = Form1.userprefs.startupmode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("hdtvtags")
            child.InnerText = Form1.userprefs.enabletvhdtags.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("disablelogs")
            child.InnerText = Form1.userprefs.disablelogfiles.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("disabletvlogs")
            child.InnerText = Form1.userprefs.disabletvlogs.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("overwritethumb")
            child.InnerText = Form1.userprefs.overwritethumbs.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("savefanart")
            child.InnerText = Form1.userprefs.savefanart.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("postertype")
            child.InnerText = Form1.userprefs.postertype
            root.AppendChild(child)

            child = doc.CreateElement("roundminutes")
            child.InnerText = Form1.userprefs.roundminutes.ToString.ToLower
            root.AppendChild(child)


            child = doc.CreateElement("tvactorscrape")
            child.InnerText = Form1.userprefs.tvdbactorscrape.ToString
            root.AppendChild(child)


            child = doc.CreateElement("videomode")
            child.InnerText = Form1.userprefs.videomode.ToString
            root.AppendChild(child)


            child = doc.CreateElement("selectedvideoplayer")
            child.InnerText = Form1.userprefs.selectedvideoplayer
            root.AppendChild(child)

            child = doc.CreateElement("externalbrowser")
            child.InnerText = Form1.userprefs.externalbrowser.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviethumbpriority")
            Dim tempstring As String
            tempstring = Form1.userprefs.moviethumbpriority(0) & "|" & Form1.userprefs.moviethumbpriority(1) & "|" & Form1.userprefs.moviethumbpriority(2) & "|" & Form1.userprefs.moviethumbpriority(3)
            child.InnerText = tempstring
            root.AppendChild(child)


            child = doc.CreateElement("tvdblanguage")
            tempstring = ""
            tempstring = Form1.userprefs.tvdblanguagecode & "|" & Form1.userprefs.tvdblanguage
            child.InnerText = tempstring
            root.AppendChild(child)



            child = doc.CreateElement("certificatepriority")
            tempstring = ""
            For f = 0 To 32
                tempstring = tempstring & Form1.userprefs.certificatepriority(f) & "|"
            Next
            tempstring = tempstring & Form1.userprefs.certificatepriority(33)
            child.InnerText = tempstring
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer1")
            child.InnerText = Form1.userprefs.splt1.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer2")
            child.InnerText = Form1.userprefs.splt2.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer3")
            child.InnerText = Form1.userprefs.splt3.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer4")
            child.InnerText = Form1.userprefs.splt4.ToString
            root.AppendChild(child)

            child = doc.CreateElement("splitcontainer5")
            child.InnerText = Form1.userprefs.splt5.ToString
            root.AppendChild(child)


            child = doc.CreateElement("seasonall")
            child.InnerText = Form1.userprefs.seasonall
            root.AppendChild(child)

            child = doc.CreateElement("maximised")
            If Form1.userprefs.maximised = True Then
                child.InnerText = "true"
            Else
                child.InnerText = "false"
            End If
            root.AppendChild(child)

            child = doc.CreateElement("tvrename")
            child.InnerText = Form1.userprefs.tvrename.ToString
            root.AppendChild(child)

            child = doc.CreateElement("eprenamelowercase")
            child.InnerText = Form1.userprefs.eprenamelowercase.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("tvshowrebuildlog")
            child.InnerText = Form1.userprefs.tvshowrebuildlog.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesortorder")
            child.InnerText = Form1.userprefs.moviesortorder.ToString
            root.AppendChild(child)

            child = doc.CreateElement("moviedefaultlist")
            child.InnerText = Form1.userprefs.moviedefaultlist.ToString
            root.AppendChild(child)

            child = doc.CreateElement("autoepisodescreenshot")
            child.InnerText = Form1.userprefs.autoepisodescreenshot.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("ignorearticle")
            child.InnerText = Form1.userprefs.ignorearticle.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("TVShowUseXBMCScraper")
            child.InnerText = Form1.userprefs.tvshow_useXBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesUseXBMCScraper")
            child.InnerText = Form1.userprefs.movies_useXBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("whatXBMCScraper")
            child.InnerText = Form1.userprefs.XBMC_Scraper.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("startuptab")
            child.InnerText = Form1.userprefs.startuptab.ToString
            root.AppendChild(child)

            child = doc.CreateElement("intruntime")
            child.InnerText = Form1.userprefs.intruntime.ToString.ToLower
            root.AppendChild(child)

            If Form1.userprefs.font <> Nothing Then
                If Form1.userprefs.font <> "" Then
                    child = doc.CreateElement("font")
                    child.InnerText = Form1.userprefs.font
                    root.AppendChild(child)
                End If
            End If

            child = doc.CreateElement("autorenameepisodes")
            child.InnerText = Form1.userprefs.autorenameepisodes.ToString.ToLower
            root.AppendChild(child)

            child = doc.CreateElement("moviesets")
            Dim childchild As XmlElement
            For Each movieset In Form1.userprefs.moviesets
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
            childchild2.InnerText = Form1.userprefs.tablesortorder
            child.AppendChild(childchild2)
            For Each tabs In Form1.userprefs.tableview
                childchild2 = doc.CreateElement("tab")
                childchild2.InnerText = tabs
                child.AppendChild(childchild2)
            Next
            root.AppendChild(child)



            For Each com In Form1.userprefs.commandlist
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
            Dim tempstring2 As String = Form1.workingprofile.config
            Dim output As New XmlTextWriter(Form1.workingprofile.config, System.Text.Encoding.UTF8)
            output.Formatting = Formatting.Indented
            doc.WriteTo(output)
            output.Close()
        Catch

        Finally
            Monitor.Exit(Me)
        End Try


    End Sub

    Public Sub loadconfig()
        Form1.userprefs.commandlist.Clear()
        Form1.userprefs.moviesets.Clear()
        Form1.userprefs.moviesets.Add("None")
        Form1.moviefolders.Clear()
        Form1.tvfolders.Clear()
        Form1.tvrootfolders.Clear()
        Form1.userprefs.tableview.Clear()
        Dim tempstring As String = Form1.workingprofile.config
        If Not IO.File.Exists(Form1.workingprofile.config) Then
            Exit Sub
        End If

        Dim prefs As New XmlDocument
        Try
            prefs.Load(Form1.workingprofile.config)
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
                                Form1.userprefs.moviesets.Add(thisset.InnerText)
                        End Select
                    Next
                Case "table"
                    Dim thistable As XmlNode = Nothing
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "tab"
                                Form1.userprefs.tableview.Add(thistable.InnerText)
                            Case "sort"
                                Form1.userprefs.tablesortorder = thistable.InnerText
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
                        Form1.userprefs.commandlist.Add(newcom)
                    End If
                Case "seasonall"
                    Form1.userprefs.seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    Form1.userprefs.splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    Form1.userprefs.splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    Form1.userprefs.splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    Form1.userprefs.splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    Form1.userprefs.splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        Form1.userprefs.maximised = True
                    Else
                        Form1.userprefs.maximised = False
                    End If
                Case "locx"
                    Form1.userprefs.locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    Form1.userprefs.locy = Convert.ToInt32(thisresult.InnerText)
                Case "nfofolder"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.moviefolders.Add(decodestring)
                Case "offlinefolder"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.userprefs.offlinefolders.Add(decodestring)
                Case "tvfolder"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.tvfolders.Add(decodestring)
                Case "tvrootfolder"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.tvrootfolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.gettrailer = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.tvshowautoquick = False
                    End If
                Case "intruntime"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.intruntime = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.intruntime = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.startupcache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.startupcache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            Form1.userprefs.font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    Form1.userprefs.maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    Form1.userprefs.maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.enablehdtags = False
                    End If

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.renamenfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.renamenfofiles = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.disablelogfiles = False
                    End If

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.dontdisplayposter = False
                    End If

                Case "rarsize"
                    Form1.userprefs.rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.copytvactorthumbs = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.userprefs.actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = Form1.nfofunction.decxmlchars(thisresult.InnerText)
                    Form1.userprefs.actornetworkpath = decodestring

                Case "resizefanart"
                    Form1.userprefs.resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    Form1.userprefs.defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    Form1.userprefs.imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim Form1.userprefs.moviethumbpriority(3)
                    Form1.userprefs.moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim Form1.userprefs.certificatepriority(33)
                    Form1.userprefs.certificatepriority = thisresult.InnerXml.Split("|")

                Case "backgroundcolour"
                    Form1.userprefs.backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    Form1.userprefs.forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.remembersize = False
                    End If

                Case "formheight"
                    Form1.userprefs.formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    Form1.userprefs.formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    Form1.userprefs.videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.usefoldernames = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    Form1.userprefs.startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    Form1.userprefs.namemode = thisresult.InnerXml

                Case "tvdblanguage"
                    Dim partone() As String
                    partone = thisresult.InnerXml.Split("|")
                    For f = 0 To 1
                        If partone(0).Length = 2 Then
                            Form1.userprefs.tvdblanguagecode = partone(0)
                            Form1.userprefs.tvdblanguage = partone(1)
                            Exit For
                        Else
                            Form1.userprefs.tvdblanguagecode = partone(1)
                            Form1.userprefs.tvdblanguage = partone(0)
                        End If
                    Next

                Case "tvdbmode"
                    Form1.userprefs.sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    Form1.userprefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.usetransparency = False
                    End If

                Case "transparencyvalue"
                    Form1.userprefs.transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.roundminutes = False
                    End If

                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.autoepisodescreenshot = False
                    End If

                Case "ignorearticle"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.ignorearticle = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.ignorearticle = False
                    End If

                Case "TVShowUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.tvshow_useXBMC_Scraper = True
                        Form1.GroupBox22.Visible = False
                        Form1.GroupBox22.SendToBack()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = True
                        Form1.GroupBox_TVDB_Scraper_Preferences.BringToFront()
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.tvshow_useXBMC_Scraper = False
                        Form1.GroupBox22.Visible = True
                        Form1.GroupBox22.BringToFront()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = False
                        Form1.GroupBox_TVDB_Scraper_Preferences.SendToBack()
                    End If
                    Read_XBMC_TVDB_Scraper_Config()

                Case "moviesUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.movies_useXBMC_Scraper = True
                        Form1.RadioButton51.Visible = True
                        Form1.RadioButton52.Visible = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.movies_useXBMC_Scraper = False
                        Form1.RadioButton51.Visible = False
                        Form1.RadioButton52.Visible = False
                    End If


                Case "whatXBMCScraper"
                    Form1.userprefs.XBMC_Scraper = thisresult.InnerXml
                    If thisresult.InnerXml = "imdb" Then
                        Form1.RadioButton51.Checked = True
                        Read_XBMC_IMDB_Scraper_Config()
                    ElseIf thisresult.InnerXml = "tmdb" Then
                        Form1.RadioButton52.Checked = True
                        Read_XBMC_TMDB_Scraper_Config()
                    End If


                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    Form1.userprefs.maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    Form1.userprefs.startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.disabletvlogs = False
                    End If

                Case "overwritethumb"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.overwritethumbs = False
                    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.savefanart = False
                    End If

                Case "postertype"
                    Form1.userprefs.postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    Form1.userprefs.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    Form1.userprefs.videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    Form1.userprefs.selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    Form1.userprefs.maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    Form1.userprefs.lastpath = thisresult.InnerXml

                Case "moviescraper"
                    Form1.userprefs.moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    Form1.userprefs.nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.externalbrowser = False
                    End If
                Case "tvrename"
                    Form1.userprefs.tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrebuildlog"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.tvshowrebuildlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.tvshowrebuildlog = False
                    End If
                    'Public moviesortorder As Byte
                    'Public moviedefaultlist As Byte
                    'Public lasttab As Byte
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.autorenameepisodes = False
                    End If
                Case "eprenamelowercase"
                    If thisresult.InnerXml = "true" Then
                        Form1.userprefs.eprenamelowercase = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Form1.userprefs.eprenamelowercase = False
                    End If
                Case "moviesortorder"
                    Form1.userprefs.moviesortorder = Convert.ToByte(thisresult.InnerText)
                Case "moviedefaultlist"
                    Form1.userprefs.moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    Form1.userprefs.startuptab = Convert.ToByte(thisresult.InnerText)
            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
        Next

    End Sub

    

   
End Class
