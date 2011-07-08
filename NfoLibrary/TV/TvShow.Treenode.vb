Imports System.Windows.Forms

Partial Public Class TvShow
    Public ShowNode As New TreeNode


    Public Sub UpdateTreenode()
        Me.ShowNode.Tag = Me
        Me.ShowNode.Text = Me.Title.Value
        Select Case Me.State
            Case Nfo.ShowState.Open
                ShowNode.ImageKey = "blank"
                ShowNode.SelectedImageKey = "blank"
            Case Nfo.ShowState.Locked
                ShowNode.ImageKey = "padlock"
                ShowNode.SelectedImageKey = "padlock"
            Case Nfo.ShowState.Unverified
                ShowNode.ImageKey = "qmark"
                ShowNode.SelectedImageKey = "qmark"
            Case Nfo.ShowState.Error
                ShowNode.ImageKey = "error"
                ShowNode.SelectedImageKey = "error"
        End Select
    End Sub

    Public Overrides Sub Load(ByVal Path As String)
        MyBase.Load(Path)
        UpdateTreenode()
        SearchForEpisodesInFolder()
    End Sub
End Class
