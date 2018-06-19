Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Module ModFunctions
    Public Function GenerateNewIDNumber(ByVal srcTable As String, ByVal srcField As String) As Double
        Dim dt As New DataTable
        dt = GetDataSet("SELECT MAX(" & srcField & ") FROM " & srcTable)

        If dt.Rows.Count > 0 Then
            GenerateNewIDNumber = toNumber(dt.Rows(0).Item(0).ToString()) + 1
        Else
            GenerateNewIDNumber = 1
        End If

    End Function
    Public Function is_empty(ByRef sText As TextBox) As Boolean
        On Error Resume Next
        If sText.Text = "" Then
            is_empty = True
            MsgBox("The field is required.Please check it!", vbExclamation)
            sText.Focus()
        Else
            is_empty = False
        End If
    End Function
    Public Function sqlSafe(ByRef p_string As String) As String
        sqlSafe = Replace(Trim(p_string), "'", "''")
    End Function

    Public Function is_numeric(ByRef sText As String) As Boolean
        If IsNumeric(sText) = False Then
            is_numeric = False
            MsgBox("The field required a numeric input.Please check it!", MsgBoxStyle.Exclamation)
        Else
            is_numeric = True
        End If
    End Function

    Public Function toNumber(ByVal srcCurrency As String, Optional ByRef RetZeroIfNegative As Boolean = False) As Double
        Dim retValue As Double
        If srcCurrency = "" Then
            toNumber = 0
        Else
            If InStr(1, srcCurrency, ",") > 0 Then
                retValue = Val(Replace(srcCurrency, ",", "", , , CompareMethod.Text))
            Else
                retValue = Val(srcCurrency)
            End If
            If RetZeroIfNegative = True Then
                If retValue < 1 Then retValue = 0
            End If
            toNumber = retValue
            retValue = 0
        End If
    End Function
    Public Function toMoney(ByVal srcCurr As String) As String
        toMoney = Format(toNumber(srcCurr), "#,##0.00")
    End Function

    Public Function changeYNValue(ByVal srcStr As String) As String
        changeYNValue = "N"
        Select Case srcStr
            Case "Y" : changeYNValue = "True"
            Case "N" : changeYNValue = "False"
            Case "True" : changeYNValue = "Y"
            Case "False" : changeYNValue = "N"
        End Select
    End Function

    Public Function GetValueAt(ByVal srcSQL As String, ByVal whichField As String) As String
        Dim dt As New DataTable
        GetValueAt = ""

        dt = GetDataSet(srcSQL)

        If dt.Rows.Count > 0 Then
            GetValueAt = dt.Rows(0).Item(whichField).ToString()
        Else
            GetValueAt = ""
        End If
    End Function
    Public Function GetValueAtDB(ByVal srcTable As String, ByVal whichField As String, ByVal isNumber As Boolean, ByVal whereField As String, ByVal sPassValue As String) As String
        Dim sReturn As String = ""
        Dim dt As DataTable = New DataTable
        If isNumber = True Then
            dt = GetDataTable("SELECT " + whichField + " FROM " + srcTable + " WHERE " + whereField + " = " + sPassValue)
        Else
            dt = GetDataTable("SELECT " + whichField + " FROM " + srcTable + " WHERE " + whereField + " = '" + sPassValue + "'")
        End If
        If dt.Rows.Count > 0 Then
            sReturn = dt.Rows(0).ItemArray.GetValue(0).ToString()
        End If
        Return sReturn
    End Function

    Public Function GetDataTable(ByVal tSQL As String) As DataTable
        Dim da As New SqlDataAdapter
        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        MyConn.Open()
        da.SelectCommand = New SqlCommand(tSQL, MyConn)
        Dim dt As DataTable = New DataTable
        Try
            da.Fill(dt)
        Catch e As SqlException
            MessageBox.Show("Error: " + e.Message, "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Return Nothing
        End Try
        Return dt
    End Function
    Public Function GetServerDateTime() As String
        Dim dt As New DataTable
        dt = GetDataSet("SELECT GETDATE()")

        GetServerDateTime = dt.Rows(0).Item(0).ToString()
    End Function

    Public Function MonthValue(ByVal cMonth As String) As Integer
        On Error GoTo Err
        MonthValue = 0
        If UCase(cMonth) = "JANUARY" Then MonthValue = 1
        If UCase(cMonth) = "FEBRUARY" Then MonthValue = 2
        If UCase(cMonth) = "MARCH" Then MonthValue = 3
        If UCase(cMonth) = "APRIL" Then MonthValue = 4
        If UCase(cMonth) = "MAY" Then MonthValue = 5
        If UCase(cMonth) = "JUNE" Then MonthValue = 6
        If UCase(cMonth) = "JULY" Then MonthValue = 7
        If UCase(cMonth) = "AUGUST" Then MonthValue = 8
        If UCase(cMonth) = "SEPTEMBER" Then MonthValue = 9
        If UCase(cMonth) = "OCTOBER" Then MonthValue = 10
        If UCase(cMonth) = "NOVEMBER" Then MonthValue = 11
        If UCase(cMonth) = "DECEMBER" Then MonthValue = 12
        Exit Function
Err:    MsgBox(Err.Description, vbExclamation)
    End Function

    Public Function IsRecordExist(ByVal sTable As String, ByVal sField As String, ByVal sStr As String, Optional isString As Boolean = True) As Boolean
        On Error GoTo Hell
        Dim sSQL As String

        If isString = False Then
            sSQL = "Select * From " & sTable & " Where " & sField & " = " & sStr
        Else
            sSQL = "Select * From " & sTable & " Where " & sField & " = '" & sStr & "'"
        End If

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        If Dtrdr.HasRows = True Then
            Return True
        Else
            Return False
        End If

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()

        Exit Function
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Function
End Module
