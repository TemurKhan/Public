<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProvInquiry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProvInquiry))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label1 = New Label()
        txtProviderID = New TextBox()
        btnProvLookUp = New Button()
        txtProviderName = New TextBox()
        txtAddress = New TextBox()
        dgvProviders = New DataGridView()
        ProvID = New DataGridViewTextBoxColumn()
        ProvName = New DataGridViewTextBoxColumn()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        txtContact = New TextBox()
        Label5 = New Label()
        txtHours = New TextBox()
        txtConfiguration = New TextBox()
        Label6 = New Label()
        Label7 = New Label()
        CType(dgvProviders, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(27, 25)
        Label1.Margin = New Padding(5, 0, 5, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(162, 27)
        Label1.TabIndex = 0
        Label1.Text = "Provider ID"
        ' 
        ' txtProviderID
        ' 
        txtProviderID.Location = New Point(27, 60)
        txtProviderID.Margin = New Padding(5, 6, 5, 6)
        txtProviderID.Name = "txtProviderID"
        txtProviderID.Size = New Size(142, 31)
        txtProviderID.TabIndex = 1
        txtProviderID.TextAlign = HorizontalAlignment.Center
        ' 
        ' btnProvLookUp
        ' 
        btnProvLookUp.Image = CType(resources.GetObject("btnProvLookUp.Image"), Image)
        btnProvLookUp.Location = New Point(182, 56)
        btnProvLookUp.Margin = New Padding(5, 6, 5, 6)
        btnProvLookUp.Name = "btnProvLookUp"
        btnProvLookUp.Size = New Size(43, 44)
        btnProvLookUp.TabIndex = 2
        btnProvLookUp.UseVisualStyleBackColor = True
        ' 
        ' txtProviderName
        ' 
        txtProviderName.Location = New Point(235, 60)
        txtProviderName.Margin = New Padding(5, 6, 5, 6)
        txtProviderName.Name = "txtProviderName"
        txtProviderName.ReadOnly = True
        txtProviderName.Size = New Size(447, 31)
        txtProviderName.TabIndex = 3
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(27, 144)
        txtAddress.Margin = New Padding(5, 6, 5, 6)
        txtAddress.Name = "txtAddress"
        txtAddress.ReadOnly = True
        txtAddress.Size = New Size(656, 31)
        txtAddress.TabIndex = 4
        ' 
        ' dgvProviders
        ' 
        dgvProviders.AllowUserToAddRows = False
        dgvProviders.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.WhiteSmoke
        dgvProviders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvProviders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvProviders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvProviders.Columns.AddRange(New DataGridViewColumn() {ProvID, ProvName})
        dgvProviders.Location = New Point(713, 60)
        dgvProviders.Margin = New Padding(5, 6, 5, 6)
        dgvProviders.Name = "dgvProviders"
        dgvProviders.ReadOnly = True
        dgvProviders.RowHeadersVisible = False
        dgvProviders.RowHeadersWidth = 62
        dgvProviders.Size = New Size(427, 342)
        dgvProviders.TabIndex = 5
        ' 
        ' ProvID
        ' 
        ProvID.FillWeight = 80F
        ProvID.HeaderText = "ID"
        ProvID.MinimumWidth = 8
        ProvID.Name = "ProvID"
        ProvID.ReadOnly = True
        ' 
        ' ProvName
        ' 
        ProvName.FillWeight = 173F
        ProvName.HeaderText = "Provider"
        ProvName.MinimumWidth = 8
        ProvName.Name = "ProvName"
        ProvName.ReadOnly = True
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(235, 25)
        Label2.Margin = New Padding(5, 0, 5, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(213, 27)
        Label2.TabIndex = 6
        Label2.Text = "Provider Name"
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(27, 112)
        Label3.Margin = New Padding(5, 0, 5, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(162, 27)
        Label3.TabIndex = 7
        Label3.Text = "Address"
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(713, 25)
        Label4.Margin = New Padding(5, 0, 5, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(157, 27)
        Label4.TabIndex = 8
        Label4.Text = "Associates"
        ' 
        ' txtContact
        ' 
        txtContact.BackColor = Color.SeaShell
        txtContact.Location = New Point(27, 229)
        txtContact.Margin = New Padding(5, 6, 5, 6)
        txtContact.Multiline = True
        txtContact.Name = "txtContact"
        txtContact.ReadOnly = True
        txtContact.Size = New Size(656, 169)
        txtContact.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(32, 196)
        Label5.Margin = New Padding(5, 0, 5, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(295, 27)
        Label5.TabIndex = 10
        Label5.Text = "Contact Information"
        ' 
        ' txtHours
        ' 
        txtHours.BackColor = Color.Ivory
        txtHours.Location = New Point(27, 473)
        txtHours.Margin = New Padding(5, 6, 5, 6)
        txtHours.Multiline = True
        txtHours.Name = "txtHours"
        txtHours.ReadOnly = True
        txtHours.Size = New Size(542, 362)
        txtHours.TabIndex = 11
        ' 
        ' txtConfiguration
        ' 
        txtConfiguration.BackColor = Color.Ivory
        txtConfiguration.Location = New Point(595, 473)
        txtConfiguration.Margin = New Padding(5, 6, 5, 6)
        txtConfiguration.Multiline = True
        txtConfiguration.Name = "txtConfiguration"
        txtConfiguration.ReadOnly = True
        txtConfiguration.Size = New Size(542, 362)
        txtConfiguration.TabIndex = 12
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(32, 440)
        Label6.Margin = New Padding(5, 0, 5, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(295, 27)
        Label6.TabIndex = 15
        Label6.Text = "Business Hours"
        ' 
        ' Label7
        ' 
        Label7.Location = New Point(590, 440)
        Label7.Margin = New Padding(5, 0, 5, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(290, 27)
        Label7.TabIndex = 16
        Label7.Text = "Report Delivery Information"
        ' 
        ' frmProvInquiry
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1160, 873)
        Controls.Add(Label7)
        Controls.Add(Label6)
        Controls.Add(txtConfiguration)
        Controls.Add(txtHours)
        Controls.Add(Label5)
        Controls.Add(txtContact)
        Controls.Add(Label4)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(dgvProviders)
        Controls.Add(txtAddress)
        Controls.Add(txtProviderName)
        Controls.Add(btnProvLookUp)
        Controls.Add(txtProviderID)
        Controls.Add(Label1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(5, 6, 5, 6)
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmProvInquiry"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Provider Inquiry"
        CType(dgvProviders, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtProviderID As System.Windows.Forms.TextBox
    Friend WithEvents btnProvLookUp As System.Windows.Forms.Button
    Friend WithEvents txtProviderName As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents dgvProviders As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtContact As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHours As System.Windows.Forms.TextBox
    Friend WithEvents txtConfiguration As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ProvID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProvName As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
