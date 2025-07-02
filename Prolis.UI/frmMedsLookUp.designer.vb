<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMedsLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMedsLookUp))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.dgvMeds = New System.Windows.Forms.DataGridView()
        Me.Med = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lbl_TotRec = New System.Windows.Forms.Label()
        Me.btnLook = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvMeds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(424, 27)
        Me.ToolStrip1.TabIndex = 6
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
        Me.btnCancel.Size = New System.Drawing.Size(80, 22)
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
        Me.btnHelp.Size = New System.Drawing.Size(80, 22)
        Me.btnHelp.Text = "Help"
        '
        'dgvMeds
        '
        Me.dgvMeds.AllowUserToAddRows = False
        Me.dgvMeds.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.dgvMeds.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMeds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMeds.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Med})
        Me.dgvMeds.Location = New System.Drawing.Point(13, 143)
        Me.dgvMeds.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvMeds.Name = "dgvMeds"
        Me.dgvMeds.ReadOnly = True
        Me.dgvMeds.RowHeadersVisible = False
        Me.dgvMeds.Size = New System.Drawing.Size(397, 235)
        Me.dgvMeds.TabIndex = 7
        '
        'Med
        '
        Me.Med.FillWeight = 260.0!
        Me.Med.HeaderText = "Drug"
        Me.Med.Name = "Med"
        Me.Med.ReadOnly = True
        Me.Med.Width = 260
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(294, 163)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(93, 41)
        Me.btn_Cancel.TabIndex = 30
        Me.btn_Cancel.TabStop = False
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lbl_TotRec)
        Me.GroupBox1.Controls.Add(Me.btnLook)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 48)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(397, 74)
        Me.GroupBox1.TabIndex = 31
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Search"
        '
        'lbl_TotRec
        '
        Me.lbl_TotRec.AutoSize = True
        Me.lbl_TotRec.ForeColor = System.Drawing.Color.Blue
        Me.lbl_TotRec.Location = New System.Drawing.Point(268, 45)
        Me.lbl_TotRec.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_TotRec.Name = "lbl_TotRec"
        Me.lbl_TotRec.Size = New System.Drawing.Size(129, 17)
        Me.lbl_TotRec.TabIndex = 5
        Me.lbl_TotRec.Text = "Total Rec               "
        '
        'btnLook
        '
        Me.btnLook.Image = CType(resources.GetObject("btnLook.Image"), System.Drawing.Image)
        Me.btnLook.Location = New System.Drawing.Point(202, 38)
        Me.btnLook.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLook.Name = "btnLook"
        Me.btnLook.Size = New System.Drawing.Size(39, 30)
        Me.btnLook.TabIndex = 1
        Me.btnLook.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.SaddleBrown
        Me.Label1.Location = New System.Drawing.Point(9, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(351, 16)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "A part of Name of Medications"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(12, 42)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(182, 22)
        Me.txtSearch.TabIndex = 0
        '
        'frmMedsLookUp
        '
        Me.AcceptButton = Me.btnLook
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(424, 391)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvMeds)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMedsLookUp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Medications LookUp"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvMeds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvMeds As System.Windows.Forms.DataGridView
    Friend WithEvents Med As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_TotRec As System.Windows.Forms.Label
    Friend WithEvents btnLook As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox

End Class
