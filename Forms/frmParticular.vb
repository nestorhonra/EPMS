Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient
Friend Class frmParticular

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

                txtParticularNo.Text = "(Auto)"

                CheckBox1.Checked = False
                CheckBox2.Checked = False
                CheckBox3.Checked = False
                CheckBox4.Checked = False
                CheckBox5.Checked = False
                CheckBox6.Checked = True

                txtShortName.Focus()

            Case "Save"
                If is_empty(txtDescription) = True Then Exit Sub
                If is_empty(txtShortName) = True Then Exit Sub

                If State = FormState.AddStateMode Then
                    PK = GenerateNewIDNumber("tblMst_Particular", "ParticularCode")

                    sSQL = "INSERT INTO tblMst_Particular"
                    sSQL = sSQL & "(ParticularCode"
                    sSQL = sSQL & ", Description"
                    sSQL = sSQL & ", ShortDesc"
                    sSQL = sSQL & ", IsIncome"
                    sSQL = sSQL & ", HasUnit"
                    sSQL = sSQL & ", HasERShare"
                    sSQL = sSQL & ", IsGovtDeduction"
                    sSQL = sSQL & ", Multiplier"
                    sSQL = sSQL & ", IsTaxable"
                    sSQL = sSQL & ", IsActive"
                    sSQL = sSQL & ", DateEncoded"
                    sSQL = sSQL & ", EncodedBy"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & " " & PK
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtShortName.Text)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox1.Checked)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox2.Checked)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox3.Checked)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox4.Checked)) & "'"
                    sSQL = sSQL & ", " & toNumber(txtMultiplier.Text)
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox5.Checked)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(changeYNValue(CheckBox6.Checked)) & "'"
                    sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

                ElseIf State = FormState.EditStateMode Then
                    sSQL = "UPDATE tblMst_Particular SET"
                    sSQL = sSQL & " Description = '" & sqlSafe(UCase(txtDescription.Text)) & "'"
                    sSQL = sSQL & " ,ShortDesc = '" & sqlSafe(UCase(txtShortName.Text)) & "'"
                    sSQL = sSQL & " ,IsIncome = '" & sqlSafe(changeYNValue(CheckBox1.Checked)) & "'"
                    sSQL = sSQL & " ,HasUnit = '" & sqlSafe(changeYNValue(CheckBox2.Checked)) & "'"
                    sSQL = sSQL & " ,HasERShare = '" & sqlSafe(changeYNValue(CheckBox3.Checked)) & "'"
                    sSQL = sSQL & " ,IsGovtDeduction = '" & sqlSafe(changeYNValue(CheckBox4.Checked)) & "'"
                    sSQL = sSQL & " ,Multiplier = " & toNumber(txtMultiplier.Text)
                    sSQL = sSQL & " ,IsTaxable = '" & sqlSafe(changeYNValue(CheckBox5.Checked)) & "'"
                    sSQL = sSQL & " ,IsActive = '" & sqlSafe(changeYNValue(CheckBox6.Checked)) & "'"
                    sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
                    sSQL = sSQL & " WHERE ParticularCode = " & toNumber(PK)
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

                CheckBox1.Checked = False
                CheckBox2.Checked = False
                CheckBox3.Checked = False
                CheckBox4.Checked = False
                CheckBox5.Checked = False
                CheckBox6.Checked = True

                txtParticularNo.Focus()

            Case "Delete"
                If is_empty(txtParticularNo) = True Then Exit Sub
                If is_empty(txtDescription) = True Then Exit Sub

                If MsgBox("Are you sure you want to delete selected record? <Y/N>", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

                    Call DeleteSQL("tblMst_Particular", "ParticularCode", True, "", PK)
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
    Private Sub frmParticular_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrLoad
        CenterForm(Me)

        State = FormState.ViewStateMode
        Call EnableMenu(True, False, False, False, True, True)

        Exit Sub
ErrLoad: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub frmParticular_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub frmParticular_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
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
    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If State = FormState.AddStateMode Then Exit Sub
        frmParticularQuery.ShowDialog()
    End Sub

    Public Sub FillFormView(ByVal p_k As Double)
        On Error GoTo ErrHandler
        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQLCmd As New SqlCommand("SELECT * FROM tblMst_Particular WHERE ParticularCode=" & toNumber(p_k), MyConn)

        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQLCmd.ExecuteReader()

        If Dtrdr.Read = False Then
            Exit Sub
        Else
            PK = Dtrdr.Item("ParticularCode").ToString
            blnUpdateInProgress = True

            Call EnableMenu(False, True, True, True, False, False)

            txtParticularNo.Text = Dtrdr.Item("ParticularCode").ToString
            txtDescription.Text = Dtrdr.Item("Description").ToString
            txtShortName.Text = Dtrdr.Item("ShortDesc").ToString
            CheckBox1.Checked = changeYNValue(Dtrdr.Item("IsIncome").ToString)
            CheckBox2.Checked = changeYNValue(Dtrdr.Item("HasUnit").ToString)
            CheckBox3.Checked = changeYNValue(Dtrdr.Item("HasERShare").ToString)
            CheckBox4.Checked = changeYNValue(Dtrdr.Item("IsGovtDeduction").ToString)
            txtMultiplier.Text = toMoney(Dtrdr.Item("Multiplier").ToString)
            CheckBox5.Checked = changeYNValue(Dtrdr.Item("IsTaxable").ToString)
            CheckBox6.Checked = changeYNValue(Dtrdr.Item("IsActive").ToString)

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
End Class