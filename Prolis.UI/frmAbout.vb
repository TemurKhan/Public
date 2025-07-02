Public NotInheritable Class frmAbout

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '
        If LIC Is Nothing Then LIC = New LicenseManager.ProlisLicense(connString, My.Application.Info.AssemblyName)
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        ' TODO: Customize the application's assembly information in the "Application" pane of the project 
        '    properties dialog (under the "Project" menu).
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        If LIC IsNot Nothing Then
            Me.TextBoxDescription.Text = My.Application.Info.Description & vbCrLf & _
            "License Holder: " & LIC.Licensee & vbCrLf & _
            "Licensed APP: Prolis" & vbCrLf & _
            "License Version: " & LIC.LicVer
        Else
            Me.TextBoxDescription.Text = My.Application.Info.Description & vbCrLf & _
            "No License information" & vbCrLf & _
            "License validation failed"
        End If
        '
        If ThisUser.LogoutMins > 0 Then ALO.WatchControl(Me)
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
