Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq


Public Class MovieFilter

    Property Name        As String
    Property Tag         As Integer
    Property Visible     As Boolean
    Property QuickSelect As Boolean

    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        Name        = node.Attributes("name"        ).Value
        Tag         = node.Attributes("tag"         ).Value
        Visible     = node.Attributes("visible"     ).Value
        Try
            QuickSelect = node.Attributes("QuickSelect" ).Value
        Catch
            QuickSelect = False
        End Try
    End Sub

    Public Function GetChild(doc As XmlDocument) As XmlElement
        Dim child As XmlElement = doc.CreateElement("filter")

        child.SetAttribute("name"       , Name        )
        child.SetAttribute("tag"        , Tag         )
        child.SetAttribute("visible"    , Visible     )
        child.SetAttribute("QuickSelect", QuickSelect )

        Return child
    End Function
End Class


Public Class MovieFilters

    Property Border      As Integer =  4
    Property FilterSpace As Integer = 22
    Property Items       As New List(Of MovieFilter)
    Property Filtercount As Integer = 15     'Placed here to be able to clean config files of duplicate filters


    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        Dim x As Integer = 0                            'Found if multi profiles, filters were not cleared, but
        For Each child As XmlNode In node.ChildNodes    'added again and again.
            x += 1                                      'Count put in place to clean users config xml files
            Items.Add(New MovieFilter(child))           'back to single list of filters.
            If x = Filtercount Then Exit For
        Next
    End Sub
    Public Sub Reset()
        Items.Clear()
    End Sub


    Public Function GetChild(doc As XmlDocument) As XmlElement

        Dim child As XmlElement = doc.CreateElement("movie_filters")

        For Each item In Items
            child.AppendChild(item.GetChild(doc))
        Next

        Return child
    End Function



    Public Sub SetMovieFiltersVisibility(oPanel As Panel)
        Dim c       As Control
        Dim lbl     As Label
        Dim lblMode As Label
        Dim item    As MovieFilter

        For i=Items.Count-1 To 0 Step -1

            item    = Items(i)
            c       = oPanel.Controls(item.Name)
            lbl     = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2))
            lblMode = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2)+"Mode")

            c  .Tag     = item.Tag
            c  .Visible = item.Visible
            lbl.Visible = item.Visible

            If Not IsNothing(lblMode) Then
                lblMode.TextAlign = ContentAlignment.MiddleCenter
                lblMode.Visible   = item.Visible

                Dim filter As MC_UserControls.TriStateCheckedComboBox = c
                filter.QuickSelect = item.QuickSelect

                lblMode.Text = If(filter.QuickSelect, "S", "M" )
            End If
        Next
    End Sub


    Public Function GetMovieFilterPanelSize(oPanel As Panel) As Integer

        Dim count As Integer = (From c As Control In oPanel.Controls Where c.Name.IndexOf("cbFilter")=0 And c.Visible).Count

        Return (count*FilterSpace)+(Border*2) + 2
    End Function

    Public Sub PositionMovieFilters(oPanel As Panel)
        Dim index       As Integer =  1
        Dim count       As Integer =  0
        Dim Y           As Integer
        Dim lbl         As Label
        Dim lblMode     As Label
        Dim width       As Integer = oPanel.Width - 154

        Dim query = From c As Control In oPanel.Controls Where c.Name.IndexOf("cbFilter")=0 And c.Visible Order by Convert.ToInt16(c.Tag.ToString) Descending

        For Each c As Control In query
            lbl     = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2)       )
            lblMode = oPanel.Controls("lbl"+ c.Name.SubString(2,c.Name.Length-2)+"Mode")
            Y       = oPanel.Height - ((index*FilterSpace)+Border)

            c  .Width    = width
            c  .Location = New Point( c  .Location.X, Y )
            lbl.Location = New Point( lbl.Location.X, Y )

            If Not IsNothing(lblMode) Then
                lblMode.Location = New Point( lblMode.Location.X, Y )
            End If

            index += 1
        Next
    End Sub

    Public Function GetModeLabelName(c As Control)
        Return "lbl"+ c.Name.SubString(2,c.Name.Length-2)+"Mode"
    End Function


    Public Function GetItem(filterName As String) As MovieFilter
        For Each item In Items
            If item.Name=filterName Then Return item
        Next
        Return Nothing
    End Function

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

            If TypeName(c) = "TriStateCheckedComboBox" Then

                Dim lblMode As Label = oPanel.Controls(GetModeLabelName(c))

                item.QuickSelect = (lblMode.Text="S")
            End If

        Next
    End Sub

End Class
