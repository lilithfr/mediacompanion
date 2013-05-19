Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq


Public Class ExcludeTag

    Property Name As String

    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        Name = node.Attributes("name").Value
    End Sub

    Public Function GetChild(doc As XmlDocument) As XmlElement
        Dim child As XmlElement = doc.CreateElement("Exclude")

        child.SetAttribute("name" , Name )

        Return child
    End Function
End Class


Public Class Excludes

    Property Type  As String = ""
    Property Items As New List(Of ExcludeTag)


    Public Sub New(type As String)
        Me.Type = type
    End Sub


    Public Sub Load(node As XmlNode)
        For Each child As XmlNode In node.ChildNodes
            Items.Add(New ExcludeTag(child))
        Next
    End Sub


    Public Function GetChild(doc As XmlDocument) As XmlElement

        Dim child As XmlElement = doc.CreateElement("Exclude" + Type)

        For Each item In Items
            child.AppendChild(item.GetChild(doc))
        Next

        Return child
    End Function


    Function Match(name As String) As String
        
        Dim q = From x In Items Select x Where x.Name.ToUpper = name.ToUpper

        If q.Count=1 Then Return True

        q = From x In Items Select x Where x.Name.GetLastChar="*" and name.ToUpper.IndexOf(x.Name.ToUpper.RemoveLastChar)=0

        If q.Count=1 Then Return True

        Return False
    End Function

End Class
