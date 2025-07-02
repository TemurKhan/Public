<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEob_Attachments
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
        DataGridView1 = New DataGridView()
        DataGridViewImageColumn1 = New DataGridViewImageColumn()
        AttName = New DataGridViewTextBoxColumn()
        Action = New DataGridViewImageColumn()
        Delete = New DataGridViewImageColumn()
        ID = New DataGridViewTextBoxColumn()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Columns.AddRange(New DataGridViewColumn() {AttName, Action, Delete, ID})
        DataGridView1.Location = New Point(44, 77)
        DataGridView1.Margin = New Padding(4, 5, 4, 5)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersWidth = 51
        DataGridView1.RowTemplate.Height = 24
        DataGridView1.Size = New Size(599, 608)
        DataGridView1.TabIndex = 0
        ' 
        ' DataGridViewImageColumn1
        ' 
        DataGridViewImageColumn1.HeaderText = "Delete"
        DataGridViewImageColumn1.MinimumWidth = 6
        DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        DataGridViewImageColumn1.Resizable = DataGridViewTriState.True
        DataGridViewImageColumn1.Width = 125
        ' 
        ' AttName
        ' 
        AttName.HeaderText = "Name"
        AttName.MinimumWidth = 6
        AttName.Name = "AttName"
        AttName.Width = 125
        ' 
        ' Action
        ' 
        Action.HeaderText = "View"
        Action.Image = My.Resources.Resources.icons8_view_241
        Action.MinimumWidth = 6
        Action.Name = "Action"
        Action.Resizable = DataGridViewTriState.True
        Action.SortMode = DataGridViewColumnSortMode.Automatic
        Action.Width = 125
        ' 
        ' Delete
        ' 
        Delete.HeaderText = "Delete"
        Delete.Image = My.Resources.Resources.icons8_delete_16
        Delete.MinimumWidth = 6
        Delete.Name = "Delete"
        Delete.Resizable = DataGridViewTriState.True
        Delete.Width = 125
        ' 
        ' ID
        ' 
        ID.HeaderText = "ID"
        ID.MinimumWidth = 6
        ID.Name = "ID"
        ID.ReadOnly = True
        ID.Visible = False
        ID.Width = 125
        ' 
        ' frmEob_Attachments
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(690, 703)
        Controls.Add(DataGridView1)
        Margin = New Padding(4, 5, 4, 5)
        Name = "frmEob_Attachments"
        ShowIcon = False
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Eob_Attachments"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents AttName As DataGridViewTextBoxColumn
    Friend WithEvents Action As DataGridViewImageColumn
    Friend WithEvents Delete As DataGridViewImageColumn
    Friend WithEvents ID As DataGridViewTextBoxColumn
End Class
