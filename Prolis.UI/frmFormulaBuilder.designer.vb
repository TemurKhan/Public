<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFormulaBuilder
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Choice Analytes")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Numeric Analytes")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("IsEmpty(x)")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ToNumber(x)")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Round(x, n)")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Math", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Instr(x, Y)")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Left(x, n)")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Mid(x, n [, m])")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Right(x, n)")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ToText(x)")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("String", New System.Windows.Forms.TreeNode() {TreeNode7, TreeNode8, TreeNode9, TreeNode10, TreeNode11})
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("CDate(Exp)")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("IsDate(Exp)")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DateDiff(Int, d1, d2)")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DateAdd(Int, n, d)")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DatePart(Int, d)")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("DateTime", New System.Windows.Forms.TreeNode() {TreeNode13, TreeNode14, TreeNode15, TreeNode16, TreeNode17})
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("{ResultOf(x)}")
        Dim TreeNode20 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("{AVE(x,y,z)}")
        Dim TreeNode21 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("{MIN(x, y,z)}")
        Dim TreeNode22 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("{MAX(x,y,z)}")
        Dim TreeNode23 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("{IIF(Exp, Truepart, Falsepart)}")
        Dim TreeNode24 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("@Age@")
        Dim TreeNode25 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("@Sex@")
        Dim TreeNode26 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("@Ethnicity@")
        Dim TreeNode27 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Other", New System.Windows.Forms.TreeNode() {TreeNode19, TreeNode20, TreeNode21, TreeNode22, TreeNode23, TreeNode24, TreeNode25, TreeNode26})
        Dim TreeNode28 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Functions", New System.Windows.Forms.TreeNode() {TreeNode6, TreeNode12, TreeNode18, TreeNode27})
        Dim TreeNode29 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("( ) Change Precedence")
        Dim TreeNode30 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("- Subtract")
        Dim TreeNode31 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("+ Addition")
        Dim TreeNode32 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("* Multiplication")
        Dim TreeNode33 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("/ Division")
        Dim TreeNode34 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("\ Modulus")
        Dim TreeNode35 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Operators", New System.Windows.Forms.TreeNode() {TreeNode29, TreeNode30, TreeNode31, TreeNode32, TreeNode33, TreeNode34})
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFormulaBuilder))
        Me.tvTests = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFormula = New System.Windows.Forms.TextBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Label2 = New System.Windows.Forms.Label
        Me.tvOperators = New System.Windows.Forms.TreeView
        Me.Label3 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnAccept = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.btnSyntax = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.btnCancel = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.btnHelp = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tvTests
        '
        Me.tvTests.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvTests.Location = New System.Drawing.Point(6, 18)
        Me.tvTests.Name = "tvTests"
        TreeNode1.Name = "Choice Analytes"
        TreeNode1.Text = "Choice Analytes"
        TreeNode2.Name = "NumericNode"
        TreeNode2.Text = "Numeric Analytes"
        Me.tvTests.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Me.tvTests.Size = New System.Drawing.Size(298, 114)
        Me.tvTests.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Analytes"
        '
        'txtFormula
        '
        Me.txtFormula.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFormula.Location = New System.Drawing.Point(6, 16)
        Me.txtFormula.MaxLength = 250
        Me.txtFormula.Multiline = True
        Me.txtFormula.Name = "txtFormula"
        Me.txtFormula.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFormula.Size = New System.Drawing.Size(521, 131)
        Me.txtFormula.TabIndex = 3
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Location = New System.Drawing.Point(12, 41)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtFormula)
        Me.SplitContainer1.Size = New System.Drawing.Size(530, 289)
        Me.SplitContainer1.SplitterDistance = 135
        Me.SplitContainer1.TabIndex = 4
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.tvTests)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.tvOperators)
        Me.SplitContainer2.Size = New System.Drawing.Size(530, 135)
        Me.SplitContainer2.SplitterDistance = 310
        Me.SplitContainer2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label2.Location = New System.Drawing.Point(0, 2)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Operators/Functions"
        '
        'tvOperators
        '
        Me.tvOperators.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvOperators.Location = New System.Drawing.Point(3, 18)
        Me.tvOperators.Name = "tvOperators"
        TreeNode3.Name = "IsEmpty( )"
        TreeNode3.Text = "IsEmpty(x)"
        TreeNode4.Name = "ToNumber( )"
        TreeNode4.Text = "ToNumber(x)"
        TreeNode5.Name = "Round( , )"
        TreeNode5.Text = "Round(x, n)"
        TreeNode6.Name = "Math"
        TreeNode6.Text = "Math"
        TreeNode7.Name = "Instr( , )"
        TreeNode7.Text = "Instr(x, Y)"
        TreeNode8.Name = "Left( , )"
        TreeNode8.Text = "Left(x, n)"
        TreeNode9.Name = "Mid( , )"
        TreeNode9.Text = "Mid(x, n [, m])"
        TreeNode10.Name = "Right( , )"
        TreeNode10.Text = "Right(x, n)"
        TreeNode11.Name = "ToText( )"
        TreeNode11.Text = "ToText(x)"
        TreeNode12.Name = "String"
        TreeNode12.Text = "String"
        TreeNode13.Name = "CDate( )"
        TreeNode13.Text = "CDate(Exp)"
        TreeNode14.Name = "IsDate( )"
        TreeNode14.Text = "IsDate(Exp)"
        TreeNode15.Name = "DateDiff( ,  , )"
        TreeNode15.Text = "DateDiff(Int, d1, d2)"
        TreeNode16.Name = "DateAdd( ,  , )"
        TreeNode16.Text = "DateAdd(Int, n, d)"
        TreeNode17.Name = "DatePart( , )"
        TreeNode17.Text = "DatePart(Int, d)"
        TreeNode18.Name = "DateTime"
        TreeNode18.Text = "DateTime"
        TreeNode19.Name = "{ResultOf( )}"
        TreeNode19.Text = "{ResultOf(x)}"
        TreeNode20.Name = "{AVE( ,  , )}"
        TreeNode20.Text = "{AVE(x,y,z)}"
        TreeNode21.Name = "{MIN( ,  , )}"
        TreeNode21.Text = "{MIN(x, y,z)}"
        TreeNode22.Name = "{MAX( , , )}"
        TreeNode22.Text = "{MAX(x,y,z)}"
        TreeNode23.Name = "{IIF( ,  , )}"
        TreeNode23.Text = "{IIF(Exp, Truepart, Falsepart)}"
        TreeNode24.Name = "Patient Age"
        TreeNode24.Text = "@Age@"
        TreeNode25.Name = "Patient Gender"
        TreeNode25.Text = "@Sex@"
        TreeNode26.Name = "Patient Ethnicity"
        TreeNode26.Text = "@Ethnicity@"
        TreeNode27.Name = "Other"
        TreeNode27.Text = "Other"
        TreeNode28.Name = "Functions"
        TreeNode28.Text = "Functions"
        TreeNode29.Name = "( ) Change Precedence"
        TreeNode29.Text = "( ) Change Precedence"
        TreeNode30.Name = "- Subtract"
        TreeNode30.Text = "- Subtract"
        TreeNode31.Name = "+ Addition"
        TreeNode31.Text = "+ Addition"
        TreeNode32.Name = "* Multiplication"
        TreeNode32.Text = "* Multiplication"
        TreeNode33.Name = "/ Division"
        TreeNode33.Text = "/ Division"
        TreeNode34.Name = "\ Modulus"
        TreeNode34.Text = "\ Modulus"
        TreeNode35.Name = "Operators"
        TreeNode35.Text = "Operators"
        Me.tvOperators.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode28, TreeNode35})
        Me.tvOperators.Size = New System.Drawing.Size(210, 114)
        Me.tvOperators.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.MidnightBlue
        Me.Label3.Location = New System.Drawing.Point(3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Formula Output"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnAccept, Me.ToolStripSeparator1, Me.btnSyntax, Me.ToolStripSeparator2, Me.btnCancel, Me.ToolStripSeparator3, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(557, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnAccept
        '
        Me.btnAccept.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnAccept.Image = CType(resources.GetObject("btnAccept.Image"), System.Drawing.Image)
        Me.btnAccept.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(64, 22)
        Me.btnAccept.Text = "Accept"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSyntax
        '
        Me.btnSyntax.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnSyntax.Image = CType(resources.GetObject("btnSyntax.Image"), System.Drawing.Image)
        Me.btnSyntax.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSyntax.Name = "btnSyntax"
        Me.btnSyntax.Size = New System.Drawing.Size(61, 22)
        Me.btnSyntax.Text = "Syntax"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnCancel
        '
        Me.btnCancel.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "Cancel"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.ForeColor = System.Drawing.Color.MidnightBlue
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "Help"
        '
        'frmFormulaBuilder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 352)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFormulaBuilder"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Formula Builder"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tvTests As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFormula As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tvOperators As System.Windows.Forms.TreeView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnAccept As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSyntax As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label

End Class
