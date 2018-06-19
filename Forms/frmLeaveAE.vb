Option Explicit On
Option Strict Off
Friend Class frmLeaveAE
    Public State As FormState

    Public PK As Double
    Dim sSQL As String

    Private Sub frmLeaveAE_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub frmLeaveAE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrTrapper
        CenterForm(Me)

        Call FillComboBox(dcLeaveType, "SELECT * FROM tblMst_LeaveType ", "Description", "LeaveTypeCode")

        If State = FormState.AddStateMode Then
            Me.Text = "Create New Entry"
            Call ClearAllText(Me)

            txtID.Text = "(Auto)"
            txtEmployeeNo.Focus()
            dtFrom.Value = GetServerDateTime()
            dtTo.Value = GetServerDateTime()
            CheckBox1.Checked = False

        ElseIf State = FormState.EditStateMode Then
            Me.Text = "Modify Existing Entry"
            Call DisplayForEditing(PK)
        End If

        Exit Sub
ErrTrapper:
        MsgBox("Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description, MsgBoxStyle.Exclamation)
    End Sub

    Private Sub frmLeaveAE_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
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

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        frmSelectEmployee.ShowDialog()
    End Sub

    Private Sub DisplayForEditing(ByVal p_k As Double)
        On Error GoTo ErrHandler
        Dim dt As New DataTable

        dt = GetDataSet("SELECT * FROM trn_Leave_Filling WHERE LeaveCode=" & p_k)

        If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

        txtID.Text = dt.Rows(0)("LeaveCode")
        txtEmployeeNo.Text = dt.Rows(0)("EmployeeCode")
        Call GetEmployeeInfo(dt.Rows(0)("EmployeeCode"))

        dcLeaveType.SelectedValue = dt.Rows(0)("LeaveTypeCode")
        dtFrom.Value = dt.Rows(0)("StartDate")
        dtTo.Value = dt.Rows(0)("EndDate")
        txtDays.Text = toNumber(dt.Rows(0)("Days"))
        txtReason.Text = dt.Rows(0)("Reason")
        CheckBox1.Checked = changeYNValue(dt.Rows(0)("WPay"))

        LAST_USER.Text = "Last Updated By : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("ModifiedBy").ToString), dt.Rows(0)("EncodedBy").ToString, dt.Rows(0)("ModifiedBy").ToString)
        LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("LastDateModified").ToString), dt.Rows(0)("DateEncoded").ToString, dt.Rows(0)("LastDateModified").ToString)
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub dtFrom_ValueChanged(sender As Object, e As EventArgs) Handles dtFrom.ValueChanged
        txtDays.Text = DateDiff("d", dtFrom.Value, dtTo.Value)
    End Sub

    Private Sub dtTo_ValueChanged(sender As Object, e As EventArgs) Handles dtTo.ValueChanged
        txtDays.Text = DateDiff("d", dtFrom.Value, dtTo.Value)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If is_empty(txtID) = True Then Exit Sub
        If is_empty(txtEmployeeNo) = True Then Exit Sub
        If dcLeaveType.SelectedValue = 0 Then Exit Sub

        If State = FormState.AddStateMode Then
            sSQL = "INSERT INTO trn_Leave_Filling"
            sSQL = sSQL & "(LeaveCode"
            sSQL = sSQL & ", EmployeeCode"
            sSQL = sSQL & ", LeaveTypeCode"
            sSQL = sSQL & ", StartDate"
            sSQL = sSQL & ", EndDate"
            sSQL = sSQL & ", Days"
            sSQL = sSQL & ", Reason"
            sSQL = sSQL & ", WPay"
            sSQL = sSQL & ", StatusCode"
            sSQL = sSQL & ", DateEncoded"
            sSQL = sSQL & ", EncodedBy"
            sSQL = sSQL & ") VALUES ("
            sSQL = sSQL & " " & toNumber(GenerateNewIDNumber("trn_Leave_Filling", "LeaveCode"))
            sSQL = sSQL & ", " & toNumber(txtEmployeeNo.Text)
            sSQL = sSQL & ", " & toNumber(dcLeaveType.SelectedValue)
            sSQL = sSQL & ", '" & (dtFrom.Value) & "'"
            sSQL = sSQL & ", '" & (dtTo.Value) & "'"
            sSQL = sSQL & ", " & toNumber(txtDays.Text)
            sSQL = sSQL & ", '" & sqlSafe(txtReason.Text) & "'"
            sSQL = sSQL & ", '" & changeYNValue(CheckBox1.Checked) & "'"
            sSQL = sSQL & ", 'AP'"
            sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

        ElseIf State = FormState.EditStateMode Then
            sSQL = "UPDATE trn_Leave_Filling SET"
            sSQL = sSQL & " EmployeeCode = " & toNumber(txtEmployeeNo.Text)
            sSQL = sSQL & " ,LeaveTypeCode = " & toNumber(dcLeaveType.SelectedValue)
            sSQL = sSQL & " ,StartDate = '" & dtFrom.Value & "'"
            sSQL = sSQL & " ,EndDate = '" & dtTo.Value & "'"
            sSQL = sSQL & " ,Days = " & toNumber(txtDays.Text)
            sSQL = sSQL & " ,Reason = '" & sqlSafe(txtReason.Text) & "'"
            sSQL = sSQL & " ,WPay = '" & sqlSafe(changeYNValue(CheckBox1.Checked)) & "'"
            sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
            sSQL = sSQL & " WHERE LeaveCode = " & PK
        End If

        Call ExecuteSQL(sSQL)
        frmWait.seconds = 1
        frmWait.ShowDialog()

        If State = FormState.AddStateMode Then
            MsgBox("New leave record has been successfully saved!", vbInformation)
        ElseIf State = FormState.EditStateMode Then
            MsgBox("Information saved successfully!", vbInformation)
        End If

        Me.Dispose()
        frmLeave.CommandPass("Refresh")

    End Sub
End Class