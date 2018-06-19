Public Class MAIN

    Private Sub MAIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            m_CURRENT_DATE.Text = FormatDateTime(Today, DateFormat.LongDate)
            Call LoadForm(frmShortcut)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub MAIN_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        clsConnections.CN = Nothing
        MyConn = Nothing
        Me.Dispose()
    End Sub

    Private Sub MAIN_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim Cancel As Boolean = e.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
        Dim Reply As Short

        Reply = MsgBox("This will terminate the application.Do you want to proceed?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo)

        If Reply = MsgBoxResult.No Then
            Cancel = 1
        Else
            Me.Dispose()
            Application.ExitThread()

            END_APPLICATION = True
            clsConnections.CN = Nothing

            MyConn.Close()
            MyConn = Nothing
        End If

        e.Cancel = Cancel
    End Sub

    Private Sub DepartmentToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DepartmentToolStripMenuItem.Click

        frmDepartment.ShowDialog()
    End Sub

    Private Sub btnEmployee_Click(sender As Object, e As EventArgs)

        frmEmployeeFile.ShowDialog()
    End Sub

    Private Sub CostCenterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CostCenterToolStripMenuItem.Click

        frmDivision.ShowDialog()
    End Sub

    Private Sub SSSTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SSSTableToolStripMenuItem.Click

        frmSSSTable.ShowDialog()
    End Sub

    Private Sub PHICToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PHICToolStripMenuItem.Click

        frmPHICTable.ShowDialog()
    End Sub

    Private Sub btnCalc_Click(sender As Object, e As EventArgs) Handles btnCalc.Click
        Shell("calc.exe", AppWinStyle.NormalFocus)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Shell("explorer.exe", AppWinStyle.NormalFocus)
    End Sub

    Private Sub btnShortcut_Click(sender As Object, e As EventArgs) Handles btnShortcut.Click
        Call LoadForm(frmShortcut)
    End Sub

    Private Sub EmployeeMasterfileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmployeeMasterfileToolStripMenuItem.Click
        frmEmployeeFile.ShowDialog()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim Reply As Short

        Reply = MsgBox("This will terminate the application.Do you want to proceed?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo)

        If Reply = MsgBoxResult.No Then
            Exit Sub
        Else
            Me.Dispose()
            Application.ExitThread()

            END_APPLICATION = True
            clsConnections.CN = Nothing

            MyConn.Close()
            MyConn = Nothing
        End If
    End Sub

    Private Sub LeaveTypeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeaveTypeToolStripMenuItem.Click
        frmLeaveType.ShowDialog()
    End Sub
End Class
