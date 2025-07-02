Imports System.Windows.Forms

Public Class frmAccPayment
    Private PMT() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
    Private xKey As String

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Tag = ""
        Me.Close()
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Prices(txtAmount, e)
    End Sub

    Private Sub txtYear_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Numerals(sender, e)
    End Sub

    Private Sub cmbPaymentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPaymentType.SelectedIndexChanged
        If cmbPaymentType.SelectedIndex = 0 Then    'Cash
            lblDoc.Text = "Doc No"
            txtDoc.Text = Trim(frmRequisitions.txtAccID.Text) & "-CASH"
            txtAccCharge.Text = Trim(frmRequisitions.txtAccID.Text)
            txtAmount.Text = Trim(frmRequisitions.txtCopay.Text)
            chkReadMan.Checked = False
            chkReadMan.Enabled = False
            gbCC.Enabled = False
        ElseIf cmbPaymentType.SelectedIndex = 1 Then    'Check
            lblDoc.Text = "Check/Money Order No"
            txtDoc.Text = ""
            txtAccCharge.Text = Trim(frmRequisitions.txtAccID.Text)
            txtAmount.Text = Trim(frmRequisitions.txtCopay.Text)
            chkReadMan.Checked = False
            chkReadMan.Enabled = False
            gbCC.Enabled = False
        Else    'Credit Card
            lblDoc.Text = "Credit Card Approval"
            txtAccCharge.Text = Trim(frmRequisitions.txtAccID.Text)
            txtAmount.Text = Trim(frmRequisitions.txtCopay.Text)
            txtDoc.Text = ""
            txtDoc.ReadOnly = True
            txtAmount.Text = ""
            chkReadMan.Checked = False
            chkReadMan.Enabled = True
            gbCC.Enabled = True
            'txtCCNo.ReadOnly = False
        End If
        Update_Progress()
    End Sub

    Private Sub Update_Progress()
        If ((cmbPaymentType.SelectedIndex = 0 Or cmbPaymentType.SelectedIndex = 1) _
        And IsDate(txtDate.Text) And txtDoc.Text <> "" And txtAmount.Text <> "") Then
            '(cmbPaymentType.SelectedIndex = 2 And IsDate(txtDate.Text) And _
            'txtDoc.Text <> "" And txtAmount.Text <> "" And txtCardNo.Text <> "" And _
            'cmbMonth.SelectedIndex <> -1 And txtYear.Text.Length = 4) Then
            btnAccept.Enabled = True
        ElseIf ((cmbPaymentType.SelectedIndex = 2) And IsDate(txtDate.Text) _
        And txtDoc.Text = "" And Val(txtAmount.Text) > 0) Then
            btnAccept.Enabled = False
            Me.KeyPreview = True
            txtCardinfo.Focus()
            BlinkerStart()
        End If
    End Sub

    Private Sub cmbArType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub txtDate_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDate.Validated
        If txtDate.Text <> "" Then
            If Not IsDate(txtDate.Text) Then
                MsgBox("Invalid Date")
                txtDate.Text = ""
                txtDate.Focus()
            End If
        End If
        Update_Progress()
    End Sub

    Private Sub txtDoc_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDoc.Validated
        Update_Progress()
    End Sub

    Private Sub txtAmount_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.Validated
        Update_Progress()
    End Sub

    Private Sub txtCardNo_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub cmbMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    Private Sub txtYear_Validated(ByVal sender As Object, ByVal e As System.EventArgs)
        Update_Progress()
    End Sub

    'Private Sub frmAccPayment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
    '    xKey.Append(e.KeyChar)
    'End Sub

    Private Sub frmAccPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim PMT() As String = {"", "", "", "", "", "", "", "", "", "", "", "", "", "", ""}
        txtDate.Text = Format(Date.Now, SystemConfig.DateFormat)
        If frmQuickQuote.IsHandleCreated Then
            PMT = frmQuickQuote.PMT
        ElseIf frmRequisitions.IsHandleCreated Then
            txtAccCharge.Text = Trim(frmRequisitions.txtAccID.Text)
            txtAmount.Text = Trim(frmRequisitions.txtCopay.Text)
        End If
        cmbPaymentType.SelectedIndex = 0
        'If PMT.Length > 7 Then
        '    'Dim PMT() As String = frmQuickQuote.PMT
        '    '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp
        '    cmbPaymentType.SelectedIndex = Val(PMT(3))
        '    txtDate.Text = PMT(4)
        '    txtDoc.Text = PMT(5)
        '    txtAmount.Text = Format(Val(PMT(6)), "0.00")
        '    'If PMT.Length > 8 AndAlso PMT(8) <> "" Then
        '    '    Dim i As Integer
        '    '    For i = 0 To cmbCardType.Items.Count - 1
        '    '        If cmbCardType.Items(i).ToString = PMT(8) Then
        '    '            cmbCardType.SelectedIndex = i
        '    '            Exit For
        '    '        End If
        '    '    Next
        '    '    If PMT.Length > 9 Then txtCardNo.Text = PMT(9)
        '    '    If PMT.Length > 10 Then
        '    '        For i = 0 To cmbMonth.Items.Count - 1
        '    '            If Microsoft.VisualBasic.Left(PMT(10), 2) = cmbMonth.Items(i).ToString Then
        '    '                cmbMonth.SelectedIndex = i
        '    '                Exit For
        '    '            End If
        '    '        Next
        '    '        txtYear.Text = Microsoft.VisualBasic.Right(PMT(10), 4)
        '    '    End If
        '    '    If PMT.Length > 11 Then txtCVV.Text = PMT(11)
        '    '    If PMT.Length > 12 Then txtBillName.Text = PMT(12)
        '    '    If PMT.Length > 13 Then txtZip.Text = PMT(13)
        '    'End If
        '    btnAccept.Enabled = True
        'Else
        '    cmbPaymentType.SelectedIndex = 0
        '    txtDate.Text = Format(frmRequisitions.dtpDate.Value, SystemConfig.DateFormat)
        '    txtAmount.Text = ""
        'End If
        'If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        '0=ID, 1=ArType, 2=ArID, 3= PType, 4= PDate, 5= DocNo, 6=AMT, 7= UnApp
        If cmbPaymentType.SelectedIndex <> 2 Then
            Me.Tag = PMT(0) & "|2|" & PMT(2) & "|" & cmbPaymentType.SelectedIndex.ToString _
            & "|" & txtDate.Text & "|" & txtDoc.Text & "|" & txtAmount.Text & "||||||||"
        Else
            Me.Tag = PMT(0) & "|2|" & PMT(2) & "|" & cmbPaymentType.SelectedIndex.ToString & _
            "|" & txtDate.Text & "|" & txtDoc.Text & "|" & txtAmount.Text & "|||" '& _
            'cmbCardType.Text & "|" & Trim(txtCardNo.Text) & "|" & _
            'GetCardDate(cmbMonth.Text, txtYear.Text) & "|" & Trim(txtCVV.Text) _
            '& "|" & Trim(txtBillName.Text) & "|" & Trim(txtZip.Text)
        End If
        Me.Close()
    End Sub

    Public Shadows Function ShowDialog() As String
        MyBase.ShowDialog()
        Return Me.Tag 'CType(TextBox1.Text, Integer)
    End Function

    Private Function GetCardDate(ByVal MM As String, ByVal YYYY As String) As String
        Dim CardDate As String = ""
        Select Case MM
            Case "01", "03", "05", "07", "08", "10", "12"
                CardDate = MM & "/31/" & YYYY
            Case "02"
                If (Val(YYYY) \ 4) = 0 Then     'Leap
                    CardDate = MM & "/29/" & YYYY
                Else
                    CardDate = MM & "/28/" & YYYY
                End If
            Case Else
                CardDate = MM & "/30/" & YYYY
        End Select
        Return CardDate
    End Function

    Private Sub chkReadMan_CheckedChanged(sender As Object, e As EventArgs) Handles chkReadMan.CheckedChanged
        If chkReadMan.Checked = False Then  'Reader
            chkReadMan.Text = "Reader"
            txtCCNo.ReadOnly = True
            txtExp.ReadOnly = True
            txtCVV.ReadOnly = True
            txtCardHolder.ReadOnly = True
            txtBillZip.ReadOnly = True
        Else 'Manual
            chkReadMan.Text = "Manual"
            txtCCNo.ReadOnly = False
            txtExp.ReadOnly = False
            txtCVV.ReadOnly = False
            txtCardHolder.ReadOnly = False
            txtBillZip.ReadOnly = False
        End If
    End Sub

    Private Sub txtCardinfo_TextChanged(sender As Object, e As EventArgs) Handles txtCardinfo.TextChanged
        Wait(10)
        If txtCardinfo.Text.Length > 100 Then BlinkerStop()
    End Sub

    Private Sub BlinkerStart()
        Timer1.Start()
    End Sub

    Private Sub BlinkerStop()
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If lblnotify.Visible = False Then
            lblnotify.Visible = True
        Else
            lblnotify.Visible = False
        End If
    End Sub
End Class
