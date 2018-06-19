Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Friend Class frmPHICTable
    Public State As FormState
    Public PK As Double

    Dim blnUpdateInProgress As Boolean
    Dim sSQL As String
    Private Sub frmPHICTable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        State = FormState.ViewStateMode
        Call EnableMenu(True, False, False, False, True, True)

        Call FillListview(" SELECT " &
                                " s.* " &
                            " FROM " &
                                " tblMst_PhilHealth s " &
                            "ORDER BY s.PhilHealthCode ASC ", lvList)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub frmPHICTable_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Public Sub CommandPass(ByVal srcPerformWhat As String)
        On Error GoTo ErrHandler
        Select Case srcPerformWhat
            Case "New"
                State = FormState.AddStateMode
                blnUpdateInProgress = True

                Call ClearAllText(Me, Panel.Controls)
                Call EnableMenu(False, True, True, False, False, False)

                txtID.Text = "(Auto)"
                txtMinSalary.Focus()

            Case "Save"
                If is_empty(txtID) = True Then Exit Sub

                If State = FormState.AddStateMode Then
                    PK = GenerateNewIDNumber("tblMst_PhilHealth", "PhilHealthCode")

                    sSQL = "INSERT INTO tblMst_PhilHealth"
                    sSQL = sSQL & "(PhilHealthCode"
                    sSQL = sSQL & ", SalaryMinValue"
                    sSQL = sSQL & ", SalaryMaxValue"
                    sSQL = sSQL & ", SalaryBaseValue"
                    sSQL = sSQL & ", EmployerShare"
                    sSQL = sSQL & ", EmployeeShare"
                    sSQL = sSQL & ", MonthlyPremium"
                    sSQL = sSQL & ", DateEncoded"
                    sSQL = sSQL & ", EncodedBy"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & " " & PK
                    sSQL = sSQL & ", " & toNumber(txtMinSalary.Text)
                    sSQL = sSQL & ", " & toNumber(txtMaxSalary.Text)
                    sSQL = sSQL & ", " & toNumber(txtSalaryBase.Text)
                    sSQL = sSQL & ", " & toNumber(txtER.Text)
                    sSQL = sSQL & ", " & toNumber(txtEE.Text)
                    sSQL = sSQL & ", " & toNumber(txtTotal.Text)
                    sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

                ElseIf State = FormState.EditStateMode Then
                    sSQL = "UPDATE tblMst_PhilHealth SET"
                    sSQL = sSQL & " SalaryMinValue = " & toNumber(txtMinSalary.Text)
                    sSQL = sSQL & " ,SalaryMaxValue = " & toNumber(txtMaxSalary.Text)
                    sSQL = sSQL & " ,SalaryBaseValue = " & toNumber(txtSalaryBase.Text)
                    sSQL = sSQL & " ,EmployerShare = " & toNumber(txtER.Text)
                    sSQL = sSQL & " ,EmployeeShare = " & toNumber(txtEE.Text)
                    sSQL = sSQL & " ,MonthlyPremium = " & toNumber(txtTotal.Text)
                    sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
                    sSQL = sSQL & " WHERE PhilHealthCode = " & toNumber(PK)
                End If

                Call ExecuteSQL(sSQL)

                frmWait.seconds = 1
                frmWait.ShowDialog()

                If State = FormState.AddStateMode Then
                    MsgBox("New record has been successfully saved!", MsgBoxStyle.Information)
                    Call CreateLog(Name, "ADD NEW RECORD", "ID NUMBER:" & toNumber(PK))

                    If MsgBox("Do you want to add another record? (Y/N)", MsgBoxStyle.Question & vbYesNo) = vbYes Then
                        blnUpdateInProgress = False
                        CommandPass("Cancel")

                        CommandPass("New")
                    Else
                        blnUpdateInProgress = False
                        CommandPass("Cancel")
                    End If
                ElseIf State = FormState.EditStateMode Then
                    MsgBox("Information has been successfully updated!", MsgBoxStyle.Information)
                    Call CreateLog(Name, "UPDATE RECORD", "ID NUMBER:" & toNumber(PK))

                    blnUpdateInProgress = False
                    CommandPass("Cancel")
                End If

            Case "Cancel"
                Call ClearAllText(Me, Panel.Controls)
                Call EnableMenu(True, False, False, False, True, True)

                State = FormState.ViewStateMode
                blnUpdateInProgress = False

                Call FillListview(" SELECT " &
                                " s.* " &
                            " FROM " &
                                " tblMst_PhilHealth s " &
                            "ORDER BY s.PhilHealthCode ASC ", lvList)

                LAST_USER.Text = "Last Updated By:"
                LAST_DATE_MODIFIED.Text = "Last Date Modified:"

                txtID.Focus()

            Case "Delete"
                If is_empty(txtID) = True Then Exit Sub

                If MsgBox("Are you sure you want to delete selected record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Call DeleteSQL("tblMst_PhilHealth", "PhilHealthCode", True, "", PK)
                Else
                    Exit Sub
                End If

                MsgBox("Record has been successfully deleted!", MsgBoxStyle.Critical)

                blnUpdateInProgress = False
                CommandPass("Cancel")

            Case "Print"

            Case "Close"
                Me.Dispose()
        End Select
        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub
    Public Sub EnableMenu(iNew As Boolean, iSave As Boolean, iCancel As Boolean, iDelete As Boolean, iPrint As Boolean, iClose As Boolean)
        On Error GoTo ErrHandler
        With Me
            .btnNew.Enabled = iNew
            .btnSave.Enabled = iSave
            .btnCancel.Enabled = iCancel
            .btnDelete.Enabled = iDelete
            .btnPrint.Enabled = iPrint
            .btnClose.Enabled = iClose
        End With
        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub

    Public Sub FillFormView(ByVal p_k As Double)
        On Error GoTo ErrHandler
        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQLCmd As New SqlCommand("SELECT * FROM tblMst_PhilHealth WHERE PhilHealthCode=" & toNumber(p_k), MyConn)

        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQLCmd.ExecuteReader()

        If Dtrdr.Read = False Then
            Exit Sub
        Else
            PK = Dtrdr.Item("PhilHealthCode").ToString

            txtID.Text = Dtrdr.Item("PhilHealthCode").ToString
            txtMinSalary.Text = toMoney(Dtrdr.Item("SalaryMinValue").ToString)
            txtMaxSalary.Text = toMoney(Dtrdr.Item("SalaryMaxValue").ToString)
            txtSalaryBase.Text = toMoney(Dtrdr.Item("SalaryBaseValue").ToString)

            txtER.Text = toMoney(Dtrdr.Item("EmployerShare").ToString)
            txtEE.Text = toMoney(Dtrdr.Item("EmployeeShare").ToString)
            txtTotal.Text = toMoney(Dtrdr.Item("MonthlyPremium").ToString)

            LAST_USER.Text = "Last Updated By : " & IIf(String.IsNullOrEmpty(Dtrdr.Item("ModifiedBy").ToString), Dtrdr.Item("EncodedBy").ToString, Dtrdr.Item("ModifiedBy").ToString)
            LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(Dtrdr.Item("LastDateModified").ToString), Dtrdr.Item("DateEncoded").ToString, Dtrdr.Item("LastDateModified").ToString)
        End If

        Dtrdr.Close()
        SQLCmd.Dispose()
        MyConn.Close()
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
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
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("PhilHealthCode").ToString())
            lv.SubItems.Add(toMoney(Dtrdr.Item("SalaryMinValue").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("SalaryMaxValue").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("SalaryBaseValue").ToString()))

            lv.SubItems.Add(toMoney(Dtrdr.Item("EmployerShare").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("EmployeeShare").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("MonthlyPremium").ToString()))
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        CommandPass("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        CommandPass("Save")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CommandPass("Cancel")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        CommandPass("Delete")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CommandPass("Print")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CommandPass("Close")
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        blnUpdateInProgress = True
        State = FormState.EditStateMode
        Call EnableMenu(False, True, True, True, False, False)
        Call FillFormView(PK)
    End Sub
    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged
        Dim i As Integer

        For i = 0 To lvList.SelectedItems.Count - 1
            PK = toNumber(lvList.SelectedItems(i).Text)
        Next

        Call FillFormView(PK)
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        On Error GoTo ErrFinder
        If txtFind.Text = "" Then
            Call FillListview(" SELECT " &
                                " s.* " &
                            " FROM " &
                                " tblMst_PhilHealth s " &
                            "ORDER BY s.PhilHealthCode ASC ", lvList)
        Else
            Call FillListview(" SELECT " &
                                " s.* " &
                            " FROM " &
                                " tblMst_PhilHealth s " &
                          " WHERE " &
                          " s.SalaryMinValue Like '%" & sqlSafe(txtFind.Text) & "%' " &
                          " OR s.SalaryMaxValue LIKE '%" & sqlSafe(txtFind.Text) & "%'", lvList)
        End If
        Exit Sub
ErrFinder:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub txtFind_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFind.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnFind_Click(btnFind, New EventArgs())
        End If
    End Sub
End Class