Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure basicmovienfo
        Public title As String
        Public sortorder As String
        Public movieset As String
        Public year As String
        Public rating As String
        Public votes As String
        Public outline As String
        Public plot As String
        Public tagline As String
        Public runtime As String
        Public mpaa As String
        Public genre As String
        Public credits As String
        Public director As String
        Public premiered As String
        Public studio As String
        Public trailer As String
        Public playcount As String
        Public imdbid As String
        Public top250 As String
        Public filename As String
        Public thumbnails As String
        Public fanart As String
    End Structure
End Namespace

