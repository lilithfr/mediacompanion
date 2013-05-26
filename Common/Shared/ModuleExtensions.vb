Imports System.Globalization
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Public Module ModuleExtensions

    <Extension()> _
    Public Function ToInt(ByVal s As String) As Integer
        Dim num as Integer

        If Integer.TryParse(s.Replace(",",""), num) Then Return num
        
        Return 0
    End Function

  
    <Extension()> _
    Public Function ToRating(ByVal s As String) As Double
        Dim num As Double
        Dim x   As String = s
                            
        x = x.Replace("/10", "" )
        x = x.Replace(" "  , "" )
        x = x.Replace(","  , ".")

        If Double.TryParse(x, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), num) Then 
            If num>=0 and num<=10 Then Return num
        End If
        
        Return 0
    End Function


    <Extension()> _
    Public Function FormatRating(ByVal s As String) As String
        Dim numRating As Single

        If s = "" Or Not Single.TryParse(s, numRating) Then
            Return s
        End If

        numRating = Math.Min(numRating,10)

        Return numRating.ToString("f1")
    End Function


    <Extension()> _
    Public Function RemoveAfterMatch(ByVal s As String, Optional match As String=" (") As String
        Dim i As Integer = s.LastIndexOf(match)

        If i=-1 Then Return s
        
        Return s.Substring(0,i)
    End Function


    <Extension()> _
    Public Function RemoveLastChar(ByVal s As String) As String
        Return s.Remove(s.Length-1)
    End Function

    <Extension()> _
    Public Function GetLastChar(ByVal s As String) As String
        Return s(s.Length-1)
    End Function

    
    <Extension()> _
    Public Function SafeTrim(ByVal s As String) As String

        If IsNothing(s) Then Return s

        Return s.Trim
    End Function

    <Extension()> _
    Public Sub AppendLine(tb As TextBox, value As String)
        If tb.Text.Length=0 Then
            tb.Text = value
        Else
            tb.AppendText( Environment.NewLine + value )
        End If
    End Sub

    <Extension()> _
    Public Function RemoveWhitespace(input As String) As String
        If IsNothing(input) Then Return ""
        Return New String(input.ToCharArray().Where(Function(c) Not [Char].IsWhiteSpace(c)).ToArray())
    End Function

End Module

