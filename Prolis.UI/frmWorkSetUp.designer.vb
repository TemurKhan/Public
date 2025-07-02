<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmWorkSetUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmWorkSetUp))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        btnWorkLook = New Button()
        txtName = New TextBox()
        Label2 = New Label()
        txtWorkID = New TextBox()
        Label1 = New Label()
        dgvTGs = New DataGridView()
        NR_ID = New DataGridViewTextBoxColumn()
        Drill = New DataGridViewCheckBoxColumn()
        NRTo = New DataGridViewTextBoxColumn()
        Label9 = New Label()
        txtCompName = New TextBox()
        btnTGPLookup = New Button()
        Label7 = New Label()
        txtCompID = New TextBox()
        btnAdd = New Button()
        btnRemAll = New Button()
        btnRem = New Button()
        txtControls = New TextBox()
        Label3 = New Label()
        ToolStrip1.SuspendLayout()
        CType(dgvTGs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripButton2, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator2, btnCancel, ToolStripSeparator3, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(751, 34)
        ToolStrip1.TabIndex = 7
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.ForeColor = Color.DarkBlue
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(66, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.ForeColor = Color.DarkBlue
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(73, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Enabled = False
        btnDelete.ForeColor = Color.Red
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(86, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(87, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' btnWorkLook
        ' 
        btnWorkLook.Image = CType(resources.GetObject("btnWorkLook.Image"), Image)
        btnWorkLook.Location = New Point(144, 103)
        btnWorkLook.Margin = New Padding(5, 6, 5, 6)
        btnWorkLook.Name = "btnWorkLook"
        btnWorkLook.Size = New Size(44, 47)
        btnWorkLook.TabIndex = 106
        btnWorkLook.UseVisualStyleBackColor = True
        ' 
        ' txtName
        ' 
        txtName.Location = New Point(196, 111)
        txtName.Margin = New Padding(5, 6, 5, 6)
        txtName.MaxLength = 60
        txtName.Name = "txtName"
        txtName.Size = New Size(443, 31)
        txtName.TabIndex = 105
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Red
        Label2.Location = New Point(196, 75)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(224, 31)
        Label2.TabIndex = 104
        Label2.Text = "Worksheet Name"
        ' 
        ' txtWorkID
        ' 
        txtWorkID.Location = New Point(20, 111)
        txtWorkID.Margin = New Padding(5, 6, 5, 6)
        txtWorkID.MaxLength = 12
        txtWorkID.Name = "txtWorkID"
        txtWorkID.Size = New Size(110, 31)
        txtWorkID.TabIndex = 103
        txtWorkID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(24, 75)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(129, 31)
        Label1.TabIndex = 102
        Label1.Text = "Worksheet ID"
        ' 
        ' dgvTGs
        ' 
        dgvTGs.AllowUserToAddRows = False
        dgvTGs.AllowUserToDeleteRows = False
        dgvTGs.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = Color.Lavender
        dgvTGs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvTGs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvTGs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTGs.Columns.AddRange(New DataGridViewColumn() {NR_ID, Drill, NRTo})
        dgvTGs.Location = New Point(20, 164)
        dgvTGs.Margin = New Padding(5, 6, 5, 6)
        dgvTGs.Name = "dgvTGs"
        dgvTGs.RowHeadersVisible = False
        dgvTGs.RowHeadersWidth = 62
        dgvTGs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTGs.Size = New Size(711, 328)
        dgvTGs.TabIndex = 107
        ' 
        ' NR_ID
        ' 
        NR_ID.FillWeight = 80F
        NR_ID.HeaderText = "Comp ID"
        NR_ID.MaxInputLength = 5
        NR_ID.MinimumWidth = 8
        NR_ID.Name = "NR_ID"
        NR_ID.ReadOnly = True
        ' 
        ' Drill
        ' 
        Drill.FillWeight = 30F
        Drill.HeaderText = "Drill"
        Drill.MinimumWidth = 8
        Drill.Name = "Drill"
        Drill.Resizable = DataGridViewTriState.True
        ' 
        ' NRTo
        ' 
        NRTo.FillWeight = 314F
        NRTo.HeaderText = "Component Name"
        NRTo.MaxInputLength = 60
        NRTo.MinimumWidth = 8
        NRTo.Name = "NRTo"
        NRTo.ReadOnly = True
        ' 
        ' Label9
        ' 
        Label9.ForeColor = Color.DarkBlue
        Label9.Location = New Point(220, 527)
        Label9.Margin = New Padding(5, 0, 5, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(221, 27)
        Label9.TabIndex = 112
        Label9.Text = "Component Name"
        ' 
        ' txtCompName
        ' 
        txtCompName.Location = New Point(219, 561)
        txtCompName.Margin = New Padding(5, 6, 5, 6)
        txtCompName.MaxLength = 60
        txtCompName.Name = "txtCompName"
        txtCompName.ReadOnly = True
        txtCompName.Size = New Size(443, 31)
        txtCompName.TabIndex = 111
        ' 
        ' btnTGPLookup
        ' 
        btnTGPLookup.Image = CType(resources.GetObject("btnTGPLookup.Image"), Image)
        btnTGPLookup.Location = New Point(160, 553)
        btnTGPLookup.Margin = New Padding(5, 6, 5, 6)
        btnTGPLookup.Name = "btnTGPLookup"
        btnTGPLookup.Size = New Size(50, 50)
        btnTGPLookup.TabIndex = 110
        btnTGPLookup.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(21, 528)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(130, 27)
        Label7.TabIndex = 109
        Label7.Text = "Component ID"
        ' 
        ' txtCompID
        ' 
        txtCompID.Location = New Point(20, 561)
        txtCompID.Margin = New Padding(5, 6, 5, 6)
        txtCompID.MaxLength = 12
        txtCompID.Name = "txtCompID"
        txtCompID.Size = New Size(129, 31)
        txtCompID.TabIndex = 108
        txtCompID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnAdd
        ' 
        btnAdd.Image = CType(resources.GetObject("btnAdd.Image"), Image)
        btnAdd.Location = New Point(674, 553)
        btnAdd.Margin = New Padding(5, 6, 5, 6)
        btnAdd.Name = "btnAdd"
        btnAdd.Size = New Size(56, 50)
        btnAdd.TabIndex = 113
        btnAdd.UseVisualStyleBackColor = True
        ' 
        ' btnRemAll
        ' 
        btnRemAll.Image = CType(resources.GetObject("btnRemAll.Image"), Image)
        btnRemAll.Location = New Point(674, 503)
        btnRemAll.Margin = New Padding(5, 6, 5, 6)
        btnRemAll.Name = "btnRemAll"
        btnRemAll.Size = New Size(56, 50)
        btnRemAll.TabIndex = 114
        btnRemAll.UseVisualStyleBackColor = True
        ' 
        ' btnRem
        ' 
        btnRem.Image = CType(resources.GetObject("btnRem.Image"), Image)
        btnRem.Location = New Point(604, 503)
        btnRem.Margin = New Padding(5, 6, 5, 6)
        btnRem.Name = "btnRem"
        btnRem.Size = New Size(60, 50)
        btnRem.TabIndex = 115
        btnRem.UseVisualStyleBackColor = True
        ' 
        ' txtControls
        ' 
        txtControls.Location = New Point(651, 109)
        txtControls.Margin = New Padding(5, 6, 5, 6)
        txtControls.MaxLength = 12
        txtControls.Name = "txtControls"
        txtControls.Size = New Size(78, 31)
        txtControls.TabIndex = 116
        txtControls.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(654, 75)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(79, 27)
        Label3.TabIndex = 117
        Label3.Text = "Controls"
        ' 
        ' frmWorkSetUp
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(751, 633)
        Controls.Add(Label3)
        Controls.Add(txtControls)
        Controls.Add(btnRem)
        Controls.Add(btnRemAll)
        Controls.Add(btnAdd)
        Controls.Add(Label9)
        Controls.Add(txtCompName)
        Controls.Add(btnTGPLookup)
        Controls.Add(Label7)
        Controls.Add(txtCompID)
        Controls.Add(dgvTGs)
        Controls.Add(btnWorkLook)
        Controls.Add(txtName)
        Controls.Add(Label2)
        Controls.Add(txtWorkID)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmWorkSetUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Worksheet Set Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvTGs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnWorkLook As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWorkID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvTGs As System.Windows.Forms.DataGridView
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCompName As System.Windows.Forms.TextBox
    Friend WithEvents btnTGPLookup As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCompID As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemAll As System.Windows.Forms.Button
    Friend WithEvents btnRem As System.Windows.Forms.Button
    Friend WithEvents NR_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Drill As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents NRTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtControls As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
