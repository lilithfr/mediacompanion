Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure medianfo_video
        Public width As String
        Public height As String
        Public aspect As String
        Public codec As String
        Public formatinfo As String
        Public duration As String
        Public bitrate As String
        Public bitratemode As String
        Public bitratemax As String
        Public container As String
        Public codecid As String
        Public codecinfo As String
        Public scantype As String
    End Structure
End Namespace

