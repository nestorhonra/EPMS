Imports System.Diagnostics

Public Class clsAppSettings

    Public Function PreviousInstance(ByVal name As String) As Boolean
        Dim processes As Process() = Process.GetProcessesByName(name)
        Dim processArr1 As Process() = processes
        Dim i1 As Integer = 0
        While i1 < processArr1.Length
            Dim proc As Process = processArr1(i1)
            If proc.MainWindowHandle <> CType(0, IntPtr) Then
                clsAppSettings.SetForegroundWindow(proc.MainWindowHandle)
                Return True
            End If
            i1 = i1 + 1
        End While
        Return False
    End Function
    Private Declare Ansi Function SetForegroundWindow Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean

End Class