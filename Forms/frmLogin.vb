Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient

Public Class frmLogin
    Dim sSQL As String
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrHandler
        CenterForm(Me)

        If Connected2Server() = False Then : Exit Sub : End If
        'This is now on GitHub
        txtServer.Text = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_Source", "")
        txtDB.Text = ReadIni(Application.StartupPath + "\config.ini", "Settings", "DB_Name", "")

        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub frmLogin_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmLogin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Call object_center(Me, Panel)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Dispose()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If MyConn.State = ConnectionState.Open Then MyConn.Close()

        If is_empty(txtLoginName) = True Then Exit Sub
        If is_empty(txtPassword) = True Then Exit Sub

        Dim SQLCmd As New SqlCommand(" SELECT * FROM a_user_master WHERE login_name='" & sqlSafe(txtLoginName.Text) & "' AND pass_word ='" & sqlSafe(txtPassword.Text) & "' AND status_id='AC' ", MyConn)
        MyConn.Open()

        Dim Dtrdr As SqlDataReader = SQLCmd.ExecuteReader()

        If Dtrdr.Read = False Then
            MsgBox("Login Name and/or Password is incorrect! Try again.", MsgBoxStyle.Critical)
            Exit Sub
        Else
            LOGIN_SUCCEEDED = True
            ACTIVE_USER.m_LoginName = Dtrdr.Item("login_name").ToString
            ACTIVE_LOCATION.m_LocationID = 1305
            MAIN.m_ACTIVE_USER.Text = UCase(Dtrdr.Item("last_name").ToString) & "," & UCase(Dtrdr.Item("first_name").ToString)
            MAIN.Show()
            Me.Hide()
        End If

        LOGIN_SUCCEEDED = True
        ACTIVE_USER.m_LoginName = "admin"
        ACTIVE_LOCATION.m_LocationID = 1305
        MAIN.Show()
        Me.Hide()
    End Sub

    Private Sub txtLoginName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLoginName.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnLogin_Click(btnLogin, New EventArgs)
        End If
    End Sub

    Private Sub txtPassword_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnLogin_Click(btnLogin, New EventArgs)
        End If
    End Sub
End Class