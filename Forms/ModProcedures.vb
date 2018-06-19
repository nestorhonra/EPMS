Module ModProcedures
    Public Sub OpenRecordset(srcCN As ADODB.Connection, srcRS As ADODB.Recordset)
        If srcCN.State = False Then srcCN.Open()
        If srcRS.State <> 0 Then srcRS.Close()
        srcRS.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        srcRS.ActiveConnection = srcCN
        srcRS.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        srcRS.LockType = ADODB.LockTypeEnum.adLockReadOnly
    End Sub
    Public Sub CenterForm(ByVal srcForm As Form)
        Dim sw As Integer = My.Computer.Screen.Bounds.Width / 2
        Dim sh As Integer = My.Computer.Screen.Bounds.Height / 2
        Dim fw As Integer = srcForm.Size.Width / 2
        Dim fh As Integer = srcForm.Size.Height / 2
        Dim lw As Integer = sw - fw
        Dim lh As Integer = sh - fh
        Dim pnt As New Point(lw, lh)
        srcForm.Location = pnt
    End Sub

    Public Sub LoadForm(ByRef srcForm As System.Windows.Forms.Form)
        srcForm.MdiParent = MAIN
        srcForm.Show()
        srcForm.WindowState = System.Windows.Forms.FormWindowState.Maximized
        srcForm.Focus()
    End Sub

    Public Sub ClearAllText(ByRef sForm As System.Windows.Forms.Form)
        Dim Control As System.Windows.Forms.Control
        For Each Control In sForm.Controls
            If (TypeOf Control Is System.Windows.Forms.TextBox) Then Control = Nothing
        Next Control
        Control = Nothing

    End Sub


End Module
