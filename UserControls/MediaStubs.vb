Imports System.Reflection

Public Class MediaStubs
'#Region "Properties"
'    Private MovieFolderMappings As XBMC_MC_FolderMappings = New XBMC_MC_FolderMappings

'    ReadOnly Property Changed As Boolean
'        Get
'            Return  cb_prxyEnabled      .Checked <> Preferences.prxyEnabled     OrElse
'                    tb_prxyIP           .Text    <> Preferences.prxyIP          OrElse
'                    tb_prxyPort         .Text    <> Preferences.prxyPort        OrElse
'                    tb_prxyUsername     .Text    <> Preferences.prxyUsername    OrElse
'                    tb_prxyPassword     .Text    <> Preferences.prxyPassword
'        End Get
'    End Property
'#End Region         'Properties

'#Region "Event Handlers"
'    Private Sub btnProxySaveChanges_Click( sender As Object,  e As EventArgs) Handles btnStubSaveStub.Click

'        UpdatePreferences

'        Preferences.SaveConfig
'        SetEnabledStates
'    End Sub

'    Private Sub AnyFieldChanged(sender As Object,  e As EventArgs) Handles   tb_Stub_folder.TextChanged, _
'                                                                            tb_Stub_filename.TextChanged, tb_Stub_Alt_Title.TextChanged, _
'                                                                            tb_Stub_Message.TextChanged
'        SetEnabledStates
'    End Sub
'#End Region         'Event Handlers

'#Region "Main Subs"
'    Public Sub pop
'        AssignFormFields
'        SetEnabledStates
'    End Sub

'#End Region         'Main Subs

'#Region "Other Subs"
'    Sub SetEnabledStates

'        btnStubSaveStub.Enabled = Changed

'    End Sub

'    Sub AssignFormFields
'        cb_prxyEnabled      .Checked    = Preferences.prxyEnabled
'        tb_Stub_folder           .Text       = Preferences.prxyIp
'        tb_Stub_filename         .Text       = Preferences.prxyPort
'        tb_Stub_Alt_Title     .Text       = Preferences.prxyUsername
'        tb_Stub_Message     .Text       = Preferences.prxyPassword
'        'MovieFolderMappings.Assign(Preferences.XBMC_MC_MovieFolderMappings)
'    End Sub

'    Sub UpdatePreferences
'        Preferences.prxyEnabled         = cb_prxyEnabled      .Checked
'        Preferences.prxyIp              = tb_Stub_folder           .Text
'        Preferences.prxyPort            = tb_Stub_filename         .Text
'        Preferences.prxyUsername        = tb_Stub_Alt_Title     .Text
'        Preferences.prxyPassword        = tb_Stub_Message     .Text
'        'Preferences.XBMC_MC_MovieFolderMappings.Assign(MovieFolderMappings)
'    End Sub
'#End Region         'Other Subs

End Class
