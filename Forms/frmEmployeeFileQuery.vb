Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Public Class frmEmployeeFileQuery
    Dim PK As Double
    Private Sub frmEmployeeFileQuery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrQuery
        CenterForm(Me)

        PK = 0

        Call ClearAllText(Me)
        Call FillListview(" SELECT " &
                " ea.* " &
            " FROM " &
                " (" &
                    " SELECT DISTINCT " &
                        " e.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " e.PresentAddress AS [Address], " &
                        " e.BirthDate, " &
                        " DATEDIFF(HOUR, e.BirthDate, GETDATE())/ 8766 AS [Age], " &
                        " e.DateHired, " &
                        " DATEDIFF(HOUR, e.DateHired, GETDATE())/ 8766 AS [YOS], " &
                        " e.SeparationDate, " &
                        " e.CompanyCode, " &
                        " cc.CompanyName AS [Company], " &
                        " e.EmploymentTypeCode, " &
                        " et.Description AS [Employment Type], " &
                        " e.EmploymentStatusCode, " &
                        " es.Description AS [Status], " &
                        " jt.Description AS [Job Title], " &
                        " e.DepartmentCode, " &
                        " d.Description AS [Department], " &
                        " e.UnitCode, " &
                        " un.Description AS [Unit], " &
                        " e.DivisionCode, " &
                        " u.Description AS [Division], " &
                        " e.GenderCode, " &
                        " g.Description AS [Gender], " &
                        " e.CivilStatusCode, " &
                        " cs.CivilStatus AS [Civil Status], " &
                        " e.ContactNo AS [Phone No], " &
                        " e.MobileNo AS [Mobile No], " &
                        " e.SSSID AS [SSS No], " &
                        " e.TINID AS [TIN], " &
                        " e.HDMF AS [Pagibig Number], " &
                        " e.PhilHealth AS [PhilHealth No], " &
                        " e.BankAccountNo AS [Account Number], " &
                        " e.Religion, " &
                        " e.FaxNo, " &
                        " e.CityCode" &
                    " FROM " &
                        " tblMst_Employee e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Company cc(nolock)ON e.CompanyCode = cc.CompanyCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentType et(nolock)ON e.EmploymentTypeCode = et.EmploymentTypeCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentStatus es(nolock)ON e.EmploymentStatusCode = es.EmploymentStatusCode " &
                    " LEFT OUTER JOIN tblMst_JobTitle jt(nolock)ON e.JobTitleCode = jt.JobTitleCode " &
                    " LEFT OUTER JOIN tblMst_Department d(nolock)ON e.DepartmentCode = d.DepartmentCode " &
                    " LEFT OUTER JOIN tblMst_Division u(nolock)ON e.DivisionCode = u.DivisionCode " &
                    " LEFT OUTER JOIN tblMst_Unit un(nolock)ON e.UnitCode = un.UnitCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)ON e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                    " LEFT OUTER JOIN tblMst_City ct(nolock)ON e.CityCode = ct.CityCode " &
                " )ea " &
                " ORDER BY ea.[Employee Name] ASC ", lvList)

        Exit Sub
ErrQuery: MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmEmployeeFileQuery_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvList.Height = (Me.Height - p_FOOTER.Height) - (lvList.Top + 40)
            lvList.Width = Me.Width - (lvList.Left + 20)
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
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Employee No").ToString(), 8)
            lv.SubItems.Add(Dtrdr.Item("Employee Name").ToString())
            lv.SubItems.Add(Dtrdr.Item("Address").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("BirthDate").ToString(), DateFormat.ShortDate))

            lv.SubItems.Add(Dtrdr.Item("Job Title").ToString())
            lv.SubItems.Add(Dtrdr.Item("Gender").ToString())
            lv.SubItems.Add(Dtrdr.Item("Civil Status").ToString())

            lv.SubItems.Add(Dtrdr.Item("Phone No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Mobile No").ToString())

            lv.SubItems.Add(Dtrdr.Item("SSS No").ToString())
            lv.SubItems.Add(Dtrdr.Item("PhilHealth No").ToString())
            lv.SubItems.Add(Dtrdr.Item("Pagibig Number").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmEmployeeFileQuery_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub frmEmployeeFileQuery_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub lvList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvList.SelectedIndexChanged
        Dim i As Integer

        If lvList.Items.Count = 0 Then Exit Sub

        For i = 0 To lvList.SelectedItems.Count - 1
            PK = toNumber(lvList.SelectedItems(i).Text)
        Next
    End Sub

    Private Sub lvList_DoubleClick(sender As Object, e As EventArgs) Handles lvList.DoubleClick
        On Error GoTo ErrHandler

        If PK = 0 Then Exit Sub

        With frmEmployeeFile
            .State = FormState.EditStateMode
            .FillFormViewUsingDataset(PK)
        End With

        Me.Dispose()
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Call FillListview(" SELECT " &
                " ea.* " &
            " FROM " &
                " (" &
                    " SELECT DISTINCT " &
                        " e.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " e.PresentAddress AS [Address], " &
                        " e.BirthDate, " &
                        " DATEDIFF(HOUR, e.BirthDate, GETDATE())/ 8766 AS [Age], " &
                        " e.DateHired, " &
                        " DATEDIFF(HOUR, e.DateHired, GETDATE())/ 8766 AS [YOS], " &
                        " e.SeparationDate, " &
                        " e.CompanyCode, " &
                        " cc.CompanyName AS [Company], " &
                        " e.EmploymentTypeCode, " &
                        " et.Description AS [Employment Type], " &
                        " e.EmploymentStatusCode, " &
                        " es.Description AS [Status], " &
                        " jt.Description AS [Job Title], " &
                        " e.DepartmentCode, " &
                        " d.Description AS [Department], " &
                        " e.UnitCode, " &
                        " un.Description AS [Unit], " &
                        " e.DivisionCode, " &
                        " u.Description AS [Division], " &
                        " e.GenderCode, " &
                        " g.Description AS [Gender], " &
                        " e.CivilStatusCode, " &
                        " cs.CivilStatus AS [Civil Status], " &
                        " e.ContactNo AS [Phone No], " &
                        " e.MobileNo AS [Mobile No], " &
                        " e.SSSID AS [SSS No], " &
                        " e.TINID AS [TIN], " &
                        " e.HDMF AS [Pagibig Number], " &
                        " e.PhilHealth AS [PhilHealth No], " &
                        " e.BankAccountNo AS [Account Number], " &
                        " e.Religion, " &
                        " e.FaxNo, " &
                        " e.CityCode" &
                    " FROM " &
                        " tblMst_Employee e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Company cc(nolock)ON e.CompanyCode = cc.CompanyCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentType et(nolock)ON e.EmploymentTypeCode = et.EmploymentTypeCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentStatus es(nolock)ON e.EmploymentStatusCode = es.EmploymentStatusCode " &
                    " LEFT OUTER JOIN tblMst_JobTitle jt(nolock)ON e.JobTitleCode = jt.JobTitleCode " &
                    " LEFT OUTER JOIN tblMst_Department d(nolock)ON e.DepartmentCode = d.DepartmentCode " &
                    " LEFT OUTER JOIN tblMst_Division u(nolock)ON e.DivisionCode = u.DivisionCode " &
                    " LEFT OUTER JOIN tblMst_Unit un(nolock)ON e.UnitCode = un.UnitCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)ON e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                    " LEFT OUTER JOIN tblMst_City ct(nolock)ON e.CityCode = ct.CityCode " &
                " )ea " &
                " ORDER BY ea.[Employee Name] ASC ", lvList)
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Call FillListview(" SELECT " &
                " ea.* " &
            " FROM " &
                " (" &
                    " SELECT DISTINCT " &
                        " e.EmployeeCode AS [Employee No], " &
                        " e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name], " &
                        " e.PresentAddress AS [Address], " &
                        " e.BirthDate, " &
                        " DATEDIFF(HOUR, e.BirthDate, GETDATE())/ 8766 AS [Age], " &
                        " e.DateHired, " &
                        " DATEDIFF(HOUR, e.DateHired, GETDATE())/ 8766 AS [YOS], " &
                        " e.SeparationDate, " &
                        " e.CompanyCode, " &
                        " cc.CompanyName AS [Company], " &
                        " e.EmploymentTypeCode, " &
                        " et.Description AS [Employment Type], " &
                        " e.EmploymentStatusCode, " &
                        " es.Description AS [Status], " &
                        " jt.Description AS [Job Title], " &
                        " e.DepartmentCode, " &
                        " d.Description AS [Department], " &
                        " e.UnitCode, " &
                        " un.Description AS [Unit], " &
                        " e.DivisionCode, " &
                        " u.Description AS [Division], " &
                        " e.GenderCode, " &
                        " g.Description AS [Gender], " &
                        " e.CivilStatusCode, " &
                        " cs.CivilStatus AS [Civil Status], " &
                        " e.ContactNo AS [Phone No], " &
                        " e.MobileNo AS [Mobile No], " &
                        " e.SSSID AS [SSS No], " &
                        " e.TINID AS [TIN], " &
                        " e.HDMF AS [Pagibig Number], " &
                        " e.PhilHealth AS [PhilHealth No], " &
                        " e.BankAccountNo AS [Account Number], " &
                        " e.Religion, " &
                        " e.FaxNo, " &
                        " e.CityCode" &
                    " FROM " &
                        " tblMst_Employee e(nolock) " &
                    " LEFT OUTER JOIN tblMst_Company cc(nolock)ON e.CompanyCode = cc.CompanyCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentType et(nolock)ON e.EmploymentTypeCode = et.EmploymentTypeCode " &
                    " LEFT OUTER JOIN tblMst_EmploymentStatus es(nolock)ON e.EmploymentStatusCode = es.EmploymentStatusCode " &
                    " LEFT OUTER JOIN tblMst_JobTitle jt(nolock)ON e.JobTitleCode = jt.JobTitleCode " &
                    " LEFT OUTER JOIN tblMst_Department d(nolock)ON e.DepartmentCode = d.DepartmentCode " &
                    " LEFT OUTER JOIN tblMst_Division u(nolock)ON e.DivisionCode = u.DivisionCode " &
                    " LEFT OUTER JOIN tblMst_Unit un(nolock)ON e.UnitCode = un.UnitCode " &
                    " LEFT OUTER JOIN tblMst_Gender g(nolock)ON e.GenderCode = g.GenderCode " &
                    " LEFT OUTER JOIN tblMst_CivilStatus cs(nolock)ON e.CivilStatusCode = cs.CivilStatusCode " &
                    " LEFT OUTER JOIN tblMst_City ct(nolock)ON e.CityCode = ct.CityCode " &
                " )ea " &
                    " WHERE " &
                    "[" & cboFields.Text & "]" &
                    " Like '%" & sqlSafe(txtFind.Text) & "%'", lvList)
    End Sub

    Private Sub txtFind_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFind.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnFind_Click(btnFind, New EventArgs())
        ElseIf Asc(e.KeyChar) = 116 Then
            btnRefresh_Click(btnRefresh, New EventArgs())
        End If
    End Sub

    Private Sub frmEmployeeFileQuery_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        On Error Resume Next
        Dim i As Integer

        cboFields.Items.Clear()
        For i = 0 To lvList.Columns.Count - 1
            cboFields.Items.Add(lvList.Columns(i).Text)
        Next

        cboFields.SelectedIndex = 1
        txtFind.Focus()
    End Sub
End Class