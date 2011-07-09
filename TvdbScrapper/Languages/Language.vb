Imports ProtoXML

Namespace Tvdb
    Public Class Language
        Inherits ProtoPropertyGroup

        Public Property Language As New ProtoProperty(Me, "name")
        Public Property Abbreviation As New ProtoProperty(Me, "abbreviation")
        Public Property Id As New ProtoProperty(Me, "id")
    End Class
End Namespace