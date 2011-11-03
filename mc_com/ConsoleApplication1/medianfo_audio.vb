Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure medianfo_audio
        Public language As String
        Public codec As String
        Public channels As String
        Public bitrate As String
    End Structure
End Namespace

