<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScriptLookUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScriptLookUp))
        ToolStrip1 = New ToolStrip()
        btnOK = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgv = New DataGridView()
        Identifier = New DataGridViewTextBoxColumn()
        Script = New DataGridViewTextBoxColumn()
        btnSearch = New Button()
        txtIdentifier = New TextBox()
        Label1 = New Label()
        btn_Cancel = New Button()
        ToolStrip1.SuspendLayout()
        CType(dgv, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnOK, ToolStripSeparator1, btnCancel, ToolStripSeparator2, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(641, 34)
        ToolStrip1.TabIndex = 3
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnOK
        ' 
        btnOK.Enabled = False
        btnOK.Image = CType(resources.GetObject("btnOK.Image"), Image)
        btnOK.ImageTransparentColor = Color.Magenta
        btnOK.Name = "btnOK"
        btnOK.Size = New Size(90, 29)
        btnOK.Text = "Accept"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(87, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgv
        ' 
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgv.Columns.AddRange(New DataGridViewColumn() {Identifier, Script})
        dgv.Location = New Point(16, 106)
        dgv.Margin = New Padding(4)
        dgv.Name = "dgv"
        dgv.ReadOnly = True
        dgv.RowHeadersVisible = False
        dgv.RowHeadersWidth = 62
        dgv.Size = New Size(609, 299)
        dgv.TabIndex = 2
        ' 
        ' Identifier
        ' 
        Identifier.FillWeight = 200F
        Identifier.HeaderText = "Identifier"
        Identifier.MaxInputLength = 100
        Identifier.MinimumWidth = 8
        Identifier.Name = "Identifier"
        Identifier.ReadOnly = True
        Identifier.Width = 200
        ' 
        ' Script
        ' 
        Script.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Script.FillWeight = 228F
        Script.HeaderText = "Script"
        Script.MaxInputLength = 4000
        Script.MinimumWidth = 8
        Script.Name = "Script"
        Script.ReadOnly = True
        Script.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' btnSearch
        ' 
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(575, 60)
        btnSearch.Margin = New Padding(4)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(51, 31)
        btnSearch.TabIndex = 1
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' txtIdentifier
        ' 
        txtIdentifier.Location = New Point(16, 64)
        txtIdentifier.Margin = New Padding(4)
        txtIdentifier.Name = "txtIdentifier"
        txtIdentifier.Size = New Size(549, 31)
        txtIdentifier.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(28, 34)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(215, 26)
        Label1.TabIndex = 7
        Label1.Text = "Script Identifier (First few letters)"
        ' 
        ' btn_Cancel
        ' 
        btn_Cancel.DialogResult = DialogResult.Cancel
        btn_Cancel.Location = New Point(505, 151)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New Size(93, 41)
        btn_Cancel.TabIndex = 8
        btn_Cancel.TabStop = False
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        ' 
        ' frmScriptLookUp
        ' 
        AcceptButton = btnSearch
        AutoScaleMode = AutoScaleMode.None
        CancelButton = btn_Cancel
        ClientSize = New Size(641, 424)
        ControlBox = False
        Controls.Add(Label1)
        Controls.Add(txtIdentifier)
        Controls.Add(btnSearch)
        Controls.Add(dgv)
        Controls.Add(ToolStrip1)
        Controls.Add(btn_Cancel)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmScriptLookUp"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Script Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgv, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtIdentifier As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Identifier As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Script As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
