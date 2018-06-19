Imports System.Windows.Forms

Public Class clsMessageBox
    Inherits clsVariables

    Public Sub New()
    End Sub

    Public Function MessageBox_ComfirmDelete(ByVal bSelectedListMode As Boolean) As DialogResult
        Dim dReturn As DialogResult

        If bSelectedListMode Then
            dReturn = MessageBox.Show("Are you sure you want to delete the selected record(s)? WARNING: You cannot undo this operation.", "Confirm Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        Else
            dReturn = MessageBox.Show("Are you sure you want to delete this record? WARNING: You cannot undo this operation.", "Confirm Record Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        End If
        Return dReturn
    End Function

    Public Function MessageBox_ConfirmFormExit(ByVal bModified As Boolean, ByVal frm As Form) As Boolean
        Dim bReturn As Boolean = False
        If bModified AndAlso (MessageBox.Show("Form data has been modified but not saved. Continue anyway?", "Confirm Form Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            bReturn = True
        End If
        Return bReturn
    End Function

    Public Function MessageBox_ConfirmFormExit(ByVal bModified As Boolean) As Boolean
        Dim bReturn As Boolean = False
        If bModified AndAlso (MessageBox.Show("Form data has been modified but not saved. Continue anyway?", "Confirm Form Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            bReturn = True
        End If
        Return bReturn
    End Function

    Public Function MessageBox_ConfirmFormReset(ByVal bModified As Boolean) As DialogResult
        Dim dReturn As DialogResult = DialogResult.No
        If bModified Then
            dReturn = MessageBox.Show("Are you sure you want to clear the form?", "Confirm Form Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
        Return dReturn
    End Function

    Public Function MessageBox_ConfirmSaveUpdate(ByVal State As clsTypeVariables.FORM_STATE, ByVal ctrl As Control) As DialogResult
        Dim dReturn As DialogResult

        If State = clsTypeVariables.FORM_STATE.adStateAddMode Then
            dReturn = MessageBox.Show("Are you sure you want to save this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Else
            dReturn = MessageBox.Show("Are you sure you want to save the changes on this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
        Return dReturn
    End Function

    Public Function MessageBox_ConfirmSaveUpdate(ByVal State As clsTypeVariables.FORM_STATE) As DialogResult
        Dim dReturn As DialogResult

        If State = clsTypeVariables.FORM_STATE.adStateAddMode Then
            dReturn = MessageBox.Show("Are you sure you want to save this record?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        Else
            dReturn = MessageBox.Show("Are you sure you want to save the changes on this record?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        End If
        Return dReturn
    End Function

    Public Sub MessageBox_DeleteSuccessful(ByVal frm As Form, ByVal bListMode As Boolean)
        If bListMode Then
            MessageBox.Show("Selected record(s) has been successfully deleted", "Records Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return
        End If
        MessageBox.Show("Record has been successfully deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        frm.Close()
    End Sub

    Public Sub MessageBox_DeleteSuccessful(ByVal bListMode As Boolean)
        If bListMode Then
            MessageBox.Show("Selected record(s) has been successfully deleted", "Records Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return
        End If
        MessageBox.Show("Record has been successfully deleted", "Record Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

    Public Sub MessageBox_SaveUpdateSuccessful(ByVal State As clsTypeVariables.FORM_STATE, ByVal ctrlFocus As Control)
        If State = clsTypeVariables.FORM_STATE.adStateAddMode Then
            MessageBox.Show("New record has been successfully added.", "Add Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        Else
            MessageBox.Show("Changes to record has been successfully saved.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
        End If
        ctrlFocus.Focus()
    End Sub

    Public Sub MessageBox_SaveUpdateSuccessful(ByVal State As clsTypeVariables.FORM_STATE)
        If State = clsTypeVariables.FORM_STATE.adStateAddMode Then
            MessageBox.Show("New record has been successfully added.", "Add Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            Return
        End If
        MessageBox.Show("Changes to record has been successfully saved.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
    End Sub

End Class ' class clsMessageBox