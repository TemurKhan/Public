<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPhraseLook
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPhraseLook))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        ToolStrip1 = New ToolStrip()
        btnAccept = New ToolStripButton()
        ToolStripSeparator1 = New ToolStripSeparator()
        btnPhrases = New ToolStripButton()
        ToolStripSeparator2 = New ToolStripSeparator()
        btnCancel = New ToolStripButton()
        ToolStripSeparator4 = New ToolStripSeparator()
        btnHelp = New ToolStripButton()
        GroupBox1 = New GroupBox()
        Label3 = New Label()
        cmbType = New ComboBox()
        txtSearch = New MaskedTextBox()
        Label2 = New Label()
        cmbSearch = New ComboBox()
        btnSearch = New Button()
        Label1 = New Label()
        dgvPhrases = New DataGridView()
        PhraseID = New DataGridViewTextBoxColumn()
        phrasekey = New DataGridViewTextBoxColumn()
        Phrase = New DataGridViewTextBoxColumn()
        ToolStrip1.SuspendLayout()
        GroupBox1.SuspendLayout()
        CType(dgvPhrases, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(24, 24)
        ToolStrip1.Items.AddRange(New ToolStripItem() {btnAccept, ToolStripSeparator1, btnPhrases, ToolStripSeparator2, btnCancel, ToolStripSeparator4, btnHelp})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Padding = New Padding(0, 0, 3, 0)
        ToolStrip1.Size = New Size(882, 34)
        ToolStrip1.TabIndex = 4
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' btnAccept
        ' 
        btnAccept.Enabled = False
        btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), Image)
        btnAccept.ImageTransparentColor = Color.Magenta
        btnAccept.Name = "btnAccept"
        btnAccept.Size = New Size(94, 29)
        btnAccept.Text = "Accept"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(6, 34)
        ' 
        ' btnPhrases
        ' 
        btnPhrases.Image = CType(resources.GetObject("btnPhrases.Image"), Image)
        btnPhrases.ImageTransparentColor = Color.Magenta
        btnPhrases.Name = "btnPhrases"
        btnPhrases.Size = New Size(92, 29)
        btnPhrases.Text = "Phrase"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(6, 34)
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
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(cmbType)
        GroupBox1.Controls.Add(txtSearch)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(cmbSearch)
        GroupBox1.Controls.Add(btnSearch)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Location = New Point(20, 71)
        GroupBox1.Margin = New Padding(5, 6, 5, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(5, 6, 5, 6)
        GroupBox1.Size = New Size(842, 131)
        GroupBox1.TabIndex = 26
        GroupBox1.TabStop = False
        GroupBox1.Text = "Search"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.ForeColor = Color.DarkBlue
        Label3.Location = New Point(593, 35)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(92, 25)
        Label3.TabIndex = 7
        Label3.Text = "Type Filter"
        ' 
        ' cmbType
        ' 
        cmbType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbType.FormattingEnabled = True
        cmbType.Location = New Point(582, 71)
        cmbType.Margin = New Padding(5, 6, 5, 6)
        cmbType.Name = "cmbType"
        cmbType.Size = New Size(234, 33)
        cmbType.TabIndex = 6
        ' 
        ' txtSearch
        ' 
        txtSearch.Location = New Point(23, 71)
        txtSearch.Margin = New Padding(5, 6, 5, 6)
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(237, 31)
        txtSearch.TabIndex = 5
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.ForeColor = Color.DarkBlue
        Label2.Location = New Point(345, 35)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(107, 25)
        Label2.TabIndex = 4
        Label2.Text = "Search Term"
        ' 
        ' cmbSearch
        ' 
        cmbSearch.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSearch.FormattingEnabled = True
        cmbSearch.Items.AddRange(New Object() {"Phrase ID", "Phrase Key"})
        cmbSearch.Location = New Point(335, 71)
        cmbSearch.Margin = New Padding(5, 6, 5, 6)
        cmbSearch.Name = "cmbSearch"
        cmbSearch.Size = New Size(234, 33)
        cmbSearch.TabIndex = 3
        ' 
        ' btnSearch
        ' 
        btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), Image)
        btnSearch.Location = New Point(273, 62)
        btnSearch.Margin = New Padding(5, 6, 5, 6)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(52, 54)
        btnSearch.TabIndex = 2
        btnSearch.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.ForeColor = Color.MidnightBlue
        Label1.Location = New Point(23, 35)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(240, 25)
        Label1.TabIndex = 0
        Label1.Text = "Phrase ID"
        ' 
        ' dgvPhrases
        ' 
        dgvPhrases.AllowUserToAddRows = False
        dgvPhrases.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(255), CByte(224), CByte(192))
        dgvPhrases.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvPhrases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvPhrases.Columns.AddRange(New DataGridViewColumn() {PhraseID, phrasekey, Phrase})
        dgvPhrases.Location = New Point(20, 231)
        dgvPhrases.Margin = New Padding(5, 6, 5, 6)
        dgvPhrases.Name = "dgvPhrases"
        dgvPhrases.ReadOnly = True
        dgvPhrases.RowHeadersVisible = False
        dgvPhrases.RowHeadersWidth = 62
        dgvPhrases.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvPhrases.Size = New Size(842, 402)
        dgvPhrases.TabIndex = 27
        ' 
        ' PhraseID
        ' 
        PhraseID.FillWeight = 60F
        PhraseID.HeaderText = "ID"
        PhraseID.MaxInputLength = 5
        PhraseID.MinimumWidth = 8
        PhraseID.Name = "PhraseID"
        PhraseID.ReadOnly = True
        ' 
        ' phrasekey
        ' 
        phrasekey.FillWeight = 80F
        phrasekey.HeaderText = "Key"
        phrasekey.MaxInputLength = 25
        phrasekey.MinimumWidth = 8
        phrasekey.Name = "phrasekey"
        phrasekey.ReadOnly = True
        phrasekey.Width = 134
        ' 
        ' Phrase
        ' 
        Phrase.FillWeight = 362F
        Phrase.HeaderText = "Phrase"
        Phrase.MaxInputLength = 60
        Phrase.MinimumWidth = 8
        Phrase.Name = "Phrase"
        Phrase.ReadOnly = True
        Phrase.Width = 605
        ' 
        ' frmPhraseLook
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(882, 658)
        Controls.Add(dgvPhrases)
        Controls.Add(GroupBox1)
        Controls.Add(ToolStrip1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPhraseLook"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Phrase Look Up"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(dgvPhrases, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPhrases As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbSearch As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgvPhrases As System.Windows.Forms.DataGridView
    Friend WithEvents PhraseID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents phrasekey As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Phrase As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox

End Class
