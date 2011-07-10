﻿Imports System.Windows.Forms
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

        If Me.IsMissing Then
            EpisodeNode.ForeColor = Drawing.Color.Blue
        ElseIf Me.IsAltered Then
            EpisodeNode.ForeColor = Drawing.Color.Gold
        End If

        If Me.FailedLoad Then
            EpisodeNode.ForeColor = Drawing.Color.Red
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
