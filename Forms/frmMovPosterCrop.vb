Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Public Class frmMovPosterCrop
    Private rect As UserRect
    Dim Public img As Bitmap = Form1.PictureBoxAssignedMoviePoster.Image
    'Dim Public zm As Double = 1
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
        Dim zm as Double = (img.Height/PicBox.Height)
        Form1.btnMoviePosterResetImage.Enabled = True
        Form1.btnMoviePosterSaveCroppedImage.Enabled = True
        Form1.PictureBoxAssignedMoviePoster.Image = rect.GetCropped(zm)
        Form1.lblCurrentLoadedPoster.Text = "Width: " & Form1.PictureBoxAssignedMoviePoster.Image.Width.ToString & "  Height: " & Form1.PictureBoxAssignedMoviePoster.Image.Height.ToString
        PicBox.Dispose()
        Me.Close()
    End Sub

    Private Sub btn_CropCancel_Click( sender As System.Object,  e As System.EventArgs) Handles btn_CropCancel.Click
        Me.Close()
    End Sub

    Private Sub SizePicBox()
        'zm = (img.Height/PicBox.Height)
        Dim newW As Integer = Math.Ceiling(img.Width/(img.Height/PicBox.Height))
        PicBox.Width = newW
        Dim newxpos = Math.Ceiling(245-(newW/2))
        PicBox.Location = New Point(newxpos, 15)
    End Sub
End Class