Imports System.Windows.Forms
Imports Media_Companion

Partial Public Class TvEpisode
    Public EpisodeNode As New TreeNode With {.SelectedImageKey = "blank", .ImageKey = "blank"}

    Public Sub UpdateTreenode()
        Me.EpisodeNode.Tag = Me
        If Me.Title.Value IsNot Nothing Then
            If Me.Episode.Value IsNot Nothing Then
                If IsNumeric(Me.Episode.Value) Then

                    Me.EpisodeNode.Text = Utilities.PadNumber(Me.Episode.Value, 2) & " - " & Me.Title.Value
                Else
                    Me.EpisodeNode.Text = Utilities.PadNumber(Me.Episode.Value, 2) & " - " & Me.Title.Value
                End If
            Else
                Me.EpisodeNode.Text = Me.Title.Value
            End If
        Else
            Me.EpisodeNode.Text = Me.NfoFilePath
        End If

        If Me.Missing.Value IsNot Nothing AndAlso Me.Missing.Value <> "true" Then
            'Make it blue
        End If
    End Sub
    Public Overrides Sub Load(ByVal Path As String)
        MyBase.Load(Path)

        UpdateTreenode()
    End Sub
End Class
