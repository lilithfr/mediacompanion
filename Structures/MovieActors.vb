Imports System.Text.RegularExpressions

Public Structure str_MovieActors

    Public actorname As String
    Public actorrole As String
    Public actorthumb As String
    Public actorid As String

    Sub New(SetDefaults As Boolean) 'When called with new keyword & boolean constant SetDefault (either T or F), initialises all values to defaults to avoid having some variables left as 'nothing'
        actorname = ""
        actorrole = ""
        actorthumb = ""
        actorid = ""
    End Sub

    Shared Widening Operator CType(ByVal Input As Media_Companion.Actor) As str_MovieActors
        Dim Temp As New str_MovieActors(True)

        Temp.actorid = Input.ActorId.Value
        Temp.actorname = Input.Name.Value
        Temp.actorrole = Input.Role.Value
        Temp.actorthumb = Input.Thumb.Value

        Return Temp
    End Operator

    Shared Widening Operator CType(ByVal Input As str_MovieActors) As Media_Companion.Actor
        Dim Temp As New Media_Companion.Actor

        Temp.ActorId.Value = Input.actorid
        Temp.Name.Value = Input.actorname
        Temp.Role.Value = Input.actorrole
        Temp.Thumb.Value = Input.actorthumb

        Return Temp
    End Operator

    Function GetActorFileName(ActorPath As String) As String
        Return IO.Path.Combine(ActorPath, actorname.Replace(" ", "_") & ".jpg")
    End Function

    Public Sub SaveActor(ActorPath As String)

        If actorthumb <> Nothing Then

            Dim filename As String

            If Preferences.actorseasy Then
                Dim hg As New IO.DirectoryInfo(ActorPath)
                If Not hg.Exists Then
                    IO.Directory.CreateDirectory(ActorPath)
                End If
                filename = GetActorFileName(ActorPath)
                Movie.SaveActorImageToCacheAndPath(actorthumb, filename)
                ActorSave(filename)

            Else
                If Preferences.actorsave And actorid <> "" Then
                    Dim tempstring = Preferences.actorsavepath & "\" & actorid.Substring(actorid.Length - 2, 2)
                    Dim hg As New IO.DirectoryInfo(tempstring)
                    If Not hg.Exists Then
                        IO.Directory.CreateDirectory(tempstring)
                    End If

                    Dim workingpath = tempstring & "\" & actorid & ".jpg"
                    DownloadCache.SaveImageToCacheAndPath(actorthumb, workingpath, Preferences.overwritethumbs, , Movie.GetHeightResolution(Preferences.ActorResolutionSI))
                    ActorSave(workingpath)
                    actorthumb = IO.Path.Combine(Preferences.actornetworkpath, actorid.Substring(actorid.Length - 2, 2))

                    If Preferences.actornetworkpath.IndexOf("/") <> -1 Then
                        actorthumb = Preferences.actornetworkpath & "/" & actorid.Substring(actorid.Length - 2, 2) & "/" & actorid & ".jpg"
                    Else
                        actorthumb = Preferences.actornetworkpath & "\" & actorid.Substring(actorid.Length - 2, 2) & "\" & actorid & ".jpg"
                    End If
                End If
            End If

        End If

    End Sub


    Sub ActorSave(workingpath As String)
        If Preferences.EdenEnabled And Not Preferences.FrodoEnabled Then
            Utilities.SafeCopyFile(workingpath, workingpath.Replace(".jpg", ".tbn"), Preferences.overwritethumbs)
            Utilities.SafeDeleteFile(workingpath)
        ElseIf Preferences.EdenEnabled And Preferences.FrodoEnabled Then
            Utilities.SafeCopyFile(workingpath, workingpath.Replace(".jpg", ".tbn"), Preferences.overwritethumbs)
        End If
    End Sub


    'Input  : http://ia.media-imdb.com/images/M/MV5BMTY3Njc5ODc4OV5BMl5BanBnXkFtZTYwNjY5MTU0._V1_SX32_CR0,0,32,44_.jpg
    'Output : http://ia.media-imdb.com/images/M/MV5BMTY3Njc5ODc4OV5BMl5BanBnXkFtZTYwNjY5MTU0._V1._SY400_SX300_.jpg

    Public Function GetBigThumb(smallThumb As String) As String

        If smallThumb="" Then Return ""
        Return smallThumb.Substring(0, smallThumb.IndexOf("._V1_")) & "._V1._SY400_SX300_.jpg"
    End Function



    'Public Sub AssignFromImdbTr(Tr As String)

    '    Dim m As Match = Regex.Match(Tr, MovieRegExs.REGEX_ACTOR_2, RegexOptions.Singleline)

    '    actorname  = m.Groups("actorname").ToString.CleanSpecChars.CleanFilenameIllegalChars.EncodeSpecialChrs
    '    actorrole  = m.Groups("actorrole").ToString.StripTagsLeaveContent.CleanSpecChars.EncodeSpecialChrs.Trim
    '    actorthumb = GetBigThumb(m.Groups("actorthumb").ToString).EncodeSpecialChrs
    '    actorid    = m.Groups("actorid").ToString
    'End Sub

    Public Function AssignFromImdbTr(Tr As String) As Boolean

        Dim m As Match

        If Tr.IndexOf("loadlate")=-1 Then
            m = Regex.Match(Tr, MovieRegExs.REGEX_ACTOR_NO_IMAGE, RegexOptions.Singleline)
        Else
            m = Regex.Match(Tr, MovieRegExs.REGEX_ACTOR_WITH_IMAGE, RegexOptions.Singleline)
        End If

        
        Try
            actorname  = m.Groups("actorname").ToString.CleanSpecChars.CleanFilenameIllegalChars
        Catch
        End Try

        Try
            actorrole  = m.Groups("actorrole").ToString.StripTagsLeaveContent.CleanSpecChars.Trim.CleanActorRole
        Catch
        End Try

        Try 
            actorthumb = GetBigThumb(m.Groups("actorthumb").ToString).EncodeSpecialChrs
        Catch
        End Try

        Try
            actorid    = m.Groups("actorid").ToString
        Catch
        End Try

        Return actorname<>""
    End Function

End Structure
