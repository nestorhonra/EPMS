Imports System.Data.SqlClient
Public Class clsVariables
    Inherits clsTypeVariables

    Private Shared bError_Trapping_for_ListviewSorter As Boolean
    Public Shared dbs_Settings As clsTypeVariables.Sys_Database
    Public Shared Global_Grantor As Boolean
    Private Shared pictures_path As String
    Private Shared sql As String
    Private Shared sTitle As String
    Public sAdapter As SqlDataAdapter

    Public Property App_Title() As String
        Get
            If Not (clsVariables.sTitle) Is Nothing Then
                Return clsVariables.sTitle
            End If
            Return "THETANS SOFTWARE"
        End Get
        Set(ByVal value As String)
            If clsVariables.sTitle = value Then
                Return
            End If
            clsVariables.sTitle = value
        End Set
    End Property

    Public Property Error_Trap_LvSorter() As Boolean
        Get
            Return clsVariables.bError_Trapping_for_ListviewSorter
        End Get
        Set(ByVal value As Boolean)
            If clsVariables.bError_Trapping_for_ListviewSorter = value Then
                Return
            End If
            clsVariables.bError_Trapping_for_ListviewSorter = value
        End Set
    End Property



End Class ' class clsVariables

