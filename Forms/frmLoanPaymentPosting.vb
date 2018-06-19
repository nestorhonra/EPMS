Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Friend Class frmLoanPaymentPosting
    Public State As FormState
    Public PK As Double

    Dim sSQL As String
    Dim i As Double
    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        frmSelectLoanForPaymentPosting.ShowDialog()
    End Sub

    Private Sub frmLoanPaymentPosting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        Call ClearAllText(Me)
        State = FormState.ViewStateMode
        Dim i As Integer
        dcMonth.Items.Clear()
        For i = 1 To 12
            dcMonth.Items.Add(MonthName(i))
        Next
        i = 0

        dcYear.Items.Clear()
        For i = 2016 To Year(Now) + 2
            dcYear.Items.Add(i)
        Next
        i = 0

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub

    Public Sub FillFormView(ByVal p_k As Double)
        Try
            If MyConn.State = ConnectionState.Open Then MyConn.Close()
            MyConn.Open()

            Dim SQLCmd As New SqlCommand(" SELECT " &
                                        " lf.LoanCode As [Loan No], " &
                                        " lf.EmployeeCode AS [Employee No], " &
                                        " e.LastName + ', ' + e.FirstName AS [Employee Name], " &
                                        " lf.ReferenceNo, " &
                                        " lf.ParticularCode AS [ParticularCode], " &
                                        " p.Description, " &
                                        " lf.LoanDate, " &
                                        " lf.LoanAmount, " &
                                        " lf.Interest, " &
                                        " lf.Terms, " &
                                        " lf.MonthlyDeduction, " &
                                        " lf.StartPay, " &
                                        " lf.EndPay " &
                                    " FROM " &
                                        " trn_Loan_Filling lf(nolock) " &
                                    " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                                    " LEFT OUTER JOIN tblMst_Particular p(nolock)On lf.ParticularCode = p.ParticularCode " &
                                    " WHERE lf.LoanCode=" & toNumber(p_k), MyConn)

            Dim reader As SqlDataReader = SQLCmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                txtLoanNo.Text = reader.Item("Loan No").ToString
                txtEmployeeNo.Text = reader.Item("Employee No").ToString
                txtEmployeeName.Text = reader.Item("Employee Name").ToString

                txtParticular.Text = reader.Item("Description").ToString
                txtRefNo.Text = reader.Item("ReferenceNo").ToString
                txtLoanAmount.Text = toMoney(reader.Item("LoanAmount").ToString)

                txtLoanedDate.Text = FormatDateTime(reader.Item("LoanDate").ToString, DateFormat.ShortDate)
                txtTerms.Text = toMoney(reader.Item("Terms").ToString)
                txtInterest.Text = toMoney(reader.Item("Interest").ToString)

                txtMonthlyDeduction.Text = toMoney(reader.Item("MonthlyDeduction").ToString)
                txtAmount.Text = toMoney(reader.Item("MonthlyDeduction").ToString)

                txtStartPayment.Text = FormatDateTime(reader.Item("StartPay").ToString, DateFormat.ShortDate)
                txtEndPayment.Text = FormatDateTime(reader.Item("EndPay").ToString, DateFormat.ShortDate)
            End If

            Call FillListview(" SELECT " &
                                        " lp.LoanCode As [LoanCode], " &
                                        " lp.LoanPaymentCode, " &
                                        " lp.PeriodMonth, " &
                                        " lp.PeriodYear, " &
                                        " lp.Amount, " &
                                        " lp.PaymentDate, " &
                                        " lp.ORNo " &
                                    " FROM " &
                                        " trn_Loan_Payment lp(nolock) " &
                                    " WHERE lp.LoanCode=" & toNumber(p_k), lvList)
            reader.Close()
            SQLCmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            MyConn.Close()
        End Try
    End Sub

    Public Sub FillFormViewPayment(ByVal p_k As Double)
        Try
            If MyConn.State = ConnectionState.Open Then MyConn.Close()
            MyConn.Open()

            Dim SQLCmd As New SqlCommand(" SELECT " &
                                        " lp.* " &
                                    " FROM " &
                                        " trn_Loan_Payment lp(nolock) " &
                                    " WHERE lp.LoanPaymentCode=" & toNumber(p_k), MyConn)

            Dim reader As SqlDataReader = SQLCmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                txtPaymentNo.Text = reader.Item("LoanPaymentCode").ToString
                dcMonth.Text = MonthName(reader.Item("PeriodMonth").ToString)
                dcYear.Text = toNumber(reader.Item("PeriodYear").ToString)
                txtAmount.Text = toMoney(reader.Item("Amount").ToString)
                dtPaymentDate.Text = FormatDateTime(reader.Item("PaymentDate").ToString, DateFormat.ShortDate)
                txtOR.Text = reader.Item("ORNo").ToString
            End If

            reader.Close()
            SQLCmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            MyConn.Close()
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        If is_empty(txtLoanNo) = True Then
            btnNew.Enabled = False
            State = FormState.AddStateMode
            dtPaymentDate.Value = GetServerDateTime()
            dcMonth.Text = MonthName(Month(Now))
            dcYear.Text = toNumber(Year(Now))
            txtAmount.Text = 0
            txtPaymentNo.Text = "(Auto)"
            frmSelectLoanForPaymentPosting.ShowDialog()
        Else
            btnNew.Enabled = False
            State = FormState.AddStateMode
            dtPaymentDate.Value = GetServerDateTime()
            dcMonth.Text = MonthName(Month(Now))
            dcYear.Text = toNumber(Year(Now))
            txtAmount.Text = 0
            txtPaymentNo.Text = "(Auto)"
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        btnNew.Enabled = True
        Call ClearAllText(Me)
        Call ClearAllText(Me, GroupBox1.Controls)
        State = FormState.ViewStateMode

        lvList.Items.Clear()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If is_empty(txtPaymentNo) = True Then Exit Sub
        If is_empty(txtAmount) = True Then Exit Sub
        If toNumber(txtAmount.Text) <= 0 Then Exit Sub

        If State = FormState.AddStateMode Then
            PK = toNumber(GenerateNewIDNumber("trn_Loan_Payment", "LoanPaymentCode"))

            sSQL = "INSERT INTO trn_Loan_Payment"
            sSQL = sSQL & "(LoanPaymentCode"
            sSQL = sSQL & ", LoanCode"
            sSQL = sSQL & ", EmployeeCode"
            sSQL = sSQL & ", PeriodMonth"
            sSQL = sSQL & ", PeriodYear"
            sSQL = sSQL & ", Amount"
            sSQL = sSQL & ", ORNo"
            sSQL = sSQL & ", PaymentDate"
            sSQL = sSQL & ", DateEncoded"
            sSQL = sSQL & ", EncodedBy"
            sSQL = sSQL & ") VALUES ("
            sSQL = sSQL & " " & toNumber(PK)
            sSQL = sSQL & ", " & toNumber(txtLoanNo.Text)
            sSQL = sSQL & ", " & toNumber(txtEmployeeNo.Text)
            sSQL = sSQL & ", " & MonthValue(dcMonth.Text)
            sSQL = sSQL & ", " & toNumber(dcYear.Text)
            sSQL = sSQL & ", " & toNumber(txtAmount.Text)
            sSQL = sSQL & ", '" & sqlSafe(txtOR.Text) & "'"
            sSQL = sSQL & ", '" & dtPaymentDate.Value & "'"
            sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

        ElseIf State = FormState.EditStateMode Then

            sSQL = "UPDATE trn_Loan_Payment SET"
            sSQL = sSQL & " PeriodMonth = " & MonthValue(dcMonth.Text)
            sSQL = sSQL & " ,PeriodYear = " & toNumber(dcYear.Text)
            sSQL = sSQL & " ,Amount = " & toNumber(txtAmount.Text)
            sSQL = sSQL & " ,ORNo = '" & sqlSafe(txtOR.Text) & "'"
            sSQL = sSQL & " ,PaymentDate = '" & dtPaymentDate.Value & "'"
            sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
            sSQL = sSQL & " WHERE LoanPaymentCode = " & toNumber(PK)
        End If

        If MsgBox("Are you sure you want to post payment for the selected loan details?", vbQuestion + vbYesNo) = vbYes Then

            frmWait.seconds = 1
            frmWait.lblProgress.Text = "please wait..."
            frmWait.ShowDialog()

            Call ExecuteSQL(sSQL)
        Else
            Exit Sub
        End If

        If State = FormState.AddStateMode Then
            MsgBox("New record has been successfully saved!", MsgBoxStyle.Information)
            Call RefreshLoanPayment()
            Call CreateLog(Name, "ADD NEW RECORD", "ID NUMBER:" & toNumber(PK))
        ElseIf State = FormState.EditStateMode Then
            MsgBox("Information has been successfully updated!", MsgBoxStyle.Information)
            Call RefreshLoanPayment()
            Call CreateLog(Name, "UPDATE RECORD", "ID NUMBER:" & toNumber(PK))
        End If
    End Sub

    Private Sub FillListview(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("LoanPaymentCode").ToString())
            lv.SubItems.Add(MonthName(Dtrdr.Item("PeriodMonth").ToString()))
            lv.SubItems.Add(toNumber(Dtrdr.Item("PeriodYear").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Amount").ToString()))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("PaymentDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(toNumber(Dtrdr.Item("ORNo").ToString()))
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub RefreshLoanPayment()
        On Error GoTo ErrRefresh
        Call FillListview(" SELECT " &
                          " lp.LoanCode As [LoanCode], " &
                          " lp.LoanPaymentCode, " &
                          " lp.PeriodMonth, " &
                          " lp.PeriodYear, " &
                          " lp.Amount, " &
                          " lp.PaymentDate, " &
                          " lp.ORNo " &
                          " FROM " &
                          " trn_Loan_Payment lp(nolock) " &
                          " WHERE lp.LoanCode=" & toNumber(txtLoanNo.Text), lvList)
        Exit Sub
ErrRefresh: MsgBox(Err.Description, MsgBoxStyle.Exclamation)
    End Sub
    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged
        Dim i As Integer

        For i = 0 To lvList.SelectedItems.Count - 1
            PK = toNumber(lvList.SelectedItems(i).Text)
        Next

        Call FillFormViewPayment(PK)
    End Sub

    Private Sub lvList_Click(sender As Object, e As EventArgs) Handles lvList.Click
        Call FillFormViewPayment(PK)
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        lvList_Click(lvList, New EventArgs)
    End Sub

    Private Sub btnPrintLedger_Click(sender As Object, e As EventArgs) Handles btnPrintLedger.Click
        Dim dt As New DataTable

        If is_empty(txtLoanNo) = True Then Exit Sub

        With frmReportViewer
            .CrxRpt.Load(Application.StartupPath & "\Reports\Loan Ledger.rpt")

            dt = GetDataSet(" SELECT " &
                            " l.LoanCode, " &
                            " l.EmployeeCode, " &
                            " e.LastName + ', ' + e.FirstName AS [Employee Name], " &
                            " l.ParticularCode, " &
                            " a.Description AS [Particular], " &
                            " l.LoanDate, " &
                            " l.LoanAmount, " &
                            " l.MonthlyDeduction, " &
                            " l.Terms, " &
                            " l.Interest, " &
                            " l.[LoanAmount] * l.[Interest] AS [Principal Amount], " &
                            " l.StartPay, " &
                            " l.EndPay, " &
                            " p.LoanPaymentCode As [Payment No], " &
                            " p.PeriodMonth, " &
                            " p.PeriodYear, " &
                            " p.[ORNo], " &
                            " p.PaymentDate, " &
                            " p.Amount " &
                        " FROM " &
                            " trn_Loan_Filling l(nolock) " &
                        " LEFT OUTER JOIN trn_Loan_Payment p(nolock)On p.LoanCode = l.LoanCode " &
                        " LEFT OUTER JOIN tblMst_Employee e(nolock)ON l.EmployeeCode = e.EmployeeCode " &
                        " LEFT OUTER JOIN tblMst_Particular a(nolock)On l.ParticularCode = a.ParticularCode " &
                        " WHERE " &
                            " l.LoanCode = " & toNumber(txtLoanNo.Text))

            .CrxRpt.DataSourceConnections.Item(0).SetConnection(DB_Source, DB_Name, False)
            .CrxRpt.DataSourceConnections.Item(0).SetLogon(DB_UserID, DB_Password)

            .CrxRpt.SetDataSource(dt)
            .CrxRpt.SetParameterValue(0, ACTIVE_LOCATION.m_LocationID)
            .CrxRpt.SetParameterValue(1, GetServerDateTime())

            .CRViewer.ReportSource = .CrxRpt
            .CRViewer.Refresh()
            .ShowDialog()
        End With
    End Sub
End Class