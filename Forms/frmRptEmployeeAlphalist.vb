Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmRptEmployeeAlphalist
    Public CrxRpt As New ReportDocument
    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Panel.Visible = Not Panel.Visible
        Panel1.Visible = False
        Panel2.Visible = False
    End Sub

    Private Sub frmRptEmployeeAlphalist_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            CRViewer.Height = (Me.Height) - (CRViewer.Top + 40)
            CRViewer.Width = Me.Width - (CRViewer.Left + 20)
            Panel3.Width = Me.Width - (CRViewer.Left + 20)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
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

    Private Sub FillStatus(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("EmploymentTypeCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub FillUnit(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("UnitCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub frmRptEmployeeAlphalist_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Call FillCompany("SELECT * FROM tblMst_Company ORDER BY CompanyCode ASC ", lvCompany)
            Call FillStatus("SELECT * FROM tblMst_EmploymentType ORDER BY EmploymentTypeCode ASC ", lvStatus)
            Call FillUnit("SELECT * FROM tblMst_Unit ORDER BY UnitCode ASC ", lvUnit)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim strFilter As String
        Dim sql_source As String
        Dim strCompany As String
        Dim strStatus As String
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

        If ttxStatus.Text <> "" Then
            strStatus = ""
            For Each ls As ListViewItem In lvStatus.CheckedItems
                strStatus = strStatus & ls.Text & ","
            Next

            strStatus = Mid$(strStatus, 1, Len(strStatus) - 1)
            strFilter = strFilter & " ea.EmploymentTypeCode IN(" & strStatus & ") 1234"
        End If

        strFilter = Mid$(strFilter, 1, Len(strFilter) - 4)
        strFilter = strFilter & " AND "

        If ttxUnit.Text <> "" Then
            strDivision = ""
            For Each ls As ListViewItem In lvUnit.CheckedItems
                strDivision = strDivision & ls.Text & ","
            Next

            strDivision = Mid$(strDivision, 1, Len(strDivision) - 1)
            strFilter = strFilter & " ea.UnitCode IN(" & strDivision & ") 1234"
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
                    " LEFT OUTER JOIN tblMst_Division u(nolock)ON e.DivisionCode = u.DivisionCode " &
                    " LEFT OUTER JOIN tblMst_Unit un(nolock)ON e.UnitCode = un.UnitCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)ON e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                " )ea " &
                " WHERE " & strFilter &
                " ORDER BY ea.[Employee Name] ASC "

        With Me
            .CrxRpt.Load(Application.StartupPath & "\Reports\Employee Alphalist.rpt")

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
        Panel1.Visible = False
    End Sub

    Private Sub txtUnit_TextChanged(sender As Object, e As EventArgs) Handles txtUnit.TextChanged
        Try
            Call FillUnit("SELECT * FROM tblMst_Unit WHERE Description Like '%" & sqlSafe(txtUnit.Text) & "%'", lvUnit)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh2_Click(sender As Object, e As EventArgs) Handles btnRefresh2.Click
        For Each ls As ListViewItem In lvUnit.Items
            ls.Checked = False
        Next
        ttxUnit.Text = ""
        Call ClearAllText(Me, Panel2.Controls)
    End Sub

    Private Sub btnSelect2_Click(sender As Object, e As EventArgs) Handles btnSelect2.Click
        Try
            ttxUnit.Text = ""

            For Each ls As ListViewItem In lvUnit.CheckedItems
                ttxUnit.Text = ttxUnit.Text & ls.Text & ","
            Next

            ttxUnit.Text = Mid$(ttxUnit.Text, 1, Len(ttxUnit.Text) - 1)
            Panel2.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFind1_Click(sender As Object, e As EventArgs) Handles btnFind1.Click
        Call object_center(Me, Panel1)
        Panel1.Visible = Not Panel1.Visible
        Panel.Visible = False
        Panel2.Visible = False
    End Sub

    Private Sub txtStatus_TextChanged(sender As Object, e As EventArgs) Handles txtStatus.TextChanged
        Try
            Call FillStatus("SELECT * FROM tblMst_EmploymentType WHERE Description Like '%" & sqlSafe(txtStatus.Text) & "%'", lvStatus)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect1_Click(sender As Object, e As EventArgs) Handles btnSelect1.Click
        Try
            ttxStatus.Text = ""

            For Each ls As ListViewItem In lvStatus.CheckedItems
                ttxStatus.Text = ttxStatus.Text & ls.Text & ","
            Next

            ttxStatus.Text = Mid$(ttxStatus.Text, 1, Len(ttxStatus.Text) - 1)
            Panel1.Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh1_Click(sender As Object, e As EventArgs) Handles btnRefresh1.Click
        For Each ls As ListViewItem In lvStatus.Items
            ls.Checked = False
        Next
        ttxStatus.Text = ""
        Call ClearAllText(Me, Panel1.Controls)
    End Sub

    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        Call ClearAllText(Me)
        Call ClearAllText(Me, Panel.Controls)
        Call ClearAllText(Me, Panel1.Controls)
        Call ClearAllText(Me, Panel2.Controls)
    End Sub
End Class