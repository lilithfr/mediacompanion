Imports System.Reflection

Public Class ucGenPref_Proxy
#Region "Properties"
    Private Property Changed As Boolean = False
    'ReadOnly Property Changed As Boolean
    '    Get
    '        Return  cb_prxyEnabled      .Checked <> Preferences.prxyEnabled     OrElse
    '                tb_prxyIP           .Text    <> Preferences.prxyIP          OrElse
    '                tb_prxyPort         .Text    <> Preferences.prxyPort        OrElse
    '                tb_prxyUsername     .Text    <> Preferences.prxyUsername    OrElse
    '                tb_prxyPassword     .Text    <> Preferences.prxyPassword
    '    End Get
    'End Property
#End Region         'Properties

#Region "Event Handlers"
    Private Sub btnProxySaveChanges_Click( sender As Object,  e As EventArgs) Handles btnProxySaveChanges.Click

        UpdatePreferences

        Preferences.ConfigSave()
        btnProxySaveChanges.Enabled = False
        'SetEnabledStates
    End Sub

    Private Sub AnyFieldChanged(sender As Object,  e As EventArgs) Handles  cb_prxyEnabled.CheckedChanged, tb_prxyIP.TextChanged, _
                                                                            tb_prxyPort.TextChanged, tb_prxyUsername.TextChanged, _
                                                                            tb_prxyPassword.TextChanged, cb_prxyNone.CheckedChanged, cb_prxySystem.CheckedChanged
        If Changed Then Exit Sub
        If sender Is cb_prxyEnabled AndAlso cb_prxyEnabled.Checked Then
            Preferences.prxyEnabled = "true"
            Changed = True
            cb_prxyNone.Checked = False
            cb_prxySystem.Checked = False
        ElseIf sender Is cb_prxyNone AndAlso cb_prxyNone.Checked Then
            Preferences.prxyEnabled = "false"
            Changed = True
            cb_prxyEnabled.Checked = False
            cb_prxySystem.Checked = False
        ElseIf sender Is cb_prxySystem AndAlso cb_prxySystem.Checked Then
            Preferences.prxyEnabled = "system"
            Changed = True
            cb_prxyEnabled.Checked = False
            cb_prxyNone.Checked = False
        End If
        Changed = False
        btnProxySaveChanges.Enabled = True
        'SetEnabledStates
    End Sub
#End Region         'Event Handlers

#Region "Main Subs"
    Public Sub pop
        AssignFormFields
        btnProxySaveChanges.Enabled = False
        'SetEnabledStates
    End Sub

#End Region         'Main Subs

#Region "Other Subs"
    'Sub SetEnabledStates

    '    btnProxySaveChanges.Enabled = Changed

    'End Sub

    Sub AssignFormFields
        Changed = True
        cb_prxyEnabled      .Checked    = Preferences.prxyEnabled = "true"
        cb_prxyNone         .Checked    = Preferences.prxyEnabled = "false"
        cb_prxySystem       .Checked    = Preferences.prxyEnabled = "system"
        tb_prxyIP           .Text       = Preferences.prxyIp
        tb_prxyPort         .Text       = Preferences.prxyPort
        tb_prxyUsername     .Text       = Preferences.prxyUsername
        tb_prxyPassword     .Text       = Preferences.prxyPassword
        Changed = False 
    End Sub

    Sub UpdatePreferences
        'Preferences.prxyEnabled         = cb_prxyEnabled      .Checked
        Preferences.prxyIp              = tb_prxyIP           .Text
        Preferences.prxyPort            = tb_prxyPort         .Text
        Preferences.prxyUsername        = tb_prxyUsername     .Text
        Preferences.prxyPassword        = tb_prxyPassword     .Text
    End Sub
#End Region         'Other Subs

End Class
