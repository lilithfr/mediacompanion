
Public Structure MediaNFOSubtitles
    Dim language As String


    Shared Widening Operator CType(ByVal Input As Nfo.SubtitleDetails) As MediaNFOSubtitles
        Dim Temp As New MediaNFOSubtitles

        Temp.language = Input.Language


        Return Temp
    End Operator

    Shared Widening Operator CType(ByVal Input As MediaNFOSubtitles) As Nfo.SubtitleDetails
        Dim Temp As New Nfo.SubtitleDetails

        Temp.Language.Value = Input.language

        Return Temp
    End Operator

End Structure
