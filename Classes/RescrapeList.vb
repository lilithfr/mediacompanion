Imports System.Reflection

Public Class RescrapeList
    Property title               As Boolean   'Booleans default to false
    Property votes               As Boolean
    Property rating              As Boolean
    Property top250              As Boolean
    Property runtime             As Boolean
    Property director            As Boolean
    Property stars               As Boolean
    Property year                As Boolean
    Property outline             As Boolean
    Property plot                As Boolean
    Property tagline             As Boolean
    Property genre               As Boolean
    Property studio              As Boolean
    Property premiered           As Boolean
    Property mpaa                As Boolean
    Property trailer             As Boolean
    Property credits             As Boolean
    Property posterurls          As Boolean
    Property country             As Boolean
    Property actors              As Boolean
    Property mediatags           As Boolean
    Property missingposters      As Boolean
    Property missingfanart       As Boolean
    Property dlxtraart           As Boolean
    Property runtime_file        As Boolean
    Property tmdb_set_name       As Boolean
    Property Download_Trailer    As Boolean
    Property Rename_Files        As Boolean
    Property Rename_Folders      As Boolean
    Property Frodo_Poster_Thumbs As Boolean
    Property Frodo_Fanart_Thumbs As Boolean
    Property Xbmc_Sync           As Boolean
    Property Convert_To_Frodo    As Boolean
    Property EmptyMainTags       As Boolean
    Property TagsFromKeywords    As Boolean
    Property SetWatched          As Boolean
    Property ClearWatched        As Boolean
    Property FromTMDB            As Boolean

    Sub New
    End Sub

    Sub New(field As String)
        Dim propInfos() As PropertyInfo = Me.GetType.GetProperties

        For Each propInfo in propInfos

            If propInfo.Name.ToLower = field.ToLower then
                propInfo.SetValue(Me,True,Nothing)
            End If
        Next
    End Sub

    Sub ResetFields
        Dim propInfos() As PropertyInfo = Me.GetType.GetProperties

        For Each propInfo in propInfos

            If Not propInfo.CanWrite Then Continue For

            propInfo.SetValue(Me,False,Nothing)
        Next
    End Sub

    ReadOnly Property AnyEnabled As Boolean
        Get
            Dim propInfos() As PropertyInfo = Me.GetType.GetProperties

            For Each propInfo in propInfos

                If Not propInfo.CanWrite Then Continue For
                If propInfo.Name = "FromTMDB" Then Continue For

                Dim value As Boolean = propInfo.GetValue(Me,Nothing)

                If value Then Return True
            Next

            Return False
        End Get
    End Property


End Class
