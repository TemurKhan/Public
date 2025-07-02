<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmERA_Processed
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmERA_Processed))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.dgvEOBs = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Document_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Date_Time = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PaymentType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvClaims = New System.Windows.Forms.DataGridView()
        Me.Accession_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clipboardMsg = New System.Windows.Forms.Label()
        Me.dtpPost = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.From = New System.Windows.Forms.Label()
        Me.Tod = New System.Windows.Forms.Label()
        Me.Search = New System.Windows.Forms.Button()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewImageColumn2 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.dgvDetail = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Doc = New System.Windows.Forms.TextBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Accid = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Accession = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.InvID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SvcDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Patient = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BillAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DocNo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TrnDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Pmnt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.WO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Balance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Age = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dgvEOBs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvClaims, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvEOBs
        '
        Me.dgvEOBs.AllowUserToAddRows = False
        Me.dgvEOBs.AllowUserToDeleteRows = False
        Me.dgvEOBs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEOBs.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Document_ID, Me.Date_Time, Me.PaymentType})
        Me.dgvEOBs.Location = New System.Drawing.Point(41, 88)
        Me.dgvEOBs.Name = "dgvEOBs"
        Me.dgvEOBs.ReadOnly = True
        Me.dgvEOBs.RowTemplate.Height = 24
        Me.dgvEOBs.Size = New System.Drawing.Size(1095, 198)
        Me.dgvEOBs.TabIndex = 0
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'Document_ID
        '
        Me.Document_ID.HeaderText = "Document_ID"
        Me.Document_ID.Name = "Document_ID"
        Me.Document_ID.ReadOnly = True
        Me.Document_ID.Width = 300
        '
        'Date_Time
        '
        Me.Date_Time.HeaderText = "Date"
        Me.Date_Time.Name = "Date_Time"
        Me.Date_Time.ReadOnly = True
        Me.Date_Time.Width = 300
        '
        'PaymentType
        '
        Me.PaymentType.HeaderText = "Ar Type"
        Me.PaymentType.Name = "PaymentType"
        Me.PaymentType.ReadOnly = True
        '
        'dgvClaims
        '
        Me.dgvClaims.AllowUserToAddRows = False
        Me.dgvClaims.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dgvClaims.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvClaims.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClaims.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Accession_ID})
        Me.dgvClaims.Location = New System.Drawing.Point(41, 306)
        Me.dgvClaims.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvClaims.Name = "dgvClaims"
        Me.dgvClaims.RowHeadersVisible = False
        Me.dgvClaims.Size = New System.Drawing.Size(1095, 198)
        Me.dgvClaims.TabIndex = 8
        '
        'Accession_ID
        '
        Me.Accession_ID.HeaderText = "Accession_ID"
        Me.Accession_ID.Name = "Accession_ID"
        Me.Accession_ID.ReadOnly = True
        Me.Accession_ID.Width = 200
        '
        'clipboardMsg
        '
        Me.clipboardMsg.AutoSize = True
        Me.clipboardMsg.Location = New System.Drawing.Point(38, 785)
        Me.clipboardMsg.Name = "clipboardMsg"
        Me.clipboardMsg.Size = New System.Drawing.Size(144, 17)
        Me.clipboardMsg.TabIndex = 95
        Me.clipboardMsg.Text = "_________________"
        '
        'dtpPost
        '
        Me.dtpPost.CustomFormat = ""
        Me.dtpPost.Location = New System.Drawing.Point(40, 45)
        Me.dtpPost.Name = "dtpPost"
        Me.dtpPost.Size = New System.Drawing.Size(272, 22)
        Me.dtpPost.TabIndex = 96
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(381, 45)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(271, 22)
        Me.DateTimePicker2.TabIndex = 97
        '
        'From
        '
        Me.From.Location = New System.Drawing.Point(37, 21)
        Me.From.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.From.Name = "From"
        Me.From.Size = New System.Drawing.Size(125, 20)
        Me.From.TabIndex = 98
        Me.From.Text = "Posted Date From"
        Me.From.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Tod
        '
        Me.Tod.Location = New System.Drawing.Point(378, 21)
        Me.Tod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Tod.Name = "Tod"
        Me.Tod.Size = New System.Drawing.Size(123, 20)
        Me.Tod.TabIndex = 99
        Me.Tod.Text = "Posted Date To"
        Me.Tod.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(1061, 43)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(75, 30)
        Me.Search.TabIndex = 100
        Me.Search.Text = "Search"
        Me.Search.UseVisualStyleBackColor = True
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.FillWeight = 48.0!
        Me.DataGridViewImageColumn1.HeaderText = ""
        Me.DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.Width = 48
        '
        'DataGridViewImageColumn2
        '
        Me.DataGridViewImageColumn2.FillWeight = 40.0!
        Me.DataGridViewImageColumn2.HeaderText = ""
        Me.DataGridViewImageColumn2.Image = CType(resources.GetObject("DataGridViewImageColumn2.Image"), System.Drawing.Image)
        Me.DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        Me.DataGridViewImageColumn2.Width = 40
        '
        'dgvDetail
        '
        Me.dgvDetail.AllowUserToAddRows = False
        Me.dgvDetail.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.MintCream
        Me.dgvDetail.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Accession, Me.InvID, Me.SvcDate, Me.BillTo, Me.Patient, Me.BillAmount, Me.DocNo, Me.TrnDate, Me.Pmnt, Me.WO, Me.Balance, Me.Age})
        Me.dgvDetail.Location = New System.Drawing.Point(41, 526)
        Me.dgvDetail.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvDetail.Name = "dgvDetail"
        Me.dgvDetail.ReadOnly = True
        Me.dgvDetail.RowHeadersVisible = False
        Me.dgvDetail.Size = New System.Drawing.Size(1095, 255)
        Me.dgvDetail.TabIndex = 101
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(670, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 17)
        Me.Label1.TabIndex = 103
        Me.Label1.Text = "OR"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(712, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(109, 20)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "Document No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(851, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 17)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "OR"
        '
        'Doc
        '
        Me.Doc.Location = New System.Drawing.Point(715, 45)
        Me.Doc.Name = "Doc"
        Me.Doc.Size = New System.Drawing.Size(120, 22)
        Me.Doc.TabIndex = 105
        '
        'Button2
        '
        Me.Button2.Image = My.Resources.Resources.ViewHistory
        Me.Button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button2.Location = New System.Drawing.Point(1028, 788)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 24)
        Me.Button2.TabIndex = 108
        Me.Button2.Text = "adjust"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(893, 21)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 20)
        Me.Label2.TabIndex = 110
        Me.Label2.Text = "Accession ID"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Accid
        '
        Me.Accid.Location = New System.Drawing.Point(896, 47)
        Me.Accid.Name = "Accid"
        Me.Accid.Size = New System.Drawing.Size(120, 22)
        Me.Accid.TabIndex = 109
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Image = My.Resources.Resources.paste
        Me.Label10.Location = New System.Drawing.Point(998, 13)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 31)
        Me.Label10.TabIndex = 111
        Me.Label10.Text = "      "
        '
        'Accession
        '
        Me.Accession.FillWeight = 80.0!
        Me.Accession.HeaderText = "Accession"
        Me.Accession.Name = "Accession"
        Me.Accession.ReadOnly = True
        Me.Accession.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Accession.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Accession.Width = 80
        '
        'InvID
        '
        Me.InvID.FillWeight = 66.0!
        Me.InvID.HeaderText = "Invoice"
        Me.InvID.MaxInputLength = 15
        Me.InvID.Name = "InvID"
        Me.InvID.ReadOnly = True
        Me.InvID.Width = 66
        '
        'SvcDate
        '
        Me.SvcDate.FillWeight = 76.0!
        Me.SvcDate.HeaderText = "Svc Date"
        Me.SvcDate.Name = "SvcDate"
        Me.SvcDate.ReadOnly = True
        Me.SvcDate.Width = 76
        '
        'BillTo
        '
        Me.BillTo.FillWeight = 94.0!
        Me.BillTo.HeaderText = "Bill To"
        Me.BillTo.MaxInputLength = 100
        Me.BillTo.Name = "BillTo"
        Me.BillTo.ReadOnly = True
        Me.BillTo.Width = 94
        '
        'Patient
        '
        Me.Patient.FillWeight = 94.0!
        Me.Patient.HeaderText = "Patient"
        Me.Patient.MaxInputLength = 60
        Me.Patient.Name = "Patient"
        Me.Patient.ReadOnly = True
        Me.Patient.Width = 94
        '
        'BillAmount
        '
        Me.BillAmount.FillWeight = 66.0!
        Me.BillAmount.HeaderText = "Bill Amt"
        Me.BillAmount.MaxInputLength = 12
        Me.BillAmount.Name = "BillAmount"
        Me.BillAmount.ReadOnly = True
        Me.BillAmount.Width = 66
        '
        'DocNo
        '
        Me.DocNo.FillWeight = 64.0!
        Me.DocNo.HeaderText = "Doc No"
        Me.DocNo.MaxInputLength = 25
        Me.DocNo.Name = "DocNo"
        Me.DocNo.ReadOnly = True
        Me.DocNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DocNo.Width = 64
        '
        'TrnDate
        '
        Me.TrnDate.FillWeight = 74.0!
        Me.TrnDate.HeaderText = "Trn Date"
        Me.TrnDate.Name = "TrnDate"
        Me.TrnDate.ReadOnly = True
        Me.TrnDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.TrnDate.Width = 74
        '
        'Pmnt
        '
        Me.Pmnt.FillWeight = 64.0!
        Me.Pmnt.HeaderText = "Payment"
        Me.Pmnt.MaxInputLength = 12
        Me.Pmnt.Name = "Pmnt"
        Me.Pmnt.ReadOnly = True
        Me.Pmnt.Width = 64
        '
        'WO
        '
        Me.WO.FillWeight = 64.0!
        Me.WO.HeaderText = "W.O."
        Me.WO.MaxInputLength = 12
        Me.WO.Name = "WO"
        Me.WO.ReadOnly = True
        Me.WO.Width = 64
        '
        'Balance
        '
        Me.Balance.FillWeight = 70.0!
        Me.Balance.HeaderText = "Balance"
        Me.Balance.MaxInputLength = 12
        Me.Balance.Name = "Balance"
        Me.Balance.ReadOnly = True
        Me.Balance.Width = 70
        '
        'Age
        '
        Me.Age.FillWeight = 40.0!
        Me.Age.HeaderText = "Age"
        Me.Age.MaxInputLength = 5
        Me.Age.Name = "Age"
        Me.Age.ReadOnly = True
        Me.Age.Width = 40
        '
        'ERA_Processed
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(1175, 822)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Accid)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Doc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dgvDetail)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.Tod)
        Me.Controls.Add(Me.From)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.dtpPost)
        Me.Controls.Add(Me.clipboardMsg)
        Me.Controls.Add(Me.dgvClaims)
        Me.Controls.Add(Me.dgvEOBs)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "ERA_Processed"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ERA_Processed"
        CType(Me.dgvEOBs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvClaims, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvEOBs As System.Windows.Forms.DataGridView
    Friend WithEvents dgvClaims As System.Windows.Forms.DataGridView
    Friend WithEvents clipboardMsg As System.Windows.Forms.Label
    Friend WithEvents dtpPost As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents From As System.Windows.Forms.Label
    Friend WithEvents Tod As System.Windows.Forms.Label
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents dgvDetail As System.Windows.Forms.DataGridView
    Friend WithEvents Accession_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Doc As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Accid As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Document_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Date_Time As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PaymentType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Accession As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents InvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SvcDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Patient As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BillAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DocNo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TrnDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pmnt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents WO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Balance As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Age As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
