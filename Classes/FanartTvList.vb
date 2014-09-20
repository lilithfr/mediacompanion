Imports Media_Companion

Public Class FanartTvList
    Public hdmovieclearart     As List(Of str_fanarttvart)
    Public hdmovielogo         As List(Of str_fanarttvart)
    Public movielogo           As List(Of str_fanarttvart)
    Public movieart            As List(Of str_fanarttvart)
    Public moviebackground     As List(Of str_fanarttvart)
    Public moviedisc           As List(Of str_fanarttvart)
    Public moviebanner         As List(Of str_fanarttvart)
    Public moviethumb          As List(Of str_fanarttvart)
    Public movieposter         As List(Of str_fanarttvart)

    Sub New
        Init
    End Sub

    Public Sub Init
        hdmovieclearart     = New List(Of str_fanarttvart)
        hdmovielogo         = New List(Of str_fanarttvart)
        movielogo           = New List(Of str_fanarttvart)
        movieart            = New List(Of str_fanarttvart)
        moviebackground     = New List(Of str_fanarttvart)
        moviedisc           = New List(Of str_fanarttvart)
        moviebanner         = New List(Of str_fanarttvart)
        moviethumb          = New List(Of str_fanarttvart)
        movieposter         = New List(Of str_fanarttvart)
    End Sub
End Class
