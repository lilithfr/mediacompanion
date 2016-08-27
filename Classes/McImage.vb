
Public Class McImage
    Public Property hdUrl       As String   = ""
    Public Property ldUrl       As String   = ""
    Public Property hdwidth     As String   = ""
    Public Property hdheight    As String   = ""
    Public Property ldwidth     As String   = ""
    Public Property ldheight    As String   = ""
    Public Property votes       As Integer  = 0

    Shared Function GetFromTmDbBackDrop( backBrop As Object, Optional hdUrlPrefix As String="", Optional ldUrlPrefix As String="" )

        Dim result = New McImage

        result.hdUrl    = hdUrlPrefix + backBrop.file_path
        result.hdwidth  = backBrop.width .ToString
        result.hdheight = backBrop.height.ToString

        result.ldUrl    = ldUrlPrefix + backBrop.file_path
        result.ldwidth  = "1280"
        result.ldheight = "720"
        Try
            result.votes = backBrop.vote_count
        Catch
            result.votes = 0
        End Try
        Return result

    End Function
End Class

Public Class FanartPicBox
    Public Property pbox As PictureBox = New Picturebox
    Public Property imagepath As String = ""
End Class
