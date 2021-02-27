Imports Microsoft.Win32

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
''' Manage all registry keys for t4e ecu editor 
''' 
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class T4eRegistry
    Private T4eRegistryPath As String = "SOFTWARE\" & Application.CompanyName & "\" & Application.ProductName
    Private T4eRegistryKey As RegistryKey


    Public Sub New()

        ' create our RegistryPath if we run this the first time on a computer        
        Try
            My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\", True).CreateSubKey(Application.CompanyName)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\" & Application.CompanyName, True).CreateSubKey(Application.ProductName)
        ' open our SubKey area with R/W mode
        Me.T4eRegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
    End Sub



    Public Function GetECUComPort() As String
        Dim setting As String
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath)
            setting = CStr(Key.GetValue("ECUComPort", ""))
        End Using
        Return (setting)
    End Function

    Public Sub SetECUComPort(ByVal setting As String)
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
            Key.SetValue("ECUComPort", setting)
        End Using
    End Sub

    Public Function GetECUAutoConnect() As String
        Dim setting As Boolean
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath)
            setting = CStr(Key.GetValue("ECUAutoConnect", False))
        End Using
        Return (setting)
    End Function

    Public Sub SetECUAutoConnect(ByVal setting As Boolean)        
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
            Key.SetValue("ECUAutoConnect", setting)
        End Using
    End Sub

    Public Function GetECUCANSpeed() As Integer
        Dim setting As Integer
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath)
            setting = CInt(Key.GetValue("ECUCANSpeed", 501))
        End Using
        Return (setting)
    End Function

    Public Sub SetECUCANSpeed(ByVal setting As Integer)        
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
            Key.SetValue("ECUCANSpeed", setting)
        End Using
    End Sub


    Public Function GetLastOpenCALFile() As String
        Dim path As String
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath)
            path = CStr(Key.GetValue("LastOpenCALFile", ""))
        End Using
        Return (path)
    End Function


    Public Sub SetLastOpenCALFile(ByVal path As String)
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
            Key.SetValue("LastOpenCALFile", path)
        End Using
    End Sub

    Public Function GetLastOpenCALDataFile() As String
        Dim path As String
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath)
            path = CStr(Key.GetValue("LastOpenCALDataFile", ""))
        End Using
        Return (path)
    End Function

    Public Sub SetLastOpenCALDataFile(ByVal path As String)
        Using Key As RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey(T4eRegistryPath, True)
            Key.SetValue("LastOpenCALDataFile", path)
        End Using
    End Sub
End Class
