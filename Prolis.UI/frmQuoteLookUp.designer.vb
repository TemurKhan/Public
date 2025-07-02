<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuoteLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQuoteLookUp))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtPatient = New System.Windows.Forms.MaskedTextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.QuoteID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Dated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Lname = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Sex = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DOB = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.QAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        Me.ToolStrip1.Size = New System.Drawing.Size(885, 27)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.Enabled = False
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(79, 24)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(59, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 27)
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
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtDate)
        Me.GroupBox1.Controls.Add(Me.txtPatient)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 47)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(833, 86)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(635, 18)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 16)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Quote Date"
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(627, 38)
        Me.txtDate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtDate.Mask = "00/00/0000"
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(111, 22)
        Me.txtDate.TabIndex = 2
        Me.txtDate.ValidatingType = GetType(Date)
        '
        'txtPatient
        '
        Me.txtPatient.Location = New System.Drawing.Point(25, 38)
        Me.txtPatient.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.Size = New System.Drawing.Size(575, 22)
        Me.txtPatient.TabIndex = 1
        '
        'btnSearch
        '
        Me.btnSearch.Enabled = False
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(763, 32)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(41, 34)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(35, 18)
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
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.QuoteID, Me.Dated, Me.Lname, Me.FName, Me.Sex, Me.DOB, Me.QAmt, Me.DocNo, Me.PayAmt})
        Me.dgv.Location = New System.Drawing.Point(16, 153)
        Me.dgv.Margin = New System.Windows.Forms.Padding(4)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(853, 342)
        Me.dgv.TabIndex = 1
        '
        'QuoteID
        '
        Me.QuoteID.FillWeight = 76.0!
        Me.QuoteID.HeaderText = "Quote ID"
        Me.QuoteID.Name = "QuoteID"
        Me.QuoteID.ReadOnly = True
        Me.QuoteID.Width = 76
        '
        'Dated
        '
        Me.Dated.FillWeight = 70.0!
        Me.Dated.HeaderText = "Dated"
        Me.Dated.Name = "Dated"
        Me.Dated.ReadOnly = True
        Me.Dated.Width = 70
        '
        'Lname
        '
        Me.Lname.FillWeight = 90.0!
        Me.Lname.HeaderText = "Last Name"
        Me.Lname.Name = "Lname"
        Me.Lname.ReadOnly = True
        Me.Lname.Width = 90
        '
        'FName
        '
        Me.FName.FillWeight = 90.0!
        Me.FName.HeaderText = "First Name"
        Me.FName.Name = "FName"
        Me.FName.ReadOnly = True
        Me.FName.Width = 90
        '
        'Sex
        '
        Me.Sex.FillWeight = 30.0!
        Me.Sex.HeaderText = "Sex"
        Me.Sex.Name = "Sex"
        Me.Sex.ReadOnly = True
        Me.Sex.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Sex.Width = 30
        '
        'DOB
        '
        Me.DOB.FillWeight = 60.0!
        Me.DOB.HeaderText = "D.O.B."
        Me.DOB.Name = "DOB"
        Me.DOB.ReadOnly = True
        Me.DOB.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DOB.Width = 60
        '
        'QAmt
        '
        Me.QAmt.FillWeight = 80.0!
        Me.QAmt.HeaderText = "Amount $"
        Me.QAmt.Name = "QAmt"
        Me.QAmt.ReadOnly = True
        Me.QAmt.Width = 80
        '
        'DocNo
        '
        Me.DocNo.FillWeight = 50.0!
        Me.DocNo.HeaderText = "Doc No"
        Me.DocNo.Name = "DocNo"
        Me.DocNo.ReadOnly = True
        Me.DocNo.Width = 50
        '
        'PayAmt
        '
        Me.PayAmt.FillWeight = 80.0!
        Me.PayAmt.HeaderText = "Paid $"
        Me.PayAmt.Name = "PayAmt"
        Me.PayAmt.ReadOnly = True
        Me.PayAmt.Width = 80
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(756, 258)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(93, 41)
        Me.btn_Cancel.TabIndex = 29
        Me.btn_Cancel.TabStop = False
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frmQuoteLookUp
        '
        Me.AcceptButton = Me.btnSearch
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(885, 553)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuoteLookUp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmQuoteLookUp"
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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtPatient As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents QuoteID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Dated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Lname As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DOB As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayAmt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

End Class
