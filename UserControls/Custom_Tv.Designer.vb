<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Custom_Tv
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Tp1 = New System.Windows.Forms.TabPage()
        Me.SpCont1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btn_Cust_New = New System.Windows.Forms.Button()
        Me.btn_Cust_Refresh = New System.Windows.Forms.Button()
        Me.tb_ShowCount = New System.Windows.Forms.TextBox()
        Me.tb_EpCount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CustTreeview1 = New System.Windows.Forms.TreeView()
        Me.Panel_Show = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_ShRunTime = New System.Windows.Forms.Label()
        Me.lbl_ShID = New System.Windows.Forms.Label()
        Me.lbl_ShPremiered = New System.Windows.Forms.Label()
        Me.lbl_ShStudio = New System.Windows.Forms.Label()
        Me.lbl_ShGenre = New System.Windows.Forms.Label()
        Me.lbl_ShCert = New System.Windows.Forms.Label()
        Me.tb_ShPlot = New System.Windows.Forms.TextBox()
        Me.tb_ShRunTime = New System.Windows.Forms.TextBox()
        Me.tb_ShId = New System.Windows.Forms.TextBox()
        Me.tb_ShPremiered = New System.Windows.Forms.TextBox()
        Me.tb_ShStudio = New System.Windows.Forms.TextBox()
        Me.tb_ShGenre = New System.Windows.Forms.TextBox()
        Me.tb_ShCert = New System.Windows.Forms.TextBox()
        Me.lbl_ShPlot = New System.Windows.Forms.Label()
        Me.Panel_Episode = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.lbl_EpDirector = New System.Windows.Forms.Label()
        Me.lbl_EpPlot = New System.Windows.Forms.Label()
        Me.lbl_EpCredits = New System.Windows.Forms.Label()
        Me.lbl_EpAired = New System.Windows.Forms.Label()
        Me.lbl_EpPath = New System.Windows.Forms.Label()
        Me.lbl_EpFilename = New System.Windows.Forms.Label()
        Me.lbl_EpDetails = New System.Windows.Forms.Label()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Show_SplCont1 = New System.Windows.Forms.SplitContainer()
        Me.Show_SplCont2 = New System.Windows.Forms.SplitContainer()
        Me.pb_Cust_Fanart = New System.Windows.Forms.PictureBox()
        Me.pb_Cust_Poster = New System.Windows.Forms.PictureBox()
        Me.pb_Cust_Banner = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TP2 = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TP3 = New System.Windows.Forms.TabPage()
        Me.tb_EpDirector = New System.Windows.Forms.TextBox()
        Me.tb_EpCredits = New System.Windows.Forms.TextBox()
        Me.tb_EpAired = New System.Windows.Forms.TextBox()
        Me.tb_EpPath = New System.Windows.Forms.TextBox()
        Me.tb_EpFilename = New System.Windows.Forms.TextBox()
        Me.tb_EpDetails = New System.Windows.Forms.TextBox()
        Me.tb_EpPlot = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.Tp1.SuspendLayout()
        CType(Me.SpCont1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SpCont1.Panel1.SuspendLayout()
        Me.SpCont1.Panel2.SuspendLayout()
        Me.SpCont1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel_Show.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.Panel_Episode.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.Show_SplCont1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Show_SplCont1.Panel1.SuspendLayout()
        Me.Show_SplCont1.Panel2.SuspendLayout()
        Me.Show_SplCont1.SuspendLayout()
        CType(Me.Show_SplCont2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Show_SplCont2.Panel1.SuspendLayout()
        Me.Show_SplCont2.Panel2.SuspendLayout()
        Me.Show_SplCont2.SuspendLayout()
        CType(Me.pb_Cust_Fanart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_Cust_Poster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_Cust_Banner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TP2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tp1)
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Controls.Add(Me.TP3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1060, 660)
        Me.TabControl1.TabIndex = 0
        '
        'Tp1
        '
        Me.Tp1.Controls.Add(Me.SpCont1)
        Me.Tp1.Location = New System.Drawing.Point(4, 22)
        Me.Tp1.Name = "Tp1"
        Me.Tp1.Padding = New System.Windows.Forms.Padding(3)
        Me.Tp1.Size = New System.Drawing.Size(1052, 634)
        Me.Tp1.TabIndex = 0
        Me.Tp1.Text = "Main Browser"
        Me.Tp1.UseVisualStyleBackColor = True
        '
        'SpCont1
        '
        Me.SpCont1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SpCont1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SpCont1.Location = New System.Drawing.Point(3, 3)
        Me.SpCont1.Name = "SpCont1"
        '
        'SpCont1.Panel1
        '
        Me.SpCont1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SpCont1.Panel2
        '
        Me.SpCont1.Panel2.Controls.Add(Me.Panel_Show)
        Me.SpCont1.Panel2.Controls.Add(Me.Panel_Episode)
        Me.SpCont1.Panel2.Controls.Add(Me.TableLayoutPanel3)
        Me.SpCont1.Size = New System.Drawing.Size(1046, 628)
        Me.SpCont1.SplitterDistance = 348
        Me.SpCont1.SplitterWidth = 8
        Me.SpCont1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btn_Cust_New, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.btn_Cust_Refresh, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_ShowCount, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.tb_EpCount, 5, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.CustTreeview1, 1, 5)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 8
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(344, 624)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btn_Cust_New
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_Cust_New, 2)
        Me.btn_Cust_New.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cust_New.Location = New System.Drawing.Point(11, 11)
        Me.btn_Cust_New.Name = "btn_Cust_New"
        Me.btn_Cust_New.Size = New System.Drawing.Size(130, 39)
        Me.btn_Cust_New.TabIndex = 0
        Me.btn_Cust_New.Text = "Add New Show"
        Me.btn_Cust_New.UseVisualStyleBackColor = True
        '
        'btn_Cust_Refresh
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.btn_Cust_Refresh, 2)
        Me.btn_Cust_Refresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_Cust_Refresh.Location = New System.Drawing.Point(168, 11)
        Me.btn_Cust_Refresh.Name = "btn_Cust_Refresh"
        Me.btn_Cust_Refresh.Size = New System.Drawing.Size(130, 39)
        Me.btn_Cust_Refresh.TabIndex = 1
        Me.btn_Cust_Refresh.Text = "Refresh All"
        Me.btn_Cust_Refresh.UseVisualStyleBackColor = True
        '
        'tb_ShowCount
        '
        Me.tb_ShowCount.Location = New System.Drawing.Point(113, 64)
        Me.tb_ShowCount.Name = "tb_ShowCount"
        Me.tb_ShowCount.Size = New System.Drawing.Size(33, 20)
        Me.tb_ShowCount.TabIndex = 2
        '
        'tb_EpCount
        '
        Me.tb_EpCount.Location = New System.Drawing.Point(270, 64)
        Me.tb_EpCount.Name = "tb_EpCount"
        Me.tb_EpCount.Size = New System.Drawing.Size(33, 20)
        Me.tb_EpCount.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.65!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(11, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Total Shows:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(168, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Total Episodes:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CustTreeview1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.CustTreeview1, 7)
        Me.CustTreeview1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustTreeview1.Location = New System.Drawing.Point(11, 93)
        Me.CustTreeview1.Name = "CustTreeview1"
        Me.TableLayoutPanel1.SetRowSpan(Me.CustTreeview1, 2)
        Me.CustTreeview1.Size = New System.Drawing.Size(313, 519)
        Me.CustTreeview1.TabIndex = 6
        '
        'Panel_Show
        '
        Me.Panel_Show.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Show.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel_Show.Controls.Add(Me.TableLayoutPanel4)
        Me.Panel_Show.Location = New System.Drawing.Point(23, 371)
        Me.Panel_Show.Name = "Panel_Show"
        Me.Panel_Show.Size = New System.Drawing.Size(577, 241)
        Me.Panel_Show.TabIndex = 4
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 9
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShRunTime, 1, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShID, 1, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShPremiered, 1, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShStudio, 5, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShGenre, 5, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShCert, 5, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShPlot, 3, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShRunTime, 3, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShId, 3, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShPremiered, 3, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShStudio, 7, 3)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShGenre, 7, 5)
        Me.TableLayoutPanel4.Controls.Add(Me.tb_ShCert, 7, 7)
        Me.TableLayoutPanel4.Controls.Add(Me.lbl_ShPlot, 1, 1)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 9
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(575, 239)
        Me.TableLayoutPanel4.TabIndex = 0
        '
        'lbl_ShRunTime
        '
        Me.lbl_ShRunTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShRunTime.Location = New System.Drawing.Point(23, 114)
        Me.lbl_ShRunTime.Name = "lbl_ShRunTime"
        Me.lbl_ShRunTime.Size = New System.Drawing.Size(78, 32)
        Me.lbl_ShRunTime.TabIndex = 1
        Me.lbl_ShRunTime.Text = "Runtime:"
        Me.lbl_ShRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ShID
        '
        Me.lbl_ShID.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShID.Location = New System.Drawing.Point(23, 156)
        Me.lbl_ShID.Name = "lbl_ShID"
        Me.lbl_ShID.Size = New System.Drawing.Size(78, 32)
        Me.lbl_ShID.TabIndex = 2
        Me.lbl_ShID.Text = "Show ID:"
        Me.lbl_ShID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ShPremiered
        '
        Me.lbl_ShPremiered.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShPremiered.Location = New System.Drawing.Point(23, 201)
        Me.lbl_ShPremiered.Name = "lbl_ShPremiered"
        Me.lbl_ShPremiered.Size = New System.Drawing.Size(78, 32)
        Me.lbl_ShPremiered.TabIndex = 3
        Me.lbl_ShPremiered.Text = "Premiered:"
        Me.lbl_ShPremiered.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ShStudio
        '
        Me.lbl_ShStudio.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShStudio.Location = New System.Drawing.Point(294, 114)
        Me.lbl_ShStudio.Name = "lbl_ShStudio"
        Me.lbl_ShStudio.Size = New System.Drawing.Size(92, 32)
        Me.lbl_ShStudio.TabIndex = 4
        Me.lbl_ShStudio.Text = "Studio:"
        Me.lbl_ShStudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ShGenre
        '
        Me.lbl_ShGenre.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShGenre.Location = New System.Drawing.Point(294, 156)
        Me.lbl_ShGenre.Name = "lbl_ShGenre"
        Me.lbl_ShGenre.Size = New System.Drawing.Size(81, 32)
        Me.lbl_ShGenre.TabIndex = 5
        Me.lbl_ShGenre.Text = "Genre:"
        Me.lbl_ShGenre.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_ShCert
        '
        Me.lbl_ShCert.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShCert.Location = New System.Drawing.Point(294, 201)
        Me.lbl_ShCert.Name = "lbl_ShCert"
        Me.lbl_ShCert.Size = New System.Drawing.Size(92, 32)
        Me.lbl_ShCert.TabIndex = 6
        Me.lbl_ShCert.Text = "Certification:"
        Me.lbl_ShCert.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tb_ShPlot
        '
        Me.TableLayoutPanel4.SetColumnSpan(Me.tb_ShPlot, 5)
        Me.tb_ShPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShPlot.Location = New System.Drawing.Point(117, 11)
        Me.tb_ShPlot.Multiline = True
        Me.tb_ShPlot.Name = "tb_ShPlot"
        Me.tb_ShPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tb_ShPlot.Size = New System.Drawing.Size(440, 91)
        Me.tb_ShPlot.TabIndex = 7
        '
        'tb_ShRunTime
        '
        Me.tb_ShRunTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShRunTime.Location = New System.Drawing.Point(117, 120)
        Me.tb_ShRunTime.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShRunTime.Name = "tb_ShRunTime"
        Me.tb_ShRunTime.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShRunTime.TabIndex = 8
        '
        'tb_ShId
        '
        Me.tb_ShId.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShId.Location = New System.Drawing.Point(117, 162)
        Me.tb_ShId.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShId.Name = "tb_ShId"
        Me.tb_ShId.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShId.TabIndex = 9
        '
        'tb_ShPremiered
        '
        Me.tb_ShPremiered.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShPremiered.Location = New System.Drawing.Point(117, 207)
        Me.tb_ShPremiered.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShPremiered.Name = "tb_ShPremiered"
        Me.tb_ShPremiered.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShPremiered.TabIndex = 10
        '
        'tb_ShStudio
        '
        Me.tb_ShStudio.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShStudio.Location = New System.Drawing.Point(403, 120)
        Me.tb_ShStudio.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShStudio.Name = "tb_ShStudio"
        Me.tb_ShStudio.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShStudio.TabIndex = 11
        '
        'tb_ShGenre
        '
        Me.tb_ShGenre.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShGenre.Location = New System.Drawing.Point(403, 162)
        Me.tb_ShGenre.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShGenre.Name = "tb_ShGenre"
        Me.tb_ShGenre.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShGenre.TabIndex = 12
        '
        'tb_ShCert
        '
        Me.tb_ShCert.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_ShCert.Location = New System.Drawing.Point(403, 207)
        Me.tb_ShCert.Margin = New System.Windows.Forms.Padding(3, 6, 3, 3)
        Me.tb_ShCert.Name = "tb_ShCert"
        Me.tb_ShCert.Size = New System.Drawing.Size(154, 20)
        Me.tb_ShCert.TabIndex = 13
        '
        'lbl_ShPlot
        '
        Me.lbl_ShPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_ShPlot.Location = New System.Drawing.Point(23, 8)
        Me.lbl_ShPlot.Name = "lbl_ShPlot"
        Me.lbl_ShPlot.Size = New System.Drawing.Size(78, 33)
        Me.lbl_ShPlot.TabIndex = 0
        Me.lbl_ShPlot.Text = "Plot:"
        Me.lbl_ShPlot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel_Episode
        '
        Me.Panel_Episode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel_Episode.Controls.Add(Me.TableLayoutPanel5)
        Me.Panel_Episode.Location = New System.Drawing.Point(23, 371)
        Me.Panel_Episode.Name = "Panel_Episode"
        Me.Panel_Episode.Size = New System.Drawing.Size(577, 241)
        Me.Panel_Episode.TabIndex = 3
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 9
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 17.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 161.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpPlot, 3, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpDetails, 3, 11)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpFilename, 3, 9)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpPath, 3, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpAired, 3, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpCredits, 7, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.tb_EpDirector, 3, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpDirector, 1, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpPlot, 1, 3)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpCredits, 5, 1)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpAired, 1, 5)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpPath, 1, 7)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpFilename, 1, 9)
        Me.TableLayoutPanel5.Controls.Add(Me.lbl_EpDetails, 1, 11)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 13
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23.0!))
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(577, 241)
        Me.TableLayoutPanel5.TabIndex = 0
        '
        'lbl_EpDirector
        '
        Me.lbl_EpDirector.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpDirector.Location = New System.Drawing.Point(23, 8)
        Me.lbl_EpDirector.Name = "lbl_EpDirector"
        Me.lbl_EpDirector.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpDirector.TabIndex = 0
        Me.lbl_EpDirector.Text = "Director"
        Me.lbl_EpDirector.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpPlot
        '
        Me.lbl_EpPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpPlot.Location = New System.Drawing.Point(23, 39)
        Me.lbl_EpPlot.Name = "lbl_EpPlot"
        Me.lbl_EpPlot.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpPlot.TabIndex = 1
        Me.lbl_EpPlot.Text = "Plot"
        Me.lbl_EpPlot.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpCredits
        '
        Me.lbl_EpCredits.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpCredits.Location = New System.Drawing.Point(295, 8)
        Me.lbl_EpCredits.Name = "lbl_EpCredits"
        Me.lbl_EpCredits.Size = New System.Drawing.Size(90, 23)
        Me.lbl_EpCredits.TabIndex = 2
        Me.lbl_EpCredits.Text = "Credits"
        Me.lbl_EpCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpAired
        '
        Me.lbl_EpAired.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpAired.Location = New System.Drawing.Point(23, 110)
        Me.lbl_EpAired.Name = "lbl_EpAired"
        Me.lbl_EpAired.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpAired.TabIndex = 3
        Me.lbl_EpAired.Text = "Aired:"
        Me.lbl_EpAired.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpPath
        '
        Me.lbl_EpPath.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpPath.Location = New System.Drawing.Point(23, 142)
        Me.lbl_EpPath.Name = "lbl_EpPath"
        Me.lbl_EpPath.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpPath.TabIndex = 4
        Me.lbl_EpPath.Text = "Path:"
        Me.lbl_EpPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpFilename
        '
        Me.lbl_EpFilename.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpFilename.Location = New System.Drawing.Point(23, 173)
        Me.lbl_EpFilename.Name = "lbl_EpFilename"
        Me.lbl_EpFilename.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpFilename.TabIndex = 5
        Me.lbl_EpFilename.Text = "Filename:"
        Me.lbl_EpFilename.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_EpDetails
        '
        Me.lbl_EpDetails.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_EpDetails.Location = New System.Drawing.Point(23, 204)
        Me.lbl_EpDetails.Name = "lbl_EpDetails"
        Me.lbl_EpDetails.Size = New System.Drawing.Size(79, 23)
        Me.lbl_EpDetails.TabIndex = 6
        Me.lbl_EpDetails.Text = "Details:"
        Me.lbl_EpDetails.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 12
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TextBox1, 1, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.Show_SplCont1, 1, 3)
        Me.TableLayoutPanel3.Controls.Add(Me.PictureBox1, 10, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 7
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 248.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(686, 624)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'TextBox1
        '
        Me.TableLayoutPanel3.SetColumnSpan(Me.TextBox1, 9)
        Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(23, 12)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(587, 26)
        Me.TextBox1.TabIndex = 0
        '
        'Show_SplCont1
        '
        Me.Show_SplCont1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TableLayoutPanel3.SetColumnSpan(Me.Show_SplCont1, 9)
        Me.Show_SplCont1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Show_SplCont1.Location = New System.Drawing.Point(23, 63)
        Me.Show_SplCont1.Name = "Show_SplCont1"
        Me.Show_SplCont1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'Show_SplCont1.Panel1
        '
        Me.Show_SplCont1.Panel1.Controls.Add(Me.Show_SplCont2)
        '
        'Show_SplCont1.Panel2
        '
        Me.Show_SplCont1.Panel2.Controls.Add(Me.pb_Cust_Banner)
        Me.Show_SplCont1.Size = New System.Drawing.Size(587, 293)
        Me.Show_SplCont1.SplitterDistance = 207
        Me.Show_SplCont1.TabIndex = 1
        '
        'Show_SplCont2
        '
        Me.Show_SplCont2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Show_SplCont2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Show_SplCont2.Location = New System.Drawing.Point(0, 0)
        Me.Show_SplCont2.Name = "Show_SplCont2"
        '
        'Show_SplCont2.Panel1
        '
        Me.Show_SplCont2.Panel1.Controls.Add(Me.pb_Cust_Fanart)
        '
        'Show_SplCont2.Panel2
        '
        Me.Show_SplCont2.Panel2.Controls.Add(Me.pb_Cust_Poster)
        Me.Show_SplCont2.Size = New System.Drawing.Size(587, 207)
        Me.Show_SplCont2.SplitterDistance = 386
        Me.Show_SplCont2.TabIndex = 0
        '
        'pb_Cust_Fanart
        '
        Me.pb_Cust_Fanart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb_Cust_Fanart.Location = New System.Drawing.Point(0, 0)
        Me.pb_Cust_Fanart.Name = "pb_Cust_Fanart"
        Me.pb_Cust_Fanart.Size = New System.Drawing.Size(382, 203)
        Me.pb_Cust_Fanart.TabIndex = 0
        Me.pb_Cust_Fanart.TabStop = False
        '
        'pb_Cust_Poster
        '
        Me.pb_Cust_Poster.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb_Cust_Poster.Location = New System.Drawing.Point(0, 0)
        Me.pb_Cust_Poster.Name = "pb_Cust_Poster"
        Me.pb_Cust_Poster.Size = New System.Drawing.Size(193, 203)
        Me.pb_Cust_Poster.TabIndex = 0
        Me.pb_Cust_Poster.TabStop = False
        '
        'pb_Cust_Banner
        '
        Me.pb_Cust_Banner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pb_Cust_Banner.Location = New System.Drawing.Point(0, 0)
        Me.pb_Cust_Banner.Name = "pb_Cust_Banner"
        Me.pb_Cust_Banner.Size = New System.Drawing.Size(583, 78)
        Me.pb_Cust_Banner.TabIndex = 0
        Me.pb_Cust_Banner.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Media_Companion.My.Resources.Resources.Save
        Me.PictureBox1.Location = New System.Drawing.Point(616, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(30, 29)
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'TP2
        '
        Me.TP2.Controls.Add(Me.TableLayoutPanel2)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(1052, 634)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "Artwork"
        Me.TP2.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1046, 628)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TP3
        '
        Me.TP3.Location = New System.Drawing.Point(4, 22)
        Me.TP3.Name = "TP3"
        Me.TP3.Padding = New System.Windows.Forms.Padding(3)
        Me.TP3.Size = New System.Drawing.Size(1052, 634)
        Me.TP3.TabIndex = 2
        Me.TP3.Text = "Folders"
        Me.TP3.UseVisualStyleBackColor = True
        '
        'tb_EpDirector
        '
        Me.tb_EpDirector.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpDirector.Location = New System.Drawing.Point(118, 10)
        Me.tb_EpDirector.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tb_EpDirector.Name = "tb_EpDirector"
        Me.tb_EpDirector.Size = New System.Drawing.Size(154, 20)
        Me.tb_EpDirector.TabIndex = 9
        '
        'tb_EpCredits
        '
        Me.tb_EpCredits.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpCredits.Location = New System.Drawing.Point(404, 11)
        Me.tb_EpCredits.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.tb_EpCredits.Name = "tb_EpCredits"
        Me.tb_EpCredits.Size = New System.Drawing.Size(156, 20)
        Me.tb_EpCredits.TabIndex = 10
        '
        'tb_EpAired
        '
        Me.tb_EpAired.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpAired.Location = New System.Drawing.Point(118, 112)
        Me.tb_EpAired.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tb_EpAired.Name = "tb_EpAired"
        Me.tb_EpAired.Size = New System.Drawing.Size(154, 20)
        Me.tb_EpAired.TabIndex = 11
        '
        'tb_EpPath
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.tb_EpPath, 5)
        Me.tb_EpPath.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpPath.Location = New System.Drawing.Point(118, 144)
        Me.tb_EpPath.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tb_EpPath.Name = "tb_EpPath"
        Me.tb_EpPath.Size = New System.Drawing.Size(441, 20)
        Me.tb_EpPath.TabIndex = 12
        '
        'tb_EpFilename
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.tb_EpFilename, 5)
        Me.tb_EpFilename.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpFilename.Location = New System.Drawing.Point(118, 175)
        Me.tb_EpFilename.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tb_EpFilename.Name = "tb_EpFilename"
        Me.tb_EpFilename.Size = New System.Drawing.Size(441, 20)
        Me.tb_EpFilename.TabIndex = 13
        '
        'tb_EpDetails
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.tb_EpDetails, 5)
        Me.tb_EpDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpDetails.Location = New System.Drawing.Point(118, 206)
        Me.tb_EpDetails.Margin = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tb_EpDetails.Name = "tb_EpDetails"
        Me.tb_EpDetails.Size = New System.Drawing.Size(441, 20)
        Me.tb_EpDetails.TabIndex = 14
        '
        'tb_EpPlot
        '
        Me.TableLayoutPanel5.SetColumnSpan(Me.tb_EpPlot, 5)
        Me.tb_EpPlot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tb_EpPlot.Location = New System.Drawing.Point(118, 42)
        Me.tb_EpPlot.Multiline = True
        Me.tb_EpPlot.Name = "tb_EpPlot"
        Me.tb_EpPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.tb_EpPlot.Size = New System.Drawing.Size(441, 57)
        Me.tb_EpPlot.TabIndex = 15
        '
        'Custom_Tv
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Custom_Tv"
        Me.Size = New System.Drawing.Size(1060, 660)
        Me.TabControl1.ResumeLayout(False)
        Me.Tp1.ResumeLayout(False)
        Me.SpCont1.Panel1.ResumeLayout(False)
        Me.SpCont1.Panel2.ResumeLayout(False)
        CType(Me.SpCont1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SpCont1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel_Show.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.Panel_Episode.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.Show_SplCont1.Panel1.ResumeLayout(False)
        Me.Show_SplCont1.Panel2.ResumeLayout(False)
        CType(Me.Show_SplCont1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Show_SplCont1.ResumeLayout(False)
        Me.Show_SplCont2.Panel1.ResumeLayout(False)
        Me.Show_SplCont2.Panel2.ResumeLayout(False)
        CType(Me.Show_SplCont2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Show_SplCont2.ResumeLayout(False)
        CType(Me.pb_Cust_Fanart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_Cust_Poster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_Cust_Banner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TP2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Tp1 As System.Windows.Forms.TabPage
    Friend WithEvents TP2 As System.Windows.Forms.TabPage
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SpCont1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btn_Cust_New As System.Windows.Forms.Button
    Friend WithEvents btn_Cust_Refresh As System.Windows.Forms.Button
    Friend WithEvents tb_ShowCount As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpCount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CustTreeview1 As System.Windows.Forms.TreeView
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TP3 As System.Windows.Forms.TabPage
    Friend WithEvents Panel_Show As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_ShPlot As System.Windows.Forms.Label
    Friend WithEvents lbl_ShRunTime As System.Windows.Forms.Label
    Friend WithEvents lbl_ShID As System.Windows.Forms.Label
    Friend WithEvents lbl_ShPremiered As System.Windows.Forms.Label
    Friend WithEvents lbl_ShStudio As System.Windows.Forms.Label
    Friend WithEvents lbl_ShGenre As System.Windows.Forms.Label
    Friend WithEvents lbl_ShCert As System.Windows.Forms.Label
    Friend WithEvents tb_ShPlot As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShRunTime As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShId As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShPremiered As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShStudio As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShGenre As System.Windows.Forms.TextBox
    Friend WithEvents tb_ShCert As System.Windows.Forms.TextBox
    Friend WithEvents Panel_Episode As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Show_SplCont1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Show_SplCont2 As System.Windows.Forms.SplitContainer
    Friend WithEvents pb_Cust_Fanart As System.Windows.Forms.PictureBox
    Friend WithEvents pb_Cust_Poster As System.Windows.Forms.PictureBox
    Friend WithEvents pb_Cust_Banner As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lbl_EpDirector As System.Windows.Forms.Label
    Friend WithEvents lbl_EpPlot As System.Windows.Forms.Label
    Friend WithEvents lbl_EpCredits As System.Windows.Forms.Label
    Friend WithEvents lbl_EpAired As System.Windows.Forms.Label
    Friend WithEvents lbl_EpPath As System.Windows.Forms.Label
    Friend WithEvents lbl_EpFilename As System.Windows.Forms.Label
    Friend WithEvents lbl_EpDetails As System.Windows.Forms.Label
    Friend WithEvents tb_EpPlot As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpDetails As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpFilename As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpPath As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpAired As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpCredits As System.Windows.Forms.TextBox
    Friend WithEvents tb_EpDirector As System.Windows.Forms.TextBox

End Class
