Imports System
Imports System.Collections.Generic

Namespace ConsoleApplication1
    Public Class episodeinfo
        ' Fields
        Public aired As String
        Public credits As String
        Public director As String
        Public episodeno As String
        Public episodepath As String
        Public fanartpath As String
        Public filedetails As fullfiledetails = New fullfiledetails
        Public genre As String
        Public listactors As List(Of movieactors) = New List(Of movieactors)
        Public mediaextension As String
        Public playcount As String
        Public plot As String
        Public rating As String
        Public runtime As String
        Public seasonno As String
        Public thumb As String
        Public title As String
    End Class
End Namespace

