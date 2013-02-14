Imports System.Windows.Forms

Partial Public Class TvSeason
    Public SeasonNode As New TreeNode With {.SelectedImageKey = "blank", .ImageKey = "blank"}

    Public Sub UpdateTreenode()
        Me.SeasonNode.Tag = Me
        Me.SeasonNode.Text = Me.SeasonLabel
        SeasonNode.Tag = Me

        If Me.SeasonNode.TreeView Is Nothing Then
            If ShowObj IsNot Nothing Then
                Me.ShowObj.ShowNode.Nodes.Add(Me.SeasonNode)
            End If
        End If
    End Sub

    Public Function GetParentShow() As TvShow
        If Me.SeasonNode.Parent IsNot Nothing Then
            Return Me.SeasonNode.Parent.Tag
        End If
        Return Nothing
    End Function

End Class
