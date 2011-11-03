Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure filedetails
        Public fullpathandfilename As String
        Public path As String
        Public filename As String
        Public foldername As String
        Public fanartpath As String
        Public posterpath As String
        Public trailerpath As String
        Public createdate As String
    End Structure
End Namespace

