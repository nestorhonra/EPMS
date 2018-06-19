Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmRptEmployeeLenghtOfService
    Public CrxRpt As New ReportDocument
    Dim i As Double
    Private Sub frmRptEmployeeLenghtOfService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Call FillCompany("SELECT * FROM tblMst_Company ORDER BY CompanyCode ASC ", lvCompany)
            Call FillDivision("SELECT * FROM tblMst_Division ORDER BY DivisionCode ASC ", lvDivision)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmRptEmployeeLenghtOfService_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            CRViewer.Height = (Me.Height) - (CRViewer.Top + 40)
            CRViewer.Width = Me.Width - (CRViewer.Left + 20)
            Panel3.Width = Me.Width - (CRViewer.Left + 20)
        End If
    End Sub

    Private Sub FillCompany(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("CompanyCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("CompanyName").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub


    Private Sub FillDivision(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("DivisionCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmRptEmployeeLenghtOfService_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            ttxCompany.Text = ""

            For Each ls As ListViewItem In lvCompany.CheckedItems
                ttxCompany.Text = ttxCompany.Text & ls.Text & ","
            Next

            ttxCompany.Text = Mid$(ttxCompany.Text, 1, Len(ttxCompany.Text) - 1)
            Panel.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        For Each ls As ListViewItem In lvCompany.Items
            ls.Checked = False
        Next
        ttxCompany.Text = ""
        Call ClearAllText(Me, Panel.Controls)
    End Sub

    Private Sub txtCompany_TextChanged(sender As Object, e As EventArgs) Handles txtCompany.TextChanged
        Try
            Call FillCompany("Select * FROM tblMst_Company WHERE CompanyName Like '%" & sqlSafe(txtCompany.Text) & "%'", lvCompany)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind2_Click(sender As Object, e As EventArgs) Handles btnFind2.Click
        Panel2.Visible = Not Panel2.Visible
        Panel.Visible = False
    End Sub

    Private Sub txtDivision_TextChanged(sender As Object, e As EventArgs) Handles txtDivision.TextChanged
        Try
            Call FillDivision("SELECT * FROM tblMst_Division WHERE Description Like '%" & sqlSafe(txtDivision.Text) & "%'", lvDivision)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh2_Click(sender As Object, e As EventArgs) Handles btnRefresh2.Click
        For Each ls As ListViewItem In lvDivision.Items
            ls.Checked = False
        Next
        ttxtDivision.Text = ""
        Call ClearAllText(Me, Panel2.Controls)
    End Sub

    Private Sub btnSelect2_Click(sender As Object, e As EventArgs) Handles btnSelect2.Click
        Try
            ttxtDivision.Text = ""

            For Each ls As ListViewItem In lvDivision.CheckedItems
                ttxtDivision.Text = ttxtDivision.Text & ls.Text & ","
            Next

            ttxtDivision.Text = Mid$(ttxtDivision.Text, 1, Len(ttxtDivision.Text) - 1)
            Panel2.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim strFilter As String
        Dim sql_source As String
        Dim strCompany As String
        Dim strDivision As String
        Dim dt As New DataTable

        strFilter = " ea.DateHired <= '" & CDate(GetServerDateTime()) & "'" &
                    " AND "

        If ttxCompany.Text <> "" Then
            strCompany = ""
            For Each ls As ListViewItem In lvCompany.CheckedItems
                strCompany = strCompany & ls.Text & ","
            Next

            strCompany = Mid$(strCompany, 1, Len(strCompany) - 1)
            strFilter = strFilter & " ea.CompanyCode IN(" & strCompany & ") 1234"
        End If

        strFilter = Mid$(strFilter, 1, Len(strFilter) - 4)
        strFilter = strFilter & " AND "

        If ttxtDivision.Text <> "" Then
            strDivision = ""
            For Each ls As ListViewItem In lvDivision.CheckedItems
                strDivision = strDivision & ls.Text & ","
            Next

            strDivision = Mid$(strDivision, 1, Len(strDivision) - 1)
            strFilter = strFilter & " ea.DivisionCode IN(" & strDivision & ") 1234"
        End If

        strFilter = Mid$(strFilter, 1, Len(strFilter) - 4)

        sql_source = " SELECT " &
                " ea.* " &
            " FROM " &
                " (" &
                    " SELECT DISTINCT " &
                        " e.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " e.PresentAddress AS [Address], " &
                        " DATEPART(month, e.BirthDate) AS [MonthBirth], " &
                        " e.BirthDate, " &
                        " DATEDIFF(HOUR, e.BirthDate, GETDATE())/ 8766 AS [Age], " &
                        " e.DateHired, " &
                        " DATEDIFF(HOUR, e.DateHired, GETDATE())/ 8766 AS [YOS], " &
                        " e.SeparationDate, " &
                        " e.CompanyCode, " &
                        " cc.CompanyName AS [Company], " &
                        " e.EmploymentTypeCode, " &
                        " et.Description AS [Employment Type], " &
                        " e.EmploymentStatusCode, " &
                        " es.Description AS [Status], " &
                        " jt.Description AS [Job Title], " &
                        " e.DepartmentCode, " &
                        " d.Description AS [Department], " &
                        " e.UnitCode, " &
                        " un.Description AS [Unit], " &
                        " e.DivisionCode, " &
                        " u.Description AS [Division], " &
                        " e.GenderCode, " &
                        " g.Description AS [Gender], " &
                        " e.CivilStatusCode, " &
                        " cs.CivilStatus AS [Civil Status], " &
                        " e.ContactNo AS [Phone No], " &
                        " e.MobileNo AS [Mobile No], " &
                        " e.SSSID AS [SSS No], " &
                        " e.TINID AS [TIN], " &
                        " e.HDMF AS [Pagibig Number], " &
                        " e.PhilHealth AS [PhilHealth No], " &
                        " e.BankAccountNo AS [Account Number] " &
                    " FROM " &
                        " tblMst_Employee e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Company cc(nolock)ON e.CompanyCode = cc.CompanyCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentType et(nolock)ON e.EmploymentTypeCode = et.EmploymentTypeCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentStatus es(nolock)ON e.EmploymentStatusCode = es.EmploymentStatusCode " &
                    " LEFT OUTER JOIN tblMst_JobTitle jt(nolock)ON e.JobTitleCode = jt.JobTitleCode " &
                    " LEFT OUTER JOIN tblMst_Department d(nolock)ON e.DepartmentCode = d.DepartmentCode " &
                    " LEFT OUTER JOIN tblMst_Unit un(nolock)ON e.UnitCode = un.UnitCode " &
                    " LEFT OUTER JOIN tblMst_Division u(nolock)ON e.DivisionCode = u.DivisionCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)ON e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                " )ea " &
                " WHERE " & strFilter &
                " ORDER BY ea.[Employee Name] ASC, ea.[YOS] DESC "

        With Me
            .CrxRpt.Load(Application.StartupPath & "\Reports\Employee Length of Service.rpt")

            dt = GetDataSet(sql_source)

            .CrxRpt.DataSourceConnections.Item(0).SetConnection(DB_Source, DB_Name, False)
            .CrxRpt.DataSourceConnections.Item(0).SetLogon(DB_UserID, DB_Password)

            .CrxRpt.SetDataSource(dt)
            .CrxRpt.SetParameterValue(0, ACTIVE_LOCATION.m_LocationID)
            .CrxRpt.SetParameterValue(1, GetServerDateTime())

            .CRViewer.ReportSource = .CrxRpt
            .CRViewer.Refresh()
        End With
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Panel.Visible = Not Panel.Visible
        Panel2.Visible = False
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Call ClearAllText(Me)
        Call ClearAllText(Me, Panel.Controls)
        Call ClearAllText(Me, Panel2.Controls)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
End Class