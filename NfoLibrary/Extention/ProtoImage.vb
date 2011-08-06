
Imports System.Runtime.CompilerServices
Imports Tasks

Public Module ProtoImageExtentions

    <Extension()>
    Public Sub DownloadFile(ByVal pImage As ProtoXML.ProtoImage)
        TaskCache.Tasks.Add(New DownloadFileTask(pImage.Url, pImage.Path))
    End Sub



End Module
