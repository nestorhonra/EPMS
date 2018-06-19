Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmRptPayrollRegisterSummary
    Public CrxRpt As New ReportDocument

    Private Sub frmRptPayrollRegisterSummary_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmRptPayrollRegisterSummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub frmRptPayrollRegisterSummary_Resize(sender As Object, e As EventArgs) Handles Me.Resize
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

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CRViewer.PrintReport()
    End Sub

    Private Sub btnPreview_Click(sender As Object, e As EventArgs) Handles btnPreview.Click
        Dim strFilter As String
        Dim sql_source As String
        Dim dt As New DataTable

        strFilter = " sp.StartDate >= '" & CDate(Format$(dtFrom.Value, "M/d/yyyy")) & " 00:00:00 " & "'" &
                    " AND sp.StartDate <= '" & CDate(Format$(dtTo.Value, "M/d/yyyy")) & " 23:59:59 " & "'" &
                    " AND "

        strFilter = Mid$(strFilter, 1, Len(strFilter) - 4)

        sql_source = " SELECT " &
                " * " &
            " FROM " &
                " (" &
                    " SELECT " &
                        " ph.PayrollID AS [Payroll No], " &
                        " pp.MonthText AS [Payroll Period], " &
                        " pp.StartDate, " &
                        " pp.EndDate, " &
                        " pd.Description AS [Payroll Distribution], " &
                        " vm.No_Of_Employees, " &
                        " vm.Total_Income, " &
                        " vm.Total_Deduction, " &
                        " vm.Net_Income, " &
                        " ph.IsPosted " &
                    " FROM " &
                        " trn_Payroll_Header ph(nolock) " &
                    " LEFT OUTER JOIN tblMst_PayrollPeriod pp(nolock)ON ph.PeriodCode = pp.PeriodCode " &
                    " LEFT OUTER JOIN tblMst_PayrollDistribution pd(nolock)ON ph.PayrollDistributionCode = pd.PayrollDistributionCode " &
                    " LEFT OUTER JOIN( " &
                        " SELECT " &
                            " pd.PayrollID, " &
                            " pd.No_Of_Employees, " &
                            " pd.Income AS Total_Income, " &
                            " pd.Deductions AS Total_Deduction, " &
                            " pd.Income - pd.Deductions AS Net_Income " &
                        " FROM " &
                            " ( " &
                                " SELECT " &
                                    " a.PayrollID AS PayrollID, " &
                                    " COUNT(DISTINCT EmployeeCode)AS No_Of_Employees, " &
                                    " ( " &
                                        " SELECT " &
                                            " SUM(ParticularAmount) " &
                                        " FROM " &
                                            " trn_Payroll_Details " &
                                        " WHERE " &
                                            " PayrollID = a.PayrollID " &
                                        " AND ParticularCode IN( " &
                                            " SELECT " &
                                                " ParticularCode " &
                                            " FROM " &
                                                " tblMst_Particular " &
                                            " WHERE " &
                                                " IsIncome = 'Y' " &
                                        " ) " &
                                    " )AS Income, " &
                                    " ( " &
                                        " SELECT " &
                                            " SUM(ParticularAmount) " &
                                        " FROM " &
                                            " trn_Payroll_Details " &
                                        " WHERE " &
                                            " PayrollID = a.PayrollID " &
                                        " AND ParticularCode IN( " &
                                            " SELECT " &
                                                " ParticularCode " &
                                            " FROM " &
                                                " tblMst_Particular " &
                                            " WHERE " &
                                                " IsIncome = 'N' " &
                                        " ) " &
                                    " )AS Deductions " &
                                " FROM " &
                                    " trn_Payroll_Details AS a " &
                                " WHERE " &
                                    " PayrollID IN( " &
                                        " SELECT " &
                                            " PayrollID " &
                                        " FROM " &
                                            " trn_Payroll_Header " &
                                    " ) " &
                                " GROUP BY " &
                                    " a.PayrollID " &
                            " )pd " &
                        " INNER JOIN trn_Payroll_Header ph ON pd.PayrollID = ph.PayrollID " &
                    " )vm ON ph.PayrollID = vm.[PayrollID] " &
                " )sp " &
                " WHERE " & strFilter
        With Me
            .CrxRpt.Load(Application.StartupPath & "\Reports\Payroll Register Summary.rpt")
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
End Class