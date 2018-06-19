Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class clsFunctions
#Region "Variables"
    Inherits clsVariables
    Public AppSettings As clsAppSettings
    Public Call_MessageBox As clsMessageBox
    Public myControlMethod As clsControls
    Public sConnections As clsConnections
    Public sListView As clsListView
    Public SystemRegistry As clsRegistry
    Public SystemSecurity As clsSecurity
    Public sStatment As clsConnections
#End Region

#Region "Public Sub New"
    Public Sub New()
        sConnections = New clsConnections
        sListView = New clsListView
        myControlMethod = New clsControls
        SystemSecurity = New clsSecurity
        Call_MessageBox = New clsMessageBox
        SystemRegistry = New clsRegistry
        AppSettings = New clsAppSettings
        sStatment = New clsConnections
    End Sub
#End Region

#Region "FillDataSet"
    Protected Function FillDataSet(ByVal dset As DataSet, ByVal sSQL As String, ByVal sTable As String) As DataSet
        Try
            Dim cmd As SqlCommand = New SqlCommand(sSQL, clsConnections.CN)
            Dim adapter As SqlDataAdapter = New SqlDataAdapter(cmd)
            adapter.Fill(dset, sTable)
        Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try

        Return dset
    End Function
#End Region


#Region "getRecordCount"
    Public Function getRecordCount(ByVal sTable As String) As Integer
        Dim dt As DataTable = New DataTable
        dt = sConnections.GetDataTable("SELECT * FROM " + sTable)
        Return Convert.ToInt32(dt.Rows.Count)
    End Function

#End Region

#Region "GetReport"
    Public Function GetReport(ByVal ds As DataSet, ByVal sSQL As String, ByVal sTable As String) As DataSet
        Dim dsReturn As DataSet = ds
        FillDataSet(dsReturn, sSQL, sTable)
        Return dsReturn
    End Function

#End Region

#Region "GetValueAt"


#End Region

#Region "InvertSort"
    Public Function InvertSort(ByVal s As String) As String
        Dim sReturn As String

        If s.CompareTo("ASC") = 0 Then
            sReturn = "DESC"
        Else
            sReturn = "ASC"
        End If
        Return sReturn
    End Function
#End Region

#Region "Proc_Mode"
    Public Function Proc_Mode(ByVal State As clsTypeVariables.FORM_STATE) As String
        If State <> clsTypeVariables.FORM_STATE.adStateAddMode Then
            Return "UPDATE"
        End If
        Return "INSERT"
    End Function
#End Region

#Region "String_Left"
    Public Function String_Left(ByVal sString As String, ByVal value As Integer) As String
        Dim sReturn As String = ""
        Dim i As Integer = 0
        While i < value
            sReturn = String.Concat(sReturn, sString(i))
            i = i + 1
        End While
        Return sReturn
    End Function
#End Region

#Region "String_Trim"
    Public Function String_Trim(ByVal sEval As String) As String
        Dim s1 As String

        Try
            sEval.Trim()
            Dim s As String = sEval.Replace("'", "''")
            s1 = s
        Catch e As EvaluateException
            MessageBox.Show("Error: " + e.Message, "String Trim Functions", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            s1 = ""
        End Try
        Return s1
    End Function
#End Region

#Region "Set Report"
    Public Function setReport(ByVal ds As DataSet, ByVal sSQL As String, ByVal sTable As String) As DataSet
        setCommand(ds, sAdapter, sSQL, sTable)
        Return ds
    End Function
#End Region

#Region "Set Command"
    Public Sub setCommand(ByVal ds As DataSet, ByVal da As SqlDataAdapter, ByVal sSQL As String, ByVal sTable As String)
        da.SelectCommand = New SqlCommand(sSQL, clsConnections.CN)
        ds.Clear()
        da.Fill(ds, sTable)
    End Sub
#End Region

End Class ' class clsFunctions
