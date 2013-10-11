Imports System.Security.Cryptography
Imports System.Net
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System
Imports System.Drawing

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

    Public Shared Function SaveImageToCacheAndPath(ByVal URL As String, Path As String, Optional ByVal ForceDownload As Boolean = False, _
                                           Optional ByVal resizeWidth As Integer = 0, Optional ByVal resizeHeight As Integer = 0) As Boolean

        If Not SaveImageToCache(URL, Path, ForceDownload) Then Return False

        Dim CachePath = IO.Path.Combine(CacheFolder, GetCacheFileName(URL))

        IfNotValidImage_Delete(CachePath)



        'Resize cache image only if need to
        CopyAndDownSizeImage(CachePath, CachePath, resizeWidth, resizeHeight)

        Utilities.EnsureFolderExists(Path)
        File.Copy(CachePath, Path, True)

        Return True
    End Function


    Public Shared Function SaveImageToCacheAndPaths(ByVal URL As String, Paths As List(Of String), Optional ByVal ForceDownload As Boolean = False, _
                                           Optional ByVal resizeWidth As Integer = 0, Optional ByVal resizeHeight As Integer = 0) As Boolean

        If Not SaveImageToCache(URL, Paths(0), ForceDownload) Then Return False

        Dim CachePath = IO.Path.Combine(CacheFolder, GetCacheFileName(URL))

        IfNotValidImage_Delete(CachePath)


        'Resize cache image only if need to
        CopyAndDownSizeImage(CachePath, CachePath, resizeWidth, resizeHeight)

        For Each path In Paths
            Utilities.EnsureFolderExists(path)
            File.Copy(CachePath, path, True)
        Next

        Return True
    End Function

    Public Shared Sub IfNotValidImage_Delete(filename As String)
        Try
            Dim testImage = new Drawing.Bitmap(filename)
        Catch ex As Exception
            Try
                File.Delete(filename)
            Catch 
            End Try
            Throw 
        End Try
    End Sub

    Public Shared Sub CopyAndDownSizeImage(ByVal src As String, ByVal dest As String, Optional ByVal resizeWidth As Integer = 0, Optional ByVal resizeHeight As Integer = 0)
        Try
            Dim img = Utilities.GetImage(src)
            Dim resized = False

            'Down-size only
            If (resizeWidth <> 0 And img.Width > resizeWidth) Or img.Height > resizeHeight And Not (resizeWidth = 0 And resizeHeight = 0) Then

                'Calc scaled height - width is passed in as zero for people pictures and posters.
                If resizeWidth = 0 Then
                    resizeWidth = img.Width * resizeHeight / img.Height
                End If

                img = Utilities.ResizeImage(img, resizeWidth, resizeHeight)
                resized = True
            End If

            If resized Or src <> dest Then
                Utilities.SaveImage(img, dest)
            End If
        Catch
        End Try
    End Sub

    Public Shared Function DownloadFileAndCache(ByVal URL As String, Optional ByVal Path As String = "", _
                                          Optional ByVal ForceDownload As Boolean = False, _
                                          Optional ByVal resizeFanart As Integer = 0, _
                                          Optional ByRef strValue As String = "") As Boolean

        Dim returnCode As Boolean = SaveImageToCache(URL, Path, ForceDownload)
        If returnCode Then
            Dim CacheFileName As String = GetCacheFileName(URL)
            Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)


            If String.IsNullOrEmpty(Path) Then
                strValue = IO.File.ReadAllText(CachePath)
            Else
                Utilities.copyImage(CachePath, Path, resizeFanart)
            End If
        End If

        DownloadFileAndCache = returnCode
        Return returncode
    End Function

    Public Shared Function SaveImageToCache(ByVal URL As String, Optional ByVal Path As String = "", Optional ByVal ForceDownload As Boolean = False) As Boolean

        Utilities.EnsureFolderExists(CacheFolder)
        Dim returnCode As Boolean = True
        If URL = "" Then Return False
        Dim CacheFileName As String = GetCacheFileName(URL)
        Dim CachePath As String = IO.Path.Combine(CacheFolder, CacheFileName)

        If Not File.Exists(CachePath) OrElse ForceDownload Then

            'Check to see if URL is actually a local file
            If File.Exists(URL) Then
                If CachePath <> URL Then
                    If Utilities.SafeDeleteFile(CachePath) Then
                        File.Copy(URL, CachePath)
                    End If
                End If
                Return True
            End If

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
                            'If (File.Exists(CachePath)) Then
                            'File.Delete(CachePath)
                            'End If
                            Utilities.SafeDeleteFile(CachePath)

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
                If ex.Message.Contains("could not be resolved") Then Return False : Exit Try
                Using errorResp As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                    Using errorRespStream As Stream = errorResp.GetResponseStream()
                        Dim errorText As String = New StreamReader(errorRespStream).ReadToEnd()

                        'Writing to TvLog! -> Poo -> To do anyone -> Raise event?
                        returnCode = False
                        'Utilities.tvScraperLog &= String.Format("**** Scraper Error: Code {0} ****{3}     {2}{3}", errorResp.StatusCode, vbCrLf)
                    End Using
                End Using
                returnCode = False
            End Try

        End If

        Return returnCode
    End Function

End Class






