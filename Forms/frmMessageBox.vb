
Public Class frmMessageBox

    Public Property Cancelled As Boolean
    Public Property btn1site As String
    Public Property IsActive As Boolean

    Public Sub New(ByVal line1 As String, Optional ByVal line2 As String = "", Optional ByVal line3 As String = "", Optional ByVal Btn1 As String = "", Optional ByVal Btn2 as String = "")
        InitializeComponent()
        If Btn1 = "" Then 
            Button1.Visible = False
        ElseIf Btn1 = "1" Then
            btn1site = "tvdb"
            Button1.Text = ""
            Button1.BackgroundImageLayout = ImageLayout.Stretch
            Button1.BackgroundImage = My.Resources.TVDB_sm  
            Button1.Visible = True
        ElseIf Btn1 = "2" Then
            btn1site = "tmdb"
            Button1.Text = ""
            Button1.BackgroundImageLayout = ImageLayout.Stretch
            Button1.BackgroundImage = My.Resources.TMDB_Icon
            Button1.Visible = True
        End If

        If Btn2 = "" Then 
            Button2.Visible = False
        Else
            Button2.Text = ""
            Button2.BackgroundImageLayout = ImageLayout.Stretch
            Button2.BackgroundImage = My.Resources.imdb1 
            Button2.Visible = True
        End If

        If line2 = "" And line3 = "" Then
            TextBox2.Text = line1
            TextBox1.Visible = False
            TextBox3.Visible = False
            Exit Sub
        Else 
            TextBox1.Visible = True
            TextBox3.Visible = True
        End If
        TextBox1.Text = line1
        TextBox2.Text = line2
        TextBox3.Text = line3
        IsActive = True
        Me.TopMost = True
    End Sub

    Private Sub DeactivateMessageBox(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Deactivate
        Try
            Me.Activate()
            Me.BringToFront()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub CloseMessageBox(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Me.Cursor = Cursors.Default
            IsActive = False
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub LoadMessageBox(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MessageBoxLostFocus(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LostFocus
        Try
            Me.Activate()
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox1.TextChanged
        Try
            TextBox1.SelectionStart = TextBox1.Text.Length
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox2.TextChanged
        Try
            TextBox2.SelectionStart = TextBox2.Text.Length
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TextBox3.TextChanged
        Try
            TextBox3.SelectionStart = TextBox3.Text.Length
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub MessageBoxVisibilityChanged(ByVal sender As Object, ByVal e As EventArgs) Handles Me.VisibleChanged
        Try
            If Me.Visible = False Then
                Windows.Forms.Cursor.Current = Cursors.Default
            Else
                Windows.Forms.Cursor.Current = Cursors.WaitCursor
            End If
            TextBox1.SelectionStart = TextBox1.Text.Length
            TextBox2.SelectionStart = TextBox2.Text.Length
            TextBox3.SelectionStart = TextBox3.Text.Length
        Catch ex As Exception
            ExceptionHandler.LogError(ex)
        End Try
    End Sub

    Private Sub Button1_Click( sender As System.Object,  e As System.EventArgs) Handles Button1.Click
        Try
            Preferences.WebSite = btn1site
            Me.Close()
        Catch ex As Exception

        End Try
    
    End Sub

    Private Sub Button2_Click( sender As System.Object,  e As System.EventArgs) Handles Button2.Click
        Try
            Preferences.WebSite = "imdb"
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub frmMessageBox_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        If  e.KeyCode=Keys.Escape Then
            Cancelled = True
        End If
    End Sub

End Class