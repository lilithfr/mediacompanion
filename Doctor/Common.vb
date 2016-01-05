Public Class Common

End Class

Public Class BooleanToVisibilityConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If (TypeOf value Is Boolean) Then
            Return IIf((CBool(value)), Visibility.Visible, Visibility.Collapsed)
        End If

        Return value
    End Function

    Public Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class

