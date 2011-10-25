Imports System
Imports System.Runtime.InteropServices

Namespace ConsoleApplication1
    'Public Class mediainfo
    '    ' Methods
    '    Public Sub Close()
    '        mediainfo.MediaInfo_Close(Me.Handle)
    '    End Sub

    '    Public Function Count_Get(ByVal StreamKind As StreamKind, ByVal Optional StreamNumber As UInt32 = &HFFFFFFFF) As Integer
    '        If (StreamNumber = UInt32.MaxValue) Then
    '            Dim num2 As Long = 0
    '            num2 = (num2 - 1)
    '            Return CInt(DirectCast(mediainfo.MediaInfo_Count_Get(Me.Handle, DirectCast(StreamKind, UIntPtr), DirectCast(num2, IntPtr)), UInt32))
    '        End If
    '        Return CInt(DirectCast(mediainfo.MediaInfo_Count_Get(Me.Handle, DirectCast(StreamKind, UIntPtr), DirectCast(StreamNumber, IntPtr)), UInt32))
    '    End Function

    '    Protected Overrides Sub Finalize()
    '        mediainfo.MediaInfo_Delete(Me.Handle)
    '    End Sub

    '    Public Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As Integer, ByVal Optional KindOfInfo As InfoKind = 1) As String
    '        Return Marshal.PtrToStringUni(mediainfo.MediaInfo_GetI(Me.Handle, DirectCast(StreamKind, UIntPtr), DirectCast(CULng(StreamNumber), UIntPtr), DirectCast(CULng(Parameter), UIntPtr), DirectCast(KindOfInfo, UIntPtr)))
    '    End Function

    '    Public Function Get_(ByVal StreamKind As StreamKind, ByVal StreamNumber As Integer, ByVal Parameter As String, ByVal Optional KindOfInfo As InfoKind = 1, ByVal Optional KindOfSearch As InfoKind = 0) As String
    '        Return Marshal.PtrToStringUni(mediainfo.MediaInfo_Get(Me.Handle, DirectCast(StreamKind, UIntPtr), DirectCast(CULng(StreamNumber), UIntPtr), Parameter, DirectCast(KindOfInfo, UIntPtr), DirectCast(KindOfSearch, UIntPtr)))
    '    End Function

    '    Public Function Inform() As String
    '        Return Marshal.PtrToStringUni(mediainfo.MediaInfo_Inform(Me.Handle, DirectCast(0, UIntPtr)))
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Sub MediaInfo_Close(ByVal Handle As IntPtr)
    '    End Sub

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_Count_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As IntPtr) As UIntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Sub MediaInfo_Delete(ByVal Handle As IntPtr)
    '    End Sub

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_Get(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef Parameter As String, ByVal KindOfInfo As UIntPtr, ByVal KindOfSearch As UIntPtr) As IntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_GetI(ByVal Handle As IntPtr, ByVal StreamKind As UIntPtr, ByVal StreamNumber As UIntPtr, ByVal Parameter As UIntPtr, ByVal KindOfInfo As UIntPtr) As IntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_Inform(ByVal Handle As IntPtr, ByVal Reserved As UIntPtr) As IntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_New() As IntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_Open(ByVal Handle As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef FileName As String) As UIntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_Option(ByVal Handle As IntPtr, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef Option_ As String, <MarshalAs(UnmanagedType.VBByRefStr)> ByRef Value As String) As IntPtr
    '    End Function

    '    <DllImport("MediaInfo.DLL", CharSet:=CharSet.Unicode, SetLastError:=True, ExactSpelling:=True)> _
    '    Private Shared Function MediaInfo_State_Get(ByVal Handle As IntPtr) As UIntPtr
    '    End Function

    '    Public Function Open(ByVal FileName As String) As Integer
    '        Return CInt(DirectCast(mediainfo.MediaInfo_Open(Me.Handle, FileName), UInt32))
    '    End Function

    '    Public Function Option_(ByVal Option__ As String, ByVal Optional Value As String = "") As String
    '        Return Marshal.PtrToStringUni(mediainfo.MediaInfo_Option(Me.Handle, Option__, Value))
    '    End Function

    '    Public Function State_Get() As Integer
    '        Return CInt(DirectCast(mediainfo.MediaInfo_State_Get(Me.Handle), UInt32))
    '    End Function


    '    ' Fields
    '    Private Handle As IntPtr = mediainfo.MediaInfo_New
    'End Class
End Namespace

