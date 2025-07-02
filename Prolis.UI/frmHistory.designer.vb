<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHistory))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.txtDOB = New System.Windows.Forms.TextBox
        Me.pctSex = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtSex = New System.Windows.Forms.TextBox
        Me.txtTest = New System.Windows.Forms.TextBox
        Me.txtUnit = New System.Windows.Forms.TextBox
        Me.dgvResults = New System.Windows.Forms.DataGridView
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.AccID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.AccDate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Result = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ToolStrip1.SuspendLayout()
        CType(Me.pctSex, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(410, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
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
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(12, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 15)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Patient Name (Last, First)"
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtName.Location = New System.Drawing.Point(11, 53)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(171, 20)
        Me.txtName.TabIndex = 6
        '
        'txtDOB
        '
        Me.txtDOB.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.txtDOB.Location = New System.Drawing.Point(188, 53)
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.ReadOnly = True
        Me.txtDOB.Size = New System.Drawing.Size(81, 20)
        Me.txtDOB.TabIndex = 7
        '
        'pctSex
        '
        Me.pctSex.Location = New System.Drawing.Point(284, 52)
        Me.pctSex.Name = "pctSex"
        Me.pctSex.Size = New System.Drawing.Size(22, 20)
        Me.pctSex.TabIndex = 8
        Me.pctSex.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(188, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 15)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "D.O.B."
        '
        'txtSex
        '
        Me.txtSex.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtSex.Location = New System.Drawing.Point(312, 52)
        Me.txtSex.Name = "txtSex"
        Me.txtSex.ReadOnly = True
        Me.txtSex.Size = New System.Drawing.Size(81, 20)
        Me.txtSex.TabIndex = 10
        '
        'txtTest
        '
        Me.txtTest.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtTest.Location = New System.Drawing.Point(11, 96)
        Me.txtTest.Name = "txtTest"
        Me.txtTest.ReadOnly = True
        Me.txtTest.Size = New System.Drawing.Size(295, 20)
        Me.txtTest.TabIndex = 11
        '
        'txtUnit
        '
        Me.txtUnit.BackColor = System.Drawing.SystemColors.HighlightText
        Me.txtUnit.Location = New System.Drawing.Point(312, 96)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.ReadOnly = True
        Me.txtUnit.Size = New System.Drawing.Size(81, 20)
        Me.txtUnit.TabIndex = 12
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.AccID, Me.AccDate, Me.Result})
        Me.dgvResults.Location = New System.Drawing.Point(12, 127)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.ReadOnly = True
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.Size = New System.Drawing.Size(382, 150)
        Me.dgvResults.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(312, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 15)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Gender"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(14, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 15)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Analyte"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(316, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 15)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Unit"
        '
        'AccID
        '
        Me.AccID.HeaderText = "Accession ID"
        Me.AccID.MaxInputLength = 12
        Me.AccID.Name = "AccID"
        Me.AccID.ReadOnly = True
        '
        'AccDate
        '
        Me.AccDate.FillWeight = 90.0!
        Me.AccDate.HeaderText = "Dated"
        Me.AccDate.Name = "AccDate"
        Me.AccDate.ReadOnly = True
        Me.AccDate.Width = 90
        '
        'Result
        '
        Me.Result.FillWeight = 189.0!
        Me.Result.HeaderText = "Result"
        Me.Result.MaxInputLength = 100
        Me.Result.Name = "Result"
        Me.Result.ReadOnly = True
        Me.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Result.Width = 189
        '
        'frmHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 289)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dgvResults)
        Me.Controls.Add(Me.txtUnit)
        Me.Controls.Add(Me.txtTest)
        Me.Controls.Add(Me.txtSex)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pctSex)
        Me.Controls.Add(Me.txtDOB)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHistory"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "History"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.pctSex, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtDOB As System.Windows.Forms.TextBox
    Friend WithEvents pctSex As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSex As System.Windows.Forms.TextBox
    Friend WithEvents txtTest As System.Windows.Forms.TextBox
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AccID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
