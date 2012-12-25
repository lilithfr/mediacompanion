Public Class Rating

    Dim ValueRating As Single
    Dim PictureBoxWidth As Integer

    Private Sub Rating_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBoxWidth = PictureBoxRating.Width
    End Sub

    Public Sub Rating(ByVal value As String)
        ValueRating = Convert.ToSingle(value.Replace(".", ","))
        PictureBoxRating.Width = (ValueRating * 10) * (PictureBoxWidth / 100)
        Me.Width = (ValueRating * 10) * (PictureBoxWidth / 100)

        PictureBoxRating.Refresh()
    End Sub

    Private Sub PictureBoxRating_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBoxRating.Paint
        Dim drawString As [String] = ValueRating.ToString
        Dim drawFont As New Font("Segoe UI", 14, FontStyle.Bold)
        Dim drawBrush As New SolidBrush(Color.Black)
        Dim x As Single
        Dim y As Single = 5

        If Convert.ToInt16(ValueRating) Mod ValueRating > 0 Then
            x = 0
        Else
            x = 7
        End If

        Dim width As Single = 200.0F
        Dim height As Single = 50.0F
        Dim drawRect As New RectangleF(x, y, width, height)
        Dim blackPen As New Pen(Color.Transparent)
        e.Graphics.DrawRectangle(blackPen, x, y, width, height)
        e.Graphics.DrawString(drawString, drawFont, drawBrush, drawRect)
    End Sub


End Class
