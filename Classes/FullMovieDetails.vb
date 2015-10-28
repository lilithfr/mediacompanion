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

    Private _folderSize As Long = -1


    Public ReadOnly Property FolderSize As Long
        Get
            If _folderSize = -1 Then
                Try
                    Dim fi As System.IO.FileInfo = New System.IO.FileInfo(fileinfo.fullpathandfilename)
                    _folderSize = Utilities.GetFolderSize(fi.DirectoryName)
                Catch
                    _folderSize = 0
                End Try
            End If
            
            Return _folderSize
        End Get
    End Property


    Public Function GetDisplayFolderSize(precision As Integer) As String
        Return (FolderSize/(1024*1024*1024)).ToString("N" & precision.ToString)
    End Function

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
