Imports System.Reflection

Public Class ucGenPref_XbmcLink

    Private MovieFolderMappings As XBMC_MC_FolderMappings = New XBMC_MC_FolderMappings

    ReadOnly Property Changed As Boolean
        Get
            Return  MovieFolderMappings.Changed(Preferences.XBMC_MC_MovieFolderMappings) OrElse
                    tbXBMC_Address         .Text <> Preferences.XBMC_Address             OrElse
                    tbXBMC_Port            .Text <> Preferences.XBMC_Port                OrElse
                    tbXBMC_Username        .Text <> Preferences.XBMC_Username            OrElse
                    tbXBMC_Password        .Text <> Preferences.XBMC_Password            OrElse
                    tbXBMC_UserdataFolder  .Text <> Preferences.XBMC_UserdataFolder      OrElse
                    tbXBMC_TexturesDb      .Text <> Preferences.XBMC_TexturesDb          OrElse
                    tbXBMC_ThumbnailsFolder.Text <> Preferences.XBMC_ThumbnailsFolder
                
        End Get
    End Property


    Public Sub Pop
        tbXBMC_Address         .Text = Preferences.XBMC_Address
        tbXBMC_Port            .Text = Preferences.XBMC_Port
        tbXBMC_Username        .Text = Preferences.XBMC_Username
        tbXBMC_Password        .Text = Preferences.XBMC_Password
        tbXBMC_UserdataFolder  .Text = Preferences.XBMC_UserdataFolder
        tbXBMC_TexturesDb      .Text = Preferences.XBMC_TexturesDb
        tbXBMC_ThumbnailsFolder.Text = Preferences.XBMC_ThumbnailsFolder
        MovieFolderMappings.Assign(Preferences.XBMC_MC_MovieFolderMappings)

        SetSaveBtnState
    End Sub

    Public Sub SetSaveBtnState
        btnGeneralPrefsSaveChanges.Enabled = Changed
    End Sub


    Private Sub llXBMC_MovieFolderMappings_LinkClicked( sender As Object,  e As LinkLabelLinkClickedEventArgs) Handles llXBMC_MovieFolderMappings.LinkClicked 
        Dim frm As new frmConfigureXBMC_MC_Folders

        frm.Init(MovieFolderMappings)
        frm.ShowDialog

        SetSaveBtnState
    End Sub

    Private Sub btnSelect_XBMC_UserdataFolder_Click( sender As Object,  e As EventArgs) Handles btnSelect_XBMC_UserdataFolder.Click 
        Dim ofd As New FolderBrowserDialog

        ofd.RootFolder          = Environment.SpecialFolder.Desktop
        ofd.Description         = "Locate 'XBMC\userdata' folder"
        ofd.ShowNewFolderButton = False

        If IO.Directory.Exists(tbXBMC_UserdataFolder.Text) Then
            ofd.SelectedPath = tbXBMC_UserdataFolder.Text
        Else
            ofd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)

            If IO.Directory.Exists(IO.Path.Combine(ofd.SelectedPath,"XBMC\userdata")) Then
                ofd.SelectedPath = IO.Path.Combine(ofd.SelectedPath,"XBMC\userdata")
            End If
        End If

        'Dim type As Type = ofd.[GetType]

        'Dim fieldInfo As Reflection.FieldInfo = type.GetField("rootFolder", BindingFlags.NonPublic Or BindingFlags.Instance)

        'fieldInfo.SetValue(ofd,DirectCast(18, Environment.SpecialFolder))

        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then 
            tbXBMC_UserdataFolder.Text = ofd.SelectedPath
        End If

    End Sub



    Private Sub btnGeneralPrefsSaveChanges_Click( sender As Object,  e As EventArgs) Handles btnGeneralPrefsSaveChanges.Click

        Preferences.XBMC_Address           = tbXBMC_Address         .Text
        Preferences.XBMC_Port              = tbXBMC_Port            .Text
        Preferences.XBMC_Username          = tbXBMC_Username        .Text
        Preferences.XBMC_Password          = tbXBMC_Password        .Text
        Preferences.XBMC_UserdataFolder    = tbXBMC_UserdataFolder  .Text
        Preferences.XBMC_TexturesDb        = tbXBMC_TexturesDb      .Text
        Preferences.XBMC_ThumbnailsFolder  = tbXBMC_ThumbnailsFolder.Text
        Preferences.XBMC_MC_MovieFolderMappings.Assign(MovieFolderMappings)

        Preferences.SaveConfig
        SetSaveBtnState
    End Sub

    Private Sub AnyFieldChanged(sender As Object,  e As EventArgs) Handles  tbXBMC_Address       .TextChanged, tbXBMC_Port.TextChanged      , tbXBMC_Username        .TextChanged, tbXBMC_Password.TextChanged, _
                                                                            tbXBMC_UserdataFolder.TextChanged, tbXBMC_TexturesDb.TextChanged, tbXBMC_ThumbnailsFolder.TextChanged
        SetSaveBtnState
    End Sub
End Class
