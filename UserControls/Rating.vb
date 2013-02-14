Public Class Rating

    'Dim ValueRating As Single
    'Dim PictureBoxWidth As Integer
    'Public PictureInit As Image

    'Private Sub Rating_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    '    PictureBoxWidth = PictureBoxRating.Width
    'End Sub
    
    'Public Function BitmapRating(ByVal PictureBoxInit As Image, ByVal PictureBoxWidth As Integer, ByVal PictureBoxHeight As Integer, ByVal Value As String) As Bitmap
    '    Dim Ratingwidth As Integer
    '    Dim Graph As New Bitmap(120, 40)
    '    Dim Brush As New SolidBrush(Color.Black)
    '    Dim BrushCircle As New SolidBrush(Color.Chocolate)
    '    Dim pen As New System.Drawing.Pen(Color.DarkOrange, 30)
    '    Dim drawFont As New Font("Segoe UI", 13, FontStyle.Bold)
    '    Dim Ratio As Single

    '    If Value = "" Or Not Single.TryParse(Value, ValueRating) Then
    '        Return PictureInit
    '    End If

    '    Ratio = PictureInit.Width / PictureInit.Height

    '    'Resize source Image
    '    Dim bm_source As New Bitmap(PictureInit)
    '    Dim bm_dest As New Bitmap(PictureBoxWidth, PictureBoxHeight)
    '    Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)

    '    Dim topOffset As Integer = (PictureBoxHeight - (PictureBoxWidth / Ratio))/2

    '    gr_dest.DrawImage(bm_source, 0, topOffset, PictureBoxWidth, PictureBoxWidth / Ratio)

    '    'ValueRating = Convert.ToSingle(Value.Replace(".", ","))
    '    Ratingwidth = 39 + (Convert.ToInt16((ValueRating * 10) * ((PictureBoxRating.Width - 39) / 100)))
    '    'Ratingwidth = 39 + (Convert.ToInt16(ValueRating * ((PictureBoxRating.Width - 39) / 100)))

    '    'Copy Stars 
    '    Using gr As Graphics = Graphics.FromImage(bm_dest)
    '        Dim src_rect As New Rectangle(0, topOffset, Ratingwidth, PictureBoxRating.Height)
    '        Dim dst_rect As New Rectangle(0, topOffset, Ratingwidth, PictureBoxRating.Height)
    '        gr.DrawImage(PictureBoxRating.Image, dst_rect, src_rect, GraphicsUnit.Pixel)
    '    End Using

    '    'write text
    '    Dim Graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bm_dest)

    '    If Value.Length > 2 Then
    '        Graphic.DrawString(Value.ToString.Substring(0, 3), drawFont, Brush, 0, topOffset+3)
    '    Else
    '        Graphic.DrawString(Value.ToString, drawFont, Brush, 8, topOffset+3)
    '    End If

    '    Return bm_dest
    'End Function



    Public Sub BitmapRating_V2(ByRef pbFanart As PictureBox, ByVal sRating As String)

        Dim iRating  As Single

        If sRating = "" Or Not Single.TryParse(sRating, iRating) Then
            Return
        End If

        iRating = Math.Min(iRating,10)

        Dim bmStars  As New Bitmap(PictureBoxRating.Image)
        Dim bmFanart As New Bitmap(pbFanart        .Image)

        Dim Ratingwidth As Integer = 39 + (Convert.ToInt16((iRating * 10) * ((bmStars.Width - 39) / 100)))

        Dim rectStars As New Rectangle(0, 0, Ratingwidth, bmStars.Height)

        Dim widthRatio  As Double = bmFanart.Width /1920
        Dim heightRatio As Double = bmFanart.Height/1080

        Dim w As Integer = widthRatio *Ratingwidth
        Dim h As Integer = heightRatio*bmStars.Height

        dim minScaler As Double = 1

        If pbFanart.ClientRectangle.Width < 600 Then

            minScaler = 600/pbFanart.ClientRectangle.Width

            w = w*minScaler
            h = h*minScaler

            w = Math.Min(w, pbFanart.ClientRectangle.Width )
            h = Math.Min(h, pbFanart.ClientRectangle.Height)
        End If


        Dim rectFanart As New Rectangle(0, 0, w*2, h*2)

        Dim gr       As Graphics = Graphics.FromImage(bmFanart)
        Dim Brush    As New SolidBrush(Color.Black)
        Dim fontSize As Integer = heightRatio*27*minScaler
        Dim drawFont As New Font("Segoe UI", fontSize, FontStyle.Bold)

        gr.DrawImage(bmStars, rectFanart, rectStars, GraphicsUnit.Pixel)

        If sRating.Length > 2 Then
            gr.DrawString(sRating.ToString.Substring(0, 3), drawFont, Brush, 0, heightRatio*5)
        Else
            gr.DrawString(sRating.ToString, drawFont, Brush, 8, heightRatio*5)
        End If

        pbFanart.Image = bmFanart
    End Sub

End Class
