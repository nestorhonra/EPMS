Option Explicit On
Option Strict Off

Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data
Friend Class frmPayrollPerEmployee
    Public Property employee_id As Double
    Public Property payroll_distribution As Double
    Public Property payroll_id As Double
    Public Property payroll_period As Double
End Class