<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCRCopy
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCRCopy))
        Me.chkOutIn = New System.Windows.Forms.CheckBox
        Me.btnLoad = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblComponent = New System.Windows.Forms.Label
        Me.lstFields = New System.Windows.Forms.ListBox
        Me.cmbFlag = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbDelimiter = New System.Windows.Forms.ComboBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.btnOK = New System.Windows.Forms.ToolStripButton
        Me.txtFile = New System.Windows.Forms.TextBox
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkOutIn
        '
        Me.chkOutIn.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkOutIn.Location = New System.Drawing.Point(167, 47)
        Me.chkOutIn.Name = "chkOutIn"
        Me.chkOutIn.Size = New System.Drawing.Size(133, 27)
        Me.chkOutIn.TabIndex = 26
        Me.chkOutIn.Text = "Delimited Text File"
        Me.chkOutIn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.chkOutIn.UseVisualStyleBackColor = True
        '
        'btnLoad
        '
        Me.btnLoad.Image = CType(resources.GetObject("btnLoad.Image"), System.Drawing.Image)
        Me.btnLoad.Location = New System.Drawing.Point(391, 159)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(32, 24)
        Me.btnLoad.TabIndex = 25
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(12, 170)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Field To Copy"
        '
        'lblComponent
        '
        Me.lblComponent.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblComponent.Location = New System.Drawing.Point(207, 116)
        Me.lblComponent.Name = "lblComponent"
        Me.lblComponent.Size = New System.Drawing.Size(179, 13)
        Me.lblComponent.TabIndex = 23
        Me.lblComponent.Text = "Default Refluxed Component"
        '
        'lstFields
        '
        Me.lstFields.FormattingEnabled = True
        Me.lstFields.Location = New System.Drawing.Point(12, 185)
        Me.lstFields.Name = "lstFields"
        Me.lstFields.Size = New System.Drawing.Size(411, 121)
        Me.lstFields.TabIndex = 22
        '
        'cmbFlag
        '
        Me.cmbFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFlag.FormattingEnabled = True
        Me.cmbFlag.Items.AddRange(New Object() {"A - Abnormal", "I - Ignore", "N - Normal", "P - Panic", "R - Repeat"})
        Me.cmbFlag.Location = New System.Drawing.Point(210, 132)
        Me.cmbFlag.Name = "cmbFlag"
        Me.cmbFlag.Size = New System.Drawing.Size(214, 21)
        Me.cmbFlag.Sorted = True
        Me.cmbFlag.TabIndex = 21
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(13, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Delimiter"
        '
        'cmbDelimiter
        '
        Me.cmbDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDelimiter.FormattingEnabled = True
        Me.cmbDelimiter.Items.AddRange(New Object() {"Comma (,)", "TAB (" & Global.Microsoft.VisualBasic.ChrW(9) & ")", "Pipe (|)", "CR + LF"})
        Me.cmbDelimiter.Location = New System.Drawing.Point(13, 132)
        Me.cmbDelimiter.Name = "cmbDelimiter"
        Me.cmbDelimiter.Size = New System.Drawing.Size(191, 21)
        Me.cmbDelimiter.TabIndex = 19
        '
        'btnBrowse
        '
        Me.btnBrowse.Image = CType(resources.GetObject("btnBrowse.Image"), System.Drawing.Image)
        Me.btnBrowse.Location = New System.Drawing.Point(392, 85)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(32, 24)
        Me.btnBrowse.TabIndex = 18
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(12, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(152, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Source File"
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(59, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Enabled = False
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(60, 22)
        Me.btnOK.Text = "Accept"
        '
        'txtFile
        '
        Me.txtFile.Location = New System.Drawing.Point(13, 88)
        Me.txtFile.Name = "txtFile"
        Me.txtFile.ReadOnly = True
        Me.txtFile.Size = New System.Drawing.Size(373, 20)
        Me.txtFile.TabIndex = 16
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnOK, Me.ToolStripSeparator1, Me.btnCancel, Me.ToolStripSeparator2, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(435, 25)
        Me.ToolStrip1.TabIndex = 15
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(48, 22)
        Me.btnHelp.Text = "Help"
        '
        'frmCRCopy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 315)
        Me.Controls.Add(Me.chkOutIn)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblComponent)
        Me.Controls.Add(Me.lstFields)
        Me.Controls.Add(Me.cmbFlag)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDelimiter)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFile)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCRCopy"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Copy Result Choices"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents chkOutIn As System.Windows.Forms.CheckBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblComponent As System.Windows.Forms.Label
    Friend WithEvents lstFields As System.Windows.Forms.ListBox
    Friend WithEvents cmbFlag As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbDelimiter As System.Windows.Forms.ComboBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnOK As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtFile As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton

End Class
