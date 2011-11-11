Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure combolist
        Public fullpathandfilename As String
        Public movieset As String
        Public filename As String
        Public foldername As String
        Public title As String
        Public titleandyear As String
        Public year As String
        Public filedate As String
        Public id As String
        Public rating As String
        Public top250 As String
        Public genre As String
        Public playcount As String
        Public sortorder As String
        Public outline As String
        Public runtime As String
        Public createdate As String
        Public missingdata1 As Byte
    End Structure
End Namespace

