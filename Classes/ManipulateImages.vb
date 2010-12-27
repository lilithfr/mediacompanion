Imports System.Threading

Public Class ManipulateImages

    Public Function ResizeImage(ByVal bmp As Bitmap, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim bm_source As New Bitmap(bmp)
        Dim bm_dest As New Bitmap(width, height)
        Dim gr As Graphics = Graphics.FromImage(bm_dest)
        gr.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
        gr.DrawImage(bm_source, 0, 0, width - 1, height - 1)
        Dim tempbitmap As Bitmap = bm_dest
        Return tempbitmap
    End Function

    Public Function loadbitmap(ByVal path As String) As Bitmap
        Monitor.Enter(Me)
        Try
            Dim bmp As New Bitmap(path)
            Dim bmp2 As New Bitmap(bmp)
            bmp.Dispose()
            Return bmp2
            bmp.Dispose()
        Catch
            Return Nothing
        Finally
            Monitor.Exit(Me)
        End Try
    End Function
End Class
