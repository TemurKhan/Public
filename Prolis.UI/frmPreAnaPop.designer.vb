<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPreAnaPop
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPreAnaPop))
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.CompID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CompName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Result = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.txtTGP = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtAccID = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnOK = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.Answers = New System.Windows.Forms.RichTextBox()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.MintCream
        Me.dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CompID, Me.CompName, Me.Result})
        Me.dgvResults.Location = New System.Drawing.Point(12, 208)
        Me.dgvResults.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.RowHeadersWidth = 51
        Me.dgvResults.Size = New System.Drawing.Size(633, 277)
        Me.dgvResults.TabIndex = 0
        '
        'CompID
        '
        Me.CompID.FillWeight = 80.0!
        Me.CompID.HeaderText = "ID"
        Me.CompID.MinimumWidth = 6
        Me.CompID.Name = "CompID"
        Me.CompID.ReadOnly = True
        Me.CompID.Width = 80
        '
        'CompName
        '
        Me.CompName.FillWeight = 150.0!
        Me.CompName.HeaderText = "Component"
        Me.CompName.MinimumWidth = 6
        Me.CompName.Name = "CompName"
        Me.CompName.ReadOnly = True
        Me.CompName.Width = 150
        '
        'Result
        '
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        Me.Result.DefaultCellStyle = DataGridViewCellStyle2
        Me.Result.FillWeight = 150.0!
        Me.Result.HeaderText = "Result"
        Me.Result.MaxInputLength = 100
        Me.Result.MinimumWidth = 6
        Me.Result.Name = "Result"
        Me.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Result.Width = 150
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(13, 64)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(113, 22)
        Me.txtID.TabIndex = 1
        '
        'txtTGP
        '
        Me.txtTGP.Location = New System.Drawing.Point(136, 64)
        Me.txtTGP.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTGP.Name = "txtTGP"
        Me.txtTGP.ReadOnly = True
        Me.txtTGP.Size = New System.Drawing.Size(425, 22)
        Me.txtTGP.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 16)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Parent ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(136, 46)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 16)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Parent Name"
        '
        'txtAccID
        '
        Me.txtAccID.Location = New System.Drawing.Point(528, 41)
        Me.txtAccID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAccID.Name = "txtAccID"
        Me.txtAccID.ReadOnly = True
        Me.txtAccID.Size = New System.Drawing.Size(33, 22)
        Me.txtAccID.TabIndex = 5
        Me.txtAccID.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator2, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(658, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnOK
        '
        Me.btnOK.AutoSize = False
        Me.btnOK.Enabled = False
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(80, 22)
        Me.btnOK.Text = "Accept"
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
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
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
        'Answers
        '
        Me.Answers.Location = New System.Drawing.Point(16, 105)
        Me.Answers.Name = "Answers"
        Me.Answers.Size = New System.Drawing.Size(629, 96)
        Me.Answers.TabIndex = 7
        Me.Answers.Text = ""
        '
        'frmPreAnaPop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(658, 498)
        Me.ControlBox = False
        Me.Controls.Add(Me.Answers)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtAccID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTGP)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.dgvResults)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPreAnaPop"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Preanalytical Entry"
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents CompID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CompName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Result As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtTGP As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Answers As RichTextBox
End Class
