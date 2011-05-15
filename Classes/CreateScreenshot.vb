Imports System.Threading

Public Class CreateScreenshot

    Public Function screenshot(ByVal fullnfopath As String, Optional ByVal overwrite As Boolean = False) As String
        Monitor.Enter(Me)
        Dim status As String = "working"
        Try
            Dim extensions(100) As String
            Dim extensioncount As Integer
            extensions(1) = ".avi"
            extensions(2) = ".xvid"
            extensions(3) = ".divx"
            extensions(4) = ".img"
            extensions(5) = ".mpg"
            extensions(6) = ".mpeg"
            extensions(7) = ".mov"
            extensions(8) = ".rm"
            extensions(9) = ".3gp"
            extensions(10) = ".m4v"
            extensions(11) = ".wmv"
            extensions(12) = ".asf"
            extensions(13) = ".mp4"
            extensions(14) = ".mkv"
            extensions(15) = ".nrg"
            extensions(16) = ".iso"
            extensions(17) = ".rmvb"
            extensions(18) = ".ogm"
            extensions(19) = ".bin"
            extensions(20) = ".ts"
            extensions(21) = ".vob"
            extensions(22) = ".m2ts"
            extensions(23) = ".rar"
            extensions(24) = ".flv"
            extensions(25) = ".dvr-ms"
            extensions(26) = "VIDEO_TS.IFO"
            extensioncount = 26
            Dim thumbpathandfilename As String = fullnfopath.Replace(IO.Path.GetExtension(fullnfopath), ".tbn")
            Dim skip As Boolean = False
            If Not IO.File.Exists(thumbpathandfilename) Or overwrite = True Then
                If IO.File.Exists(thumbpathandfilename) Then
                    Try
                        IO.File.Delete(thumbpathandfilename)
                    Catch
                        status = "nodelete"
                        skip = True
                    End Try
                End If
                If skip = False Then
                    Dim nfofilename As String = IO.Path.GetFileName(fullnfopath)
                    For j = 1 To extensioncount
                        Dim tempfilename As String = nfofilename
                        tempfilename = nfofilename.Replace(IO.Path.GetExtension(nfofilename), extensions(j))
                        Dim tempstring2 As String = fullnfopath.Replace(IO.Path.GetFileName(fullnfopath), tempfilename)
                        If IO.File.Exists(tempstring2) Then
                            Try
                                Dim seconds As Integer = 100
                                Dim myProcess As Process = New Process
                                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                                myProcess.StartInfo.CreateNoWindow = False
                                myProcess.StartInfo.FileName = Form1.applicationPath & "\ffmpeg.exe"
                                Dim proc_arguments As String = "-y -i """ & tempstring2 & """ -f mjpeg -ss " & seconds.ToString & " -vframes 1 -an " & """" & thumbpathandfilename & """"
                                myProcess.StartInfo.Arguments = proc_arguments
                                myProcess.Start()
                                myProcess.WaitForExit()
                                status = "done"
                                Exit For
                            Catch ex As Exception
                                MsgBox("boo")
                            End Try
                        End If
                    Next
                End If
            End If
        Catch
        Finally
        End Try
        Return status
        Monitor.Exit(Me)
    End Function
End Class
