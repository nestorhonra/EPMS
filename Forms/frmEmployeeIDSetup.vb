Option Explicit On
Option Strict Off

Imports System.Data
Imports System.Data.SqlClient
Public Class frmEmployeeIDSetup
    Public src_EmployeeNo As Double
    Private Sub frmEmployeeIDSetup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrHandler
        CenterForm(Me)

        lvIncome.SmallImageList = MAIN.i16x16
        lvIncome.LargeImageList = MAIN.i16x16

        lvDeduction.SmallImageList = MAIN.i16x16
        lvDeduction.LargeImageList = MAIN.i16x16

        Call FillComboBox(dcIncome, "SELECT * FROM tblMst_Particular WHERE IsIncome='Y' AND IsActive='Y' ORDER BY ParticularCode ASC ", "Description", "ParticularCode")
        Call FillComboBox(dcDeduction, "SELECT * FROM tblMst_Particular WHERE IsIncome='N' AND IsActive='Y' ORDER BY ParticularCode ASC ", "Description", "ParticularCode")

        Call GetEmployeeInfo(src_EmployeeNo)
        Call FillIncome(" SELECT " &
                        " e.EmployeeCode, " &
                        " e.ParticularCode, " &
                        " p.Description, " &
                        " e.Unit, " &
                        " e.ParticularAmount " &
                    " FROM " &
                        " trn_Employee_DefaultIncomeDeduction e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Particular p(nolock)ON e.ParticularCode = p.ParticularCode " &
                    " WHERE " &
                        " p.IsIncome = 'Y' " &
                    " AND e.EmployeeCode =" & toNumber(src_EmployeeNo), lvIncome)

        Call FillDeduction(" SELECT " &
                        " e.EmployeeCode, " &
                        " e.ParticularCode, " &
                        " p.Description, " &
                        " e.Unit, " &
                        " e.ParticularAmount, " &
                        " e.EmployerShare " &
                    " FROM " &
                        " trn_Employee_DefaultIncomeDeduction e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Particular p(nolock)ON e.ParticularCode = p.ParticularCode " &
                    " WHERE " &
                        " p.IsIncome = 'N' " &
                    " AND e.EmployeeCode =" & toNumber(src_EmployeeNo), lvDeduction)

        Exit Sub
ErrHandler: MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Public Sub GetEmployeeInfo(ByVal p_k As Double)
        On Error GoTo ErrHandler
        Dim dt As New DataTable

        dt = GetDataSet("SELECT * FROM tblMst_Employee WHERE EmployeeCode=" & p_k)

        If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

        txtEmployeeNo.Text = dt.Rows(0)("EmployeeCode")
        txtEmployeeName.Text = UCase(dt.Rows(0)("LastName")) & ", " & UCase(dt.Rows(0)("FirstName"))

        txtDepartment.Text = GetValueAt("SELECT * FROM tblMst_Department WHERE DepartmentCode=" & toNumber(dt.Rows(0)("DepartmentCode")), "Remarks")
        txtJobTitle.Text = GetValueAt("SELECT * FROM tblMst_JobTitle WHERE JobTitleCode=" & toNumber(dt.Rows(0)("JobTitleCode")), "Description")

        txtPayrollDistribution.Text = GetValueAt("SELECT * FROM tblMst_PayrollDistribution WHERE PayrollDistributionCode=" & toNumber(dt.Rows(0)("PayrollDistributionCode")), "Description")
        txtSalary.Text = toMoney(dt.Rows(0)("MonthlyRate"))

        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmEmployeeIDSetup_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub FillIncome(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add("", 9)
            lv.SubItems.Add(Dtrdr.Item("ParticularCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
            lv.SubItems.Add(toNumber(Dtrdr.Item("Unit").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("ParticularAmount").ToString()))
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub FillDeduction(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add("", 9)
            lv.SubItems.Add(Dtrdr.Item("ParticularCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
            lv.SubItems.Add(toNumber(Dtrdr.Item("Unit").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("ParticularAmount").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("EmployerShare").ToString()))
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub InsertDefaultIncome()
        Dim srcSQL As String

        For Each ls As ListViewItem In lvIncome.Items
            srcSQL = "INSERT INTO trn_Employee_DefaultIncomeDeduction"
            srcSQL = srcSQL & "(EmployeeCode"
            srcSQL = srcSQL & ", ParticularCode"
            srcSQL = srcSQL & ", Unit"
            srcSQL = srcSQL & ", ParticularAmount"
            srcSQL = srcSQL & ", EmployerShare"
            srcSQL = srcSQL & ") VALUES ("
            srcSQL = srcSQL & " " & toNumber(src_EmployeeNo)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(1).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(3).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(4).Text)
            srcSQL = srcSQL & ", " & toNumber(0) & ")"

            Call ExecuteSQL(srcSQL)
        Next
    End Sub

    Private Sub InsertDefaultDeduction()
        Dim srcSQL As String

        For Each ls As ListViewItem In lvDeduction.Items
            srcSQL = "INSERT INTO trn_Employee_DefaultIncomeDeduction"
            srcSQL = srcSQL & "(EmployeeCode"
            srcSQL = srcSQL & ", ParticularCode"
            srcSQL = srcSQL & ", Unit"
            srcSQL = srcSQL & ", ParticularAmount"
            srcSQL = srcSQL & ", EmployerShare"
            srcSQL = srcSQL & ") VALUES ("
            srcSQL = srcSQL & " " & toNumber(src_EmployeeNo)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(1).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(3).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(4).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(5).Text) & ")"

            Call ExecuteSQL(srcSQL)
        Next
    End Sub

    Private Sub lvDeduction_DoubleClick(sender As Object, e As EventArgs) Handles lvDeduction.DoubleClick
        If lvDeduction.Items.Count = 0 Then Exit Sub

        If lvDeduction.SelectedItems.Count > 0 Then
            lvDeduction.Items.Remove(lvDeduction.SelectedItems(0))
        Else
            MsgBox("Invalid selection. Please check it!", vbExclamation)
            Exit Sub
        End If
    End Sub

    Private Sub lvDeduction_KeyDown(sender As Object, e As KeyEventArgs) Handles lvDeduction.KeyDown
        Try
            If e.KeyValue = 46 Then
                If lvDeduction.Items.Count = 0 Then Exit Sub
                lvDeduction.Items.Remove(lvDeduction.SelectedItems(0))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAddIncome_Click(sender As Object, e As EventArgs) Handles btnAddIncome.Click
        If is_empty(txtIncomeUnit) = True Then Exit Sub
        If toNumber(txtIncomeAmount.Text) = 0 Then Exit Sub

        Dim lv As ListViewItem = lvIncome.Items.Add("", 9)
        lv.SubItems.Add(dcIncome.SelectedValue)
        lv.SubItems.Add(GetValueAt("SELECT * FROM tblMst_Particular WHERE ParticularCode=" & toNumber(dcIncome.SelectedValue), "Description"))
        lv.SubItems.Add(toMoney(txtIncomeUnit.Text))
        lv.SubItems.Add(toMoney(txtIncomeAmount.Text))

        txtIncomeUnit.Text = 0
        txtIncomeAmount.Text = 0
        dcIncome.Focus()
    End Sub

    Private Sub btnAddDeduction_Click(sender As Object, e As EventArgs) Handles btnAddDeduction.Click
        If is_empty(txtDeductionUnit) = True Then Exit Sub
        If toNumber(txtDeductionAmount.Text) = 0 Then Exit Sub

        Dim lv As ListViewItem = lvDeduction.Items.Add("", 7)
        lv.SubItems.Add(dcDeduction.SelectedValue)
        lv.SubItems.Add(GetValueAt("SELECT * FROM tblMst_Particular WHERE ParticularCode=" & toNumber(dcDeduction.SelectedValue), "Description"))
        lv.SubItems.Add(toMoney(txtDeductionUnit.Text))
        lv.SubItems.Add(toMoney(txtDeductionAmount.Text))
        lv.SubItems.Add(toMoney(txtEmployerShare.Text))

        txtDeductionUnit.Text = 0
        txtDeductionAmount.Text = 0
        txtEmployerShare.Text = 0
        dcDeduction.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If is_empty(txtEmployeeNo) = True Then Exit Sub
        If lvIncome.Items.Count = 0 Then Exit Sub

        If MsgBox("Are you sure you want to proceed employee's payroll setup?", vbQuestion + vbYesNo) = vbYes Then

            frmWait.seconds = 2
            frmWait.lblProgress.Text = "please wait..."
            frmWait.ShowDialog()

            Call DeleteSQL("trn_Employee_DefaultIncomeDeduction", "EmployeeCode", True, "", src_EmployeeNo)
            Call InsertDefaultIncome()
            Call InsertDefaultDeduction()

            MsgBox("Employee's default income and deduction list has been successfully set", MsgBoxStyle.Information)

            Me.Dispose()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub


    Private Sub lvIncome_DoubleClick(sender As Object, e As EventArgs) Handles lvIncome.DoubleClick
        If lvIncome.Items.Count = 0 Then Exit Sub

        If lvIncome.SelectedItems.Count > 0 Then
            lvIncome.Items.Remove(lvIncome.SelectedItems(0))
        Else
            MsgBox("Invalid selection. Please check it!", vbExclamation)
            Exit Sub
        End If
    End Sub
End Class