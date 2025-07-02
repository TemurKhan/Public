<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewBARHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewBARHistory))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnPrint = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        dgvHistory = New DataGridView()
        BAREvent = New DataGridViewTextBoxColumn()
        EvDate = New DataGridViewTextBoxColumn()
        Party = New DataGridViewTextBoxColumn()
        PartyType = New DataGridViewTextBoxColumn()
        DocNo = New DataGridViewTextBoxColumn()
        Amount = New DataGridViewTextBoxColumn()
        ExecBy = New DataGridViewTextBoxColumn()
        txtAccID = New TextBox()
        Label1 = New Label()
        Label2 = New Label()
        txtPatientID = New TextBox()
        Label3 = New Label()
        txtPatient = New TextBox()
        Label4 = New Label()
        txtProviderID = New TextBox()
        Label5 = New Label()
        txtProvider = New TextBox()
        Label6 = New Label()
        txtAccDate = New TextBox()
        ToolStrip1.SuspendLayout()
        CType(dgvHistory, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnPrint, ToolStripSeparator1, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(980, 34)
        ToolStrip1.TabIndex = 8
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnPrint
        ' 
        btnPrint.Enabled = False
        btnPrint.ForeColor = Color.DarkBlue
        btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), Image)
        btnPrint.ImageTransparentColor = Color.Magenta
        btnPrint.Name = "btnPrint"
        btnPrint.Size = New Size(76, 29)
        btnPrint.Text = "Print"
        btnPrint.TextAlign = ContentAlignment.MiddleRight
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
        ' dgvHistory
        ' 
        dgvHistory.AllowUserToAddRows = False
        dgvHistory.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.LightYellow
        dgvHistory.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvHistory.Columns.AddRange(New DataGridViewColumn() {BAREvent, EvDate, Party, PartyType, DocNo, Amount, ExecBy})
        dgvHistory.Location = New Point(23, 290)
        dgvHistory.Margin = New Padding(5, 6, 5, 6)
        dgvHistory.Name = "dgvHistory"
        dgvHistory.ReadOnly = True
        dgvHistory.RowHeadersVisible = False
        dgvHistory.RowHeadersWidth = 62
        dgvHistory.Size = New Size(930, 448)
        dgvHistory.TabIndex = 9
        ' 
        ' BAREvent
        ' 
        BAREvent.FillWeight = 70F
        BAREvent.HeaderText = "Event"
        BAREvent.MinimumWidth = 8
        BAREvent.Name = "BAREvent"
        BAREvent.ReadOnly = True
        BAREvent.Width = 118
        ' 
        ' EvDate
        ' 
        EvDate.FillWeight = 70F
        EvDate.HeaderText = "Dated"
        EvDate.MinimumWidth = 8
        EvDate.Name = "EvDate"
        EvDate.ReadOnly = True
        EvDate.Width = 118
        ' 
        ' Party
        ' 
        Party.FillWeight = 120F
        Party.HeaderText = "Party"
        Party.MinimumWidth = 8
        Party.Name = "Party"
        Party.ReadOnly = True
        Party.Width = 202
        ' 
        ' PartyType
        ' 
        PartyType.FillWeight = 60F
        PartyType.HeaderText = "Type"
        PartyType.MinimumWidth = 8
        PartyType.Name = "PartyType"
        PartyType.ReadOnly = True
        PartyType.Width = 101
        ' 
        ' DocNo
        ' 
        DocNo.FillWeight = 70F
        DocNo.HeaderText = "Doc No"
        DocNo.MinimumWidth = 8
        DocNo.Name = "DocNo"
        DocNo.ReadOnly = True
        DocNo.Width = 118
        ' 
        ' Amount
        ' 
        Amount.FillWeight = 70F
        Amount.HeaderText = "Amount"
        Amount.MinimumWidth = 8
        Amount.Name = "Amount"
        Amount.ReadOnly = True
        Amount.Width = 118
        ' 
        ' ExecBy
        ' 
        ExecBy.FillWeight = 90F
        ExecBy.HeaderText = "User"
        ExecBy.MinimumWidth = 8
        ExecBy.Name = "ExecBy"
        ExecBy.ReadOnly = True
        ExecBy.Width = 152
        ' 
        ' txtAccID
        ' 
        txtAccID.Location = New Point(23, 113)
        txtAccID.Margin = New Padding(5, 6, 5, 6)
        txtAccID.Name = "txtAccID"
        txtAccID.ReadOnly = True
        txtAccID.Size = New Size(149, 31)
        txtAccID.TabIndex = 10
        txtAccID.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(45, 77)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(130, 25)
        Label1.TabIndex = 11
        Label1.Text = "Accession ID"
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(363, 77)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(115, 25)
        Label2.TabIndex = 13
        Label2.Text = "Patient ID"
        ' 
        ' txtPatientID
        ' 
        txtPatientID.Location = New Point(345, 113)
        txtPatientID.Margin = New Padding(5, 6, 5, 6)
        txtPatientID.Name = "txtPatientID"
        txtPatientID.ReadOnly = True
        txtPatientID.Size = New Size(146, 31)
        txtPatientID.TabIndex = 12
        txtPatientID.TextAlign = HorizontalAlignment.Center
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(498, 77)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(162, 25)
        Label3.TabIndex = 15
        Label3.Text = "Patient"
        ' 
        ' txtPatient
        ' 
        txtPatient.Location = New Point(503, 113)
        txtPatient.Margin = New Padding(5, 6, 5, 6)
        txtPatient.Name = "txtPatient"
        txtPatient.ReadOnly = True
        txtPatient.Size = New Size(447, 31)
        txtPatient.TabIndex = 14
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(45, 181)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(130, 25)
        Label4.TabIndex = 17
        Label4.Text = "Provider ID"
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(23, 217)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.Name = "txtProviderID"
        txtProviderID.ReadOnly = True
        txtProviderID.Size = New Size(149, 31)
        txtProviderID.TabIndex = 16
        txtProviderID.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(200, 181)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(130, 25)
        Label5.TabIndex = 19
        Label5.Text = "Provider"
        ' 
        ' txtProvider
        ' 
        txtProvider.Location = New Point(197, 217)
        txtProvider.Margin = New Padding(5, 6, 5, 6)
        txtProvider.Name = "txtProvider"
        txtProvider.ReadOnly = True
        txtProvider.Size = New Size(754, 31)
        txtProvider.TabIndex = 18
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(200, 77)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(115, 25)
        Label6.TabIndex = 21
        Label6.Text = "Acc Date"
        ' 
        ' txtAccDate
        ' 
        txtAccDate.Location = New Point(185, 113)
        txtAccDate.Margin = New Padding(5, 6, 5, 6)
        txtAccDate.Name = "txtAccDate"
        txtAccDate.ReadOnly = True
        txtAccDate.Size = New Size(146, 31)
        txtAccDate.TabIndex = 20
        txtAccDate.TextAlign = HorizontalAlignment.Center
        ' 
        ' frmViewBARHistory
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(980, 779)
        Controls.Add(Label6)
        Controls.Add(txtAccDate)
        Controls.Add(Label5)
        Controls.Add(txtProvider)
        Controls.Add(Label4)
        Controls.Add(txtProviderID)
        Controls.Add(Label3)
        Controls.Add(txtPatient)
        Controls.Add(Label2)
        Controls.Add(txtPatientID)
        Controls.Add(Label1)
        Controls.Add(txtAccID)
        Controls.Add(dgvHistory)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmViewBARHistory"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "View BAR History"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        CType(dgvHistory, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents dgvHistory As System.Windows.Forms.DataGridView
    Friend WithEvents BAREvent As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EvDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Party As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PartyType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ExecBy As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtAccID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPatientID As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPatient As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtProvider As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtAccDate As System.Windows.Forms.TextBox

End Class
