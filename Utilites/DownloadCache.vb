Imports System.Security.Cryptography
Imports System.Net
Imports System.IO
Imports System.IO.Compression
Imports System.Text

Public Class DownloadCache

    Public Shared Property CacheFolder As String

    Private Shared Function DownloadFileToByte(ByVal URL As String) As Byte()

        Dim size As Integer = 0
        Dim bytesRead As Integer = 0
        Dim req As HttpWebRequest = WebRequest.Create(URL)
        req.AllowAutoRedirect = True
        'req.AllowWriteStreamBuffering = True

        Dim res As HttpWebResponse = req.GetResponse()

        Dim contents As Stream = res.GetResponseStream()
        Dim Reader As New StreamReader(contents)


        Dim buffer(res.ContentLength) As Byte
        Dim bytesToRead As Integer = CInt(buffer.Length - 1)
        While bytesToRead > 0
            size = contents.Read(buffer, bytesRead, bytesToRead)
            If size = 0 Then Exit While
            bytesToRead -= size
            bytesRead += size
        End While

        Return buffer
    End Function

    Public Shared Sub DownloadFileToDisk(ByVal URL As String, ByVal Path As String, Optional ByVal ForceDownload As Boolean = False)
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        Utilities.EnsureFolderExists(CacheFolder)

        If IO.File.Exists(CachePath) AndAlso Not ForceDownload Then
            Exit Sub
        End If

        Dim buffer() As Byte = DownloadFileToByte(URL)

        Utilities.EnsureFolderExists(Path.ToString.Replace(IO.Path.GetFileName(Path.ToString), ""))

        Dim fstrm As New FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write)

        fstrm.Write(buffer, 0, buffer.Length)
        fstrm.Close()

        Dim Cache As New FileStream(CachePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
        Cache.Write(buffer, 0, buffer.Length)
        fstrm.Close()
    End Sub

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

    Public Shared Function DownloadFileToString(ByVal URL As String, Optional ByVal ForceDownload As Boolean = False) As String
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        Utilities.EnsureFolderExists(CacheFolder)

        If IO.File.Exists(CachePath) AndAlso Not ForceDownload Then
            Return IO.File.ReadAllText(CachePath)
        End If

        Dim Http As HttpWebRequest = WebRequest.Create(URL)
        Dim html As String
        Using WebResponse As HttpWebResponse = Http.GetResponse()

            Dim responseStream As Stream = WebResponse.GetResponseStream()
            If (WebResponse.ContentEncoding.ToLower().Contains("gzip")) Then
                responseStream = New GZipStream(responseStream, CompressionMode.Decompress)
                Utilities.tvScraperLog &= "**** TVDB Returned GZIP Encoded *****" & vbCrLf
            ElseIf (WebResponse.ContentEncoding.ToLower().Contains("deflate")) Then
                responseStream = New DeflateStream(responseStream, CompressionMode.Decompress)
                Utilities.tvScraperLog &= "**** TVDB Returned DEFLATE Encoded *****" & vbCrLf
            Else
                Utilities.tvScraperLog &= "**** TVDB Returned TEXT Stream *****" & vbCrLf
            End If
            Dim reader As StreamReader = New StreamReader(responseStream, Encoding.Default)

            html = reader.ReadToEnd()
            IO.File.WriteAllText(CachePath, html)
            'Dim buffer() As Byte
            'Dim bytesToRead As Integer = CInt(buffer.Length)
            'Dim Size As Long
            'Dim bytesRead As Long
            'While bytesToRead > 0
            '    Size = responseStream.Read(buffer, bytesRead, bytesToRead)
            '    If size = 0 Then Exit While
            '    bytesToRead -= size
            '    bytesRead += size
            'End While

            'Dim Cache As New FileStream(CachePath, FileMode.OpenOrCreate, FileAccess.ReadWrite)
            'Cache.Write(buffer, 0, buffer.Length)
            'Cache.Close()
            responseStream.Close()

        End Using
        Return html
    End Function
End Class






