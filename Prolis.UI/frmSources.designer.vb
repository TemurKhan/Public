<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSources
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSources))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        txtSourceID = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtSource = New TextBox()
        Label3 = New Label()
        txtUoM = New TextBox()
        cmbMaterials = New ComboBox()
        Label4 = New Label()
        ToolTip1 = New ToolTip(components)
        btnMatLook = New Button()
        dgvSources = New DataGridView()
        SrcID = New DataGridViewTextBoxColumn()
        SrcName = New DataGridViewTextBoxColumn()
        UoM = New DataGridViewTextBoxColumn()
        MatID = New DataGridViewTextBoxColumn()
        MatName = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripSeparator1, btnSave, ToolStripSeparator2, btnDelete, ToolStripSeparator3, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(708, 34)
        ToolStrip1.TabIndex = 0
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(70, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(91, 29)
        btnCancel.Text = "Cancel"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' txtSourceID
        ' 
        txtSourceID.Location = New Point(28, 421)
        txtSourceID.Margin = New Padding(5, 6, 5, 6)
        txtSourceID.MaxLength = 4
        txtSourceID.Name = "txtSourceID"
        txtSourceID.ReadOnly = True
        txtSourceID.Size = New Size(179, 31)
        txtSourceID.TabIndex = 1
        txtSourceID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(23, 390)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(155, 25)
        Label1.TabIndex = 2
        Label1.Text = "Source ID"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.MidnightBlue
        Label2.Location = New Point(220, 390)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(183, 25)
        Label2.TabIndex = 4
        Label2.Text = "Source Name"
        ' 
        ' txtSource
        ' 
        txtSource.Location = New Point(225, 421)
        txtSource.Margin = New Padding(5, 6, 5, 6)
        txtSource.MaxLength = 60
        txtSource.Name = "txtSource"
        txtSource.Size = New Size(461, 31)
        txtSource.TabIndex = 3
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.MidnightBlue
        Label3.Location = New Point(23, 465)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(113, 25)
        Label3.TabIndex = 8
        Label3.Text = "UoM"
        ' 
        ' txtUoM
        ' 
        txtUoM.Location = New Point(28, 496)
        txtUoM.Margin = New Padding(5, 6, 5, 6)
        txtUoM.MaxLength = 25
        txtUoM.Name = "txtUoM"
        txtUoM.Size = New Size(179, 31)
        txtUoM.TabIndex = 7
        ' 
        ' cmbMaterials
        ' 
        cmbMaterials.FormattingEnabled = True
        cmbMaterials.Items.AddRange(New Object() {"Biopsy", "Bone Marrow", "Cheese", "Culture Swab", "Metal", "Milk", "Mud", "Plasma", "Red Cells", "Sand", "Semen", "Serum", "Slide", "Urine", "Water", "Whole Blood"})
        cmbMaterials.Location = New Point(225, 496)
        cmbMaterials.Margin = New Padding(5, 6, 5, 6)
        cmbMaterials.Name = "cmbMaterials"
        cmbMaterials.Size = New Size(394, 33)
        cmbMaterials.TabIndex = 9
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.MidnightBlue
        Label4.Location = New Point(220, 465)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(188, 25)
        Label4.TabIndex = 11
        Label4.Text = "Material Name"
        ' 
        ' btnMatLook
        ' 
        btnMatLook.Image = CType(resources.GetObject("btnMatLook.Image"), Image)
        btnMatLook.Location = New Point(638, 488)
        btnMatLook.Margin = New Padding(5, 6, 5, 6)
        btnMatLook.Name = "btnMatLook"
        btnMatLook.Size = New Size(50, 50)
        btnMatLook.TabIndex = 10
        ToolTip1.SetToolTip(btnMatLook, "Material Lookup")
        btnMatLook.UseVisualStyleBackColor = True
        ' 
        ' dgvSources
        ' 
        dgvSources.AllowUserToAddRows = False
        dgvSources.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvSources.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvSources.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSources.Columns.AddRange(New DataGridViewColumn() {SrcID, SrcName, UoM, MatID, MatName})
        dgvSources.Location = New Point(28, 67)
        dgvSources.Margin = New Padding(5, 6, 5, 6)
        dgvSources.Name = "dgvSources"
        dgvSources.ReadOnly = True
        dgvSources.RowHeadersVisible = False
        dgvSources.RowHeadersWidth = 62
        dgvSources.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvSources.Size = New Size(658, 304)
        dgvSources.TabIndex = 12
        ' 
        ' SrcID
        ' 
        SrcID.FillWeight = 40F
        SrcID.HeaderText = "ID"
        SrcID.MaxInputLength = 4
        SrcID.MinimumWidth = 8
        SrcID.Name = "SrcID"
        SrcID.ReadOnly = True
        SrcID.Width = 63
        ' 
        ' SrcName
        ' 
        SrcName.FillWeight = 150F
        SrcName.HeaderText = "Name"
        SrcName.MaxInputLength = 60
        SrcName.MinimumWidth = 8
        SrcName.Name = "SrcName"
        SrcName.ReadOnly = True
        SrcName.Width = 237
        ' 
        ' UoM
        ' 
        UoM.HeaderText = "UoM"
        UoM.MaxInputLength = 25
        UoM.MinimumWidth = 8
        UoM.Name = "UoM"
        UoM.ReadOnly = True
        UoM.SortMode = DataGridViewColumnSortMode.NotSortable
        UoM.Width = 158
        ' 
        ' MatID
        ' 
        MatID.HeaderText = "Mat ID"
        MatID.MinimumWidth = 8
        MatID.Name = "MatID"
        MatID.ReadOnly = True
        MatID.Visible = False
        MatID.Width = 150
        ' 
        ' MatName
        ' 
        MatName.FillWeight = 125F
        MatName.HeaderText = "Material"
        MatName.MaxInputLength = 60
        MatName.MinimumWidth = 8
        MatName.Name = "MatName"
        MatName.ReadOnly = True
        MatName.SortMode = DataGridViewColumnSortMode.NotSortable
        MatName.Width = 197
        ' 
        ' frmSources
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(708, 560)
        Controls.Add(dgvSources)
        Controls.Add(Label4)
        Controls.Add(btnMatLook)
        Controls.Add(cmbMaterials)
        Controls.Add(Label3)
        Controls.Add(txtUoM)
        Controls.Add(Label2)
        Controls.Add(txtSource)
        Controls.Add(Label1)
        Controls.Add(txtSourceID)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmSources"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Source Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtSourceID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUoM As System.Windows.Forms.TextBox
    Friend WithEvents cmbMaterials As System.Windows.Forms.ComboBox
    Friend WithEvents btnMatLook As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents dgvSources As System.Windows.Forms.DataGridView
    Friend WithEvents SrcID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SrcName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents UoM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MatID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents MatName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
