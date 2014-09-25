Imports System.Linq
Imports System.ComponentModel
Imports System.Windows.Forms

Public Class frmGenreSelect
    Property FilterSpace     As Integer = 30
    Property chkbxlst As New List(Of String)
    Dim _selectgenres As New List (Of String)

    Public Property SelectedGenres As List(Of String)
        Get
            Return _selectgenres 
        End Get
        Set
            _selectgenres.Clear
            If Value.Count > 0 Then
                _selectgenres = Value
            End If
        End Set
    End Property

    Public Sub Init()
        PopCheckListBox
    End Sub

    Sub PopCheckListBox
        clbColumnsSelect.Items.Clear
        Dim chkd As Boolean
        Dim i   As Integer=0
        For Each genre In Form1.Genrelist
            chkd = False
            chkbxlst.Add(genre)
            Dim lbl As New Label
            lbl.text = genre
            clbColumnsSelect.Items.Add(lbl.Text)
            For Each g In _selectgenres
                If g.ToLower = genre.ToLower Then
                    chkd = True
                    Exit For
                End If
            Next
            clbColumnsSelect.SetItemChecked(i, chkd)
            i += 1
        Next
    End Sub

    Private Sub btnDone_Click( sender As Object,  e As EventArgs) Handles btnDone.Click
        RetrieveSelected
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click( sender As Object,  e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel 
    End Sub

    Private Sub RetrieveSelected
        Dim listof As New List(Of String)
        Dim show As Boolean
        Dim item As String
        listof.Clear()
        For i = 0 to clbColumnsSelect.Items.Count-1
            item = clbColumnsSelect.Items(i)
            show = clbColumnsSelect.GetItemChecked(i)
            If show Then listof.Add(item)
        Next
        SelectedGenres = listof
    End Sub

End Class
