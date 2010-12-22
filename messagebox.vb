Public Class message_box
    Public Sub New(ByVal line1 As String, Optional ByVal line2 As String = "", Optional ByVal line3 As String = "")
        Me.InitializeComponent()
        TextBox1.Text = line1
        TextBox2.Text = line2
        TextBox3.Text = line3
    End Sub
    Private Sub message_box_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.Activate()
        Me.BringToFront()
    End Sub

    Private Sub message_box_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        System.Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub message_box_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'System.Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Me.Cursor = Cursors.WaitCursor
        'Me.TopMost = True
    End Sub


    Private Sub message_box_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
        Me.Activate()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        TextBox1.SelectionStart = TextBox1.Text.Length
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        TextBox2.SelectionStart = TextBox2.Text.Length
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        TextBox3.SelectionStart = TextBox3.Text.Length
    End Sub

    Private Sub message_box_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = False Then
            Me.Cursor.Current = Cursors.Default
        Else
            Me.Cursor.Current = Cursors.WaitCursor
        End If
        TextBox1.SelectionStart = TextBox1.Text.Length
        TextBox2.SelectionStart = TextBox2.Text.Length
        TextBox3.SelectionStart = TextBox3.Text.Length
    End Sub
End Class