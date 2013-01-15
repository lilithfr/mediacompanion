Imports System.IO
Imports System.Xml

Public Class Profiles

    Property StartupProfile     As String
    Property DefaultProfile     As String
    Property WorkingProfileName As String
    Property ProfileList        As New List(Of ListOfProfiles)


    Public Sub Load
        ProfileList.Clear()

        Dim profilepath As String = Path.Combine(Preferences.applicationPath, "settings")

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
                                Select Case result.name
                                    Case "actorcache"
                                        currentprofile.ActorCache = result.innertext
                                    Case "config"
                                        currentprofile.Config = result.innertext
                                    Case "moviecache"
                                        currentprofile.MovieCache = result.innertext
                                    Case "profilename"
                                        currentprofile.ProfileName = result.innertext
                                    Case "regex"
                                        currentprofile.RegExList = result.innertext
                                    Case "filters"
                                        currentprofile.Filters = result.innertext
                                    Case "tvcache"
                                        currentprofile.TvCache = result.innertext
                                End Select
                            Next
                            ProfileList.Add(currentprofile)
                    End Select
                Next

            End If
        End If
    End Sub



End Class
