Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Public Class frmLeave
    Dim PK As Double
    Private Sub frmLeave_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Dim i As Integer

        cboFields.Items.Clear()
        For i = 0 To lvList.Columns.Count - 1
            cboFields.Items.Add(lvList.Columns(i).Text)
        Next

        cboFields.SelectedIndex = 1
        txtFind.Focus()
    End Sub

    Private Sub frmLeave_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
    Private Sub frmLeave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        PK = 0
        Call ClearAllText(Me)
        Call FillListview(" SELECT " &
                        " lf.LeaveCode As [Application No], " &
                        " lf.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName AS [Employee Name], " &
                        " l.ShortDesc As [Leave Type], " &
                        " lf.StartDate AS [Start Date], " &
                        " lf.EndDate As [End Date], " &
                        " lf.Days, " &
                        " lf.Reason " &
                    " FROM " &
                        " trn_Leave_Filling lf(nolock) " &
                    " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                    " LEFT OUTER JOIN tblMst_LeaveType l(nolock)On lf.LeaveTypeCode = l.LeaveTypeCode " &
                    " ORDER BY " &
                        " lf.LeaveCode ASC ", lvList)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub frmLeave_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvList.Height = (Me.Height - p_FOOTER.Height) - (lvList.Top + 40)
            lvList.Width = Me.Width - (lvList.Left + 20)
            Panel3.Width = lvList.Width
        End If
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
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Application No").ToString(), 0)
            lv.SubItems.Add(Dtrdr.Item("Employee No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Employee Name").ToString())
            lv.SubItems.Add(Dtrdr.Item("Leave Type").ToString())

            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("Start Date").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("End Date").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Days").ToString())

            lv.SubItems.Add(Dtrdr.Item("Reason").ToString())
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
                With frmLeaveAE
                    .State = FormState.AddStateMode
                    .ShowDialog()
                End With

            Case "Update"
                If lvList.SelectedItems.Count < 1 Then Exit Sub
                If lvList.Items.Count = 0 Then Exit Sub

                With frmLeaveAE
                    .State = FormState.EditStateMode
                    .PK = PK
                    .ShowDialog()
                End With

            Case "Delete"
                If lvList.SelectedItems.Count < 1 Then Exit Sub
                If lvList.Items.Count = 0 Then Exit Sub

                If MsgBox("Are you sure you want to delete selected record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Call DeleteSQL("trn_Leave_Filling", "LeaveCode", True, "", PK)
                Else
                    Exit Sub
                End If

                MsgBox("Record has been successfully deleted!", MsgBoxStyle.Critical)
                CommandPass("Refresh")

            Case "Refresh"
                PK = 0
                Call ClearAllText(Me)
                Call FillListview(" SELECT " &
                                " lf.LeaveCode As [Application No], " &
                                " lf.EmployeeCode AS [Employee No], " &
                                " e.LastName + ', ' + e.FirstName AS [Employee Name], " &
                                " l.ShortDesc As [Leave Type], " &
                                " lf.StartDate AS [Start Date], " &
                                " lf.EndDate As [End Date], " &
                                " lf.Days, " &
                                " lf.Reason " &
                            " FROM " &
                                " trn_Leave_Filling lf(nolock) " &
                            " LEFT OUTER JOIN tblMst_Employee e(nolock)ON lf.EmployeeCode = e.EmployeeCode " &
                            " LEFT OUTER JOIN tblMst_LeaveType l(nolock)On lf.LeaveTypeCode = l.LeaveTypeCode " &
                            " ORDER BY " &
                                " lf.LeaveCode ASC ", lvList)
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
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        CommandPass("New")
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        CommandPass("Update")
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

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

    End Sub
End Class