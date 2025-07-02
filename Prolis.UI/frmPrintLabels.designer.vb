<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPrintLabels
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintLabels))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        grpAccRange = New GroupBox()
        Label4 = New Label()
        Label9 = New Label()
        cmbDataType = New ComboBox()
        lblTo = New Label()
        Label5 = New Label()
        txtAccTo = New TextBox()
        txtAccFrom = New TextBox()
        btnClear = New Button()
        dgvDiscrete = New DataGridView()
        Accessions = New DataGridViewTextBoxColumn()
        txtQty = New TextBox()
        Label1 = New Label()
        gbSources = New GroupBox()
        dgvSources = New DataGridView()
        SID = New DataGridViewTextBoxColumn()
        Chk = New DataGridViewCheckBoxColumn()
        Source = New DataGridViewTextBoxColumn()
        QTY = New DataGridViewTextBoxColumn()
        ChkRemotePrint = New CheckBox()
        BradyPnal = New Panel()
        txtP = New TextBox()
        txtInitial = New TextBox()
        dtpDate = New DateTimePicker()
        LabelP = New Label()
        dateLabel = New Label()
        initlabel = New Label()
        ToolStrip1.SuspendLayout()
        grpAccRange.SuspendLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).BeginInit()
        gbSources.SuspendLayout()
        CType(dgvSources, ComponentModel.ISupportInitialize).BeginInit()
        BradyPnal.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(679, 34)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.Enabled = False
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(96, 29)
        btnProcess.Text = "Process"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnCancel
        ' 
        btnCancel.ForeColor = Color.DarkBlue
        btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), Image)
        btnCancel.ImageTransparentColor = Color.Magenta
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(87, 29)
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
        btnHelp.Size = New Size(73, 29)
        btnHelp.Text = "Help"
        ' 
        ' grpAccRange
        ' 
        grpAccRange.Controls.Add(Label4)
        grpAccRange.Controls.Add(Label9)
        grpAccRange.Controls.Add(cmbDataType)
        grpAccRange.Controls.Add(lblTo)
        grpAccRange.Controls.Add(Label5)
        grpAccRange.Controls.Add(txtAccTo)
        grpAccRange.Controls.Add(txtAccFrom)
        grpAccRange.Location = New Point(20, 83)
        grpAccRange.Margin = New Padding(5, 6, 5, 6)
        grpAccRange.Name = "grpAccRange"
        grpAccRange.Padding = New Padding(5, 6, 5, 6)
        grpAccRange.Size = New Size(376, 350)
        grpAccRange.TabIndex = 6
        grpAccRange.TabStop = False
        grpAccRange.Text = "Range"
        ' 
        ' Label4
        ' 
        Label4.BackColor = SystemColors.Control
        Label4.Image = My.Resources.Resources.paste
        Label4.Location = New Point(322, 142)
        Label4.Margin = New Padding(4, 0, 4, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(41, 48)
        Label4.TabIndex = 85
        Label4.Text = "      "
        ' 
        ' Label9
        ' 
        Label9.BackColor = SystemColors.Control
        Label9.Image = My.Resources.Resources.paste
        Label9.Location = New Point(325, 77)
        Label9.Margin = New Padding(4, 0, 4, 0)
        Label9.Name = "Label9"
        Label9.Size = New Size(41, 48)
        Label9.TabIndex = 84
        Label9.Text = "      "
        ' 
        ' cmbDataType
        ' 
        cmbDataType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDataType.FormattingEnabled = True
        cmbDataType.Items.AddRange(New Object() {"Accession", "Requisition", "Free Form"})
        cmbDataType.Location = New Point(110, 31)
        cmbDataType.Margin = New Padding(5, 6, 5, 6)
        cmbDataType.Name = "cmbDataType"
        cmbDataType.Size = New Size(209, 33)
        cmbDataType.TabIndex = 19
        ' 
        ' lblTo
        ' 
        lblTo.ForeColor = Color.DarkBlue
        lblTo.Location = New Point(10, 147)
        lblTo.Margin = New Padding(5, 0, 5, 0)
        lblTo.Name = "lblTo"
        lblTo.Size = New Size(90, 34)
        lblTo.TabIndex = 17
        lblTo.Text = "To"
        lblTo.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(10, 86)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(90, 34)
        Label5.TabIndex = 16
        Label5.Text = "From"
        Label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtAccTo
        ' 
        txtAccTo.Location = New Point(110, 147)
        txtAccTo.Margin = New Padding(5, 6, 5, 6)
        txtAccTo.MaxLength = 16
        txtAccTo.Name = "txtAccTo"
        txtAccTo.Size = New Size(209, 31)
        txtAccTo.TabIndex = 2
        txtAccTo.TextAlign = HorizontalAlignment.Center
        ' 
        ' txtAccFrom
        ' 
        txtAccFrom.Location = New Point(110, 86)
        txtAccFrom.Margin = New Padding(5, 6, 5, 6)
        txtAccFrom.MaxLength = 16
        txtAccFrom.Name = "txtAccFrom"
        txtAccFrom.Size = New Size(209, 31)
        txtAccFrom.TabIndex = 1
        txtAccFrom.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnClear
        ' 
        btnClear.Enabled = False
        btnClear.ForeColor = Color.Red
        btnClear.Image = CType(resources.GetObject("btnClear.Image"), Image)
        btnClear.Location = New Point(519, 805)
        btnClear.Margin = New Padding(5, 6, 5, 6)
        btnClear.Name = "btnClear"
        btnClear.Size = New Size(129, 44)
        btnClear.TabIndex = 13
        btnClear.Text = "Clear List"
        btnClear.TextAlign = ContentAlignment.MiddleRight
        btnClear.TextImageRelation = TextImageRelation.ImageBeforeText
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' dgvDiscrete
        ' 
        dgvDiscrete.AllowUserToAddRows = False
        dgvDiscrete.AllowUserToDeleteRows = False
        dgvDiscrete.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvDiscrete.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvDiscrete.Columns.AddRange(New DataGridViewColumn() {Accessions})
        dgvDiscrete.Location = New Point(406, 98)
        dgvDiscrete.Margin = New Padding(5, 6, 5, 6)
        dgvDiscrete.Name = "dgvDiscrete"
        dgvDiscrete.RowHeadersVisible = False
        dgvDiscrete.RowHeadersWidth = 51
        dgvDiscrete.Size = New Size(241, 662)
        dgvDiscrete.TabIndex = 12
        ' 
        ' Accessions
        ' 
        Accessions.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Accessions.HeaderText = "Discrete Values"
        Accessions.MinimumWidth = 6
        Accessions.Name = "Accessions"
        ' 
        ' txtQty
        ' 
        txtQty.Location = New Point(400, 811)
        txtQty.Margin = New Padding(5, 6, 5, 6)
        txtQty.MaxLength = 2
        txtQty.Name = "txtQty"
        txtQty.Size = New Size(94, 31)
        txtQty.TabIndex = 14
        txtQty.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(400, 770)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(96, 34)
        Label1.TabIndex = 18
        Label1.Text = "Label(s)"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' gbSources
        ' 
        gbSources.Controls.Add(dgvSources)
        gbSources.Location = New Point(20, 450)
        gbSources.Margin = New Padding(5, 6, 5, 6)
        gbSources.Name = "gbSources"
        gbSources.Padding = New Padding(5, 6, 5, 6)
        gbSources.Size = New Size(376, 341)
        gbSources.TabIndex = 19
        gbSources.TabStop = False
        gbSources.Text = "Sources"
        ' 
        ' dgvSources
        ' 
        dgvSources.AllowUserToAddRows = False
        dgvSources.AllowUserToDeleteRows = False
        dgvSources.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvSources.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvSources.Columns.AddRange(New DataGridViewColumn() {SID, Chk, Source, QTY})
        dgvSources.Location = New Point(10, 36)
        dgvSources.Margin = New Padding(5, 6, 5, 6)
        dgvSources.Name = "dgvSources"
        dgvSources.RowHeadersVisible = False
        dgvSources.RowHeadersWidth = 51
        dgvSources.Size = New Size(356, 275)
        dgvSources.TabIndex = 16
        ' 
        ' SID
        ' 
        SID.HeaderText = "SID"
        SID.MinimumWidth = 6
        SID.Name = "SID"
        SID.SortMode = DataGridViewColumnSortMode.NotSortable
        SID.Visible = False
        ' 
        ' Chk
        ' 
        Chk.FillWeight = 30F
        Chk.HeaderText = ""
        Chk.MinimumWidth = 6
        Chk.Name = "Chk"
        ' 
        ' Source
        ' 
        Source.FillWeight = 128F
        Source.HeaderText = "Sp. Contents"
        Source.MinimumWidth = 6
        Source.Name = "Source"
        Source.ReadOnly = True
        Source.Resizable = DataGridViewTriState.False
        Source.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' QTY
        ' 
        QTY.FillWeight = 36F
        QTY.HeaderText = "QTY"
        QTY.MinimumWidth = 6
        QTY.Name = "QTY"
        QTY.Resizable = DataGridViewTriState.False
        QTY.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' ChkRemotePrint
        ' 
        ChkRemotePrint.AutoSize = True
        ChkRemotePrint.Location = New Point(29, 773)
        ChkRemotePrint.Margin = New Padding(4, 5, 4, 5)
        ChkRemotePrint.Name = "ChkRemotePrint"
        ChkRemotePrint.Size = New Size(188, 29)
        ChkRemotePrint.TabIndex = 20
        ChkRemotePrint.Text = "Prolis Remote Print"
        ChkRemotePrint.UseVisualStyleBackColor = True
        ' 
        ' BradyPnal
        ' 
        BradyPnal.Controls.Add(txtP)
        BradyPnal.Controls.Add(txtInitial)
        BradyPnal.Controls.Add(dtpDate)
        BradyPnal.Controls.Add(LabelP)
        BradyPnal.Controls.Add(dateLabel)
        BradyPnal.Controls.Add(initlabel)
        BradyPnal.Location = New Point(20, 269)
        BradyPnal.Margin = New Padding(4, 5, 4, 5)
        BradyPnal.Name = "BradyPnal"
        BradyPnal.Size = New Size(376, 164)
        BradyPnal.TabIndex = 21
        ' 
        ' txtP
        ' 
        txtP.Location = New Point(110, 117)
        txtP.Margin = New Padding(5, 6, 5, 6)
        txtP.Name = "txtP"
        txtP.Size = New Size(209, 31)
        txtP.TabIndex = 27
        ' 
        ' txtInitial
        ' 
        txtInitial.Location = New Point(110, 17)
        txtInitial.Margin = New Padding(5, 6, 5, 6)
        txtInitial.Name = "txtInitial"
        txtInitial.Size = New Size(209, 31)
        txtInitial.TabIndex = 25
        ' 
        ' dtpDate
        ' 
        dtpDate.Format = DateTimePickerFormat.Short
        dtpDate.Location = New Point(110, 67)
        dtpDate.Margin = New Padding(5, 6, 5, 6)
        dtpDate.Name = "dtpDate"
        dtpDate.Size = New Size(209, 31)
        dtpDate.TabIndex = 26
        ' 
        ' LabelP
        ' 
        LabelP.ForeColor = Color.DarkBlue
        LabelP.Location = New Point(48, 117)
        LabelP.Margin = New Padding(5, 0, 5, 0)
        LabelP.Name = "LabelP"
        LabelP.Size = New Size(52, 34)
        LabelP.TabIndex = 22
        LabelP.Text = "P?"
        LabelP.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' dateLabel
        ' 
        dateLabel.ForeColor = Color.DarkBlue
        dateLabel.Location = New Point(48, 67)
        dateLabel.Margin = New Padding(5, 0, 5, 0)
        dateLabel.Name = "dateLabel"
        dateLabel.Size = New Size(52, 34)
        dateLabel.TabIndex = 23
        dateLabel.Text = "Date"
        dateLabel.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' initlabel
        ' 
        initlabel.ForeColor = Color.DarkBlue
        initlabel.Location = New Point(44, 17)
        initlabel.Margin = New Padding(5, 0, 5, 0)
        initlabel.Name = "initlabel"
        initlabel.Size = New Size(56, 34)
        initlabel.TabIndex = 24
        initlabel.Text = "Initial"
        initlabel.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' frmPrintLabels
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(679, 873)
        Controls.Add(BradyPnal)
        Controls.Add(ChkRemotePrint)
        Controls.Add(gbSources)
        Controls.Add(Label1)
        Controls.Add(txtQty)
        Controls.Add(btnClear)
        Controls.Add(dgvDiscrete)
        Controls.Add(grpAccRange)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrintLabels"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Print Labels"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        grpAccRange.ResumeLayout(False)
        grpAccRange.PerformLayout()
        CType(dgvDiscrete, ComponentModel.ISupportInitialize).EndInit()
        gbSources.ResumeLayout(False)
        CType(dgvSources, ComponentModel.ISupportInitialize).EndInit()
        BradyPnal.ResumeLayout(False)
        BradyPnal.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpAccRange As System.Windows.Forms.GroupBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccTo As System.Windows.Forms.TextBox
    Friend WithEvents txtAccFrom As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents dgvDiscrete As System.Windows.Forms.DataGridView
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbSources As System.Windows.Forms.GroupBox
    Friend WithEvents Accessions As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvSources As System.Windows.Forms.DataGridView
    Friend WithEvents SID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Chk As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Source As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbDataType As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ChkRemotePrint As System.Windows.Forms.CheckBox
    Friend WithEvents BradyPnal As System.Windows.Forms.Panel
    Friend WithEvents txtP As System.Windows.Forms.TextBox
    Friend WithEvents txtInitial As System.Windows.Forms.TextBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents LabelP As System.Windows.Forms.Label
    Friend WithEvents dateLabel As System.Windows.Forms.Label
    Friend WithEvents initlabel As System.Windows.Forms.Label
End Class
