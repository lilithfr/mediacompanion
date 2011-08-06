Imports System.Threading

Public Class TaskCache
    Public Shared Tasks As New List(Of ITask)

    Public Shared MainThread As New Thread(New ThreadStart(AddressOf RunTasks))
    Public Shared Threads As New List(Of Threading.Thread)
    Public Shared Property MaxThreads As Integer = 16

    Public Shared Property Done As Boolean


    Public Shared Sub RunTasks()
        Do Until Done
            Try 'This Try block lets me be really lazy and ignore the fact that if a new task is created it breaks the enumeration and starts the whole process
                For Each Task In Tasks
                    If Task.State = TaskState.NotStarted Then
                        If Not Threads.Count > MaxThreads Then
                            Dim NewThread As Thread = New Thread(New ThreadStart(AddressOf Task.Run))

                            Threads.Add(NewThread)

                            NewThread.Start()
                        End If
                    End If
                Next

                Dim I As Integer = 0
                Do
                    If I > Threads.Count - 1 Then Exit Do

                    If Threads(I).ThreadState = ThreadState.Stopped Then
                        Threads.Remove(Threads(I))
                        Continue Do
                    End If
                    I += 1
                Loop
            Catch

            Finally
                Thread.Sleep(1)
            End Try
        Loop
    End Sub
End Class

'Private Sub cmdTasks_Refresh_Click(sender As System.Object, e As System.EventArgs) Handles cmdTasks_Refresh.Click
'    lstTasks.Items.Clear()
'    tv_MissingArtDownload(tv_ShowSelectedCurrently)

'    For Each Item As ITask In TaskCache.Tasks
'        lstTasks.Items.Add(Item)
'    Next
'End Sub

'Private Sub lstTasks_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstTasks.SelectedIndexChanged
'    If lstTasks.SelectedItem Is Nothing Then Exit Sub

'    Dim SelectedTask As ITask

'    SelectedTask = lstTasks.SelectedItem

'    Select Case SelectedTask.State
'        Case TaskState.Completed
'            lblTask_State.Text = "Completed"
'        Case TaskState.BackgroundWorkComplete
'            lblTask_State.Text = "Background Completed"
'        Case TaskState.CriticalFault
'            lblTask_State.Text = "Critial Fault"
'        Case TaskState.Fault
'            lblTask_State.Text = "Fault"
'        Case TaskState.Halted
'            lblTask_State.Text = "Halted"
'        Case TaskState.NotStarted
'            lblTask_State.Text = "Not Started"
'        Case TaskState.WaitingForUserInput
'            lblTask_State.Text = "Waiting For Input"
'        Case TaskState.Running
'            lblTask_State.Text = "Running"
'    End Select

'    lblTask_Attempts.Text = SelectedTask.Attempts

'    cmbTasks_Arguments.Items.Clear()
'    cmbTasks_Arguments.Text = ""
'    For Each Item In SelectedTask.Arguments
'        cmbTasks_Arguments.Items.Add(Item)

'    Next
'    If cmbTasks_Arguments.Items.Count > 0 Then cmbTasks_Arguments.SelectedIndex = 0

'    lstTasks_Dependancies.Items.Clear()
'    For Each Item In SelectedTask.Dependancies
'        lstTasks_Dependancies.Items.Add(Item)
'    Next

'    lstTasks_Messages.Items.Clear()
'    For Each Item In SelectedTask.Messages
'        lstTasks_Messages.Items.Add(Item)
'    Next
'End Sub

'Private Sub lstTasks_Messages_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lstTasks_Messages.SelectedIndexChanged
'    If lstTasks_Messages.SelectedItem Is Nothing Then Exit Sub

'    txtTasks_SelectedMessage.Text = lstTasks_Messages.SelectedItem
'End Sub

'Private Sub cmbTasks_Arguments_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbTasks_Arguments.SelectedIndexChanged
'    If cmbTasks_Arguments.SelectedItem Is Nothing Then Exit Sub

'    pnlTasks_Arguments.Controls.Clear()

'    Dim Item As KeyValuePair(Of String, Object) = cmbTasks_Arguments.SelectedItem
'    If TypeOf Item.Value Is String Then
'        pnlTasks_Arguments.Controls.Add(New TextBox() With {.Text = Item.Value, .Dock = DockStyle.Fill, .Multiline = True, .ScrollBars = ScrollBars.Both})
'    ElseIf TypeOf Item.Value Is TvShow Then
'        Dim TempShow As TvShow = Item.Value
'        pnlTasks_Arguments.Controls.Add(New TextBox() With {.Text = TempShow.Id.Value & " - " & TempShow.Title.Value, .Dock = DockStyle.Fill, .ScrollBars = ScrollBars.Both})
'    ElseIf TypeOf Item.Value Is TvEpisode Then
'        Dim TempEpisode As TvEpisode = Item.Value
'        pnlTasks_Arguments.Controls.Add(New TextBox() With {.Text = TempEpisode.Id.Value & " - " & TempEpisode.Title.Value, .Dock = DockStyle.Fill, .ScrollBars = ScrollBars.Both})
'    ElseIf TypeOf Item.Value Is Image Then
'        pnlTasks_Arguments.Controls.Add(New PictureBox() With {.Image = Item.Value, .Dock = DockStyle.Fill})
'    End If
'End Sub


'Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
'    Try
'        For Each Task In TaskCache.Tasks
'            If Task.State = TaskState.BackgroundWorkComplete Then
'                Task.FinishWork()
'            End If
'            Windows.Forms.Application.DoEvents()
'        Next
'    Catch

'    End Try
'End Sub

'Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

'End Sub

'Private Sub TaskListUpdater_Tick(sender As System.Object, e As System.EventArgs) Handles TaskListUpdater.Tick
'    'cmdTasks_Refresh_Click(Nothing, Nothing)
'    TaskListUpdater.Enabled = False
'    lstTasks.ResetText()

'End Sub