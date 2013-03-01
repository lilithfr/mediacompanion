Public Class Rating

    Public Sub BitmapRating_V2(ByRef pbFanart As PictureBox, ByVal sRating As String)
        Dim iRating  As Single

        If sRating = "" Or Not Single.TryParse(sRating, iRating) Then
            Return
        End If

        iRating = Math.Min(iRating,10)

        Dim bmStars  As New Bitmap(PictureBoxRating.Image)
        Dim grStars  As Graphics = Graphics.FromImage(bmStars)
        Dim Brush    As New SolidBrush(Color.Black)

        Dim drawFont As New Font("Segoe UI", 14, FontStyle.Bold)

        If sRating.Length > 2 Then
            grStars.DrawString(sRating.ToString, drawFont, Brush, 0, 2)
        Else
            grStars.DrawString(sRating.ToString, drawFont, Brush, 8, 2)
        End If

        Dim StarsWidth As Integer = 39 + (Convert.ToInt16((iRating * 10) * ((bmStars.Width - 39) / 100)))
        Dim rectStars  As New Rectangle(0, 0, StarsWidth, bmStars.Height)

        Dim bmFanart As New Bitmap(pbFanart.Image)
        Dim grFanart As Graphics = Graphics.FromImage(bmFanart)

        Dim w As Integer = StarsWidth    *bmFanart.Width/pbFanart.ClientRectangle.Width
        Dim h As Integer = bmStars.Height*bmFanart.Width/pbFanart.ClientRectangle.Width

        Dim rectFanart As New Rectangle(0, 0, w, h)

        grFanart.DrawImage(bmStars, rectFanart, rectStars, GraphicsUnit.Pixel)

        pbFanart.Image = bmFanart
    End Sub

End Class
