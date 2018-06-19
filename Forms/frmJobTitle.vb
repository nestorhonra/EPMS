Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Friend Class frmChargeGroup
    Public State As FormState
    Public PK As Double

    Dim blnUpdateInProgress As Boolean
    Dim sSQL As String

    Public Sub CommandPass(ByVal srcPerformWhat As String)
        On Error GoTo ErrHandler
        Select Case srcPerformWhat
            Case "New"
                State = FormState.AddStateMode
                blnUpdateInProgress = True

                Call ClearAllText(Me)
                Call EnableMenu(False, True, True, False, False, False)

                txtCode.Text = "(Auto)"
                chkActive.Checked = True
                txtDescription.Focus()

            Case "Save"
                If is_empty(txtDescription) = True Then Exit Sub
                If is_empty(txtRemarks) = True Then Exit Sub

                If State = FormState.AddStateMode Then
                    PK = GenerateNewIDNumber("tblMst_ChargeGroup", "ChargeGroupCode")

                    sSQL = "INSERT INTO tblMst_ChargeGroup"
                    sSQL = sSQL & "(ChargeGroupCode"
                    sSQL = sSQL & ", Description"
                    sSQL = sSQL & ", Remarks"
                    sSQL = sSQL & ", IsActive"
                    sSQL = sSQL & ", DateEncoded"
                    sSQL = sSQL & ", EncodedBy"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & " " & PK
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtRemarks.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(chkActive.Checked)) & "'"
                    sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

                ElseIf State = FormState.EditStateMode Then
                    sSQL = "UPDATE tblMst_ChargeGroup SET"
                    sSQL = sSQL & " Description = '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & " ,Remarks = '" & sqlSafe(txtRemarks.Text) & "'"
                    sSQL = sSQL & " ,IsActive = '" & sqlSafe(changeYNValue(chkActive.Checked)) & "'"
                    sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
                    sSQL = sSQL & " WHERE ChargeGroupCode = " & toNumber(PK)
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
                Call ClearAllText(Me)
                Call EnableMenu(True, False, False, False, True, True)

                State = FormState.ViewStateMode
                blnUpdateInProgress = False

                txtCode.Focus()
            Case "Delete"
                If is_empty(txtCode) = True Then Exit Sub
                If is_empty(txtDescription) = True Then Exit Sub

                If MsgBox("Are you sure you want to delete selected record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Call DeleteSQL("tblMst_ChargeGroup", "ChargeGroupCode", True, "", PK)
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
    Private Sub frmChargeGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        State = FormState.ViewStateMode
        Call EnableMenu(True, False, False, False, True, True)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub
    Private Sub frmChargeGroup_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        On Error Resume Next
        txtCode.Focus()
    End Sub
    Private Sub frmChargeGroup_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CommandPass("Close")
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CommandPass("Print")
    End Sub

    Private Sub frmChargeGroup_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If State = FormState.AddStateMode Then Exit Sub
        frmChargeGroupQuery.ShowDialog()
    End Sub

    Public Sub FillFormView(ByVal p_k As Double)
        On Error GoTo ErrHandler
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Dim SQLCmd As New SqlCommand("SELECT * FROM tblMst_ChargeGroup WHERE ChargeGroupCode=" & toNumber(p_k), Conn)

        Conn.Open()
        Dim Dtrdr As SqlDataReader = SQLCmd.ExecuteReader()

        If Dtrdr.Read = False Then
            Exit Sub
        Else
            PK = Dtrdr.Item("ChargeGroupCode").ToString
            blnUpdateInProgress = True

            Call EnableMenu(False, True, True, True, False, False)

            txtCode.Text = Dtrdr.Item("ChargeGroupCode").ToString
            txtDescription.Text = Dtrdr.Item("Description").ToString
            txtRemarks.Text = Dtrdr.Item("Remarks").ToString
            chkActive.Checked = changeYNValue(Dtrdr.Item("IsActive").ToString)

            LAST_USER.Text = "Last Updated By : " & IIf(String.IsNullOrEmpty(Dtrdr.Item("ModifiedBy").ToString), Dtrdr.Item("EncodedBy").ToString, Dtrdr.Item("ModifiedBy").ToString)
            LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(Dtrdr.Item("LastDateModified").ToString), Dtrdr.Item("DateEncoded").ToString, Dtrdr.Item("LastDateModified").ToString)
        End If

        Dtrdr.Close()
        SQLCmd.Dispose()
        Conn.Close()
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
End Class