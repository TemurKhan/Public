<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdhocQuery
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdhocQuery))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label1 = New Label()
        ToolStrip1 = New ToolStrip()
        chkGridText = New ToolStripButton()
        ToolStripSeparator5 = New ToolStripSeparator()
        btnExecute = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnPrint = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        btnExport = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvResult = New DataGridView()
        txtResult = New TextBox()
        Label2 = New Label()
        txtSQL = New RichTextBox()
        StatusStrip1 = New StatusStrip()
        lblStatus = New ToolStripStatusLabel()
        PB = New ToolStripProgressBar()
        PrintDocument1 = New Printing.PrintDocument()
        SaveFileDialog1 = New SaveFileDialog()
        btnScriptLookUp = New Button()
        Label3 = New Label()
        txtScriptID = New TextBox()
        btnSave = New Button()
        CMS = New ContextMenuStrip(components)
        mnuCopy = New ToolStripMenuItem()
        mnuExportCSV = New ToolStripMenuItem()
        mnuExportTAB = New ToolStripMenuItem()
        ToolStrip1.SuspendLayout()
        CType(dgvResult, ComponentModel.ISupportInitialize).BeginInit()
        StatusStrip1.SuspendLayout()
        CMS.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.Red
        Label1.Location = New Point(382, 77)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(623, 48)
        Label1.TabIndex = 0
        Label1.Text = "WARNING:  Familiarity with SQL is required, to use this functionality"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkGridText, ToolStripSeparator5, btnExecute, ToolStripSeparator2, btnPrint, ToolStripSeparator3, btnExport, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1362, 34)
        ToolStrip1.TabIndex = 1
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkGridText
        ' 
        chkGridText.Image = CType(resources.GetObject("chkGridText.Image"), Image)
        chkGridText.ImageTransparentColor = Color.Magenta
        chkGridText.Name = "chkGridText"
        chkGridText.Size = New Size(73, 29)
        chkGridText.Text = "Grid"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(6, 34)
        ' 
        ' btnExecute
        ' 
        btnExecute.Enabled = False
        btnExecute.Image = CType(resources.GetObject("btnExecute.Image"), Image)
        btnExecute.ImageTransparentColor = Color.Magenta
        btnExecute.Name = "btnExecute"
        btnExecute.Size = New Size(99, 29)
        btnExecute.Text = "Execute"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(76, 29)
        btnPrint.Text = "Print"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' btnExport
        ' 
        btnExport.Enabled = False
        btnExport.Image = CType(resources.GetObject("btnExport.Image"), Image)
        btnExport.ImageTransparentColor = Color.Magenta
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(126, 29)
        btnExport.Text = "Export Text"
        btnExport.ToolTipText = "Export Tab delimited Text"
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
        ' dgvResult
        ' 
        dgvResult.AllowUserToAddRows = False
        dgvResult.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FloralWhite
        dgvResult.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvResult.Location = New Point(23, 644)
        dgvResult.Margin = New Padding(5, 6, 5, 6)
        dgvResult.Name = "dgvResult"
        dgvResult.ReadOnly = True
        dgvResult.RowHeadersVisible = False
        dgvResult.RowHeadersWidth = 62
        dgvResult.Size = New Size(1338, 617)
        dgvResult.TabIndex = 2
        ' 
        ' txtResult
        ' 
        txtResult.BackColor = SystemColors.ButtonHighlight
        txtResult.Location = New Point(23, 644)
        txtResult.Margin = New Padding(5, 6, 5, 6)
        txtResult.Multiline = True
        txtResult.Name = "txtResult"
        txtResult.ReadOnly = True
        txtResult.ScrollBars = ScrollBars.Both
        txtResult.Size = New Size(1336, 614)
        txtResult.TabIndex = 4
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(42, 104)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(207, 25)
        Label2.TabIndex = 5
        Label2.Text = "SQL Statement (No DDL)"
        ' 
        ' txtSQL
        ' 
        txtSQL.Font = New Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtSQL.Location = New Point(25, 140)
        txtSQL.Margin = New Padding(5, 6, 5, 6)
        txtSQL.Name = "txtSQL"
        txtSQL.Size = New Size(1334, 402)
        txtSQL.TabIndex = 6
        txtSQL.Text = ""
        ' 
        ' StatusStrip1
        ' 
        StatusStrip1.ImageScalingSize = New Size(24, 24)
        StatusStrip1.Items.AddRange(New ToolStripItem() {lblStatus, PB})
        StatusStrip1.Location = New Point(0, 1259)
        StatusStrip1.Name = "StatusStrip1"
        StatusStrip1.Padding = New Padding(2, 0, 23, 0)
        StatusStrip1.Size = New Size(1362, 35)
        StatusStrip1.TabIndex = 7
        StatusStrip1.Text = "StatusStrip1"
        ' 
        ' lblStatus
        ' 
        lblStatus.AutoSize = False
        lblStatus.Name = "lblStatus"
        lblStatus.Size = New Size(650, 28)
        ' 
        ' PB
        ' 
        PB.Name = "PB"
        PB.Size = New Size(167, 27)
        ' 
        ' PrintDocument1
        ' 
        ' 
        ' btnScriptLookUp
        ' 
        btnScriptLookUp.Image = CType(resources.GetObject("btnScriptLookUp.Image"), Image)
        btnScriptLookUp.Location = New Point(1135, 71)
        btnScriptLookUp.Margin = New Padding(5, 6, 5, 6)
        btnScriptLookUp.Name = "btnScriptLookUp"
        btnScriptLookUp.Size = New Size(227, 58)
        btnScriptLookUp.TabIndex = 8
        btnScriptLookUp.Text = "Script Look Up"
        btnScriptLookUp.TextAlign = ContentAlignment.MiddleRight
        btnScriptLookUp.TextImageRelation = TextImageRelation.ImageBeforeText
        btnScriptLookUp.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(218, 581)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(133, 25)
        Label3.TabIndex = 9
        Label3.Text = "Script Identifier"
        Label3.TextAlign = ContentAlignment.TopRight
        ' 
        ' txtScriptID
        ' 
        txtScriptID.Location = New Point(362, 575)
        txtScriptID.Margin = New Padding(5, 6, 5, 6)
        txtScriptID.MaxLength = 100
        txtScriptID.Name = "txtScriptID"
        txtScriptID.Size = New Size(647, 31)
        txtScriptID.TabIndex = 10
        ' 
        ' btnSave
        ' 
        btnSave.Enabled = False
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.Location = New Point(1022, 569)
        btnSave.Margin = New Padding(5, 6, 5, 6)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(147, 46)
        btnSave.TabIndex = 11
        btnSave.Text = "SAVE"
        btnSave.TextAlign = ContentAlignment.MiddleRight
        btnSave.TextImageRelation = TextImageRelation.ImageBeforeText
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' CMS
        ' 
        CMS.ImageScalingSize = New Size(24, 24)
        CMS.Items.AddRange(New ToolStripItem() {mnuCopy, mnuExportCSV, mnuExportTAB})
        CMS.Name = "CMS"
        CMS.Size = New Size(223, 100)
        ' 
        ' mnuCopy
        ' 
        mnuCopy.Name = "mnuCopy"
        mnuCopy.Size = New Size(222, 32)
        mnuCopy.Text = "Copy"
        ' 
        ' mnuExportCSV
        ' 
        mnuExportCSV.Name = "mnuExportCSV"
        mnuExportCSV.Size = New Size(222, 32)
        mnuExportCSV.Text = "Export to CSV file"
        ' 
        ' mnuExportTAB
        ' 
        mnuExportTAB.Name = "mnuExportTAB"
        mnuExportTAB.Size = New Size(222, 32)
        mnuExportTAB.Text = "Export to Tab file"
        ' 
        ' frmAdhocQuery
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1362, 1294)
        Controls.Add(btnSave)
        Controls.Add(txtScriptID)
        Controls.Add(Label3)
        Controls.Add(btnScriptLookUp)
        Controls.Add(dgvResult)
        Controls.Add(StatusStrip1)
        Controls.Add(txtSQL)
        Controls.Add(Label2)
        Controls.Add(txtResult)
        Controls.Add(ToolStrip1)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MaximumSize = New Size(1384, 1350)
        MinimizeBox = False
        MinimumSize = New Size(1384, 1350)
        Name = "frmAdhocQuery"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Adhoc SQL Query"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvResult, ComponentModel.ISupportInitialize).EndInit()
        StatusStrip1.ResumeLayout(False)
        StatusStrip1.PerformLayout()
        CMS.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnExecute As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvResult As System.Windows.Forms.DataGridView
    Friend WithEvents txtResult As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSQL As System.Windows.Forms.RichTextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents PB As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents chkGridText As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnExport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnScriptLookUp As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtScriptID As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents CMS As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportCSV As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportTAB As System.Windows.Forms.ToolStripMenuItem

End Class
