Imports System.Windows.Forms
Imports Media_Companion

Partial Public Class TvEpisode
    Public EpisodeNode As New TreeNode With {.SelectedImageKey = "blank", .ImageKey = "blank"}

    Public Sub UpdateTreenode()
        Me.EpisodeNode.Tag = Me

        Dim episode_count = 1
        If Me.ShowObj IsNot Nothing Then
            If Me.ShowObj.MaxEpisodeCount.Value IsNot Nothing Then
                If Integer.Parse(Me.ShowObj.MaxEpisodeCount.Value) > 99 Then
                    episode_count = 3
                ElseIf Integer.Parse(Me.ShowObj.MaxEpisodeCount.Value) > 9 Then
                    episode_count = 2
                End If
            End If
        End If

        If Me.Title.Value IsNot Nothing Then
            Me.EpisodeNode.Name = Me.NfoFilePath             ' save nfo path in this node
            If Me.Episode.Value IsNot Nothing Then
                If IsNumeric(Me.Episode.Value) Then
                    Me.EpisodeNode.Text = Utilities.PadNumber(Me.Episode.Value, episode_count) & " - " & Me.Title.Value
                Else
                    Me.EpisodeNode.Text = Utilities.PadNumber(Me.Episode.Value, episode_count) & " - " & Me.Title.Value
                End If
            Else
                Me.EpisodeNode.Text = Me.Title.Value
            End If
        Else
            Me.EpisodeNode.Text = Me.NfoFilePath
        End If

        If Me.IsMissing Then
            ' Phyonics - Fix for issue #208
            If String.IsNullOrEmpty(Aired.Value) Then
                ' Change the colour to gray
                EpisodeNode.ForeColor = Drawing.Color.Gray
            Else
                Try
                    ' Is the episode in the future?
                    If Convert.ToDateTime(Aired.Value) > Now Then
                        ' Yes, so change its colour to gray
                        EpisodeNode.ForeColor = Drawing.Color.Gray
                    Else
                        EpisodeNode.ForeColor = Drawing.Color.Blue
                    End If
                Catch ex As Exception
                    ' Set the colour to the missing colour
                    EpisodeNode.ForeColor = Drawing.Color.Blue
                End Try
            End If
        End If

        If Me.FailedLoad Then
            EpisodeNode.ForeColor = Drawing.Color.Red
        End If

        If Me.IsAltered Then
            EpisodeNode.ImageKey = "edit"
            EpisodeNode.SelectedImageKey = "edit"
        Else
            EpisodeNode.ImageKey = "blank"
            EpisodeNode.SelectedImageKey = "blank"
        End If

        If Me.EpisodeNode.TreeView Is Nothing Then
            If Me.SeasonObj IsNot Nothing Then
                Me.SeasonObj.SeasonNode.Nodes.Add(Me.EpisodeNode)
            End If
        End If
    End Sub
    Public Overrides Sub Load()
        MyBase.Load()

        UpdateTreenode()
    End Sub

    Public Overrides Sub Load(ByVal Path As String)
        MyBase.Load(Path)

        UpdateTreenode()
    End Sub
End Class
