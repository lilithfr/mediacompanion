Imports ProtoXML
Namespace Tvdb
    Public Class ShowData
        Inherits ProtoFile

        Public Property Series As New SeriesList(Me, "Series")
    End Class
End Namespace