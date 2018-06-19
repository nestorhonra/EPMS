Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Friend Class frmLeaveType
    Public State As FormState
    Public PK As Double

    Dim blnUpdateInProgress As Boolean
    Dim sSQL As String
    Private Sub frmLeaveType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        State = FormState.ViewStateMode
        Call EnableMenu(True, False, False, False, True, True)

        Call FillListview("SELECT * FROM tblMst_LeaveType ORDER BY LeaveTypeCode ASC ", lvList)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
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
                txtShortDesc.Focus()

            Case "Save"
                If is_empty(txtDescription) = True Then Exit Sub
                If is_empty(txtID) = True Then Exit Sub

                If State = FormState.AddStateMode Then
                    PK = GenerateNewIDNumber("tblMst_LeaveType", "LeaveTypeCode")

                    sSQL = "INSERT INTO tblMst_LeaveType"
                    sSQL = sSQL & "(LeaveTypeCode"
                    sSQL = sSQL & ", Description"
                    sSQL = sSQL & ", ShortDesc"
                    sSQL = sSQL & ", DateEncoded"
                    sSQL = sSQL & ", EncodedBy"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & " " & PK
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtShortDesc.Text)) & "'"
                    sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

                ElseIf State = FormState.EditStateMode Then
                    sSQL = "UPDATE tblMst_LeaveType SET"
                    sSQL = sSQL & " Description = '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & " ,ShortDesc = '" & sqlSafe(UCase(txtShortDesc.Text)) & "'"
                    sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
                    sSQL = sSQL & " WHERE LeaveTypeCode = " & toNumber(PK)
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

                Call FillListview("SELECT * FROM tblMst_LeaveType ORDER BY LeaveTypeCode ASC ", lvList)
                LAST_USER.Text = "Last Updated By:"
                LAST_DATE_MODIFIED.Text = "Last Date Modified:"

                txtID.Focus()

            Case "Delete"
                If is_empty(txtID) = True Then Exit Sub
                If is_empty(txtDescription) = True Then Exit Sub

                If MsgBox("Are you sure you want to delete selected record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Call DeleteSQL("tblMst_LeaveType", "LeaveTypeCode", True, "", PK)
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
        Dim SQLCmd As New SqlCommand("SELECT * FROM tblMst_LeaveType WHERE LeaveTypeCode=" & toNumber(p_k), MyConn)

        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQLCmd.ExecuteReader()

        If Dtrdr.Read = False Then
            Exit Sub
        Else
            PK = Dtrdr.Item("LeaveTypeCode").ToString

            txtID.Text = Dtrdr.Item("LeaveTypeCode").ToString
            txtShortDesc.Text = Dtrdr.Item("ShortDesc").ToString
            txtDescription.Text = Dtrdr.Item("Description").ToString

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
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("LeaveTypeCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("ShortDesc").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
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
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        On Error GoTo ErrFinder
        If txtFind.Text = "" Then
            Call FillListview("SELECT * FROM tblMst_LeaveType ORDER BY LeaveTypeCode ASC ", lvList)
        Else
            Call FillListview("SELECT * FROM tblMst_LeaveType WHERE " &
                          " Description LIKE '%" & sqlSafe(txtFind.Text) & "%' " &
                          " OR ShortDesc LIKE '%" & sqlSafe(txtFind.Text) & "%'", lvList)
        End If
        Exit Sub
ErrFinder:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class