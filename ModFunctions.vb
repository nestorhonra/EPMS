Option Strict Off
Option Explicit On
Module ModFunctions

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
    Public Function GenerateNewIDNumber(ByVal srcTable As String, ByVal srcField As String) As Double
        Dim srcRS As New ADODB.Recordset
        If srcRS.State = 1 Then srcRS.Close()
        srcRS.Open("SELECT MAX(" & srcField & ") FROM " & srcTable, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

        If IsDBNull(srcRS.Fields(0).Value) = True Then
            GenerateNewIDNumber = 1
        Else
            GenerateNewIDNumber = Val(srcRS.Fields(0).Value) + 1
        End If
    End Function
    Public Function getValueAt(ByVal srcSQL As String, ByVal whichField As String) As String
        Dim RS As New ADODB.Recordset

        getValueAt = ""

        RS.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        RS.Open(srcSQL, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        If RS.RecordCount > 0 Then getValueAt = RS.Fields(whichField).Value

        RS = Nothing
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
        toMoney = Format$(srcCurr, "#,##0.00")
    End Function
    Public Function isRecordExist(ByVal sTable As String, ByVal sField As String, ByVal sStr As String, Optional ByVal isString As Boolean = False) As Boolean
        Dim RS As New ADODB.Recordset

        RS.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        If isString = False Then
            RS.Open("Select * From " & sTable & " Where " & sField & " = " & sStr, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        Else
            RS.Open("Select * From " & sTable & " Where " & sField & " = '" & sStr & "'", CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
        End If
        If RS.RecordCount < 1 Then
            isRecordExist = False
        Else
            isRecordExist = True
        End If
        RS = Nothing
    End Function

    Public Function CanAccess(ByVal srcLoginName As String, ByVal ModuleKey As String, ByVal srcSQL As String) As Boolean
        Dim RS As New ADODB.Recordset

        OpenRecordset(CN, RS)
        RS.Open(srcSQL, CN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)

        If RS.RecordCount > 0 Then
            CanAccess = True
        Else
            MsgBox("You don't have access on this module. Contact your system administrator", MsgBoxStyle.Exclamation)
            CanAccess = False
        End If

        RS = Nothing
    End Function

    Public Function GetServerDateTime() As String
        Dim RS As New ADODB.Recordset
        RS = CN.Execute("SELECT GETDATE()")

        GetServerDateTime = RS.Fields(0).Value
    End Function

End Module
