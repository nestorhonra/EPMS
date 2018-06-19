Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class frmRptEmployeeLeaveFilling
    Public CrxRpt As New ReportDocument
    Private Sub frmRptEmployeeLeaveFilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim sql_source As String
            sql_source = " SELECT " &
                    " ea.* " &
                    " FROM " &
                    " ( " &
                        " SELECT DISTINCT " &
                        " e.EmployeeCode As [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " e.PresentAddress As [Address], " &
                        " e.DateHired, " &
                        " e.SeparationDate, " &
                        " e.CompanyCode," &
                        " cc.CompanyName AS [Company], " &
                        " e.EmploymentTypeCode," &
                        " et.Description As [Employment Type], " &
                        " e.EmploymentStatusCode," &
                        " es.Description AS [Status], " &
                        " jt.Description As [Job Title], " &
                        " d.Description AS [Department], " &
                        " co.Description As [Cost Center], " &
                        " g.Description AS [Gender], " &
                        " cs.CivilStatus As [Civil Status], " &
                        " e.ContactNo AS [Phone No], " &
                        " e.MobileNo As [Mobile No], " &
                        " e.SSSID AS [SSS No], " &
                        " e.TINID As [TIN], " &
                        " e.HDMF AS [Pagibig Number], " &
                        " e.PhilHealth As [PhilHealth No], " &
                        " e.BankAccountNo AS [Account Number] " &
                    " FROM " &
                        " tblMst_Employee e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Company cc(nolock)On e.CompanyCode = cc.CompanyCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentType et(nolock)ON e.EmploymentTypeCode = et.EmploymentTypeCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentStatus es(nolock)On e.EmploymentStatusCode = es.EmploymentStatusCode " &
                    " LEFT OUTER JOIN tblMst_JobTitle jt(nolock)ON e.JobTitleCode = jt.JobTitleCode " &
                    " LEFT OUTER JOIN tblMst_Department d(nolock)On e.DepartmentCode = d.DepartmentCode " &
                    " LEFT OUTER JOIN tblMst_Cost_Center co(nolock)ON e.CostCenterCode = co.CostCenterCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)On e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                " ) ea "
            Call FillEmployee(sql_source, lvEmployee)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmRptEmployeeLeaveFilling_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            CRViewer.Height = (Me.Height) - (CRViewer.Top + 40)
            CRViewer.Width = Me.Width - (CRViewer.Left + 20)
            Panel3.Width = Me.Width - (CRViewer.Left + 20)
        End If
    End Sub

    Private Sub FillEmployee(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Employee No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Employee Name").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Try
            ttxEmployee.Text = ""

            For Each ls As ListViewItem In lvEmployee.CheckedItems
                ttxEmployee.Text = ttxEmployee.Text & ls.Text & ","
            Next

            ttxEmployee.Text = Mid$(ttxEmployee.Text, 1, Len(ttxEmployee.Text) - 1)
            Panel.Visible = False

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        For Each ls As ListViewItem In lvEmployee.Items
            ls.Checked = False
        Next
        Call ClearAllText(Me)
        Call ClearAllText(Me, Panel.Controls)
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Panel.Visible = Not Panel.Visible
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim strFilter As String
        Dim sql_source As String
        Dim strEmployee As String
        Dim dt As New DataTable

        strFilter = " lf.StartDate >= '" & CDate(Format$(dtFrom.Value, "M/d/yyyy")) & " 00:00:00 " & "'" &
                    " AND lf.StartDate <= '" & CDate(Format$(dtTo.Value, "M/d/yyyy")) & " 23:59:59 " & "'" &
                    " AND "

        If ttxEmployee.Text <> "" Then
            strEmployee = ""
            For Each ls As ListViewItem In lvEmployee.CheckedItems
                strEmployee = strEmployee & ls.Text & ","
            Next

            strEmployee = Mid$(strEmployee, 1, Len(strEmployee) - 1)
            strFilter = strFilter & " lf.EmployeeCode IN(" & strEmployee & ") 1234"
        End If

        strFilter = Mid$(strFilter, 1, Len(strFilter) - 4)

        sql_source = " SELECT " &
                " lf.* " &
            " FROM " &
                " (" &
                    " SELECT " &
                        " lf.LeaveCode AS [Application No], " &
                        " e.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " l.ShortDesc AS [Leave Type], " &
                        " lf.StartDate, " &
                        " lf.EndDate, " &
                        " lf.Days AS [No_of_Days], " &
                        " lf.Reason, " &
                        " lf.StatusCode " &
                    " FROM " &
                        " trn_Leave_Filling lf(nolock) " &
                    " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                    " LEFT OUTER JOIN tblMst_LeaveType l(nolock)ON lf.LeaveTypeCode = l.LeaveTypeCode " &
                    " LEFT OUTER JOIN tblMst_TransStatus s(nolock)ON lf.StatusCode = s.StatusCode " &
                " )lf " &
                " WHERE " & strFilter

        With Me
            .CrxRpt.Load(Application.StartupPath & "\Reports\Employee Leave Filling.rpt")
            dt = GetDataSet(sql_source)

            .CrxRpt.DataSourceConnections.Item(0).SetConnection(DB_Source, DB_Name, False)
            .CrxRpt.DataSourceConnections.Item(0).SetLogon(DB_UserID, DB_Password)

            .CrxRpt.SetDataSource(dt)
            .CrxRpt.SetParameterValue(0, ACTIVE_LOCATION.m_LocationID)
            .CrxRpt.SetParameterValue(1, FormatDateTime(dtFrom.Value, DateFormat.ShortDate) & " to " & FormatDateTime(dtTo.Value, DateFormat.ShortDate))

            .CRViewer.ReportSource = .CrxRpt
            .CRViewer.Refresh()
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CRViewer.PrintReport()
    End Sub
    Private Sub btnReload_Click(sender As Object, e As EventArgs) Handles btnReload.Click
        btnRefresh_Click(btnRefresh, New EventArgs)
        dtFrom.Value = CDate(GetServerDateTime())
        dtTo.Value = CDate(GetServerDateTime())
    End Sub
End Class