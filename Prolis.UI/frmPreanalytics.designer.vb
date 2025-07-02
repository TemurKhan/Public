<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreanalytics
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreanalytics))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        btnFirst = New Button()
        btnPrevious = New Button()
        btnNext = New Button()
        btnLast = New Button()
        txtNavStatus = New TextBox()
        btnLoad = New Button()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        Label103 = New Label()
        Label104 = New Label()
        Label23 = New Label()
        Label22 = New Label()
        txtProvider = New TextBox()
        txtPatient = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        dgvInfos = New DataGridView()
        InfoID = New DataGridViewTextBoxColumn()
        InfoName = New DataGridViewTextBoxColumn()
        Response = New DataGridViewTextBoxColumn()
        InfoFormat = New DataGridViewTextBoxColumn()
        Required = New DataGridViewCheckBoxColumn()
        RowType = New DataGridViewTextBoxColumn()
        TGPID = New DataGridViewTextBoxColumn()
        AccID = New DataGridViewTextBoxColumn()
        dtpDateTo = New DateTimePicker()
        lblClearDates = New Label()
        dtpDateFrom = New DateTimePicker()
        CType(dgvInfos, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnFirst
        ' 
        btnFirst.AutoSize = True
        btnFirst.Enabled = False
        btnFirst.Image = CType(resources.GetObject("btnFirst.Image"), Image)
        btnFirst.Location = New Point(750, 73)
        btnFirst.Margin = New Padding(5, 6, 5, 6)
        btnFirst.Name = "btnFirst"
        btnFirst.Size = New Size(50, 50)
        btnFirst.TabIndex = 23
        btnFirst.TabStop = False
        btnFirst.UseVisualStyleBackColor = True
        ' 
        ' btnPrevious
        ' 
        btnPrevious.Enabled = False
        btnPrevious.Image = CType(resources.GetObject("btnPrevious.Image"), Image)
        btnPrevious.Location = New Point(810, 75)
        btnPrevious.Margin = New Padding(5, 6, 5, 6)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New Size(48, 50)
        btnPrevious.TabIndex = 21
        btnPrevious.TabStop = False
        btnPrevious.UseVisualStyleBackColor = True
        ' 
        ' btnNext
        ' 
        btnNext.Enabled = False
        btnNext.Image = CType(resources.GetObject("btnNext.Image"), Image)
        btnNext.Location = New Point(1260, 73)
        btnNext.Margin = New Padding(5, 6, 5, 6)
        btnNext.Name = "btnNext"
        btnNext.Size = New Size(53, 50)
        btnNext.TabIndex = 18
        btnNext.TabStop = False
        btnNext.UseVisualStyleBackColor = True
        ' 
        ' btnLast
        ' 
        btnLast.Enabled = False
        btnLast.Image = CType(resources.GetObject("btnLast.Image"), Image)
        btnLast.Location = New Point(1323, 75)
        btnLast.Margin = New Padding(5, 6, 5, 6)
        btnLast.Name = "btnLast"
        btnLast.Size = New Size(55, 50)
        btnLast.TabIndex = 19
        btnLast.TabStop = False
        btnLast.UseVisualStyleBackColor = True
        ' 
        ' txtNavStatus
        ' 
        txtNavStatus.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtNavStatus.Location = New Point(868, 81)
        txtNavStatus.Margin = New Padding(5, 6, 5, 6)
        txtNavStatus.Name = "txtNavStatus"
        txtNavStatus.ReadOnly = True
        txtNavStatus.Size = New Size(379, 26)
        txtNavStatus.TabIndex = 24
        txtNavStatus.TabStop = False
        txtNavStatus.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(673, 73)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(50, 50)
        btnLoad.TabIndex = 17
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(510, 81)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 12
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(132, 31)
        txtAccTo.TabIndex = 15
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(365, 81)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 12
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(132, 31)
        txtAccFrom.TabIndex = 13
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label103
        ' 
        Label103.ForeColor = Color.Magenta
        Label103.Location = New Point(510, 50)
        Label103.Margin = New Padding(5, 0, 5, 0)
        Label103.Name = "Label103"
        Label103.Size = New Size(108, 25)
        Label103.TabIndex = 22
        Label103.Text = "Acc To"
        ' 
        ' Label104
        ' 
        Label104.ForeColor = Color.Red
        Label104.Location = New Point(365, 50)
        Label104.Margin = New Padding(5, 0, 5, 0)
        Label104.Name = "Label104"
        Label104.Size = New Size(135, 25)
        Label104.TabIndex = 20
        Label104.Text = "Acc From"
        Label104.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label23
        ' 
        Label23.ForeColor = Color.Magenta
        Label23.Location = New Point(183, 50)
        Label23.Margin = New Padding(5, 0, 5, 0)
        Label23.Name = "Label23"
        Label23.Size = New Size(110, 25)
        Label23.TabIndex = 16
        Label23.Text = "Date To"
        ' 
        ' Label22
        ' 
        Label22.ForeColor = Color.Red
        Label22.Location = New Point(20, 50)
        Label22.Margin = New Padding(5, 0, 5, 0)
        Label22.Name = "Label22"
        Label22.Size = New Size(122, 25)
        Label22.TabIndex = 14
        Label22.Text = "Date From"
        Label22.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtProvider
        ' 
        txtProvider.Location = New Point(25, 185)
        txtProvider.Margin = New Padding(5, 6, 5, 6)
        txtProvider.Name = "txtProvider"
        txtProvider.ReadOnly = True
        txtProvider.Size = New Size(696, 31)
        txtProvider.TabIndex = 25
        ' 
        ' txtPatient
        ' 
        txtPatient.Location = New Point(750, 185)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Name = "txtPatient"
        txtPatient.ReadOnly = True
        txtPatient.Size = New Size(619, 31)
        txtPatient.TabIndex = 26
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(20, 154)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(197, 25)
        Label1.TabIndex = 27
        Label1.Text = "Ordering Provider"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(745, 154)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(197, 25)
        Label2.TabIndex = 28
        Label2.Text = "Patient"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dgvInfos
        ' 
        dgvInfos.AllowUserToAddRows = False
        dgvInfos.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.MintCream
        dgvInfos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvInfos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvInfos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvInfos.Columns.AddRange(New DataGridViewColumn() {InfoID, InfoName, Response, InfoFormat, Required, RowType, TGPID, AccID})
        dgvInfos.Location = New Point(25, 269)
        dgvInfos.Margin = New Padding(5, 6, 5, 6)
        dgvInfos.Name = "dgvInfos"
        dgvInfos.RowHeadersVisible = False
        dgvInfos.RowHeadersWidth = 56
        dgvInfos.Size = New Size(1347, 925)
        dgvInfos.TabIndex = 29
        ' 
        ' InfoID
        ' 
        InfoID.FillWeight = 90F
        InfoID.HeaderText = "InfoID"
        InfoID.MinimumWidth = 7
        InfoID.Name = "InfoID"
        InfoID.Visible = False
        ' 
        ' InfoName
        ' 
        InfoName.FillWeight = 200F
        InfoName.HeaderText = "Info Name"
        InfoName.MinimumWidth = 7
        InfoName.Name = "InfoName"
        InfoName.ReadOnly = True
        ' 
        ' Response
        ' 
        Response.FillWeight = 400F
        Response.HeaderText = "Response"
        Response.MaxInputLength = 100
        Response.MinimumWidth = 7
        Response.Name = "Response"
        Response.Resizable = DataGridViewTriState.True
        Response.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' InfoFormat
        ' 
        InfoFormat.HeaderText = "Format"
        InfoFormat.MinimumWidth = 7
        InfoFormat.Name = "InfoFormat"
        InfoFormat.ReadOnly = True
        ' 
        ' Required
        ' 
        Required.FillWeight = 90F
        Required.HeaderText = "Required"
        Required.MinimumWidth = 7
        Required.Name = "Required"
        Required.ReadOnly = True
        ' 
        ' RowType
        ' 
        RowType.HeaderText = "RowType"
        RowType.MaxInputLength = 1
        RowType.MinimumWidth = 7
        RowType.Name = "RowType"
        RowType.ReadOnly = True
        RowType.Visible = False
        ' 
        ' TGPID
        ' 
        TGPID.HeaderText = "TGPID"
        TGPID.MinimumWidth = 7
        TGPID.Name = "TGPID"
        TGPID.ReadOnly = True
        TGPID.Visible = False
        ' 
        ' AccID
        ' 
        AccID.HeaderText = "AccID"
        AccID.MinimumWidth = 7
        AccID.Name = "AccID"
        AccID.ReadOnly = True
        AccID.Visible = False
        ' 
        ' dtpDateTo
        ' 
        dtpDateTo.CustomFormat = " "
        dtpDateTo.Format = DateTimePickerFormat.Custom
        dtpDateTo.Location = New Point(183, 81)
        dtpDateTo.Margin = New Padding(3, 4, 3, 4)
        dtpDateTo.Name = "dtpDateTo"
        dtpDateTo.Size = New Size(129, 31)
        dtpDateTo.TabIndex = 1
        ' 
        ' lblClearDates
        ' 
        lblClearDates.BackColor = SystemColors.Control
        lblClearDates.Image = My.Resources.Resources.Eraser
        lblClearDates.Location = New Point(322, 75)
        lblClearDates.Name = "lblClearDates"
        lblClearDates.Size = New Size(33, 46)
        lblClearDates.TabIndex = 93
        lblClearDates.Text = "      "
        ' 
        ' dtpDateFrom
        ' 
        dtpDateFrom.CustomFormat = " "
        dtpDateFrom.Format = DateTimePickerFormat.Custom
        dtpDateFrom.Location = New Point(23, 81)
        dtpDateFrom.Margin = New Padding(3, 4, 3, 4)
        dtpDateFrom.Name = "dtpDateFrom"
        dtpDateFrom.Size = New Size(129, 31)
        dtpDateFrom.TabIndex = 0
        ' 
        ' frmPreanalytics
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1418, 1242)
        Controls.Add(dtpDateTo)
        Controls.Add(lblClearDates)
        Controls.Add(dtpDateFrom)
        Controls.Add(dgvInfos)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(txtPatient)
        Controls.Add(txtProvider)
        Controls.Add(btnFirst)
        Controls.Add(btnPrevious)
        Controls.Add(btnNext)
        Controls.Add(btnLast)
        Controls.Add(txtNavStatus)
        Controls.Add(btnLoad)
        Controls.Add(txtAccTo)
        Controls.Add(txtAccFrom)
        Controls.Add(Label103)
        Controls.Add(Label104)
        Controls.Add(Label23)
        Controls.Add(Label22)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPreanalytics"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Pre Analytics"
        CType(dgvInfos, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents btnFirst As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnLast As System.Windows.Forms.Button
    Friend WithEvents txtNavStatus As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents Label103 As System.Windows.Forms.Label
    Friend WithEvents Label104 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dgvInfos As System.Windows.Forms.DataGridView
    Friend WithEvents InfoID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InfoName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Response As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents InfoFormat As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Required As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents RowType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dtpDateTo As DateTimePicker
    Friend WithEvents lblClearDates As Label
    Friend WithEvents dtpDateFrom As DateTimePicker
End Class
