Imports System.Reflection

Public Class MediaStubs
#Region "Properties"
    Private Property alt_title As String = ""

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
#End Region         'Properties

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

    Private Sub MediaStubs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tb_Stub_folder.text = Preferences.stubfolder
        tb_Stub_Alt_Title.Text = ""
        tb_Stub_File.Text = ""
        tb_Stub_filename.Text = ""
        tb_Stub_Message.Text = Preferences.stubmessage
        tb_disc_filename.Text = ""
    End Sub

    Private Sub btn_Browse_Offline_Folder_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Browse_Offline_Folder.Click
        Try
            'Dim WorkingTvShow As TvShow = tv_ShowSelectedCurrently()
            'browse
            'Form1.openFD.InitialDirectory = WorkingTvShow.NfoFilePath.Replace(IO.Path.GetFileName(WorkingTvShow.NfoFilePath), "")
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim newfolder As String = ""
                newfolder = FolderBrowserDialog1.SelectedPath
                If Preferences.stubofflinefolder(newfolder) Then
                    Preferences.stubfolder = newfolder
                    Preferences.SaveConfig
                    tb_Stub_folder.Text = newfolder
                Else
                    MsgBox ("Folder " & vbCrLf & "[" & newfolder & "]" & vbCrLf & "already in Movie Folder List")
                End If
            Else
                MsgBox ("No folder selected")
            End If
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles Button1.Click
            Preferences.stubmessage = tb_Stub_Message.Text
            Preferences.SaveConfig 
    End Sub

    Private Sub btnStubSaveStub_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btnStubSaveStub.Click

    End Sub
End Class
