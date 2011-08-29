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

        If Me.FailedLoad Then
            ShowNode.ForeColor = Drawing.Color.Red
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
