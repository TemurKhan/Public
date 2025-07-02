<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCultureLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCultureLookUp))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.dgvCultures = New System.Windows.Forms.DataGridView()
        Me.Country = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Language = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Culture2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.C3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.L2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.L3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btn_Cancel = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvCultures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(736, 27)
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
        'dgvCultures
        '
        Me.dgvCultures.AllowUserToAddRows = False
        Me.dgvCultures.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvCultures.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCultures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCultures.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Country, Me.Language, Me.Culture2, Me.C2, Me.C3, Me.L2, Me.L3})
        Me.dgvCultures.Location = New System.Drawing.Point(16, 52)
        Me.dgvCultures.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvCultures.Name = "dgvCultures"
        Me.dgvCultures.ReadOnly = True
        Me.dgvCultures.RowHeadersVisible = False
        Me.dgvCultures.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvCultures.Size = New System.Drawing.Size(700, 372)
        Me.dgvCultures.TabIndex = 7
        '
        'Country
        '
        Me.Country.FillWeight = 140.0!
        Me.Country.HeaderText = "Country"
        Me.Country.MaxInputLength = 60
        Me.Country.Name = "Country"
        Me.Country.ReadOnly = True
        Me.Country.Width = 140
        '
        'Language
        '
        Me.Language.FillWeight = 110.0!
        Me.Language.HeaderText = "Language"
        Me.Language.MaxInputLength = 60
        Me.Language.Name = "Language"
        Me.Language.ReadOnly = True
        Me.Language.Width = 110
        '
        'Culture2
        '
        Me.Culture2.FillWeight = 90.0!
        Me.Culture2.HeaderText = "Culture"
        Me.Culture2.MaxInputLength = 10
        Me.Culture2.Name = "Culture2"
        Me.Culture2.ReadOnly = True
        Me.Culture2.Width = 90
        '
        'C2
        '
        Me.C2.FillWeight = 40.0!
        Me.C2.HeaderText = "C2"
        Me.C2.Name = "C2"
        Me.C2.ReadOnly = True
        Me.C2.Width = 40
        '
        'C3
        '
        Me.C3.FillWeight = 40.0!
        Me.C3.HeaderText = "C3"
        Me.C3.Name = "C3"
        Me.C3.ReadOnly = True
        Me.C3.Width = 40
        '
        'L2
        '
        Me.L2.FillWeight = 40.0!
        Me.L2.HeaderText = "L2"
        Me.L2.Name = "L2"
        Me.L2.ReadOnly = True
        Me.L2.Width = 40
        '
        'L3
        '
        Me.L3.FillWeight = 40.0!
        Me.L3.HeaderText = "L3"
        Me.L3.Name = "L3"
        Me.L3.ReadOnly = True
        Me.L3.Width = 40
        '
        'btn_Cancel
        '
        Me.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Cancel.Location = New System.Drawing.Point(587, 259)
        Me.btn_Cancel.Name = "btn_Cancel"
        Me.btn_Cancel.Size = New System.Drawing.Size(93, 41)
        Me.btn_Cancel.TabIndex = 29
        Me.btn_Cancel.TabStop = False
        Me.btn_Cancel.Text = "Cancel"
        Me.btn_Cancel.UseVisualStyleBackColor = True
        '
        'frmCultureLookUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btn_Cancel
        Me.ClientSize = New System.Drawing.Size(736, 446)
        Me.Controls.Add(Me.dgvCultures)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btn_Cancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCultureLookUp"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Culture LookUp"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvCultures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvCultures As System.Windows.Forms.DataGridView
    Friend WithEvents Country As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Culture2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents C3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents L2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents L3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button

End Class
