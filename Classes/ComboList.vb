
Public Class ComboList

    Property fullpathandfilename As String = "" 
    Property MovieSet            As String = ""
    Property filename            As String = ""
    Property foldername          As String = ""
    Property title               As String = ""
    Property originaltitle       As String = ""

    'ReadOnly Property titleandyear As String
    '    Get
    '        Dim t = If(IsNothing(title),"Unknown",title)
    '        Dim y = If(IsNothing(year ),"0000"   ,year )

    '        If Preferences.ignorearticle And t.ToLower.IndexOf("the ")=0 Then
    '            Return t.Substring(4, t.Length - 4) & ", The (" & y & ")"
    '        Else
    '            Return t & " (" & y & ")"
    '        End If
    '    End Get
    'End Property
    
    Property year                As String  = ""
    Property filedate            As String  = ""
    Property id                  As String  = ""
    Property rating              As String  = ""
    Property top250              As String  = 0
    Property genre               As String  = ""
    Property playcount           As String  = ""
    Property sortorder           As String  = ""
    Property outline             As String  = ""
    Property runtime             As String  = ""
    Property createdate          As String  = ""
    Property missingdata1        As Byte    = 0
    Property plot                As String  = ""
    Property source              As String  = ""
    Property votes               As String  = ""
    Property Resolution         As Integer = -1

    Public ReadOnly Property MissingFanart As Boolean
        Get
            Return _missingdata1 And 1
        End Get
    End Property

    Public ReadOnly Property MissingPoster As Boolean
        Get
            Return _missingdata1 And 2
        End Get
    End Property

    Public ReadOnly Property MissingTrailer As Boolean
        Get
            Return _missingdata1 And 4
        End Get
    End Property

End Class
