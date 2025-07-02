<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMergePats
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMergePats))
        dgvDupPat = New DataGridView()
        Keep = New DataGridViewCheckBoxColumn()
        PatID = New DataGridViewTextBoxColumn()
        LName = New DataGridViewTextBoxColumn()
        First = New DataGridViewTextBoxColumn()
        Mname = New DataGridViewTextBoxColumn()
        DOB = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        PINS = New DataGridViewTextBoxColumn()
        Policy = New DataGridViewTextBoxColumn()
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnLoadNext = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        chkTransAddress = New CheckBox()
        chkTransInsurance = New CheckBox()
        chkAuto = New CheckBox()
        cmbKeep = New ComboBox()
        Label1 = New Label()
        txtCount = New TextBox()
        CType(dgvDupPat, ComponentModel.ISupportInitialize).BeginInit()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' dgvDupPat
        ' 
        dgvDupPat.AllowUserToAddRows = False
        dgvDupPat.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.SeaShell
        dgvDupPat.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvDupPat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDupPat.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDupPat.Columns.AddRange(New DataGridViewColumn() {Keep, PatID, LName, First, Mname, DOB, Sex, Address, PINS, Policy})
        dgvDupPat.Location = New Point(20, 81)
        dgvDupPat.Margin = New Padding(5, 6, 5, 6)
        dgvDupPat.Name = "dgvDupPat"
        dgvDupPat.RowHeadersVisible = False
        dgvDupPat.RowHeadersWidth = 62
        dgvDupPat.Size = New Size(1078, 444)
        dgvDupPat.TabIndex = 0
        ' 
        ' Keep
        ' 
        Keep.FillWeight = 36F
        Keep.HeaderText = "Keep"
        Keep.MinimumWidth = 8
        Keep.Name = "Keep"
        ' 
        ' PatID
        ' 
        PatID.FillWeight = 50F
        PatID.HeaderText = "ID"
        PatID.MinimumWidth = 8
        PatID.Name = "PatID"
        PatID.ReadOnly = True
        PatID.Resizable = DataGridViewTriState.True
        PatID.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' LName
        ' 
        LName.FillWeight = 70F
        LName.HeaderText = "Last"
        LName.MinimumWidth = 8
        LName.Name = "LName"
        LName.ReadOnly = True
        ' 
        ' First
        ' 
        First.FillWeight = 60F
        First.HeaderText = "First"
        First.MinimumWidth = 8
        First.Name = "First"
        First.ReadOnly = True
        First.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Mname
        ' 
        Mname.FillWeight = 24F
        Mname.HeaderText = "MI"
        Mname.MinimumWidth = 8
        Mname.Name = "Mname"
        Mname.ReadOnly = True
        ' 
        ' DOB
        ' 
        DOB.FillWeight = 70F
        DOB.HeaderText = "DOB"
        DOB.MinimumWidth = 8
        DOB.Name = "DOB"
        DOB.ReadOnly = True
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 30F
        Sex.HeaderText = "Sex"
        Sex.MinimumWidth = 8
        Sex.Name = "Sex"
        Sex.ReadOnly = True
        ' 
        ' Address
        ' 
        Address.FillWeight = 110F
        Address.HeaderText = "Address"
        Address.MinimumWidth = 8
        Address.Name = "Address"
        Address.ReadOnly = True
        Address.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' PINS
        ' 
        PINS.HeaderText = "Prime Ins"
        PINS.MinimumWidth = 8
        PINS.Name = "PINS"
        PINS.ReadOnly = True
        ' 
        ' Policy
        ' 
        Policy.FillWeight = 80F
        Policy.HeaderText = "Policy"
        Policy.MinimumWidth = 8
        Policy.Name = "Policy"
        Policy.ReadOnly = True
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnLoadNext, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1118, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(100, 29)
        btnProcess.Text = "Process"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnLoadNext
        ' 
        btnLoadNext.Image = CType(resources.GetObject("btnLoadNext.Image"), Image)
        btnLoadNext.ImageTransparentColor = Color.Magenta
        btnLoadNext.Name = "btnLoadNext"
        btnLoadNext.Size = New Size(120, 29)
        btnLoadNext.Text = "Load Next"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
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
        ' chkTransAddress
        ' 
        chkTransAddress.AutoSize = True
        chkTransAddress.Location = New Point(655, 565)
        chkTransAddress.Margin = New Padding(5, 6, 5, 6)
        chkTransAddress.Name = "chkTransAddress"
        chkTransAddress.Size = New Size(169, 29)
        chkTransAddress.TabIndex = 28
        chkTransAddress.Text = "Transfer Address"
        chkTransAddress.UseVisualStyleBackColor = True
        ' 
        ' chkTransInsurance
        ' 
        chkTransInsurance.AutoSize = True
        chkTransInsurance.Location = New Point(872, 565)
        chkTransInsurance.Margin = New Padding(5, 6, 5, 6)
        chkTransInsurance.Name = "chkTransInsurance"
        chkTransInsurance.Size = New Size(216, 29)
        chkTransInsurance.TabIndex = 29
        chkTransInsurance.Text = "Transfer Insurance Info"
        chkTransInsurance.UseVisualStyleBackColor = True
        ' 
        ' chkAuto
        ' 
        chkAuto.Enabled = False
        chkAuto.Location = New Point(243, 565)
        chkAuto.Margin = New Padding(5, 6, 5, 6)
        chkAuto.Name = "chkAuto"
        chkAuto.Size = New Size(130, 33)
        chkAuto.TabIndex = 30
        chkAuto.Text = "Auto mode"
        chkAuto.UseVisualStyleBackColor = True
        ' 
        ' cmbKeep
        ' 
        cmbKeep.Enabled = False
        cmbKeep.FormattingEnabled = True
        cmbKeep.Items.AddRange(New Object() {"Keep Earlier", "Keep Later"})
        cmbKeep.Location = New Point(430, 562)
        cmbKeep.Margin = New Padding(5, 6, 5, 6)
        cmbKeep.Name = "cmbKeep"
        cmbKeep.Size = New Size(161, 33)
        cmbKeep.TabIndex = 31
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(20, 567)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(64, 25)
        Label1.TabIndex = 32
        Label1.Text = "Count:"
        ' 
        ' txtCount
        ' 
        txtCount.Location = New Point(93, 562)
        txtCount.Margin = New Padding(5, 6, 5, 6)
        txtCount.Name = "txtCount"
        txtCount.ReadOnly = True
        txtCount.Size = New Size(94, 31)
        txtCount.TabIndex = 33
        txtCount.TextAlign = HorizontalAlignment.Center
        ' 
        ' frmMergePats
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1118, 652)
        Controls.Add(txtCount)
        Controls.Add(Label1)
        Controls.Add(cmbKeep)
        Controls.Add(chkAuto)
        Controls.Add(chkTransInsurance)
        Controls.Add(chkTransAddress)
        Controls.Add(ToolStrip1)
        Controls.Add(dgvDupPat)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmMergePats"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Remove Duplicate Patients"
        CType(dgvDupPat, ComponentModel.ISupportInitialize).EndInit()
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents dgvDupPat As System.Windows.Forms.DataGridView
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnLoadNext As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkTransAddress As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransInsurance As System.Windows.Forms.CheckBox
    Friend WithEvents Keep As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PatID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents First As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Mname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Address As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PINS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Policy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkAuto As System.Windows.Forms.CheckBox
    Friend WithEvents cmbKeep As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCount As System.Windows.Forms.TextBox

End Class
