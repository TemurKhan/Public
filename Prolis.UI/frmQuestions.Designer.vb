<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmQuestions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.txt_Question = New System.Windows.Forms.TextBox()
        Me.Label102 = New System.Windows.Forms.Label()
        Me.dgv = New System.Windows.Forms.DataGridView()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Col_Delete = New System.Windows.Forms.DataGridViewImageColumn()
        Me.txt_Q_ID = New System.Windows.Forms.TextBox()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Save
        '
        Me.btn_Save.Location = New System.Drawing.Point(1014, 48)
        Me.btn_Save.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(80, 50)
        Me.btn_Save.TabIndex = 19
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'txt_Question
        '
        Me.txt_Question.Location = New System.Drawing.Point(90, 39)
        Me.txt_Question.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_Question.MaxLength = 250
        Me.txt_Question.Multiline = True
        Me.txt_Question.Name = "txt_Question"
        Me.txt_Question.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Question.Size = New System.Drawing.Size(916, 59)
        Me.txt_Question.TabIndex = 18
        '
        'Label102
        '
        Me.Label102.AutoSize = True
        Me.Label102.Location = New System.Drawing.Point(9, 48)
        Me.Label102.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label102.Name = "Label102"
        Me.Label102.Size = New System.Drawing.Size(73, 20)
        Me.Label102.TabIndex = 17
        Me.Label102.Text = "Question"
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column2, Me.Column1, Me.Col_Delete})
        Me.dgv.Location = New System.Drawing.Point(90, 120)
        Me.dgv.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersVisible = False
        Me.dgv.RowHeadersWidth = 62
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(916, 461)
        Me.dgv.TabIndex = 20
        '
        'Column2
        '
        Me.Column2.HeaderText = "ID"
        Me.Column2.MinimumWidth = 8
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 50
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column1.HeaderText = "Questions"
        Me.Column1.MinimumWidth = 8
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        '
        'Col_Delete
        '
        Me.Col_Delete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Col_Delete.HeaderText = "Delete"
        Me.Col_Delete.MinimumWidth = 8
        Me.Col_Delete.Name = "Col_Delete"
        Me.Col_Delete.ReadOnly = True
        Me.Col_Delete.Width = 62
        '
        'txt_Q_ID
        '
        Me.txt_Q_ID.Location = New System.Drawing.Point(13, 73)
        Me.txt_Q_ID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txt_Q_ID.MaxLength = 250
        Me.txt_Q_ID.Name = "txt_Q_ID"
        Me.txt_Q_ID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txt_Q_ID.Size = New System.Drawing.Size(69, 26)
        Me.txt_Q_ID.TabIndex = 21
        '
        'frmQuestions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1117, 607)
        Me.Controls.Add(Me.txt_Q_ID)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.txt_Question)
        Me.Controls.Add(Me.Label102)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQuestions"
        Me.Text = "Questions"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btn_Save As Button
    Friend WithEvents txt_Question As TextBox
    Friend WithEvents Label102 As Label
    Friend WithEvents dgv As DataGridView
    Friend WithEvents txt_Q_ID As TextBox
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Col_Delete As DataGridViewImageColumn
End Class
