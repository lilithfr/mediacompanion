Public Class Rating

    Dim ValueRating As Single
    Dim PictureBoxWidth As Integer
    Public PictureInit As Image

    Private Sub Rating_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBoxWidth = PictureBoxRating.Width
    End Sub

    Public Function BitmapRating(ByVal PictureBoxInit As Image, ByVal PictureBoxWidth As Integer, ByVal PictureBoxHeight As Integer, ByVal Value As String) As Bitmap
        Dim Ratingwidth As Integer
        Dim Graph As New Bitmap(120, 40)
        Dim Brush As New SolidBrush(Color.Black)
        Dim BrushCircle As New SolidBrush(Color.Chocolate)
        Dim pen As New System.Drawing.Pen(Color.DarkOrange, 30)
        Dim drawFont As New Font("Segoe UI", 13, FontStyle.Bold)
        Dim Ratio As Single

        If Value = "" Then
            Return PictureInit
        End If

        Ratio = PictureInit.Width / PictureInit.Height

        'Resize source Image
        Dim bm_source As New Bitmap(PictureInit)
        Dim bm_dest As New Bitmap(PictureBoxWidth, PictureBoxHeight)
        Dim gr_dest As Graphics = Graphics.FromImage(bm_dest)
        gr_dest.DrawImage(bm_source, 0, 0, PictureBoxWidth, PictureBoxWidth / Ratio)

        ValueRating = Convert.ToSingle(Value.Replace(".", ","))
'        Ratingwidth = 39 + (Convert.ToInt16((ValueRating * 10) * ((PictureBoxRating.Width - 39) / 100)))
        Ratingwidth = 39 + (Convert.ToInt16(ValueRating * ((PictureBoxRating.Width - 39) / 100)))

        'Copy Stars 
        Using gr As Graphics = Graphics.FromImage(bm_dest)
            Dim src_rect As New Rectangle(0, 0, Ratingwidth, PictureBoxRating.Height)
            Dim dst_rect As New Rectangle(0, 0, Ratingwidth, PictureBoxRating.Height)
            gr.DrawImage(PictureBoxRating.Image, dst_rect, src_rect, GraphicsUnit.Pixel)
        End Using

        'write text
        Dim Graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bm_dest)

        If Value.Length > 2 Then
            Graphic.DrawString(Value.ToString.Substring(0, 3), drawFont, Brush, 0, 3)
        Else
            Graphic.DrawString(Value.ToString, drawFont, Brush, 8, 3)
        End If

        Return bm_dest
    End Function


End Class
