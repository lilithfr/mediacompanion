Imports System.IO
Imports System.Xml

Public Class Profiles

    Property StartupProfile     As String
    Property DefaultProfile     As String
    Property WorkingProfileName As String
    Property ProfileList        As New List(Of ListOfProfiles)


    Public Sub Load
        ProfileList.Clear()
        Dim applicationpath As String = Preferences.applicationPath 
        Dim profilepath As String = Path.Combine(applicationpath, "settings")

        profilepath = Path.Combine(profilepath, "profile.xml")

        If File.Exists(profilepath) Then

            Dim xmlDoc As New XmlDocument
            xmlDoc.Load(profilepath)

            If xmlDoc.DocumentElement.Name = "profile" Then

                For Each thisresult In xmlDoc("profile")
                    Select Case thisresult.Name
                        Case "default"
                            DefaultProfile = thisresult.innertext
                        Case "startup"
                            StartupProfile = thisresult.innertext
                        Case "profiledetails"
                            Dim currentprofile As New ListOfProfiles
                            For Each result In thisresult.childnodes
                                Dim t As Integer = result.innertext.ToString.ToLower.IndexOf("\s")
                                Select Case result.name
                                    Case "actorcache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.ActorCache = applicationPath & s 'result.innertext

                                    Case "directorcache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.DirectorCache = applicationPath & s 'result.innertext

                                    Case "config"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.Config = applicationPath & s 'result.innertext
                                    Case "moviecache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.MovieCache = applicationPath & s 'result.innertext
                                    Case "profilename"
                                        'Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.ProfileName = result.innertext
                                    Case "regex"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.RegExList = applicationPath & s 'result.innertext
                                    Case "filters"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.Filters = applicationPath & s 'result.innertext
                                    Case "genres"
                                        Dim s As String = ""
                                        If result.innertext = "" Then 
                                            s = "\settings\genres.txt"  'incase missing from existing profile.xml
                                        Else
                                            s = result.innertext.ToString.Substring(t)
                                        End If
                                        currentprofile.Genres = applicationPath & s

                                    Case "tvcache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.TvCache = applicationPath & s 'result.innertext
                                    Case "musicvideocache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.MusicVideoCache = applicationPath & s
                                    Case "moviesetcache"
                                        Dim s As String = result.innertext.ToString.Substring(t)
                                        currentprofile.MovieSetCache = applicationPath & s
                                End Select
                            Next
                            ProfileList.Add(currentprofile)
                    End Select
                Next

            End If
        End If
    End Sub



End Class
