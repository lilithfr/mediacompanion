Imports System.Reflection

Public Class RescrapeList
    Property title          As Boolean   'Booleans default to false
    Property votes          As Boolean
    Property rating         As Boolean
    Property top250         As Boolean
    Property runtime        As Boolean
    Property director       As Boolean
    Property stars          As Boolean
    Property year           As Boolean
    Property outline        As Boolean
    Property plot           As Boolean
    Property tagline        As Boolean
    Property genre          As Boolean
    Property studio         As Boolean
    Property premiered      As Boolean
    Property mpaa           As Boolean
    Property trailer        As Boolean
    Property credits        As Boolean
    Property posterurls     As Boolean
    Property country        As Boolean
    Property actors         As Boolean
    Property mediatags      As Boolean
    Property missingposters As Boolean
    Property missingfanart  As Boolean
    Property activate       As Boolean
    Property runtime_file   As Boolean
    Property tmdb_set_name  As Boolean
    Property Download_Trailer As Boolean
    Property Rename_Files   As Boolean

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
            propInfo.SetValue(Me,False,Nothing)
        Next
    End Sub


End Class
