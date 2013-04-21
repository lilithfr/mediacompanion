Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq


Public Class MovieFilter
    Property Name    As String
    Property Tag     As Integer
    Property Visible As Boolean


    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        Name    = node.Attributes("name"    ).Value
        Tag     = node.Attributes("tag"     ).Value
        Visible = node.Attributes("visible" ).Value
    End Sub

    Public Function GetChild(doc As XmlDocument) As XmlElement
        Dim child As XmlElement = doc.CreateElement("filter")

        child.SetAttribute("name"   , Name    )
        child.SetAttribute("tag"    , Tag     )
        child.SetAttribute("visible", Visible )

        Return child
    End Function
End Class


Public Class MovieFilters

    Property Border      As Integer =  4
    Property FilterSpace As Integer = 28
    Property Items       As New List(Of MovieFilter)


    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        For Each child As XmlNode In node.ChildNodes
            Items.Add(New MovieFilter(child))
        Next
    End Sub


    Public Function GetChild(doc As XmlDocument) As XmlElement

        Dim child As XmlElement = doc.CreateElement("movie_filters")

        For Each item In Items
            child.AppendChild(item.GetChild(doc))
        Next

        Return child
    End Function



    Public Sub SetMovieFiltersVisibility(oPanel As Panel)
        Dim c     As Control
        Dim lbl   As Label
        Dim item  As MovieFilter

        For i=Items.Count-1 To 0 Step -1

            item = Items(i)
            c    = oPanel.Controls(item.Name)
            lbl  = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2))

            c  .Tag     = item.Tag
            c  .Visible = item.Visible
            lbl.Visible = item.Visible
        Next
    End Sub


    Public Function GetMovieFilterPanelSize(oPanel As Panel) As Integer

        Dim count As Integer = (From c As Control In oPanel.Controls Where c.Name.IndexOf("cbFilter")=0 And c.Visible).Count

        Return (count*FilterSpace)+(Border*2)
    End Function

    Public Sub PositionMovieFilters(oPanel As Panel)
        Dim index       As Integer =  1
        Dim count       As Integer =  0
        Dim Y           As Integer
        Dim lbl         As Label

        Dim query = From c As Control In oPanel.Controls Where c.Name.IndexOf("cbFilter")=0 And c.Visible Order by Convert.ToInt16(c.Tag.ToString) Descending

        For Each c As Control In query
            lbl = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2))
            Y   = oPanel.Height - (index*FilterSpace)

            c.Width      = 174
            c  .Location = New Point( c .Location.X, Y )
            lbl.Location = New Point( lbl.Location.X, Y )

            index += 1
        Next
    End Sub


    Public Sub UpdateFromPanel(oPanel As Panel)
        '
        'Add new since previous release
        '
        Dim query = From 
                        c As Control In oPanel.Controls 
                    Where
                        c.Name.IndexOf("cbFilter")=0 _
                    And
                        Not Items.Any(Function(x) x.Name = c.Name)
                    Select 
                        c

        For Each c As Control In query
            Dim item As New MovieFilter
            
            item.Name = c.Name
            Items.Add(item)
        Next


        For Each item in Items
            Dim c As Control = oPanel.Controls(item.Name)

            item.Tag     = c.Tag
            item.Visible = c.Visible 
        Next
    End Sub

End Class
