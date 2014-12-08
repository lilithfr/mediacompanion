Imports System.IO

Public Class GraphicInfo

    Public Sub OverlayInfo(ByRef picbxFanart As PictureBox, ByVal sRating As String, ByVal flags As List(Of KeyValuePair(Of String, String)))
        'OVERLAY RATING STARS
        Dim iRating As Single

        Dim bmFanart As New Bitmap(picbxFanart.Image)
        Dim grFanart As Graphics = Graphics.FromImage(bmFanart)
        Dim fanartWidth As Integer = picbxFanart.ClientRectangle.Width
        Dim fanartHeight As Integer = picbxFanart.ClientRectangle.Height
        Dim fanartRatio As Double = bmFanart.Height / fanartHeight

        If Preferences.DisplayRatingOverlay Then
            If Not sRating = "" Or Single.TryParse(sRating, iRating) Then
            iRating = Math.Min(iRating, 10)
            sRating = sRating.FormatRating

            Dim Brush As New SolidBrush(Color.Black)
            Dim drawFont As New Font("Segoe UI", 14, FontStyle.Bold)

            Dim bmStars As New Bitmap(picbxGraphicInfo.Image)

            Dim StarsWidth As Integer = 39 + (Convert.ToInt16((iRating * 10) * ((bmStars.Width - 39) / 100)))
            Dim rectStars As New Rectangle(0, 0, StarsWidth, bmStars.Height)
            Dim rectFanart As New Rectangle(0, 0, StarsWidth * fanartRatio, bmStars.Height * fanartRatio)

            Dim grStars As Graphics = Graphics.FromImage(bmStars)
            grStars.DrawString(sRating, drawFont, Brush, If(sRating.Length > 2, 0, 8), 2)
            grFanart.DrawImage(bmStars, rectFanart, rectStars, GraphicsUnit.Pixel)
            End If
        End If

        'OVERLAY VIDEO FLAGS
        If Preferences.DisplayMediainfoOverlay Then
            Dim padding As Integer = 2
            Dim xPos As Integer = padding * fanartRatio
            Dim yPos As Integer = (fanartHeight - (32 + padding)) * fanartRatio

            For Each item In flags
                Try
                    If Not String.IsNullOrEmpty(item.Value) Then    'Catch any empty values for selected flags

                        Dim flagName As String = String.Format("media_{0}_{1}.png", item.Key, item.Value )
                        Dim flagPath As String = IO.Path.Combine(Preferences.applicationPath, String.Format("Resources\video_flags\{0}", flagName.ToLower))
                        Dim bmflagStream As New MemoryStream(My.Computer.FileSystem.ReadAllBytes(flagPath))
                        Dim bmFlag As Bitmap = New Bitmap(bmflagStream)
                        Dim rectFlag As New Rectangle(0, 0, bmFlag.Width, bmFlag.Height)
                        Dim recFanart As New Rectangle(xPos, yPos, bmFlag.Width * fanartRatio, bmFlag.Height * fanartRatio)
                        grFanart.DrawImage(bmFlag, recFanart, rectFlag, GraphicsUnit.Pixel)
                        xPos += (bmFlag.Width + padding) * fanartRatio
                    End If
                Catch ex As Exception
                    'If the derived filname doesn't exist, we'll just ignore it (this will happen if aspect comes back as empty string).
                End Try
            Next

        End If

        picbxFanart.Image = bmFanart
    End Sub

End Class
