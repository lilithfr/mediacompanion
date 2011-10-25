'Imports System.Net
Imports System.IO
Imports System.Data
'Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
'Imports Media_Companion.Scraper_Functions
'Imports System.Management
'Imports Media_Companion._preferences
Imports System.Xml
'Imports imdb.Classimdbscraper
'Imports System.Reflection
'Imports System.ComponentModel
'Imports System
'Imports System.Collections.Generic
'Imports System.Drawing
'Imports System.Windows.Forms
'Imports stdole
'Imports IMPA


Public Class InputOutput

    Public Function savetext(ByVal text As String, ByVal path As String) As Boolean
        Monitor.Enter(Me)
        Try
            Dim file As IO.StreamWriter = IO.File.CreateText(path)
            Try
                file.Write(text, False, Encoding.UTF8)
                file.Close()
                Return True
            Catch ex As Exception
                file.Close()
                Try
                    IO.File.Delete(path)
                Catch
                End Try
                Return False
            End Try
        Catch ex As Exception
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function deletefile(ByVal path As String) As Boolean
        Monitor.Enter(Me)
        Try
            If IO.File.Exists(path) Then
                IO.File.Delete(path)
            End If
            Return True
        Catch
            Return False
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function loadtextlines(ByVal path As String) As List(Of String)
        Monitor.Enter(Me)
        Dim listoflines As New List(Of String)
        Try
            If Not IO.File.Exists(path) Then
                listoflines.Add("nofile")
                Return listoflines
            Else
                Dim lines As IO.StreamReader = IO.File.OpenText(path)
                Dim line As String
                Do
                    line = lines.ReadLine
                    If Not line Is Nothing Then
                        listoflines.Add(line)
                    Else
                        Exit Do
                    End If
                Loop Until line = Nothing
                Return listoflines
            End If
        Catch
            If listoflines.Count > 0 Then
                Return listoflines
            Else
                listoflines.Add("Error")
                Return listoflines
            End If
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function loadtextfull(ByVal path As String) As String
        Monitor.Enter(Me)
        Dim text As String
        Try
            If Not IO.File.Exists(path) Then
                text = "nofile"
                Return text
            Else
                Dim lines As IO.StreamReader = IO.File.OpenText(path)
                text = lines.ReadToEnd
                Return text
            End If
        Catch
            If text Is Nothing Then
                text = "error"
            End If
            If text.Length = 0 Then
                text = "error"
            End If
            Return text
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function savexml(ByVal path As String, ByVal xmldoc As XmlDocument) As Boolean
        Monitor.Enter(Me)
        Try
            Dim output As New XmlTextWriter(Path, System.Text.Encoding.UTF8)
            Try
                output.Formatting = Formatting.Indented
                xmldoc.WriteTo(output)
                output.Close()
                Return True
            Catch ex As Exception
                Try
                    output.Close()
                    Try
                        IO.File.Delete(Path)
                    Catch
                    End Try
                Catch
                End Try
                Return False
            End Try
        Catch ex As Exception
            Return False
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function loadimage(ByVal path As String) As Bitmap
        Monitor.Enter(Me)
        Try
            Dim bitmap As New Bitmap(path)
            Dim newbitmap As New Bitmap(bitmap)
            bitmap.Dispose()
            Return newbitmap
            newbitmap.Dispose()
        Catch ex As Exception
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

    Public Function saveimage(ByVal image As Bitmap, ByVal path As String) As Boolean
        Monitor.Enter(Me)
        Try
            image.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg)
            Return True
        Catch ex As Exception
            Return False
        Finally
            Monitor.Exit(Me)
        End Try
    End Function

End Class
