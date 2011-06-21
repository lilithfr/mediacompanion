Imports ProtoXML

Namespace Tvdb
    Public Class Actors
        Inherits ProtoFile

        Public Property Items As New ActorList(Me, "Actor")
    End Class
End Namespace
