Option Explicit On
Option Strict Off
Friend Class frmLoanAE
    Public State As FormState

    Public PK As Double
    Dim sSQL As String
    Private Sub frmLoanAE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrTrapper
        CenterForm(Me)

        Call FillComboBox(dcParticular, "SELECT * FROM tblMst_Particular WHERE IsIncome='N'", "Description", "ParticularCode")

        If State = FormState.AddStateMode Then
            Me.Text = "Create New Entry"
            Call ClearAllText(Me)

            txtID.Text = "(Auto)"
            txtEmployeeNo.Focus()
            dtLoanedDate.Value = GetServerDateTime()
            dtFrom.Value = GetServerDateTime()
            dtTo.Value = GetServerDateTime()

        ElseIf State = FormState.EditStateMode Then
            Me.Text = "Modify Existing Entry"
            Call DisplayForEditing(PK)
        End If

        Exit Sub
ErrTrapper:
        MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation)
    End Sub

    Private Sub frmLoanAE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If KeyAscii = 27 Then
            Me.Dispose()
        ElseIf KeyAscii = 13 Then
            System.Windows.Forms.SendKeys.Send("{TAB}")
        End If
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmLoanAE_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Public Sub GetEmployeeInfo(ByVal p_k As Double)
        On Error GoTo ErrHandler
        Dim dt As New DataTable

        dt = GetDataSet("SELECT * FROM tblMst_Employee WHERE EmployeeCode=" & p_k)

        If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

        txtEmployeeNo.Text = dt.Rows(0)("EmployeeCode")
        txtEmployeeName.Text = dt.Rows(0)("LastName") & ", " & dt.Rows(0)("FirstName")
        txtDepartment.Text = GetValueAt("SELECT * FROM tblMst_Department WHERE DepartmentCode='" & dt.Rows(0)("DepartmentCode") & "'", "Remarks")
        txtJobTitle.Text = GetValueAt("SELECT * FROM tblMst_JobTitle WHERE JobTitleCode='" & dt.Rows(0)("JobTitleCode") & "'", "Description")

        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub DisplayForEditing(ByVal p_k As Double)
        On Error GoTo ErrHandler
        Dim dt As New DataTable

        dt = GetDataSet("SELECT * FROM trn_Loan_Filling WHERE LoanCode=" & p_k)

        If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

        txtID.Text = dt.Rows(0)("LoanCode")
        txtEmployeeNo.Text = dt.Rows(0)("EmployeeCode")
        Call GetEmployeeInfo(dt.Rows(0)("EmployeeCode"))

        dcParticular.SelectedValue = dt.Rows(0)("ParticularCode")
        txtLoanAmount.Text = toMoney(dt.Rows(0)("LoanAmount"))
        txtTerms.Text = toMoney(dt.Rows(0)("Terms"))
        txtInterest.Text = toMoney(dt.Rows(0)("Interest"))
        txtMonthlyDeduction.Text = toMoney(dt.Rows(0)("MonthlyDeduction"))
        dtLoanedDate.Value = dt.Rows(0)("LoanDate")
        dtFrom.Value = dt.Rows(0)("StartPay")
        dtTo.Value = dt.Rows(0)("EndPay")
        txtRefNo.Text = dt.Rows(0)("ReferenceNo")

        LAST_USER.Text = "Last Updated By : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("ModifiedBy").ToString), dt.Rows(0)("EncodedBy").ToString, dt.Rows(0)("ModifiedBy").ToString)
        LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("LastDateModified").ToString), dt.Rows(0)("DateEncoded").ToString, dt.Rows(0)("LastDateModified").ToString)
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If is_empty(txtID) = True Then Exit Sub
        If is_empty(txtEmployeeNo) = True Then Exit Sub
        If dcParticular.SelectedValue = 0 Then Exit Sub

        If State = FormState.AddStateMode Then
            sSQL = "INSERT INTO trn_Loan_Filling"
            sSQL = sSQL & "(LoanCode"
            sSQL = sSQL & ", EmployeeCode"
            sSQL = sSQL & ", ParticularCode"
            sSQL = sSQL & ", ReferenceNo"
            sSQL = sSQL & ", LoanDate"
            sSQL = sSQL & ", LoanAmount"
            sSQL = sSQL & ", MonthlyDeduction"
            sSQL = sSQL & ", Terms"
            sSQL = sSQL & ", Interest"
            sSQL = sSQL & ", StartPay"
            sSQL = sSQL & ", EndPay"
            sSQL = sSQL & ", Posted"
            sSQL = sSQL & ", DateEncoded"
            sSQL = sSQL & ", EncodedBy"
            sSQL = sSQL & ") VALUES ("
            sSQL = sSQL & " " & toNumber(GenerateNewIDNumber("trn_Loan_Filling", "LoanCode"))
            sSQL = sSQL & ", " & toNumber(txtEmployeeNo.Text)
            sSQL = sSQL & ", " & toNumber(dcParticular.SelectedValue)
            sSQL = sSQL & ", '" & sqlSafe(txtRefNo.Text) & "'"
            sSQL = sSQL & ", '" & (dtLoanedDate.Value) & "'"
            sSQL = sSQL & ", " & toNumber(txtLoanAmount.Text)
            sSQL = sSQL & ", " & toNumber(txtMonthlyDeduction.Text)
            sSQL = sSQL & ", " & toNumber(txtTerms.Text)
            sSQL = sSQL & ", " & toNumber(txtInterest.Text)
            sSQL = sSQL & ", '" & (dtFrom.Value) & "'"
            sSQL = sSQL & ", '" & (dtTo.Value) & "'"
            sSQL = sSQL & ", 'Y'"
            sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

        ElseIf State = FormState.EditStateMode Then
            sSQL = "UPDATE trn_Loan_Filling SET"
            sSQL = sSQL & " EmployeeCode = " & toNumber(txtEmployeeNo.Text)
            sSQL = sSQL & " ,ParticularCode = " & toNumber(dcParticular.SelectedValue)
            sSQL = sSQL & " ,ReferenceNo = '" & sqlSafe(txtRefNo.Text) & "'"
            sSQL = sSQL & " ,LoanDate = '" & dtLoanedDate.Value & "'"
            sSQL = sSQL & " ,LoanAmount = " & toNumber(txtLoanAmount.Text)
            sSQL = sSQL & " ,MonthlyDeduction = " & toNumber(txtMonthlyDeduction.Text)
            sSQL = sSQL & " ,Terms = " & toNumber(txtTerms.Text)
            sSQL = sSQL & " ,Interest = " & toNumber(txtInterest.Text)
            sSQL = sSQL & " ,StartPay = '" & dtFrom.Value & "'"
            sSQL = sSQL & " ,EndPay = '" & dtTo.Value & "'"
            sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
            sSQL = sSQL & " WHERE LoanCode = " & PK
        End If

        Call ExecuteSQL(sSQL)
        frmWait.seconds = 1
        frmWait.ShowDialog()

        If State = FormState.AddStateMode Then
            MsgBox("New loan record has been successfully saved!", vbInformation)
        ElseIf State = FormState.EditStateMode Then
            MsgBox("Information saved successfully!", vbInformation)
        End If

        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        frmSelectEmployeeForLoan.ShowDialog()
    End Sub

    Private Sub txtTerms_TextChanged(sender As Object, e As EventArgs) Handles txtTerms.TextChanged
        txtMonthlyDeduction.Text = toMoney((toNumber(txtLoanAmount.Text) * toNumber(txtInterest.Text)) / Val(toNumber(txtTerms.Text)))
        dtTo.Value = DateAdd("m", Val(toNumber(txtTerms.Text)), dtFrom.Value)
    End Sub

    Private Sub txtInterest_TextChanged(sender As Object, e As EventArgs) Handles txtInterest.TextChanged
        txtMonthlyDeduction.Text = toMoney((toNumber(txtLoanAmount.Text) * toNumber(txtInterest.Text)) / Val(toNumber(txtTerms.Text)))
    End Sub

    Private Sub txtLoanAmount_TextChanged(sender As Object, e As EventArgs) Handles txtLoanAmount.TextChanged
        txtMonthlyDeduction.Text = toMoney((toNumber(txtLoanAmount.Text) * toNumber(txtInterest.Text)) / Val(toNumber(txtTerms.Text)))
    End Sub
End Class