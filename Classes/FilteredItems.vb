Imports MC_UserControls

Public Class FilteredItems

    Property Include As New List(Of String)
    Property Exclude As New List(Of String)

    Sub New (ccb As TriStateCheckedComboBox,Optional Find As String=Nothing, Optional Replace As String=Nothing)

        Dim i As Integer = 0

        For Each item As CCBoxItem In ccb.Items
            Dim value As String = item.Name.RemoveAfterMatch

            If Not IsNothing(Find) Then
                value = If(value=Find,Replace,value)
            End If

            Select ccb.GetItemCheckState(i)
                Case CheckState.Checked   : Include.Add(value)
                Case CheckState.Unchecked : Exclude.Add(value)
            End Select
            i += 1
        Next
    End Sub

End Class
