Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Public Class frmLoanManager
    Dim PK As Double
    Private Sub frmLoanManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        PK = 0
        Call ClearAllText(Me)
        Call FillListview(" SELECT " &
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
                                " lf.MonthlyDeduction " &
                            " FROM " &
                                " trn_Loan_Filling lf(nolock) " &
                            " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                            " LEFT OUTER JOIN tblMst_Particular p(nolock)On lf.ParticularCode = p.ParticularCode " &
                            " ORDER BY " &
                                " lf.LoanCode ASC ", lvList)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub FillListview(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        srcListview.SmallImageList = MAIN.i16x16
        srcListview.LargeImageList = MAIN.i16x16

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Loan No").ToString(), 8)
            lv.SubItems.Add(Dtrdr.Item("Employee No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Employee Name").ToString())
            lv.SubItems.Add(Dtrdr.Item("ReferenceNo").ToString())
            lv.SubItems.Add(Dtrdr.Item("ParticularCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("LoanDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(toMoney(Dtrdr.Item("LoanAmount").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Interest").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Terms").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("MonthlyDeduction").ToString()))
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Public Sub CommandPass(ByVal srcPerformWhat As String)
        On Error GoTo ErrHandler
        Select Case srcPerformWhat
            Case "New"
                With frmLoanAE
                    .State = FormState.AddStateMode
                    .ShowDialog()
                End With

            Case "Update"
                If lvList.SelectedItems.Count < 1 Then Exit Sub
                If lvList.Items.Count = 0 Then Exit Sub

                With frmLoanAE
                    .State = FormState.EditStateMode
                    .PK = PK
                    .ShowDialog()
                End With

            Case "Delete"
                If lvList.SelectedItems.Count < 1 Then Exit Sub
                If lvList.Items.Count = 0 Then Exit Sub

                If IsRecordExist("trn_Loan_Payment", "LoanCode", PK, False) = True Then
                    MsgBox("Unable to delete loan details. Selected loan has already payment posted.Please check it!", MsgBoxStyle.Critical)
                    Exit Sub
                End If

                If MsgBox("Are you sure you want to delete selected loan record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Call DeleteSQL("trn_Loan_Filling", "LoanCode", True, "", PK)
                Else
                    Exit Sub
                End If

                MsgBox("Record has been successfully deleted!", MsgBoxStyle.Critical)
                CommandPass("Refresh")

            Case "Refresh"
                PK = 0
                Call ClearAllText(Me)
                Call FillListview(" SELECT " &
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
                                " lf.MonthlyDeduction " &
                            " FROM " &
                                " trn_Loan_Filling lf(nolock) " &
                            " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                            " LEFT OUTER JOIN tblMst_Particular p(nolock)On lf.ParticularCode = p.ParticularCode " &
                            " ORDER BY " &
                                " lf.LoanCode ASC ", lvList)
            Case "Print"
                Dim dt As New DataTable
                Dim sSQL As String

                With frmReportViewer
                    .CrxRpt.Load(Application.StartupPath & "\Reports\SSS Table.rpt")

                    dt = GetDataSet(" SELECT " &
                                    " Convert(" &
                                        " VARCHAR(50), " &
                                        " s.SalaryMinValue " &
                                    " )+ ' - ' + CONVERT( " &
                                        " VARCHAR(50), " &
                                        " s.SalaryMaxValue " &
                                    " )As [Salary Range], " &
                                    " s.SalaryBaseValue AS [Monthly Salary], " &
                                    " s.EmployerShare, " &
                                    " s.EmployeeShare, " &
                                    " s.TotalMonthlyPremium, " &
                                    " s.ExtendedContribution, " &
                                    " s.TotalEmployerContribution, " &
                                    " s.TotalEmployeeContribution, " &
                                    " s.TotalEEECContribution, " &
                                    " s.SEOFW " &
                                " FROM " &
                                    " tblMst_SSSTable s(nolock) ORDER BY s.SSSCode ASC ")

                    .CrxRpt.DataSourceConnections.Item(0).SetConnection(DB_Source, DB_Name, False)
                    .CrxRpt.DataSourceConnections.Item(0).SetLogon(DB_UserID, DB_Password)

                    .CrxRpt.SetDataSource(dt)
                    .CrxRpt.SetParameterValue(0, ACTIVE_LOCATION.m_LocationID)
                    .CrxRpt.SetParameterValue(1, GetServerDateTime())

                    .CRViewer.ReportSource = .CrxRpt
                    .CRViewer.Refresh()
                    .ShowDialog()
                End With

            Case "Close"
                Me.Dispose()
        End Select
        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged
        Dim i As Integer

        For i = 0 To lvList.SelectedItems.Count - 1
            PK = toNumber(lvList.SelectedItems(i).Text)
        Next

        Call FillFormView(PK)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        CommandPass("New")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        CommandPass("Delete")
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        CommandPass("Refresh")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CommandPass("Close")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CommandPass("Print")
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        CommandPass("Update")
    End Sub

    Private Sub lvList_KeyPress(sender As Object, e As KeyPressEventArgs) Handles lvList.KeyPress
        If Asc(e.KeyChar) = 13 Then CommandPass("Update")
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        CommandPass("Update")
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
                PK = reader.Item("Loan No").ToString

                txtParticular.Text = reader.Item("Description").ToString
                txtRefNo.Text = reader.Item("ReferenceNo").ToString
                txtLoanAmount.Text = toMoney(reader.Item("LoanAmount").ToString)

                txtLoanedDate.Text = FormatDateTime(reader.Item("LoanDate").ToString, DateFormat.ShortDate)
                txtTerms.Text = toMoney(reader.Item("Terms").ToString)
                txtInterest.Text = toMoney(reader.Item("Interest").ToString)

                txtMonthlyDeduction.Text = toMoney(reader.Item("MonthlyDeduction").ToString)
                txtStartPayment.Text = FormatDateTime(reader.Item("StartPay").ToString, DateFormat.ShortDate)
                txtEndPayment.Text = FormatDateTime(reader.Item("EndPay").ToString, DateFormat.ShortDate)
            End If
            reader.Close()
            SQLCmd.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        Finally
            MyConn.Close()
        End Try
    End Sub

    Private Sub lvList_Click(sender As Object, e As EventArgs) Handles lvList.Click
        If lvList.Items.Count = 0 Then Exit Sub
        PK = lvList.SelectedItems(0).Text

        Call FillFormView(PK)
    End Sub

    Private Sub lvList_ItemSelectionChanged(sender As Object, e As ListViewItemSelectionChangedEventArgs) Handles lvList.ItemSelectionChanged
        Call FillFormView(PK)
    End Sub

End Class