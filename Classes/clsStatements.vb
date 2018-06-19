Imports System.Data
Imports System.Data.SqlClient


Public Class clsStatements
    Inherits clsVariables

    Public sConnections As New clsConnections

    Public Sub setCommand(ByVal dsFill As DataSet, ByVal daFill As SqlDataAdapter, ByVal sSQL As String, ByVal sTable As String)
        daFill.SelectCommand = New SqlCommand(sSQL, clsConnections.CN)
        dsFill.Clear()
        daFill.Fill(dsFill, sTable)
    End Sub

End Class ' class clsStatements
