<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccPayment))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnAccept = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.cmbPaymentType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDoc = New System.Windows.Forms.Label()
        Me.txtDoc = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAccCharge = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.gbCC = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtBillZip = New System.Windows.Forms.TextBox()
        Me.txtCardHolder = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtExp = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtCVV = New System.Windows.Forms.TextBox()
        Me.txtCCNo = New System.Windows.Forms.TextBox()
        Me.btnProcessCard = New System.Windows.Forms.Button()
        Me.chkReadMan = New System.Windows.Forms.CheckBox()
        Me.lblnotify = New System.Windows.Forms.Label()
        Me.txtCardinfo = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.gbCC.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(358, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.AutoSize = False
        Me.btnAccept.Enabled = False
        Me.btnAccept.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(80, 22)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.AutoSize = False
        Me.btnCancel.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(80, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.AutoSize = False
        Me.btnHelp.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(80, 22)
        Me.btnHelp.Text = "Help"
        '
        'cmbPaymentType
        '
        Me.cmbPaymentType.FormattingEnabled = True
        Me.cmbPaymentType.Items.AddRange(New Object() {"CASH", "CHECK / MONEY ORDER", "CREDIT CARD"})
        Me.cmbPaymentType.Location = New System.Drawing.Point(12, 53)
        Me.cmbPaymentType.Name = "cmbPaymentType"
        Me.cmbPaymentType.Size = New System.Drawing.Size(150, 21)
        Me.cmbPaymentType.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(17, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Payment Type"
        '
        'lblDoc
        '
        Me.lblDoc.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblDoc.Location = New System.Drawing.Point(17, 85)
        Me.lblDoc.Name = "lblDoc"
        Me.lblDoc.Size = New System.Drawing.Size(63, 15)
        Me.lblDoc.TabIndex = 11
        Me.lblDoc.Text = "Doc No"
        '
        'txtDoc
        '
        Me.txtDoc.Location = New System.Drawing.Point(12, 103)
        Me.txtDoc.MaxLength = 100
        Me.txtDoc.Name = "txtDoc"
        Me.txtDoc.Size = New System.Drawing.Size(232, 20)
        Me.txtDoc.TabIndex = 7
        '
        'txtDate
        '
        Me.txtDate.Location = New System.Drawing.Point(168, 54)
        Me.txtDate.Mask = "00/00/0000"
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(76, 20)
        Me.txtDate.TabIndex = 5
        Me.txtDate.ValidatingType = GetType(Date)
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(250, 103)
        Me.txtAmount.MaxLength = 15
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(96, 20)
        Me.txtAmount.TabIndex = 8
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(168, 35)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Payment Date"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(250, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 15)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Amount"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtAccCharge
        '
        Me.txtAccCharge.Location = New System.Drawing.Point(250, 53)
        Me.txtAccCharge.MaxLength = 20
        Me.txtAccCharge.Name = "txtAccCharge"
        Me.txtAccCharge.Size = New System.Drawing.Size(96, 20)
        Me.txtAccCharge.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(258, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 15)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Accession"
        '
        'gbCC
        '
        Me.gbCC.Controls.Add(Me.Label9)
        Me.gbCC.Controls.Add(Me.txtBillZip)
        Me.gbCC.Controls.Add(Me.txtCardHolder)
        Me.gbCC.Controls.Add(Me.Label8)
        Me.gbCC.Controls.Add(Me.Label7)
        Me.gbCC.Controls.Add(Me.Label6)
        Me.gbCC.Controls.Add(Me.txtExp)
        Me.gbCC.Controls.Add(Me.Label3)
        Me.gbCC.Controls.Add(Me.txtCVV)
        Me.gbCC.Controls.Add(Me.txtCCNo)
        Me.gbCC.Controls.Add(Me.btnProcessCard)
        Me.gbCC.Location = New System.Drawing.Point(12, 154)
        Me.gbCC.Name = "gbCC"
        Me.gbCC.Size = New System.Drawing.Size(334, 124)
        Me.gbCC.TabIndex = 20
        Me.gbCC.TabStop = False
        Me.gbCC.Text = "Credit Card Info"
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label9.Location = New System.Drawing.Point(159, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(73, 15)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "Billing Zip"
        '
        'txtBillZip
        '
        Me.txtBillZip.Location = New System.Drawing.Point(159, 85)
        Me.txtBillZip.MaxLength = 10
        Me.txtBillZip.Name = "txtBillZip"
        Me.txtBillZip.ReadOnly = True
        Me.txtBillZip.Size = New System.Drawing.Size(87, 20)
        Me.txtBillZip.TabIndex = 14
        '
        'txtCardHolder
        '
        Me.txtCardHolder.Location = New System.Drawing.Point(8, 85)
        Me.txtCardHolder.MaxLength = 70
        Me.txtCardHolder.Name = "txtCardHolder"
        Me.txtCardHolder.ReadOnly = True
        Me.txtCardHolder.Size = New System.Drawing.Size(142, 20)
        Me.txtCardHolder.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(18, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 15)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Name on Card"
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(283, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 15)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "CVV"
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(196, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 15)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Exp (MM/YYYY)"
        '
        'txtExp
        '
        Me.txtExp.HidePromptOnLeave = True
        Me.txtExp.Location = New System.Drawing.Point(207, 34)
        Me.txtExp.Mask = "00/0000"
        Me.txtExp.Name = "txtExp"
        Me.txtExp.ReadOnly = True
        Me.txtExp.Size = New System.Drawing.Size(62, 20)
        Me.txtExp.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(18, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 15)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Credit Card #"
        '
        'txtCVV
        '
        Me.txtCVV.Location = New System.Drawing.Point(275, 34)
        Me.txtCVV.MaxLength = 4
        Me.txtCVV.Name = "txtCVV"
        Me.txtCVV.ReadOnly = True
        Me.txtCVV.Size = New System.Drawing.Size(52, 20)
        Me.txtCVV.TabIndex = 12
        '
        'txtCCNo
        '
        Me.txtCCNo.Location = New System.Drawing.Point(8, 34)
        Me.txtCCNo.MaxLength = 20
        Me.txtCCNo.Name = "txtCCNo"
        Me.txtCCNo.ReadOnly = True
        Me.txtCCNo.Size = New System.Drawing.Size(183, 20)
        Me.txtCCNo.TabIndex = 10
        '
        'btnProcessCard
        '
        Me.btnProcessCard.Image = CType(resources.GetObject("btnProcessCard.Image"), System.Drawing.Image)
        Me.btnProcessCard.Location = New System.Drawing.Point(253, 83)
        Me.btnProcessCard.Name = "btnProcessCard"
        Me.btnProcessCard.Size = New System.Drawing.Size(75, 23)
        Me.btnProcessCard.TabIndex = 15
        Me.btnProcessCard.Text = "Process"
        Me.btnProcessCard.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProcessCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnProcessCard.UseVisualStyleBackColor = True
        '
        'chkReadMan
        '
        Me.chkReadMan.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkReadMan.Location = New System.Drawing.Point(146, 129)
        Me.chkReadMan.Name = "chkReadMan"
        Me.chkReadMan.Size = New System.Drawing.Size(67, 23)
        Me.chkReadMan.TabIndex = 9
        Me.chkReadMan.Text = "Reader"
        Me.chkReadMan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkReadMan.UseVisualStyleBackColor = True
        '
        'lblnotify
        '
        Me.lblnotify.BackColor = System.Drawing.Color.Red
        Me.lblnotify.ForeColor = System.Drawing.Color.White
        Me.lblnotify.Location = New System.Drawing.Point(223, 134)
        Me.lblnotify.Name = "lblnotify"
        Me.lblnotify.Size = New System.Drawing.Size(116, 15)
        Me.lblnotify.TabIndex = 29
        Me.lblnotify.Text = "Waiting the Card dip"
        Me.lblnotify.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblnotify.Visible = False
        '
        'txtCardinfo
        '
        Me.txtCardinfo.BackColor = System.Drawing.SystemColors.Control
        Me.txtCardinfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCardinfo.ForeColor = System.Drawing.Color.Blue
        Me.txtCardinfo.Location = New System.Drawing.Point(20, 134)
        Me.txtCardinfo.MaxLength = 45000
        Me.txtCardinfo.Name = "txtCardinfo"
        Me.txtCardinfo.Size = New System.Drawing.Size(96, 13)
        Me.txtCardinfo.TabIndex = 30
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'frmAccPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 290)
        Me.Controls.Add(Me.txtCardinfo)
        Me.Controls.Add(Me.lblnotify)
        Me.Controls.Add(Me.chkReadMan)
        Me.Controls.Add(Me.gbCC)
        Me.Controls.Add(Me.txtAccCharge)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.txtDate)
        Me.Controls.Add(Me.txtDoc)
        Me.Controls.Add(Me.lblDoc)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbPaymentType)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAccPayment"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Accession Payment"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.gbCC.ResumeLayout(False)
        Me.gbCC.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbPaymentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDoc As System.Windows.Forms.Label
    Friend WithEvents txtDoc As System.Windows.Forms.TextBox
    Friend WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAccCharge As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gbCC As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBillZip As System.Windows.Forms.TextBox
    Friend WithEvents txtCardHolder As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtExp As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCVV As System.Windows.Forms.TextBox
    Friend WithEvents txtCCNo As System.Windows.Forms.TextBox
    Friend WithEvents btnProcessCard As System.Windows.Forms.Button
    Friend WithEvents chkReadMan As System.Windows.Forms.CheckBox
    Friend WithEvents lblnotify As System.Windows.Forms.Label
    Friend WithEvents txtCardinfo As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
