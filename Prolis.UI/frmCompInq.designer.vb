<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCompInq
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompInq))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvMarking = New DataGridView()
        Gender = New DataGridViewTextBoxColumn()
        AgeFrom = New DataGridViewTextBoxColumn()
        AgeTo = New DataGridViewTextBoxColumn()
        txtDescription = New TextBox()
        Label7 = New Label()
        dgvRanges = New DataGridView()
        RangeType = New DataGridViewTextBoxColumn()
        Range = New DataGridViewTextBoxColumn()
        Flag = New DataGridViewTextBoxColumn()
        Sex = New DataGridViewTextBoxColumn()
        AgeRange = New DataGridViewTextBoxColumn()
        dgvConstituents = New DataGridView()
        TGID = New DataGridViewTextBoxColumn()
        TGType = New DataGridViewImageColumn()
        TGName = New DataGridViewTextBoxColumn()
        Label25 = New Label()
        Label26 = New Label()
        txtResultNote = New TextBox()
        Label28 = New Label()
        dgvProperties = New DataGridView()
        PropName = New DataGridViewTextBoxColumn()
        Value = New DataGridViewTextBoxColumn()
        Label29 = New Label()
        dgvTGs = New DataGridView()
        TestID = New DataGridViewTextBoxColumn()
        Logo = New DataGridViewImageColumn()
        TestName = New DataGridViewTextBoxColumn()
        Abbr = New DataGridViewTextBoxColumn()
        Description = New DataGridViewTextBoxColumn()
        Label1 = New Label()
        txtTerm = New TextBox()
        btnLook = New Button()
        Label2 = New Label()
        cmbPosition = New ComboBox()
        ToolStrip1.SuspendLayout()
        CType(dgvMarking, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvRanges, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvConstituents, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvProperties, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvTGs, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1480, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
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
        btnHelp.ForeColor = Color.DarkBlue
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' dgvMarking
        ' 
        dgvMarking.AllowUserToAddRows = False
        dgvMarking.AllowUserToDeleteRows = False
        dgvMarking.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvMarking.Columns.AddRange(New DataGridViewColumn() {Gender, AgeFrom, AgeTo})
        dgvMarking.Location = New Point(37, 925)
        dgvMarking.Margin = New Padding(5, 6, 5, 6)
        dgvMarking.Name = "dgvMarking"
        dgvMarking.ReadOnly = True
        dgvMarking.RowHeadersVisible = False
        dgvMarking.RowHeadersWidth = 62
        dgvMarking.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvMarking.Size = New Size(668, 254)
        dgvMarking.TabIndex = 11
        ' 
        ' Gender
        ' 
        Gender.FillWeight = 58.0F
        Gender.HeaderText = "Gender"
        Gender.MinimumWidth = 8
        Gender.Name = "Gender"
        Gender.ReadOnly = True
        Gender.SortMode = DataGridViewColumnSortMode.NotSortable
        Gender.Width = 58
        ' 
        ' AgeFrom
        ' 
        AgeFrom.FillWeight = 80.0F
        AgeFrom.HeaderText = "Age From"
        AgeFrom.MinimumWidth = 8
        AgeFrom.Name = "AgeFrom"
        AgeFrom.ReadOnly = True
        AgeFrom.SortMode = DataGridViewColumnSortMode.NotSortable
        AgeFrom.Width = 80
        ' 
        ' AgeTo
        ' 
        AgeTo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        AgeTo.FillWeight = 80.0F
        AgeTo.HeaderText = "Age To"
        AgeTo.MinimumWidth = 8
        AgeTo.Name = "AgeTo"
        AgeTo.ReadOnly = True
        AgeTo.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' txtDescription
        ' 
        txtDescription.Location = New Point(32, 767)
        txtDescription.Margin = New Padding(5, 6, 5, 6)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ReadOnly = True
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Size = New Size(661, 81)
        txtDescription.TabIndex = 18
        ' 
        ' Label7
        ' 
        Label7.ForeColor = Color.DarkBlue
        Label7.Location = New Point(50, 729)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(142, 33)
        Label7.TabIndex = 19
        Label7.Text = "Description"
        ' 
        ' dgvRanges
        ' 
        dgvRanges.AllowUserToAddRows = False
        dgvRanges.AllowUserToDeleteRows = False
        dgvRanges.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRanges.Columns.AddRange(New DataGridViewColumn() {RangeType, Range, Flag, Sex, AgeRange})
        dgvRanges.Location = New Point(738, 925)
        dgvRanges.Margin = New Padding(5, 6, 5, 6)
        dgvRanges.Name = "dgvRanges"
        dgvRanges.ReadOnly = True
        dgvRanges.RowHeadersVisible = False
        dgvRanges.RowHeadersWidth = 62
        dgvRanges.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRanges.Size = New Size(610, 254)
        dgvRanges.TabIndex = 49
        ' 
        ' RangeType
        ' 
        RangeType.HeaderText = "Type"
        RangeType.MinimumWidth = 8
        RangeType.Name = "RangeType"
        RangeType.ReadOnly = True
        RangeType.Width = 150
        ' 
        ' Range
        ' 
        Range.FillWeight = 80.0F
        Range.HeaderText = "Range"
        Range.MinimumWidth = 8
        Range.Name = "Range"
        Range.ReadOnly = True
        Range.Width = 80
        ' 
        ' Flag
        ' 
        Flag.FillWeight = 40.0F
        Flag.HeaderText = "Flag"
        Flag.MinimumWidth = 8
        Flag.Name = "Flag"
        Flag.ReadOnly = True
        Flag.Width = 40
        ' 
        ' Sex
        ' 
        Sex.FillWeight = 30.0F
        Sex.HeaderText = "Sex"
        Sex.MinimumWidth = 8
        Sex.Name = "Sex"
        Sex.ReadOnly = True
        Sex.Width = 30
        ' 
        ' AgeRange
        ' 
        AgeRange.FillWeight = 93.0F
        AgeRange.HeaderText = "Age Range"
        AgeRange.MinimumWidth = 8
        AgeRange.Name = "AgeRange"
        AgeRange.ReadOnly = True
        AgeRange.Width = 93
        ' 
        ' dgvConstituents
        ' 
        dgvConstituents.AllowUserToAddRows = False
        dgvConstituents.AllowUserToDeleteRows = False
        dgvConstituents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvConstituents.Columns.AddRange(New DataGridViewColumn() {TGID, TGType, TGName})
        dgvConstituents.Location = New Point(738, 925)
        dgvConstituents.Margin = New Padding(5, 6, 5, 6)
        dgvConstituents.Name = "dgvConstituents"
        dgvConstituents.ReadOnly = True
        dgvConstituents.RowHeadersVisible = False
        dgvConstituents.RowHeadersWidth = 62
        dgvConstituents.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvConstituents.Size = New Size(722, 254)
        dgvConstituents.TabIndex = 55
        ' 
        ' TGID
        ' 
        TGID.FillWeight = 80.0F
        TGID.HeaderText = "ID"
        TGID.MinimumWidth = 8
        TGID.Name = "TGID"
        TGID.ReadOnly = True
        TGID.Width = 80
        ' 
        ' TGType
        ' 
        TGType.FillWeight = 40.0F
        TGType.HeaderText = "Type"
        TGType.Image = CType(resources.GetObject("TGType.Image"), Image)
        TGType.MinimumWidth = 8
        TGType.Name = "TGType"
        TGType.ReadOnly = True
        TGType.Width = 40
        ' 
        ' TGName
        ' 
        TGName.FillWeight = 260.0F
        TGName.HeaderText = "Component"
        TGName.MinimumWidth = 8
        TGName.Name = "TGName"
        TGName.ReadOnly = True
        TGName.Width = 260
        ' 
        ' Label25
        ' 
        Label25.ForeColor = Color.DarkBlue
        Label25.Location = New Point(32, 875)
        Label25.Margin = New Padding(5, 0, 5, 0)
        Label25.Name = "Label25"
        Label25.Size = New Size(250, 27)
        Label25.TabIndex = 56
        Label25.Text = "Marking Exended"
        ' 
        ' Label26
        ' 
        Label26.ForeColor = Color.DarkBlue
        Label26.Location = New Point(740, 875)
        Label26.Margin = New Padding(5, 0, 5, 0)
        Label26.Name = "Label26"
        Label26.Size = New Size(222, 27)
        Label26.TabIndex = 57
        Label26.Text = "Ranges"
        ' 
        ' txtResultNote
        ' 
        txtResultNote.Location = New Point(32, 621)
        txtResultNote.Margin = New Padding(5, 6, 5, 6)
        txtResultNote.Multiline = True
        txtResultNote.Name = "txtResultNote"
        txtResultNote.ReadOnly = True
        txtResultNote.ScrollBars = ScrollBars.Vertical
        txtResultNote.Size = New Size(666, 81)
        txtResultNote.TabIndex = 58
        ' 
        ' Label28
        ' 
        Label28.ForeColor = Color.DarkBlue
        Label28.Location = New Point(50, 588)
        Label28.Margin = New Padding(5, 0, 5, 0)
        Label28.Name = "Label28"
        Label28.Size = New Size(142, 27)
        Label28.TabIndex = 59
        Label28.Text = "Result Note"
        ' 
        ' dgvProperties
        ' 
        dgvProperties.AllowUserToAddRows = False
        dgvProperties.AllowUserToDeleteRows = False
        dgvProperties.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        dgvProperties.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProperties.Columns.AddRange(New DataGridViewColumn() {PropName, Value})
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = SystemColors.Window
        DataGridViewCellStyle1.Font = New Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle1.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = DataGridViewTriState.True
        dgvProperties.DefaultCellStyle = DataGridViewCellStyle1
        dgvProperties.Location = New Point(738, 115)
        dgvProperties.Margin = New Padding(5, 6, 5, 6)
        dgvProperties.Name = "dgvProperties"
        dgvProperties.ReadOnly = True
        dgvProperties.RowHeadersVisible = False
        dgvProperties.RowHeadersWidth = 62
        DataGridViewCellStyle2.BackColor = Color.MintCream
        DataGridViewCellStyle2.ForeColor = Color.FromArgb(CByte(0), CByte(0), CByte(192))
        dgvProperties.RowsDefaultCellStyle = DataGridViewCellStyle2
        dgvProperties.Size = New Size(722, 737)
        dgvProperties.TabIndex = 60
        ' 
        ' PropName
        ' 
        PropName.FillWeight = 130.0F
        PropName.Frozen = True
        PropName.HeaderText = "Property"
        PropName.MinimumWidth = 8
        PropName.Name = "PropName"
        PropName.ReadOnly = True
        PropName.Width = 130
        ' 
        ' Value
        ' 
        Value.FillWeight = 284.0F
        Value.HeaderText = "Value"
        Value.MinimumWidth = 8
        Value.Name = "Value"
        Value.ReadOnly = True
        Value.SortMode = DataGridViewColumnSortMode.NotSortable
        Value.Width = 284
        ' 
        ' Label29
        ' 
        Label29.ForeColor = Color.DarkBlue
        Label29.Location = New Point(748, 79)
        Label29.Margin = New Padding(5, 0, 5, 0)
        Label29.Name = "Label29"
        Label29.Size = New Size(213, 27)
        Label29.TabIndex = 61
        Label29.Text = "Component Properties"
        ' 
        ' dgvTGs
        ' 
        dgvTGs.AllowUserToAddRows = False
        dgvTGs.AllowUserToDeleteRows = False
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvTGs.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        dgvTGs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTGs.Columns.AddRange(New DataGridViewColumn() {TestID, Logo, TestName, Abbr, Description})
        dgvTGs.Location = New Point(32, 196)
        dgvTGs.Margin = New Padding(5, 6, 5, 6)
        dgvTGs.Name = "dgvTGs"
        dgvTGs.ReadOnly = True
        dgvTGs.RowHeadersVisible = False
        dgvTGs.RowHeadersWidth = 62
        dgvTGs.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvTGs.Size = New Size(668, 362)
        dgvTGs.TabIndex = 62
        ' 
        ' TestID
        ' 
        TestID.FillWeight = 50.0F
        TestID.HeaderText = "ID"
        TestID.MaxInputLength = 10
        TestID.MinimumWidth = 8
        TestID.Name = "TestID"
        TestID.ReadOnly = True
        TestID.Width = 50
        ' 
        ' Logo
        ' 
        Logo.FillWeight = 30.0F
        Logo.HeaderText = ""
        Logo.MinimumWidth = 8
        Logo.Name = "Logo"
        Logo.ReadOnly = True
        Logo.Width = 30
        ' 
        ' TestName
        ' 
        TestName.FillWeight = 150.0F
        TestName.HeaderText = "Component Name"
        TestName.MinimumWidth = 8
        TestName.Name = "TestName"
        TestName.ReadOnly = True
        TestName.Width = 150
        ' 
        ' Abbr
        ' 
        Abbr.FillWeight = 50.0F
        Abbr.HeaderText = "Abbr"
        Abbr.MaxInputLength = 10
        Abbr.MinimumWidth = 8
        Abbr.Name = "Abbr"
        Abbr.ReadOnly = True
        Abbr.SortMode = DataGridViewColumnSortMode.NotSortable
        Abbr.Width = 50
        ' 
        ' Description
        ' 
        Description.FillWeight = 225.0F
        Description.HeaderText = "Description"
        Description.MinimumWidth = 8
        Description.Name = "Description"
        Description.ReadOnly = True
        Description.SortMode = DataGridViewColumnSortMode.NotSortable
        Description.Width = 225
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.SaddleBrown
        Label1.Location = New Point(27, 79)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(457, 25)
        Label1.TabIndex = 64
        Label1.Text = "ID (complete), Name, Abbr or Description  (even partial)"
        ' 
        ' txtTerm
        ' 
        txtTerm.Location = New Point(32, 117)
        txtTerm.Margin = New Padding(5, 6, 5, 6)
        txtTerm.Name = "txtTerm"
        txtTerm.Size = New Size(391, 31)
        txtTerm.TabIndex = 63
        ' 
        ' btnLook
        ' 
        btnLook.Image = CType(resources.GetObject("btnLook.Image"), Image)
        btnLook.Location = New Point(435, 110)
        btnLook.Margin = New Padding(5, 6, 5, 6)
        btnLook.Name = "btnLook"
        btnLook.Size = New Size(48, 46)
        btnLook.TabIndex = 65
        btnLook.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.SaddleBrown
        Label2.Location = New Point(512, 79)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(120, 25)
        Label2.TabIndex = 67
        Label2.Text = "Term position"
        ' 
        ' cmbPosition
        ' 
        cmbPosition.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPosition.FormattingEnabled = True
        cmbPosition.Items.AddRange(New Object() {"Contains", "Starts with"})
        cmbPosition.Location = New Point(498, 115)
        cmbPosition.Margin = New Padding(5, 6, 5, 6)
        cmbPosition.Name = "cmbPosition"
        cmbPosition.Size = New Size(199, 33)
        cmbPosition.TabIndex = 66
        ' 
        ' frmCompInq
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1480, 1206)
        Controls.Add(Label2)
        Controls.Add(cmbPosition)
        Controls.Add(btnLook)
        Controls.Add(Label1)
        Controls.Add(txtTerm)
        Controls.Add(dgvTGs)
        Controls.Add(Label29)
        Controls.Add(dgvProperties)
        Controls.Add(Label28)
        Controls.Add(txtResultNote)
        Controls.Add(Label26)
        Controls.Add(Label25)
        Controls.Add(dgvConstituents)
        Controls.Add(dgvRanges)
        Controls.Add(Label7)
        Controls.Add(txtDescription)
        Controls.Add(dgvMarking)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCompInq"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Component Inquiry"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvMarking, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvRanges, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvConstituents, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvProperties, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvTGs, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvMarking As System.Windows.Forms.DataGridView
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dgvRanges As System.Windows.Forms.DataGridView
    Friend WithEvents dgvConstituents As System.Windows.Forms.DataGridView
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtResultNote As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Gender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgeFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgeTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RangeType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Range As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Flag As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Sex As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AgeRange As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvProperties As System.Windows.Forms.DataGridView
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents TGID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TGType As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TGName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvTGs As System.Windows.Forms.DataGridView
    Friend WithEvents TestID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Logo As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents TestName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Abbr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents btnLook As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbPosition As System.Windows.Forms.ComboBox
    Friend WithEvents PropName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Value As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
