Option Explicit On
Option Strict Off

Imports System.Data
Imports System.Data.SqlClient

Friend Class frmPayrollBatchAE
    Public Payroll_ID As Double

    Dim sSQL As String
    Private Sub frmPayrollBatchAE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            CenterForm(Me)
            Me.Text = "List of Employees Under Payroll No: " & Payroll_ID
            Call FillFormView(Payroll_ID)

            sSQL = " Select " &
                    " pm.PayrollID, " &
                    " pm.EmployeeCode As [Employee No], " &
                    " pm.[Employee Name], " &
                    " pm.CompanyName As [Company], " &
                    " pm.[Job Title], " &
                    " ( " &
                        " Select " &
                            " SUM(pd.ParticularAmount)As [ParticularAmount] " &
                        " FROM " &
                            " trn_Payroll_Details pd(nolock) " &
                        " INNER JOIN tblMst_Particular p(nolock)On pd.ParticularCode = p.ParticularCode " &
                        " WHERE " &
                            " p.IsIncome = 'Y' " &
                        " AND pd.PayrollID = pm.PayrollID " &
                        " AND pd.EmployeeCode = pm.EmployeeCode " &
                    " )AS [INCOME], " &
                    " ( " &
                        " SELECT " &
                            " SUM(pd.ParticularAmount)AS [ParticularAmount] " &
                        " FROM " &
                            " trn_Payroll_Details pd(nolock) " &
                        " INNER JOIN tblMst_Particular p(nolock)ON pd.ParticularCode = p.ParticularCode " &
                        " WHERE " &
                            " p.IsIncome = 'N' " &
                        " AND pd.PayrollID = pm.PayrollID " &
                        " AND pd.EmployeeCode = pm.EmployeeCode " &
                    " )AS [DEDUCTION], " &
                    " ( " &
                        " SELECT " &
                            " SUM(pd.ParticularAmount)AS [ParticularAmount] " &
                        " FROM " &
                            " trn_Payroll_Details pd(nolock) " &
                        " INNER JOIN tblMst_Particular p(nolock)ON pd.ParticularCode = p.ParticularCode " &
                        " WHERE " &
                            " p.IsIncome = 'Y' " &
                        " And pd.PayrollID = pm.PayrollID " &
                        " AND pd.EmployeeCode = pm.EmployeeCode " &
                    " )-( " &
                        " SELECT " &
                            " SUM(pd.ParticularAmount)AS [ParticularAmount] " &
                        " FROM " &
                            " trn_Payroll_Details pd(nolock) " &
                        " INNER JOIN tblMst_Particular p(nolock)ON pd.ParticularCode = p.ParticularCode " &
                        " WHERE " &
                            " p.IsIncome = 'N' " &
                        " And pd.PayrollID = pm.PayrollID " &
                        " AND pd.EmployeeCode = pm.EmployeeCode " &
                    " )AS [NET_INCOME] " &
                " FROM " &
                    " ( " &
                        " SELECT DISTINCT " &
                            " pd.PayrollID, " &
                            " ph.PayrollDistributionCode, " &
                            " d.Description AS [Payroll Distribution], " &
                            " pd.PeriodCode, " &
                            " p.StartDate, " &
                            " p.EndDate, " &
                            " pd.EmployeeCode, " &
                            " e.LastName + ',' + e.FirstName AS [Employee Name], " &
                            " c.CompanyName, " &
                            " j.Description AS [Job Title] " &
                        " FROM " &
                            " trn_Payroll_Details pd(nolock) " &
                        " INNER JOIN trn_Payroll_Header ph(nolock)ON pd.PayrollID = ph.PayrollID " &
                        " INNER JOIN tblMst_PayrollDistribution d(nolock)ON ph.PayrollDistributionCode = d.PayrollDistributionCode " &
                        " INNER JOIN tblMst_PayrollPeriod p(nolock)ON ph.PeriodCode = p.PeriodCode " &
                        " INNER JOIN tblMst_Employee e(nolock)ON pd.EmployeeCode = e.EmployeeCode " &
                        " INNER JOIN tblMst_Company c(nolock)ON e.CompanyCode = c.CompanyCode " &
                        " INNER JOIN tblMst_JobTitle j(nolock)ON e.JobTitleCode = j.JobTitleCode " &
                    " )pm " &
                            " WHERE " &
                            " pm.PayrollID=" & toNumber(Payroll_ID)

            Call FillPayrollBatchEmployees(sSQL)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub frmPayrollBatchAE_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FillPayrollBatchEmployees(ByVal sSQL As String)
        On Error GoTo Hell

        lvEmployee.SmallImageList = MAIN.i16x16
        lvEmployee.LargeImageList = MAIN.i16x16

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        lvEmployee.GridLines = True
        lvEmployee.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = lvEmployee.Items.Add(Dtrdr.Item("PayrollID").ToString(), 3)
            lv.SubItems.Add(Dtrdr.Item("Employee No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Employee Name").ToString())
            lv.SubItems.Add(Dtrdr.Item("Job Title").ToString())
            lv.SubItems.Add(Dtrdr.Item("Company").ToString())
            lv.SubItems.Add(toMoney(Dtrdr.Item("INCOME").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("DEDUCTION").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("NET_INCOME").ToString()))
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub FillFormView(ByVal p_k As Double)
        Dim dt As New DataTable
        Try
            dt = GetDataSet("SELECT * FROM trn_Payroll_Header WHERE PayrollID=" & p_k)
            If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

            txtPayrollDistribution.Text = dt.Rows(0)("PayrollDistributionCode").ToString()
            txtDistribution.Text = GetValueAt("SELECT * FROM tblMst_PayrollDistribution WHERE PayrollDistributionCode=" & toNumber(dt.Rows(0)("PayrollDistributionCode").ToString()), "Description")
            txtPeriodCode.Text = dt.Rows(0)("PeriodCode").ToString()
            txtStart.Text = FormatDateTime(GetValueAt("SELECT * FROM tblMst_PayrollPeriod WHERE PeriodCode=" & toNumber(dt.Rows(0)("PeriodCode")), "StartDate").ToString(), DateFormat.ShortDate)
            txtEnd.Text = FormatDateTime(GetValueAt("SELECT * FROM tblMst_PayrollPeriod WHERE PeriodCode=" & toNumber(dt.Rows(0)("PeriodCode")), "EndDate").ToString(), DateFormat.ShortDate)

            txtPayrollStatus.Text = IIf(dt.Rows(0)("IsPosted").ToString() = "Y", "POSTED", "PENDING")
            LAST_USER.Text = "Last Updated By : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("ModifiedBy").ToString()), dt.Rows(0)("EncodedBy").ToString(), dt.Rows(0)("ModifiedBy").ToString())
            LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("LastDateModified").ToString()), dt.Rows(0)("DateEncoded").ToString(), dt.Rows(0)("LastDateModified").ToString())

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lvEmployee_DoubleClick(sender As Object, e As EventArgs) Handles lvEmployee.DoubleClick
        Try
            If lvEmployee.Items.Count = 0 Then Exit Sub

            With frmPayrollPerEmployee
                .payroll_id = Payroll_ID
                .payroll_distribution = toNumber(txtPayrollDistribution.Text)
                .payroll_period = toNumber(txtPeriodCode.Text)
                .employee_id = toNumber(lvEmployee.SelectedItems(0).SubItems(1).Text)
                .ShowDialog()
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub lvEmployee_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lvEmployee.KeyPress
        If Asc(e.KeyChar) = 13 Then
            lvEmployee_DoubleClick(lvEmployee, New EventArgs)
        End If
    End Sub
    Private Sub frmPayrollBatchAE_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvEmployee.Height = (Me.Height - StatusStrip1.Height) - (lvEmployee.Top + 40)
            lvEmployee.Width = Me.Width - (lvEmployee.Left + 20)

            GroupBox1.Width = Me.Width - (lvEmployee.Left + 20)
            Panel3.Width = Me.Width - (lvEmployee.Left + 20)

        End If
    End Sub
    Private Sub GroupBox1_Resize(sender As Object, e As EventArgs) Handles GroupBox1.Resize
        Panel1.Left = GroupBox1.Width - Panel1.Width
    End Sub
End Class