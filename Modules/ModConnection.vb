Option Strict Off
Option Explicit On

Imports System.Data.SqlClient
Imports System.Data
Module ModConnection
    Public MyConn As New SqlConnection
    Public MyCommand As New SqlCommand
    Public MyDA As New SqlDataAdapter
    Public MyDR As SqlDataReader

    Public MyDS As New DataSet
    Public myDT As DataTable

    Public Function Connected2Server() As Boolean
        Dim strCon As String

        Connected2Server = False

        DB_Source = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_Source", "")
        DB_Name = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_Name", "")
        DB_UserID = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_UserID", "")
        DB_Password = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_Password", "")

        Try
            If MyConn.State = ConnectionState.Open Then MyConn.Close()
            strCon = "Data Source=" & DB_Source & ";" & "Initial Catalog=" & DB_Name & ";" & "User ID=" & DB_UserID & ";" & "Password=" & DB_Password
            MyConn.ConnectionString = strCon
            MyConn.Open()
            Connected2Server = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Function

    Public Sub ExecuteSQL(ByVal myCmd As String)

        Dim cmd As SqlCommand
        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        cmd = New SqlCommand(myCmd, MyConn)

        MyConn.Open()
        cmd.ExecuteNonQuery()

        cmd.Dispose()
        MyConn.Close()


    End Sub
    Public Sub DeleteSQL(ByVal sTable As String, ByVal sField As String, ByVal isNumber As Boolean, ByVal sString As String, ByVal snum As Long)
        Try
            Dim cmd As SqlCommand
            If MyConn.State = ConnectionState.Open Then MyConn.Close()

            If isNumber = True Then
                cmd = New SqlCommand("DELETE FROM " & sTable & " WHERE " & sField & " =" & snum, MyConn)
            Else
                cmd = New SqlCommand("DELETE FROM " & sTable & " WHERE " & sField & " ='" & sString & "'", MyConn)
            End If

            MyConn.Open()
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            MyConn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        
    End Sub

    Public Function GetDataSet(ByVal cmd As String) As DataTable
        Dim adp As New SqlDataAdapter
        Dim dt As New DataTable

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        MyConn.Open()

        adp = New SqlDataAdapter(cmd, MyConn)
        dt = New DataTable
        adp.Fill(dt)
        MyConn.Close()
        Return dt
    End Function
End Module
