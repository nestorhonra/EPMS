Option Explicit On
Option Strict Off

Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data

Public Class frmReportViewer
    Public CrxRpt As New ReportDocument
    Public CrxSubRpt1 As New ReportDocument
    Public CrxSubRpt2 As New ReportDocument
    Private Sub frmReportViewer_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub CRViewer_Load(sender As Object, e As EventArgs) Handles CRViewer.Load

    End Sub
End Class