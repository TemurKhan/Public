<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDepts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDepts))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.chkEditNew = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnDelete = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.dgvDepts = New System.Windows.Forms.DataGridView
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DeptName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Note = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtDeptID = New System.Windows.Forms.TextBox
        Me.txtDeptName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNote = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TC = New System.Windows.Forms.TabControl
        Me.General = New System.Windows.Forms.TabPage
        Me.analytes = New System.Windows.Forms.TabPage
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtTestName = New System.Windows.Forms.TextBox
        Me.btnTestLook = New System.Windows.Forms.Button
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtTestID = New System.Windows.Forms.TextBox
        Me.btnRemTst = New System.Windows.Forms.Button
        Me.btnRemTstAll = New System.Windows.Forms.Button
        Me.btnAddTest = New System.Windows.Forms.Button
        Me.dgvTests = New System.Windows.Forms.DataGridView
        Me.NR_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Logo = New System.Windows.Forms.DataGridViewImageColumn
        Me.NRTo = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvDepts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TC.SuspendLayout()
        Me.General.SuspendLayout()
        Me.analytes.SuspendLayout()
        CType(Me.dgvTests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.chkEditNew, Me.ToolStripSeparator1, Me.btnSave, Me.ToolStripSeparator2, Me.btnDelete, Me.ToolStripSeparator3, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(668, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'chkEditNew
        '
        Me.chkEditNew.CheckOnClick = True
        Me.chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), System.Drawing.Image)
        Me.chkEditNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkEditNew.Name = "chkEditNew"
        Me.chkEditNew.Size = New System.Drawing.Size(47, 22)
        Me.chkEditNew.Text = "Edit"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "Help"
        '
        'dgvDepts
        '
        Me.dgvDepts.AllowUserToAddRows = False
        Me.dgvDepts.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvDepts.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDepts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDepts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.DeptName, Me.Note})
        Me.dgvDepts.Location = New System.Drawing.Point(12, 6)
        Me.dgvDepts.Name = "dgvDepts"
        Me.dgvDepts.ReadOnly = True
        Me.dgvDepts.RowHeadersVisible = False
        Me.dgvDepts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDepts.Size = New System.Drawing.Size(607, 143)
        Me.dgvDepts.TabIndex = 1
        '
        'ID
        '
        Me.ID.FillWeight = 60.0!
        Me.ID.HeaderText = "ID"
        Me.ID.MaxInputLength = 2
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        Me.ID.Width = 60
        '
        'DeptName
        '
        Me.DeptName.FillWeight = 220.0!
        Me.DeptName.HeaderText = "Dept Name"
        Me.DeptName.MaxInputLength = 60
        Me.DeptName.Name = "DeptName"
        Me.DeptName.ReadOnly = True
        Me.DeptName.Width = 220
        '
        'Note
        '
        Me.Note.FillWeight = 324.0!
        Me.Note.HeaderText = "Note"
        Me.Note.Name = "Note"
        Me.Note.ReadOnly = True
        Me.Note.Width = 324
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(16, 152)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDeptID
        '
        Me.txtDeptID.Location = New System.Drawing.Point(11, 174)
        Me.txtDeptID.MaxLength = 5
        Me.txtDeptID.Name = "txtDeptID"
        Me.txtDeptID.Size = New System.Drawing.Size(66, 20)
        Me.txtDeptID.TabIndex = 3
        Me.txtDeptID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDeptName
        '
        Me.txtDeptName.Location = New System.Drawing.Point(83, 174)
        Me.txtDeptName.MaxLength = 60
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.Size = New System.Drawing.Size(536, 20)
        Me.txtDeptName.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(88, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(204, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Department Name"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(12, 219)
        Me.txtNote.MaxLength = 4000
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNote.Size = New System.Drawing.Size(607, 160)
        Me.txtNote.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(9, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 19)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Note"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TC
        '
        Me.TC.Controls.Add(Me.General)
        Me.TC.Controls.Add(Me.analytes)
        Me.TC.Location = New System.Drawing.Point(13, 37)
        Me.TC.Name = "TC"
        Me.TC.SelectedIndex = 0
        Me.TC.Size = New System.Drawing.Size(643, 411)
        Me.TC.TabIndex = 8
        '
        'General
        '
        Me.General.Controls.Add(Me.dgvDepts)
        Me.General.Controls.Add(Me.txtNote)
        Me.General.Controls.Add(Me.Label3)
        Me.General.Controls.Add(Me.txtDeptName)
        Me.General.Controls.Add(Me.Label1)
        Me.General.Controls.Add(Me.txtDeptID)
        Me.General.Controls.Add(Me.Label2)
        Me.General.Location = New System.Drawing.Point(4, 22)
        Me.General.Name = "General"
        Me.General.Padding = New System.Windows.Forms.Padding(3)
        Me.General.Size = New System.Drawing.Size(635, 385)
        Me.General.TabIndex = 0
        Me.General.Text = "General"
        Me.General.UseVisualStyleBackColor = True
        '
        'analytes
        '
        Me.analytes.Controls.Add(Me.Label9)
        Me.analytes.Controls.Add(Me.txtTestName)
        Me.analytes.Controls.Add(Me.btnTestLook)
        Me.analytes.Controls.Add(Me.Label7)
        Me.analytes.Controls.Add(Me.txtTestID)
        Me.analytes.Controls.Add(Me.btnRemTst)
        Me.analytes.Controls.Add(Me.btnRemTstAll)
        Me.analytes.Controls.Add(Me.btnAddTest)
        Me.analytes.Controls.Add(Me.dgvTests)
        Me.analytes.Location = New System.Drawing.Point(4, 22)
        Me.analytes.Name = "analytes"
        Me.analytes.Padding = New System.Windows.Forms.Padding(3)
        Me.analytes.Size = New System.Drawing.Size(635, 385)
        Me.analytes.TabIndex = 1
        Me.analytes.Text = "Analytes"
        Me.analytes.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(163, 316)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 14)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Analyte Name"
        '
        'txtTestName
        '
        Me.txtTestName.Location = New System.Drawing.Point(166, 336)
        Me.txtTestName.MaxLength = 60
        Me.txtTestName.Name = "txtTestName"
        Me.txtTestName.ReadOnly = True
        Me.txtTestName.Size = New System.Drawing.Size(353, 20)
        Me.txtTestName.TabIndex = 15
        '
        'btnTestLook
        '
        Me.btnTestLook.Image = CType(resources.GetObject("btnTestLook.Image"), System.Drawing.Image)
        Me.btnTestLook.Location = New System.Drawing.Point(118, 332)
        Me.btnTestLook.Name = "btnTestLook"
        Me.btnTestLook.Size = New System.Drawing.Size(30, 26)
        Me.btnTestLook.TabIndex = 14
        Me.btnTestLook.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(18, 316)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 14)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Analyte ID"
        '
        'txtTestID
        '
        Me.txtTestID.Location = New System.Drawing.Point(18, 336)
        Me.txtTestID.MaxLength = 12
        Me.txtTestID.Name = "txtTestID"
        Me.txtTestID.Size = New System.Drawing.Size(85, 20)
        Me.txtTestID.TabIndex = 12
        Me.txtTestID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnRemTst
        '
        Me.btnRemTst.Enabled = False
        Me.btnRemTst.ForeColor = System.Drawing.Color.Red
        Me.btnRemTst.Location = New System.Drawing.Point(534, 17)
        Me.btnRemTst.Name = "btnRemTst"
        Me.btnRemTst.Size = New System.Drawing.Size(79, 23)
        Me.btnRemTst.TabIndex = 9
        Me.btnRemTst.Text = "Remove"
        Me.btnRemTst.UseVisualStyleBackColor = True
        '
        'btnRemTstAll
        '
        Me.btnRemTstAll.Enabled = False
        Me.btnRemTstAll.ForeColor = System.Drawing.Color.Red
        Me.btnRemTstAll.Location = New System.Drawing.Point(534, 275)
        Me.btnRemTstAll.Name = "btnRemTstAll"
        Me.btnRemTstAll.Size = New System.Drawing.Size(79, 23)
        Me.btnRemTstAll.TabIndex = 10
        Me.btnRemTstAll.Text = "Remove All"
        Me.btnRemTstAll.UseVisualStyleBackColor = True
        '
        'btnAddTest
        '
        Me.btnAddTest.Enabled = False
        Me.btnAddTest.ForeColor = System.Drawing.Color.DarkGreen
        Me.btnAddTest.Location = New System.Drawing.Point(534, 334)
        Me.btnAddTest.Name = "btnAddTest"
        Me.btnAddTest.Size = New System.Drawing.Size(79, 23)
        Me.btnAddTest.TabIndex = 11
        Me.btnAddTest.Text = "Add to List"
        Me.btnAddTest.UseVisualStyleBackColor = True
        '
        'dgvTests
        '
        Me.dgvTests.AllowUserToAddRows = False
        Me.dgvTests.AllowUserToDeleteRows = False
        Me.dgvTests.AllowUserToResizeRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Azure
        Me.dgvTests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTests.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NR_ID, Me.Logo, Me.NRTo})
        Me.dgvTests.Location = New System.Drawing.Point(18, 17)
        Me.dgvTests.Name = "dgvTests"
        Me.dgvTests.ReadOnly = True
        Me.dgvTests.RowHeadersVisible = False
        Me.dgvTests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTests.Size = New System.Drawing.Size(501, 281)
        Me.dgvTests.TabIndex = 1
        '
        'NR_ID
        '
        Me.NR_ID.HeaderText = "Analyte ID"
        Me.NR_ID.MaxInputLength = 12
        Me.NR_ID.Name = "NR_ID"
        Me.NR_ID.ReadOnly = True
        '
        'Logo
        '
        Me.Logo.FillWeight = 30.0!
        Me.Logo.HeaderText = ""
        Me.Logo.Name = "Logo"
        Me.Logo.ReadOnly = True
        Me.Logo.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Logo.Width = 30
        '
        'NRTo
        '
        Me.NRTo.FillWeight = 368.0!
        Me.NRTo.HeaderText = "Analyte Name"
        Me.NRTo.MaxInputLength = 60
        Me.NRTo.Name = "NRTo"
        Me.NRTo.ReadOnly = True
        Me.NRTo.Width = 368
        '
        'frmDepts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(668, 460)
        Me.Controls.Add(Me.TC)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDepts"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Departments"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvDepts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TC.ResumeLayout(False)
        Me.General.ResumeLayout(False)
        Me.General.PerformLayout()
        Me.analytes.ResumeLayout(False)
        Me.analytes.PerformLayout()
        CType(Me.dgvTests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
    Friend WithEvents dgvDepts As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDeptID As System.Windows.Forms.TextBox
    Friend WithEvents txtDeptName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TC As System.Windows.Forms.TabControl
    Friend WithEvents General As System.Windows.Forms.TabPage
    Friend WithEvents analytes As System.Windows.Forms.TabPage
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeptName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Note As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvTests As System.Windows.Forms.DataGridView
    Friend WithEvents NR_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents NRTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTestName As System.Windows.Forms.TextBox
    Friend WithEvents btnTestLook As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTestID As System.Windows.Forms.TextBox
    Friend WithEvents btnRemTst As System.Windows.Forms.Button
    Friend WithEvents btnRemTstAll As System.Windows.Forms.Button
    Friend WithEvents btnAddTest As System.Windows.Forms.Button

End Class
