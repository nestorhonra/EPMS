Option Strict Off
Option Explicit On

Imports System.IO
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Friend Class frmParticularQuery
    Dim PK As Double

    Private Sub frmParticularQuery_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        On Error Resume Next
        Dim i As Integer

        cboFields.Items.Clear()
        For i = 0 To lvList.Columns.Count - 1
            cboFields.Items.Add(lvList.Columns(i).Text)
        Next

        cboFields.SelectedIndex = 1
        txtFind.Focus()
    End Sub

    Private Sub frmParticularQuery_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub frmParticularQuery_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrQuery
        CenterForm(Me)

        PK = 0

        Call ClearAllText(Me)
        Call FillListview(" SELECT " &
                            " p.ParticularCode As [Particular ID], " &
                            " p.ShortDesc AS [Short Desc], " &
                            " p.Description, " &
                            " p.IsIncome AS [Income], " &
                            " p.HasUnit As [Has Unit], " &
                            " p.HasERShare AS [Has ER Share], " &
                            " p.IsGovtDeduction As [Mandatory], " &
                            " p.Multiplier, " &
                            " p.IsTaxable As [Taxable], " &
                            " p.IsActive AS [Active] " &
                            " FROM " &
                            " 	tblMst_Particular p(nolock) " &
                            " ORDER BY " &
                                " p.ParticularCode ASC", lvList)

        Exit Sub
ErrQuery: MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub frmParticularQuery_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState <> FormWindowState.Minimized Then
            If Me.Width < 550 Then Me.Width = 550
            If Me.Height < 250 Then Me.Height = 250

            lvList.Height = (Me.Height - p_FOOTER.Height) - (lvList.Top + 40)
            lvList.Width = Me.Width - (lvList.Left + 20)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        PK = 0

        Call ClearAllText(Me)
        Call FillListview(" SELECT " &
                            " p.ParticularCode As [Particular ID], " &
                            " p.ShortDesc AS [Short Desc], " &
                            " p.Description, " &
                            " p.IsIncome AS [Income], " &
                            " p.HasUnit As [Has Unit], " &
                            " p.HasERShare AS [Has ER Share], " &
                            " p.isGovtDeduction As [Mandatory], " &
                            " p.Multiplier, " &
                            " p.IsTaxable As [Taxable], " &
                            " p.IsActive AS [Active] " &
                            " FROM " &
                            " 	tblMst_Particular p(nolock) " &
                            " ORDER BY " &
                                " p.ParticularCode ASC", lvList)
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
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Particular ID").ToString(), 3)
            lv.SubItems.Add(Dtrdr.Item("Short Desc").ToString())
            lv.SubItems.Add(Dtrdr.Item("Description").ToString())
            lv.SubItems.Add(Dtrdr.Item("Income").ToString())
            lv.SubItems.Add(Dtrdr.Item("Has Unit").ToString())
            lv.SubItems.Add(Dtrdr.Item("Has ER Share").ToString())
            lv.SubItems.Add(Dtrdr.Item("Mandatory").ToString())
            lv.SubItems.Add(toMoney(Dtrdr.Item("Multiplier").ToString()))
            lv.SubItems.Add(Dtrdr.Item("Taxable").ToString())
            lv.SubItems.Add(Dtrdr.Item("Active").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
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

        With frmParticular
            .State = FormState.EditStateMode
            .FillFormView(PK)
        End With

        Me.Dispose()
        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If is_empty(txtFind) = True Then Exit Sub
        Call FillListview(" SELECT " &
                            " p.ParticularCode As [Particular ID], " &
                            " p.ShortDesc AS [Short Desc], " &
                            " p.Description, " &
                            " p.IsIncome AS [Income], " &
                            " p.HasUnit As [Has Unit], " &
                            " p.HasERShare AS [Has ER Share], " &
                            " p.isGovtDeduction As [Mandatory], " &
                            " p.Multiplier, " &
                            " p.IsTaxable As [Taxable], " &
                            " p.IsActive AS [Active] " &
                            " FROM " &
                            " 	tblMst_Particular p(nolock) " &
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

    Private Sub frmParticularQuery_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub
End Class