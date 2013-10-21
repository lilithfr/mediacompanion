Imports System.Net
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.Linq

Public Class frmMediaInfoEdit
    Dim edits As Boolean = False
    Private ReadOnly audch() As String = {"0", "1", "2", "2.1", "4", "4.1", 
                                          "5", "5.1", "6", "7.1", "8"}
    Private ReadOnly audcodec() As String = {"mp2", "mp3", "aac", "ac3", "dts",
                                             "truehd", "dtshd"}
    Private ReadOnly vidFormat() As String = {"avc", "avc1", "h264"}
    Private ReadOnly vidCodec() As String = {"h264", "h263"}
    Dim movie As FullMovieDetails = Form1.workingMovieDetails



End Class