Imports System.Security.Cryptography
Imports System.Net
Imports System.IO
Imports System.IO.Compression
Imports System.Text

Public Class DownloadCache

    Public Shared Property CacheFolder As String

    Private Shared Function GetCacheFileName(ByVal URL As String) As String
        Dim Buffer As Byte() = Utilities.ComputeHashValueToByte(URL)

        Dim Extention As String = URL.Split("?")(0)
        Extention = IO.Path.GetExtension(Extention)

        Dim Result As String = ""
        For Each Item As Byte In Buffer
            Result &= Conversion.Hex(Item)
        Next

        Return Result & Extention
    End Function

    Public Shared Sub DownloadFileToDisk(ByVal URL As String, ByVal Path As String, Optional ByVal ForceDownload As Boolean = False)
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        Utilities.EnsureFolderExists(CacheFolder)

        Dim size As Integer = 0
        Dim bytesRead As Integer = 0
        Dim webReq As HttpWebRequest = WebRequest.Create(URL)

        If IO.File.Exists(CachePath) AndAlso Not ForceDownload Then
            webReq.IfModifiedSince = IO.File.GetCreationTimeUtc(CachePath)
        End If

        webReq.AllowAutoRedirect = True
        'req.AllowWriteStreamBuffering = True

        Dim webResp As HttpWebResponse = webReq.GetResponse()

        If webResp.StatusCode = HttpStatusCode.NotModified Then
            IO.File.Copy(CachePath, Path)

            Exit Sub
        End If

        Dim contents As Stream = webResp.GetResponseStream()
        Dim Reader As New StreamReader(contents)

        Dim buffer(webResp.ContentLength) As Byte
        Dim bytesToRead As Integer = CInt(buffer.Length - 1)
        While bytesToRead > 0
            size = contents.Read(buffer, bytesRead, bytesToRead)
            If size = 0 Then Exit While
            bytesToRead -= size
            bytesRead += size
        End While

        Utilities.EnsureFolderExists(Path.ToString.Replace(IO.Path.GetFileName(Path.ToString), ""))

        Dim fstrm As New FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write)

        fstrm.Write(buffer, 0, buffer.Length)
        fstrm.Close()

        Dim Cache As New FileStream(CachePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        Cache.Write(buffer, 0, buffer.Length)
        fstrm.Close()
    End Sub

    Public Shared Function DownloadFileToString(ByVal URL As String, Optional ByVal ForceDownload As Boolean = False) As String
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        Utilities.EnsureFolderExists(CacheFolder)

        Dim webReq As HttpWebRequest = WebRequest.Create(URL)

        webReq.IfModifiedSince = IO.File.GetCreationTimeUtc(CachePath)

        Dim html As String
        Using webResp As HttpWebResponse = webReq.GetResponse()

            If IO.File.Exists(CachePath) AndAlso Not ForceDownload Then
                If webResp.StatusCode = HttpStatusCode.NotModified Then
                    Return IO.File.ReadAllText(CachePath)
                End If
            End If

            Dim responseStream As Stream = webResp.GetResponseStream()
            'MsgBox("Encoding was: " & webResp.ContentEncoding.ToLower())   'uncomment this line to show encoding found....empty string means text format
            If (webResp.ContentEncoding.ToLower().Contains("gzip")) Then
                responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                Utilities.tvScraperLog &= "**** TVDB Returned GZIP Encoded *****" & vbCrLf
            ElseIf (webResp.ContentEncoding.ToLower().Contains("deflate")) Then
                responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                Utilities.tvScraperLog &= "**** TVDB Returned DEFLATE Encoded *****" & vbCrLf
            Else
                Utilities.tvScraperLog &= "**** TVDB Returned TEXT Stream *****" & vbCrLf
            End If
            Dim reader As StreamReader = New StreamReader(responseStream, Encoding.UTF8)

            html = reader.ReadToEnd()
            IO.File.WriteAllText(CachePath, html)
         
            responseStream.Close()

        End Using
        Return html
    End Function

    Public Shared Function DownloadFileTo(ByVal URL As String, Optional ByVal Path As String = "", Optional ByVal ForceDownload As Boolean = False, _
                                          Optional ByVal resize As Boolean = False, Optional ByRef strValue As String = "") As Boolean
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)
        Dim NotModifiedData As Boolean = False

        Utilities.EnsureFolderExists(CacheFolder)

        Try
            Dim webReq As HttpWebRequest = WebRequest.Create(URL)
            webReq.IfModifiedSince = IO.File.GetCreationTimeUtc(CachePath)
            webReq.AllowAutoRedirect = True

            Using webResp As HttpWebResponse = webReq.GetResponse()
                If IO.File.Exists(CachePath) AndAlso Not ForceDownload AndAlso webResp.StatusCode = HttpStatusCode.NotModified Then
                    NotModifiedData = True
                End If
                Dim responseStreamData As Stream = webResp.GetResponseStream()
                If Path = "" Then
                    'DownloadFileToString
                    If NotModifiedData Then
                        strValue = IO.File.ReadAllText(CachePath)
                    Else
                        'MsgBox("Encoding was: " & webResp.ContentEncoding.ToLower())   'uncomment this line to show encoding found....empty string means text format
                        If (webResp.ContentEncoding.ToLower().Contains("gzip")) Then
                            responseStreamData = New GZipStream(responseStreamData, CompressionMode.Decompress)
                            Utilities.tvScraperLog &= "**** TVDB Returned GZIP Encoded *****" & vbCrLf
                        ElseIf (webResp.ContentEncoding.ToLower().Contains("deflate")) Then
                            responseStreamData = New DeflateStream(responseStreamData, CompressionMode.Decompress)
                            Utilities.tvScraperLog &= "**** TVDB Returned DEFLATE Encoded *****" & vbCrLf
                        Else
                            Utilities.tvScraperLog &= "**** TVDB Returned TEXT Stream *****" & vbCrLf
                        End If
                        Dim html As String = New StreamReader(responseStreamData, Encoding.UTF8).ReadToEnd()
                        IO.File.WriteAllText(CachePath, html)
                        strValue = html
                    End If
                Else
                    'DownloadFileToDisk
                    If NotModifiedData Then
                        IO.File.Copy(CachePath, Path)
                    Else
                        Dim size As Integer = 0
                        Dim bytesRead As Integer = 0
                        Dim buffer(webResp.ContentLength) As Byte
                        Dim bytesToRead As Integer = CInt(buffer.Length - 1)
                        While bytesToRead > 0
                            size = responseStreamData.Read(buffer, bytesRead, bytesToRead)
                            If size = 0 Then Exit While
                            bytesToRead -= size
                            bytesRead += size
                        End While

                        Utilities.EnsureFolderExists(Path.ToString.Replace(IO.Path.GetFileName(Path.ToString), ""))

                        If resize Then
                            Dim bmp As New Drawing.Bitmap(responseStreamData)
                            ' NOTE: HueyHQ 26-Jan-2012
                            ' Currently there are still sometimes errors when scraping images, so the idea here is to get
                            ' all web requests into this one function so that all errors are handled more gracefully. 
                            ' The many independant image scraping calls vary to certain degrees, so I am trying to amalgamate
                            ' them without breaking functionality.
                            ' It could take some time.
                            ' The call for this is centralised from Utilities.vb:DownloadImage but no code calls it as yet,
                            ' so hopefully this delivery doesn't break anything.
                        Else
                            Dim fstrm As New FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write)
                            fstrm.Write(buffer, 0, buffer.Length)
                            fstrm.Close()
                        End If

                        Dim Cache As New FileStream(CachePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                        Cache.Write(buffer, 0, buffer.Length)
                        Cache.Close()
                    End If
                End If
                responseStreamData.Close()
            End Using

        Catch ex As WebException
            Using webResp As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                Utilities.tvScraperLog &= "**** Scraper Error code: " & webResp.StatusCode & " *****" & vbCrLf
                Using responseStreamData As Stream = webResp.GetResponseStream()
                    Dim text As String = New StreamReader(responseStreamData).ReadToEnd()
                    Utilities.tvScraperLog &= text & vbCrLf
                End Using
            End Using
            Return False
        End Try

        Return True
    End Function

End Class






