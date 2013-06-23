Imports XBMC.JsonRpc

Public Class XBMC_Movies_EventArgs : Inherits EventArgs

    Public Property XbmcMovies As List(Of MaxXbmcMovie)

    Sub New(ByVal XbmcMovies As List(Of MaxXbmcMovie))
        Me.XbmcMovies = XbmcMovies
    End Sub

End Class
