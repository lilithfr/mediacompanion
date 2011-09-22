Public Class frmBatchScraper

    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        Try
            If CheckBox22.CheckState = CheckState.Checked Then
                Form1.batchList.top250 = True
            Else
                Form1.batchList.top250 = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        Try
            If CheckBox8.CheckState = CheckState.Checked Then
                Form1.batchList.runtime = True
            Else
                Form1.batchList.runtime = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        Try
            If CheckBox12.CheckState = CheckState.Checked Then
                Form1.batchList.director = True
            Else
                Form1.batchList.director = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        Try
            If CheckBox5.CheckState = CheckState.Checked Then
                Form1.batchList.outline = True
            Else
                Form1.batchList.outline = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        Try
            If CheckBox9.CheckState = CheckState.Checked Then
                Form1.batchList.mpaa = True
            Else
                Form1.batchList.mpaa = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        Try
            If CheckBox13.CheckState = CheckState.Checked Then
                Form1.batchList.premiered = True
            Else
                Form1.batchList.premiered = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        Try
            If CheckBox3.CheckState = CheckState.Checked Then
                Form1.batchList.rating = True
            Else
                Form1.batchList.rating = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        Try
            If CheckBox6.CheckState = CheckState.Checked Then
                Form1.batchList.plot = True
            Else
                Form1.batchList.plot = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        Try
            If CheckBox10.CheckState = CheckState.Checked Then
                Form1.batchList.genre = True
            Else
                Form1.batchList.genre = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        Try
            If CheckBox14.CheckState = CheckState.Checked Then
                Form1.batchList.studio = True
            Else
                Form1.batchList.studio = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        Try
            If CheckBox4.CheckState = CheckState.Checked Then
                Form1.batchList.votes = True
            Else
                Form1.batchList.votes = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        Try
            If CheckBox7.CheckState = CheckState.Checked Then
                Form1.batchList.tagline = True
            Else
                Form1.batchList.tagline = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        Try
            If CheckBox11.CheckState = CheckState.Checked Then
                Form1.batchList.credits = True
            Else
                Form1.batchList.credits = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        Try
            If CheckBox21.CheckState = CheckState.Checked Then
                Form1.batchList.trailer = True
            Else
                Form1.batchList.trailer = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        Try
            If CheckBox15.CheckState = CheckState.Checked Then
                Form1.batchList.actors = True
            Else
                Form1.batchList.actors = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        Try
            If CheckBox19.CheckState = CheckState.Checked Then
                Form1.batchList.mediatags = True
            Else
                Form1.batchList.mediatags = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        Try
            If CheckBox16.CheckState = CheckState.Checked Then
                Form1.batchList.posterurls = True
            Else
                Form1.batchList.posterurls = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        Try
            If CheckBox17.CheckState = CheckState.Checked Then
                Form1.batchList.missingfanart = True
            Else
                Form1.batchList.missingfanart = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        Try
            If CheckBox18.CheckState = CheckState.Checked Then
                Form1.batchList.missingposters = True
            Else
                Form1.batchList.missingposters = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Form1.batchList.activate = True
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Me.Close()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            If CheckBox1.CheckState = CheckState.Checked Then
                Form1.batchList.country = True
            Else
                Form1.batchList.country = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        Try
            If CheckBox2.CheckState = CheckState.Checked Then
                Form1.batchList.stars = True
            Else
                Form1.batchList.stars = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try

    End Sub
    Private Sub CheckBox20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox20.CheckedChanged
        Try
            If CheckBox20.CheckState = CheckState.Checked Then
                Form1.batchList.year = True
            Else
                Form1.batchList.year = False
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Public Sub New()
        
        ' This call is required by the designer.
        InitializeComponent()
        
        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

    
End Class