<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatches
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBatches))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        cmbAnas = New ComboBox()
        Label1 = New Label()
        dtpDate = New DateTimePicker()
        txtTime = New TextBox()
        lblDate = New Label()
        Label3 = New Label()
        Label5 = New Label()
        Label6 = New Label()
        cmbTech = New ComboBox()
        dgvAccCtl = New DataGridView()
        SNo = New DataGridViewTextBoxColumn()
        Acc_Ctl = New DataGridViewTextBoxColumn()
        Sample = New DataGridViewTextBoxColumn()
        Ctl = New DataGridViewCheckBoxColumn()
        Replica = New DataGridViewCheckBoxColumn()
        Exclude = New DataGridViewCheckBoxColumn()
        Lot = New DataGridViewTextBoxColumn()
        Expire = New DataGridViewTextBoxColumn()
        Label7 = New Label()
        cmbRunName = New ComboBox()
        Label8 = New Label()
        txtAccID = New TextBox()
        txtSampleID = New TextBox()
        Label9 = New Label()
        chkCtl = New CheckBox()
        Label10 = New Label()
        Label11 = New Label()
        chkRep = New CheckBox()
        Label12 = New Label()
        chkExclude = New CheckBox()
        btnAddtoList = New Button()
        chkDeleteOrphan = New CheckBox()
        TextBox1 = New TextBox()
        Label4 = New Label()
        Label13 = New Label()
        DateTimePicker1 = New DateTimePicker()
        ToolStrip1.SuspendLayout()
        CType(dgvAccCtl, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1045, 34)
        ToolStrip1.TabIndex = 2
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.AutoSize = False
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(60, 22)
        chkEditNew.Text = "New"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' cmbAnas
        ' 
        cmbAnas.DropDownStyle = ComboBoxStyle.DropDownList
        cmbAnas.FormattingEnabled = True
        cmbAnas.Location = New Point(20, 108)
        cmbAnas.Margin = New Padding(5, 6, 5, 6)
        cmbAnas.Name = "cmbAnas"
        cmbAnas.Size = New Size(622, 33)
        cmbAnas.TabIndex = 3
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(20, 71)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(172, 31)
        Label1.TabIndex = 4
        Label1.Text = "Analysis"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(655, 108)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(217, 31)
        dtpDate.TabIndex = 5
        ' 
        ' txtTime
        ' 
        txtTime.Location = New Point(885, 108)
        txtTime.Margin = New Padding(5, 6, 5, 6)
        txtTime.MaxLength = 8
        txtTime.Name = "txtTime"
        txtTime.Size = New Size(131, 31)
        txtTime.TabIndex = 6
        txtTime.TextAlign = HorizontalAlignment.Center
        ' 
        ' lblDate
        ' 
        lblDate.ForeColor = Color.DarkBlue
        lblDate.Location = New Point(655, 71)
        lblDate.Margin = New Padding(5, 0, 5, 0)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(220, 31)
        lblDate.TabIndex = 7
        lblDate.Text = "Dated"
        lblDate.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(885, 71)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 31)
        Label3.TabIndex = 8
        Label3.Text = "Run Time"
        Label3.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(20, 160)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(177, 31)
        Label5.TabIndex = 12
        Label5.Text = "Run Name"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.ForeColor = Color.DarkBlue
        Label6.Location = New Point(703, 160)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(172, 31)
        Label6.TabIndex = 14
        Label6.Text = "Tech Name"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbTech
        ' 
        cmbTech.DropDownStyle = ComboBoxStyle.DropDownList
        cmbTech.Enabled = False
        cmbTech.FormattingEnabled = True
        cmbTech.Location = New Point(703, 196)
        cmbTech.Margin = New Padding(5, 6, 5, 6)
        cmbTech.Name = "cmbTech"
        cmbTech.Size = New Size(312, 33)
        cmbTech.TabIndex = 13
        ' 
        ' dgvAccCtl
        ' 
        dgvAccCtl.AllowUserToAddRows = False
        dgvAccCtl.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(192), CByte(255))
        dgvAccCtl.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvAccCtl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvAccCtl.Columns.AddRange(New DataGridViewColumn() {SNo, Acc_Ctl, Sample, Ctl, Replica, Exclude, Lot, Expire})
        dgvAccCtl.Location = New Point(20, 296)
        dgvAccCtl.Margin = New Padding(5, 6, 5, 6)
        dgvAccCtl.Name = "dgvAccCtl"
        dgvAccCtl.RowHeadersVisible = False
        dgvAccCtl.RowHeadersWidth = 62
        dgvAccCtl.Size = New Size(998, 442)
        dgvAccCtl.TabIndex = 15
        ' 
        ' SNo
        ' 
        SNo.FillWeight = 40F
        SNo.HeaderText = "No"
        SNo.MaxInputLength = 3
        SNo.MinimumWidth = 8
        SNo.Name = "SNo"
        SNo.ReadOnly = True
        SNo.SortMode = DataGridViewColumnSortMode.NotSortable
        SNo.Width = 67
        ' 
        ' Acc_Ctl
        ' 
        Acc_Ctl.HeaderText = "Accession/Control"
        Acc_Ctl.MaxInputLength = 12
        Acc_Ctl.MinimumWidth = 8
        Acc_Ctl.Name = "Acc_Ctl"
        Acc_Ctl.ReadOnly = True
        Acc_Ctl.Width = 169
        ' 
        ' Sample
        ' 
        Sample.FillWeight = 90F
        Sample.HeaderText = "Sample ID"
        Sample.MinimumWidth = 8
        Sample.Name = "Sample"
        Sample.ReadOnly = True
        Sample.Width = 152
        ' 
        ' Ctl
        ' 
        Ctl.FillWeight = 40F
        Ctl.HeaderText = "Ctl?"
        Ctl.MinimumWidth = 8
        Ctl.Name = "Ctl"
        Ctl.Width = 67
        ' 
        ' Replica
        ' 
        Replica.FillWeight = 40F
        Replica.HeaderText = "Rep?"
        Replica.MinimumWidth = 8
        Replica.Name = "Replica"
        Replica.Resizable = DataGridViewTriState.True
        Replica.Width = 68
        ' 
        ' Exclude
        ' 
        Exclude.FillWeight = 40F
        Exclude.HeaderText = "Exc?"
        Exclude.MinimumWidth = 8
        Exclude.Name = "Exclude"
        Exclude.Resizable = DataGridViewTriState.True
        Exclude.Width = 67
        ' 
        ' Lot
        ' 
        Lot.FillWeight = 120F
        Lot.HeaderText = "Lot"
        Lot.MaxInputLength = 35
        Lot.MinimumWidth = 8
        Lot.Name = "Lot"
        Lot.SortMode = DataGridViewColumnSortMode.NotSortable
        Lot.Width = 203
        ' 
        ' Expire
        ' 
        Expire.FillWeight = 120F
        Expire.HeaderText = "Expire Date"
        Expire.MaxInputLength = 10
        Expire.MinimumWidth = 8
        Expire.Name = "Expire"
        Expire.SortMode = DataGridViewColumnSortMode.NotSortable
        Expire.Width = 202
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(15, 250)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(328, 31)
        Label7.TabIndex = 17
        Label7.Text = "Accessioned samples and Controls"
        Label7.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' cmbRunName
        ' 
        cmbRunName.FormattingEnabled = True
        cmbRunName.Location = New Point(20, 196)
        cmbRunName.Margin = New Padding(5, 6, 5, 6)
        cmbRunName.Name = "cmbRunName"
        cmbRunName.Size = New Size(622, 33)
        cmbRunName.TabIndex = 18
        ' 
        ' Label8
        ' 
        Label8.ForeColor = Color.DarkBlue
        Label8.Location = New Point(20, 767)
        Label8.Margin = New Padding(5, 0, 5, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(128, 33)
        Label8.TabIndex = 19
        Label8.Text = "Rerun Acc/Ctl ID"
        Label8.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(20, 806)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.MaxLength = 9
        txtAccID.Name = "txtAccID"
        txtAccID.Size = New Size(126, 31)
        txtAccID.TabIndex = 20
        txtAccID.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtSampleID
        ' 
        txtSampleID.Location = New Point(158, 806)
        txtSampleID.Margin = New Padding(5, 6, 5, 6)
        txtSampleID.MaxLength = 20
        txtSampleID.Name = "txtSampleID"
        txtSampleID.Size = New Size(147, 31)
        txtSampleID.TabIndex = 22
        txtSampleID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(158, 767)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(150, 33)
        Label9.TabIndex = 21
        Label9.Text = "Rerun Sample ID"
        Label9.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkCtl
        ' 
        chkCtl.AutoSize = True
        chkCtl.Location = New Point(337, 812)
        chkCtl.Margin = New Padding(5, 6, 5, 6)
        chkCtl.Name = "chkCtl"
        chkCtl.Size = New Size(22, 21)
        chkCtl.TabIndex = 23
        chkCtl.UseVisualStyleBackColor = True
        ' 
        ' Label10
        ' 
        Label10.ForeColor = Color.DarkBlue
        Label10.Location = New Point(332, 767)
        Label10.Margin = New Padding(5, 0, 5, 0)
        Label10.Name = "Label10"
        Label10.Size = New Size(47, 33)
        Label10.TabIndex = 24
        Label10.Text = "Ctl?"
        Label10.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label11
        ' 
        Label11.ForeColor = Color.DarkBlue
        Label11.Location = New Point(388, 767)
        Label11.Margin = New Padding(5, 0, 5, 0)
        Label11.Name = "Label11"
        Label11.Size = New Size(60, 33)
        Label11.TabIndex = 26
        Label11.Text = "Rep?"
        Label11.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkRep
        ' 
        chkRep.AutoSize = True
        chkRep.Location = New Point(403, 812)
        chkRep.Margin = New Padding(5, 6, 5, 6)
        chkRep.Name = "chkRep"
        chkRep.Size = New Size(22, 21)
        chkRep.TabIndex = 25
        chkRep.UseVisualStyleBackColor = True
        ' 
        ' Label12
        ' 
        Label12.ForeColor = Color.DarkBlue
        Label12.Location = New Point(455, 767)
        Label12.Margin = New Padding(5, 0, 5, 0)
        Label12.Name = "Label12"
        Label12.Size = New Size(58, 33)
        Label12.TabIndex = 28
        Label12.Text = "Exc?"
        Label12.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' chkExclude
        ' 
        chkExclude.AutoSize = True
        chkExclude.Location = New Point(460, 812)
        chkExclude.Margin = New Padding(5, 6, 5, 6)
        chkExclude.Name = "chkExclude"
        chkExclude.Size = New Size(22, 21)
        chkExclude.TabIndex = 27
        chkExclude.UseVisualStyleBackColor = True
        ' 
        ' btnAddtoList
        ' 
        btnAddtoList.Enabled = False
        btnAddtoList.ForeColor = Color.DarkBlue
        btnAddtoList.Location = New Point(885, 796)
        btnAddtoList.Margin = New Padding(5, 6, 5, 6)
        btnAddtoList.Name = "btnAddtoList"
        btnAddtoList.Size = New Size(133, 56)
        btnAddtoList.TabIndex = 29
        btnAddtoList.Text = "Add to List"
        btnAddtoList.UseVisualStyleBackColor = True
        ' 
        ' chkDeleteOrphan
        ' 
        chkDeleteOrphan.CheckAlign = ContentAlignment.MiddleRight
        chkDeleteOrphan.Checked = True
        chkDeleteOrphan.CheckState = CheckState.Checked
        chkDeleteOrphan.Location = New Point(755, 252)
        chkDeleteOrphan.Margin = New Padding(5, 6, 5, 6)
        chkDeleteOrphan.Name = "chkDeleteOrphan"
        chkDeleteOrphan.Size = New Size(263, 33)
        chkDeleteOrphan.TabIndex = 31
        chkDeleteOrphan.Text = "Delete Orphan Batches"
        chkDeleteOrphan.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(517, 806)
        TextBox1.Margin = New Padding(5, 6, 5, 6)
        TextBox1.MaxLength = 35
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(174, 31)
        TextBox1.TabIndex = 32
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(523, 767)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(140, 33)
        Label4.TabIndex = 33
        Label4.Text = "Lot"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label13
        ' 
        Label13.ForeColor = Color.DarkBlue
        Label13.Location = New Point(705, 767)
        Label13.Margin = New Padding(5, 0, 5, 0)
        Label13.Name = "Label13"
        Label13.Size = New Size(170, 33)
        Label13.TabIndex = 34
        Label13.Text = "Expire Date"
        Label13.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' DateTimePicker1
        ' 
        DateTimePicker1.Format = DateTimePickerFormat.Short
        DateTimePicker1.Location = New Point(703, 804)
        DateTimePicker1.Margin = New Padding(5, 6, 5, 6)
        DateTimePicker1.Name = "DateTimePicker1"
        DateTimePicker1.Size = New Size(169, 31)
        DateTimePicker1.TabIndex = 35
        ' 
        ' frmBatches
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1045, 887)
        Controls.Add(DateTimePicker1)
        Controls.Add(Label13)
        Controls.Add(Label4)
        Controls.Add(TextBox1)
        Controls.Add(chkDeleteOrphan)
        Controls.Add(btnAddtoList)
        Controls.Add(Label12)
        Controls.Add(chkExclude)
        Controls.Add(Label11)
        Controls.Add(chkRep)
        Controls.Add(Label10)
        Controls.Add(chkCtl)
        Controls.Add(txtSampleID)
        Controls.Add(Label9)
        Controls.Add(txtAccID)
        Controls.Add(Label8)
        Controls.Add(cmbRunName)
        Controls.Add(Label7)
        Controls.Add(dgvAccCtl)
        Controls.Add(Label6)
        Controls.Add(cmbTech)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(lblDate)
        Controls.Add(txtTime)
        Controls.Add(dtpDate)
        Controls.Add(Label1)
        Controls.Add(cmbAnas)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmBatches"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Batch Samples"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvAccCtl, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbAnas As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTech As System.Windows.Forms.ComboBox
    Friend WithEvents dgvAccCtl As System.Windows.Forms.DataGridView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbRunName As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents txtSampleID As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkCtl As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkRep As System.Windows.Forms.CheckBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents chkExclude As System.Windows.Forms.CheckBox
    Friend WithEvents btnAddtoList As System.Windows.Forms.Button
    Friend WithEvents chkDeleteOrphan As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents SNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Acc_Ctl As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sample As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Ctl As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Replica As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Exclude As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Lot As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Expire As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker

End Class
