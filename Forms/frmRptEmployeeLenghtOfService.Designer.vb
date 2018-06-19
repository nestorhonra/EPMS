<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptEmployeeLenghtOfService
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
        Me.btnPreview = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReload = New System.Windows.Forms.ToolStripButton()
        Me.btnClose = New System.Windows.Forms.ToolStripButton()
        Me.Panel = New System.Windows.Forms.Panel()
        Me.txtCompany = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnSelect = New System.Windows.Forms.Button()
        Me.lvCompany = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.CRViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnFind2 = New System.Windows.Forms.Button()
        Me.ttxtDivision = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.ttxCompany = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnRefresh2 = New System.Windows.Forms.Button()
        Me.btnSelect2 = New System.Windows.Forms.Button()
        Me.lvDivision = New System.Windows.Forms.ListView()
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ToolStrip.SuspendLayout()
        Me.Panel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPreview, Me.ToolStripSeparator2, Me.btnPrint, Me.ToolStripSeparator3, Me.btnReload, Me.btnClose})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(1011, 25)
        Me.ToolStrip.TabIndex = 76
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
        'Panel
        '
        Me.Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel.Controls.Add(Me.txtCompany)
        Me.Panel.Controls.Add(Me.Label2)
        Me.Panel.Controls.Add(Me.btnRefresh)
        Me.Panel.Controls.Add(Me.btnSelect)
        Me.Panel.Controls.Add(Me.lvCompany)
        Me.Panel.Location = New System.Drawing.Point(98, 75)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(526, 275)
        Me.Panel.TabIndex = 96
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
        'CRViewer
        '
        Me.CRViewer.ActiveViewIndex = -1
        Me.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRViewer.DisplayBackgroundEdge = False
        Me.CRViewer.DisplayGroupTree = False
        Me.CRViewer.Location = New System.Drawing.Point(4, 75)
        Me.CRViewer.Name = "CRViewer"
        Me.CRViewer.SelectionFormula = ""
        Me.CRViewer.ShowGroupTreeButton = False
        Me.CRViewer.ShowRefreshButton = False
        Me.CRViewer.Size = New System.Drawing.Size(88, 382)
        Me.CRViewer.TabIndex = 95
        Me.CRViewer.ViewTimeSelectionFormula = ""
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
        'btnFind2
        '
        Me.btnFind2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFind2.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind2.Location = New System.Drawing.Point(532, 51)
        Me.btnFind2.Name = "btnFind2"
        Me.btnFind2.Size = New System.Drawing.Size(25, 21)
        Me.btnFind2.TabIndex = 104
        Me.btnFind2.UseVisualStyleBackColor = True
        '
        'ttxtDivision
        '
        Me.ttxtDivision.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ttxtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ttxtDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ttxtDivision.Location = New System.Drawing.Point(367, 51)
        Me.ttxtDivision.Name = "ttxtDivision"
        Me.ttxtDivision.Size = New System.Drawing.Size(163, 21)
        Me.ttxtDivision.TabIndex = 103
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(293, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 15)
        Me.Label4.TabIndex = 102
        Me.Label4.Text = "By Division :"
        '
        'btnFind
        '
        Me.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFind.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind.Location = New System.Drawing.Point(262, 50)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(25, 21)
        Me.btnFind.TabIndex = 101
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'ttxCompany
        '
        Me.ttxCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ttxCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ttxCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ttxCompany.Location = New System.Drawing.Point(97, 50)
        Me.ttxCompany.Name = "ttxCompany"
        Me.ttxCompany.Size = New System.Drawing.Size(163, 21)
        Me.ttxCompany.TabIndex = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 15)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "By Company :"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label5)
        Me.Panel3.Location = New System.Drawing.Point(4, 25)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(785, 22)
        Me.Panel3.TabIndex = 98
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.txtDivision)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.btnRefresh2)
        Me.Panel2.Controls.Add(Me.btnSelect2)
        Me.Panel2.Controls.Add(Me.lvDivision)
        Me.Panel2.Location = New System.Drawing.Point(367, 74)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(526, 275)
        Me.Panel2.TabIndex = 105
        Me.Panel2.Visible = False
        '
        'txtDivision
        '
        Me.txtDivision.BackColor = System.Drawing.Color.White
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.Location = New System.Drawing.Point(100, 245)
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.Size = New System.Drawing.Size(265, 21)
        Me.txtDivision.TabIndex = 73
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
        'lvDivision
        '
        Me.lvDivision.CheckBoxes = True
        Me.lvDivision.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5, Me.ColumnHeader6})
        Me.lvDivision.FullRowSelect = True
        Me.lvDivision.Location = New System.Drawing.Point(5, 4)
        Me.lvDivision.Name = "lvDivision"
        Me.lvDivision.Size = New System.Drawing.Size(514, 233)
        Me.lvDivision.TabIndex = 0
        Me.lvDivision.UseCompatibleStateImageBehavior = False
        Me.lvDivision.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Division Code"
        Me.ColumnHeader5.Width = 79
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Division"
        Me.ColumnHeader6.Width = 425
        '
        'frmRptEmployeeLenghtOfService
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1011, 504)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.btnFind2)
        Me.Controls.Add(Me.ttxtDivision)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnFind)
        Me.Controls.Add(Me.ttxCompany)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel)
        Me.Controls.Add(Me.CRViewer)
        Me.Controls.Add(Me.ToolStrip)
        Me.Name = "frmRptEmployeeLenghtOfService"
        Me.Text = "Employee Length of Service"
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip As ToolStrip
    Friend WithEvents btnPreview As ToolStripButton
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents btnPrint As ToolStripButton
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents btnReload As ToolStripButton
    Friend WithEvents btnClose As ToolStripButton
    Friend WithEvents Panel As Panel
    Friend WithEvents txtCompany As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnSelect As Button
    Friend WithEvents lvCompany As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents CRViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
    Friend WithEvents Label5 As Label
    Friend WithEvents btnFind2 As Button
    Friend WithEvents ttxtDivision As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents btnFind As Button
    Friend WithEvents ttxCompany As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents txtDivision As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnRefresh2 As Button
    Friend WithEvents btnSelect2 As Button
    Friend WithEvents lvDivision As ListView
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
End Class
