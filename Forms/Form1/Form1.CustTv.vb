Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports Media_Companion.WorkingWithNfoFiles
Imports System.Xml
Imports Media_Companion
Imports Media_Companion.Pref
Imports System.Linq

Partial Public Class Form1
    Dim CTvLoaded As Boolean = False

    Sub CustSearch()

    End Sub

    Sub CustRefresh()
        Custtv_CacheRefresh()
    End Sub

    Sub CustSave()

    End Sub
    
    Sub CustTvArtSetup()

    End Sub

    Sub CustTvFoldersSetup()

    End Sub

    Private Sub CustTvTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CustTvTabControl.SelectedIndexChanged
        Try
            Dim Show As Media_Companion.TvShow = tv_ShowSelectedCurrently()
            Dim WorkingEpisode As TvEpisode = ep_SelectedCurrently()
            Dim tab As String = CustTvTabControl.SelectedTab.Name
            If Show Is Nothing Then
                MsgBox("No TV Show is selected")
                Exit Sub
            End If
            
            If tab <> TpCustTvBrowser.Name AndAlso tab <> TpCustTvArt.Name And tab <> TpCustTvFolders.Name Then
                If Show.NfoFilePath = "" And custtvFolders.Count = 0 Then
                    Me.CustTvTabControl.SelectedIndex = tvCurrentTabIndex
                    MsgBox("There are no Shows in your list to work on" & vbCrLf & "Add a Custom Folder using the Folders Tab")
                ElseIf Show.NfoFilePath = "" And tvFolders.Count > 0 Then
                    Me.CustTvTabControl.SelectedIndex = CustTvIndex
                    If Cache.CustTvCache.Shows.Count > 0 Then
                        MsgBox("No Show is selected")
                        Exit Sub
                    Else
                        MsgBox("There are no Shows in your list to work on")
                        Exit Sub
                    End If
                End If
            ElseIf tab = TpCustTvArt.Name Then
                Call CustTvArtSetup()
            ElseIf tab= TpCustTvFolders.Name Then
                CustTvIndex = CustTvTabControl.SelectedIndex
                CustTvTabControl.SelectedIndex = CustTvIndex
                Call CustTvFoldersSetup()
            Else
                CustTvIndex = 0
                Exit Sub
            End If
           
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

#Region "CustTvCache Routines"

    Public Sub Custtv_CacheLoad()
        Cache.CustTvCache.TvCachePath = Pref.workingProfile.CustTvCache
        Cache.CustTvCache.Load()
        CTvTreeView.BeginUpdate()
        Try
            CTvTreeView.Nodes.Clear()              'clear the treeview of old data
            For Each TvShow As Media_Companion.TvShow In Cache.CustTvCache.Shows
                CTvTreeView.Nodes.Add(TvShow.ShowNode)
                TvShow.UpdateTreenode()
            Next
        Finally
            CTvTreeView.EndUpdate()
        End Try
        tbCShowCount.Text = Cache.CustTvCache.Shows.Count
        tbCEpCount  .Text = Cache.CustTvCache.Episodes.Count
    End Sub

    Public Function CustTv_CacheSave() As Boolean

        Cache.CustTvCache.TvCachePath = Pref.workingProfile.CustTvCache
        Cache.CustTvCache.Save()
        Return False
    End Function

    Private Sub CustTv_RefreshCacheSave(ByVal tvshowlist As List(Of TvShow), ByVal episodeelist As List(Of TvEpisode))
        Dim custtvcachepath As String = Pref.workingProfile.CustTvCache
        Dim document As New XmlDocument
        Dim root As XmlElement
        Dim child As XmlElement
        Dim childchild As XmlElement
        Dim xmlproc As XmlDeclaration
        xmlproc = document.CreateXmlDeclaration("1.0", "UTF-8", "yes")
        document.AppendChild(xmlproc)
        root = document.CreateElement("custtvcache")
        root.SetAttribute("ver", "3.5")
        For Each item In tvshowlist
            child = document.CreateElement("tvshow")
            child.SetAttribute("NfoPath", item.NfoFilePath)

            childchild = document.CreateElement("state")
            childchild.InnerText = item.State '"0"
            child.AppendChild(childchild)

            childchild = document.CreateElement("title")
            childchild.InnerText = item.Title.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("id")
            childchild.InnerText = item.TvdbId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("status")
            childchild.InnerText = item.Status.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("plot")
            childchild.InnerText = item.Plot.Value
            child.AppendChild(childchild)
            
            childchild = document.CreateElement("language")
            childchild.InnerText = item.Language.Value
            child.AppendChild(childchild)
            
            childchild = document.CreateElement("playcount")
            childchild.InnerText = item.Playcount.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("hidden")
            childchild.InnerText = item.Hidden.Value
            child.AppendChild(childchild)

            root.AppendChild(child)
        Next

        For Each item In episodeelist
            child = document.CreateElement("episodedetails")
            child.SetAttribute("NfoPath", item.NfoFilePath)
            
            childchild = document.CreateElement("title")
            childchild.InnerText = item.Title.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("season")
            childchild.InnerText = item.Season.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("episode")
            childchild.InnerText = item.Episode.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("aired")
            childchild.InnerText = item.Aired.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("showid")
            childchild.InnerText = item.ShowId.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("epextn")
            childchild.InnerText = item.EpExtn.Value
            child.AppendChild(childchild)

            childchild = document.CreateElement("playcount")
            childchild.InnerText = item.PlayCount.Value
            child.AppendChild(childchild)

            root.AppendChild(child)
        Next
        document.AppendChild(root)
        Dim output As New XmlTextWriter(custtvcachepath, System.Text.Encoding.UTF8)
        output.Formatting = Formatting.Indented
        document.WriteTo(output)
        output.Close()
    End Sub

    Private Sub Custtv_CacheRefresh(Optional ByVal TvShowSelected As TvShow = Nothing) 'refresh = clear & recreate cache from nfo's
        frmSplash2.Text = "Refresh Custom Shows..."
        frmSplash2.Label1.Text = "Searching Custom Folders....."
        frmSplash2.Label1.Visible = True
        frmSplash2.Label2.Visible = True
        frmSplash2.ProgressBar1.Visible = True
        frmSplash2.ProgressBar1.Maximum = Pref.custtvFolders.Count ' - 1
        frmSplash2.Show()
        Application.DoEvents()

        Dim fulltvshowlist As New List(Of TvShow)
        Dim fullepisodelist As New List(Of TvEpisode)

        tv_RefreshLog("Starting TV Show Refresh" & vbCrLf & vbCrLf, , True)
        tbCShowCount.Text = ""
        tbCEpCount  .Text = ""
        Me.Enabled = False

        Dim nofolder As New List(Of String)
        Dim prgCount As Integer = 0
        Dim FolderList As New List(Of String)

        If TvShowSelected IsNot Nothing Then ' if we have provided a tv show, then add just this show to the list, else scan through all of the folders
            FolderList.Add(TvShowSelected.FolderPath) 'add the single show to our list
            Cache.CustTvCache.Remove(TvShowSelected)
            For Each episode In TvShowSelected.Episodes
                Cache.CustTvCache.Remove(episode)
            Next
            For Each showitem In Cache.CustTvCache.Shows
                fulltvshowlist.Add(showitem)
            Next
            For Each episodeitem In Cache.CustTvCache.Episodes
                fullepisodelist.Add(episodeitem)
            Next
        Else
            FolderList = Pref.custtvFolders ' add all folders to list to scan
            Cache.CustTvCache.Clear() 'Full rescan means clear all old data
            CTvTreeView.Nodes.Clear()
            realTvPaths.Clear()
        End If

        For Each tvfolder In FolderList
            frmSplash2.Label2.Text = "(" & prgCount + 1 & "/" & Pref.custtvFolders.Count & ") " & tvfolder
            frmSplash2.ProgressBar1.Value = prgCount
            If Not Directory.Exists(tvfolder) OrElse Not File.Exists(tvfolder & "\tvshow.nfo") Then 
                nofolder.Add(tvfolder)
                Continue For
            End If
            prgCount += 1
            Application.DoEvents()
            Dim newtvshownfo As New TvShow
            newtvshownfo.NfoFilePath = IO.Path.Combine(tvfolder, "tvshow.nfo")
            newtvshownfo.Load() 
            fulltvshowlist.Add(newtvshownfo)
            Dim episodelist As New List(Of TvEpisode)
            episodelist = loadepisodes(newtvshownfo, episodelist)
            For Each ep In episodelist
                ep.ShowId.Value = newtvshownfo.TvdbId.Value
                fullepisodelist.Add(ep)
            Next
        Next

        If nofolder.Count > 0 Then
            Dim mymsg As String
            mymsg = (nofolder.Count).ToString + " folder/s missing or no tvshow.nfo:" + vbCrLf + vbCrLf
            For Each item In nofolder
                mymsg = mymsg + item + vbCrLf
            Next
            mymsg = mymsg + vbCrLf + "Do you wish to remove these folders" + vbCrLf + "from your list of TV Folders?" + vbCrLf
            If MsgBox(mymsg, MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                tv_Showremovedfromlist(lbCFolders, False, nofolder)
            End If
        End If

        frmSplash2.Label2.Visible = False

        frmSplash2.Label1.Text = "Saving Cache..."
        Windows.Forms.Application.DoEvents()
        CustTv_RefreshCacheSave(fulltvshowlist, fullepisodelist)    'save the cache file

        frmSplash2.Label1.Text = "Loading Cache..."
        Windows.Forms.Application.DoEvents()
        custtv_CacheLoad()    'reload the cache file to update the treeview
        Me.Enabled = True
        tbCShowCount.Text = Cache.CustTvCache.Shows.Count
        tbCEpCount  .Text = Cache.CustTvCache.Episodes.Count
        frmSplash2.Hide()
        'tv_Filter()
        BlinkTaskBar()
    End Sub

#End Region

    Public Function CustID() As String
        Dim datenow As DateTime = Date.Now()
        Dim idint As Integer = (((datenow.Year + datenow.Month + datenow.Day)+100)*1000) +((datenow.Hour + datenow.Minute + datenow.Second)+100)
        Return "MC" & idint.ToString
    End Function

End Class