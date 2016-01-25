Imports System.IO

Public Class GraphicInfo

    Public xPos As Integer
    Public yPos As Integer
    Public fanartRatio As Double
    Public grFanart As Graphics

    Public Sub OverlayInfo(ByRef picbxFanart As PictureBox, ByVal sRating As String, ByVal flags As List(Of KeyValuePair(Of String, String)),Optional folderSize As Double=-1)
        'OVERLAY RATING STARS
        Dim iRating As Single

        Dim bmFanart As New Bitmap(picbxFanart.Image)

        grFanart = Graphics.FromImage(bmFanart)

        Dim fanartWidth  As Integer = picbxFanart.ClientRectangle.Width
        Dim fanartHeight As Integer = picbxFanart.ClientRectangle.Height
        
        fanartRatio = bmFanart.Height / fanartHeight

        If Pref.DisplayRatingOverlay Then
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

        Dim padding As Integer = 2

        xPos = padding * fanartRatio
        yPos = (fanartHeight - (32 + padding)) * fanartRatio


        'OVERLAY VIDEO FLAGS
        If Pref.DisplayMediainfoOverlay Then
            For Each item In flags
                Try
                    If Not String.IsNullOrEmpty(item.Value) Then    'Catch any empty values for selected flags
                        
                        Dim flagName As String
                        Dim flagPath As String

                        If item.Key.IndexOf("lang") > -1 Then
                            flagPath = IO.Path.Combine(Pref.applicationPath, "Resources\video_flags\" + item.Key + ".png")
                        Else
                            flagName = String.Format("media_{0}_{1}.png", item.Key, item.Value )
                            flagPath = IO.Path.Combine(Pref.applicationPath, String.Format("Resources\video_flags\{0}", flagName.ToLower))
                        End If
                        
                        Dim bmflagStream As New MemoryStream(My.Computer.FileSystem.ReadAllBytes(flagPath))
                        Dim bmFlag       As New Bitmap(bmflagStream)
                        Dim rectFlag     As New Rectangle(0, 0, bmFlag.Width, bmFlag.Height)
                        Dim recFanart    As New Rectangle(xPos, yPos, bmFlag.Width * fanartRatio, bmFlag.Height * fanartRatio)


                        If item.Key.IndexOf("lang") > -1 Then
                            Dim sDisplayText As String = item.Value
                            If sDisplayText.ToLower = "error" Then Continue For
                            Dim FontSize = 19

                            'create a font to write the values in the bitmap
                            Dim font as new Font("Tahoma", FontSize)

                            'origin for the string
                            'Dim origin as new PointF(0, 2)

                            'use the bitmap to draw
                            Dim graphic = Graphics.FromImage(bmFlag)

                            Dim gradient = 224

                            Dim brush as new SolidBrush( Color.FromArgb(gradient, gradient, gradient) )

                            'draw the string including the value
                            'graphic.DrawString(sDisplayText, font, brush, origin)

                            Dim sf As new StringFormat()

                            sf.LineAlignment = StringAlignment.Center
                            sf.Alignment = StringAlignment.Center
                            graphic.DrawString(sDisplayText, font, brush, rectFlag, sf)

                            'TextRenderer.DrawText(graphic, sDisplayText, font, rectFlag, Color.FromArgb(gradient, gradient, gradient), Color.Empty, TextFormatFlags.VerticalCenter or TextFormatFlags.HorizontalCenter)
                        End If

                        grFanart.DrawImage(bmFlag, recFanart, rectFlag, GraphicsUnit.Pixel)

                        xPos += (bmFlag.Width + padding) * fanartRatio

                    End If
                Catch ex As Exception
                    'If the derived filname doesn't exist, we'll just ignore it (this will happen if aspect comes back as empty string).
                End Try
            Next
        End If

        DisplayMediaInfoFolderSize(folderSize)

        picbxFanart.Image = bmFanart
    End Sub


    Public Sub DisplayMediaInfoFolderSize(folderSize As Double)
        If Pref.DisplayMediaInfoFolderSize And folderSize > -1 Then
            Dim flagPath As String = IO.Path.Combine(Pref.applicationPath, "Resources\video_flags\long_blank.png")
            Dim bmflagStream As New MemoryStream(My.Computer.FileSystem.ReadAllBytes(flagPath))
            Dim bmFlag As Bitmap = New Bitmap(bmflagStream)

            Dim sFolderSize As String = String.Format("{0:00.0}GB",folderSize)

            Dim offSet   = sFolderSize.Length - 2
            Dim FontSize = 19 - ((offSet-4)*2)

            'create a font to write the values in the bitmap
            Dim font as new Font("Tahoma", FontSize)

            'origin for the string
            Dim origin as new PointF(0, offSet)

            'use the bitmap to draw
            Dim graphic = Graphics.FromImage(bmFlag)

            Dim gradient = 224

            Dim brush as new SolidBrush( Color.FromArgb(gradient, gradient, gradient) )

            'draw the string including the value
            graphic.DrawString(sFolderSize, font, brush, origin)

            Dim rectFlag As New Rectangle(0, 0, bmFlag.Width, bmFlag.Height)
            Dim recFanart As New Rectangle(xPos, yPos, bmFlag.Width * fanartRatio, bmFlag.Height * fanartRatio)
            grFanart.DrawImage(bmFlag, recFanart, rectFlag, GraphicsUnit.Pixel)
        End If
    End Sub

End Class
