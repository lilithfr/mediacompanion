Imports System.Reflection
Imports System.Xml

Public Class MediaStubs
#Region "Properties"

    Private Property alt_title As String = ""
    Dim Private saveload As Boolean
    Dim Private StubFilename As String = ""

#End Region             'Properties

#Region "Event Handlers"
    Private Sub AnyFieldChanged(ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles tb_Stub_filename.KeyUp 
        If tb_Stub_filename.Text <> "" Then
            saveload = True
        Else
            saveload = False 
        End If
        ToggleSaveLoad()
        tb_Stub_Alt_Title.Text = tb_Stub_filename.Text
        UpdateStubFilename()
        UpdateStubfile()
    End Sub

    Private Sub customAltorMessage_TextChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles tb_Stub_Alt_Title.KeyUp, tb_Stub_Message.KeyUp
        UpdateStubfile()
    End Sub

    Private Sub cb_Stub_Formats_SelectedIndexChanged( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles cb_Stub_Formats.SelectedIndexChanged
        UpdateStubFilename() 
    End Sub

#End Region         'Event Handlers


    Private Sub MediaStubs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btn_StubClear.PerformClick()
    End Sub

#Region "Buttons"
    Private Sub btn_Browse_Offline_Folder_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_Browse_Offline_Folder.Click
        Try
            Dim theFolderBrowser As New FolderBrowserDialog
            theFolderBrowser.Description = "Please Select Folder to Add to DB (Subfolders will also be added)"
            theFolderBrowser.ShowNewFolderButton = True
            theFolderBrowser.RootFolder = System.Environment.SpecialFolder.Desktop
            theFolderBrowser.SelectedPath = Preferences.lastpath
            If theFolderBrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim newfolder As String = ""
                newfolder = theFolderBrowser.SelectedPath
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

    Private Sub btn_StubSetDefaultMessage_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_StubSetDefaultMessage.Click
            Preferences.stubmessage = tb_Stub_Message.Text
            Preferences.SaveConfig 
    End Sub

    Private Sub btn_StubSaveStub_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_StubSaveStub.Click
        If saveload Then
            Dim success As Boolean = savestub
            If success Then
                MsgBox("Stub file: [" & StubFilename & "] saved!")
                btn_StubClear.PerformClick()
            Else
                MsgBox("Issue saving stub file" & vbCrLf & "make sure file is not protected")
            End If
        Else
            StubFilename = utilities.GetFileNameFromPath(Getstubfilename)
            If StubFilename <> "" Then
                Dim success As Boolean = loadstub
                If Not success Then
                    MsgBox("Unable to load stub file" & vbCrLf & "file may not be empty or an XML file")
                End If
            Else
                MsgBox("No File selected")
            End If
        End If
    End Sub

    Private Sub btn_StubClear_Click( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles btn_StubClear.Click
        tb_Stub_folder.text = Preferences.stubfolder
        tb_Stub_Alt_Title.Text = ""
        tb_Stub_filename.Text = ""
        tb_disc_filename.Text = ""
        tb_Stub_Message.Text = Preferences.stubmessage
        cb_Stub_Formats.SelectedIndex = 0
        UpdateStubFilename()
        UpdateStubfile()
        saveload = False
        ToggleSaveLoad()
    End Sub
#End Region                'Buttons

#Region "Subs"
    Private Sub UpdateStubFilename()
        StubFilename = tb_Stub_filename.Text.Trim & "." & cb_Stub_Formats.SelectedItem.ToString.ToLower & ".disc"
        If tb_Stub_filename.Text <> "" Then
            tb_disc_filename.Text = StubFilename 
        Else
            tb_disc_filename.Text = ""
            StubFilename = ""
        End If
    End Sub

    Private Sub UpdateStubfile()
        If tb_Stub_filename.Text = "" Then
            tb_Stub_File.Text = ""
            Exit Sub
        End If
        Dim StubFile As String = ""
        Dim t As String = "    "
        StubFile = "<discstub>"
        StubFile &= vbCrLf & t & "<title>" & tb_Stub_Alt_Title.Text.Trim & "</title>"
        StubFile &= vbCrLf & t & "<message>" & tb_Stub_Message.Text.Trim & "</message>"
        StubFile &= vbCrLf & "</discstub>"
        tb_Stub_File.Text = StubFile 
    End Sub
    
    Private Sub ToggleSaveLoad()
        If saveload Then
            btn_StubSaveStub.Text = "Save Media Stub File"
        Else
            btn_StubSaveStub.Text = "Load Media Stub File"
        End If
    End Sub
#End Region                   'Sub Routines

#Region "Functions"
    Private Function savestub As Boolean
        Dim success As Boolean = False
        Try
            Dim doc As New XmlDocument
            Dim root As XmlElement
            root = doc.CreateElement("discstub")
            root.AppendChild(doc, "title", tb_Stub_filename.Text.Trim)
            root.AppendChild(doc, "message", tb_Stub_Message.Text.Trim)
            doc.AppendChild(root)

            Dim output As XmlTextWriter = Nothing
            Try
                output = New XmlTextWriter(Preferences.stubfolder & "\" & StubFilename, System.Text.Encoding.UTF8)
                output.Formatting = Formatting.Indented
                doc.WriteTo(output)
            Catch 
                Return False
            Finally
                If Not IsNothing(output) Then output.Close
            End Try

            success = True
        Catch
        End Try
        Return success
    End Function

    Private Function loadstub As Boolean
        Dim success As Boolean = False
        Try
            Dim lstub As New XmlDocument
            Try
                lstub.Load(Preferences.stubfolder & "\" & StubFilename)
            Catch
                Return False
            End Try
            For Each thisresult As XmlNode In lstub("discstub")
                If thisresult.InnerXml <> "" Then
                    Select Case thisresult.Name
                        Case "title"
                            tb_Stub_Alt_Title.Text = thisresult.InnerText
                        Case "message"
                            tb_Stub_Message.Text = thisresult.InnerText
                    End Select
                    success = True
                End If
            Next
            Dim sp() As String = StubFilename.Split(".")
            tb_Stub_filename.Text = sp(0)
            If sp(1).Tolower <> "disc" Then
                Dim n As Integer = 0
                For i = 0 to 4
                    If cb_Stub_Formats.Items(i).ToString.ToLower = sp(1) Then
                        n = i
                        Exit For
                    End If
                Next
                cb_Stub_Formats.SelectedIndex = n
            End If
            tb_disc_filename.Text = StubFilename 
            UpdateStubfile()
            saveload = True
            ToggleSaveLoad
        Catch
            Return False
        End Try
        Return success
    End Function

    Private Function Getstubfilename As String
        Dim lname As String = ""
        Try
            Dim filebrowser As New OpenFileDialog
            Dim mstrProgramFilesPath As String = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
            filebrowser.InitialDirectory = Preferences.stubfolder
            filebrowser.Filter = "Disc Files|*.disc"
            filebrowser.Title = "Select Media Stub ""disc"" file to load"
            If filebrowser.ShowDialog = Windows.Forms.DialogResult.OK Then
                lname = filebrowser.FileName
            End If
        Catch
            Return ""
        End Try
        Return lname
    End Function
#End Region              'Functions
    
End Class
