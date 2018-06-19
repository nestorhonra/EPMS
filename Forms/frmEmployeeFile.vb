Option Strict Off
Option Explicit On

Imports System.Data
Imports System.Data.SqlClient

Friend Class frmEmployeeFile
    Public State As FormState
    Public PK As Double

    Dim blnUpdateInProgress As Boolean
    Dim sSQL As String

    Dim dataBytes() As Byte
    Dim FN As String
    Private Sub frmEmployeeFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        On Error GoTo ErrHandler
        CenterForm(Me)

        lvEmploymentType.SmallImageList = MAIN.i16x16
        lvEmploymentType.LargeImageList = MAIN.i16x16

        lvPromotion.SmallImageList = MAIN.i16x16
        lvPromotion.LargeImageList = MAIN.i16x16

        lvCostCenter.SmallImageList = MAIN.i16x16
        lvCostCenter.LargeImageList = MAIN.i16x16

        State = FormState.ViewStateMode
        Call EnableMenu(True, False, False, False, True, True)

        Exit Sub
ErrHandler:
        MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Public Sub EnableMenu(iNew As Boolean, iSave As Boolean, iCancel As Boolean, iDelete As Boolean, iPrint As Boolean, iClose As Boolean)
        On Error GoTo ErrHandler
        With Me
            .btnNew.Enabled = iNew
            .btnSave.Enabled = iSave
            .btnCancel.Enabled = iCancel
            .btnDelete.Enabled = iDelete
            .btnPrint.Enabled = iPrint
            .btnClose.Enabled = iClose
        End With
        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub frmEmployeeFile_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmEmployeeFile_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
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

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        If State = FormState.AddStateMode Then Exit Sub
        frmEmployeeFileQuery.ShowDialog()
    End Sub

    Public Sub FillFormViewUsingDataset(ByVal p_k As Double)
        Dim dt As New DataTable
        Try
            dt = GetDataSet("SELECT * FROM tblMst_Employee WHERE EmployeeCode=" & p_k)

            If dt.Rows.Count < 1 Then : MsgBox("No records found!", MsgBoxStyle.Exclamation) : End If

            Call FillComboBox(dcStatus, "SELECT * FROM tblMst_EmploymentStatus ", "Description", "EmploymentStatusCode")
            Call FillComboBox(dcCivilStatus, "SELECT * FROM tblMst_CivilStatus ", "CivilStatus", "CivilStatusCode")
            Call FillComboBox(dcNationality, "SELECT * FROM tblMst_Nationality ", "Nationality", "NationalityCode")
            Call FillComboBox(dcProvince, "SELECT * FROM tblMst_Province ORDER BY ProvinceDesc", "ProvinceDesc", "ProvinceCode")
            Call FillComboBox(dcCity, "SELECT * FROM tblMst_City ORDER BY CityDesc", "CityDesc", "CityCode")
            Call FillComboBox(dcGender, "SELECT * FROM tblMst_Gender ", "Description", "GenderCode")

            Call FillComboBox(dcEmploymentType, "SELECT * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")
            Call FillComboBox(dcCompany, "SELECT * FROM tblMst_Company ", "CompanyName", "CompanyCode")
            Call FillComboBox(dcDepartment, "SELECT * FROM tblMst_Department ", "Description", "DepartmentCode")
            Call FillComboBox(dcDivision, "SELECT * FROM tblMst_Division ", "Description", "DivisionCode")
            Call FillComboBox(dcUnit, "SELECT * FROM tblMst_Unit ", "Description", "UnitCode")
            Call FillComboBox(dcJobLevel, "SELECT * FROM tblMst_JobLevel ", "Description", "JobLevelCode")
            Call FillComboBox(dcJobTitle, "SELECT * FROM tblMst_JobTitle ", "Description", "JobTitleCode")

            Call FillComboBox(dcTaxCode, "SELECT * FROM tblMst_TaxCode ", "ShortDesc", "TaxCode")
            Call FillComboBox(dcPayrollMode, "SELECT * FROM tblMst_PayrollDistribution ", "Description", "PayrollDistributionCode")
            Call FillComboBox(dcBank, "SELECT * FROM tblMst_Bank ", "Description", "BankCode")
            Call FillComboBox(dcRelationship, "SELECT * FROM tblMst_Relationship ", "Description", "RelationshipCode")

            Call FillComboBox(dcDegree, "SELECT * FROM tblMst_Degree ", "Description", "DegreeCode")
            Call FillComboBox(dcCourse, "SELECT * FROM tblMst_Course ", "Description", "CourseCode")

            Call FillComboBox(dcEmploymentType1, "SELECT * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")
            Call FillComboBox(dcEmploymentType2, "SELECT * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")

            Call FillComboBox(dcJobTitle1, "SELECT * FROM tblMst_JobTitle ", "Description", "JobTitleCode")
            Call FillComboBox(dcJobTitle2, "SELECT * FROM tblMst_JobTitle ", "Description", "JobTitleCode")

            Call FillComboBox(dcCostCenter1, "SELECT * FROM tblMst_Division ", "ShortName", "DivisionCode")
            Call FillComboBox(dcCostCenter2, "SELECT * FROM tblMst_Division ", "ShortName", "DivisionCode")

            PK = toNumber(dt.Rows(0)("EmployeeCode").ToString)

            blnUpdateInProgress = True
            Call EnableMenu(False, True, True, True, False, False)

            If FileIO.FileSystem.FileExists((My.Application.Info.DirectoryPath & "\Profiles\Employees\" & PK & ".jpg")) = False Then
                PIC_PROFILE.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\Profiles\Employees\NoPic.jpg")
            Else
                PIC_PROFILE.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\Profiles\Employees\" & PK & ".jpg")
            End If

            txtEmployeeNo.Text = dt.Rows(0)("EmployeeCode").ToString
            txtLastName.Text = dt.Rows(0)("LastName").ToString
            txtFirstName.Text = dt.Rows(0)("FirstName").ToString
            txtMidName.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("MiddleName")), "", dt.Rows(0)("MiddleName").ToString)
            txtFullName.Text = GetValueAt("SELECT e.LastName + ', ' + e.FirstName + ' ' + SUBSTRING(e.MiddleName, 1, 1)+ '.' AS [Employee Name] FROM tblMst_Employee e WHERE e.EmployeeCode=" & toNumber(dt.Rows(0)("EmployeeCode").ToString), "Employee Name")

            dtDateHired.Value = IIf(String.IsNullOrEmpty(dt.Rows(0)("DateHired").ToString), GetServerDateTime(), dt.Rows(0)("DateHired").ToString)
            dcStatus.SelectedValue = dt.Rows(0)("EmploymentStatusCode").ToString
            dcCivilStatus.SelectedValue = toNumber(dt.Rows(0)("CivilStatusCode").ToString)
            dcNationality.SelectedValue = dt.Rows(0)("NationalityCode").ToString

            txtHouseNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("HouseNo").ToString), "", dt.Rows(0)("HouseNo").ToString)
            txtAddress.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("PresentAddress").ToString), "", dt.Rows(0)("PresentAddress").ToString)

            txtEmail.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("EmailAddress").ToString), "", dt.Rows(0)("EmailAddress").ToString)
            txtReligion.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("Religion").ToString), "", dt.Rows(0)("Religion").ToString)
            txtFaxNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("FaxNo").ToString), "", dt.Rows(0)("FaxNo").ToString)

            dcProvince.SelectedValue = toNumber(dt.Rows(0)("ProvinceCode").ToString)
            dcCity.SelectedValue = dt.Rows(0)("CityCode").ToString
            'txtZipCode.Text = GetValueAt("SELECT * FROM tblMst_City WHERE CityCode = '" & (dt.Rows(0)("CityCode").ToString) & "'", "Zipcode")
            dcGender.SelectedValue = dt.Rows(0)("GenderCode").ToString
            dtDOB.Value = IIf(String.IsNullOrEmpty(dt.Rows(0)("BirthDate").ToString), GetServerDateTime(), dt.Rows(0)("BirthDate").ToString)

            txtPhoneNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("ContactNo").ToString), "", dt.Rows(0)("ContactNo").ToString)
            txtMobileNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("MobileNo").ToString), "", dt.Rows(0)("MobileNo").ToString)
            txtBirthPlace.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("BirthPlace").ToString), "", dt.Rows(0)("BirthPlace").ToString)

            txtPerson.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("EmergencyContactPerson").ToString), "", dt.Rows(0)("EmergencyContactPerson").ToString)
            txtPersonAddress.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("Address1").ToString), "", dt.Rows(0)("Address1").ToString)
            txtPersonEmail.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("EmailAddress1").ToString), "", dt.Rows(0)("EmailAddress1").ToString)
            txtPersonPhoneNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("PhoneNo1").ToString), "", dt.Rows(0)("PhoneNo1").ToString)
            txtPersonMobileNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("MobileNo1").ToString), "", dt.Rows(0)("MobileNo1").ToString)

            txtGuardianName.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("GuardianName").ToString), "", dt.Rows(0)("GuardianName").ToString)
            txtGuardianAddress.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("GuardianAddress").ToString), "", dt.Rows(0)("GuardianAddress").ToString)
            txtGuardianPhoneNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("GuardianPhoneNo").ToString), "", dt.Rows(0)("GuardianPhoneNo").ToString)
            txtGuardianMobileNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("GuardianMobileNo").ToString), "", dt.Rows(0)("GuardianMobileNo").ToString)

            dcEmploymentType.SelectedValue = dt.Rows(0)("EmploymentTypeCode").ToString
            dcCompany.SelectedValue = dt.Rows(0)("CompanyCode").ToString
            dcDepartment.SelectedValue = toNumber(dt.Rows(0)("DepartmentCode").ToString)
            dcDivision.SelectedValue = toNumber(dt.Rows(0)("DivisionCode").ToString)
            dcJobLevel.SelectedValue = dt.Rows(0)("JobLevelCode").ToString
            dcJobTitle.SelectedValue = dt.Rows(0)("JobTitleCode").ToString

            txtSSS.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("SSSID").ToString), "", dt.Rows(0)("SSSID").ToString)
            txtTIN.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("TINID").ToString), "", dt.Rows(0)("TINID").ToString)
            txtPHIC.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("PhilHealth").ToString), "", dt.Rows(0)("PhilHealth").ToString)
            txtHDMF.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("HDMF").ToString), "", dt.Rows(0)("HDMF").ToString)
            dcBank.SelectedValue = IIf(String.IsNullOrEmpty(dt.Rows(0)("BankCode").ToString), "", dt.Rows(0)("BankCode").ToString)
            txtAccountNo.Text = IIf(String.IsNullOrEmpty(dt.Rows(0)("BankAccountNo").ToString), "", dt.Rows(0)("BankAccountNo").ToString)

            dcPayrollMode.SelectedValue = dt.Rows(0)("PayrollDistributionCode").ToString
            txtMonthlySalary.Text = toMoney(dt.Rows(0)("MonthlyRate").ToString)
            txtDaily.Text = toMoney(dt.Rows(0)("DailyRate").ToString)
            txtRatePerHour.Text = toMoney(dt.Rows(0)("RatePerHour").ToString)
            txtDTRPIN.Text = dt.Rows(0)("PinCode").ToString

            LAST_USER.Text = "Last Updated By :  " & IIf(String.IsNullOrEmpty(dt.Rows(0)("ModifiedBy").ToString), dt.Rows(0)("EncodedBy").ToString, dt.Rows(0)("ModifiedBy").ToString)
            LAST_DATE_MODIFIED.Text = "Last Date Modified : " & IIf(String.IsNullOrEmpty(dt.Rows(0)("LastDateModified").ToString), dt.Rows(0)("DateEncoded").ToString, dt.Rows(0)("LastDateModified").ToString)

            Call FillFamily(" SELECT " &
                            " f.RelationshipCode, " &
                            " r.Description AS [Relationship], " &
                            " f.LastName, " &
                            " f.FirstName, " &
                            " f.MiddleName, " &
                            " f.ContactNo, " &
                            " f.BirthDate, " &
                            " f.Address, " &
                            " f.IsNextofKin, " &
                            " f.IsBeneficiary, " &
                            " f.IsDependent " &
                        " FROM " &
                            " tblMst_Employee_Family f(nolock) " &
                        " LEFT OUTER JOIN tblMst_Relationship r(nolock)ON f.RelationshipCode = r.RelationshipCode " &
                        " WHERE " &
                        " f.EmployeeCode=" & toNumber(p_k), lvFamily)

            Call FillEducation(" SELECT " &
                                " e.DegreeCode, " &
                                " d.Description AS [Degree], " &
                                " e.CourseCode, " &
                                " c.Description AS [Course], " &
                                " e.School, " &
                                " e.DateGraduated, " &
                                " e.YearFrom, " &
                                " e.YearTo " &
                            " FROM " &
                                " tblMst_Employee_Education e(nolock) " &
                            " LEFT OUTER JOIN tblMst_Degree d(nolock)On e.DegreeCode = d.DegreeCode " &
                            " LEFT OUTER JOIN tblMst_Course c(nolock)On e.CourseCode = c.CourseCode " &
                        " WHERE " &
                        " e.EmployeeCode=" & toNumber(p_k), lvEducation)

            Call FillEmployment(" SELECT " &
                                " e.Designation, " &
                                " e.Employer AS [Employer], " &
                                " e.Salary, " &
                                " e.StartDate, " &
                                " e.EndDate, " &
                                " e.LeavingReason, " &
                                " e.Remarks " &
                            " FROM " &
                                " tblMst_Employee_Employment e(nolock) " &
                        " WHERE " &
                        " e.EmployeeCode=" & toNumber(p_k), lvEmployment)

            Call FillEmploymentType(" SELECT " &
                                        " et.ChangeNo, " &
                                        " et.EmploymentTypeCode1, " &
                                        " t1.Description As [EmploymentType1], " &
                                        " et.EmploymentTypeCode2, " &
                                        " t2.Description As [EmploymentType2], " &
                                        " et.EffectivityDate, " &
                                        " et.Remarks " &
                                    " FROM " &
                                        " tblMst_Employee_EmploymentType et(nolock) " &
                                    " LEFT OUTER JOIN tblMst_EmploymentType t1(nolock)On et.EmploymentTypeCode1 = t1.EmploymentTypeCode " &
                                    " LEFT OUTER JOIN tblMst_EmploymentType t2(nolock)On et.EmploymentTypeCode2 = t2.EmploymentTypeCode " &
                                    "WHERE " &
                                    " et.EmployeeCode=" & toNumber(p_k), lvEmploymentType)

            Call FillEmployeePromotion(" SELECT " &
                                        " p.PromotionCode, " &
                                        " p.JobTitleCode1, " &
                                        " j.Description As [JobTitle1], " &
                                        " p.JobTitleCode2, " &
                                        " t.Description As [JobTitle2], " &
                                        " p.Salary1, " &
                                        " p.Salary2, " &
                                        " p.EffectivityDate, " &
                                        " p.Remarks " &
                                    " FROM " &
                                        " tblMst_Employee_Promotion p(nolock) " &
                                    " LEFT OUTER JOIN tblMst_JobTitle j(nolock)ON p.JobTitleCode1 = j.JobTitleCode " &
                                    " LEFT OUTER JOIN tblMst_JobTitle t(nolock)ON p.JobTitleCode2 = t.JobTitleCode " &
                                    "WHERE " &
                                    " p.EmployeeCode=" & toNumber(p_k), lvPromotion)

            Call FillEmployeeCostCenter(" SELECT " &
                                            " ec.ChangeCode, " &
                                            " c.ShortName AS [CostCenter1], " &
                                            " c2.ShortName As [CostCenter2], " &
                                            " ec.EffectivityDate, " &
                                            " ec.Remarks " &
                                        " FROM " &
                                            " tblMst_Employee_Division ec(nolock) " &
                                        " LEFT OUTER JOIN tblMst_Division c(nolock)ON ec.DivisionCode1 = c.DivisionCode " &
                                        " LEFT OUTER JOIN tblMst_Division c2(nolock)ON ec.DivisionCode2 = c2.DivisionCode " &
                                        "WHERE " &
                                        " ec.EmployeeCode=" & toNumber(p_k), lvCostCenter)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub CommandPass(ByVal srcPerformWhat As String)
        On Error GoTo ErrHandler
        
        Select Case srcPerformWhat
            Case "New"
                State = FormState.AddStateMode
                blnUpdateInProgress = True

                Call ClearAllText(Me)
                Call ClearAllText(Me, TabControl1.Controls)

                Call ClearAllCombo(Me)
                Call ClearAllCombo(Me, TabControl1.Controls)

                Call EnableMenu(False, True, True, False, False, False)

                Call FillComboBox(dcStatus, "Select * FROM tblMst_EmploymentStatus ", "Description", "EmploymentStatusCode")
                Call FillComboBox(dcCivilStatus, "Select * FROM tblMst_CivilStatus ", "CivilStatus", "CivilStatusCode")
                Call FillComboBox(dcNationality, "Select * FROM tblMst_Nationality ", "Nationality", "NationalityCode")
                Call FillComboBox(dcProvince, "Select * FROM tblMst_Province ORDER BY ProvinceDesc", "ProvinceDesc", "ProvinceCode")
                Call FillComboBox(dcCity, "Select * FROM tblMst_City ORDER BY CityDesc", "CityDesc", "CityCode")
                Call FillComboBox(dcGender, "Select * FROM tblMst_Gender ", "Description", "GenderCode")

                Call FillComboBox(dcEmploymentType, "Select * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")
                Call FillComboBox(dcCompany, "Select * FROM tblMst_Company ", "CompanyName", "CompanyCode")
                Call FillComboBox(dcDepartment, "Select * FROM tblMst_Department ", "Description", "DepartmentCode")
                Call FillComboBox(dcDivision, "SELECT * FROM tblMst_Division ", "Description", "DivisionCode")
                Call FillComboBox(dcUnit, "SELECT * FROM tblMst_Unit ", "Description", "UnitCode")
                Call FillComboBox(dcJobLevel, "Select * FROM tblMst_JobLevel ", "Description", "JobLevelCode")
                Call FillComboBox(dcJobTitle, "Select * FROM tblMst_JobTitle ", "Description", "JobTitleCode")

                Call FillComboBox(dcTaxCode, "Select * FROM tblMst_TaxCode ", "ShortDesc", "TaxCode")
                Call FillComboBox(dcPayrollMode, "Select * FROM tblMst_PayrollDistribution ", "Description", "PayrollDistributionCode")
                Call FillComboBox(dcBank, "Select * FROM tblMst_Bank ", "Description", "BankCode")
                Call FillComboBox(dcRelationship, "Select * FROM tblMst_Relationship ", "Description", "RelationshipCode")

                Call FillComboBox(dcDegree, "Select * FROM tblMst_Degree ", "Description", "DegreeCode")
                Call FillComboBox(dcCourse, "Select * FROM tblMst_Course ", "Description", "CourseCode")

                Call FillComboBox(dcEmploymentType1, "SELECT * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")
                Call FillComboBox(dcEmploymentType2, "SELECT * FROM tblMst_EmploymentType ", "Description", "EmploymentTypeCode")

                Call FillComboBox(dcJobTitle1, "SELECT * FROM tblMst_JobTitle ", "Description", "JobTitleCode")
                Call FillComboBox(dcJobTitle2, "SELECT * FROM tblMst_JobTitle ", "Description", "JobTitleCode")

                Call FillComboBox(dcCostCenter1, "SELECT * FROM tblMst_Division ", "ShortName", "DivisionCode")
                Call FillComboBox(dcCostCenter2, "SELECT * FROM tblMst_Division ", "ShortName", "DivisionCode")

                txtEmployeeNo.Text = Mid$(Year(Now), 3, 3) & Format(GenerateNewIDNumber("tblMst_Employee", "AutoCode"), "000#")
                txtLastName.Focus()

            Case "Save"
                If is_empty(txtEmployeeNo) = True Then Exit Sub

                If State = FormState.AddStateMode Then
                    PK = Mid$(Year(Now), 3, 3) & Format(GenerateNewIDNumber("tblMst_Employee", "AutoCode"), "000#")

                    sSQL = "INSERT INTO tblMst_Employee"
                    sSQL = sSQL & "(EmployeeCode"
                    sSQL = sSQL & ", LastName"
                    sSQL = sSQL & ", MiddleName"
                    sSQL = sSQL & ", FirstName"
                    sSQL = sSQL & ", DateHired"
                    sSQL = sSQL & ", EmploymentStatusCode"
                    sSQL = sSQL & ", CivilStatusCode"
                    sSQL = sSQL & ", NationalityCode"
                    sSQL = sSQL & ", HouseNo"
                    sSQL = sSQL & ", PresentAddress"
                    sSQL = sSQL & ", ProvinceCode"
                    sSQL = sSQL & ", CityCode"
                    sSQL = sSQL & ", BirthDate"
                    sSQL = sSQL & ", GenderCode"
                    sSQL = sSQL & ", ContactNo"
                    sSQL = sSQL & ", MobileNo"
                    sSQL = sSQL & ", FaxNo"
                    sSQL = sSQL & ", EmailAddress"
                    sSQL = sSQL & ", Religion"
                    sSQL = sSQL & ", BirthPlace"
                    sSQL = sSQL & ", EmergencyContactPerson"
                    sSQL = sSQL & ", Address1"
                    sSQL = sSQL & ", PhoneNo1"
                    sSQL = sSQL & ", MobileNo1"
                    sSQL = sSQL & ", EmailAddress1"
                    sSQL = sSQL & ", GuardianName"
                    sSQL = sSQL & ", GuardianAddress"
                    sSQL = sSQL & ", GuardianPhoneNo"
                    sSQL = sSQL & ", GuardianMobileNo"

                    sSQL = sSQL & ", EmploymentTypeCode"
                    sSQL = sSQL & ", CompanyCode"
                    sSQL = sSQL & ", DepartmentCode"
                    sSQL = sSQL & ", DivisionCode"
                    sSQL = sSQL & ", UnitCode"
                    sSQL = sSQL & ", JobLevelCode"
                    sSQL = sSQL & ", JobTitleCode"

                    sSQL = sSQL & ", SSSID"
                    sSQL = sSQL & ", TINID"
                    sSQL = sSQL & ", PhilHealth"
                    sSQL = sSQL & ", HDMF"
                    sSQL = sSQL & ", BankCode"
                    sSQL = sSQL & ", BankAccountNo"

                    sSQL = sSQL & ", PayrollDistributionCode"
                    sSQL = sSQL & ", MonthlyRate"
                    sSQL = sSQL & ", DailyRate"
                    sSQL = sSQL & ", RatePerHour"
                    sSQL = sSQL & ", PINCode"


                    sSQL = sSQL & ", DateEncoded"
                    sSQL = sSQL & ", EncodedBy"
                    sSQL = sSQL & ") VALUES ("
                    sSQL = sSQL & " " & PK
                    sSQL = sSQL & ", '" & sqlSafe(UCase(txtLastName.Text)) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtMidName.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtFirstName.Text) & "'"
                    sSQL = sSQL & ", '" & dtDateHired.Value & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcStatus.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcCivilStatus.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcNationality.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtHouseNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtAddress.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcProvince.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcCity.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & dtDOB.Value & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcGender.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPhoneNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtMobileNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtFaxNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtEmail.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtReligion.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtBirthPlace.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPerson.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPersonAddress.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPersonPhoneNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPersonMobileNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPersonEmail.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtGuardianName.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtGuardianAddress.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtGuardianPhoneNo.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtGuardianMobileNo.Text) & "'"

                    sSQL = sSQL & ", '" & sqlSafe(dcEmploymentType.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcCompany.SelectedValue) & "'"
                    sSQL = sSQL & ", " & toNumber(dcDepartment.SelectedValue)
                    sSQL = sSQL & ", " & toNumber(dcDivision.SelectedValue)
                    sSQL = sSQL & ", " & toNumber(dcUnit.SelectedValue)
                    sSQL = sSQL & ", '" & sqlSafe(dcJobLevel.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcJobTitle.SelectedValue) & "'"

                    sSQL = sSQL & ", '" & sqlSafe(txtSSS.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtTIN.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtPHIC.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtHDMF.Text) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(dcBank.SelectedValue) & "'"
                    sSQL = sSQL & ", '" & sqlSafe(txtAccountNo.Text) & "'"

                    sSQL = sSQL & ", '" & sqlSafe(dcPayrollMode.SelectedValue) & "'"
                    sSQL = sSQL & ", " & toNumber(txtMonthlySalary.Text)
                    sSQL = sSQL & ", " & toNumber(txtDaily.Text)
                    sSQL = sSQL & ", " & toNumber(txtRatePerHour.Text)
                    sSQL = sSQL & ", " & toNumber(txtDTRPIN.Text)

                    sSQL = sSQL & ", '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & ", '" & ACTIVE_USER.m_LoginName & "')"

                ElseIf State = FormState.EditStateMode Then

                    sSQL = "UPDATE tblMst_Employee SET"
                    sSQL = sSQL & " LastName = '" & IIf(String.IsNullOrEmpty(sqlSafe(UCase(txtLastName.Text))), ".", sqlSafe(UCase(txtLastName.Text))) & "'"
                    sSQL = sSQL & " ,MiddleName = '" & sqlSafe(txtMidName.Text) & "'"
                    sSQL = sSQL & " ,FirstName = '" & sqlSafe(txtFirstName.Text) & "'"
                    sSQL = sSQL & " ,DateHired = '" & dtDateHired.Value & "'"
                    sSQL = sSQL & " ,EmploymentStatusCode = '" & sqlSafe(dcStatus.SelectedValue) & "'"
                    sSQL = sSQL & " ,CivilStatusCode = " & toNumber(dcCivilStatus.SelectedValue)
                    sSQL = sSQL & " ,NationalityCode = '" & sqlSafe(dcNationality.SelectedValue) & "'"
                    sSQL = sSQL & " ,HouseNo = '" & sqlSafe(txtHouseNo.Text) & "'"
                    sSQL = sSQL & " ,PresentAddress = '" & sqlSafe(txtAddress.Text) & "'"
                    sSQL = sSQL & " ,ProvinceCode = '" & sqlSafe(dcProvince.SelectedValue) & "'"
                    sSQL = sSQL & " ,CityCode = '" & sqlSafe(dcCity.SelectedValue) & "'"
                    sSQL = sSQL & " ,BirthDate = '" & dtDOB.Value & "'"
                    sSQL = sSQL & " ,GenderCode = '" & sqlSafe(dcGender.SelectedValue) & "'"
                    sSQL = sSQL & " ,ContactNo = '" & sqlSafe(txtPhoneNo.Text) & "'"
                    sSQL = sSQL & " ,MobileNo = '" & sqlSafe(txtMobileNo.Text) & "'"
                    sSQL = sSQL & " ,FaxNo = '" & sqlSafe(txtFaxNo.Text) & "'"
                    sSQL = sSQL & " ,EmailAddress = '" & sqlSafe(txtEmail.Text) & "'"
                    sSQL = sSQL & " ,BirthPlace = '" & sqlSafe(txtBirthPlace.Text) & "'"
                    sSQL = sSQL & " ,Religion = '" & sqlSafe(txtReligion.Text) & "'"
                    sSQL = sSQL & " ,EmergencyContactPerson = '" & sqlSafe(txtPerson.Text) & "'"
                    sSQL = sSQL & " ,Address1 = '" & sqlSafe(txtPersonAddress.Text) & "'"
                    sSQL = sSQL & " ,PhoneNo1 = '" & sqlSafe(txtPersonPhoneNo.Text) & "'"
                    sSQL = sSQL & " ,MobileNo1 = '" & sqlSafe(txtPersonMobileNo.Text) & "'"
                    sSQL = sSQL & " ,EmailAddress1 = '" & sqlSafe(txtPersonEmail.Text) & "'"
                    sSQL = sSQL & " ,GuardianName = '" & sqlSafe(txtGuardianName.Text) & "'"
                    sSQL = sSQL & " ,GuardianAddress = '" & sqlSafe(txtGuardianAddress.Text) & "'"
                    sSQL = sSQL & " ,GuardianPhoneNo = '" & sqlSafe(txtGuardianPhoneNo.Text) & "'"
                    sSQL = sSQL & " ,GuardianMobileNo = '" & sqlSafe(txtGuardianMobileNo.Text) & "'"

                    sSQL = sSQL & " ,EmploymentTypeCode = '" & sqlSafe(dcEmploymentType.SelectedValue) & "'"
                    sSQL = sSQL & " ,CompanyCode = '" & sqlSafe(dcCompany.SelectedValue) & "'"
                    sSQL = sSQL & " ,DepartmentCode = " & toNumber(dcDepartment.SelectedValue)
                    sSQL = sSQL & " ,DivisionCode = " & toNumber(dcDivision.SelectedValue)
                    sSQL = sSQL & " ,UnitCode = " & toNumber(dcUnit.SelectedValue)
                    sSQL = sSQL & " ,JobLevelCode = '" & sqlSafe(dcJobLevel.SelectedValue) & "'"
                    sSQL = sSQL & " ,JobTitleCode = '" & sqlSafe(dcJobTitle.SelectedValue) & "'"

                    sSQL = sSQL & " ,SSSID = '" & sqlSafe(txtSSS.Text) & "'"
                    sSQL = sSQL & " ,TINID = '" & sqlSafe(txtTIN.Text) & "'"
                    sSQL = sSQL & " ,PhilHealth = '" & sqlSafe(txtPHIC.Text) & "'"
                    sSQL = sSQL & " ,HDMF = '" & sqlSafe(txtHDMF.Text) & "'"
                    sSQL = sSQL & " ,BankCode = '" & sqlSafe(dcBank.SelectedValue) & "'"
                    sSQL = sSQL & " ,BankAccountNo = '" & sqlSafe(txtAccountNo.Text) & "'"

                    sSQL = sSQL & " ,PayrollDistributionCode = '" & sqlSafe(dcPayrollMode.SelectedValue) & "'"
                    sSQL = sSQL & " ,MonthlyRate = " & toNumber(txtMonthlySalary.Text)
                    sSQL = sSQL & " ,DailyRate = " & toNumber(txtDaily.Text)
                    sSQL = sSQL & " ,RatePerHour = " & toNumber(txtRatePerHour.Text)
                    sSQL = sSQL & " ,PINCode = " & toNumber(txtDTRPIN.Text)

                    sSQL = sSQL & " ,LastDateModified = '" & CDate(GetServerDateTime()) & "'"
                    sSQL = sSQL & " ,ModifiedBy = '" & ACTIVE_USER.m_LoginName & "'"
                    sSQL = sSQL & " WHERE EmployeeCode = " & toNumber(PK)
                End If

                Call ExecuteSQL(sSQL)
                Call InsertEmployeeFamily()
                Call InsertEmployeeEducation()
                Call InsertEmployeeEmployment()

                frmWait.seconds = 2
                frmWait.ShowDialog()

                If State = FormState.AddStateMode Then
                    MsgBox("New record has been successfully saved!", MsgBoxStyle.Information)

                    Dim x As Integer = PIC_PROFILE.Width
                    Dim y As Integer = PIC_PROFILE.Height
                    Dim bm As New Bitmap(x, y)
                    PIC_PROFILE.DrawToBitmap(bm, New Rectangle(0, 0, x, y))
                    PIC_PROFILE.Image = bm

                    '// Save The Visible Part of PictureBox Only (.....Cropped.....)
                    SaveImage(PK & ".jpg", bm)

                    Call CreateLog(Name, "ADD NEW RECORD", "ID NUMBER:" & toNumber(PK))

                    If MsgBox("Do you want to add another record? (Y/N)", MsgBoxStyle.Question & vbYesNo) = vbYes Then
                        blnUpdateInProgress = False
                        CommandPass("Cancel")

                        CommandPass("New")
                    Else
                        blnUpdateInProgress = False
                        CommandPass("Cancel")
                    End If
                ElseIf State = FormState.EditStateMode Then
                    MsgBox("Information has been successfully updated!", MsgBoxStyle.Information)

                    Dim x As Integer = PIC_PROFILE.Width
                    Dim y As Integer = PIC_PROFILE.Height
                    Dim bm As New Bitmap(x, y)
                    PIC_PROFILE.DrawToBitmap(bm, New Rectangle(0, 0, x, y))
                    PIC_PROFILE.Image = bm

                    '// Save The Visible Part of PictureBox Only (.....Cropped.....)
                    SaveImage(PK & ".jpg", bm)

                    Call CreateLog(Name, "UPDATE RECORD", "ID NUMBER:" & toNumber(PK))

                    blnUpdateInProgress = False
                    CommandPass("Cancel")
                End If

            Case "Cancel"
                Call ClearAllText(Me)
                Call ClearAllText(Me, TabControl1.Controls)

                Call ClearAllCombo(Me)
                Call ClearAllCombo(Me, TabControl1.Controls)

                Call EnableMenu(True, False, False, False, True, True)
                dtDateHired.Value = CDate(GetServerDateTime())
                dtDOB.Value = CDate(GetServerDateTime())
                dtDOBF.Value = CDate(GetServerDateTime())
                dtEndDate.Value = CDate(GetServerDateTime())

                lvFamily.Items.Clear()
                lvEducation.Items.Clear()
                lvEmployment.Items.Clear()
                lvEmploymentType.Items.Clear()
                lvPromotion.Items.Clear()
                lvCostCenter.Items.Clear()

                State = FormState.ViewStateMode
                blnUpdateInProgress = False

                PK = 0
                PIC_PROFILE.Image = Nothing
                txtEmployeeNo.Focus()

            Case "Delete"
                If is_empty(txtEmployeeNo) = True Then Exit Sub
                If is_empty(txtLastName) = True Then Exit Sub

                If MsgBox("Are you sure you want to delete selected employee? Please note that all records related to this employee will be deleted also!", MsgBoxStyle.Critical + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Call DeleteSQL("tblMst_Employee", "EmployeeCode", True, "", PK)
                Else
                    Exit Sub
                End If

                MsgBox("Existing employee has been successfully deleted!", MsgBoxStyle.Critical)

                blnUpdateInProgress = False
                CommandPass("Cancel")

            Case "Print"
            Case "Close"
                Me.Dispose()
        End Select
        Exit Sub
ErrHandler: MsgBox(Err.Description, vbExclamation)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        CommandPass("New")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        CommandPass("Save")
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        CommandPass("Cancel")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        CommandPass("Delete")
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CommandPass("Close")
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        CommandPass("Print")
    End Sub


    Private Sub btnPayrollSetup_Click(sender As Object, e As EventArgs) Handles btnPayrollSetup.Click
        With frmEmployeeIDSetup
            .src_EmployeeNo = PK
            .ShowDialog()
        End With
    End Sub

    Private Sub btnAddFamily_Click(sender As Object, e As EventArgs) Handles btnAddFamily.Click
        If is_empty(txtFirstNameF) = True Then Exit Sub
        If is_empty(txtLastNameF) = True Then Exit Sub
        If is_empty(txtAddressF) = True Then Exit Sub

        Dim lv As ListViewItem = lvFamily.Items.Add(dcRelationship.SelectedValue, 0)
        lv.SubItems.Add(GetValueAt("SELECT * FROM tblMst_Relationship WHERE RelationshipCode=" & toNumber(dcRelationship.SelectedValue), "Description"))
        lv.SubItems.Add(txtLastNameF.Text)
        lv.SubItems.Add(txtFirstNameF.Text)
        lv.SubItems.Add(txtMiddleNameF.Text)
        lv.SubItems.Add(txtContactNoF.Text)
        lv.SubItems.Add(FormatDateTime(dtDOBF.Value, DateFormat.ShortDate))
        lv.SubItems.Add(txtAddressF.Text)
        lv.SubItems.Add(changeYNValue(CheckBox1.Checked))
        lv.SubItems.Add(changeYNValue(CheckBox2.Checked))
        lv.SubItems.Add(changeYNValue(CheckBox3.Checked))

        txtLastNameF.Text = ""
        txtFirstNameF.Text = ""
        txtMiddleNameF.Text = ""
        txtContactNoF.Text = ""
        txtAddressF.Text = ""

        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False

        dcRelationship.Focus()
    End Sub

    Private Sub InsertEmployeeFamily()
        Dim srcSQL As String

        Call DeleteSQL("tblMst_Employee_Family", "EmployeeCode", True, "", PK)

        For Each ls As ListViewItem In lvFamily.Items

            srcSQL = "INSERT INTO tblMst_Employee_Family"
            srcSQL = srcSQL & "(EmployeeCode"
            srcSQL = srcSQL & ", RelationshipCode"
            srcSQL = srcSQL & ", LastName"
            srcSQL = srcSQL & ", FirstName"
            srcSQL = srcSQL & ", MiddleName"
            srcSQL = srcSQL & ", ContactNo"
            srcSQL = srcSQL & ", BirthDate"
            srcSQL = srcSQL & ", Address"
            srcSQL = srcSQL & ", IsNextofKin"
            srcSQL = srcSQL & ", IsBeneficiary"
            srcSQL = srcSQL & ", IsDependent"
            srcSQL = srcSQL & ") VALUES ("
            srcSQL = srcSQL & " " & toNumber(PK)
            srcSQL = srcSQL & ", " & toNumber(Trim(ls.Text))
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(2).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(3).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(4).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(5).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(6).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(7).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(8).Text)) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(Trim(ls.SubItems(9).Text)) & "'"
            srcSQL = srcSQL & ", '" & sqlSafe(Trim(ls.SubItems(10).Text)) & "')"

            Call ExecuteSQL(srcSQL)
        Next
    End Sub

    Private Sub FillFamily(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("RelationshipCode").ToString(), 1)
            lv.SubItems.Add(Dtrdr.Item("Relationship").ToString())
            lv.SubItems.Add(Dtrdr.Item("LastName").ToString())
            lv.SubItems.Add(Dtrdr.Item("FirstName").ToString())
            lv.SubItems.Add(Dtrdr.Item("MiddleName").ToString())
            lv.SubItems.Add(Dtrdr.Item("ContactNo").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("BirthDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Address").ToString())
            lv.SubItems.Add(Dtrdr.Item("IsNextofKin").ToString())
            lv.SubItems.Add(Dtrdr.Item("IsBeneficiary").ToString())
            lv.SubItems.Add(Dtrdr.Item("IsDependent").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub lvFamily_DoubleClick(sender As Object, e As EventArgs) Handles lvFamily.DoubleClick
        If lvFamily.Items.Count = 0 Then Exit Sub

        If lvFamily.SelectedItems.Count > 0 Then
            lvFamily.Items.Remove(lvFamily.SelectedItems(0))
        Else
            MsgBox("Invalid selection. Please check it!", vbExclamation)
            Exit Sub
        End If
    End Sub

    Private Sub lvEducation_DoubleClick(sender As Object, e As EventArgs) Handles lvEducation.DoubleClick
        If lvEducation.Items.Count = 0 Then Exit Sub

        If lvEducation.SelectedItems.Count > 0 Then
            lvEducation.Items.Remove(lvEducation.SelectedItems(0))
        Else
            MsgBox("Invalid selection. Please check it!", vbExclamation)
            Exit Sub
        End If
    End Sub

    Private Sub FillEducation(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("DegreeCode").ToString(), 1)
            lv.SubItems.Add(Dtrdr.Item("Degree").ToString())
            lv.SubItems.Add(Dtrdr.Item("CourseCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("Course").ToString())
            lv.SubItems.Add(Dtrdr.Item("School").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("DateGraduated").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("YearFrom").ToString())
            lv.SubItems.Add(Dtrdr.Item("YearTo").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub InsertEmployeeEducation()
        Dim srcSQL As String

        Call DeleteSQL("tblMst_Employee_Education", "EmployeeCode", True, "", PK)

        For Each ls As ListViewItem In lvEducation.Items
            srcSQL = "INSERT INTO tblMst_Employee_Education"
            srcSQL = srcSQL & "(EmployeeCode"
            srcSQL = srcSQL & ", DegreeCode"
            srcSQL = srcSQL & ", CourseCode"
            srcSQL = srcSQL & ", School"
            srcSQL = srcSQL & ", DateGraduated"
            srcSQL = srcSQL & ", YearFrom"
            srcSQL = srcSQL & ", YearTo"
            srcSQL = srcSQL & ") VALUES ("
            srcSQL = srcSQL & " " & toNumber(PK)
            srcSQL = srcSQL & ", " & toNumber(ls.Text)
            srcSQL = srcSQL & ",  " & toNumber(ls.SubItems(2).Text)
            srcSQL = srcSQL & ",  '" & sqlSafe(ls.SubItems(4).Text) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(ls.SubItems(5).Text) & "'"
            srcSQL = srcSQL & ",  " & toNumber(ls.SubItems(6).Text)
            srcSQL = srcSQL & ", " & toNumber(ls.SubItems(7).Text) & ")"

            Call ExecuteSQL(srcSQL)
        Next
    End Sub

    Private Sub btnAddEducation_Click(sender As Object, e As EventArgs) Handles btnAddEducation.Click
        If is_empty(txtSchool) = True Then Exit Sub
        If is_empty(txtYF) = True Then Exit Sub
        If is_empty(txtYT) = True Then Exit Sub

        Dim lv As ListViewItem = lvEducation.Items.Add(dcDegree.SelectedValue, 0)
        lv.SubItems.Add(GetValueAt("SELECT * FROM tblMst_Degree WHERE DegreeCode=" & toNumber(dcDegree.SelectedValue), "Description"))
        lv.SubItems.Add(dcCourse.SelectedValue)
        lv.SubItems.Add(GetValueAt("SELECT * FROM tblMst_Course WHERE CourseCode=" & toNumber(dcCourse.SelectedValue), "Description"))
        lv.SubItems.Add(txtSchool.Text)
        lv.SubItems.Add(FormatDateTime(dtGraduated.Value, DateFormat.ShortDate))
        lv.SubItems.Add(txtYF.Text)
        lv.SubItems.Add(txtYT.Text)

        txtSchool.Text = ""
        txtYF.Text = ""
        txtYT.Text = ""

        dcDegree.Focus()
    End Sub

    Private Sub lvEmployment_DoubleClick(sender As Object, e As EventArgs) Handles lvEmployment.DoubleClick
        If lvEmployment.Items.Count = 0 Then Exit Sub

        If lvEmployment.SelectedItems.Count > 0 Then
            lvEmployment.Items.Remove(lvEmployment.SelectedItems(0))
        Else
            MsgBox("Invalid selection. Please check it!", vbExclamation)
            Exit Sub
        End If
    End Sub

    Private Sub lvEmployment_KeyDown(sender As Object, e As KeyEventArgs) Handles lvEmployment.KeyDown
        Try
            If e.KeyValue = 46 Then
                If lvEmployment.Items.Count = 0 Then Exit Sub
                lvEmployment.Items.Remove(lvEmployment.SelectedItems(0))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnAddEmployment_Click(sender As Object, e As EventArgs) Handles btnAddEmployment.Click
        If is_empty(txtPosition) = True Then Exit Sub
        If is_empty(txtEmployer) = True Then Exit Sub

        Dim lv As ListViewItem = lvEmployment.Items.Add(txtPosition.Text, 0)
        lv.SubItems.Add(txtEmployer.Text)
        lv.SubItems.Add(txtSalary.Text)
        lv.SubItems.Add(FormatDateTime(FromDT.Value, DateFormat.ShortDate))
        lv.SubItems.Add(FormatDateTime(ToDT.Value, DateFormat.ShortDate))
        lv.SubItems.Add(txtReason.Text)
        lv.SubItems.Add(txtRemarks.Text)

        txtPosition.Text = ""
        txtEmployer.Text = ""
        txtSalary.Text = ""
        FromDT.Value = CDate(GetServerDateTime())
        ToDT.Value = CDate(GetServerDateTime())
        txtReason.Text = ""
        txtRemarks.Text = ""

        txtPosition.Focus()
    End Sub

    Private Sub FillEmployment(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Mars

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add(Dtrdr.Item("Designation").ToString(), 1)
            lv.SubItems.Add(Dtrdr.Item("Employer").ToString())
            lv.SubItems.Add(toMoney(Dtrdr.Item("Salary").ToString()))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("StartDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("EndDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("LeavingReason").ToString())
            lv.SubItems.Add(Dtrdr.Item("Remarks").ToString())
        Loop
        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Mars:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub InsertEmployeeEmployment()
        Dim srcSQL As String

        Call DeleteSQL("tblMst_Employee_Employment", "EmployeeCode", True, "", PK)

        For Each ls As ListViewItem In lvEmployment.Items
            srcSQL = "INSERT INTO tblMst_Employee_Employment"
            srcSQL = srcSQL & "(EmployeeCode"
            srcSQL = srcSQL & ", Designation"
            srcSQL = srcSQL & ", Employer"
            srcSQL = srcSQL & ", Salary"
            srcSQL = srcSQL & ", StartDate"
            srcSQL = srcSQL & ", EndDate"
            srcSQL = srcSQL & ", LeavingReason"
            srcSQL = srcSQL & ", Remarks"
            srcSQL = srcSQL & ") VALUES ("
            srcSQL = srcSQL & " " & toNumber(PK)
            srcSQL = srcSQL & ", '" & sqlSafe(ls.Text) & "'"
            srcSQL = srcSQL & ",  '" & sqlSafe(ls.SubItems(1).Text) & "'"
            srcSQL = srcSQL & ",  " & toNumber(ls.SubItems(2).Text)
            srcSQL = srcSQL & ", ' " & sqlSafe(ls.SubItems(3).Text) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(ls.SubItems(4).Text) & "'"
            srcSQL = srcSQL & ", ' " & sqlSafe(ls.SubItems(5).Text) & "'"
            srcSQL = srcSQL & ", '" & sqlSafe(ls.SubItems(6).Text) & "')"

            Call ExecuteSQL(srcSQL)
        Next
    End Sub


    Private Sub btnAddEmploymentType_Click(sender As Object, e As EventArgs) Handles btnAddEmploymentType.Click
        If is_empty(txtEmploymentRemarks) = True Then Exit Sub

        If MsgBox("Are you sure you want to add changes on the employee's employment type?", MsgBoxStyle.Question + vbYesNo) = vbYes Then
            Call InsertEmployeeEmploymentTypeChanges()
            MsgBox("Employment Type changes has been successfully saved!", MsgBoxStyle.Information)

            Call FillEmploymentType(" SELECT " &
                                    " et.ChangeNo, " &
                                    " et.EmploymentTypeCode1, " &
                                    " t1.Description As [EmploymentType1], " &
                                    " et.EmploymentTypeCode2, " &
                                    " t2.Description As [EmploymentType2], " &
                                    " et.EffectivityDate, " &
                                    " et.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_EmploymentType et(nolock) " &
                                " LEFT OUTER JOIN tblMst_EmploymentType t1(nolock)On et.EmploymentTypeCode1 = t1.EmploymentTypeCode " &
                                " LEFT OUTER JOIN tblMst_EmploymentType t2(nolock)On et.EmploymentTypeCode2 = t2.EmploymentTypeCode " &
                                "WHERE " &
                                " et.EmployeeCode=" & toNumber(PK), lvEmploymentType)
        Else
            Exit Sub
        End If
    End Sub
    Private Sub InsertEmployeeEmploymentTypeChanges()
        Dim srcSQL As String

        srcSQL = "INSERT INTO tblMst_Employee_EmploymentType"
        srcSQL = srcSQL & "(ChangeNo"
        srcSQL = srcSQL & ", EmployeeCode"
        srcSQL = srcSQL & ", EmploymentTypeCode1"
        srcSQL = srcSQL & ", EmploymentTypeCode2"
        srcSQL = srcSQL & ", EffectivityDate"
        srcSQL = srcSQL & ", Remarks"
        srcSQL = srcSQL & ") VALUES ("
        srcSQL = srcSQL & " " & toNumber(GenerateNewIDNumber("tblMst_Employee_EmploymentType", "ChangeNo"))
        srcSQL = srcSQL & ", " & toNumber(PK)
        srcSQL = srcSQL & ", '" & sqlSafe(dcEmploymentType1.SelectedValue) & "'"
        srcSQL = srcSQL & ", '" & sqlSafe(dcEmploymentType2.SelectedValue) & "'"
        srcSQL = srcSQL & ", ' " & CDate(dtED1.Value) & "'"
        srcSQL = srcSQL & ", '" & sqlSafe(txtEmploymentRemarks.Text) & "')"

        Call ExecuteSQL(srcSQL)
    End Sub
    Private Sub FillEmploymentType(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Hell

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add("", 9)
            lv.SubItems.Add(Dtrdr.Item("ChangeNo").ToString())
            lv.SubItems.Add(Dtrdr.Item("EmploymentType1").ToString())
            lv.SubItems.Add(Dtrdr.Item("EmploymentType2").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("EffectivityDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Remarks").ToString())
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub lvEmploymentType_KeyDown(sender As Object, e As KeyEventArgs) Handles lvEmploymentType.KeyDown
        Dim ANS As MsgBoxResult
        Try
            If e.KeyValue = 46 Then
                If lvEmploymentType.Items.Count = 0 Then Exit Sub
                ANS = MsgBox("Are you sure you want to delete selected employment changes?", vbQuestion + vbYesNo)

                If ANS = vbYes Then
                    Call DeleteSQL("tblMst_Employee_EmploymentType", "ChangeNo", True, "", toNumber(lvEmploymentType.SelectedItems(0).SubItems(1).Text))
                    MsgBox("Selected record has been successfully deleted!", MsgBoxStyle.Critical)

                    Call FillEmploymentType(" SELECT " &
                                    " et.ChangeNo, " &
                                    " et.EmploymentTypeCode1, " &
                                    " t1.Description As [EmploymentType1], " &
                                    " et.EmploymentTypeCode2, " &
                                    " t2.Description As [EmploymentType2], " &
                                    " et.EffectivityDate, " &
                                    " et.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_EmploymentType et(nolock) " &
                                " LEFT OUTER JOIN tblMst_EmploymentType t1(nolock)On et.EmploymentTypeCode1 = t1.EmploymentTypeCode " &
                                " LEFT OUTER JOIN tblMst_EmploymentType t2(nolock)On et.EmploymentTypeCode2 = t2.EmploymentTypeCode " &
                                "WHERE " &
                                " et.EmployeeCode=" & toNumber(PK), lvEmploymentType)
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub InsertEmployeePromotionChanges()
        Dim srcSQL As String

        srcSQL = "INSERT INTO tblMst_Employee_Promotion"
        srcSQL = srcSQL & "(PromotionCode"
        srcSQL = srcSQL & ", EmployeeCode"
        srcSQL = srcSQL & ", JobTitleCode1"
        srcSQL = srcSQL & ", JobTitleCode2"
        srcSQL = srcSQL & ", Salary1"
        srcSQL = srcSQL & ", Salary2"
        srcSQL = srcSQL & ", EffectivityDate"
        srcSQL = srcSQL & ", Remarks"
        srcSQL = srcSQL & ") VALUES ("
        srcSQL = srcSQL & " " & toNumber(GenerateNewIDNumber("tblMst_Employee_Promotion", "PromotionCode"))
        srcSQL = srcSQL & ", " & toNumber(PK)
        srcSQL = srcSQL & ", '" & sqlSafe(dcJobTitle1.SelectedValue) & "'"
        srcSQL = srcSQL & ", '" & sqlSafe(dcJobTitle2.SelectedValue) & "'"
        srcSQL = srcSQL & ",  " & toNumber(txtS1.Text)
        srcSQL = srcSQL & ",  " & toNumber(txtS2.Text)
        srcSQL = srcSQL & ", ' " & CDate(dtED1.Value) & "'"
        srcSQL = srcSQL & ", '" & sqlSafe(txtPromotionRemarks.Text) & "')"

        Call ExecuteSQL(srcSQL)
    End Sub
    Private Sub FillEmployeePromotion(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Hell

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add("", 9)
            lv.SubItems.Add(Dtrdr.Item("PromotionCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("JobTitle1").ToString())
            lv.SubItems.Add(Dtrdr.Item("JobTitle2").ToString())
            lv.SubItems.Add(toMoney(Dtrdr.Item("Salary1").ToString()))
            lv.SubItems.Add(toMoney(Dtrdr.Item("Salary2").ToString()))
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("EffectivityDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Remarks").ToString())
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub
    Private Sub btnAddPromotions_Click(sender As Object, e As EventArgs) Handles btnAddPromotions.Click
        If is_empty(txtPromotionRemarks) = True Then Exit Sub

        If MsgBox("Are you sure you want to add changes on the employee's promotion?", MsgBoxStyle.Question + vbYesNo) = vbYes Then
            Call InsertEmployeePromotionChanges()
            MsgBox("Promotion changes has been successfully saved!", MsgBoxStyle.Information)

            Call FillEmployeePromotion(" SELECT " &
                                    " p.PromotionCode, " &
                                    " p.JobTitleCode1, " &
                                    " j.Description As [JobTitle1], " &
                                    " p.JobTitleCode2, " &
                                    " t.Description As [JobTitle2], " &
                                    " p.Salary1, " &
                                    " p.Salary2, " &
                                    " p.EffectivityDate, " &
                                    " p.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_Promotion p(nolock) " &
                                " LEFT OUTER JOIN tblMst_JobTitle j(nolock)ON p.JobTitleCode1 = j.JobTitleCode " &
                                " LEFT OUTER JOIN tblMst_JobTitle t(nolock)ON p.JobTitleCode2 = t.JobTitleCode " &
                                "WHERE " &
                                " p.EmployeeCode=" & toNumber(PK), lvPromotion)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub lvPromotion_KeyDown(sender As Object, e As KeyEventArgs) Handles lvPromotion.KeyDown
        Dim ANS As MsgBoxResult
        Try
            If e.KeyValue = 46 Then
                If lvPromotion.Items.Count = 0 Then Exit Sub
                ANS = MsgBox("Are you sure you want to delete selected promotion?", vbQuestion + vbYesNo)

                If ANS = vbYes Then
                    Call DeleteSQL("tblMst_Employee_Promotion", "PromotionCode", True, "", toNumber(lvPromotion.SelectedItems(0).SubItems(1).Text))
                    MsgBox("Selected record has been successfully deleted!", MsgBoxStyle.Critical)

                    Call FillEmployeePromotion(" SELECT " &
                                    " p.PromotionCode, " &
                                    " p.JobTitleCode1, " &
                                    " j.Description As [JobTitle1], " &
                                    " p.JobTitleCode2, " &
                                    " t.Description As [JobTitle2], " &
                                    " p.Salary1, " &
                                    " p.Salary2, " &
                                    " p.EffectivityDate, " &
                                    " p.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_Promotion p(nolock) " &
                                " LEFT OUTER JOIN tblMst_JobTitle j(nolock)ON p.JobTitleCode1 = j.JobTitleCode " &
                                " LEFT OUTER JOIN tblMst_JobTitle t(nolock)ON p.JobTitleCode2 = t.JobTitleCode " &
                                "WHERE " &
                                " p.EmployeeCode=" & toNumber(PK), lvPromotion)
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub InsertEmployeeCostCenterChanges()
        Dim srcSQL As String

        srcSQL = "INSERT INTO tblMst_Employee_Division"
        srcSQL = srcSQL & "(ChangeCode"
        srcSQL = srcSQL & ", EmployeeCode"
        srcSQL = srcSQL & ", DivisionCode1"
        srcSQL = srcSQL & ", DivisionCode2"
        srcSQL = srcSQL & ", EffectivityDate"
        srcSQL = srcSQL & ", Remarks"
        srcSQL = srcSQL & ") VALUES ("
        srcSQL = srcSQL & " " & toNumber(GenerateNewIDNumber("tblMst_Employee_Division", "ChangeCode"))
        srcSQL = srcSQL & ", " & toNumber(PK)
        srcSQL = srcSQL & ",  " & toNumber(dcCostCenter1.SelectedValue)
        srcSQL = srcSQL & ",  " & toNumber(dcCostCenter2.SelectedValue)
        srcSQL = srcSQL & ", ' " & CDate(dtED1.Value) & "'"
        srcSQL = srcSQL & ", '" & sqlSafe(txtCostCenterRemarks.Text) & "')"

        Call ExecuteSQL(srcSQL)
    End Sub

    Private Sub FillEmployeeCostCenter(ByVal sSQL As String, ByRef srcListview As ListView)
        On Error GoTo Hell

        If MyConn.State = ConnectionState.Open Then MyConn.Close()
        Dim SQL As New SqlCommand(sSQL, MyConn)
        MyConn.Open()
        Dim Dtrdr As SqlDataReader = SQL.ExecuteReader()

        srcListview.GridLines = True
        srcListview.Items.Clear()

        Do While Dtrdr.Read()
            Dim lv As ListViewItem = srcListview.Items.Add("", 9)
            lv.SubItems.Add(Dtrdr.Item("ChangeCode").ToString())
            lv.SubItems.Add(Dtrdr.Item("CostCenter1").ToString())
            lv.SubItems.Add(Dtrdr.Item("CostCenter2").ToString())
            lv.SubItems.Add(FormatDateTime(Dtrdr.Item("EffectivityDate").ToString(), DateFormat.ShortDate))
            lv.SubItems.Add(Dtrdr.Item("Remarks").ToString())
        Loop

        Dtrdr.Close()
        SQL.Dispose()
        MyConn.Close()
        Exit Sub
Hell:   MsgBox(Err.Description, MsgBoxStyle.Critical)
    End Sub

    Private Sub btnAddCostCenter_Click(sender As Object, e As EventArgs) Handles btnAddCostCenter.Click
        If is_empty(txtCostCenterRemarks) = True Then Exit Sub

        If MsgBox("Are you sure you want to add changes on the employee's cost center?", MsgBoxStyle.Question + vbYesNo) = vbYes Then
            Call InsertEmployeeCostCenterChanges()
            MsgBox("Cost center changes has been successfully saved!", MsgBoxStyle.Information)

            Call FillEmployeeCostCenter(" SELECT " &
                                    " ec.ChangeCode, " &
                                    " c.ShortName AS [CostCenter1], " &
                                    " c2.ShortName As [CostCenter2], " &
                                    " ec.EffectivityDate, " &
                                    " ec.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_Division ec(nolock) " &
                                " LEFT OUTER JOIN tblMst_Division c(nolock)ON ec.DivisionCode1 = c.DivisionCode " &
                                " LEFT OUTER JOIN tblMst_Division c2(nolock)ON ec.DivisionCode2 = c2.DivisionCode " &
                                "WHERE " &
                                " ec.EmployeeCode=" & toNumber(PK), lvCostCenter)
        Else
            Exit Sub
        End If
    End Sub

    Private Sub lvCostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles lvCostCenter.KeyDown
        Dim ANS As MsgBoxResult
        Try
            If e.KeyValue = 46 Then
                If lvCostCenter.Items.Count = 0 Then Exit Sub
                ANS = MsgBox("Are you sure you want to delete selected cost center history?", vbQuestion + vbYesNo)

                If ANS = vbYes Then
                    Call DeleteSQL("tblMst_Employee_Division", "ChangeCode", True, "", toNumber(lvCostCenter.SelectedItems(0).SubItems(1).Text))
                    MsgBox("Selected record has been successfully deleted!", MsgBoxStyle.Critical)

                    Call FillEmployeeCostCenter(" SELECT " &
                                    " ec.ChangeCode, " &
                                    " c.ShortName AS [CostCenter1], " &
                                    " c2.ShortName As [CostCenter2], " &
                                    " ec.EffectivityDate, " &
                                    " ec.Remarks " &
                                " FROM " &
                                    " tblMst_Employee_Division ec(nolock) " &
                                " LEFT OUTER JOIN tblMst_Division c(nolock)ON ec.DivisionCode1 = c.DivisionCode " &
                                " LEFT OUTER JOIN tblMst_Division c2(nolock)ON ec.DivisionCode2 = c2.DivisionCode " &
                                "WHERE " &
                                " ec.EmployeeCode=" & toNumber(PK), lvCostCenter)
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            With OFP
                .DefaultExt = "png"
                .InitialDirectory = My.Application.Info.DirectoryPath

                .Filter = "PNG Files(*.png)|*.png|JPEG Files(*.jpeg)|*.jpg" &
                            "|GIF Files(*.gif)|*.gif|Bitmap Files(*.bmp)|*.bmp|All Supported Files|*.jpeg;*.jpg;*.gif;*.bmp;*.img"
                .ShowDialog()

                If .FileName <> "" Then
                    PIC_PROFILE.Image = Image.FromFile(.FileName)
                    dataBytes = My.Computer.FileSystem.ReadAllBytes(.FileName)
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub SaveImage(filename As String, image As Image)
        Dim path As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath & "\Profiles\Employees\" & filename & ".jpg")
        Dim mySource As New Bitmap(image.Width, image.Height)
        Dim grfx As Graphics = Graphics.FromImage(mySource)
        grfx.DrawImageUnscaled(image, Point.Empty)
        grfx.Dispose()
        mySource.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg)
        mySource.Dispose()
    End Sub

End Class