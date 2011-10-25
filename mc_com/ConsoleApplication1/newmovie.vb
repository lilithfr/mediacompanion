Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure newmovie
        Public nfopathandfilename As String
        Public nfopath As String
        Public title As String
        Public mediapathandfilename As String
    End Structure
End Namespace

