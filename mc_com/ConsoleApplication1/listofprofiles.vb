Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure listofprofiles
        Public moviecache As String
        Public tvcache As String
        Public actorcache As String
        Public profilename As String
        Public regexlist As String
        Public filters As String
        Public config As String
    End Structure
End Namespace

