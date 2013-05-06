Imports System.ComponentModel
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Media_Companion
Imports System.Text
Imports YouTubeFisher

Public Class CertificateMapping
    Public Property Find    As String 
    Public Property Replace As String 

   
    Sub New
    End Sub

    Sub New(find As String, replace As String)
        Me.Find    = find
        Me.Replace = replace
    End Sub
End Class

Public Class CertificateMappings

    Public MappingsFile        = Preferences.applicationPath & "\classes\CertificateMappings.xml"
    Public DefaultMappingsFile = Preferences.applicationPath & "\classes\DefaultCertificateMappings.xml"

    Public Property List As New List(Of CertificateMapping)

    Public ReadOnly Property XDoc As XDocument
        Get
            If Not File.Exists(MappingsFile) Then
                File.Copy(DefaultMappingsFile,MappingsFile)
            End If

            Return XDocument.Load(MappingsFile)
        End Get 
    End Property


    Sub New
        List.Clear
        
        Dim q = From x In XDoc.Descendants("Certificate")
                            Select 
                                Find    = x.Attribute("find"   ).Value,
                                Replace = x.Attribute("replace").Value
                            Order By 
                                Find
                    
        For Each item In q
            List.Add( New CertificateMapping(item.find,item.replace) )
        Next
    End Sub


    Function GetMapping(certificate As String) As String
        
        Dim q = From x In List Select x Where x.find = certificate

        If q.Count=1 Then Return q(0).Replace


        q = From x In List Select x Where x.Find.GetLastChar="*" and certificate.IndexOf(x.Find.RemoveLastChar)=0

        If q.Count=1 Then Return q(0).Replace

        Return certificate
    End Function


End Class
