
Imports System.IO


Public Class frmCopyProgress
    Private Function GetFileSize(ByVal MyFilePath As String) As Long
        Dim MyFile As New System.IO.FileInfo(MyFilePath)
        Dim FileSize As Long = MyFile.Length
        Return FileSize
    End Function


    Private Declare Function GetDiskFreeSpaceEx _
    Lib "kernel32" _
    Alias "GetDiskFreeSpaceExA" _
    (ByVal lpDirectoryName As String, _
    ByRef lpFreeBytesAvailableToCaller As Long, _
    ByRef lpTotalNumberOfBytes As Long, _
    ByRef lpTotalNumberOfFreeBytes As Long) As Long
    Public Function GetFreeSpace(ByVal Drive As String) As Long
        Dim lBytesTotal, lFreeBytes, lFreeBytesAvailable As Long
        Dim iAns As Long

        iAns = GetDiskFreeSpaceEx(Drive, lFreeBytesAvailable, _
             lBytesTotal, lFreeBytes)

        If iAns > 0 Then
            Return lFreeBytes
        Else
            Throw New Exception("Invalid or unreadable drive")
        End If

    End Function
    Private Sub Exportmovies()
        With FolderBrowserDialog1
            .ShowNewFolderButton = True
            .Description = "Select destination for file copy"
        End With
        Dim drive As String = ""
        Dim savepath As String = ""
        If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            savepath = FolderBrowserDialog1.SelectedPath
            drive = IO.Path.GetPathRoot(savepath)
            Me.Visible = True
            Me.Show()
            Me.Refresh()

            Dim listoffilestomove2 As New List(Of String)
            listoffilestomove2.Clear()
            For Each fil In Form1.listoffilestomove
                listoffilestomove2.Add(fil)
            Next

            Dim totalfilesize As Long
            totalfilesize = Form1.totalfilesize
            Dim drivespace As Long
            drivespace = GetFreeSpace(drive)
            Application.DoEvents()
            Me.Refresh()
            Dim percentages As New List(Of Integer)

            If drivespace > totalfilesize Then
                ProgressBar2.Maximum = 100
                ProgressBar2.Minimum = 0
                ProgressBar2.Step = 1
                ProgressBar2.Value = 0
                ProgressBar1.Maximum = 100
                ProgressBar1.Minimum = 0
                ProgressBar1.Step = 1
                ProgressBar1.Value = 0
                ProgressBar1.Style = ProgressBarStyle.Blocks
                ProgressBar2.Style = ProgressBarStyle.Blocks
                Dim donesize As Long = 0
                Dim percentage As Integer = 0
                For Each item In listoffilestomove2
                    Label3.Text = item
                    If donesize <> 0 Then ProgressBar2.Value = donesize * 100 / totalfilesize
                    Try
                        Application.DoEvents()
                        Me.Refresh()
                        Dim filename As String = IO.Path.GetFileName(item)
                        Dim dest As String = IO.Path.Combine(savepath, filename)
                        Dim mediafile As String = filename
                        Dim fi As New IO.FileInfo(mediafile)
                        Dim sr As New IO.FileStream(item, IO.FileMode.Open) 'source file
                        Dim sw As New IO.FileStream(dest, IO.FileMode.Create) 'target file, defaults overwrite
                        Dim len As Long = sr.Length - 1
                        donesize += len
                        For i As Long = 0 To len
                            Try
                                sw.WriteByte(sr.ReadByte)
                                If i Mod 1000000 = 0 Then 'only update UI every 1 Kb copied
                                    ProgressBar1.Value = i * 100 / len
                                    ProgressBar1.Refresh()
                                    If i Mod 10000000 = 0 Then
                                        Application.DoEvents()
                                    End If
                                End If
                                If closing = True Then
                                    sw.Close()
                                    IO.File.Delete(dest)
                                    Exit For
                                End If
                            Catch
                            End Try
                        Next
                        If closing = False Then
                            sw.Close()
                        End If
                        If closing = True Then Exit For
                        ProgressBar1.Value = 0

                        Application.DoEvents()
                        Me.Refresh()
                    Catch ex As Exception
                    End Try
                Next
                Me.Close()
            End If
        Else
            Me.Close()
        End If
    End Sub

    Shadows closing As Boolean = False
    Private Sub copyprogress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Visible = False
            Me.Refresh()
            Application.DoEvents()
            Call Exportmovies()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim tempint As Integer = MessageBox.Show("The file export is still in progress" & vbCrLf & "Are you sure you wish to cancel?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tempint = DialogResult.Yes Then
                closing = True
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub
End Class