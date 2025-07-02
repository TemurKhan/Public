<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReqLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReqLookUp))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbProvider = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkNonPatient = New System.Windows.Forms.CheckBox()
        Me.txtDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtPatient = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnAccSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.AccID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Provider = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Patient = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Payer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(839, 31)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(79, 28)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(59, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 31)
        '
        'btnHelp
        '
        Me.btnHelp.AutoSize = False
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(48, 22)
        Me.btnHelp.Text = "Help"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbProvider)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkNonPatient)
        Me.GroupBox1.Controls.Add(Me.txtDate)
        Me.GroupBox1.Controls.Add(Me.txtPatient)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnAccSearch)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 47)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(797, 86)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'cmbProvider
        '
        Me.cmbProvider.FormattingEnabled = True
        Me.cmbProvider.Items.AddRange(New Object() {"Name (Last, First) even partial", "Social Security No"})
        Me.cmbProvider.Location = New System.Drawing.Point(315, 37)
        Me.cmbProvider.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbProvider.Name = "cmbProvider"
        Me.cmbProvider.Size = New System.Drawing.Size(259, 24)
        Me.cmbProvider.Sorted = True
        Me.cmbProvider.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(8, 18)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 16)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Non-Patient"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(595, 18)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Accession Date"
        '
        'chkNonPatient
        '
        Me.chkNonPatient.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkNonPatient.ForeColor = System.Drawing.Color.DarkBlue
        Me.chkNonPatient.Location = New System.Drawing.Point(36, 38)
        Me.chkNonPatient.Margin = New System.Windows.Forms.Padding(4)
        Me.chkNonPatient.Name = "chkNonPatient"
        Me.chkNonPatient.Size = New System.Drawing.Size(17, 23)
        Me.chkNonPatient.TabIndex = 3
        Me.chkNonPatient.UseVisualStyleBackColor = True
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(595, 38)
        Me.txtDate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDate.Mask = "00/00/0000"
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(111, 22)
        Me.txtDate.TabIndex = 4
        Me.txtDate.ValidatingType = GetType(Date)
        '
        'txtPatient
        '
        Me.txtPatient.Location = New System.Drawing.Point(117, 38)
        Me.txtPatient.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(172, 22)
        Me.txtPatient.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(323, 17)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 22)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Ordering Provider"
        '
        'btnAccSearch
        '
        Me.btnAccSearch.Enabled = False
        Me.btnAccSearch.Image = CType(resources.GetObject("btnAccSearch.Image"), System.Drawing.Image)
        Me.btnAccSearch.Location = New System.Drawing.Point(732, 32)
        Me.btnAccSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAccSearch.Name = "btnAccSearch"
        Me.btnAccSearch.Size = New System.Drawing.Size(41, 34)
        Me.btnAccSearch.TabIndex = 5
        Me.btnAccSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(117, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Patient Name (Last, First)"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LemonChiffon
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AccID, Me.AccDate, Me.Provider, Me.Patient, Me.Sex, Me.DOB, Me.Payer})
        Me.dgv.Location = New System.Drawing.Point(16, 140)
        Me.dgv.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.RowHeadersWidth = 51
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(797, 342)
        Me.dgv.TabIndex = 6
        '
        'AccID
        '
        Me.AccID.FillWeight = 90.0!
        Me.AccID.HeaderText = "Acc ID"
        Me.AccID.MinimumWidth = 6
        Me.AccID.Name = "AccID"
        Me.AccID.ReadOnly = True
        Me.AccID.Width = 90
        '
        'AccDate
        '
        Me.AccDate.FillWeight = 80.0!
        Me.AccDate.HeaderText = "Acc Date"
        Me.AccDate.MinimumWidth = 6
        Me.AccDate.Name = "AccDate"
        Me.AccDate.ReadOnly = True
        Me.AccDate.Width = 80
        '
        'Provider
        '
        Me.Provider.FillWeight = 110.0!
        Me.Provider.HeaderText = "Ord Provider"
        Me.Provider.MinimumWidth = 6
        Me.Provider.Name = "Provider"
        Me.Provider.ReadOnly = True
        Me.Provider.Width = 110
        '
        'Patient
        '
        Me.Patient.FillWeight = 110.0!
        Me.Patient.HeaderText = "Patient (L, F)"
        Me.Patient.MinimumWidth = 6
        Me.Patient.Name = "Patient"
        Me.Patient.ReadOnly = True
        Me.Patient.Width = 110
        '
        'Sex
        '
        Me.Sex.FillWeight = 30.0!
        Me.Sex.HeaderText = "Sex"
        Me.Sex.MinimumWidth = 6
        Me.Sex.Name = "Sex"
        Me.Sex.ReadOnly = True
        Me.Sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sex.Width = 30
        '
        'DOB
        '
        Me.DOB.FillWeight = 60.0!
        Me.DOB.HeaderText = "D.O.B."
        Me.DOB.MinimumWidth = 6
        Me.DOB.Name = "DOB"
        Me.DOB.ReadOnly = True
        Me.DOB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DOB.Width = 60
        '
        'Payer
        '
        Me.Payer.HeaderText = "Payer"
        Me.Payer.MinimumWidth = 6
        Me.Payer.Name = "Payer"
        Me.Payer.ReadOnly = True
        Me.Payer.Width = 125
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(696, 289)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(93, 41)
        Me.btn_Cancel.TabIndex = 27
        Me.btn_Cancel.TabStop = False
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frmReqLookUp
        '
        Me.AcceptButton = Me.btnAccSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(839, 517)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReqLookUp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Requisition Look Up"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPatient As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbProvider As System.Windows.Forms.ComboBox
    Friend WithEvents btnAccSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkNonPatient As System.Windows.Forms.CheckBox
    Friend WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Payer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

End Class
