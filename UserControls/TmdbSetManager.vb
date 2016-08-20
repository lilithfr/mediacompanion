Imports System.Linq
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Class TmdbSetManager


#Region "Events"
    Public Event MoviesChanged
#End Region



#Region "Privates"
    Private _movies     As Movies
    Private _movieSetsView As List(Of TmdbCustomSetName)
#End Region



#Region "Properties"


    Property MoviesLst As Movies
        Get
            Return _movies
        End Get
        Set
           _movies = Value
           RaiseEvent MoviesChanged
        End Set
    End Property


    Public ReadOnly Property MovieSetsView 
        Get
            Return _movieSetsView 
        End Get
    End Property

    Property Dt As New DataTable
    

#End Region




#Region "Subs"


   'Public Sub New()
   '     InitializeComponent
   ' End Sub


    Public Sub Init
        AddHandler Me.MoviesChanged, AddressOf UpdateMovieSetsDependancies
   End Sub


    Public Sub UpdateMovieSetsDependancies
        UpdateMovieSetsDataTable
        PopDgvCustomSetNames
    End Sub


    Public Sub UpdateMovieSetsDataTable

        Dim q = From x As MovieSetInfo In MoviesLst.MovieSetDB Select x.MovieSetId, x.MovieSetName, x.UserMovieSetName

        dgvCustomSetNames.DataSource = q.CopyToDataTable()
    End Sub


    Public Sub PopDgvCustomSetNames
        'BsCustomSetNames.DataSource = MovieSetsView
        'dgvCustomSetNames.DataSource = BsCustomSetNames

        
    End Sub

    'Private Sub btnDone_Click( sender As Object,  e As EventArgs) 
    '    UpdateMovieSetDb

    '    Dim frm As Form= Me.Parent
        
    '    frm.Close
    'End Sub


    Sub UpdateMovieSetDb
        Dim dtChanges = dgvCustomSetNames.DataSource.GetChanges

        If IsNothing(dtChanges) Then Return

        For Each row As DataRow In dtChanges.Rows

            Dim MovieSetId       As String = row("MovieSetId"      ).ToString
            Dim UserMovieSetName As String = row("UserMovieSetName").ToString

            Dim MovieSet = MoviesLst.FindMovieSetInfoBySetId(MovieSetId)
            
            MovieSet.UserMovieSetName = UserMovieSetName

            MoviesLst.UpdateMovieCacheSetName(MovieSet)
        Next
    End Sub

#End Region



End Class
