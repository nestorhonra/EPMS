Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms

Public Class clsControls
    Inherits clsVariables

    Private sConnections As New clsConnections
    Public Sub ComboBox_FillDate(ByRef cbo As ComboBox)
        Dim dateTime1 As DateTime = DateTime.Now
        Dim iYear As Integer = dateTime1.Year
        Dim dateTime2 As DateTime = DateTime.Now
        Dim iMonth As Integer = dateTime2.Month
        Dim iDays As Integer = DateTime.DaysInMonth(iYear, iMonth)
        Dim c As Integer = 1
        While c <= iDays
            cbo.Items.Add(c)
            c = c + 1
        End While
    End Sub

    Public Sub ComboBox_FillMonth(ByRef cbo As ComboBox)
        cbo.Items.Add("January")
        cbo.Items.Add("February")
        cbo.Items.Add("March")
        cbo.Items.Add("April")
        cbo.Items.Add("May")
        cbo.Items.Add("June")
        cbo.Items.Add("July")
        cbo.Items.Add("August")
        cbo.Items.Add("September")
        cbo.Items.Add("October")
        cbo.Items.Add("November")
        cbo.Items.Add("December")
    End Sub

    Public Sub ComboBox_FillYear(ByRef cbo As ComboBox, ByVal iStartYear As Integer, ByVal iEndYear As Integer)
        Dim i As Integer = iStartYear
        While i <= iEndYear
            cbo.Items.Add(i)
            i = i + 1
        End While
    End Sub

    Public Function Control_Number(ByRef cbo As ComboBox, ByVal e As Integer) As Boolean
        Dim bReturn As Boolean = False
        If e = 46 Then
            Dim s As String = cbo.Text + "."
            Dim i As Integer = 0
            While i < cbo.Text.Length
                Dim ch1 As Char = s(i)
                If ch1.CompareTo("."c) = 0 Then
                    bReturn = False
                Else
                    i = i + 1
                End If
            End While
        ElseIf ((e >= 48) AndAlso (e <= 57)) OrElse ((e <> 8) AndAlso (e = 46)) Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function

    Public Function Control_Number(ByRef txt As TextBox, ByVal e As Integer) As Boolean
        Dim bReturn As Boolean = False
        If e = 46 Then
            Dim s As String = txt.Text + "."
            Dim i As Integer = 0
            While i < txt.TextLength
                Dim ch1 As Char = s(i)
                If ch1.CompareTo("."c) = 0 Then
                    bReturn = False
                Else
                    i = i + 1
                End If
            End While
        ElseIf ((e >= 48) AndAlso (e <= 57)) OrElse ((e <> 8) AndAlso (e = 46)) Then
            bReturn = True
        Else
            bReturn = False
        End If
        Return bReturn
    End Function

    Public Sub Focus_Off(ByRef txt As TextBox)
        If txt.BackColor = Color.LemonChiffon Then
            txt.BackColor = Color.White
        End If
    End Sub


    Public Sub Focus_Off(ByRef cbo As ComboBox)
        If cbo.BackColor = Color.LemonChiffon Then
            cbo.BackColor = Color.White
        End If
    End Sub

    Public Sub Focus_Off(ByRef lv As ListView, ByVal e As Integer)
        If lv.BackColor = Color.LemonChiffon Then
            lv.BackColor = Color.White
        End If
    End Sub

    Public Sub Focus_Off(ByRef cbo As ToolStripComboBox)
        If cbo.BackColor = Color.LemonChiffon Then
            cbo.BackColor = Color.White
        End If
    End Sub

    Public Sub Focus_On(ByRef cbo As ComboBox, ByVal bIsColor As Boolean)
        If (cbo.Text.CompareTo("Type here...") = 0) AndAlso (cbo.Tag.ToString().Length > 0) AndAlso cbo.Tag.ToString().Equals("Search") Then
            cbo.ForeColor = Color.Black
            cbo.Text = ""
        End If
        cbo.Focus()
        cbo.SelectionStart = 0
        cbo.SelectionLength = cbo.Text.Length
        If bIsColor Then
            cbo.BackColor = Color.LemonChiffon
        End If
    End Sub

    Public Sub Focus_On(ByRef txt As TextBox, ByVal bIsColor As Boolean)
        txt.Focus()
        txt.SelectionStart = 0
        txt.SelectionLength = txt.TextLength
        If bIsColor Then
            txt.BackColor = Color.LemonChiffon
        End If
    End Sub

    Public Sub Focus_On(ByRef lv As ListView, ByVal bIsColor As Boolean)
        If lv.Items.Count > 0 Then
            lv.Focus()
            Try
                Dim lvPosition As Integer = lv.FocusedItem.Index
                lv.Items(lvPosition).Focused = True
                lv.Items(lvPosition).Selected = True
                lv.Items(lvPosition).EnsureVisible()
                lvPosition = 0
            Catch e As NullReferenceException
            End Try
        End If
        If bIsColor Then
            lv.BackColor = Color.LemonChiffon
        End If
    End Sub

    Public Sub Focus_On(ByRef cbo As ToolStripComboBox, ByVal bIsColor As Boolean)
        If (cbo.Text.CompareTo("Type here...") = 0) AndAlso (cbo.Tag.ToString().CompareTo("Search") = 0) Then
            cbo.ForeColor = Color.Black
            cbo.Text = ""
        End If
        If bIsColor Then
            cbo.BackColor = Color.LemonChiffon
        End If
    End Sub

    Public Sub Populate_Control(ByRef cboColumnHeaders As ComboBox, ByRef lv As ListView)
        If lv.Columns.Count > 0 Then
            cboColumnHeaders.Items.Clear()
            Dim i As Integer = 0
            While i < lv.Columns.Count
                cboColumnHeaders.Items.Add(lv.Columns(i).Text)
                i = i + 1
            End While
            cboColumnHeaders.SelectedIndex = 0
        End If
    End Sub

    Public Sub Populate_Control(ByRef cbo As ComboBox, ByVal cs As clsTypeVariables.COMBOBOX_STATE)
        cbo.Items.Clear()
        Select Case cs
            Case clsTypeVariables.COMBOBOX_STATE.adStateGenderMode
                cbo.Items.Add("Male")
                cbo.Items.Add("Female")
                cbo.Items.Add("Undefined")
                Return

            Case clsTypeVariables.COMBOBOX_STATE.adStateStatusMode
                cbo.Items.Add("Single")
                cbo.Items.Add("Married")
                cbo.Items.Add("Seperated")
                cbo.Items.Add("Widowed")
        End Select
    End Sub

    Public Sub Populate_Control(ByRef cbo As ComboBox, ByVal sSQL As String, ByVal sTable As String, ByVal sField As String)
        Dim totalRow As Long = CLng(0)
        sConnections.SetData(sSQL, sTable)
        totalRow = CLng(Convert.ToInt32(clsConnections.DS.Tables(sTable).Rows.Count))
        If CInt(totalRow) > 0 Then
            cbo.Items.Clear()
            Dim i As Integer = 0
            While CLng(i) < totalRow
                Dim sDataRow As DataRow = clsConnections.DS.Tables(sTable).Rows(i)
                cbo.Items.Add(sDataRow(sField).ToString())
                i = i + 1
            End While
        End If
        sConnections.CloseData()
    End Sub

    Public Sub Populate_Control(ByRef lb As ListBox, ByVal sSQL As String, ByVal sTable As String, ByVal sField As String)
        Dim totalRow As Long = CLng(0)
        sConnections.SetData(sSQL, sTable)
        totalRow = CLng(Convert.ToInt32(clsConnections.DS.Tables(sTable).Rows.Count))
        If CInt(totalRow) > 0 Then
            lb.Items.Clear()
            Dim i As Integer = 0
            While CLng(i) < totalRow
                Dim sDataRow As DataRow = clsConnections.DS.Tables(sTable).Rows(i)
                lb.Items.Add(sDataRow(sField).ToString())
                i = i + 1
            End While
        End If
        sConnections.CloseData()
    End Sub

    Public Function Set_Form_Caption(ByVal sText As String, ByVal sFormCaption As String, ByVal State As clsTypeVariables.FORM_STATE) As String
        If State = clsTypeVariables.FORM_STATE.adStateAddMode Then
            sFormCaption = "Add " + sFormCaption
        ElseIf State = clsTypeVariables.FORM_STATE.adStateEditMode Then
            sFormCaption = "Edit " + sFormCaption
        End If
        If sText.Length > 0 Then
            Return sFormCaption + " - " + sText
        End If
        Return sFormCaption
    End Function

    Public Function Validate_Control(ByRef frm As Form, ByRef err As ErrorProvider, ByVal sAdditonalMsg As String) As Boolean
        err.Clear()
        Dim c As Control
        For Each c In frm.Controls
            If TypeOf c Is TextBox Then
                Dim txt As TextBox = CType(c, TextBox)
                If (txt.Text.Length <> 0) OrElse ((txt.Tag) Is Nothing) Then
                    GoTo label_1
                End If
                err.SetError(txt, txt.Tag.ToString() + " " + sAdditonalMsg)
                txt.Focus()
                Return False
            End If
            If TypeOf c Is ComboBox Then
                Dim cb As ComboBox = CType(c, ComboBox)
                If (cb.SelectedIndex = -1) AndAlso (Not (cb.Tag) Is Nothing) Then
                    err.SetError(cb, sAdditonalMsg + cb.Tag.ToString())
                    cb.Focus()
                    Return False
                End If
            End If
label_1:
        Next
        Return True
    End Function

    Public Function Validate_Control(ByRef frm As Form, ByRef err As ErrorProvider) As Boolean
        err.Clear()
        Dim c As Control
        For Each c In frm.Controls
            If TypeOf c Is TextBox Then
                Dim txt As TextBox = CType(c, TextBox)
                If (txt.Text.Length <> 0) OrElse ((txt.Tag) Is Nothing) Then
                    GoTo label_1
                End If
                err.SetError(txt, txt.Tag.ToString() + " is empty.")
                txt.Focus()
                Return False
            End If
            If TypeOf c Is ComboBox Then
                Dim cb As ComboBox = CType(c, ComboBox)
                If (cb.SelectedIndex = -1) AndAlso (Not (cb.Tag) Is Nothing) Then
                    err.SetError(cb, "Please select " + cb.Tag.ToString())
                    cb.Focus()
                    Return False
                End If
            End If
label_1:
        Next
        Return True
    End Function

End Class ' class clsControls