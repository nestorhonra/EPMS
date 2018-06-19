Imports Microsoft.Win32


Public Class clsRegistry

    Public Function Get_Setting(ByVal sSubKey As String, ByVal sValue As String) As Object
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey(sSubKey)
        Return key.GetValue(sValue)
    End Function
    Public Sub Save_Setting(ByVal sSubKey As String, ByVal sValue As String)
        Dim key As RegistryKey = Registry.LocalMachine.OpenSubKey("software", True)
        Dim newkey As RegistryKey = key.CreateSubKey(sSubKey)
        newkey.SetValue(sSubKey, sValue)
    End Sub
End Class ' class clsRegistry

