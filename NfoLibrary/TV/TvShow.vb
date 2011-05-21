Imports ProtoXML

Namespace Test

    Public Class TvShow
        Inherits ProtoFile

        Public Title As New ProtoProperty(Me, "title")
        Public Plot As New ProtoProperty(Me, "plot")
        Public Runtime As New ProtoProperty(Me, "runtime") 'With {.Value = "0:00"}
        Public Rating As New ProtoProperty(Me, "rating")
        Public Something As New ProtoProperty(Me, "something")
        Public Something2 As New ProtoProperty(Me, "something2")


    End Class

End Namespace