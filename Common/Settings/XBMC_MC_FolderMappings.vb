Imports System.Xml
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq
Imports System.IO


Public Class XBMC_MC_FolderMapping

 '   XBMC_MC_FolderMappings As New Dictionary(Of String, String)
    Property MC   As String
    Property XBMC As String


    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        MC   = node.Attributes("MC"  ).Value
        XBMC = node.Attributes("XBMC").Value
    End Sub

    Public Function GetChild(doc As XmlDocument) As XmlElement
        Dim child As XmlElement = doc.CreateElement("XBMC_MC_FolderMapping")

        child.SetAttribute("MC"   , MC  )
        child.SetAttribute("XBMC" , XBMC)

        Return child
    End Function
End Class


Public Class XBMC_MC_FolderMappings

    Property Items As New List(Of XBMC_MC_FolderMapping)

    Public Sub New
    End Sub

    Public Sub New(node As XmlNode)
        Load(node)
    End Sub

    Public Sub Load(node As XmlNode)
        For Each child As XmlNode In node.ChildNodes
            Items.Add(New XBMC_MC_FolderMapping(child))
        Next
    End Sub

    Public Function GetChild(doc As XmlDocument) As XmlElement

        Dim child As XmlElement = doc.CreateElement("XBMC_MC_FolderMappings")

        For Each item In Items
            child.AppendChild(item.GetChild(doc))
        Next

        Return child
    End Function

    Public Function GetItem(key As String) As XBMC_MC_FolderMapping
        For Each item In Items
            If item.MC.ToUpper=key.ToUpper Then Return item
        Next
        Return Nothing
    End Function

    Public Function GetXBMC_MoviePath(McMoviePath As String) As String

        For Each FolderMapping In Items
            If McMoviePath.ToUpper.StartsWith(FolderMapping.MC.ToUpper) Then

                Dim file As String = Right(McMoviePath,McMoviePath.Length-FolderMapping.MC.Length)

                If file.StartsWith(Path.DirectorySeparatorChar) Then
                    file = file.Remove(0,1)
                End If

                Return Path.Combine(FolderMapping.XBMC,file )
            End If
        Next

        Return Nothing
      End Function

    Public Function GetMC_MoviePath(XbMoviePath As String) As String

        Dim match As String = ""
        
        Dim MoviePath As String = XbMoviePath
        
        If MoviePath.IndexOf("stack://")=0 Then
            Dim LenStackPlusOne As Integer = Len("stack://")+1
            MoviePath = Mid(MoviePath,LenStackPlusOne,MoviePath.IndexOf(",")-LenStackPlusOne)
        End If


        For Each FolderMapping In Items
            If MoviePath.ToUpper.StartsWith(FolderMapping.XBMC.ToUpper) Then

                Dim file As String = Right(MoviePath,MoviePath.Length-FolderMapping.XBMC.Length)

                If file.StartsWith(Path.DirectorySeparatorChar) Then
                    file = file.Remove(0,1)
                End If

                Return Path.Combine(FolderMapping.MC,file )
            End If
        Next

        'Missing folder mapping -> Assume same
        Return XbMoviePath
      End Function

    Public Function GetMC_MovieFolder(McMoviePath As String) As String

        For Each FolderMapping In Items
            If McMoviePath.ToUpper.StartsWith(FolderMapping.MC.ToUpper) Then
                Return FolderMapping.MC.ToUpper
            End If
        Next

        Return Nothing
      End Function

End Class
