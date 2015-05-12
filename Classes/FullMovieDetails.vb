Imports Media_Companion

Public Class FullMovieDetails
    Public fileinfo          As str_FileDetails
    Public fullmoviebody     As str_BasicMovieNFO
    Public alternativetitles As List(Of String)
    Public listactors        As List(Of str_MovieActors)
    Public frodoPosterThumbs As List(Of FrodoPosterThumb)
    Public frodoFanartThumbs As FrodoFanartThumbs
    Public listthumbs        As List(Of String)
    Public filedetails       As FullFileDetails

    Sub New
        Init
    End Sub

    Public Sub Init
        fileinfo          = New str_FileDetails  (True)
        fullmoviebody     = New str_BasicMovieNFO(True)
        alternativetitles = New List(Of String)
        listactors        = New List(Of str_MovieActors)
        frodoPosterThumbs = New List(Of FrodoPosterThumb)
        frodoFanartThumbs = New FrodoFanartThumbs
        listthumbs        = New List(Of String)
        filedetails       = New FullFileDetails
    End Sub
End Class
