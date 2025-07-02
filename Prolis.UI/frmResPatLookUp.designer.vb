<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResPatLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResPatLookUp))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPatSearch = New System.Windows.Forms.Button()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.SrcID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MRN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOS = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ReqID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Provider = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnProvLookUp = New System.Windows.Forms.Button()
        Me.txtProviderName = New System.Windows.Forms.TextBox()
        Me.txtProviderID = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtDOB = New System.Windows.Forms.MaskedTextBox()
        Me.txtSearch = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbSearch = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1020, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.AutoSize = False
        Me.btnAccept.Enabled = False
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(80, 22)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.AutoSize = False
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(80, 22)
        Me.btnHelp.Text = "Help"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.btnPatSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 46)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(988, 256)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'btnPatSearch
        '
        Me.btnPatSearch.Image = CType(resources.GetObject("btnPatSearch.Image"), System.Drawing.Image)
        Me.btnPatSearch.Location = New System.Drawing.Point(835, 207)
        Me.btnPatSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPatSearch.Name = "btnPatSearch"
        Me.btnPatSearch.Size = New System.Drawing.Size(145, 40)
        Me.btnPatSearch.TabIndex = 7
        Me.btnPatSearch.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgv.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SrcID, Me.LastName, Me.FirstName, Me.DOB, Me.Gender, Me.MRN, Me.AccID, Me.DOS, Me.ReqID, Me.Provider})
        Me.dgv.Location = New System.Drawing.Point(16, 310)
        Me.dgv.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.RowHeadersWidth = 51
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(988, 354)
        Me.dgv.TabIndex = 1
        '
        'SrcID
        '
        Me.SrcID.FillWeight = 60.0!
        Me.SrcID.HeaderText = "ID"
        Me.SrcID.MaxInputLength = 12
        Me.SrcID.MinimumWidth = 6
        Me.SrcID.Name = "SrcID"
        Me.SrcID.ReadOnly = True
        Me.SrcID.Visible = False
        Me.SrcID.Width = 60
        '
        'LastName
        '
        Me.LastName.FillWeight = 90.0!
        Me.LastName.HeaderText = "Last Name"
        Me.LastName.MaxInputLength = 35
        Me.LastName.MinimumWidth = 6
        Me.LastName.Name = "LastName"
        Me.LastName.ReadOnly = True
        Me.LastName.Width = 90
        '
        'FirstName
        '
        Me.FirstName.FillWeight = 90.0!
        Me.FirstName.HeaderText = "First Name"
        Me.FirstName.MaxInputLength = 35
        Me.FirstName.MinimumWidth = 6
        Me.FirstName.Name = "FirstName"
        Me.FirstName.ReadOnly = True
        Me.FirstName.Width = 90
        '
        'DOB
        '
        Me.DOB.FillWeight = 70.0!
        Me.DOB.HeaderText = "DOB"
        Me.DOB.MinimumWidth = 6
        Me.DOB.Name = "DOB"
        Me.DOB.ReadOnly = True
        Me.DOB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DOB.Width = 70
        '
        'Gender
        '
        Me.Gender.FillWeight = 50.0!
        Me.Gender.HeaderText = "Gender"
        Me.Gender.MinimumWidth = 6
        Me.Gender.Name = "Gender"
        Me.Gender.ReadOnly = True
        Me.Gender.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Gender.Width = 50
        '
        'MRN
        '
        Me.MRN.FillWeight = 80.0!
        Me.MRN.HeaderText = "MRN"
        Me.MRN.MinimumWidth = 6
        Me.MRN.Name = "MRN"
        Me.MRN.ReadOnly = True
        Me.MRN.Width = 80
        '
        'AccID
        '
        Me.AccID.FillWeight = 80.0!
        Me.AccID.HeaderText = "Accession"
        Me.AccID.MaxInputLength = 12
        Me.AccID.MinimumWidth = 6
        Me.AccID.Name = "AccID"
        Me.AccID.ReadOnly = True
        Me.AccID.Width = 80
        '
        'DOS
        '
        Me.DOS.FillWeight = 70.0!
        Me.DOS.HeaderText = "D O S"
        Me.DOS.MaxInputLength = 112
        Me.DOS.MinimumWidth = 6
        Me.DOS.Name = "DOS"
        Me.DOS.ReadOnly = True
        Me.DOS.Width = 70
        '
        'ReqID
        '
        Me.ReqID.FillWeight = 80.0!
        Me.ReqID.HeaderText = "ReqID"
        Me.ReqID.MinimumWidth = 6
        Me.ReqID.Name = "ReqID"
        Me.ReqID.ReadOnly = True
        Me.ReqID.Width = 80
        '
        'Provider
        '
        Me.Provider.FillWeight = 135.0!
        Me.Provider.HeaderText = "Provider"
        Me.Provider.MinimumWidth = 6
        Me.Provider.Name = "Provider"
        Me.Provider.ReadOnly = True
        Me.Provider.Width = 135
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(731, 336)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(93, 41)
        Me.btn_Cancel.TabIndex = 27
        Me.btn_Cancel.TabStop = False
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.btnProvLookUp)
        Me.GroupBox2.Controls.Add(Me.txtProviderName)
        Me.GroupBox2.Controls.Add(Me.txtProviderID)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFrom)
        Me.GroupBox2.Controls.Add(Me.txtTo)
        Me.GroupBox2.Location = New System.Drawing.Point(81, 22)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(900, 100)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Search patient by Provider"
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label8.Location = New System.Drawing.Point(234, 36)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(251, 16)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Ordering Provider Name"
        '
        'btnProvLookUp
        '
        Me.btnProvLookUp.Image = CType(resources.GetObject("btnProvLookUp.Image"), System.Drawing.Image)
        Me.btnProvLookUp.Location = New System.Drawing.Point(178, 51)
        Me.btnProvLookUp.Margin = New System.Windows.Forms.Padding(4)
        Me.btnProvLookUp.Name = "btnProvLookUp"
        Me.btnProvLookUp.Size = New System.Drawing.Size(35, 32)
        Me.btnProvLookUp.TabIndex = 17
        Me.btnProvLookUp.UseVisualStyleBackColor = True
        '
        'txtProviderName
        '
        Me.txtProviderName.Location = New System.Drawing.Point(237, 56)
        Me.txtProviderName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProviderName.Name = "txtProviderName"
        Me.txtProviderName.ReadOnly = True
        Me.txtProviderName.Size = New System.Drawing.Size(315, 22)
        Me.txtProviderName.TabIndex = 19
        Me.txtProviderName.TabStop = False
        '
        'txtProviderID
        '
        Me.txtProviderID.Location = New System.Drawing.Point(57, 56)
        Me.txtProviderID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtProviderID.Name = "txtProviderID"
        Me.txtProviderID.Size = New System.Drawing.Size(112, 22)
        Me.txtProviderID.TabIndex = 16
        Me.txtProviderID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label6.Location = New System.Drawing.Point(714, 36)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 16)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "To"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label5.Location = New System.Drawing.Point(605, 37)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 16)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "From"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label4.Location = New System.Drawing.Point(566, 13)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(279, 23)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Date of Service"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(61, 36)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 16)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Provider ID"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(593, 56)
        Me.txtFrom.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFrom.Mask = "00/00/0000"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(112, 22)
        Me.txtFrom.TabIndex = 18
        Me.txtFrom.ValidatingType = GetType(Date)
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(718, 56)
        Me.txtTo.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTo.Mask = "00/00/0000"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(112, 22)
        Me.txtTo.TabIndex = 20
        Me.txtTo.ValidatingType = GetType(Date)
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.txtDOB)
        Me.GroupBox3.Controls.Add(Me.txtSearch)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmbSearch)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Location = New System.Drawing.Point(81, 128)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(900, 72)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "OR Search Patient by Name"
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(674, 20)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 16)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "DOB"
        '
        'txtDOB
        '
        Me.txtDOB.Location = New System.Drawing.Point(674, 43)
        Me.txtDOB.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDOB.Mask = "00/00/0000"
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.Size = New System.Drawing.Size(116, 22)
        Me.txtDOB.TabIndex = 19
        Me.txtDOB.ValidatingType = GetType(Date)
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(54, 43)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(236, 22)
        Me.txtSearch.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(358, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 16)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Search Term"
        '
        'cmbSearch
        '
        Me.cmbSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearch.FormattingEnabled = True
        Me.cmbSearch.Items.AddRange(New Object() {"Patient Name (Last, First) even partial", "Social Security No", "EMR No"})
        Me.cmbSearch.Location = New System.Drawing.Point(362, 43)
        Me.cmbSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbSearch.Name = "cmbSearch"
        Me.cmbSearch.Size = New System.Drawing.Size(292, 24)
        Me.cmbSearch.TabIndex = 18
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(54, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(281, 16)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Patient Name (Last, First) even partial"
        '
        'frmResPatLookUp
        '
        Me.AcceptButton = Me.btnPatSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(1020, 679)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmResPatLookUp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Patient LookUp"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
    Friend WithEvents btnPatSearch As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents SrcID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MRN As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOS As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ReqID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Provider As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label8 As Label
    Friend WithEvents btnProvLookUp As Button
    Friend WithEvents txtProviderName As TextBox
    Friend WithEvents txtProviderID As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtFrom As MaskedTextBox
    Friend WithEvents txtTo As MaskedTextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtDOB As MaskedTextBox
    Friend WithEvents txtSearch As MaskedTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbSearch As ComboBox
    Friend WithEvents Label1 As Label
End Class
