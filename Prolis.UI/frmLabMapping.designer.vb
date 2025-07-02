<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLabMapping
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLabMapping))
        ToolStrip1 = New ToolStrip()
        btnProcess = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvFieldMap = New DataGridView()
        Include = New DataGridViewCheckBoxColumn()
        FileField = New DataGridViewTextBoxColumn()
        Map = New DataGridViewComboBoxColumn()
        Label2 = New Label()
        pb = New ProgressBar()
        btnSel = New Button()
        btnDesel = New Button()
        btnLoad = New Button()
        btnOpen = New Button()
        txtFile = New TextBox()
        Label1 = New Label()
        OpenFileDialog1 = New OpenFileDialog()
        txtLabID = New TextBox()
        Label3 = New Label()
        cmbDelim = New ComboBox()
        chkDelExist = New CheckBox()
        ToolStrip1.SuspendLayout()
        CType(dgvFieldMap, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnProcess, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(792, 34)
        ToolStrip1.TabIndex = 8
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnProcess
        ' 
        btnProcess.ForeColor = Color.DarkBlue
        btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), Image)
        btnProcess.ImageTransparentColor = Color.Magenta
        btnProcess.Name = "btnProcess"
        btnProcess.Size = New Size(100, 29)
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
        ' dgvFieldMap
        ' 
        dgvFieldMap.AllowUserToAddRows = False
        dgvFieldMap.AllowUserToDeleteRows = False
        dgvFieldMap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvFieldMap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFieldMap.Columns.AddRange(New DataGridViewColumn() {Include, FileField, Map})
        dgvFieldMap.Location = New Point(25, 238)
        dgvFieldMap.Margin = New Padding(5, 6, 5, 6)
        dgvFieldMap.Name = "dgvFieldMap"
        dgvFieldMap.RowHeadersVisible = False
        dgvFieldMap.RowHeadersWidth = 62
        dgvFieldMap.Size = New Size(678, 323)
        dgvFieldMap.TabIndex = 27
        ' 
        ' Include
        ' 
        Include.FillWeight = 30F
        Include.HeaderText = ""
        Include.MinimumWidth = 8
        Include.Name = "Include"
        ' 
        ' FileField
        ' 
        FileField.FillWeight = 150F
        FileField.HeaderText = "File Field"
        FileField.MinimumWidth = 8
        FileField.Name = "FileField"
        FileField.ReadOnly = True
        FileField.Resizable = DataGridViewTriState.True
        FileField.SortMode = DataGridViewColumnSortMode.NotSortable
        ' 
        ' Map
        ' 
        Map.FillWeight = 210F
        Map.HeaderText = "Mapping"
        Map.Items.AddRange(New Object() {"Prolis ID", "Lab Order ID", "Lab Result ID"})
        Map.MinimumWidth = 8
        Map.Name = "Map"
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.Maroon
        Label2.Location = New Point(423, 83)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(107, 23)
        Label2.TabIndex = 26
        Label2.Text = "Lab ID:"
        Label2.TextAlign = ContentAlignment.TopRight
        ' 
        ' pb
        ' 
        pb.Location = New Point(20, 592)
        pb.Margin = New Padding(5, 6, 5, 6)
        pb.Name = "pb"
        pb.Size = New Size(747, 38)
        pb.TabIndex = 25
        ' 
        ' btnSel
        ' 
        btnSel.Image = CType(resources.GetObject("btnSel.Image"), Image)
        btnSel.Location = New Point(717, 462)
        btnSel.Margin = New Padding(5, 6, 5, 6)
        btnSel.Name = "btnSel"
        btnSel.Size = New Size(53, 50)
        btnSel.TabIndex = 24
        btnSel.UseVisualStyleBackColor = True
        ' 
        ' btnDesel
        ' 
        btnDesel.Image = CType(resources.GetObject("btnDesel.Image"), Image)
        btnDesel.Location = New Point(717, 400)
        btnDesel.Margin = New Padding(5, 6, 5, 6)
        btnDesel.Name = "btnDesel"
        btnDesel.Size = New Size(53, 50)
        btnDesel.TabIndex = 23
        btnDesel.UseVisualStyleBackColor = True
        ' 
        ' btnLoad
        ' 
        btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), Image)
        btnLoad.Location = New Point(717, 238)
        btnLoad.Margin = New Padding(5, 6, 5, 6)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(53, 50)
        btnLoad.TabIndex = 22
        btnLoad.UseVisualStyleBackColor = True
        ' 
        ' btnOpen
        ' 
        btnOpen.Image = CType(resources.GetObject("btnOpen.Image"), Image)
        btnOpen.Location = New Point(718, 181)
        btnOpen.Margin = New Padding(5, 6, 5, 6)
        btnOpen.Name = "btnOpen"
        btnOpen.Size = New Size(53, 50)
        btnOpen.TabIndex = 21
        btnOpen.UseVisualStyleBackColor = True
        ' 
        ' txtFile
        ' 
        txtFile.Location = New Point(20, 188)
        txtFile.Margin = New Padding(5, 6, 5, 6)
        txtFile.Name = "txtFile"
        txtFile.ReadOnly = True
        txtFile.Size = New Size(681, 31)
        txtFile.TabIndex = 20
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(20, 154)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(472, 29)
        Label1.TabIndex = 19
        Label1.Text = "CSV, Pipe or TAB DELIMITED (CSV/TXT File only)"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' txtLabID
        ' 
        txtLabID.Location = New Point(540, 75)
        txtLabID.Margin = New Padding(5, 6, 5, 6)
        txtLabID.Name = "txtLabID"
        txtLabID.ReadOnly = True
        txtLabID.Size = New Size(161, 31)
        txtLabID.TabIndex = 28
        txtLabID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(20, 81)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(90, 29)
        Label3.TabIndex = 29
        Label3.Text = "Delimiter:"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' cmbDelim
        ' 
        cmbDelim.FormattingEnabled = True
        cmbDelim.Items.AddRange(New Object() {"Comma ( , )", "Pipe ( | )", "Tab (" & vbTab & ")"})
        cmbDelim.Location = New Point(120, 77)
        cmbDelim.Margin = New Padding(5, 6, 5, 6)
        cmbDelim.Name = "cmbDelim"
        cmbDelim.Size = New Size(274, 33)
        cmbDelim.TabIndex = 30
        ' 
        ' chkDelExist
        ' 
        chkDelExist.AutoSize = True
        chkDelExist.CheckAlign = ContentAlignment.MiddleRight
        chkDelExist.Location = New Point(540, 144)
        chkDelExist.Margin = New Padding(5, 6, 5, 6)
        chkDelExist.Name = "chkDelExist"
        chkDelExist.Size = New Size(153, 29)
        chkDelExist.TabIndex = 31
        chkDelExist.Text = "Delete existing"
        chkDelExist.UseVisualStyleBackColor = True
        ' 
        ' frmLabMapping
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(792, 654)
        Controls.Add(chkDelExist)
        Controls.Add(cmbDelim)
        Controls.Add(Label3)
        Controls.Add(txtLabID)
        Controls.Add(dgvFieldMap)
        Controls.Add(Label2)
        Controls.Add(pb)
        Controls.Add(btnSel)
        Controls.Add(btnDesel)
        Controls.Add(btnLoad)
        Controls.Add(btnOpen)
        Controls.Add(txtFile)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmLabMapping"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Lab Mapping"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvFieldMap, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnProcess As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvFieldMap As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pb As System.Windows.Forms.ProgressBar
    Friend WithEvents btnSel As System.Windows.Forms.Button
    Friend WithEvents btnDesel As System.Windows.Forms.Button
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Include As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents FileField As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Map As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents txtLabID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbDelim As System.Windows.Forms.ComboBox
    Friend WithEvents chkDelExist As System.Windows.Forms.CheckBox

End Class
