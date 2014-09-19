Imports System.Windows.Forms

Partial Public Class TvShow
    Public ShowNode As New TreeNode


    Public Sub UpdateTreenode()
        Me.ShowNode.Tag = Me
        Me.ShowNode.Text = Preferences.RemoveIgnoredArticles(Me.Title.Value)
        
        Select Case Me.State
            Case Media_Companion.ShowState.Open
                ShowNode.ImageKey = "blank"
                ShowNode.SelectedImageKey = "blank"
            Case Media_Companion.ShowState.Locked
                ShowNode.ImageKey = "padlock"
                ShowNode.SelectedImageKey = "padlock"
            Case Media_Companion.ShowState.Unverified
                ShowNode.ImageKey = "qmark"
                ShowNode.SelectedImageKey = "qmark"
            Case Media_Companion.ShowState.Error
                ShowNode.ImageKey = "error"
                ShowNode.SelectedImageKey = "error"
        End Select

        If Me.IsAltered Then
            ShowNode.ImageKey = "edit"
            ShowNode.SelectedImageKey = "edit"
        End If

        If Me.FailedLoad Then
            ShowNode.ForeColor = Drawing.Color.Red
        End If

        If Preferences.displayMissingEpisodes AndAlso Me.MissingEpisodes.Count > 0 Then
            ShowNode.ImageKey = "missing.png"
            ShowNode.SelectedImageKey = "missing.png"
        End If

    End Sub

    Public Overrides Sub Load(ByVal Path As String)
        MyBase.Load(Path)
        UpdateTreenode()
    End Sub

    Public Overloads Sub Load(ByVal SearchForEpisodesAsWell As Boolean)
        Me.Load()
        If SearchForEpisodesAsWell Then
            SearchForEpisodesInFolder()
        End If
    End Sub

    Public Overloads Sub Load(ByVal Path As String, ByVal SearchForEpisodesAsWell As Boolean)
        Me.Load(Path)
        If SearchForEpisodesAsWell Then
            SearchForEpisodesInFolder()
        End If
    End Sub
End Class
