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
        ClearImages
        UpdateFields
        SetBtnStates
    End Sub


    Public Sub SetBtnStates
        btnGeneralPrefsSaveChanges.Enabled = Changed
        btnUndo.Enabled = btnGeneralPrefsSaveChanges.Enabled
    End Sub


    Private Sub llXBMC_MovieFolderMappings_LinkClicked( sender As Object,  e As LinkLabelLinkClickedEventArgs) Handles llXBMC_MovieFolderMappings.LinkClicked 
        Dim frm As new frmConfigureXBMC_MC_Folders

        frm.Init(MovieFolderMappings)
        frm.ShowDialog

        SetBtnStates
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

        UpdatePreferences

        Preferences.SaveConfig
        SetBtnStates
    End Sub


    Sub UpdateFields
        tbXBMC_Address         .Text = Preferences.XBMC_Address
        tbXBMC_Port            .Text = Preferences.XBMC_Port
        tbXBMC_Username        .Text = Preferences.XBMC_Username
        tbXBMC_Password        .Text = Preferences.XBMC_Password
        tbXBMC_UserdataFolder  .Text = Preferences.XBMC_UserdataFolder
        tbXBMC_TexturesDb      .Text = Preferences.XBMC_TexturesDb
        tbXBMC_ThumbnailsFolder.Text = Preferences.XBMC_ThumbnailsFolder
        MovieFolderMappings.Assign(Preferences.XBMC_MC_MovieFolderMappings)
    End Sub

    Sub UpdatePreferences
        Preferences.XBMC_Address           = tbXBMC_Address         .Text
        Preferences.XBMC_Port              = tbXBMC_Port            .Text
        Preferences.XBMC_Username          = tbXBMC_Username        .Text
        Preferences.XBMC_Password          = tbXBMC_Password        .Text
        Preferences.XBMC_UserdataFolder    = tbXBMC_UserdataFolder  .Text
        Preferences.XBMC_TexturesDb        = tbXBMC_TexturesDb      .Text
        Preferences.XBMC_ThumbnailsFolder  = tbXBMC_ThumbnailsFolder.Text
        Preferences.XBMC_MC_MovieFolderMappings.Assign(MovieFolderMappings)
    End Sub



    Private Sub AnyFieldChanged(sender As Object,  e As EventArgs) Handles  tbXBMC_Address       .TextChanged, tbXBMC_Port.TextChanged      , tbXBMC_Username        .TextChanged, tbXBMC_Password.TextChanged, _
                                                                            tbXBMC_UserdataFolder.TextChanged, tbXBMC_TexturesDb.TextChanged, tbXBMC_ThumbnailsFolder.TextChanged
        SetBtnStates
    End Sub


    Private Sub btnValidate_Click( sender As Object,  e As EventArgs) Handles btnValidate.Click

        Dim tmp_MovieFolderMappings As XBMC_MC_FolderMappings = New XBMC_MC_FolderMappings

        Dim tmp_tbXBMC_Address          As String = Preferences.XBMC_Address
        Dim tmp_tbXBMC_Port             As String = Preferences.XBMC_Port
        Dim tmp_tbXBMC_Username         As String = Preferences.XBMC_Username
        Dim tmp_tbXBMC_Password         As String = Preferences.XBMC_Password
        Dim tmp_tbXBMC_UserdataFolder   As String = Preferences.XBMC_UserdataFolder
        Dim tmp_tbXBMC_TexturesDb       As String = Preferences.XBMC_TexturesDb
        Dim tmp_tbXBMC_ThumbnailsFolder As String = Preferences.XBMC_ThumbnailsFolder

        tmp_MovieFolderMappings.Assign(Preferences.XBMC_MC_MovieFolderMappings)

        UpdatePreferences

        tbDialogue.Clear

        Dim canConnect As Boolean = Preferences.XBMC_CanConnect 

        Dim overAll As Boolean = _
            ShowTest("XBMC PC Ping"                      , Preferences.XBMC_CanPing                  , 1) And
            ShowTest("XBMC Connect"                      , canConnect                                , 2) And
            ShowTest("Userdata Folder"                   , Preferences.XBMC_UserdataFolder_Valid     , 5) And
            ShowTest("TexturesDb File"                   , Preferences.XBMC_TexturesDbFile_Valid     , 6) And
            ShowTest("TexturesDb Connection"             , Preferences.XBMC_TexturesDb_Conn_Valid    , 6) And
            ShowTest("TexturesDb Version (Frodo needed)" , Preferences.XBMC_TexturesDb_Version_Valid , 6) And
            ShowTest("Thumbnails Folder"                 , Preferences.XBMC_ThumbnailsFolder_Valid   , 7) And
            ShowTest("Movie Folder mappings set (NB Actual paths not validated as applicable to XBMC PC)" , Preferences.XBMC_MC_MovieFolderMappings.Initialised, 8) 
        
        UpdateImage(3 ,canConnect)
        UpdateImage(4 ,canConnect)

        ShowTest("Overall", overAll , 10) 

        If Not canConnect Then
            AppendDialogue("")
            AppendDialogue("****************************************")
            AppendDialogue("Check System - Settings - Servies - Webserver has:")
            AppendDialogue("    - Allow control of XBMC via HTTP' is checked ")
            AppendDialogue("    - Port, Username and Password match")
            AppendDialogue("    - Web interface is set to 'Default'")
            AppendDialogue("If you are trying to connect to XBMC on a remote PC, make sure:")
            AppendDialogue("    - The PCs' IP address is static")
            AppendDialogue("    - System - Settings - Servies - Remote Control has 'Allow programs on other systems to control XBMC' is checked")
            AppendDialogue("****************************************")
        End If

        Preferences.XBMC_Address           = tmp_tbXBMC_Address         
        Preferences.XBMC_Port              = tmp_tbXBMC_Port            
        Preferences.XBMC_Username          = tmp_tbXBMC_Username        
        Preferences.XBMC_Password          = tmp_tbXBMC_Password        
        Preferences.XBMC_UserdataFolder    = tmp_tbXBMC_UserdataFolder  
        Preferences.XBMC_TexturesDb        = tmp_tbXBMC_TexturesDb      
        Preferences.XBMC_ThumbnailsFolder  = tmp_tbXBMC_ThumbnailsFolder

        Preferences.XBMC_MC_MovieFolderMappings.Assign(tmp_MovieFolderMappings)



    End Sub
  

    Function ShowTest(test As String, result As Boolean, row As Integer)

        AppendTestDialoague(test,result)
        UpdateImage        (row ,result)

        Return result
    End Function
  

    Sub AppendTestDialoague(testing As String, result As Boolean)
        AppendDialogue( testing & " -> " & IIf(result,"Passed","Failed") )
    End Sub
 
    Sub ClearImages
        For row As Integer = 1 to 10
            Try
                GetPictureBox(row).Image = Nothing
            Catch
            End Try
        Next
    End Sub

    Sub UpdateImage(row As Integer,result As Boolean)
        GetPictureBox(row).Image = IIf(result,Global.Media_Companion.My.Resources.Resources.correct,Global.Media_Companion.My.Resources.Resources.incorrect)
    End Sub
    
    Function GetPictureBox(row As Integer) As PictureBox
        Return tlpXbmcLink.GetControlFromPosition(3,row)
    End Function

    Sub AppendDialogue(msg As String)
        tbDialogue.Text += msg + Environment.NewLine
    End Sub

    Private Sub btnUndo_Click( sender As Object,  e As EventArgs) Handles btnUndo.Click
        UpdateFields
        SetBtnStates
    End Sub
End Class
