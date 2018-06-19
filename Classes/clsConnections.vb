Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Public Class clsConnections
    Inherits clsVariables

    Public Shared CN As New SqlConnection
    Public Shared DA As New SqlDataAdapter
    Public Shared CMD As New SqlCommand
    Public Shared DS As New DataSet
    Public Sub closeConnection()
        If CN.State = ConnectionState.Open Then
            CN.Close()
            CN.Dispose()
        End If
    End Sub
    Public Sub CloseData()
        Try
            DS.Clear()
            DA.Dispose()
            DS.Dispose()
        Catch e As SqlException
            MessageBox.Show("Error: " + e.Message, "CloseData Function", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
    End Sub

    Public Function Connect_Database(ByVal sDatabaseLocation As String) As Boolean
        Dim flag1 As Boolean

        Try
            CN.ConnectionString = "Data Source=.\SQLEXPRESS;AttachDbFilename=" + sDatabaseLocation + ";Integrated Security=True;Connect Timeout=30;User Instance=True"
            Try
                CN.Open()
            Catch e As SqlException
                Return False
            End Try
            flag1 = True
        Catch e As ArgumentException
            flag1 = False
        End Try
        Return flag1
    End Function

    Public Function Connect2SQLServer(ByVal DBServer As String, ByVal DBName As String, ByVal UserID As String, ByVal Password As String) As Boolean
        Dim flag1 As Boolean

        Try
            CN.ConnectionString = "Data Source=" + DBServer + ";Initial Catalog=" + DBName + ";Integrated Security=False;User ID=" + UserID + ";Password=" + Password
            Try
                CN.Open()
            Catch e As SqlException
                Return False
            End Try
            flag1 = True
        Catch e As ArgumentException
            flag1 = False
        End Try
        Return flag1
    End Function

    Public Function Connect_Database(ByVal DBServer As String, ByVal sDBFile As String) As Boolean
        Dim flag1 As Boolean

        Try
            CN.ConnectionString = "Data Source=" + DBServer + ";Initial Catalog=" + sDBFile + ";Integrated Security=True;Pooling=False"
            Try
                CN.Open()
            Catch e As SqlException
                Return False
            End Try
            flag1 = True
        Catch e As ArgumentException
            flag1 = False
        End Try
        Return flag1
    End Function


    Public Sub SetData(ByVal tSQL As String, ByVal sTable As String)
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        da.SelectCommand = New SqlCommand(tSQL, CN)
        If sTable.CompareTo("") <> 0 Then
            ds.Clear()
            Try
                da.Fill(ds, sTable)
            Catch e As SqlException
                MessageBox.Show("Error: " + e.Message, "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try
        End If
    End Sub

    Public Function TransactSQL(ByVal tSQL As String) As Boolean
        DA.SelectCommand = New SqlCommand(tSQL, CN)
        Dim dt As DataTable = New DataTable
        Try
            DA.Fill(dt)
            dt.Clear()
            DA.Dispose()
            dt.Dispose()
            Return True
        Catch e As SqlException
            MessageBox.Show("Error: " + e.Message, "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand)
        End Try
        Return False
    End Function

    Public Function GetDataTable(ByVal tSQL As String) As DataTable
        Dim da As New SqlDataAdapter
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
    Public Function TransactSQL(ByVal tSQL As String, ByVal sTable As String) As Boolean
        DA.SelectCommand = New SqlCommand(tSQL, CN)
        If sTable.CompareTo("") <> 0 Then
            DS.Clear()
            Try
                DA.Fill(DS, sTable)
                CloseData()
                Return True
            Catch e As SqlException
                MessageBox.Show("Error: " + e.Message, "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            End Try
        End If
        Return False
    End Function


End Class ' class clsConnections