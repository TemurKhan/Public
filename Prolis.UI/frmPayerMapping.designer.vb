<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPayerMapping
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayerMapping))
        Me.dgvMapping = New System.Windows.Forms.DataGridView
        Me.DEL = New System.Windows.Forms.DataGridViewImageColumn
        Me.PayerID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PayerName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExternalSystem = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ExternalID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbSystems = New System.Windows.Forms.ComboBox
        Me.btnUpload = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvMapping
        '
        Me.dgvMapping.AllowUserToAddRows = False
        Me.dgvMapping.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.AntiqueWhite
        Me.dgvMapping.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvMapping.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DEL, Me.PayerID, Me.PayerName, Me.ExternalSystem, Me.ExternalID})
        Me.dgvMapping.Location = New System.Drawing.Point(12, 65)
        Me.dgvMapping.Name = "dgvMapping"
        Me.dgvMapping.RowHeadersVisible = False
        Me.dgvMapping.Size = New System.Drawing.Size(626, 407)
        Me.dgvMapping.TabIndex = 28
        '
        'DEL
        '
        Me.DEL.FillWeight = 30.0!
        Me.DEL.HeaderText = ""
        Me.DEL.Image = CType(resources.GetObject("DEL.Image"), System.Drawing.Image)
        Me.DEL.Name = "DEL"
        Me.DEL.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DEL.Width = 30
        '
        'PayerID
        '
        Me.PayerID.FillWeight = 90.0!
        Me.PayerID.HeaderText = "Prolis ID"
        Me.PayerID.MaxInputLength = 12
        Me.PayerID.Name = "PayerID"
        Me.PayerID.Width = 90
        '
        'PayerName
        '
        Me.PayerName.FillWeight = 220.0!
        Me.PayerName.HeaderText = "Payer Name"
        Me.PayerName.Name = "PayerName"
        Me.PayerName.ReadOnly = True
        Me.PayerName.Width = 220
        '
        'ExternalSystem
        '
        Me.ExternalSystem.FillWeight = 150.0!
        Me.ExternalSystem.HeaderText = "External System"
        Me.ExternalSystem.MaxInputLength = 100
        Me.ExternalSystem.Name = "ExternalSystem"
        Me.ExternalSystem.Width = 150
        '
        'ExternalID
        '
        Me.ExternalID.FillWeight = 110.0!
        Me.ExternalID.HeaderText = "External ID"
        Me.ExternalID.MaxInputLength = 200
        Me.ExternalID.Name = "ExternalID"
        Me.ExternalID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.ExternalID.Width = 110
        '
        'cmbSystems
        '
        Me.cmbSystems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSystems.FormattingEnabled = True
        Me.cmbSystems.Location = New System.Drawing.Point(12, 35)
        Me.cmbSystems.Name = "cmbSystems"
        Me.cmbSystems.Size = New System.Drawing.Size(280, 21)
        Me.cmbSystems.TabIndex = 29
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(512, 29)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(117, 30)
        Me.btnUpload.TabIndex = 30
        Me.btnUpload.Text = "Upload Mapping"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(21, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "External Existing Systems"
        '
        'frmPayerMapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 484)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.cmbSystems)
        Me.Controls.Add(Me.dgvMapping)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPayerMapping"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Payer Mapping"
        CType(Me.dgvMapping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dgvMapping As System.Windows.Forms.DataGridView
    Friend WithEvents cmbSystems As System.Windows.Forms.ComboBox
    Friend WithEvents btnUpload As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DEL As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents PayerID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PayerName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExternalSystem As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExternalID As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
