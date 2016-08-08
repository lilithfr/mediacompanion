Imports System.Xml

Public Class FullFileDetails
    Public filedetails_video     As New VideoDetails
    Public filedetails_audio     As New List(Of AudioDetails   )
    Public filedetails_subtitles As New List(Of SubtitleDetails)

    Private _assignedDefaultAudioTrack As Boolean = False
    Private _defaultAudioTrack As AudioDetails
    Private _assignedDefaultSubTrack As Boolean = False
    Private _defaultSubTrack As SubtitleDetails

    Public Property DefaultAudioTrack As AudioDetails
        Get
            If Not _assignedDefaultAudioTrack Then
                _assignedDefaultAudioTrack = True

                If filedetails_audio.Count > 0 Then
                    _defaultAudioTrack = (From x In filedetails_audio Where x.DefaultTrack.Value="Yes").FirstOrDefault

                    If IsNothing(_defaultAudioTrack) Then
                        _defaultAudioTrack = filedetails_audio(0)
                    End If
                End If
            End If

            Return _defaultAudioTrack
        End Get

        Set
            _assignedDefaultAudioTrack = True
            _defaultAudioTrack = Value
        End Set
    End Property   

    Public Property DefaultSubTrack As SubtitleDetails
        Get
            If Not _assignedDefaultSubTrack Then
                _assignedDefaultSubTrack = True

                If filedetails_subtitles.Count > 0 Then
                    _defaultSubTrack = (From x In filedetails_subtitles Where x.Primary = True).FirstOrDefault

                    If IsNothing(_defaultSubTrack) Then
                        _defaultSubTrack = filedetails_subtitles(0)
                    End If
                End If
            End If

            Return _defaultSubTrack
        End Get

        Set
            _assignedDefaultSubTrack = True
            _defaultSubTrack = Value
        End Set
    End Property

End Class
