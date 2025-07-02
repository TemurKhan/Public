Imports System.Windows.Forms
Imports System.Threading

Public Class AutoLogout
    ' This is the maximum number of minutes the application can remain 
    ' without activity before the AutoLogout routine will close the 
    ' application. The field is public so that it can be changed.
    Public maxNumberMinutesIdle As Integer
    Public Shared Event LogoutUser()
    Public Shared Event UserActivity()
    ' Keeps the timestamp for the last time activity was detected.
    Private dtLastActivity As System.DateTime = DateTime.Now
    ' This is the method that will serve as the background thread.
    Private Sub CheckForExceededIdleTime()
        'Static dtLastActivity As System.DateTime
        ' Sets up an infinite loop
        While True
            ' If the last time activity occured and the Maximum Allowable 
            ' Idle time is less than the current time, then the system 
            ' should be shut down
            If dtLastActivity.AddMinutes(maxNumberMinutesIdle) < DateTime.Now Then
                ' Exits the program, not elegant you should modify this
                ' so it disposes all open windows, etc. 
                'frmDashboard.btnLogout_Click(Nothing, Nothing)
                RaiseEvent LogoutUser()
                Exit Sub
                'System.Windows.Forms.Application.[Exit]()
            End If
            ' Probably don't need this running every second
            ' Perhaps every minute would be better in a production system
            Thread.Sleep(1000)
        End While
    End Sub
    ' Static connStringuctor. Used to launch the background thread
    Public Sub New()
        Dim ts As New ThreadStart(AddressOf CheckForExceededIdleTime)
        Dim t As New Thread(ts)

        ' Ensures background thread is killed when 
        ' last foreground terminates
        t.IsBackground = True

        ' Don't want this thread taking up too much CPU            
        t.Priority = ThreadPriority.BelowNormal
        t.Start()
    End Sub

    ' This is the method called to watch a form
    Public Sub WatchControl(ByVal c As Control)
        '  If the control is a textbox then we want to watch 
        ' for KeyStrokes since these signify activity.
        If TypeOf c Is TextBox Then
            Dim t As TextBox = DirectCast(c, TextBox)
            ' Register an event listener for the keypress event
            AddHandler t.KeyPress, New System.Windows.Forms.KeyPressEventHandler(AddressOf MethodWhichResetsTheDateofLastActivity)

            ' Register an event listener for the MouseMove event
            AddHandler c.MouseMove, New System.Windows.Forms.MouseEventHandler(AddressOf MethodWhichResetsTheDateofLastActivity)
        Else
            ' If the control is not a TextBox then the we want to watch 
            ' for MouseMovement since this signifies activity
            AddHandler c.MouseMove, New System.Windows.Forms.MouseEventHandler(AddressOf MethodWhichResetsTheDateofLastActivity)
        End If
        ' Checks to see if the control is a container for other controls
        ' if so then we need to watch of all the constituent controls/
        If c.Controls.Count > 0 Then
            For Each cx As Control In c.Controls
                ' Recursive call
                WatchControl(cx)
            Next
        End If
    End Sub
    ' This method resets the datetime stamp which indicates when the last 
    ' activity occured. Overloaded to support KeyPressEventArgs parameter
    Private Sub MethodWhichResetsTheDateofLastActivity(ByVal _
    sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'Console.WriteLine("Keyboard Activity Detected")
        'dtLastActivity = DateTime.Now
        RaiseEvent UserActivity()
    End Sub
    ' This method resets the datetime stamp which indicates when the last
    ' activity occured. Overloaded to support MouseEventArgs parameter

    Private Sub MethodWhichResetsTheDateofLastActivity(ByVal _
    sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        'Console.WriteLine("Mouse Activity Detected")
        'Static dtLastActivity As DateTime
        'dtLastActivity = DateTime.Now
        'CheckForExceededIdleTime()
        RaiseEvent UserActivity()
    End Sub
End Class
