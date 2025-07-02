Imports System.ComponentModel

Public Class frmProlisRichText

    Dim bold As Boolean = False
    Dim bulet As Boolean = False
    Dim ul As Boolean = False
    Dim italic As Boolean = False
    Dim zoom As Single = 1.0
    Dim zoomid As Integer = 5
    Dim tsize As Single = 12.0
    Private _startPoint As Point

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFileDialog1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt"
        SaveFileDialog1.Title = "Save File"
        SaveFileDialog1.ShowDialog()

        If Not SaveFileDialog1.FileName = "" Then
            Select Case SaveFileDialog1.FilterIndex
                Case 1
                    Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
                    RichTextBox1.SaveFile(fs, RichTextBoxStreamType.RichText)
                Case 2
                    Dim fs As System.IO.FileStream = CType(SaveFileDialog1.OpenFile(), System.IO.FileStream)
                    RichTextBox1.SaveFile(fs, RichTextBoxStreamType.PlainText)
            End Select

        End If
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs)
        If Not bold Then
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Bold)
            bold = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            bold = False
        End If
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)
        If Not ul Then
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Underline)
            ul = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            ul = False
        End If
    End Sub

    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        If Not italic Then
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Italic)
            italic = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            italic = False
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        ColorDialog1.ShowDialog()
        RichTextBox1.SelectionColor = ColorDialog1.Color

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click

        OpenFileDialog1.Filter = "Rich Text File|*.rtf|Plain Text File|*.txt"
        OpenFileDialog1.Title = "Open File"
        OpenFileDialog1.ShowDialog()

        If (OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (OpenFileDialog1.FileName.Length > 0) Then
            Select Case OpenFileDialog1.FilterIndex
                Case 1

                    RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.RichText)
                Case 2

                    RichTextBox1.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText)
            End Select
        End If
    End Sub

    Private Sub LengthToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MessageBox.Show("Length: " & RichTextBox1.TextLength, "Notepad")
    End Sub

    Private Sub WordsToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim spaces As Integer = 0

        For i As Integer = 0 To RichTextBox1.TextLength - 1
            If RichTextBox1.Text.Substring(i, 1) = " " Then
                spaces += 1
            End If
        Next

        MessageBox.Show("Approximate word count: " & spaces + 1, "Notepad")

    End Sub


    Private Sub ToolStripLabel3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ProlisText_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = MouseButtons.Left Then
            'Dim deltaX As Integer = e.X - _startPoint.X
            'Dim deltaY As Integer = e.Y - _startPoint.Y

            'Me.Size += New Size(deltaX, deltaY)  ' Resize the UserControl

            'RichTextBox1.Width = Me.Width  ' Resize the RichTextBox control to match the UserControl's width
            'RichTextBox1.Height = Me.Height  ' Resize the RichTextBox control to match the UserControl's height
        End If
    End Sub

    Private Sub ProlisText_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        '  _startPoint = New Point(e.X, e.Y)
    End Sub

    Private Sub ProlisText_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        ' _startPoint = Point.Empty
    End Sub



    Private Sub InsertImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InsertImageToolStripMenuItem.Click
        'OpenFileDialog2.Filter = "JPEG Image File|*.jpg|PNG Image File|*.png|Bitmap Image File|*.bmp|GIF Image File|*.gif"
        'OpenFileDialog2.Title = "Insert Image"
        'OpenFileDialog2.ShowDialog()

        'If (OpenFileDialog2.ShowDialog() = System.Windows.Forms.DialogResult.OK) And (OpenFileDialog2.FileName.Length > 0) Then
        '    Dim img As Image = Image.FromFile(OpenFileDialog2.FileName)
        '    Clipboard.SetImage(img)
        '    RichTextBox1.Paste()
        'End If
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        zoomid += 1
        If zoomid <= 100 Then
            zoom += 0.2

            RichTextBox1.ZoomFactor = zoom
            ToolStripLabel2.Text = "Zoom " & zoomid
        Else
            zoomid = zoomid - 1
        End If

    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        zoomid = zoomid - 1
        If zoomid >= 1 Then
            zoom -= 0.2

            RichTextBox1.ZoomFactor = zoom
            ToolStripLabel2.Text = "Zoom " & zoomid
        Else
            zoomid += 1
        End If


    End Sub

    Private Sub ToolStripLabel2_Click(sender As Object, e As EventArgs) Handles ToolStripLabel2.Click

    End Sub

    Private Sub RichTextBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles RichTextBox1.MouseWheel
        Dim mwe As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        mwe.Handled = True
    End Sub

    Private Sub MicrosoftSansSerifToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MicrosoftSansSerifToolStripMenuItem.Click
        RichTextBox1.SelectionFont = New Font("Microsoft Sans Serif", tsize, FontStyle.Regular)
        bold = False
        italic = False
        ul = False
    End Sub

    Private Sub ArialToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArialToolStripMenuItem.Click
        RichTextBox1.SelectionFont = New Font("Arial", tsize, FontStyle.Regular)
        bold = False
        italic = False
        ul = False
    End Sub

    Private Sub TimesNewRomanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TimesNewRomanToolStripMenuItem.Click
        RichTextBox1.SelectionFont = New Font("Times New Roman", tsize, FontStyle.Regular)
        bold = False
        italic = False
        ul = False
    End Sub

    Private Sub CalibriToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalibriToolStripMenuItem.Click
        RichTextBox1.SelectionFont = New Font("Calibri", tsize, FontStyle.Regular)
        bold = False
        italic = False
        ul = False
    End Sub

    Private Sub ComicSansToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComicSansToolStripMenuItem.Click

        RichTextBox1.SelectionFont = New Font("Comic Sans MS", tsize, FontStyle.Regular)
        bold = False
        italic = False
        ul = False
    End Sub

    Private Sub IncreaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncreaseToolStripMenuItem.Click
        If tsize <= 100 Then
            tsize += 1

            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, tsize)

            ToolStripLabel3.Text = "Size " & tsize
        End If
    End Sub

    Private Sub DecreaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DecreaseToolStripMenuItem.Click
        If tsize >= 5 Then
            tsize = tsize - 1

            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, tsize)

            ToolStripLabel3.Text = "Size " & tsize
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        If Not bold Then
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Bold)
            bold = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            bold = False
        End If
    End Sub

    Private Sub ToolStripButton2_Click_1(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        If Not ul Then
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Underline)
            ul = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            ul = False
        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click


        ' Append some text with a bullet


        ' Reset the bullet style

        If Not bulet Then
            RichTextBox1.SelectionBullet = True

            'RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Underline)
            bulet = True
        Else
            RichTextBox1.SelectionFont = New Font(Me.RichTextBox1.SelectionFont, FontStyle.Regular)
            RichTextBox1.SelectionBullet = False
            bulet = False
        End If
    End Sub

End Class
