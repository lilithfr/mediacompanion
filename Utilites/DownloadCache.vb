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

    Public Shared Function DownloadFileAndCache(ByVal URL As String, Optional ByVal Path As String = "", _
                                          Optional ByVal ForceDownload As Boolean = False, _
                                          Optional ByVal resizeFanart As Integer = 0, _
                                          Optional ByRef strValue As String = "") As Boolean

        Utilities.EnsureFolderExists(CacheFolder)
        Dim returnCode As Boolean = True
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        If Not File.Exists(CachePath) OrElse ForceDownload Then
            Try
                Dim webReq As HttpWebRequest = WebRequest.Create(URL)
                webReq.AllowAutoRedirect = True
                webReq.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate

                Using webResp As HttpWebResponse = webReq.GetResponse()
                    Using responseStreamData As Stream = webResp.GetResponseStream()
                        'got a response - should probably put a Try...Catch in here for filesystem stuff, but I'll wing it for now.
                        If String.IsNullOrEmpty(Path) Then
                            IO.File.WriteAllText(CachePath, New StreamReader(responseStreamData, Encoding.UTF8).ReadToEnd)
                        Else
                            If (File.Exists(CachePath)) Then
                                File.Delete(CachePath)
                            End If
                            Using fileStream As New FileStream(CachePath, FileMode.OpenOrCreate, FileAccess.Write)
                                Dim buffer(webResp.ContentLength) As Byte
                                Dim bytesRead = responseStreamData.Read(buffer, 0, buffer.Length)
                                While bytesRead > 0
                                    fileStream.Write(buffer, 0, bytesRead)
                                    bytesRead = responseStreamData.Read(buffer, 0, buffer.Length)
                                End While
                            End Using
                        End If
                    End Using
                End Using

            Catch ex As WebException
                Using errorResp As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                    Using errorRespStream As Stream = errorResp.GetResponseStream()
                        Dim errorText As String = New StreamReader(errorRespStream).ReadToEnd()
                        Utilities.tvScraperLog &= String.Format("**** Scraper Error: Code {0} ****{3}     {2}{3}", _
                                                                errorResp.StatusCode, errorText, vbCrLf)
                    End Using
                End Using
                returnCode = False
            End Try

        End If

        If String.IsNullOrEmpty(Path) Then
            strValue = IO.File.ReadAllText(CachePath)
        Else
            Utilities.copyImage(CachePath, Path, resizeFanart)
        End If

        DownloadFileAndCache = returnCode
    End Function

End Class






