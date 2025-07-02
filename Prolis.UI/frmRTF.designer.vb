<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRTF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRTF))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnSave = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnFont = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripSeparator
        Me.chkBold = New System.Windows.Forms.ToolStripButton
        Me.chkItalic = New System.Windows.Forms.ToolStripButton
        Me.chkUnderline = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.chkLAlign = New System.Windows.Forms.ToolStripButton
        Me.chkMAlign = New System.Windows.Forms.ToolStripButton
        Me.chkRAlign = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.chkBList = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.btnFontColor = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.txtRTF = New System.Windows.Forms.RichTextBox
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.btnPhraseLook = New System.Windows.Forms.Button
        Me.pctImg = New System.Windows.Forms.PictureBox
        Me.btnImgAdd = New System.Windows.Forms.Button
        Me.btnImgDelete = New System.Windows.Forms.Button
        Me.ToolStrip1.SuspendLayout()
        CType(Me.pctImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.ToolStripSeparator1, Me.btnFont, Me.ToolStripButton2, Me.chkBold, Me.chkItalic, Me.chkUnderline, Me.ToolStripSeparator2, Me.chkLAlign, Me.chkMAlign, Me.chkRAlign, Me.ToolStripSeparator3, Me.chkBList, Me.ToolStripSeparator4, Me.btnFontColor, Me.ToolStripSeparator5, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(652, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.AutoSize = False
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(55, 22)
        Me.btnSave.Text = "Save"
        Me.btnSave.ToolTipText = "Save the Note"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnFont
        '
        Me.btnFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFont.Image = CType(resources.GetObject("btnFont.Image"), System.Drawing.Image)
        Me.btnFont.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFont.Name = "btnFont"
        Me.btnFont.Size = New System.Drawing.Size(23, 22)
        Me.btnFont.ToolTipText = "Font Type"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(6, 25)
        '
        'chkBold
        '
        Me.chkBold.CheckOnClick = True
        Me.chkBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkBold.Image = CType(resources.GetObject("chkBold.Image"), System.Drawing.Image)
        Me.chkBold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkBold.Name = "chkBold"
        Me.chkBold.Size = New System.Drawing.Size(23, 22)
        Me.chkBold.ToolTipText = "Bold"
        '
        'chkItalic
        '
        Me.chkItalic.CheckOnClick = True
        Me.chkItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkItalic.Image = CType(resources.GetObject("chkItalic.Image"), System.Drawing.Image)
        Me.chkItalic.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkItalic.Name = "chkItalic"
        Me.chkItalic.Size = New System.Drawing.Size(23, 22)
        Me.chkItalic.ToolTipText = "Italic"
        '
        'chkUnderline
        '
        Me.chkUnderline.CheckOnClick = True
        Me.chkUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkUnderline.Image = CType(resources.GetObject("chkUnderline.Image"), System.Drawing.Image)
        Me.chkUnderline.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkUnderline.Name = "chkUnderline"
        Me.chkUnderline.Size = New System.Drawing.Size(23, 22)
        Me.chkUnderline.ToolTipText = "Underline"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'chkLAlign
        '
        Me.chkLAlign.Checked = True
        Me.chkLAlign.CheckOnClick = True
        Me.chkLAlign.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkLAlign.Image = CType(resources.GetObject("chkLAlign.Image"), System.Drawing.Image)
        Me.chkLAlign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkLAlign.Name = "chkLAlign"
        Me.chkLAlign.Size = New System.Drawing.Size(23, 22)
        Me.chkLAlign.ToolTipText = "Left Align"
        '
        'chkMAlign
        '
        Me.chkMAlign.CheckOnClick = True
        Me.chkMAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkMAlign.Image = CType(resources.GetObject("chkMAlign.Image"), System.Drawing.Image)
        Me.chkMAlign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkMAlign.Name = "chkMAlign"
        Me.chkMAlign.Size = New System.Drawing.Size(23, 22)
        Me.chkMAlign.ToolTipText = "Center Align"
        '
        'chkRAlign
        '
        Me.chkRAlign.CheckOnClick = True
        Me.chkRAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkRAlign.Image = CType(resources.GetObject("chkRAlign.Image"), System.Drawing.Image)
        Me.chkRAlign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkRAlign.Name = "chkRAlign"
        Me.chkRAlign.Size = New System.Drawing.Size(23, 22)
        Me.chkRAlign.ToolTipText = "Right Align"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'chkBList
        '
        Me.chkBList.CheckOnClick = True
        Me.chkBList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.chkBList.Image = CType(resources.GetObject("chkBList.Image"), System.Drawing.Image)
        Me.chkBList.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.chkBList.Name = "chkBList"
        Me.chkBList.Size = New System.Drawing.Size(23, 22)
        Me.chkBList.ToolTipText = "Bullet List"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnFontColor
        '
        Me.btnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFontColor.Image = CType(resources.GetObject("btnFontColor.Image"), System.Drawing.Image)
        Me.btnFontColor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFontColor.Name = "btnFontColor"
        Me.btnFontColor.Size = New System.Drawing.Size(23, 22)
        Me.btnFontColor.ToolTipText = "Font Color"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "Help"
        '
        'txtRTF
        '
        Me.txtRTF.Location = New System.Drawing.Point(14, 77)
        Me.txtRTF.Name = "txtRTF"
        Me.txtRTF.Size = New System.Drawing.Size(434, 198)
        Me.txtRTF.TabIndex = 11
        Me.txtRTF.Text = ""
        '
        'FontDialog1
        '
        Me.FontDialog1.AllowSimulations = False
        Me.FontDialog1.AllowVectorFonts = False
        Me.FontDialog1.AllowVerticalFonts = False
        Me.FontDialog1.ShowEffects = False
        Me.FontDialog1.ShowHelp = True
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.SolidColorOnly = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnPhraseLook
        '
        Me.btnPhraseLook.Image = CType(resources.GetObject("btnPhraseLook.Image"), System.Drawing.Image)
        Me.btnPhraseLook.Location = New System.Drawing.Point(300, 41)
        Me.btnPhraseLook.Name = "btnPhraseLook"
        Me.btnPhraseLook.Size = New System.Drawing.Size(148, 30)
        Me.btnPhraseLook.TabIndex = 15
        Me.btnPhraseLook.Text = "Phrase Lookup"
        Me.btnPhraseLook.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPhraseLook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnPhraseLook.UseVisualStyleBackColor = True
        '
        'pctImg
        '
        Me.pctImg.Location = New System.Drawing.Point(463, 77)
        Me.pctImg.Name = "pctImg"
        Me.pctImg.Size = New System.Drawing.Size(177, 130)
        Me.pctImg.TabIndex = 19
        Me.pctImg.TabStop = False
        '
        'btnImgAdd
        '
        Me.btnImgAdd.Image = CType(resources.GetObject("btnImgAdd.Image"), System.Drawing.Image)
        Me.btnImgAdd.Location = New System.Drawing.Point(578, 45)
        Me.btnImgAdd.Name = "btnImgAdd"
        Me.btnImgAdd.Size = New System.Drawing.Size(28, 23)
        Me.btnImgAdd.TabIndex = 20
        Me.btnImgAdd.UseVisualStyleBackColor = True
        '
        'btnImgDelete
        '
        Me.btnImgDelete.Image = CType(resources.GetObject("btnImgDelete.Image"), System.Drawing.Image)
        Me.btnImgDelete.Location = New System.Drawing.Point(612, 45)
        Me.btnImgDelete.Name = "btnImgDelete"
        Me.btnImgDelete.Size = New System.Drawing.Size(28, 23)
        Me.btnImgDelete.TabIndex = 21
        Me.btnImgDelete.UseVisualStyleBackColor = True
        '
        'frmRTF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 287)
        Me.Controls.Add(Me.btnImgDelete)
        Me.Controls.Add(Me.btnImgAdd)
        Me.Controls.Add(Me.pctImg)
        Me.Controls.Add(Me.btnPhraseLook)
        Me.Controls.Add(Me.txtRTF)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRTF"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Extended Result"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.pctImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtRTF As System.Windows.Forms.RichTextBox
    Friend WithEvents chkBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkItalic As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkUnderline As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkLAlign As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkMAlign As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkRAlign As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkBList As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnFontColor As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnFont As System.Windows.Forms.ToolStripButton
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnPhraseLook As System.Windows.Forms.Button
    Friend WithEvents pctImg As System.Windows.Forms.PictureBox
    Friend WithEvents btnImgAdd As System.Windows.Forms.Button
    Friend WithEvents btnImgDelete As System.Windows.Forms.Button

End Class
