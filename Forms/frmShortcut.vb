Option Explicit On
Option Strict Off
Public Class frmShortcut
    Private Sub frmShortcut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoading

        lvMenu.SmallImageList = MAIN.i32x32
        lvMenu.LargeImageList = MAIN.i32x32

        With lvMenu
            .Items.Clear()
            .Items.Add("Employee Information", 0)
            .Items.Add("Department", 21)
            .Items.Add("Division", 7)
            .Items.Add("Unit/Area", 23)
            .Items.Add("Account Particular", 6)
            .Items.Add("Payroll Distribution", 11)
            .Items.Add("Leave Type", 8)
            .Items.Add("SSS Table", 4)
            .Items.Add("PhilHealth Table", 5)
        End With

        Exit Sub
ErrLoading: MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmShortcut_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvMenu.Height = (Me.Height) - (lvMenu.Top + 40)
            lvMenu.Width = Me.Width - (lvMenu.Left + 20)
        End If
    End Sub

    Private Sub btnFileMaintenance_Click(sender As Object, e As EventArgs) Handles btnFileMaintenance.Click
        On Error GoTo Err
        With lvMenu
            .Items.Clear()
            .Items.Add("Employee Information", 0)
            .Items.Add("Department", 21)
            .Items.Add("Division", 7)
            .Items.Add("Unit/Area", 23)
            .Items.Add("Account Particular", 6)
            .Items.Add("Payroll Distribution", 11)
            .Items.Add("Leave Type", 8)
            .Items.Add("SSS Table", 4)
            .Items.Add("PhilHealth Table", 5)
        End With
        Exit Sub
Err:    MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnTransaction_Click(sender As Object, e As EventArgs) Handles btnTransaction.Click
        On Error GoTo Err
        With lvMenu
            .Items.Clear()
            .Items.Add("Payroll Period", 13)
            .Items.Add("Payroll Generation", 12)
            .Items.Add("Payroll Manager", 12)
            .Items.Add("Payroll Advice Printing", 15)

            .Items.Add("Overtime Filling", 20)
            .Items.Add("Overtime Approval", 19)

            .Items.Add("Leave Filling", 17)
            .Items.Add("Leave Approval", 18)

            .Items.Add("Loans & Deductions Manager", 16)
            .Items.Add("Loan Payment Posting", 14)
        End With
        Exit Sub
Err:    MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        On Error GoTo Err
        With lvMenu
            .Items.Clear()
            .Items.Add("Employee Alphalist", 3)
            .Items.Add("Employee Length of Service", 3)
            .Items.Add("List of Birthday Celebrant", 3)
            .Items.Add("Employee Leave Filling", 3)
            .Items.Add("Employee Payroll Register", 3)
            .Items.Add("Payroll Register Summary", 3)
            .Items.Add("Employee Loan Register", 3)
            .Items.Add("Employee Loan Balances", 3)
            .Items.Add("Payslip Printing", 3)
        End With
        Exit Sub
Err:    MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub lvMenu_DoubleClick(sender As Object, e As EventArgs) Handles lvMenu.DoubleClick
        Select Case lvMenu.FocusedItem.Text
            Case "Employee Information" : frmEmployeeFile.ShowDialog()
            Case "Department" : frmDepartment.ShowDialog()
            Case "Division" : frmDivision.ShowDialog()
            Case "Unit/Area" : frmUnitArea.ShowDialog()
            Case "Account Particular" : frmParticular.ShowDialog()
            Case "Payroll Distribution" : frmPayrollDistribution.ShowDialog()
            Case "Leave Type" : frmLeaveType.ShowDialog()
            Case "SSS Table" : frmSSSTable.ShowDialog()
            Case "PhilHealth Table" : frmPHICTable.ShowDialog()

            Case "Payroll Period" : frmPayrollPeriod.ShowDialog()
            Case "Payroll Manager" : frmPayrollBatchManager.ShowDialog()

            Case "Leave Filling" : frmLeave.ShowDialog()
            Case "Loans & Deductions Manager" : frmLoanManager.ShowDialog()
            Case "Loan Payment Posting" : frmLoanPaymentPosting.ShowDialog()
            Case "Payroll Generation" : frmPayrollInitiation.ShowDialog()

            Case "Employee Alphalist" : Call LoadForm(frmRptEmployeeAlphalist)
            Case "Employee Length of Service" : Call LoadForm(frmRptEmployeeLenghtOfService)
            Case "List of Birthday Celebrant" : Call LoadForm(frmRptBirthdayCelebrant)

            Case "Employee Leave Filling" : Call LoadForm(frmRptEmployeeLeaveFilling)
            Case "Employee Payroll Register" : Call LoadForm(frmRptPayrollRegisterEmployee)
            Case "Payroll Register Summary" : Call LoadForm(frmRptPayrollRegisterSummary)
            Case "Employee Loan Register" : Call LoadForm(frmRptEmployeeLoanRegister)
            Case "Employee Loan Balances" : Call LoadForm(frmRptEmployeeLoanBalances)
        End Select
    End Sub

    Private Sub lvMenu_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lvMenu.KeyPress
        If Asc(e.KeyChar) = 13 Then lvMenu_DoubleClick(lvMenu, New EventArgs)
    End Sub


    Private Sub btnConfig_Click(sender As Object, e As EventArgs) Handles btnConfig.Click
        Try
            With lvMenu
                .Items.Clear()
                .Items.Add("System Module", 18)
                .Items.Add("Application Role", 25)
                .Items.Add("User Access Permission", 3)
                .Items.Add("Local User Manager", 24)
                .Items.Add("System Configuration", 26)
                .Items.Add("Audit Trails", 27)
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class