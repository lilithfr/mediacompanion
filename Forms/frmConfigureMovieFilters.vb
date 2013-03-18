Imports System.Linq
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class frmConfigureMovieFilters

    Property FilterSpace     As Integer = 30
    Property FilterContainer As Panel

    Public Sub Init(container As Panel)
        FilterContainer = container

        PopCheckListBox

        dim x=0
    End Sub


    Sub PopCheckListBox
        clbMovieFilters.Items.Clear

        Dim lbl As Label
        Dim i   As Integer=0
        Dim query = From c As Control In FilterContainer.Controls Where c.Name.IndexOf("cbFilter")=0

        For Each cb As ComboBox In query

            lbl = FilterContainer.Controls("lbl"+ cb.Name.SubString(2,cb.Name.Length-2))

            clbMovieFilters.Items.Add(lbl.Text)
            clbMovieFilters.SetItemChecked(i,cb.Visible)
            i += 1
        Next
    End Sub


    Private Sub btnDone_Click( sender As Object,  e As EventArgs) Handles btnApply.Click
        Dim cb    As ComboBox
        Dim lbl   As Label
        Dim index As Integer=1
        Dim i     As Integer=0
        Dim show As Boolean
        Dim Y    As Integer

        For Each item As String In clbMovieFilters.Items

            Dim wotEver = item

            lbl = (From c As Control In FilterContainer.Controls Where c.Text=wotEver)(0)

            cb  = FilterContainer.Controls("cb" + lbl.Name.SubString(3,lbl.Name.Length-3) )

            show = clbMovieFilters.GetItemChecked(i)

            cb .Visible = show
            lbl.Visible = show
           
            If show Then 
                Y = FilterContainer.Height - (index*FilterSpace)

                cb .Location = New Point( cb .Location.X, Y )
                lbl.Location = New Point( lbl.Location.X, Y )

                index += 1
            End If

            i += 1
        Next
    End Sub

End Class
