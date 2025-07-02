Imports NLog
Imports NLog.Config
Imports NLog.Targets

Public Class AppLogger
    Public Shared Sub SetupLogger()
#If DEBUG Then ' Enable logging only in debug mode
        Dim config As New LoggingConfiguration()

        ' **File Logging (only in debug mode)**
        Dim fileTarget As New FileTarget("fileLog") With {
            .FileName = Application.StartupPath + "\Logs\Prolis Logs.txt",
            .Layout = "${longdate} | ${level:uppercase=true} | ${callsite} | ${message} | ${exception:format=tostring}"
        }

        ' **Debug Output Window Logging**
        Dim debugTarget As New DebugTarget("debugLog") With {
            .Layout = "${longdate} | ${level:uppercase=true} | ${callsite} | ${message} | ${exception:format=tostring}"
        }

        ' **Console Logging (optional)**
        Dim consoleTarget As New ConsoleTarget("consoleLog") With {
            .Layout = "${longdate} | ${level:uppercase=true} | ${callsite} | ${message} | ${exception:format=tostring}"
        }

        ' Add rules for logging
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget)  ' Log to file
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, debugTarget)  ' Log to Debug Output
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget) ' Log to Console

        ' Apply configuration
        LogManager.Configuration = config
#End If
    End Sub
End Class
