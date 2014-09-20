Imports System.Linq
Imports System.Collections.Generic
Imports System.IO
Imports System.xml

Public Class FanartTv

    Public Const FANARTTV_EXC_MSG = "FanartTV is unavailable!"
    
    #Region "Read-write Properties"

    Public Property ID As String
    Private Property _data               As XmlDocument 
    Private Property hdmovieclearart     As New List(Of str_fanarttvart)
    Private Property hdmovielogo         As New List(Of str_fanarttvart)
    Private Property movielogo           As New List(Of str_fanarttvart)
    Private Property movieart            As New List(Of str_fanarttvart)
    Private Property moviebackground     As New List(Of str_fanarttvart)
    Private Property moviedisc           As New List(Of str_fanarttvart)
    Private Property moviebanner         As New List(Of str_fanarttvart)
    Private Property moviethumb          As New List(Of str_fanarttvart)
    Private Property movieposter         As New List(Of str_fanarttvart)
    Private _fanartlist                  As New FanartTvList

    #End Region 'Read-write properties

    #Region "Read-only Properties"

    Private _fetched                As Boolean = False

    Public ReadOnly Property Fanarttvresults As FanartTvList 
        Get
            Fetch
            Return _fanartlist
        End Get
    End Property

#End Region  'Read-only properties

    Sub new( Optional NewID As String=Nothing )
        _id         = NewID
    End Sub

    Private Sub Fetch
        Try
            If ID <> "" And Not _fetched Then

                _fetched = True

                Dim rhs As List(Of RetryHandler) = New List(Of RetryHandler)

                rhs.Add(New RetryHandler(AddressOf GetFanartTvData))

                If Not Utilities.UrlIsValid("http://webservice.fanart.tv/v3/movies/87818?api_key=ed4b784f97227358b31ca4dd966a04f1") Then
                    Throw New Exception("FanartTV is offline")
                End If

                For Each rh In rhs
                    If Not rh.Execute Then Throw New Exception(FANARTTV_EXC_MSG)
                Next
                Assignartwork()
                Allocatelists()
            End If
        Catch ex As Exception
            Throw New Exception (ex.Message)
        End Try
    End Sub

    Function GetFanartTvData As Boolean
        Dim newobject As New class_fanart_tv.Fanarttv
        _data = newobject.get_fanart_list(ID)
        Return Not IsNothing(_data)
    End Function

    Private Sub Allocatelists()
        _fanartlist.hdmovieclearart.AddRange(hdmovieclearart)
        _fanartlist.hdmovielogo.AddRange(hdmovielogo)
        _fanartlist.movielogo.AddRange(movielogo)
        _fanartlist.movieart.AddRange(movieart)
        _fanartlist.moviebackground.AddRange(moviebackground)
        _fanartlist.moviedisc.AddRange(moviedisc)
        _fanartlist.moviebanner.AddRange(moviebanner)
        _fanartlist.moviethumb.AddRange(moviethumb)
        _fanartlist.movieposter.AddRange(movieposter)
    End Sub

    Private Sub Assignartwork()
        Dim thisresult As XmlNode = Nothing
        Dim tempid As String = ""
        For Each thisresult In _data("Document")
            Select Case thisresult.Name
                Case "Element"
                    Dim detail1 As XmlNode = Nothing
                    For Each detail1 In thisresult.ChildNodes
                        Select Case detail1.Name
                            Case "hdmovieclearart"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim hdmovclearart As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        hdmovclearart.id = detail3.InnerText
                                                    Case "url"
                                                        hdmovclearart.url = detail3.InnerText
                                                        hdmovclearart.urlpreview = hdmovclearart.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        hdmovclearart.lang = detail3.InnerText
                                                    Case "likes"
                                                        hdmovclearart.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            hdmovieclearart.Add(hdmovclearart)
                                    End Select
                                Next
                            Case "hdmovielogo"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            hdmovielogo.Add(artwork)
                                    End Select
                                Next
                            Case "movielogo"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            movielogo.Add(artwork)
                                    End Select
                                Next
                            Case "movieart"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            movieart.Add(artwork)
                                    End Select
                                Next
                            Case "moviebackground"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            moviebackground.Add(artwork)
                                    End Select
                                Next
                            Case "moviedisc"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            moviedisc.Add(artwork)
                                    End Select
                                Next
                            Case "moviebanner"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            moviebanner.Add(artwork)
                                    End Select
                                Next
                            Case "moviethumb"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            moviethumb.Add(artwork)
                                    End Select
                                Next
                            Case "movieposter"
                                Dim detail2 As XmlNode = Nothing
                                For Each detail2 in detail1.ChildNodes 
                                    Select Case detail2.Name
                                        Case "Element"
                                            Dim artwork As New str_fanarttvart
                                            Dim detail3 As XmlNode = Nothing
                                            For each detail3 In detail2.ChildNodes
                                                Select Case detail3.Name
                                                    Case "id"
                                                        artwork.id = detail3.InnerText
                                                    Case "url"
                                                        artwork.url = detail3.InnerText
                                                        artwork.urlpreview = artwork.url.Replace("tv/fanart/", "tv/preview/")
                                                    Case "lang"
                                                        artwork.lang = detail3.InnerText
                                                    Case "likes"
                                                        artwork.likes = detail3.InnerText.ToInt
                                                End Select
                                            Next
                                            movieposter.Add(artwork)
                                    End Select
                                Next
                        End Select
                    Next
            End Select
        Next
    End Sub
End Class
