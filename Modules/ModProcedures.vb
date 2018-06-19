Option Explicit On
Option Strict Off

Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Module ModProcedures
    Public Sub HLText(ByRef srcText As TextBox)
        If (Not String.IsNullOrEmpty(srcText.Text)) Then
            srcText.SelectionStart = 0
            srcText.SelectionLength = srcText.Text.Length
            srcText.BackColor = Color.LightYellow
        End If
    End Sub
    Public Sub DHLText(ByRef srcText As TextBox)
        srcText.SelectionStart = 0
        srcText.BackColor = Color.White
    End Sub

    Public Function ExportListViewToCSV(ByVal filename As String, ByVal lv As ListView) As Boolean

        Try

            ' Open output file  
            Dim os As New StreamWriter(filename)

            ' Write Headers  

            For i As Integer = 0 To lv.Columns.Count - 1
                ' replace quotes with double quotes if necessary  
                os.Write("""" & lv.Columns(i).Text.Replace("""", """""") & """,")
            Next

            os.WriteLine()

            ' Write records  
            For i As Integer = 0 To lv.Items.Count - 1
                For j As Integer = 0 To lv.Columns.Count - 1
                    os.Write("""" & lv.Items(i).SubItems(j).Text.Replace("""", """""") + """,")
                Next

                os.WriteLine()

            Next

            os.Close()

        Catch ex As Exception
            ' catch any errors  
            Return False
        End Try

        Return True

    End Function
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

    Public Sub object_center(ByVal srcForm As Form, ByVal obj As Object)
        obj.location = New Point(srcForm.Width / 2 - obj.width / 2, srcForm.Height / 2 - obj.height / 2)
        obj.Anchor = AnchorStyles.None
    End Sub
    Public Sub LoadForm(ByRef srcForm As System.Windows.Forms.Form)
        srcForm.MdiParent = MAIN
        srcForm.Show()
        srcForm.WindowState = System.Windows.Forms.FormWindowState.Maximized
        srcForm.Focus()
    End Sub

    Public Sub ClearAllText(ByRef srcForm As Form, Optional ByRef srcContainer As Control.ControlCollection = Nothing)
        Dim ctl As Control
        If srcContainer Is Nothing Then srcContainer = srcForm.Controls

        For Each ctl In srcContainer
            If TypeOf (ctl) Is TextBox  Then
                DirectCast(ctl, TextBox).Clear()
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearAllText(srcForm, ctl.Controls)
                End If
            End If
        Next ctl
        ctl = Nothing
    End Sub

    Public Sub ClearAllCombo(ByRef srcForm As Form, Optional ByRef srcContainer As Control.ControlCollection = Nothing)
        Dim ctl As Control
        If srcContainer Is Nothing Then srcContainer = srcForm.Controls

        For Each ctl In srcContainer
            If TypeOf (ctl) Is ComboBox Then
                DirectCast(ctl, ComboBox).DataSource = Nothing
            Else
                If Not ctl.Controls Is Nothing OrElse ctl.Controls.Count <> 0 Then
                    ClearAllCombo(srcForm, ctl.Controls)
                End If
            End If
        Next ctl
        ctl = Nothing
    End Sub
    Public Sub OnlyNumberDecimal(ByVal key As System.Windows.Forms.KeyPressEventArgs, ByVal txt As String)
        Dim x As Integer
        Dim withDot As Boolean = False
        For x = 0 To txt.Length - 1
            If txt.Substring(x, 1) = "." Then withDot = True
        Next
        Dim intKey As Integer
        intKey = Asc(key.KeyChar)
        If intKey >= 48 And intKey <= 57 Or intKey = 8 Or intKey = 46 Then
            If withDot = True And intKey = 46 Then
                key.KeyChar = ""
            Else
            End If
            Exit Sub
        Else
            key.KeyChar = ""
        End If
    End Sub

    Public Sub CreateLog(m_OccurredIn As String, mActionTaken As String, m_Remarks As String)
        Dim sSQL As String
        Try
            sSQL = "INSERT INTO tblMst_Logs"
            sSQL = sSQL & "(AuditTrailNo"
            sSQL = sSQL & ", OccurredIn"
            sSQL = sSQL & ", ActionTaken"
            sSQL = sSQL & ", LoginName"
            sSQL = sSQL & ", DateOccurred"
            sSQL = sSQL & ", Remarks"
            sSQL = sSQL & ") VALUES ("
            sSQL = sSQL & " " & GenerateNewIDNumber("tblMst_Logs", "AuditTrailNo") & ""
            sSQL = sSQL & ", '" & sqlSafe(m_OccurredIn) & "'"
            sSQL = sSQL & ", '" & sqlSafe(mActionTaken) & "'"
            sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "'"
            sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
            sSQL = sSQL & ", '" & sqlSafe(m_Remarks) & "')"

            ExecuteSQL(sSQL)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub FillComboBox(ByVal srcCombo As ComboBox, ByVal srcSQL As String, ByVal srcMember As String, ByVal srcValue As String)
        Dim dt As New DataTable
        Try
            dt = GetDataSet(srcSQL)

            With srcCombo
                .DataSource = dt
                .DisplayMember = srcMember
                .ValueMember = srcValue
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Public Sub FillCombo(ByVal cboCombo As ComboBox, ByVal sSQL As String, ByVal strTable As String, ByVal strDisplayMember As String, ByVal strValueMember As String)
        'FillCombo(cboCountry, "SELECT * FROM Countries", "Countries", "Country", "CountryID")
        Try
            If MyConn.State = ConnectionState.Open Then MyConn.Close()
            MyConn.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter(sSQL, MyConn)
            Dim dt As New DataSet

            da.Fill(dt, strTable)

            cboCombo.DataSource = dt.Tables(strTable).DefaultView
            cboCombo.DisplayMember = strDisplayMember
            cboCombo.ValueMember = strValueMember

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MyConn.Close()
        End Try
    End Sub
End Module
