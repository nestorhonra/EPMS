Option Explicit On
Option Strict Off

Imports System.Data
Imports System.Data.SqlClient
Public Class frmPayrollBatchManager
    Dim sql_source As String
    Private Sub frmPayrollBatchManager_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmPayrollBatchManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CenterForm(Me)
            dtFrom.Value = CDate(GetServerDateTime())
            dtTo.Value = CDate(GetServerDateTime())

            sql_source = " SELECT " &
    " * " &
" FROM " &
    " (" &
        " SELECT " &
            " ph.PayrollID AS [Payroll No], " &
            " pp.MonthText AS [Payroll Period], " &
            " pp.StartDate, pp.EndDate, " &
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
    " )sp "
            Call FillPayrollHeader(sql_source)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillPayrollHeader(ByVal sSQL As String)
        On Error GoTo Hell

        lvList.SmallImageList = MAIN.i16x16
        lvList.LargeImageList = MAIN.i16x16

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        lvList.GridLines = True
        lvList.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = lvList.Items.Add(Dtrdr.Item("Payroll No").ToString(), 3)
            lv.SubItems.Add(Dtrdr.Item("Payroll Period").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("StartDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("EndDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Payroll Distribution").ToString())
            lv.SubItems.Add(toNumber(Dtrdr.Item("No_Of_Employees").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Total_Income").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Total_Deduction").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Net_Income").ToString()))
            lv.SubItems.Add(Dtrdr.Item("IsPosted").ToString())
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmPayrollBatchManager_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvList.Height = (Me.Height - p_FOOTER.Height) - (lvList.Top + 40)
            lvList.Width = Me.Width - (lvList.Left + 20)

            Panel3.Width = Me.Width - (lvList.Left + 20)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        sql_source = " SELECT " &
    " * " &
" FROM " &
    " (" &
        " SELECT " &
            " ph.PayrollID AS [Payroll No], " &
            " pp.MonthText AS [Payroll Period], " &
            " pp.StartDate, pp.EndDate, " &
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
    " )sp "

        Call FillPayrollHeader(sql_source)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        With frmPayrollInitiation
            .WindowState = FormState.AddStateMode
            .ShowDialog()
        End With
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        Try
            If lvList.Items.Count = 0 Then Exit Sub

            With frmPayrollBatchAE
                .Payroll_ID = toNumber(lvList.SelectedItems(0).Text)
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lvList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lvList.KeyPress
        If Asc(e.KeyChar) = 13 Then lvList_DoubleClick(lvList, New EventArgs)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lvList_DoubleClick(lvList, New EventArgs)
    End Sub

End Class