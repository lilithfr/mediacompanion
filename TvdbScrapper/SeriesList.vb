﻿Namespace Tvdb
    Public Class SeriesList
        Inherits ProtoXML.ProtoFlatList(Of Series)

        Private Sub New()
            MyBase.New(Nothing, Nothing)
            Throw New NotImplementedException()
        End Sub

        Public Sub New(ByRef Parent As ProtoXML.IProtoXBase, ByVal NodeName As String)
            MyBase.New(Parent, NodeName)
        End Sub


        Public Overrides Sub ProcessNode(ByRef Element As System.Xml.Linq.XElement)

            Dim NewActor As New Series
            NewActor.ParentClass = Me.ParentClass

            NewActor.ProcessNode(Element)

            Me.Add(NewActor)

        End Sub
    End Class
End Namespace