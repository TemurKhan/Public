<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTimeSensitive
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTimeSensitive))
        Me.dgvTGPs = New System.Windows.Forms.DataGridView
        Me.Del = New System.Windows.Forms.DataGridViewImageColumn
        Me.TGPID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LookUp = New System.Windows.Forms.DataGridViewImageColumn
        Me.TGPName = New System.Windows.Forms.DataGridViewTextBoxColumn
        CType(Me.dgvTGPs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvTGPs
        '
        Me.dgvTGPs.AllowUserToAddRows = False
        Me.dgvTGPs.AllowUserToDeleteRows = False
        Me.dgvTGPs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTGPs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Del, Me.TGPID, Me.LookUp, Me.TGPName})
        Me.dgvTGPs.Location = New System.Drawing.Point(12, 24)
        Me.dgvTGPs.Name = "dgvTGPs"
        Me.dgvTGPs.RowHeadersVisible = False
        Me.dgvTGPs.Size = New System.Drawing.Size(499, 318)
        Me.dgvTGPs.TabIndex = 1
        '
        'Del
        '
        Me.Del.FillWeight = 30.0!
        Me.Del.HeaderText = ""
        Me.Del.Image = CType(resources.GetObject("Del.Image"), System.Drawing.Image)
        Me.Del.Name = "Del"
        Me.Del.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Del.Width = 30
        '
        'TGPID
        '
        Me.TGPID.FillWeight = 80.0!
        Me.TGPID.HeaderText = "Comp ID"
        Me.TGPID.MaxInputLength = 10
        Me.TGPID.Name = "TGPID"
        Me.TGPID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TGPID.Width = 80
        '
        'LookUp
        '
        Me.LookUp.FillWeight = 30.0!
        Me.LookUp.HeaderText = ""
        Me.LookUp.Image = CType(resources.GetObject("LookUp.Image"), System.Drawing.Image)
        Me.LookUp.Name = "LookUp"
        Me.LookUp.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LookUp.Width = 30
        '
        'TGPName
        '
        Me.TGPName.FillWeight = 336.0!
        Me.TGPName.HeaderText = "Component"
        Me.TGPName.Name = "TGPName"
        Me.TGPName.ReadOnly = True
        Me.TGPName.Width = 336
        '
        'frmTimeSensitive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 359)
        Me.Controls.Add(Me.dgvTGPs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimeSensitive"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Time Sensitive Tests"
        CType(Me.dgvTGPs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvTGPs As System.Windows.Forms.DataGridView
    Friend WithEvents Del As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LookUp As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGPName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
