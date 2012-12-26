<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Rating
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Rating))
        Me.PictureBoxRating = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBoxRating, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBoxRating
        '
        Me.PictureBoxRating.BackColor = System.Drawing.Color.Transparent
        Me.PictureBoxRating.Image = CType(resources.GetObject("PictureBoxRating.Image"), System.Drawing.Image)
        Me.PictureBoxRating.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxRating.Name = "PictureBoxRating"
        Me.PictureBoxRating.Size = New System.Drawing.Size(199, 32)
        Me.PictureBoxRating.TabIndex = 1
        Me.PictureBoxRating.TabStop = False
        '
        'Rating
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.PictureBoxRating)
        Me.Name = "Rating"
        Me.Size = New System.Drawing.Size(199, 32)
        CType(Me.PictureBoxRating, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PictureBoxRating As System.Windows.Forms.PictureBox

End Class
