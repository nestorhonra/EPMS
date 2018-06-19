Imports System
Public Class clsVar
    Public Shared User_Info As New clsTypeVariables.USER_INFO()
    Public Shared CompanyProfile As New clsTypeVariables.Sys_Company()

    Public Shared Function IsUserAllowed() As Boolean
        If clsVar.User_Info.USER_LEVEL = 1 Then
            MessageBox.Show("You do not have permission to do this task", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.[Stop])
            Return (False)
        Else
            Return (True)
        End If
    End Function

End Class
