Imports System.Reflection

Public Class ucGenPref_Proxy
#Region "Properties"
    Private MovieFolderMappings As XBMC_MC_FolderMappings = New XBMC_MC_FolderMappings

    ReadOnly Property Changed As Boolean
        Get
            Return  cb_prxyEnabled      .Checked <> Preferences.prxyEnabled     OrElse
                    tb_prxyIP           .Text    <> Preferences.prxyIP          OrElse
                    tb_prxyPort         .Text    <> Preferences.prxyPort        OrElse
                    tb_prxyUsername     .Text    <> Preferences.prxyUsername    OrElse
                    tb_prxyPassword     .Text    <> Preferences.prxyPassword
        End Get
    End Property
#End Region         'Properties

#Region "Event Handlers"
    Private Sub btnProxySaveChanges_Click( sender As Object,  e As EventArgs) Handles btnProxySaveChanges.Click

        UpdatePreferences

        Preferences.ConfigSave()
        SetEnabledStates
    End Sub

    Private Sub AnyFieldChanged(sender As Object,  e As EventArgs) Handles  cb_prxyEnabled.CheckedChanged, tb_prxyIP.TextChanged, _
                                                                            tb_prxyPort.TextChanged, tb_prxyUsername.TextChanged, _
                                                                            tb_prxyPassword.TextChanged
        SetEnabledStates
    End Sub
#End Region         'Event Handlers

#Region "Main Subs"
    Public Sub pop
        AssignFormFields
        SetEnabledStates
    End Sub

#End Region         'Main Subs

#Region "Other Subs"
    Sub SetEnabledStates

        btnProxySaveChanges.Enabled = Changed

    End Sub

    Sub AssignFormFields
        cb_prxyEnabled      .Checked    = Preferences.prxyEnabled
        tb_prxyIP           .Text       = Preferences.prxyIp
        tb_prxyPort         .Text       = Preferences.prxyPort
        tb_prxyUsername     .Text       = Preferences.prxyUsername
        tb_prxyPassword     .Text       = Preferences.prxyPassword
        'MovieFolderMappings.Assign(Preferences.XBMC_MC_MovieFolderMappings)
    End Sub

    Sub UpdatePreferences
        Preferences.prxyEnabled         = cb_prxyEnabled      .Checked
        Preferences.prxyIp              = tb_prxyIP           .Text
        Preferences.prxyPort            = tb_prxyPort         .Text
        Preferences.prxyUsername        = tb_prxyUsername     .Text
        Preferences.prxyPassword        = tb_prxyPassword     .Text
        'Preferences.XBMC_MC_MovieFolderMappings.Assign(MovieFolderMappings)
    End Sub
#End Region         'Other Subs

End Class
