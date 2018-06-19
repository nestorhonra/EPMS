Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Public Class frmSelectLoanForPaymentPosting
    Dim PK As Double
    Private Sub frmSelectLoanForPaymentPosting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub frmSelectLoanForPaymentPosting_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub


    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged
        Dim i As Integer

        For i = 0 To lvList.SelectedItems.Count - 1
            PK = toNumber(lvList.SelectedItems(i).Text)
        Next
    End Sub

    Private Sub lvList_Click(sender As Object, e As EventArgs) Handles lvList.Click
        If lvList.Items.Count = 0 Then Exit Sub
        PK = lvList.SelectedItems(0).Text
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        On Error GoTo ErrHandler

        If PK = 0 Then Exit Sub

        With frmLoanPaymentPosting
            .FillFormView(PK)
        End With

        Me.Dispose()
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class