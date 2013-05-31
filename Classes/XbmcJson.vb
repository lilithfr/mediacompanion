Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Media_Companion
Imports System.Text
Imports XBMC.JsonRpc
Imports Newtonsoft.Json.Linq

Public Class XbmcJson

    Private _opened     As Boolean = False
    Private _xbmcMovies As List(Of MinXbmcMovie)

    Public Property Address  As String = Preferences.XBMC_Address
    Public Property Port     As String = Preferences.XBMC_Port
    Public Property Username As String = Preferences.XBMC_Username
    Public Property Password As String = Preferences.XBMC_Password

    Public Property xbmc     As XbmcJsonRpcConnection


    Public ReadOnly Property Opened As Boolean
        Get
            Return _opened
        End Get
    End Property


    Public ReadOnly Property XbmcMovies As List(Of MinXbmcMovie)
        Get
            Return _xbmcMovies
        End Get
    End Property


    Sub New
        xbmc = new XbmcJsonRpcConnection(Address, Port, Username, Password)

        'Open

        'If Not Opened Then Return
        
        'UpdateXbmcMovies
    End Sub


    Sub Open
        _opened = xbmc.Open
    End Sub


    Sub UpdateXbmcMovies
        Dim content As Object() = {"file"}

        _xbmcMovies = xbmc.Library.Video.GetMinXbmcMovies(  new JProperty("properties", content )  )
    End Sub


    Function GetMovieId(MediaName As String)  As Integer

        Dim q = From m In _xbmcMovies Where m.file.ToUpper = MediaName.ToUpper

        If q.Count=0 Then Return -1

        Return q.First.movieid
    End Function

End Class
