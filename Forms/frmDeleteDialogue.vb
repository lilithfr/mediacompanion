Imports System.Windows.Forms

Public Class Delete_Dialogue
    'Dim foldervalue As Integer = -1
    'Dim p(20) As String

    'Private Sub Delete_Dialogue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


    '    For f = 1 To Form1.deletefilescount
    '        p(f) = Form1.deletefiles(f)
    '        CheckedListBox1.Items.Add(Form1.deletefiles(f))
    '        CheckedListBox1.SetItemCheckState(f - 1, CheckState.Unchecked)
    '        If Form1.deletefiles(f) = "Delete Folder" Then foldervalue = f
    '    Next
    '    For f = 0 To CheckedListBox1.Items.Count - 1
    '        CheckedListBox1.SetItemCheckState(f, CheckState.Unchecked)
    '    Next
    'End Sub
    'Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    '    'Me.DialogResult = System.Windows.Forms.DialogResult.OK
    '    Dim deleteall As Boolean = True
    '    Dim folderselected As Boolean = False
    '    For f = 0 To Form1.deletefilescount - 1
    '        Dim chkstate As CheckState
    '        chkstate = CheckedListBox1.GetItemCheckState(f)
    '        If (chkstate = CheckState.Unchecked) Then
    '            Form1.deletefiles(f + 1) = Nothing
    '            If p(f + 1).IndexOf("Delete Folder") = -1 Then
    '                deleteall = False
    '            End If
    '        Else
    '            If p(f + 1).IndexOf("Delete Folder") <> -1 Then
    '                folderselected = True
    '            End If
    '        End If
    '    Next


    '    If deleteall = True Or folderselected = False Then
    '        Form1.deleteok = True
    '        Label3.Visible = False
    '        Me.Close()
    '    Else
    '        MsgBox("You Can't Delete The Folder Unless You Also Delete All The Files")
    '    End If




    'End Sub

    'Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
    '    'Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    '    Form1.deleteok = False
    '    Label3.Visible = False
    '    Me.Close()
    'End Sub

End Class
