<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeIDSetup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.LAST_USER = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLastUser = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LAST_DATE_MODIFIED = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLastDateModified = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtJobTitle = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEmployeeName = New System.Windows.Forms.TextBox()
        Me.txtEmployeeNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSalary = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPayrollDistribution = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtIncomeAmount = New System.Windows.Forms.TextBox()
        Me.txtIncomeUnit = New System.Windows.Forms.TextBox()
        Me.dcIncome = New System.Windows.Forms.ComboBox()
        Me.lvIncome = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.btnAddIncome = New System.Windows.Forms.Button()
        Me.btnAddDeduction = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lvDeduction = New System.Windows.Forms.ListView()
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtDeductionAmount = New System.Windows.Forms.TextBox()
        Me.txtDeductionUnit = New System.Windows.Forms.TextBox()
        Me.dcDeduction = New System.Windows.Forms.ComboBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.txtEmployerShare = New System.Windows.Forms.TextBox()
        Me.txtTotalDeduction = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtTotalIncome = New System.Windows.Forms.TextBox()
        Me.ToolStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.ToolStripSeparator1, Me.btnPrint, Me.ToolStripSeparator2, Me.btnClose})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(879, 25)
        Me.ToolStrip.TabIndex = 71
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SAIS6._0.My.Resources.Resources.update
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 22)
        Me.btnSave.Text = "&Save Changes"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SAIS6._0.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnClose
        '
        Me.btnClose.Image = Global.SAIS6._0.My.Resources.Resources.quit
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(56, 22)
        Me.btnClose.Text = "Close"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LAST_USER, Me.lblLastUser, Me.LAST_DATE_MODIFIED, Me.lblLastDateModified})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 559)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(879, 22)
        Me.StatusStrip1.TabIndex = 105
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'LAST_USER
        '
        Me.LAST_USER.Image = Global.SAIS6._0.My.Resources.Resources.active_user
        Me.LAST_USER.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LAST_USER.Name = "LAST_USER"
        Me.LAST_USER.Size = New System.Drawing.Size(736, 17)
        Me.LAST_USER.Spring = True
        Me.LAST_USER.Text = "Last Updated By :"
        Me.LAST_USER.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'lblLastUser
        '
        Me.lblLastUser.Name = "lblLastUser"
        Me.lblLastUser.Size = New System.Drawing.Size(0, 17)
        '
        'LAST_DATE_MODIFIED
        '
        Me.LAST_DATE_MODIFIED.Image = Global.SAIS6._0.My.Resources.Resources.calendar
        Me.LAST_DATE_MODIFIED.Name = "LAST_DATE_MODIFIED"
        Me.LAST_DATE_MODIFIED.Size = New System.Drawing.Size(128, 17)
        Me.LAST_DATE_MODIFIED.Text = "Last Date Modified :"
        '
        'lblLastDateModified
        '
        Me.lblLastDateModified.Name = "lblLastDateModified"
        Me.lblLastDateModified.Size = New System.Drawing.Size(0, 17)
        '
        'txtJobTitle
        '
        Me.txtJobTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJobTitle.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJobTitle.Location = New System.Drawing.Point(388, 54)
        Me.txtJobTitle.Name = "txtJobTitle"
        Me.txtJobTitle.Size = New System.Drawing.Size(160, 22)
        Me.txtJobTitle.TabIndex = 113
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(301, 56)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(62, 14)
        Me.Label41.TabIndex = 112
        Me.Label41.Text = "Job Title :"
        '
        'txtDepartment
        '
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.Location = New System.Drawing.Point(388, 30)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(160, 22)
        Me.txtDepartment.TabIndex = 111
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(301, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 14)
        Me.Label4.TabIndex = 110
        Me.Label4.Text = "Department :"
        '
        'txtEmployeeName
        '
        Me.txtEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployeeName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeName.Location = New System.Drawing.Point(114, 54)
        Me.txtEmployeeName.Name = "txtEmployeeName"
        Me.txtEmployeeName.Size = New System.Drawing.Size(153, 22)
        Me.txtEmployeeName.TabIndex = 109
        '
        'txtEmployeeNo
        '
        Me.txtEmployeeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployeeNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeeNo.Location = New System.Drawing.Point(114, 28)
        Me.txtEmployeeNo.Name = "txtEmployeeNo"
        Me.txtEmployeeNo.ReadOnly = True
        Me.txtEmployeeNo.Size = New System.Drawing.Size(153, 22)
        Me.txtEmployeeNo.TabIndex = 108
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 107
        Me.Label3.Text = "Full Name :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 14)
        Me.Label2.TabIndex = 106
        Me.Label2.Text = "Employee No."
        '
        'txtSalary
        '
        Me.txtSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalary.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalary.Location = New System.Drawing.Point(708, 54)
        Me.txtSalary.Name = "txtSalary"
        Me.txtSalary.Size = New System.Drawing.Size(160, 22)
        Me.txtSalary.TabIndex = 117
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(591, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 14)
        Me.Label1.TabIndex = 116
        Me.Label1.Text = "Monthly Salary :"
        '
        'txtPayrollDistribution
        '
        Me.txtPayrollDistribution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayrollDistribution.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayrollDistribution.Location = New System.Drawing.Point(708, 30)
        Me.txtPayrollDistribution.Name = "txtPayrollDistribution"
        Me.txtPayrollDistribution.Size = New System.Drawing.Size(160, 22)
        Me.txtPayrollDistribution.TabIndex = 115
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(591, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(114, 14)
        Me.Label5.TabIndex = 114
        Me.Label5.Text = "Payroll Distribution :"
        '
        'txtIncomeAmount
        '
        Me.txtIncomeAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncomeAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncomeAmount.Location = New System.Drawing.Point(680, 114)
        Me.txtIncomeAmount.Name = "txtIncomeAmount"
        Me.txtIncomeAmount.Size = New System.Drawing.Size(102, 22)
        Me.txtIncomeAmount.TabIndex = 123
        '
        'txtIncomeUnit
        '
        Me.txtIncomeUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncomeUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncomeUnit.Location = New System.Drawing.Point(623, 114)
        Me.txtIncomeUnit.Name = "txtIncomeUnit"
        Me.txtIncomeUnit.Size = New System.Drawing.Size(55, 22)
        Me.txtIncomeUnit.TabIndex = 121
        '
        'dcIncome
        '
        Me.dcIncome.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dcIncome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dcIncome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dcIncome.FormattingEnabled = True
        Me.dcIncome.Location = New System.Drawing.Point(7, 114)
        Me.dcIncome.Name = "dcIncome"
        Me.dcIncome.Size = New System.Drawing.Size(611, 22)
        Me.dcIncome.TabIndex = 119
        '
        'lvIncome
        '
        Me.lvIncome.BackColor = System.Drawing.Color.White
        Me.lvIncome.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader15, Me.ColumnHeader16})
        Me.lvIncome.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvIncome.FullRowSelect = True
        Me.lvIncome.Location = New System.Drawing.Point(6, 140)
        Me.lvIncome.Name = "lvIncome"
        Me.lvIncome.Size = New System.Drawing.Size(867, 138)
        Me.lvIncome.TabIndex = 124
        Me.lvIncome.UseCompatibleStateImageBehavior = False
        Me.lvIncome.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "#"
        Me.ColumnHeader12.Width = 21
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Particular ID"
        Me.ColumnHeader13.Width = 81
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Description"
        Me.ColumnHeader14.Width = 513
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "Unit"
        Me.ColumnHeader15.Width = 57
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "Amount"
        Me.ColumnHeader16.Width = 197
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label46)
        Me.Panel3.Controls.Add(Me.Label48)
        Me.Panel3.Controls.Add(Me.Label49)
        Me.Panel3.Location = New System.Drawing.Point(6, 88)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(865, 22)
        Me.Panel3.TabIndex = 125
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.Location = New System.Drawing.Point(3, 3)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(86, 14)
        Me.Label46.TabIndex = 125
        Me.Label46.Text = "Select Income"
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.Location = New System.Drawing.Point(671, 3)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(51, 14)
        Me.Label48.TabIndex = 124
        Me.Label48.Text = "Amount"
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.Location = New System.Drawing.Point(614, 3)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(33, 14)
        Me.Label49.TabIndex = 123
        Me.Label49.Text = "Unit "
        '
        'btnAddIncome
        '
        Me.btnAddIncome.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddIncome.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddIncome.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddIncome.Location = New System.Drawing.Point(785, 114)
        Me.btnAddIncome.Name = "btnAddIncome"
        Me.btnAddIncome.Size = New System.Drawing.Size(86, 22)
        Me.btnAddIncome.TabIndex = 126
        Me.btnAddIncome.Text = "&Add To List"
        Me.btnAddIncome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddIncome.UseVisualStyleBackColor = True
        '
        'btnAddDeduction
        '
        Me.btnAddDeduction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddDeduction.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAddDeduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddDeduction.Location = New System.Drawing.Point(786, 337)
        Me.btnAddDeduction.Name = "btnAddDeduction"
        Me.btnAddDeduction.Size = New System.Drawing.Size(86, 22)
        Me.btnAddDeduction.TabIndex = 132
        Me.btnAddDeduction.Text = "&Add To List"
        Me.btnAddDeduction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnAddDeduction.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Location = New System.Drawing.Point(6, 311)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(867, 22)
        Me.Panel1.TabIndex = 131
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 14)
        Me.Label6.TabIndex = 125
        Me.Label6.Text = "Select Deduction"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(591, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(51, 14)
        Me.Label7.TabIndex = 124
        Me.Label7.Text = "Amount"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(680, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 14)
        Me.Label9.TabIndex = 133
        Me.Label9.Text = "Employer Share"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(526, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 14)
        Me.Label8.TabIndex = 123
        Me.Label8.Text = "Unit "
        '
        'lvDeduction
        '
        Me.lvDeduction.BackColor = System.Drawing.Color.White
        Me.lvDeduction.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader11, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9})
        Me.lvDeduction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDeduction.FullRowSelect = True
        Me.lvDeduction.Location = New System.Drawing.Point(6, 363)
        Me.lvDeduction.Name = "lvDeduction"
        Me.lvDeduction.Size = New System.Drawing.Size(867, 168)
        Me.lvDeduction.TabIndex = 130
        Me.lvDeduction.UseCompatibleStateImageBehavior = False
        Me.lvDeduction.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "#"
        Me.ColumnHeader11.Width = 22
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Particular ID"
        Me.ColumnHeader5.Width = 82
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Description"
        Me.ColumnHeader6.Width = 410
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Unit"
        Me.ColumnHeader7.Width = 56
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Amount"
        Me.ColumnHeader8.Width = 107
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Employer Share"
        Me.ColumnHeader9.Width = 180
        '
        'txtDeductionAmount
        '
        Me.txtDeductionAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeductionAmount.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeductionAmount.Location = New System.Drawing.Point(579, 337)
        Me.txtDeductionAmount.Name = "txtDeductionAmount"
        Me.txtDeductionAmount.Size = New System.Drawing.Size(102, 22)
        Me.txtDeductionAmount.TabIndex = 129
        '
        'txtDeductionUnit
        '
        Me.txtDeductionUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeductionUnit.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeductionUnit.Location = New System.Drawing.Point(522, 337)
        Me.txtDeductionUnit.Name = "txtDeductionUnit"
        Me.txtDeductionUnit.Size = New System.Drawing.Size(55, 22)
        Me.txtDeductionUnit.TabIndex = 128
        '
        'dcDeduction
        '
        Me.dcDeduction.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dcDeduction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.dcDeduction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dcDeduction.FormattingEnabled = True
        Me.dcDeduction.Location = New System.Drawing.Point(6, 337)
        Me.dcDeduction.Name = "dcDeduction"
        Me.dcDeduction.Size = New System.Drawing.Size(510, 22)
        Me.dcDeduction.TabIndex = 127
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(571, 645)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(102, 22)
        Me.TextBox3.TabIndex = 134
        '
        'txtEmployerShare
        '
        Me.txtEmployerShare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmployerShare.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployerShare.Location = New System.Drawing.Point(685, 337)
        Me.txtEmployerShare.Name = "txtEmployerShare"
        Me.txtEmployerShare.Size = New System.Drawing.Size(97, 22)
        Me.txtEmployerShare.TabIndex = 135
        '
        'txtTotalDeduction
        '
        Me.txtTotalDeduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalDeduction.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalDeduction.Location = New System.Drawing.Point(721, 534)
        Me.txtTotalDeduction.Name = "txtTotalDeduction"
        Me.txtTotalDeduction.Size = New System.Drawing.Size(152, 22)
        Me.txtTotalDeduction.TabIndex = 137
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(613, 286)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 14)
        Me.Label10.TabIndex = 136
        Me.Label10.Text = "Total Income :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(605, 538)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(113, 14)
        Me.Label11.TabIndex = 138
        Me.Label11.Text = "Total Deduction :"
        '
        'txtTotalIncome
        '
        Me.txtTotalIncome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalIncome.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalIncome.Location = New System.Drawing.Point(711, 283)
        Me.txtTotalIncome.Name = "txtTotalIncome"
        Me.txtTotalIncome.Size = New System.Drawing.Size(160, 22)
        Me.txtTotalIncome.TabIndex = 139
        '
        'frmEmployeeIDSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(879, 581)
        Me.Controls.Add(Me.txtTotalIncome)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtTotalDeduction)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtEmployerShare)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.btnAddDeduction)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lvDeduction)
        Me.Controls.Add(Me.txtDeductionAmount)
        Me.Controls.Add(Me.txtDeductionUnit)
        Me.Controls.Add(Me.dcDeduction)
        Me.Controls.Add(Me.btnAddIncome)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.lvIncome)
        Me.Controls.Add(Me.txtIncomeAmount)
        Me.Controls.Add(Me.txtIncomeUnit)
        Me.Controls.Add(Me.dcIncome)
        Me.Controls.Add(Me.txtSalary)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtPayrollDistribution)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtJobTitle)
        Me.Controls.Add(Me.Label41)
        Me.Controls.Add(Me.txtDepartment)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEmployeeName)
        Me.Controls.Add(Me.txtEmployeeNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.ToolStrip)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmployeeIDSetup"
        Me.Text = "Employee Income / Deduction"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents btnSave As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents btnPrint As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnClose As ToolStripButton
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents LAST_USER As ToolStripStatusLabel
    Friend WithEvents lblLastUser As ToolStripStatusLabel
    Friend WithEvents LAST_DATE_MODIFIED As ToolStripStatusLabel
    Friend WithEvents lblLastDateModified As ToolStripStatusLabel
    Friend WithEvents txtJobTitle As TextBox
    Friend WithEvents Label41 As Label
    Friend WithEvents txtDepartment As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtEmployeeName As TextBox
    Friend WithEvents txtEmployeeNo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtSalary As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtPayrollDistribution As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents txtIncomeAmount As TextBox
    Friend WithEvents txtIncomeUnit As TextBox
    Friend WithEvents dcIncome As ComboBox
    Friend WithEvents lvIncome As ListView
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label46 As Label
    Friend WithEvents Label48 As Label
    Friend WithEvents Label49 As Label
    Friend WithEvents btnAddIncome As Button
    Friend WithEvents btnAddDeduction As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lvDeduction As ListView
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents txtDeductionAmount As TextBox
    Friend WithEvents txtDeductionUnit As TextBox
    Friend WithEvents dcDeduction As ComboBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents txtEmployerShare As TextBox
    Friend WithEvents txtTotalDeduction As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents txtTotalIncome As TextBox
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents ColumnHeader13 As ColumnHeader
    Friend WithEvents ColumnHeader14 As ColumnHeader
    Friend WithEvents ColumnHeader15 As ColumnHeader
    Friend WithEvents ColumnHeader16 As ColumnHeader
End Class
