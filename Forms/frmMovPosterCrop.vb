Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Public Class frmMovPosterCrop
    Private rect As UserRect
    Dim img As Bitmap = Form1.PictureBoxAssignedMoviePoster.Image
	Public Sub New()
		InitializeComponent()
        'Dim img As Bitmap = Form1.PictureBoxAssignedMoviePoster.Image
        SizePicBox
        PicBox.Image = img
        tb_cropmovtitle.Text = Form1.workingMovieDetails.fullmoviebody.title 
		rect = New UserRect(New Rectangle(80, 10, 250, 400))
        rect.dodraw = True
		rect.SetPictureBox(Me.PicBox)
	End Sub

    Private Sub btn_CropAccept_Click( sender As System.Object,  e As System.EventArgs) Handles btn_CropAccept.Click
        Form1.btnMoviePosterResetImage.Enabled = True
        Form1.btnMoviePosterSaveCroppedImage.Enabled = True
        Form1.PictureBoxAssignedMoviePoster.Image = rect.GetCropped()
        PicBox.Dispose()
        Me.Close()
    End Sub

    Private Sub btn_CropCancel_Click( sender As System.Object,  e As System.EventArgs) Handles btn_CropCancel.Click
        Me.Close()
    End Sub

    Private Sub SizePicBox()
        Dim newW As Integer = Math.Ceiling(img.Width/(img.Height/PicBox.Height ))
        PicBox.Width = newW
        Dim newxpos = Math.Ceiling(245-(newW/2))
        PicBox.Location = New Point(newxpos, 15)
    End Sub
End Class