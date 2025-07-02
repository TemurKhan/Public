Imports Microsoft.Extensions.Configuration
Imports System.IO

Public Module AppConfig
    Private _configuration As IConfiguration

    Public ReadOnly Property Configuration As IConfiguration
        Get
            If _configuration Is Nothing Then
                Dim basePath As String = AppContext.BaseDirectory

                Dim builder As IConfigurationBuilder = New ConfigurationBuilder() _
                    .SetBasePath(basePath) _
                    .AddJsonFile("appsettings.json", optional:=True, reloadOnChange:=True)

                _configuration = builder.Build()
            End If

            Return _configuration
        End Get
    End Property

End Module
