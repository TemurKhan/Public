<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResults
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResults))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccQC = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRelease = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnBlock = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ProlisHelp = New System.Windows.Forms.HelpProvider()
        Me.btnDeleteHistory = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbAccCtl = New System.Windows.Forms.ComboBox()
        Me.cmbRun = New System.Windows.Forms.ComboBox()
        Me.btnFill = New System.Windows.Forms.Button()
        Me.txtFiller = New System.Windows.Forms.TextBox()
        Me.btnPrev = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.txtRTF = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.txtPatient = New System.Windows.Forms.TextBox()
        Me.txtValidated = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbOverride = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtEditedOn = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtEditedBy = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRelStatus = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblAccRun = New System.Windows.Forms.Label()
        Me.lblAccCtl = New System.Windows.Forms.Label()
        Me.lblWorkBatch = New System.Windows.Forms.Label()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.Test_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Analyte = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Result = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reflux = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Flag = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Normal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.History = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Note = New System.Windows.Forms.DataGridViewImageColumn()
        Me.IResult = New System.Windows.Forms.DataGridViewImageColumn()
        Me.TResult = New System.Windows.Forms.DataGridViewImageColumn()
        Me.Release = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.LorN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cause = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reflexer = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Reflexed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cmnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RTFRes = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ImgRes = New System.Windows.Forms.DataGridViewImageColumn()
        Me.ESig = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Behavior = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.txtRptDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtRptTime = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtdrawn = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtClient = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAttProv = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbDirector = New System.Windows.Forms.ComboBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpTests = New System.Windows.Forms.TabPage()
        Me.tpExtend = New System.Windows.Forms.TabPage()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.btnDelPDF = New System.Windows.Forms.Button()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.chkExtRelease = New System.Windows.Forms.CheckBox()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.lblRPTStatus = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtMeds = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.chkIncomplete = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Alert = New System.Windows.Forms.Label()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn3 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn4 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn5 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.pctImg = New System.Windows.Forms.PictureBox()
        Me.TestCount = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.btnPrintReport = New System.Windows.Forms.Button()
        Me.Wildcard = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tpTests.SuspendLayout()
        Me.tpExtend.SuspendLayout()
        CType(Me.pctImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccQC, Me.ToolStripButton2, Me.btnRelease, Me.ToolStripSeparator2, Me.btnBlock, Me.ToolStripSeparator3, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1145, 29)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccQC
        '
        Me.btnAccQC.CheckOnClick = True
        Me.btnAccQC.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnAccQC.Image = CType(resources.GetObject("btnAccQC.Image"), System.Drawing.Image)
        Me.btnAccQC.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccQC.Name = "btnAccQC"
        Me.btnAccQC.Size = New System.Drawing.Size(119, 25)
        Me.btnAccQC.Text = "Accessioned"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(6, 29)
        '
        'btnRelease
        '
        Me.btnRelease.DoubleClickEnabled = True
        Me.btnRelease.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnRelease.Image = CType(resources.GetObject("btnRelease.Image"), System.Drawing.Image)
        Me.btnRelease.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRelease.Name = "btnRelease"
        Me.btnRelease.Size = New System.Drawing.Size(109, 25)
        Me.btnRelease.Text = "Release All"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 29)
        '
        'btnBlock
        '
        Me.btnBlock.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnBlock.Image = CType(resources.GetObject("btnBlock.Image"), System.Drawing.Image)
        Me.btnBlock.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBlock.Name = "btnBlock"
        Me.btnBlock.Size = New System.Drawing.Size(89, 25)
        Me.btnBlock.Text = "Hold All"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 29)
        '
        'btnCancel
        '
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 25)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 29)
        '
        'btnHelp
        '
        Me.btnHelp.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(66, 25)
        Me.btnHelp.Text = "Help"
        '
        'ProlisHelp
        '
        Me.ProlisHelp.HelpNamespace = "prolishelp.chm"
        '
        'btnDeleteHistory
        '
        Me.btnDeleteHistory.Image = CType(resources.GetObject("btnDeleteHistory.Image"), System.Drawing.Image)
        Me.btnDeleteHistory.Location = New System.Drawing.Point(20, 628)
        Me.btnDeleteHistory.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDeleteHistory.Name = "btnDeleteHistory"
        Me.ProlisHelp.SetShowHelp(Me.btnDeleteHistory, True)
        Me.btnDeleteHistory.Size = New System.Drawing.Size(40, 37)
        Me.btnDeleteHistory.TabIndex = 85
        Me.ToolTip1.SetToolTip(Me.btnDeleteHistory, "Click me to delete the resulting history of this accession.")
        Me.btnDeleteHistory.UseVisualStyleBackColor = True
        '
        'cmbAccCtl
        '
        Me.cmbAccCtl.FormattingEnabled = True
        Me.cmbAccCtl.Location = New System.Drawing.Point(871, 63)
        Me.cmbAccCtl.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbAccCtl.Name = "cmbAccCtl"
        Me.cmbAccCtl.Size = New System.Drawing.Size(168, 24)
        Me.cmbAccCtl.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.cmbAccCtl, "Type in the initial Accession")
        '
        'cmbRun
        '
        Me.cmbRun.FormattingEnabled = True
        Me.cmbRun.Location = New System.Drawing.Point(267, 63)
        Me.cmbRun.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbRun.Name = "cmbRun"
        Me.cmbRun.Size = New System.Drawing.Size(419, 24)
        Me.cmbRun.TabIndex = 49
        Me.ToolTip1.SetToolTip(Me.cmbRun, "Select the desired worksheet to filter the tests on or clear to see all")
        '
        'btnFill
        '
        Me.btnFill.Enabled = False
        Me.btnFill.Location = New System.Drawing.Point(1007, 208)
        Me.btnFill.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFill.Name = "btnFill"
        Me.btnFill.Size = New System.Drawing.Size(84, 30)
        Me.btnFill.TabIndex = 81
        Me.btnFill.Text = "Fill Blanks"
        Me.btnFill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnFill, "Click to fill the Filler value in all blank fields")
        Me.btnFill.UseVisualStyleBackColor = True
        '
        'txtFiller
        '
        Me.txtFiller.Location = New System.Drawing.Point(893, 212)
        Me.txtFiller.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFiller.Name = "txtFiller"
        Me.txtFiller.Size = New System.Drawing.Size(104, 22)
        Me.txtFiller.TabIndex = 80
        Me.txtFiller.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.txtFiller, "A value in this field will go to all blank result fields")
        '
        'btnPrev
        '
        Me.btnPrev.Image = CType(resources.GetObject("btnPrev.Image"), System.Drawing.Image)
        Me.btnPrev.Location = New System.Drawing.Point(825, 60)
        Me.btnPrev.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrev.Name = "btnPrev"
        Me.btnPrev.Size = New System.Drawing.Size(36, 30)
        Me.btnPrev.TabIndex = 55
        Me.btnPrev.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnPrev, "Click to navigate to the first or previous Accession")
        Me.btnPrev.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.Location = New System.Drawing.Point(1033, 103)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(57, 44)
        Me.btnRefresh.TabIndex = 49
        Me.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Click to navigate to the last or the next Accession")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(1049, 60)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(41, 30)
        Me.btnLoad.TabIndex = 52
        Me.btnLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnLoad, "Click to navigate to the last or the next Accession")
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'txtRTF
        '
        Me.txtRTF.Location = New System.Drawing.Point(872, 95)
        Me.txtRTF.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRTF.Name = "txtRTF"
        Me.txtRTF.Size = New System.Drawing.Size(12, 15)
        Me.txtRTF.TabIndex = 50
        Me.txtRTF.Text = ""
        Me.txtRTF.Visible = False
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(597, 100)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(157, 16)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Patient Identification"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(903, 94)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(4)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(12, 22)
        Me.txtComment.TabIndex = 48
        Me.txtComment.Visible = False
        '
        'txtPatient
        '
        Me.txtPatient.Location = New System.Drawing.Point(595, 122)
        Me.txtPatient.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPatient.Name = "txtPatient"
        Me.txtPatient.ReadOnly = True
        Me.txtPatient.Size = New System.Drawing.Size(429, 22)
        Me.txtPatient.TabIndex = 51
        '
        'txtValidated
        '
        Me.txtValidated.Location = New System.Drawing.Point(20, 123)
        Me.txtValidated.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValidated.MaxLength = 5
        Me.txtValidated.Name = "txtValidated"
        Me.txtValidated.ReadOnly = True
        Me.txtValidated.Size = New System.Drawing.Size(83, 22)
        Me.txtValidated.TabIndex = 46
        Me.txtValidated.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(16, 101)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 16)
        Me.Label7.TabIndex = 45
        Me.Label7.Text = "Run Valid?"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbOverride
        '
        Me.cmbOverride.FormattingEnabled = True
        Me.cmbOverride.Location = New System.Drawing.Point(112, 122)
        Me.cmbOverride.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbOverride.Name = "cmbOverride"
        Me.cmbOverride.Size = New System.Drawing.Size(99, 24)
        Me.cmbOverride.TabIndex = 44
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(437, 101)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 16)
        Me.Label6.TabIndex = 43
        Me.Label6.Text = "Last Edited On"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEditedOn
        '
        Me.txtEditedOn.Location = New System.Drawing.Point(437, 122)
        Me.txtEditedOn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEditedOn.Name = "txtEditedOn"
        Me.txtEditedOn.ReadOnly = True
        Me.txtEditedOn.Size = New System.Drawing.Size(145, 22)
        Me.txtEditedOn.TabIndex = 42
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(331, 100)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 16)
        Me.Label5.TabIndex = 41
        Me.Label5.Text = "Released By"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtEditedBy
        '
        Me.txtEditedBy.Location = New System.Drawing.Point(329, 122)
        Me.txtEditedBy.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEditedBy.Name = "txtEditedBy"
        Me.txtEditedBy.ReadOnly = True
        Me.txtEditedBy.Size = New System.Drawing.Size(99, 22)
        Me.txtEditedBy.TabIndex = 40
        Me.txtEditedBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(235, 98)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 16)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Released"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRelStatus
        '
        Me.txtRelStatus.Location = New System.Drawing.Point(224, 122)
        Me.txtRelStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRelStatus.Name = "txtRelStatus"
        Me.txtRelStatus.ReadOnly = True
        Me.txtRelStatus.Size = New System.Drawing.Size(96, 22)
        Me.txtRelStatus.TabIndex = 38
        Me.txtRelStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(109, 100)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 16)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Override Valid %"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(16, 63)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(115, 22)
        Me.dtpFromDate.TabIndex = 48
        '
        'lblAccRun
        '
        Me.lblAccRun.Location = New System.Drawing.Point(16, 44)
        Me.lblAccRun.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccRun.Name = "lblAccRun"
        Me.lblAccRun.Size = New System.Drawing.Size(115, 16)
        Me.lblAccRun.TabIndex = 51
        Me.lblAccRun.Text = "From Acc Date"
        '
        'lblAccCtl
        '
        Me.lblAccCtl.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblAccCtl.Location = New System.Drawing.Point(924, 39)
        Me.lblAccCtl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccCtl.Name = "lblAccCtl"
        Me.lblAccCtl.Size = New System.Drawing.Size(87, 16)
        Me.lblAccCtl.TabIndex = 53
        Me.lblAccCtl.Text = "Accession"
        '
        'lblWorkBatch
        '
        Me.lblWorkBatch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblWorkBatch.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblWorkBatch.Location = New System.Drawing.Point(171, 39)
        Me.lblWorkBatch.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblWorkBatch.Name = "lblWorkBatch"
        Me.lblWorkBatch.Size = New System.Drawing.Size(231, 0)
        Me.lblWorkBatch.TabIndex = 54
        Me.lblWorkBatch.Text = "Worksheet"
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvResults.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Test_ID, Me.Analyte, Me.Result, Me.Reflux, Me.Flag, Me.Normal, Me.History, Me.Note, Me.IResult, Me.TResult, Me.Release, Me.LorN, Me.AccID, Me.Cause, Me.Reflexer, Me.Reflexed, Me.Cmnt, Me.RTFRes, Me.ImgRes, Me.ESig, Me.Behavior})
        Me.dgvResults.Location = New System.Drawing.Point(8, 7)
        Me.dgvResults.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.RowHeadersWidth = 51
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.Wheat
        Me.dgvResults.RowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvResults.Size = New System.Drawing.Size(1092, 345)
        Me.dgvResults.TabIndex = 56
        '
        'Test_ID
        '
        Me.Test_ID.HeaderText = "Test_ID"
        Me.Test_ID.MinimumWidth = 6
        Me.Test_ID.Name = "Test_ID"
        Me.Test_ID.ReadOnly = True
        Me.Test_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Test_ID.Visible = False
        Me.Test_ID.Width = 125
        '
        'Analyte
        '
        Me.Analyte.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Analyte.FillWeight = 156.0!
        Me.Analyte.HeaderText = "Analyte Name"
        Me.Analyte.MinimumWidth = 6
        Me.Analyte.Name = "Analyte"
        Me.Analyte.ReadOnly = True
        '
        'Result
        '
        Me.Result.FillWeight = 200.0!
        Me.Result.HeaderText = "Result"
        Me.Result.MaxInputLength = 100
        Me.Result.MinimumWidth = 6
        Me.Result.Name = "Result"
        Me.Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Result.Width = 200
        '
        'Reflux
        '
        Me.Reflux.FillWeight = 30.0!
        Me.Reflux.HeaderText = ""
        Me.Reflux.Image = CType(resources.GetObject("Reflux.Image"), System.Drawing.Image)
        Me.Reflux.MinimumWidth = 6
        Me.Reflux.Name = "Reflux"
        Me.Reflux.ReadOnly = True
        Me.Reflux.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Reflux.Width = 30
        '
        'Flag
        '
        Me.Flag.FillWeight = 69.0!
        Me.Flag.HeaderText = "Flag/Interp"
        Me.Flag.MinimumWidth = 6
        Me.Flag.Name = "Flag"
        Me.Flag.ReadOnly = True
        Me.Flag.Width = 69
        '
        'Normal
        '
        Me.Normal.FillWeight = 95.0!
        Me.Normal.HeaderText = "Range/Cutoff"
        Me.Normal.MinimumWidth = 6
        Me.Normal.Name = "Normal"
        Me.Normal.ReadOnly = True
        Me.Normal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Normal.Width = 95
        '
        'History
        '
        Me.History.FillWeight = 50.0!
        Me.History.HeaderText = "History"
        Me.History.Image = CType(resources.GetObject("History.Image"), System.Drawing.Image)
        Me.History.MinimumWidth = 6
        Me.History.Name = "History"
        Me.History.ReadOnly = True
        Me.History.Width = 50
        '
        'Note
        '
        Me.Note.FillWeight = 50.0!
        Me.Note.HeaderText = "Note"
        Me.Note.Image = CType(resources.GetObject("Note.Image"), System.Drawing.Image)
        Me.Note.MinimumWidth = 6
        Me.Note.Name = "Note"
        Me.Note.ReadOnly = True
        Me.Note.Width = 50
        '
        'IResult
        '
        Me.IResult.FillWeight = 40.0!
        Me.IResult.HeaderText = "Image"
        Me.IResult.Image = CType(resources.GetObject("IResult.Image"), System.Drawing.Image)
        Me.IResult.MinimumWidth = 6
        Me.IResult.Name = "IResult"
        Me.IResult.ReadOnly = True
        Me.IResult.Visible = False
        Me.IResult.Width = 40
        '
        'TResult
        '
        Me.TResult.FillWeight = 50.0!
        Me.TResult.HeaderText = "RTFI"
        Me.TResult.Image = CType(resources.GetObject("TResult.Image"), System.Drawing.Image)
        Me.TResult.MinimumWidth = 6
        Me.TResult.Name = "TResult"
        Me.TResult.ReadOnly = True
        Me.TResult.Width = 50
        '
        'Release
        '
        Me.Release.FillWeight = 60.0!
        Me.Release.HeaderText = "Release"
        Me.Release.MinimumWidth = 6
        Me.Release.Name = "Release"
        Me.Release.Width = 60
        '
        'LorN
        '
        Me.LorN.HeaderText = "L/N"
        Me.LorN.MinimumWidth = 6
        Me.LorN.Name = "LorN"
        Me.LorN.ReadOnly = True
        Me.LorN.Visible = False
        Me.LorN.Width = 125
        '
        'AccID
        '
        Me.AccID.HeaderText = "AccID"
        Me.AccID.MinimumWidth = 6
        Me.AccID.Name = "AccID"
        Me.AccID.ReadOnly = True
        Me.AccID.Visible = False
        Me.AccID.Width = 125
        '
        'Cause
        '
        Me.Cause.HeaderText = "Cause"
        Me.Cause.MinimumWidth = 6
        Me.Cause.Name = "Cause"
        Me.Cause.Visible = False
        Me.Cause.Width = 125
        '
        'Reflexer
        '
        Me.Reflexer.HeaderText = "Reflexer"
        Me.Reflexer.MinimumWidth = 6
        Me.Reflexer.Name = "Reflexer"
        Me.Reflexer.Visible = False
        Me.Reflexer.Width = 125
        '
        'Reflexed
        '
        Me.Reflexed.HeaderText = "Reflexed"
        Me.Reflexed.MinimumWidth = 6
        Me.Reflexed.Name = "Reflexed"
        Me.Reflexed.Visible = False
        Me.Reflexed.Width = 125
        '
        'Cmnt
        '
        Me.Cmnt.HeaderText = "Cmnt"
        Me.Cmnt.MinimumWidth = 6
        Me.Cmnt.Name = "Cmnt"
        Me.Cmnt.ReadOnly = True
        Me.Cmnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Cmnt.Visible = False
        Me.Cmnt.Width = 125
        '
        'RTFRes
        '
        Me.RTFRes.HeaderText = "RTFRes"
        Me.RTFRes.MinimumWidth = 6
        Me.RTFRes.Name = "RTFRes"
        Me.RTFRes.ReadOnly = True
        Me.RTFRes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.RTFRes.Visible = False
        Me.RTFRes.Width = 125
        '
        'ImgRes
        '
        Me.ImgRes.HeaderText = "ImgRes"
        Me.ImgRes.MinimumWidth = 6
        Me.ImgRes.Name = "ImgRes"
        Me.ImgRes.Visible = False
        Me.ImgRes.Width = 125
        '
        'ESig
        '
        Me.ESig.HeaderText = "ESig"
        Me.ESig.MinimumWidth = 6
        Me.ESig.Name = "ESig"
        Me.ESig.Visible = False
        Me.ESig.Width = 125
        '
        'Behavior
        '
        Me.Behavior.HeaderText = "Behavior"
        Me.Behavior.MinimumWidth = 6
        Me.Behavior.Name = "Behavior"
        Me.Behavior.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.Behavior.Visible = False
        Me.Behavior.Width = 125
        '
        'txtRptDate
        '
        Me.txtRptDate.Location = New System.Drawing.Point(849, 674)
        Me.txtRptDate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRptDate.Mask = "00/00/0000"
        Me.txtRptDate.Name = "txtRptDate"
        Me.txtRptDate.Size = New System.Drawing.Size(103, 22)
        Me.txtRptDate.TabIndex = 57
        Me.txtRptDate.ValidatingType = GetType(Date)
        '
        'txtRptTime
        '
        Me.txtRptTime.Location = New System.Drawing.Point(1032, 674)
        Me.txtRptTime.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRptTime.Mask = "90:00"
        Me.txtRptTime.Name = "txtRptTime"
        Me.txtRptTime.Size = New System.Drawing.Size(89, 22)
        Me.txtRptTime.TabIndex = 58
        Me.txtRptTime.ValidatingType = GetType(Date)
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(985, 679)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 16)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Time"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(747, 678)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 16)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "Report Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNote
        '
        Me.txtNote.Location = New System.Drawing.Point(63, 674)
        Me.txtNote.Margin = New System.Windows.Forms.Padding(4)
        Me.txtNote.MaxLength = 960
        Me.txtNote.Multiline = True
        Me.txtNote.Name = "txtNote"
        Me.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNote.Size = New System.Drawing.Size(663, 66)
        Me.txtNote.TabIndex = 61
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(13, 695)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 16)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "Note"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label10.Location = New System.Drawing.Point(19, 154)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(193, 16)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "Specimen Collected On"
        '
        'txtdrawn
        '
        Me.txtdrawn.Location = New System.Drawing.Point(23, 174)
        Me.txtdrawn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtdrawn.Name = "txtdrawn"
        Me.txtdrawn.ReadOnly = True
        Me.txtdrawn.Size = New System.Drawing.Size(188, 22)
        Me.txtdrawn.TabIndex = 63
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label11.Location = New System.Drawing.Point(224, 154)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 16)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "Client"
        '
        'txtClient
        '
        Me.txtClient.Location = New System.Drawing.Point(224, 174)
        Me.txtClient.Margin = New System.Windows.Forms.Padding(4)
        Me.txtClient.Name = "txtClient"
        Me.txtClient.ReadOnly = True
        Me.txtClient.Size = New System.Drawing.Size(359, 22)
        Me.txtClient.TabIndex = 65
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label12.Location = New System.Drawing.Point(595, 154)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(131, 16)
        Me.Label12.TabIndex = 68
        Me.Label12.Text = "Attending Provider"
        '
        'txtAttProv
        '
        Me.txtAttProv.Location = New System.Drawing.Point(592, 174)
        Me.txtAttProv.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAttProv.Name = "txtAttProv"
        Me.txtAttProv.ReadOnly = True
        Me.txtAttProv.Size = New System.Drawing.Size(292, 22)
        Me.txtAttProv.TabIndex = 67
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(747, 720)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(54, 16)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Director"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmbDirector
        '
        Me.cmbDirector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDirector.FormattingEnabled = True
        Me.cmbDirector.Location = New System.Drawing.Point(847, 718)
        Me.cmbDirector.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbDirector.Name = "cmbDirector"
        Me.cmbDirector.Size = New System.Drawing.Size(273, 24)
        Me.cmbDirector.TabIndex = 70
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tpTests)
        Me.TabControl1.Controls.Add(Me.tpExtend)
        Me.TabControl1.ItemSize = New System.Drawing.Size(80, 18)
        Me.TabControl1.Location = New System.Drawing.Point(16, 242)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1112, 382)
        Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabControl1.TabIndex = 71
        '
        'tpTests
        '
        Me.tpTests.Controls.Add(Me.dgvResults)
        Me.tpTests.Location = New System.Drawing.Point(4, 22)
        Me.tpTests.Margin = New System.Windows.Forms.Padding(4)
        Me.tpTests.Name = "tpTests"
        Me.tpTests.Padding = New System.Windows.Forms.Padding(4)
        Me.tpTests.Size = New System.Drawing.Size(1104, 356)
        Me.tpTests.TabIndex = 0
        Me.tpTests.Text = "Tests"
        Me.tpTests.UseVisualStyleBackColor = True
        '
        'tpExtend
        '
        Me.tpExtend.AutoScroll = True
        Me.tpExtend.Controls.Add(Me.WebBrowser1)
        Me.tpExtend.Controls.Add(Me.btnDelPDF)
        Me.tpExtend.Controls.Add(Me.btnScan)
        Me.tpExtend.Controls.Add(Me.btnBrowse)
        Me.tpExtend.Controls.Add(Me.chkExtRelease)
        Me.tpExtend.Location = New System.Drawing.Point(4, 22)
        Me.tpExtend.Margin = New System.Windows.Forms.Padding(4)
        Me.tpExtend.Name = "tpExtend"
        Me.tpExtend.Padding = New System.Windows.Forms.Padding(4)
        Me.tpExtend.Size = New System.Drawing.Size(1104, 356)
        Me.tpExtend.TabIndex = 1
        Me.tpExtend.Text = "Extended"
        Me.tpExtend.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(155, 0)
        Me.WebBrowser1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(920, 359)
        Me.WebBrowser1.TabIndex = 22
        '
        'btnDelPDF
        '
        Me.btnDelPDF.Enabled = False
        Me.btnDelPDF.Image = CType(resources.GetObject("btnDelPDF.Image"), System.Drawing.Image)
        Me.btnDelPDF.Location = New System.Drawing.Point(32, 167)
        Me.btnDelPDF.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelPDF.Name = "btnDelPDF"
        Me.btnDelPDF.Size = New System.Drawing.Size(89, 33)
        Me.btnDelPDF.TabIndex = 21
        Me.btnDelPDF.UseVisualStyleBackColor = True
        '
        'btnScan
        '
        Me.btnScan.Enabled = False
        Me.btnScan.Location = New System.Drawing.Point(32, 284)
        Me.btnScan.Margin = New System.Windows.Forms.Padding(4)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(89, 33)
        Me.btnScan.TabIndex = 18
        Me.btnScan.Text = "Scan"
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Enabled = False
        Me.btnBrowse.Location = New System.Drawing.Point(32, 226)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(89, 33)
        Me.btnBrowse.TabIndex = 19
        Me.btnBrowse.Text = "Browse"
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'chkExtRelease
        '
        Me.chkExtRelease.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkExtRelease.Location = New System.Drawing.Point(32, 341)
        Me.chkExtRelease.Margin = New System.Windows.Forms.Padding(4)
        Me.chkExtRelease.Name = "chkExtRelease"
        Me.chkExtRelease.Size = New System.Drawing.Size(89, 26)
        Me.chkExtRelease.TabIndex = 17
        Me.chkExtRelease.Text = "Release"
        Me.chkExtRelease.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblRPTStatus
        '
        Me.lblRPTStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRPTStatus.ForeColor = System.Drawing.Color.Red
        Me.lblRPTStatus.Location = New System.Drawing.Point(927, 159)
        Me.lblRPTStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRPTStatus.Name = "lblRPTStatus"
        Me.lblRPTStatus.Size = New System.Drawing.Size(180, 30)
        Me.lblRPTStatus.TabIndex = 72
        Me.lblRPTStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(749, 635)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(83, 17)
        Me.Label15.TabIndex = 75
        Me.Label15.Text = "Legend ="
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.GreenYellow
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.ForeColor = System.Drawing.Color.Blue
        Me.Label14.Location = New System.Drawing.Point(847, 635)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(105, 18)
        Me.Label14.TabIndex = 76
        Me.Label14.Text = "CORRECTED"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMeds
        '
        Me.txtMeds.Location = New System.Drawing.Point(112, 210)
        Me.txtMeds.Margin = New System.Windows.Forms.Padding(4)
        Me.txtMeds.Name = "txtMeds"
        Me.txtMeds.ReadOnly = True
        Me.txtMeds.Size = New System.Drawing.Size(772, 22)
        Me.txtMeds.TabIndex = 77
        '
        'Label16
        '
        Me.Label16.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label16.Location = New System.Drawing.Point(12, 215)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(88, 16)
        Me.Label16.TabIndex = 78
        Me.Label16.Text = "Medications:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStatus
        '
        Me.lblStatus.Location = New System.Drawing.Point(72, 743)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(187, 18)
        Me.lblStatus.TabIndex = 79
        '
        'Label17
        '
        Me.Label17.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label17.Location = New System.Drawing.Point(924, 192)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(49, 16)
        Me.Label17.TabIndex = 82
        Me.Label17.Text = "Filler"
        '
        'chkIncomplete
        '
        Me.chkIncomplete.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkIncomplete.Location = New System.Drawing.Point(695, 63)
        Me.chkIncomplete.Margin = New System.Windows.Forms.Padding(4)
        Me.chkIncomplete.Name = "chkIncomplete"
        Me.chkIncomplete.Size = New System.Drawing.Size(120, 26)
        Me.chkIncomplete.TabIndex = 83
        Me.chkIncomplete.Text = "All"
        Me.chkIncomplete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkIncomplete.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label18.Location = New System.Drawing.Point(697, 39)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(119, 16)
        Me.Label18.TabIndex = 84
        Me.Label18.Text = "Result Status"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Alert
        '
        Me.Alert.AutoSize = True
        Me.Alert.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Alert.Location = New System.Drawing.Point(487, 242)
        Me.Alert.Name = "Alert"
        Me.Alert.Size = New System.Drawing.Size(0, 16)
        Me.Alert.TabIndex = 57
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.FillWeight = 30.0!
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn1.MinimumWidth = 6
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewImageColumn1.Width = 30
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.FillWeight = 50.0!
        Me.DataGridViewImageColumn2.HeaderText = "History"
        Me.DataGridViewImageColumn2.Image = CType(resources.GetObject("DataGridViewImageColumn2.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn2.MinimumWidth = 6
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.ReadOnly = True
        Me.DataGridViewImageColumn2.Width = 50
        '
        'DataGridViewImageColumn3
        '
        Me.DataGridViewImageColumn3.FillWeight = 50.0!
        Me.DataGridViewImageColumn3.HeaderText = "Note"
        Me.DataGridViewImageColumn3.Image = CType(resources.GetObject("DataGridViewImageColumn3.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn3.MinimumWidth = 6
        Me.DataGridViewImageColumn3.Name = "DataGridViewImageColumn3"
        Me.DataGridViewImageColumn3.ReadOnly = True
        Me.DataGridViewImageColumn3.Width = 50
        '
        'DataGridViewImageColumn4
        '
        Me.DataGridViewImageColumn4.FillWeight = 40.0!
        Me.DataGridViewImageColumn4.HeaderText = "Image"
        Me.DataGridViewImageColumn4.Image = CType(resources.GetObject("DataGridViewImageColumn4.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn4.MinimumWidth = 6
        Me.DataGridViewImageColumn4.Name = "DataGridViewImageColumn4"
        Me.DataGridViewImageColumn4.ReadOnly = True
        Me.DataGridViewImageColumn4.Visible = False
        Me.DataGridViewImageColumn4.Width = 40
        '
        'DataGridViewImageColumn5
        '
        Me.DataGridViewImageColumn5.FillWeight = 50.0!
        Me.DataGridViewImageColumn5.HeaderText = "RTFI"
        Me.DataGridViewImageColumn5.Image = CType(resources.GetObject("DataGridViewImageColumn5.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn5.MinimumWidth = 6
        Me.DataGridViewImageColumn5.Name = "DataGridViewImageColumn5"
        Me.DataGridViewImageColumn5.ReadOnly = True
        Me.DataGridViewImageColumn5.Width = 50
        '
        'pctImg
        '
        Me.pctImg.Location = New System.Drawing.Point(948, 95)
        Me.pctImg.Margin = New System.Windows.Forms.Padding(4)
        Me.pctImg.Name = "pctImg"
        Me.pctImg.Size = New System.Drawing.Size(25, 20)
        Me.pctImg.TabIndex = 73
        Me.pctImg.TabStop = False
        Me.pctImg.Visible = False
        '
        'TestCount
        '
        Me.TestCount.AutoSize = True
        Me.TestCount.Location = New System.Drawing.Point(1061, 244)
        Me.TestCount.Name = "TestCount"
        Me.TestCount.Size = New System.Drawing.Size(0, 16)
        Me.TestCount.TabIndex = 86
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Image = My.Resources.Resources.paste
        Me.Label19.Location = New System.Drawing.Point(869, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(33, 31)
        Me.Label19.TabIndex = 87
        Me.Label19.Text = "      "
        '
        'btnPrintReport
        '
        Me.btnPrintReport.Location = New System.Drawing.Point(1011, 628)
        Me.btnPrintReport.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnPrintReport.Name = "btnPrintReport"
        Me.btnPrintReport.Size = New System.Drawing.Size(113, 30)
        Me.btnPrintReport.TabIndex = 88
        Me.btnPrintReport.Text = "Print Report"
        Me.btnPrintReport.UseVisualStyleBackColor = True
        '
        'Wildcard
        '
        Me.Wildcard.Location = New System.Drawing.Point(491, 635)
        Me.Wildcard.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Wildcard.Name = "Wildcard"
        Me.Wildcard.Size = New System.Drawing.Size(236, 22)
        Me.Wildcard.TabIndex = 89
        Me.Wildcard.Text = "Interpretation unavailable"
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(325, 638)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(153, 17)
        Me.Label20.TabIndex = 90
        Me.Label20.Text = "Remove Flag On Note"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(144, 44)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(115, 16)
        Me.Label21.TabIndex = 51
        Me.Label21.Text = "To Acc Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(141, 63)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(115, 22)
        Me.dtpToDate.TabIndex = 48
        '
        'frmResults
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1145, 775)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Wildcard)
        Me.Controls.Add(Me.btnPrintReport)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.TestCount)
        Me.Controls.Add(Me.Alert)
        Me.Controls.Add(Me.btnDeleteHistory)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.chkIncomplete)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.btnFill)
        Me.Controls.Add(Me.txtFiller)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtMeds)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.pctImg)
        Me.Controls.Add(Me.lblRPTStatus)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.cmbDirector)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtAttProv)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtClient)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtdrawn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtNote)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRptTime)
        Me.Controls.Add(Me.txtRptDate)
        Me.Controls.Add(Me.txtRTF)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblAccRun)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.cmbAccCtl)
        Me.Controls.Add(Me.txtPatient)
        Me.Controls.Add(Me.btnPrev)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.cmbRun)
        Me.Controls.Add(Me.txtValidated)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbOverride)
        Me.Controls.Add(Me.lblAccCtl)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblWorkBatch)
        Me.Controls.Add(Me.txtEditedOn)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtEditedBy)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtRelStatus)
        Me.ProlisHelp.SetHelpKeyword(Me, "html\hs1052.htm")
        Me.ProlisHelp.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1081, 746)
        Me.Name = "frmResults"
        Me.ProlisHelp.SetShowHelp(Me, True)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Result Entry & Review"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tpTests.ResumeLayout(False)
        Me.tpExtend.ResumeLayout(False)
        CType(Me.pctImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRelease As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnBlock As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAccQC As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ProlisHelp As System.Windows.Forms.HelpProvider
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtRTF As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtValidated As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbOverride As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtEditedOn As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEditedBy As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRelStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblAccRun As System.Windows.Forms.Label
    Friend WithEvents btnPrev As System.Windows.Forms.Button
    Friend WithEvents cmbRun As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblAccCtl As System.Windows.Forms.Label
    Friend WithEvents lblWorkBatch As System.Windows.Forms.Label
    Friend WithEvents dgvResults As System.Windows.Forms.DataGridView
    Friend WithEvents txtRptDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtRptTime As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtNote As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtdrawn As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtClient As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtAttProv As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbDirector As System.Windows.Forms.ComboBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tpTests As System.Windows.Forms.TabPage
    Friend WithEvents tpExtend As System.Windows.Forms.TabPage
    Friend WithEvents btnScan As System.Windows.Forms.Button
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents chkExtRelease As System.Windows.Forms.CheckBox
    Friend WithEvents btnDelPDF As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblRPTStatus As System.Windows.Forms.Label
    Friend WithEvents pctImg As System.Windows.Forms.PictureBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtMeds As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtFiller As System.Windows.Forms.TextBox
    Friend WithEvents btnFill As System.Windows.Forms.Button
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents chkIncomplete As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteHistory As System.Windows.Forms.Button
    Friend WithEvents Alert As System.Windows.Forms.Label
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn3 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn4 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn5 As System.Windows.Forms.DataGridViewImageColumn
    'Friend WithEvents PdfViewer1 As Foxit.PDF.Viewer.PdfViewer
    Friend WithEvents TestCount As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnPrintReport As System.Windows.Forms.Button
    Friend WithEvents Wildcard As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As Label
    Friend WithEvents dtpToDate As DateTimePicker
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents cmbAccCtl As ComboBox
    Friend WithEvents Test_ID As DataGridViewTextBoxColumn
    Friend WithEvents Analyte As DataGridViewTextBoxColumn
    Friend WithEvents Result As DataGridViewTextBoxColumn
    Friend WithEvents Reflux As DataGridViewImageColumn
    Friend WithEvents Flag As DataGridViewTextBoxColumn
    Friend WithEvents Normal As DataGridViewTextBoxColumn
    Friend WithEvents History As DataGridViewImageColumn
    Friend WithEvents Note As DataGridViewImageColumn
    Friend WithEvents IResult As DataGridViewImageColumn
    Friend WithEvents TResult As DataGridViewImageColumn
    Friend WithEvents Release As DataGridViewCheckBoxColumn
    Friend WithEvents LorN As DataGridViewTextBoxColumn
    Friend WithEvents AccID As DataGridViewTextBoxColumn
    Friend WithEvents Cause As DataGridViewTextBoxColumn
    Friend WithEvents Reflexer As DataGridViewTextBoxColumn
    Friend WithEvents Reflexed As DataGridViewTextBoxColumn
    Friend WithEvents Cmnt As DataGridViewTextBoxColumn
    Friend WithEvents RTFRes As DataGridViewTextBoxColumn
    Friend WithEvents ImgRes As DataGridViewImageColumn
    Friend WithEvents ESig As DataGridViewCheckBoxColumn
    Friend WithEvents Behavior As DataGridViewTextBoxColumn
End Class
