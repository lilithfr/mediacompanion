Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure batchwizard
        Public title As Boolean
        Public votes As Boolean
        Public rating As Boolean
        Public top250 As Boolean
        Public runtime As Boolean
        Public director As Boolean
        Public year As Boolean
        Public outline As Boolean
        Public plot As Boolean
        Public tagline As Boolean
        Public genre As Boolean
        Public studio As Boolean
        Public premiered As Boolean
        Public mpaa As Boolean
        Public trailer As Boolean
        Public credits As Boolean
        Public posterurls As Boolean
        Public actors As Boolean
        Public mediatags As Boolean
        Public missingposters As Boolean
        Public missingfanart As Boolean
        Public activate As Boolean
    End Structure
End Namespace

