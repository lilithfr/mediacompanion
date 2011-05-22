
Public Structure MediaNFOAudio
    Dim language As String
    Dim codec As String
    Dim channels As String
    Dim bitrate As String

    Shared Widening Operator CType(ByVal Input As Nfo.AudioDetails) As MediaNFOAudio
        Dim Temp As New MediaNFOAudio
        Temp.codec = Input.Codec
        Temp.bitrate = Input.Bitrate
        Temp.language = Input.Language
        Temp.channels = Input.Channels

        Return Temp
    End Operator

    Shared Widening Operator CType(ByVal Input As MediaNFOAudio) As Nfo.AudioDetails
        Dim Temp As New Nfo.AudioDetails

        Temp.Codec.Value = Input.codec
        Temp.Bitrate.Value = Input.bitrate
        Temp.Channels.Value = Input.channels
        Temp.Language.Value = Input.language

        Return Temp
    End Operator
End Structure
