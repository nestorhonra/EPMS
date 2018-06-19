Option Strict Off
Option Explicit On

Imports System.Data.OleDb

Module ModConnection
    Public CN As New ADODB.Connection
    Public sqlDT As New DataTable

    Public Const strCONNECTION As String = "Provider=SQLOLEDB;" &
                    "Data Source=JAM\SQL2K5;" &
                    "Initial Catalog=SP_BRIGHTAPPS;" &
                    "User ID=sa;" &
                    "Password=admin123;"
    Public Function Connected2Server() As Boolean
        Dim isOpen As Boolean
        Dim Reply As MsgBoxResult

        isOpen = False
        On Error GoTo Err_Tracker

        Do Until isOpen = True
            CN = New ADODB.Connection
            CN.CursorLocation = ADODB.CursorLocationEnum.adUseClient
            If CN.State = ADODB.ObjectStateEnum.adStateOpen Then CN.Close()

            CN.ConnectionString = strCONNECTION
            CN.Open()

            isOpen = True
        Loop
        Connected2Server = isOpen
        Exit Function
Err_Tracker:
        Reply = MsgBox("Error No.:" & Err.Number & vbNewLine & "Description:" & Err.Description, MsgBoxStyle.Exclamation + MsgBoxStyle.RetryCancel, "Connection Error")
        If Reply = MsgBoxResult.Retry Then
            Resume
        ElseIf Reply = MsgBoxResult.Cancel Then
            Connected2Server = False
        End If
    End Function

    Public Function ExecuteSQLQuery(ByVal SQLQuery As String) As DataTable
        Try
            Dim sqlCon As New OleDbConnection(CN.ConnectionString)
            Dim sqlDA As New OleDbDataAdapter(SQLQuery, sqlCon)
            Dim sqlCB As New OleDbCommandBuilder(sqlDA)
            sqlDT.Reset()
            sqlDA.Fill(sqlDT)

        Catch ex As Exception
            If Err.Number = 5 Then
                MsgBox("Database settings was invalid !!", MsgBoxStyle.Information)
            End If
        End Try
        Return sqlDT
    End Function
End Module
