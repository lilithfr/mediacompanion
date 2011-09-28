Imports System
Imports System.Collections.Generic

Namespace ConsoleApplication1
    Public Class tvshownfo
        ' Fields
        Public episodeactorsource As String
        Public episodeguideurl As String
        Public fanart As List(Of String) = New List(Of String)
        Public fanartpath As String
        Public genre As String
        Public imdbid As String
        Public language As String
        Public listactors As List(Of movieactors) = New List(Of movieactors)
        Public locked As Integer
        Public mpaa As String
        Public path As String
        Public plot As String
        Public posterpath As String
        Public posters As List(Of String) = New List(Of String)
        Public premiered As String
        Public rating As String
        Public runtime As String
        Public sortorder As String
        Public status As String
        Public studio As String
        Public title As String
        Public trailer As String
        Public tvdbid As String
        Public tvshowactorsource As String
        Public year As String
    End Class
End Namespace

