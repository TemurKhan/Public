<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhrases
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPhrases))
        Label1 = New Label()
        txtPhraseID = New TextBox()
        btnPhraseLook = New Button()
        txtPhraseKey = New TextBox()
        Label2 = New Label()
        Label3 = New Label()
        HelpProvider1 = New HelpProvider()
        cmbInputKeys = New ComboBox()
        Label4 = New Label()
        txtPhrase = New RichTextBox()
        ToolStrip1 = New ToolStrip()
        chkEditNew = New ToolStripButton()
        ToolStripSeparator6 = New ToolStripSeparator()
        btnSave = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnDelete = New ToolStripButton()
        ToolStripSeparator7 = New ToolStripSeparator()
        btnFont = New ToolStripButton()
        ToolStripButton2 = New ToolStripSeparator()
        chkBold = New ToolStripButton()
        ToolStripSeparator8 = New ToolStripSeparator()
        chkItalic = New ToolStripButton()
        ToolStripSeparator9 = New ToolStripSeparator()
        chkUnderline = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        chkLAlign = New ToolStripButton()
        chkMAlign = New ToolStripButton()
        chkRAlign = New ToolStripButton()
        ToolStripSeparator3 = New ToolStripSeparator()
        chkBList = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnFontColor = New ToolStripButton()
        ToolStripSeparator5 = New ToolStripSeparator()
        btnImageFile = New ToolStripButton()
        btnImageScan = New ToolStripButton()
        ToolStripButton8 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        cmbCatagories = New ComboBox()
        Label5 = New Label()
        FontDialog1 = New FontDialog()
        ColorDialog1 = New ColorDialog()
        OpenFileDialog1 = New OpenFileDialog()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.DarkBlue
        Label1.Location = New Point(20, 87)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(160, 25)
        Label1.TabIndex = 5
        Label1.Text = "ID"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' txtPhraseID
        ' 
        txtPhraseID.Location = New Point(20, 117)
        txtPhraseID.Margin = New Padding(5, 6, 5, 6)
        txtPhraseID.MaxLength = 5
        txtPhraseID.Name = "txtPhraseID"
        txtPhraseID.Size = New Size(157, 31)
        txtPhraseID.TabIndex = 1
        txtPhraseID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnPhraseLook
        ' 
        btnPhraseLook.Image = CType(resources.GetObject("btnPhraseLook.Image"), Image)
        btnPhraseLook.Location = New Point(190, 110)
        btnPhraseLook.Margin = New Padding(5, 6, 5, 6)
        btnPhraseLook.Name = "btnPhraseLook"
        btnPhraseLook.Size = New Size(48, 52)
        btnPhraseLook.TabIndex = 2
        btnPhraseLook.UseVisualStyleBackColor = True
        ' 
        ' txtPhraseKey
        ' 
        txtPhraseKey.Location = New Point(248, 117)
        txtPhraseKey.Margin = New Padding(5, 6, 5, 6)
        txtPhraseKey.MaxLength = 25
        txtPhraseKey.Name = "txtPhraseKey"
        txtPhraseKey.Size = New Size(417, 31)
        txtPhraseKey.TabIndex = 3
        ' 
        ' Label2
        ' 
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(268, 87)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(140, 25)
        Label2.TabIndex = 10
        Label2.Text = "Key"
        ' 
        ' Label3
        ' 
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(25, 192)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(155, 25)
        Label3.TabIndex = 11
        Label3.Text = "Phrase"
        ' 
        ' cmbInputKeys
        ' 
        cmbInputKeys.FormattingEnabled = True
        cmbInputKeys.Location = New Point(955, 117)
        cmbInputKeys.Margin = New Padding(5, 6, 5, 6)
        cmbInputKeys.Name = "cmbInputKeys"
        cmbInputKeys.Size = New Size(139, 33)
        cmbInputKeys.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.ForeColor = Color.DarkBlue
        Label4.Location = New Point(955, 87)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(142, 25)
        Label4.TabIndex = 13
        Label4.Text = "Input Key(s)"
        ' 
        ' txtPhrase
        ' 
        txtPhrase.Location = New Point(20, 223)
        txtPhrase.Margin = New Padding(5, 6, 5, 6)
        txtPhrase.Name = "txtPhrase"
        txtPhrase.Size = New Size(1074, 692)
        txtPhrase.TabIndex = 15
        txtPhrase.Text = ""
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {chkEditNew, ToolStripSeparator6, btnSave, ToolStripSeparator1, btnDelete, ToolStripSeparator7, btnFont, ToolStripButton2, chkBold, ToolStripSeparator8, chkItalic, ToolStripSeparator9, chkUnderline, ToolStripSeparator2, chkLAlign, chkMAlign, chkRAlign, ToolStripSeparator3, chkBList, ToolStripSeparator4, btnFontColor, ToolStripSeparator5, btnImageFile, btnImageScan, ToolStripButton8, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(1125, 34)
        ToolStrip1.TabIndex = 16
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' chkEditNew
        ' 
        chkEditNew.CheckOnClick = True
        chkEditNew.Image = CType(resources.GetObject("chkEditNew.Image"), Image)
        chkEditNew.ImageTransparentColor = Color.Magenta
        chkEditNew.Name = "chkEditNew"
        chkEditNew.Size = New Size(70, 29)
        chkEditNew.Text = "Edit"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(6, 34)
        ' 
        ' btnSave
        ' 
        btnSave.Image = CType(resources.GetObject("btnSave.Image"), Image)
        btnSave.ImageTransparentColor = Color.Magenta
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(77, 29)
        btnSave.Text = "Save"
        btnSave.ToolTipText = "Save the Note"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnDelete
        ' 
        btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), Image)
        btnDelete.ImageTransparentColor = Color.Magenta
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(90, 29)
        btnDelete.Text = "Delete"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(6, 34)
        ' 
        ' btnFont
        ' 
        btnFont.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnFont.Image = CType(resources.GetObject("btnFont.Image"), Image)
        btnFont.ImageTransparentColor = Color.Magenta
        btnFont.Name = "btnFont"
        btnFont.Size = New Size(34, 29)
        btnFont.ToolTipText = "Font Type"
        ' 
        ' ToolStripButton2
        ' 
        ToolStripButton2.Name = "ToolStripButton2"
        ToolStripButton2.Size = New Size(6, 34)
        ' 
        ' chkBold
        ' 
        chkBold.CheckOnClick = True
        chkBold.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkBold.Image = CType(resources.GetObject("chkBold.Image"), Image)
        chkBold.ImageTransparentColor = Color.Magenta
        chkBold.Name = "chkBold"
        chkBold.Size = New Size(34, 29)
        chkBold.ToolTipText = "Bold"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(6, 34)
        ' 
        ' chkItalic
        ' 
        chkItalic.CheckOnClick = True
        chkItalic.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkItalic.Image = CType(resources.GetObject("chkItalic.Image"), Image)
        chkItalic.ImageTransparentColor = Color.Magenta
        chkItalic.Name = "chkItalic"
        chkItalic.Size = New Size(34, 29)
        chkItalic.ToolTipText = "Italic"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(6, 34)
        ' 
        ' chkUnderline
        ' 
        chkUnderline.CheckOnClick = True
        chkUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkUnderline.Image = CType(resources.GetObject("chkUnderline.Image"), Image)
        chkUnderline.ImageTransparentColor = Color.Magenta
        chkUnderline.Name = "chkUnderline"
        chkUnderline.Size = New Size(34, 29)
        chkUnderline.ToolTipText = "Underline"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
        ' 
        ' chkLAlign
        ' 
        chkLAlign.Checked = True
        chkLAlign.CheckOnClick = True
        chkLAlign.CheckState = CheckState.Checked
        chkLAlign.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkLAlign.Image = CType(resources.GetObject("chkLAlign.Image"), Image)
        chkLAlign.ImageTransparentColor = Color.Magenta
        chkLAlign.Name = "chkLAlign"
        chkLAlign.Size = New Size(34, 29)
        chkLAlign.ToolTipText = "Left Align"
        ' 
        ' chkMAlign
        ' 
        chkMAlign.CheckOnClick = True
        chkMAlign.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkMAlign.Image = CType(resources.GetObject("chkMAlign.Image"), Image)
        chkMAlign.ImageTransparentColor = Color.Magenta
        chkMAlign.Name = "chkMAlign"
        chkMAlign.Size = New Size(34, 29)
        chkMAlign.ToolTipText = "Center Align"
        ' 
        ' chkRAlign
        ' 
        chkRAlign.CheckOnClick = True
        chkRAlign.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkRAlign.Image = CType(resources.GetObject("chkRAlign.Image"), Image)
        chkRAlign.ImageTransparentColor = Color.Magenta
        chkRAlign.Name = "chkRAlign"
        chkRAlign.Size = New Size(34, 29)
        chkRAlign.ToolTipText = "Right Align"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(6, 34)
        ' 
        ' chkBList
        ' 
        chkBList.CheckOnClick = True
        chkBList.DisplayStyle = ToolStripItemDisplayStyle.Image
        chkBList.Image = CType(resources.GetObject("chkBList.Image"), Image)
        chkBList.ImageTransparentColor = Color.Magenta
        chkBList.Name = "chkBList"
        chkBList.Size = New Size(34, 29)
        chkBList.ToolTipText = "Bullet List"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(6, 34)
        ' 
        ' btnFontColor
        ' 
        btnFontColor.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnFontColor.Image = CType(resources.GetObject("btnFontColor.Image"), Image)
        btnFontColor.ImageTransparentColor = Color.Magenta
        btnFontColor.Name = "btnFontColor"
        btnFontColor.Size = New Size(34, 29)
        btnFontColor.ToolTipText = "Font Color"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(6, 34)
        ' 
        ' btnImageFile
        ' 
        btnImageFile.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnImageFile.Image = CType(resources.GetObject("btnImageFile.Image"), Image)
        btnImageFile.ImageTransparentColor = Color.Magenta
        btnImageFile.Name = "btnImageFile"
        btnImageFile.Size = New Size(34, 29)
        btnImageFile.ToolTipText = "Image from File"
        ' 
        ' btnImageScan
        ' 
        btnImageScan.DisplayStyle = ToolStripItemDisplayStyle.Image
        btnImageScan.Image = CType(resources.GetObject("btnImageScan.Image"), Image)
        btnImageScan.ImageTransparentColor = Color.Magenta
        btnImageScan.Name = "btnImageScan"
        btnImageScan.Size = New Size(34, 29)
        btnImageScan.ToolTipText = "Image from Camera"
        ' 
        ' ToolStripButton8
        ' 
        ToolStripButton8.Name = "ToolStripButton8"
        ToolStripButton8.Size = New Size(6, 34)
        ' 
        ' btnHelp
        ' 
        btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), Image)
        btnHelp.ImageTransparentColor = Color.Magenta
        btnHelp.Name = "btnHelp"
        btnHelp.Size = New Size(77, 29)
        btnHelp.Text = "Help"
        ' 
        ' cmbCatagories
        ' 
        cmbCatagories.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCatagories.FormattingEnabled = True
        cmbCatagories.Location = New Point(702, 115)
        cmbCatagories.Margin = New Padding(5, 6, 5, 6)
        cmbCatagories.Name = "cmbCatagories"
        cmbCatagories.Size = New Size(227, 33)
        cmbCatagories.TabIndex = 17
        ' 
        ' Label5
        ' 
        Label5.ForeColor = Color.DarkBlue
        Label5.Location = New Point(715, 85)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(140, 25)
        Label5.TabIndex = 18
        Label5.Text = "Catagory"
        ' 
        ' OpenFileDialog1
        ' 
        OpenFileDialog1.FileName = "OpenFileDialog1"
        ' 
        ' frmPhrases
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1125, 942)
        Controls.Add(Label5)
        Controls.Add(cmbCatagories)
        Controls.Add(ToolStrip1)
        Controls.Add(txtPhrase)
        Controls.Add(Label4)
        Controls.Add(cmbInputKeys)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(txtPhraseKey)
        Controls.Add(btnPhraseLook)
        Controls.Add(txtPhraseID)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPhrases"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Phrase Management"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPhraseID As System.Windows.Forms.TextBox
    Friend WithEvents btnPhraseLook As System.Windows.Forms.Button
    Friend WithEvents txtPhraseKey As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents cmbInputKeys As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPhrase As System.Windows.Forms.RichTextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkEditNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnFont As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkBold As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkItalic As System.Windows.Forms.ToolStripButton
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
    Friend WithEvents btnImageFile As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnImageScan As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbCatagories As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class
