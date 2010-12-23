Public Class frnBatchScraper



    Private Sub CheckBox22_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox22.CheckedChanged
        If CheckBox22.CheckState = CheckState.Checked Then
            Form1.batchlist.top250 = True
        Else
            Form1.batchlist.top250 = False
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.CheckState = CheckState.Checked Then
            Form1.batchlist.runtime = True
        Else
            Form1.batchlist.runtime = False
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.CheckState = CheckState.Checked Then
            Form1.batchlist.director = True
        Else
            Form1.batchlist.director = False
        End If
    End Sub



    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.CheckState = CheckState.Checked Then
            Form1.batchlist.outline = True
        Else
            Form1.batchlist.outline = False
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.CheckState = CheckState.Checked Then
            Form1.batchlist.mpaa = True
        Else
            Form1.batchlist.mpaa = False
        End If
    End Sub

    Private Sub CheckBox13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox13.CheckedChanged
        If CheckBox13.CheckState = CheckState.Checked Then
            Form1.batchlist.premiered = True
        Else
            Form1.batchlist.premiered = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.CheckState = CheckState.Checked Then
            Form1.batchlist.rating = True
        Else
            Form1.batchlist.rating = False
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.CheckState = CheckState.Checked Then
            Form1.batchlist.plot = True
        Else
            Form1.batchlist.plot = False
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.CheckState = CheckState.Checked Then
            Form1.batchlist.genre = True
        Else
            Form1.batchlist.genre = False
        End If
    End Sub

    Private Sub CheckBox14_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox14.CheckedChanged
        If CheckBox14.CheckState = CheckState.Checked Then
            Form1.batchlist.studio = True
        Else
            Form1.batchlist.studio = False
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.CheckState = CheckState.Checked Then
            Form1.batchlist.votes = True
        Else
            Form1.batchlist.votes = False
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.CheckState = CheckState.Checked Then
            Form1.batchlist.tagline = True
        Else
            Form1.batchlist.tagline = False
        End If
    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.CheckState = CheckState.Checked Then
            Form1.batchlist.credits = True
        Else
            Form1.batchlist.credits = False
        End If
    End Sub

    Private Sub CheckBox21_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox21.CheckedChanged
        If CheckBox21.CheckState = CheckState.Checked Then
            Form1.batchlist.trailer = True
        Else
            Form1.batchlist.trailer = False
        End If
    End Sub

    Private Sub CheckBox15_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox15.CheckedChanged
        If CheckBox15.CheckState = CheckState.Checked Then
            Form1.batchlist.actors = True
        Else
            Form1.batchlist.actors = False
        End If
    End Sub

    Private Sub CheckBox19_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox19.CheckedChanged
        If CheckBox19.CheckState = CheckState.Checked Then
            Form1.batchlist.mediatags = True
        Else
            Form1.batchlist.mediatags = False
        End If
    End Sub

    Private Sub CheckBox16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox16.CheckedChanged
        If CheckBox16.CheckState = CheckState.Checked Then
            Form1.batchlist.posterurls = True
        Else
            Form1.batchlist.posterurls = False
        End If
    End Sub

    Private Sub CheckBox17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox17.CheckedChanged
        If CheckBox17.CheckState = CheckState.Checked Then
            Form1.batchlist.missingfanart = True
        Else
            Form1.batchlist.missingfanart = False
        End If
    End Sub

    Private Sub CheckBox18_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox18.CheckedChanged
        If CheckBox18.CheckState = CheckState.Checked Then
            Form1.batchlist.missingposters = True
        Else
            Form1.batchlist.missingposters = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Form1.batchlist.activate = True
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then
            Form1.batchlist.country = True
        Else
            Form1.batchlist.country = False
        End If
    End Sub
End Class