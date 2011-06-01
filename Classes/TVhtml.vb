Imports System.Collections.Generic

Public Class SeasonEpisodeComparer
    Implements IComparer(Of String)
    Private Shared Function ParseSeasonEpisode(ByVal item As String, ByVal type As Boolean) As Integer
        Dim hyphenIndex As Integer = item.IndexOf("-")
        ' Normally do some error checking in case hyphenIndex==-1
        Dim firstPart As String
        If type = True Then
            firstPart = item.Substring(0, hyphenIndex)
        Else
            firstPart = item.Substring(hyphenIndex + 1)
        End If
        Return Integer.Parse(firstPart)
    End Function

    Public Function Compare(ByVal first As String, ByVal second As String) _
                As Integer Implements IComparer(Of String).Compare
        ' In real code you would probably add nullity checks
        Dim firstSeason As Integer = ParseSeasonEpisode(first, True)
        Dim secondSeason As Integer = ParseSeasonEpisode(second, True)
        Dim sameSeason As Integer = firstSeason.CompareTo(secondSeason)
        If sameSeason = 0 Then
            Dim firstEpisode As Integer = ParseSeasonEpisode(first, False)
            Dim secondEpisode As Integer = ParseSeasonEpisode(second, False)
            sameSeason = firstEpisode.CompareTo(secondEpisode)
        End If
        Return sameSeason
    End Function
End Class
