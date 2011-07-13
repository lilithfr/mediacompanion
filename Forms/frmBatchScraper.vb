Public Class frmBatchScraper



    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        If CheckBox22.CheckState = CheckState.Checked Then
            Form1.batchList.top250 = True
        Else
            Form1.batchList.top250 = False
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.batchList.runtime = True
        Else
            Form1.batchList.runtime = False
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.batchList.director = True
        Else
            Form1.batchList.director = False
        End If
    End Sub



    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.batchList.outline = True
        Else
            Form1.batchList.outline = False
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.batchList.mpaa = True
        Else
            Form1.batchList.mpaa = False
        End If
    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.batchList.premiered = True
        Else
            Form1.batchList.premiered = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.batchList.rating = True
        Else
            Form1.batchList.rating = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.batchList.plot = True
        Else
            Form1.batchList.plot = False
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.batchList.genre = True
        Else
            Form1.batchList.genre = False
        End If
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.batchList.studio = True
        Else
            Form1.batchList.studio = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.batchList.votes = True
        Else
            Form1.batchList.votes = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.batchList.tagline = True
        Else
            Form1.batchList.tagline = False
        End If
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.batchList.credits = True
        Else
            Form1.batchList.credits = False
        End If
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        If CheckBox21.CheckState = CheckState.Checked Then
            Form1.batchList.trailer = True
        Else
            Form1.batchList.trailer = False
        End If
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.batchList.actors = True
        Else
            Form1.batchList.actors = False
        End If
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        If CheckBox19.CheckState = CheckState.Checked Then
            Form1.batchList.mediatags = True
        Else
            Form1.batchList.mediatags = False
        End If
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.batchList.posterurls = True
        Else
            Form1.batchList.posterurls = False
        End If
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.batchList.missingfanart = True
        Else
            Form1.batchList.missingfanart = False
        End If
    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.batchList.missingposters = True
        Else
            Form1.batchList.missingposters = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form1.batchList.activate = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.batchList.country = True
        Else
            Form1.batchList.country = False
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.CheckState = CheckState.Checked Then
            Form1.batchList.stars = True
        Else
            Form1.batchList.stars = False
        End If

    End Sub
    Private Sub CheckBox20_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox20.CheckedChanged
        If CheckBox20.CheckState = CheckState.Checked Then
            Form1.batchList.year = True
        Else
            Form1.batchList.year = False
        End If
    End Sub

    Public Sub New()
        
        ' This call is required by the designer.
        InitializeComponent()
        
        ' Add any initialization after the InitializeComponent() call.
        
    End Sub

    
End Class