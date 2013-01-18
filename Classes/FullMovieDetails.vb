Imports Media_Companion

Public Class FullMovieDetails
    Public fileinfo As New str_FileDetails(True)
    Public fullmoviebody As New str_BasicMovieNFO(True)
    Public alternativetitles As New List(Of String)
    Public listactors As New List(Of str_MovieActors)
    Public frodoPosterThumbs As New List(Of FrodoPosterThumb)
    Public frodoFanartThumbs As New FrodoFanartThumbs
    Public listthumbs As New List(Of String)
    Public filedetails As New FullFileDetails
End Class
