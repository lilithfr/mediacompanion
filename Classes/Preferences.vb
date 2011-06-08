Imports System.IO
Imports System.Xml
Imports System.Threading


Public Class Preferences
    Public Shared customcounter As Integer
    Public Shared tvrename As Integer
    Public Shared locx As Integer
    Public Shared locy As Integer
    Public Shared maxactors As Integer
    Public Shared maxmoviegenre As Integer
    Public Shared rarsize As Integer
    Public Shared splt1 As Integer
    Public Shared splt2 As Integer
    Public Shared splt3 As Integer
    Public Shared splt4 As Integer
    Public Shared splt5 As Integer
    Public Shared resizefanart As Integer
    Public Shared formheight As Integer
    Public Shared formwidth As Integer
    Public Shared videoplaybackmode As Integer
    Public Shared startupdisplaynamemode As Integer
    Public Shared tvdbactorscrape As Integer
    Public Shared transparencyvalue As Integer
    Public Shared maximumthumbs As Integer
    Public Shared startupmode As Integer
    Public Shared videomode As Integer ' = 3
    Public Shared maximagecount As Integer
    Public Shared moviescraper As Integer
    Public Shared nfoposterscraper As Integer

    'Dim tvdbmode As String
    Public Shared XBMC_Scraper As String = ""
    Public Shared font As String
    Public Shared moviethumbpriority() As String
    Public Shared certificatepriority() As String
    Public Shared actorsavepath As String
    Public Shared actornetworkpath As String
    Public Shared defaulttvthumb As String
    Public Shared imdbmirror As String
    Public Shared backgroundcolour As String
    Public Shared forgroundcolour As String
    Public Shared namemode As String
    Public Shared tvdblanguage As String
    Public Shared tvdblanguagecode As String
    Public Shared configpath As String
    Public Shared postertype As String
    Public Shared sortorder As String
    Public Shared selectedvideoplayer As String
    Public Shared lastpath As String
    Public Shared episodeacrorsource As String
    Public Shared seasonall As String
    Public Shared tablesortorder As String

    Public Shared intruntime As Boolean
    Public Shared autorenameepisodes As Boolean
    Public Shared autoepisodescreenshot As Boolean
    Public Shared ignorearticle As Boolean
    Public Shared tvshow_useXBMC_Scraper As Boolean
    Public Shared movies_useXBMC_Scraper As Boolean
    Public Shared eprenamelowercase As Boolean
    Public Shared tvshowautoquick As Boolean
    Public Shared tvshowrebuildlog As Boolean
    Public Shared roundminutes As Boolean
    Public Shared keepfoldername As Boolean
    Public Shared startupCache As Boolean
    Public Shared ignoretrailers As Boolean
    Public Shared ignoreactorthumbs As Boolean
    Public Shared enabletvhdtags As Boolean
    Public Shared enablehdtags As Boolean
    Public Shared renamenfofiles As Boolean
    Public Shared checkinfofiles As Boolean
    Public Shared disablelogfiles As Boolean
    Public Shared fanartnotstacked As Boolean
    Public Shared posternotstacked As Boolean
    Public Shared scrapemovieposters As Boolean
    Public Shared usefanart As Boolean
    Public Shared dontdisplayposter As Boolean
    Public Shared actorsave As Boolean
    Public Shared overwritethumbs As Boolean
    Public Shared remembersize As Boolean
    Public Shared usefoldernames As Boolean
    Public Shared createfolderjpg As Boolean
    Public Shared basicsavemode As Boolean
    Public Shared usetransparency As Boolean
    Public Shared downloadtvseasonthumbs As Boolean
    Public Shared disabletvlogs As Boolean
    Public Shared savefanart As Boolean
    Public Shared tvposter As Boolean
    Public Shared tvfanart As Boolean
    Public Shared alwaysuseimdbid As Boolean
    Public Shared gettrailer As Boolean
    Public Shared externalbrowser As Boolean
    Public Shared maximised As Boolean
    Public Shared copytvactorthumbs As Boolean = False
    Public Shared actorseasy As Boolean
    Public Shared scrapefullcert As Boolean

    Public Shared moviesortorder As Byte
    Public Shared moviedefaultlist As Byte
    Public Shared startuptab As Byte

    Public Shared moviesets As New List(Of String)
    Public Shared tableview As New List(Of String)
    Public Shared offlinefolders As New List(Of String)

    Public Shared commandlist As New List(Of ListOfCommands)

    Public Shared Sub SaveConfig()

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
        For Each path In Preferences.offlinefolders
            If Not list.Contains(path) Then
                child = doc.CreateElement("offlinefolder")
                child.InnerText = path
                root.AppendChild(child)
                list.Add(path)
            End If
        Next


        child = doc.CreateElement("gettrailer")
        child.InnerText = Preferences.gettrailer.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("keepfoldername")
        child.InnerText = Preferences.keepfoldername.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startupcache")
        child.InnerText = Preferences.startupCache.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("ignoretrailers")
        child.InnerText = Preferences.ignoretrailers.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviescraper")
        child.InnerText = Preferences.moviescraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("nfoposterscraper")
        child.InnerText = Preferences.nfoposterscraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("alwaysuseimdbid")
        child.InnerText = Preferences.alwaysuseimdbid.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignoreactorthumbs")
        child.InnerText = Preferences.ignoreactorthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maxactors")
        child.InnerText = Preferences.maxactors.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("maxmoviegenre")
        child.InnerText = Preferences.maxmoviegenre.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("enablehdtags")
        child.InnerText = Preferences.enablehdtags.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("renamenfofiles")
        child.InnerText = Preferences.renamenfofiles.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("checkinfofiles")
        child.InnerText = Preferences.checkinfofiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowautoquick")
        child.InnerText = Preferences.tvshowautoquick.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disablelogfiles")
        child.InnerText = Preferences.disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("actorseasy")
        child.InnerText = Preferences.actorseasy.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("copytvactorthumbs")
        child.InnerText = Preferences.copytvactorthumbs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("fanartnotstacked")
        child.InnerText = Preferences.fanartnotstacked.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("posternotstacked")
        child.InnerText = Preferences.posternotstacked.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("lastpath")
        child.InnerText = Preferences.lastpath
        root.AppendChild(child)

        child = doc.CreateElement("downloadfanart")
        child.InnerText = Preferences.savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("scrapemovieposters")
        child.InnerText = Preferences.scrapemovieposters.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefanart")
        child.InnerText = Preferences.usefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("dontdisplayposter")
        child.InnerText = Preferences.dontdisplayposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("usefoldernames")
        child.InnerText = Preferences.usefoldernames.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("rarsize")
        child.InnerText = Preferences.rarsize.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsave")
        child.InnerText = Preferences.actorsave.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("actorsavepath")
        child.InnerText = Preferences.actorsavepath
        root.AppendChild(child)


        child = doc.CreateElement("actornetworkpath")
        child.InnerText = Preferences.actornetworkpath
        root.AppendChild(child)


        child = doc.CreateElement("resizefanart")
        child.InnerText = Preferences.resizefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("overwritethumbs")
        child.InnerText = Preferences.overwritethumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("defaulttvthumb")
        child.InnerText = Preferences.defaulttvthumb
        root.AppendChild(child)


        child = doc.CreateElement("imdbmirror")
        child.InnerText = Preferences.imdbmirror
        root.AppendChild(child)


        child = doc.CreateElement("backgroundcolour")
        child.InnerText = Preferences.backgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("forgroundcolour")
        child.InnerText = Preferences.forgroundcolour
        root.AppendChild(child)


        child = doc.CreateElement("remembersize")
        child.InnerText = Preferences.remembersize.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("locx")
        child.InnerText = Preferences.locx.ToString
        root.AppendChild(child)

        child = doc.CreateElement("locy")
        child.InnerText = Preferences.locy.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formheight")
        child.InnerText = Preferences.formheight.ToString
        root.AppendChild(child)


        child = doc.CreateElement("formwidth")
        child.InnerText = Preferences.formwidth.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videoplaybackmode")
        child.InnerText = Preferences.videoplaybackmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("createfolderjpg")
        child.InnerText = Preferences.createfolderjpg.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("basicsavemode")
        child.InnerText = Preferences.basicsavemode.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("startupdisplaynamemode")
        child.InnerText = Preferences.startupdisplaynamemode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("namemode")
        child.InnerText = Preferences.namemode
        root.AppendChild(child)


        child = doc.CreateElement("tvdbmode")
        child.InnerText = Preferences.sortorder
        root.AppendChild(child)


        child = doc.CreateElement("tvdbactorscrape")
        child.InnerText = Preferences.tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("usetransparency")
        child.InnerText = Preferences.usetransparency.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("transparencyvalue")
        child.InnerText = Preferences.transparencyvalue.ToString
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvfanart")
        child.InnerText = Preferences.tvfanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvposter")
        child.InnerText = Preferences.tvposter.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("keepfoldername")
        child.InnerText = Preferences.keepfoldername.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("downloadtvseasonthumbs")
        child.InnerText = Preferences.downloadtvseasonthumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("maximumthumbs")
        child.InnerText = Preferences.maximumthumbs.ToString
        root.AppendChild(child)


        child = doc.CreateElement("startupmode")
        child.InnerText = Preferences.startupmode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("hdtvtags")
        child.InnerText = Preferences.enabletvhdtags.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("disablelogs")
        child.InnerText = Preferences.disablelogfiles.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("disabletvlogs")
        child.InnerText = Preferences.disabletvlogs.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("overwritethumb")
        child.InnerText = Preferences.overwritethumbs.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("savefanart")
        child.InnerText = Preferences.savefanart.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("postertype")
        child.InnerText = Preferences.postertype
        root.AppendChild(child)

        child = doc.CreateElement("roundminutes")
        child.InnerText = Preferences.roundminutes.ToString.ToLower
        root.AppendChild(child)


        child = doc.CreateElement("tvactorscrape")
        child.InnerText = Preferences.tvdbactorscrape.ToString
        root.AppendChild(child)


        child = doc.CreateElement("videomode")
        child.InnerText = Preferences.videomode.ToString
        root.AppendChild(child)


        child = doc.CreateElement("selectedvideoplayer")
        child.InnerText = Preferences.selectedvideoplayer
        root.AppendChild(child)

        child = doc.CreateElement("externalbrowser")
        child.InnerText = Preferences.externalbrowser.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviethumbpriority")
        Dim tempstring As String
        tempstring = Preferences.moviethumbpriority(0) & "|" & Preferences.moviethumbpriority(1) & "|" & Preferences.moviethumbpriority(2) & "|" & Preferences.moviethumbpriority(3)
        child.InnerText = tempstring
        root.AppendChild(child)


        child = doc.CreateElement("tvdblanguage")
        tempstring = ""
        tempstring = Preferences.tvdblanguagecode & "|" & Preferences.tvdblanguage
        child.InnerText = tempstring
        root.AppendChild(child)



        child = doc.CreateElement("certificatepriority")
        tempstring = ""
        For f = 0 To 32
            tempstring = tempstring & Preferences.certificatepriority(f) & "|"
        Next
        tempstring = tempstring & Preferences.certificatepriority(33)
        child.InnerText = tempstring
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer1")
        child.InnerText = Preferences.splt1.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer2")
        child.InnerText = Preferences.splt2.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer3")
        child.InnerText = Preferences.splt3.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer4")
        child.InnerText = Preferences.splt4.ToString
        root.AppendChild(child)

        child = doc.CreateElement("splitcontainer5")
        child.InnerText = Preferences.splt5.ToString
        root.AppendChild(child)


        child = doc.CreateElement("seasonall")
        child.InnerText = Preferences.seasonall
        root.AppendChild(child)

        child = doc.CreateElement("maximised")
        If Preferences.maximised = True Then
            child.InnerText = "true"
        Else
            child.InnerText = "false"
        End If
        root.AppendChild(child)

        child = doc.CreateElement("tvrename")
        child.InnerText = Preferences.tvrename.ToString
        root.AppendChild(child)

        child = doc.CreateElement("eprenamelowercase")
        child.InnerText = Preferences.eprenamelowercase.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("tvshowrebuildlog")
        child.InnerText = Preferences.tvshowrebuildlog.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesortorder")
        child.InnerText = Preferences.moviesortorder.ToString
        root.AppendChild(child)

        child = doc.CreateElement("moviedefaultlist")
        child.InnerText = Preferences.moviedefaultlist.ToString
        root.AppendChild(child)

        child = doc.CreateElement("autoepisodescreenshot")
        child.InnerText = Preferences.autoepisodescreenshot.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("ignorearticle")
        child.InnerText = Preferences.ignorearticle.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("TVShowUseXBMCScraper")
        child.InnerText = Preferences.tvshow_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesUseXBMCScraper")
        child.InnerText = Preferences.movies_useXBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("whatXBMCScraper")
        child.InnerText = Preferences.XBMC_Scraper.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("startuptab")
        child.InnerText = Preferences.startuptab.ToString
        root.AppendChild(child)

        child = doc.CreateElement("intruntime")
        child.InnerText = Preferences.intruntime.ToString.ToLower
        root.AppendChild(child)

        If Preferences.font <> Nothing Then
            If Preferences.font <> "" Then
                child = doc.CreateElement("font")
                child.InnerText = Preferences.font
                root.AppendChild(child)
            End If
        End If

        child = doc.CreateElement("autorenameepisodes")
        child.InnerText = Preferences.autorenameepisodes.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("scrapefullcert")
        child.InnerText = Preferences.scrapefullcert.ToString.ToLower
        root.AppendChild(child)

        child = doc.CreateElement("moviesets")
        Dim childchild As XmlElement
        For Each movieset In Preferences.moviesets
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
        childchild2.InnerText = Preferences.tablesortorder
        child.AppendChild(childchild2)
        For Each tabs In Preferences.tableview
            childchild2 = doc.CreateElement("tab")
            childchild2.InnerText = tabs
            child.AppendChild(childchild2)
        Next
        root.AppendChild(child)

        child = doc.CreateElement("offlinemovielabeltext")
        child.InnerText = Form1.TextBox_OfflineDVDTitle.Text
        root.AppendChild(child)


        For Each com In Preferences.commandlist
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
    End Sub

    Public Shared Sub LoadConfig()
        Preferences.commandlist.Clear()
        Preferences.moviesets.Clear()
        Preferences.moviesets.Add("None")
        Form1.movieFolders.Clear()
        Form1.tvFolders.Clear()
        Form1.tvRootFolders.Clear()
        Preferences.tableview.Clear()
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
                                Preferences.moviesets.Add(thisset.InnerText)
                        End Select
                    Next
                Case "table"
                    Dim thistable As XmlNode = Nothing
                    For Each thistable In thisresult.ChildNodes
                        Select Case thistable.Name
                            Case "tab"
                                Preferences.tableview.Add(thistable.InnerText)
                            Case "sort"
                                Preferences.tablesortorder = thistable.InnerText
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
                        Preferences.commandlist.Add(newcom)
                    End If
                Case "seasonall"
                    Preferences.seasonall = thisresult.InnerText
                Case "splitcontainer1"
                    Preferences.splt1 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer2"
                    Preferences.splt2 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer3"
                    Preferences.splt3 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer4"
                    Preferences.splt4 = Convert.ToInt32(thisresult.InnerText)
                Case "splitcontainer5"
                    Preferences.splt5 = Convert.ToInt32(thisresult.InnerText)
                Case "maximised"
                    If thisresult.InnerText = "true" Then
                        Preferences.maximised = True
                    Else
                        Preferences.maximised = False
                    End If
                Case "locx"
                    Preferences.locx = Convert.ToInt32(thisresult.InnerText)
                Case "locy"
                    Preferences.locy = Convert.ToInt32(thisresult.InnerText)
                Case "nfofolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.movieFolders.Add(decodestring)
                Case "offlinefolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Preferences.offlinefolders.Add(decodestring)
                Case "tvfolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.tvFolders.Add(decodestring)
                Case "tvrootfolder"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Form1.tvRootFolders.Add(decodestring)
                Case "gettrailer"
                    If thisresult.InnerXml = "true" Then
                        Preferences.gettrailer = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.gettrailer = False
                    End If
                Case "tvshowautoquick"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshowautoquick = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshowautoquick = False
                    End If
                Case "intruntime"
                    If thisresult.InnerXml = "true" Then
                        Preferences.intruntime = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.intruntime = False
                    End If
                Case "keepfoldername"
                    If thisresult.InnerXml = "true" Then
                        Preferences.keepfoldername = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.keepfoldername = False
                    End If

                Case "startupcache"
                    If thisresult.InnerXml = "true" Then
                        Preferences.startupCache = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.startupCache = False
                    End If

                Case "ignoretrailers"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignoretrailers = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignoretrailers = False
                    End If

                Case "ignoreactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignoreactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignoreactorthumbs = False
                    End If

                Case "font"
                    If thisresult.InnerXml <> Nothing Then
                        If thisresult.InnerXml <> "" Then
                            Preferences.font = thisresult.InnerXml
                        End If
                    End If

                Case "maxactors"
                    Preferences.maxactors = Convert.ToInt32(thisresult.InnerXml)

                Case "maxmoviegenre"
                    Preferences.maxmoviegenre = Convert.ToInt32(thisresult.InnerXml)

                Case "enablehdtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enablehdtags = False
                    End If

                Case "hdtvtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enabletvhdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enabletvhdtags = False
                    End If

                Case "renamenfofiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.renamenfofiles = True
                        Form1.CheckBoxRenameNFOtoINFO.Checked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.renamenfofiles = False
                        Form1.CheckBoxRenameNFOtoINFO.Checked = False
                    End If

                Case "checkinfofiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.checkinfofiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.checkinfofiles = False
                    End If

                Case "disablelogfiles"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disablelogfiles = False
                    End If

                Case "fanartnotstacked"
                    If thisresult.InnerXml = "true" Then
                        Preferences.fanartnotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.fanartnotstacked = False
                    End If

                Case "posternotstacked"
                    If thisresult.InnerXml = "true" Then
                        Preferences.posternotstacked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.posternotstacked = False
                    End If

                Case "downloadfanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.savefanart = False
                    End If

                Case "scrapemovieposters"
                    If thisresult.InnerXml = "true" Then
                        Preferences.scrapemovieposters = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.scrapemovieposters = False
                    End If

                Case "usefanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usefanart = False
                    End If

                Case "dontdisplayposter"
                    If thisresult.InnerXml = "true" Then
                        Preferences.dontdisplayposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.dontdisplayposter = False
                    End If

                Case "rarsize"
                    Preferences.rarsize = Convert.ToInt32(thisresult.InnerXml)

                Case "actorsave"
                    If thisresult.InnerXml = "true" Then
                        Preferences.actorsave = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.actorsave = False
                    End If

                Case "actorseasy"
                    If thisresult.InnerXml = "true" Then
                        Preferences.actorseasy = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.actorseasy = False
                    End If

                Case "copytvactorthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.copytvactorthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.copytvactorthumbs = False
                    End If

                Case "actorsavepath"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Preferences.actorsavepath = decodestring

                Case "actornetworkpath"
                    Dim decodestring As String = Form1.nfoFunction.decxmlchars(thisresult.InnerText)
                    Preferences.actornetworkpath = decodestring

                Case "resizefanart"
                    Preferences.resizefanart = Convert.ToInt32(thisresult.InnerXml)

                Case "overwritethumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.overwritethumbs = False
                    End If

                Case "defaulttvthumb"
                    Preferences.defaulttvthumb = thisresult.InnerXml

                Case "imdbmirror"
                    Preferences.imdbmirror = thisresult.InnerXml

                Case "moviethumbpriority"
                    ReDim Preferences.moviethumbpriority(3)
                    Preferences.moviethumbpriority = thisresult.InnerXml.Split("|")

                Case "certificatepriority"
                    ReDim Preferences.certificatepriority(33)
                    Preferences.certificatepriority = thisresult.InnerXml.Split("|")

                Case "backgroundcolour"
                    Preferences.backgroundcolour = thisresult.InnerXml

                Case "forgroundcolour"
                    Preferences.forgroundcolour = thisresult.InnerXml

                Case "remembersize"
                    If thisresult.InnerXml = "true" Then
                        Preferences.remembersize = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.remembersize = False
                    End If

                Case "formheight"
                    Preferences.formheight = Convert.ToInt32(thisresult.InnerXml)

                Case "formwidth"
                    Preferences.formwidth = Convert.ToInt32(thisresult.InnerXml)
                Case "videoplaybackmode"
                    Preferences.videoplaybackmode = Convert.ToInt32(thisresult.InnerXml)

                Case "usefoldernames"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usefoldernames = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usefoldernames = False
                    End If

                Case "createfolderjpg"
                    If thisresult.InnerXml = "true" Then
                        Preferences.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.createfolderjpg = False
                    End If

                Case "basicsavemode"
                    If thisresult.InnerXml = "true" Then
                        Preferences.basicsavemode = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.basicsavemode = False
                    End If

                Case "startupdisplaynamemode"
                    Preferences.startupdisplaynamemode = Convert.ToInt32(thisresult.InnerXml)

                Case "namemode"
                    Preferences.namemode = thisresult.InnerXml

                Case "tvdblanguage"
                    Dim partone() As String
                    partone = thisresult.InnerXml.Split("|")
                    For f = 0 To 1
                        If partone(0).Length = 2 Then
                            Preferences.tvdblanguagecode = partone(0)
                            Preferences.tvdblanguage = partone(1)
                            Exit For
                        Else
                            Preferences.tvdblanguagecode = partone(1)
                            Preferences.tvdblanguage = partone(0)
                        End If
                    Next

                Case "tvdbmode"
                    Preferences.sortorder = thisresult.InnerXml
                Case "tvdbactorscrape"
                    Preferences.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "usetransparency"
                    If thisresult.InnerXml = "true" Then
                        Preferences.usetransparency = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.usetransparency = False
                    End If

                Case "transparencyvalue"
                    Preferences.transparencyvalue = Convert.ToInt32(thisresult.InnerXml)

                Case "downloadtvfanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvfanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvfanart = False
                    End If

                Case "roundminutes"
                    If thisresult.InnerXml = "true" Then
                        Preferences.roundminutes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.roundminutes = False
                    End If

                Case "autoepisodescreenshot"
                    If thisresult.InnerXml = "true" Then
                        Preferences.autoepisodescreenshot = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.autoepisodescreenshot = False
                    End If

                Case "ignorearticle"
                    If thisresult.InnerXml = "true" Then
                        Preferences.ignorearticle = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.ignorearticle = False
                    End If

                Case "TVShowUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshow_useXBMC_Scraper = True
                        Form1.GroupBox22.Visible = False
                        Form1.GroupBox22.SendToBack()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = True
                        Form1.GroupBox_TVDB_Scraper_Preferences.BringToFront()
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshow_useXBMC_Scraper = False
                        Form1.GroupBox22.Visible = True
                        Form1.GroupBox22.BringToFront()
                        Form1.GroupBox_TVDB_Scraper_Preferences.Visible = False
                        Form1.GroupBox_TVDB_Scraper_Preferences.SendToBack()
                    End If
                    Read_XBMC_TVDB_Scraper_Config()

                Case "moviesUseXBMCScraper"
                    If thisresult.InnerXml = "true" Then
                        Preferences.movies_useXBMC_Scraper = True
                        Form1.RadioButton51.Visible = True
                        Form1.RadioButton52.Visible = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.movies_useXBMC_Scraper = False
                        Form1.RadioButton51.Visible = False
                        Form1.RadioButton52.Visible = False
                    End If


                Case "whatXBMCScraper"
                    Preferences.XBMC_Scraper = thisresult.InnerXml
                    If thisresult.InnerXml = "imdb" Then
                        Form1.RadioButton51.Checked = True
                        Read_XBMC_IMDB_Scraper_Config()
                    ElseIf thisresult.InnerXml = "tmdb" Then
                        Form1.RadioButton52.Checked = True
                        Read_XBMC_TMDB_Scraper_Config()
                    End If


                Case "downloadtvposter"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvposter = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvposter = False
                    End If

                Case "downloadtvseasonthumbs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.downloadtvseasonthumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.downloadtvseasonthumbs = False
                    End If

                Case "maximumthumbs"
                    Preferences.maximumthumbs = Convert.ToInt32(thisresult.InnerXml)

                Case "startupmode"
                    Preferences.startupmode = Convert.ToInt32(thisresult.InnerXml)

                Case "hdtags"
                    If thisresult.InnerXml = "true" Then
                        Preferences.enablehdtags = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.enablehdtags = False
                    End If

                Case "disablelogs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disablelogfiles = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disablelogfiles = False
                    End If

                Case "disabletvlogs"
                    If thisresult.InnerXml = "true" Then
                        Preferences.disabletvlogs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.disabletvlogs = False
                    End If

                Case "overwritethumb"
                    If thisresult.InnerXml = "true" Then
                        Preferences.overwritethumbs = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.overwritethumbs = False
                    End If

                Case "folderjpg"
                    If thisresult.InnerXml = "true" Then
                        Preferences.createfolderjpg = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.createfolderjpg = False
                    End If

                Case "savefanart"
                    If thisresult.InnerXml = "true" Then
                        Preferences.savefanart = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.savefanart = False
                    End If

                Case "postertype"
                    Preferences.postertype = thisresult.InnerXml

                Case "tvactorscrape"
                    Preferences.tvdbactorscrape = Convert.ToInt32(thisresult.InnerXml)

                Case "videomode"
                    Preferences.videomode = Convert.ToInt32(thisresult.InnerXml)

                Case "selectedvideoplayer"
                    Preferences.selectedvideoplayer = thisresult.InnerXml

                Case "maximagecount"
                    Preferences.maximagecount = Convert.ToInt32(thisresult.InnerXml)

                Case "lastpath"
                    Preferences.lastpath = thisresult.InnerXml

                Case "moviescraper"
                    Preferences.moviescraper = thisresult.InnerXml

                Case "nfoposterscraper"
                    Preferences.nfoposterscraper = thisresult.InnerXml

                Case "alwaysuseimdbid"
                    If thisresult.InnerXml = "true" Then
                        Preferences.alwaysuseimdbid = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.alwaysuseimdbid = False
                    End If

                Case "externalbrowser"
                    If thisresult.InnerXml = "true" Then
                        Preferences.externalbrowser = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.externalbrowser = False
                    End If
                Case "tvrename"
                    Preferences.tvrename = Convert.ToInt32(thisresult.InnerText)
                Case "tvshowrebuildlog"
                    If thisresult.InnerXml = "true" Then
                        Preferences.tvshowrebuildlog = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.tvshowrebuildlog = False
                    End If
                    'public shared moviesortorder As Byte
                    'public shared moviedefaultlist As Byte
                    'public shared lasttab As Byte
                Case "autorenameepisodes"
                    If thisresult.InnerXml = "true" Then
                        Preferences.autorenameepisodes = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.autorenameepisodes = False
                    End If
                Case "eprenamelowercase"
                    If thisresult.InnerXml = "true" Then
                        Preferences.eprenamelowercase = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.eprenamelowercase = False
                    End If
                Case "moviesortorder"
                    Preferences.moviesortorder = Convert.ToByte(thisresult.InnerText)
                Case "moviedefaultlist"
                    Preferences.moviedefaultlist = Convert.ToByte(thisresult.InnerText)
                Case "startuptab"
                    Preferences.startuptab = Convert.ToByte(thisresult.InnerText)

                Case "offlinemovielabeltext"
                    Form1.TextBox_OfflineDVDTitle.Text = thisresult.InnerText


                Case "scrapefullcert"
                    If thisresult.InnerXml = "true" Then
                        Preferences.scrapefullcert = True
                        Form1.ScrapeFullCertCheckBox.Checked = True
                    ElseIf thisresult.InnerXml = "false" Then
                        Preferences.scrapefullcert = False
                    End If
            End Select
            'Catch
            '    'MsgBox("Error : pr278")
            'End Try
        Next

    End Sub




End Class
