Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class clsSecurity
    Inherits clsTypeVariables

    Private myVariables As clsVariables
    Private sConnections As clsConnections

    Private Shared activeUser As clsTypeVariables.TypeSysLogin
    Private Shared grantor As clsTypeVariables.TypeSysLogin
    Private Shared grantResult As Boolean
    Private Shared grantStyle As Byte
    Private Shared hasGrant As Boolean
    Public Sub New()
        sConnections = New clsConnections
        myVariables = New clsVariables
    End Sub

    Shared Sub New()
        clsSecurity.activeUser = New clsTypeVariables.TypeSysLogin
        clsSecurity.grantor = New clsTypeVariables.TypeSysLogin
        clsSecurity.hasGrant = False
    End Sub

    Public Function Decrypt(ByVal sourceData As String) As String
        Dim s1 As String

        Dim key As Byte() = New Byte() {}
        Dim iv As Byte() = New Byte() {}
        Try
            Dim encryptedDataBytes As Byte() = Convert.FromBase64String(sourceData)
            Dim tempStream As MemoryStream = New MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length)
            Dim decryptor As DESCryptoServiceProvider = New DESCryptoServiceProvider
            Dim decryptionStream As CryptoStream = New CryptoStream(tempStream, decryptor.CreateDecryptor(key, iv), CryptoStreamMode.Read)
            Dim allDataReader As StreamReader = New StreamReader(decryptionStream)
            s1 = allDataReader.ReadToEnd()
        Catch e As Exception
            s1 = "Error Decryption."
        End Try
        Return s1
    End Function

    Public Function Encrypt(ByVal sourceData As String) As String
        Dim s1 As String

        Dim key As Byte() = New Byte() {}
        Dim iv As Byte() = New Byte() {}
        Try
            Dim sourceDataBytes As Byte() = Encoding.ASCII.GetBytes(sourceData)
            Dim tempStream As MemoryStream = New MemoryStream
            Dim encryptor As DESCryptoServiceProvider = New DESCryptoServiceProvider
            Dim encryptionStream As CryptoStream = New CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write)
            encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length)
            encryptionStream.FlushFinalBlock()
            Dim encryptedDataBytes As Byte() = tempStream.GetBuffer()
            s1 = Convert.ToBase64String(encryptedDataBytes, 0, CInt(tempStream.Length))
        Catch e As Exception
            s1 = "Error Encryption."
        End Try
        Return s1
    End Function

    Public Function GetUserInformation(ByVal tUserName As String) As clsTypeVariables.TypeSysLogin
        Dim tID As Long = CLng(0)
        Dim sGetUserInformation As clsTypeVariables.TypeSysLogin = New clsTypeVariables.TypeSysLogin
        Dim dt As DataTable = New DataTable
        dt = sConnections.GetDataTable("SELECT ID, Active, u.EmployeeID, Name, Password, Username FROM Users u LEFT JOIN vw_Employees e ON u.EmployeeID = e.EmployeeID WHERE Username = '" + tUserName + "'")
        If dt.Rows.Count > 0 Then
            Dim sDataRow As DataRow = dt.Rows(0)
            sGetUserInformation.Active = Convert.ToBoolean(sDataRow("Active"))
            sGetUserInformation.ID = Int32.Parse(sDataRow("EmployeeID").ToString())
            sGetUserInformation.Name = sDataRow("Name").ToString()
            sGetUserInformation.Password = sDataRow("Password").ToString()
            sGetUserInformation.Username = sDataRow("Username").ToString()
            tID = CLng(Convert.ToInt32(sDataRow("ID")))
        End If
        Dim i As Byte = 0
        dt.Clear()
        dt.Dispose()
        dt = sConnections.GetDataTable(String.Concat("SELECT * FROM User_Rights WHERE ID = ", tID))
        If dt.Rows.Count > 0 Then
            i = 0
            While i < CByte(dt.Rows.Count)
                sGetUserInformation.Rights(i) = dt.Rows(i).ItemArray.GetValue(1).ToString()
                i = i + CByte(1)
            End While
        End If
        Return sGetUserInformation
    End Function

    Public Function IsUserAllowed(ByVal onAction As String) As Boolean
        Dim i As Integer = 0
        While i < activeUser.Rights.Length
            Try
                If activeUser.Rights(i).Equals(onAction) OrElse activeUser.Rights(i).Equals("System Administrator") Then
                    Return True
                End If
            Catch e As NullReferenceException
            End Try
            i = i + 1
        End While
        If hasGrant Then
            i = 0
            While i < grantor.Rights.Length
                If (grantor.Rights(i).CompareTo(onAction) = 0) OrElse (grantor.Rights(i).CompareTo("System Administrator") = 0) Then
                    Return True
                End If
                i = i + 1
            End While
        End If
        grantResult = False
        If grantResult AndAlso (grantStyle = 0) Then
            hasGrant = False
            Return True
        End If
        Return False
    End Function

End Class ' class clsSecurity

