Public Class clsCompareFileInfo
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim File1 As System.IO.FileInfo
        Dim File2 As System.IO.FileInfo

        File1 = DirectCast(x, System.IO.FileInfo)
        File2 = DirectCast(y, System.IO.FileInfo)

        Compare = String.Compare(File1.FullName, File2.FullName)
    End Function
End Class
