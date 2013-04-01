Imports System.Runtime.CompilerServices

Public Module ModuleExtensions

    <Extension()> _
    Public Function ToInt(ByVal s As String) As Integer
        Dim num as Integer

        If Integer.TryParse(s.Replace(",",""), num) Then Return num
        
        Return 0
    End Function
    
    <Extension()> _
    Public Function ToRating(ByVal s As String) As Double
        'Dim num as Double

        'If Double.TryParse(s.Replace(",","."), num) Then 
        '    If num>=0 and num<=10 Then Return num
        'End If
        
        'Return 0

        Dim num As Double
        Dim x   As String = s
                            
        x = x.Replace("/10", "" )
        x = x.Replace(" "  , "" )
        x = x.Replace(","  , ".")

        If Double.TryParse(x,num) Then 
            If num>=0 and num<=10 Then Return num
        End If
        
        Return 0
    End Function


    <Extension()> _
    Public Function RemoveAfterMatch(ByVal s As String, Optional match As String=" (") As String
        Dim i As Integer = s.IndexOf(match)

        If i=-1 Then Return s
        
        Return s.Substring(0,i)
    End Function


End Module

