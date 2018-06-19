<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptEmployeeAlphalist
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
        Me.CRViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ttxCompany = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.lvCompany = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnPreview = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReload = New System.Windows.Forms.ToolStripButton()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.btnFind1 = New System.Windows.Forms.Button()
        Me.ttxStatus = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnFind2 = New System.Windows.Forms.Button()
        Me.ttxUnit = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnRefresh2 = New System.Windows.Forms.Button()
        Me.btnSelect2 = New System.Windows.Forms.Button()
        Me.lvUnit = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnRefresh1 = New System.Windows.Forms.Button()
        Me.btnSelect1 = New System.Windows.Forms.Button()
        Me.lvStatus = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Panel3.SuspendLayout()
        Me.Panel.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CRViewer
        '
        Me.CRViewer.ActiveViewIndex = -1
        Me.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer.DisplayBackgroundEdge = False
        Me.CRViewer.DisplayGroupTree = False
        Me.CRViewer.Location = New System.Drawing.Point(6, 80)
        Me.CRViewer.Name = "CRViewer"
        Me.CRViewer.SelectionFormula = ""
        Me.CRViewer.ShowGroupTreeButton = False
        Me.CRViewer.ShowRefreshButton = False
        Me.CRViewer.Size = New System.Drawing.Size(88, 382)
        Me.CRViewer.TabIndex = 1
        Me.CRViewer.ViewTimeSelectionFormula = ""
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Location = New System.Drawing.Point(6, 28)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(785, 22)
        Me.Panel3.TabIndex = 69
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(96, 15)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Filtering Options"
        '
        'ttxCompany
        '
        Me.ttxCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ttxCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ttxCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ttxCompany.Location = New System.Drawing.Point(99, 55)
        Me.ttxCompany.Name = "ttxCompany"
        Me.ttxCompany.Size = New System.Drawing.Size(163, 21)
        Me.ttxCompany.TabIndex = 71
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 15)
        Me.Label1.TabIndex = 70
        Me.Label1.Text = "By Company :"
        '
        'Panel
        '
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel.Controls.Add(Me.txtCompany)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.btnRefresh)
        Me.Panel.Controls.Add(Me.btnSelect)
        Me.Panel.Controls.Add(Me.lvCompany)
        Me.Panel.Location = New System.Drawing.Point(100, 80)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(526, 275)
        Me.Panel.TabIndex = 73
        Me.Panel.Visible = False
        '
        'txtCompany
        '
        Me.txtCompany.BackColor = System.Drawing.Color.White
        Me.txtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.Location = New System.Drawing.Point(100, 245)
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Size = New System.Drawing.Size(265, 21)
        Me.txtCompany.TabIndex = 73
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 247)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 15)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Quick Search :"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SAIS6._0.My.Resources.Resources.refresh
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(449, 243)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(70, 24)
        Me.btnRefresh.TabIndex = 36
        Me.btnRefresh.Text = "Clear"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnSelect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelect.Location = New System.Drawing.Point(370, 243)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(77, 24)
        Me.btnSelect.TabIndex = 35
        Me.btnSelect.Text = "&Select"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'lvCompany
        '
        Me.lvCompany.CheckBoxes = True
        Me.lvCompany.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lvCompany.FullRowSelect = True
        Me.lvCompany.Location = New System.Drawing.Point(5, 4)
        Me.lvCompany.Name = "lvCompany"
        Me.lvCompany.Size = New System.Drawing.Size(514, 233)
        Me.lvCompany.TabIndex = 0
        Me.lvCompany.UseCompatibleStateImageBehavior = False
        Me.lvCompany.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Company Code"
        Me.ColumnHeader1.Width = 91
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Company Name"
        Me.ColumnHeader2.Width = 425
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPreview, Me.ToolStripSeparator2, Me.btnPrint, Me.ToolStripSeparator3, Me.btnReload, Me.btnClose})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1221, 25)
        Me.ToolStrip.TabIndex = 74
        '
        'btnPreview
        '
        Me.btnPreview.Image = Global.SAIS6._0.My.Resources.Resources.PrintPreview
        Me.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(68, 22)
        Me.btnPreview.Text = "&Preview"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SAIS6._0.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnReload
        '
        Me.btnReload.Image = Global.SAIS6._0.My.Resources.Resources.cancel
        Me.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReload.Name = "btnReload"
        Me.btnReload.Size = New System.Drawing.Size(66, 22)
        Me.btnReload.Text = "&Refresh"
        '
        'btnClose
        '
        Me.btnClose.Image = Global.SAIS6._0.My.Resources.Resources.quit
        Me.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(56, 22)
        Me.btnClose.Text = "Close"
        '
        'btnFind
        '
        Me.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFind.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind.Location = New System.Drawing.Point(264, 55)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(25, 21)
        Me.btnFind.TabIndex = 72
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'btnFind1
        '
        Me.btnFind1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFind1.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind1.Location = New System.Drawing.Point(523, 56)
        Me.btnFind1.Name = "btnFind1"
        Me.btnFind1.Size = New System.Drawing.Size(25, 21)
        Me.btnFind1.TabIndex = 77
        Me.btnFind1.UseVisualStyleBackColor = True
        '
        'ttxStatus
        '
        Me.ttxStatus.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ttxStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ttxStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ttxStatus.Location = New System.Drawing.Point(358, 56)
        Me.ttxStatus.Name = "ttxStatus"
        Me.ttxStatus.Size = New System.Drawing.Size(163, 21)
        Me.ttxStatus.TabIndex = 76
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 15)
        Me.Label3.TabIndex = 75
        Me.Label3.Text = "By Status :"
        '
        'btnFind2
        '
        Me.btnFind2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFind2.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind2.Location = New System.Drawing.Point(806, 57)
        Me.btnFind2.Name = "btnFind2"
        Me.btnFind2.Size = New System.Drawing.Size(25, 21)
        Me.btnFind2.TabIndex = 80
        Me.btnFind2.UseVisualStyleBackColor = True
        '
        'ttxUnit
        '
        Me.ttxUnit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ttxUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ttxUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ttxUnit.Location = New System.Drawing.Point(641, 57)
        Me.ttxUnit.Name = "ttxUnit"
        Me.ttxUnit.Size = New System.Drawing.Size(163, 21)
        Me.ttxUnit.TabIndex = 79
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(554, 58)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 15)
        Me.Label4.TabIndex = 78
        Me.Label4.Text = "By Unit / Area :"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtUnit)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnRefresh2)
        Me.Panel2.Controls.Add(Me.btnSelect2)
        Me.Panel2.Controls.Add(Me.lvUnit)
        Me.Panel2.Location = New System.Drawing.Point(628, 81)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(526, 275)
        Me.Panel2.TabIndex = 84
        Me.Panel2.Visible = False
        '
        'txtUnit
        '
        Me.txtUnit.BackColor = System.Drawing.Color.White
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.Location = New System.Drawing.Point(100, 245)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(265, 21)
        Me.txtUnit.TabIndex = 73
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 247)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(86, 15)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Quick Search :"
        '
        'btnRefresh2
        '
        Me.btnRefresh2.Image = Global.SAIS6._0.My.Resources.Resources.refresh
        Me.btnRefresh2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh2.Location = New System.Drawing.Point(449, 243)
        Me.btnRefresh2.Name = "btnRefresh2"
        Me.btnRefresh2.Size = New System.Drawing.Size(70, 24)
        Me.btnRefresh2.TabIndex = 36
        Me.btnRefresh2.Text = "Clear"
        Me.btnRefresh2.UseVisualStyleBackColor = True
        '
        'btnSelect2
        '
        Me.btnSelect2.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnSelect2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelect2.Location = New System.Drawing.Point(370, 243)
        Me.btnSelect2.Name = "btnSelect2"
        Me.btnSelect2.Size = New System.Drawing.Size(77, 24)
        Me.btnSelect2.TabIndex = 35
        Me.btnSelect2.Text = "&Select"
        Me.btnSelect2.UseVisualStyleBackColor = True
        '
        'lvUnit
        '
        Me.lvUnit.CheckBoxes = True
        Me.lvUnit.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvUnit.FullRowSelect = True
        Me.lvUnit.Location = New System.Drawing.Point(5, 4)
        Me.lvUnit.Name = "lvUnit"
        Me.lvUnit.Size = New System.Drawing.Size(514, 233)
        Me.lvUnit.TabIndex = 0
        Me.lvUnit.UseCompatibleStateImageBehavior = False
        Me.lvUnit.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Unit Code"
        Me.ColumnHeader5.Width = 79
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Unit / Area"
        Me.ColumnHeader6.Width = 425
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.txtStatus)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.btnRefresh1)
        Me.Panel1.Controls.Add(Me.btnSelect1)
        Me.Panel1.Controls.Add(Me.lvStatus)
        Me.Panel1.Location = New System.Drawing.Point(100, 361)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(526, 275)
        Me.Panel1.TabIndex = 88
        Me.Panel1.Visible = False
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.White
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(100, 245)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(265, 21)
        Me.txtStatus.TabIndex = 73
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 247)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 15)
        Me.Label6.TabIndex = 72
        Me.Label6.Text = "Quick Search :"
        '
        'btnRefresh1
        '
        Me.btnRefresh1.Image = Global.SAIS6._0.My.Resources.Resources.refresh
        Me.btnRefresh1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh1.Location = New System.Drawing.Point(449, 243)
        Me.btnRefresh1.Name = "btnRefresh1"
        Me.btnRefresh1.Size = New System.Drawing.Size(70, 24)
        Me.btnRefresh1.TabIndex = 36
        Me.btnRefresh1.Text = "Clear"
        Me.btnRefresh1.UseVisualStyleBackColor = True
        '
        'btnSelect1
        '
        Me.btnSelect1.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnSelect1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelect1.Location = New System.Drawing.Point(370, 243)
        Me.btnSelect1.Name = "btnSelect1"
        Me.btnSelect1.Size = New System.Drawing.Size(77, 24)
        Me.btnSelect1.TabIndex = 35
        Me.btnSelect1.Text = "&Select"
        Me.btnSelect1.UseVisualStyleBackColor = True
        '
        'lvStatus
        '
        Me.lvStatus.CheckBoxes = True
        Me.lvStatus.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4})
        Me.lvStatus.FullRowSelect = True
        Me.lvStatus.Location = New System.Drawing.Point(5, 4)
        Me.lvStatus.Name = "lvStatus"
        Me.lvStatus.Size = New System.Drawing.Size(514, 233)
        Me.lvStatus.TabIndex = 0
        Me.lvStatus.UseCompatibleStateImageBehavior = False
        Me.lvStatus.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Status Code"
        Me.ColumnHeader3.Width = 72
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Status"
        Me.ColumnHeader4.Width = 425
        '
        'frmRptEmployeeAlphalist
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1221, 677)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnFind2)
        Me.Controls.Add(Me.ttxUnit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnFind1)
        Me.Controls.Add(Me.ttxStatus)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.ttxCompany)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.CRViewer)
        Me.Name = "frmRptEmployeeAlphalist"
        Me.Text = "Employee Alphalist"
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CRViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents btnFind As Button
    Friend WithEvents ttxCompany As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel As Panel
    Friend WithEvents lvCompany As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents txtCompany As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents btnPreview As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnReload As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents btnClose As ToolStripButton
    Friend WithEvents btnPrint As ToolStripButton
    Friend WithEvents btnFind1 As Button
    Friend WithEvents ttxStatus As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents btnFind2 As Button
    Friend WithEvents ttxUnit As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtUnit As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnRefresh2 As Button
    Friend WithEvents btnSelect2 As Button
    Friend WithEvents lvUnit As ListView
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtStatus As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnRefresh1 As Button
    Friend WithEvents btnSelect1 As Button
    Friend WithEvents lvStatus As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
End Class
