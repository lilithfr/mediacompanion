Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Xml 


Public Class Fanarttv
    Public Function get_fanart_list(ByVal ID As String, Optional ByVal s As String = "movie")
        Monitor.Enter(Me)
        Try
            Dim fanartlinecount As Integer = 0
            Dim apple2(10000)
            Dim fanarttvxml As String
            Dim fanarturl2 As String = ""
            If s = "movie" Then
                fanarturl2 = String.Format("http://webservice.fanart.tv/v3/movies/{0}?api_key=ed4b784f97227358b31ca4dd966a04f1", ID)
            ElseIf s = "tv" Then
                fanarturl2 = String.Format("http://webservice.fanart.tv/v3/tv/{0}?api_key=ed4b784f97227358b31ca4dd966a04f1", ID)
            ElseIf s <> "movie" and s <> "tv" Then
                Return Nothing
            End If

            Dim wrGETURL2 As WebRequest
            wrGETURL2 = WebRequest.Create(fanarturl2)
            Dim myProxy2 As New WebProxy("myproxy", 80)
            myProxy2.BypassProxyOnLocal = True
            Dim objStream2 As Stream
            objStream2 = wrGETURL2.GetResponse.GetResponseStream()
            Dim objReader2 As New StreamReader(objStream2)
            'Dim sLine2 As String = ""
            'fanartlinecount = 0

            'Do While Not sLine2 Is Nothing
            '    fanartlinecount += 1
            '    sLine2 = objReader2.ReadLine
            '    apple2(fanartlinecount) = sLine2
            'Loop
            'fanartlinecount -= 1

            fanarttvxml = objReader2.ReadToEnd
            objReader2.Close()
            
            Return JsonToXml(fanarttvxml) 
        Catch ex As Exception
            Return Nothing
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Private Function JsonToXml(ByVal json As String) As XmlDocument
        Dim newNode As XmlNode = Nothing
        Dim appendToNode As XmlNode = Nothing
        Dim returnXmlDoc As XmlDocument = New XmlDocument
        returnXmlDoc.LoadXml("<Document />")
        Dim rootNode As XmlNode = returnXmlDoc.SelectSingleNode("Document")
        appendToNode = rootNode

        json = json.Replace(",", "," & vbLf)
        json = json.Replace("{", "{" & vbLf)
        json = json.Replace("}", vbLf & "}" & vbLf)
        json = json.Replace("[", "[" & vbLf)
        json = json.Replace("]", vbLf & "]" & vbLf)

        'Dim arrElementData() As String
        Dim arrElements() As String = json.Split(vbLf)
        For Each element As String In arrElements
            Dim processElement As String = element.Replace(vbLf, "").Replace(vbCr, "").Replace(vbTab, "").Trim()
            If processElement.Trim <> "" Then
                If (processElement.IndexOf("}") > -1 Or processElement.IndexOf("]") > -1) And Not (appendToNode Is rootNode) Then
                    appendToNode = appendToNode.ParentNode
                ElseIf (processElement.IndexOf("[") > -1) Then
                    processElement = processElement.Replace(":", "").Replace("[", "").Replace("""", "").Trim()
                    If processElement.Trim = "" Then
                        processElement = "Square"
                    End If
                    newNode = returnXmlDoc.CreateElement(processElement)
                    appendToNode.AppendChild(newNode)
                    appendToNode = newNode
                ElseIf (processElement.IndexOf("{") > -1) Then
                    processElement = processElement.Replace(":", "").Replace("{", "").Replace("""", "").Trim()
                    If processElement.Trim = "" Then
                        processElement = "Element"
                    End If
                    newNode = returnXmlDoc.CreateElement(processElement)
                    appendToNode.AppendChild(newNode)
                    appendToNode = newNode
                Else
                    Dim FoundAt As Integer = processElement.IndexOf(":")
                    If (FoundAt > -1) Then
                        Dim NodeName As String = processElement.Substring(0, FoundAt - 1).Replace("""", "")
                        Dim NodeValue As String = processElement.Substring(FoundAt + 1, processElement.Length - FoundAt - 1).Trim
                        If NodeValue.StartsWith("""") Then
                            NodeValue = NodeValue.Substring(1, NodeValue.Length - 1).Trim
                        End If
                        If NodeValue.EndsWith(",") Then
                            NodeValue = NodeValue.Substring(0, NodeValue.Length - 1).Trim
                        End If
                        If NodeValue.EndsWith("""") Then
                            NodeValue = NodeValue.Substring(0, NodeValue.Length - 1).Trim
                        End If
                        newNode = returnXmlDoc.CreateElement(NodeName)
                        newNode.InnerText = NodeValue
                        appendToNode.AppendChild(newNode)
                    End If
                End If
            End If
        Next
        Return returnXmlDoc
    End Function

End Class
