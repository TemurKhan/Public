<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintReqs
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintReqs))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.chkCusGen = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnPrint = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtProvID = New System.Windows.Forms.TextBox
        Me.btnProvLook = New System.Windows.Forms.Button
        Me.txtProvName = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPrintName = New System.Windows.Forms.RichTextBox
        Me.txtComment = New System.Windows.Forms.RichTextBox
        Me.btnTForm = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbAttend = New System.Windows.Forms.ComboBox
        Me.txtCSZ = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.btnNForm = New System.Windows.Forms.Button
        Me.txtReqID = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label8 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.chkCusGen, Me.ToolStripSeparator2, Me.btnPrint, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator4, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(734, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'chkCusGen
        '
        Me.chkCusGen.AutoSize = False
        Me.chkCusGen.CheckOnClick = True
        Me.chkCusGen.Image = CType(resources.GetObject("chkCusGen.Image"), System.Drawing.Image)
        Me.chkCusGen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkCusGen.Name = "chkCusGen"
        Me.chkCusGen.Size = New System.Drawing.Size(80, 22)
        Me.chkCusGen.Text = "Generic"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.AutoSize = False
        Me.btnPrint.Enabled = False
        Me.btnPrint.ForeColor = System.Drawing.Color.DarkBlue
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(80, 22)
        Me.btnPrint.Text = "Print"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.btnCancel.Size = New System.Drawing.Size(65, 22)
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
        Me.btnHelp.Size = New System.Drawing.Size(65, 22)
        Me.btnHelp.Text = "Help"
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(14, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 15)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Provider ID"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtProvID
        '
        Me.txtProvID.Location = New System.Drawing.Point(12, 57)
        Me.txtProvID.MaxLength = 9
        Me.txtProvID.Name = "txtProvID"
        Me.txtProvID.Size = New System.Drawing.Size(78, 20)
        Me.txtProvID.TabIndex = 10
        Me.txtProvID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnProvLook
        '
        Me.btnProvLook.Image = CType(resources.GetObject("btnProvLook.Image"), System.Drawing.Image)
        Me.btnProvLook.Location = New System.Drawing.Point(96, 52)
        Me.btnProvLook.Name = "btnProvLook"
        Me.btnProvLook.Size = New System.Drawing.Size(31, 29)
        Me.btnProvLook.TabIndex = 11
        Me.btnProvLook.UseVisualStyleBackColor = True
        '
        'txtProvName
        '
        Me.txtProvName.Location = New System.Drawing.Point(133, 57)
        Me.txtProvName.MaxLength = 60
        Me.txtProvName.Name = "txtProvName"
        Me.txtProvName.ReadOnly = True
        Me.txtProvName.Size = New System.Drawing.Size(292, 20)
        Me.txtProvName.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(133, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Provider Name"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(665, 310)
        Me.txtQty.MaxLength = 3
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(55, 20)
        Me.txtQty.TabIndex = 15
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(14, 144)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 15)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Name and Address to print"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(12, 109)
        Me.txtAddress.MaxLength = 155
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.ReadOnly = True
        Me.txtAddress.Size = New System.Drawing.Size(390, 20)
        Me.txtAddress.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(14, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Address"
        '
        'txtPrintName
        '
        Me.txtPrintName.Location = New System.Drawing.Point(12, 162)
        Me.txtPrintName.MaxLength = 4000
        Me.txtPrintName.Name = "txtPrintName"
        Me.txtPrintName.Size = New System.Drawing.Size(708, 139)
        Me.txtPrintName.TabIndex = 18
        Me.txtPrintName.Text = ""
        '
        'txtComment
        '
        Me.txtComment.Location = New System.Drawing.Point(12, 337)
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(708, 121)
        Me.txtComment.TabIndex = 20
        Me.txtComment.Text = ""
        '
        'btnTForm
        '
        Me.btnTForm.Image = CType(resources.GetObject("btnTForm.Image"), System.Drawing.Image)
        Me.btnTForm.Location = New System.Drawing.Point(143, 307)
        Me.btnTForm.Name = "btnTForm"
        Me.btnTForm.Size = New System.Drawing.Size(24, 24)
        Me.btnTForm.TabIndex = 21
        Me.btnTForm.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label5.Location = New System.Drawing.Point(616, 313)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 15)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Qty"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(14, 313)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 15)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Tests and Comment"
        '
        'cmbAttend
        '
        Me.cmbAttend.FormattingEnabled = True
        Me.cmbAttend.Location = New System.Drawing.Point(446, 57)
        Me.cmbAttend.Name = "cmbAttend"
        Me.cmbAttend.Size = New System.Drawing.Size(274, 21)
        Me.cmbAttend.TabIndex = 24
        '
        'txtCSZ
        '
        Me.txtCSZ.Location = New System.Drawing.Point(408, 109)
        Me.txtCSZ.MaxLength = 155
        Me.txtCSZ.Name = "txtCSZ"
        Me.txtCSZ.ReadOnly = True
        Me.txtCSZ.Size = New System.Drawing.Size(312, 20)
        Me.txtCSZ.TabIndex = 25
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(443, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(152, 15)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Attending Provider"
        '
        'btnNForm
        '
        Me.btnNForm.Image = CType(resources.GetObject("btnNForm.Image"), System.Drawing.Image)
        Me.btnNForm.Location = New System.Drawing.Point(696, 135)
        Me.btnNForm.Name = "btnNForm"
        Me.btnNForm.Size = New System.Drawing.Size(24, 24)
        Me.btnNForm.TabIndex = 27
        Me.btnNForm.UseVisualStyleBackColor = True
        '
        'txtReqID
        '
        Me.txtReqID.Location = New System.Drawing.Point(384, 310)
        Me.txtReqID.MaxLength = 4
        Me.txtReqID.Name = "txtReqID"
        Me.txtReqID.Size = New System.Drawing.Size(49, 20)
        Me.txtReqID.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.txtReqID, "Leave the default value to print Requisition ID and clear to suppress")
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label8.Location = New System.Drawing.Point(285, 313)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(91, 15)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Requisition Seed"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmPrintReqs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 471)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtReqID)
        Me.Controls.Add(Me.btnNForm)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtCSZ)
        Me.Controls.Add(Me.cmbAttend)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnTForm)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.txtPrintName)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtProvName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnProvLook)
        Me.Controls.Add(Me.txtProvID)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(740, 496)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(740, 484)
        Me.Name = "frmPrintReqs"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Request Forms"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkCusGen As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProvID As System.Windows.Forms.TextBox
    Friend WithEvents btnProvLook As System.Windows.Forms.Button
    Friend WithEvents txtProvName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPrintName As System.Windows.Forms.RichTextBox
    Friend WithEvents txtComment As System.Windows.Forms.RichTextBox
    Friend WithEvents btnTForm As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbAttend As System.Windows.Forms.ComboBox
    Friend WithEvents txtCSZ As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents btnNForm As System.Windows.Forms.Button
    Friend WithEvents txtReqID As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label8 As System.Windows.Forms.Label

End Class
