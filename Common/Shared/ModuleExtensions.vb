Imports System.Runtime.CompilerServices

Public Module ModuleExtensions

    <Extension()> _
    Public Function ToInt(ByVal s As String) As Integer
        Dim num as Integer

        If Integer.TryParse(s.Replace(",",""), num) Then Return num
        
        Return 0
    End Function
    
    <Extension()> _
    Public Function ToDouble(ByVal s As String) As Double
        Dim num as Double

        If Double.TryParse(s.Replace(",",""), num) Then Return num
        
        Return 0
    End Function

End Module

