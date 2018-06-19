<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParticularQuery
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
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnFind = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFind = New System.Windows.Forms.TextBox()
        Me.cboFields = New System.Windows.Forms.ComboBox()
        Me.p_FOOTER = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvList = New System.Windows.Forms.ListView()
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.p_FOOTER.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SAIS6._0.My.Resources.Resources.refresh
        Me.btnRefresh.Location = New System.Drawing.Point(526, 2)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(30, 24)
        Me.btnRefresh.TabIndex = 34
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnFind
        '
        Me.btnFind.Image = Global.SAIS6._0.My.Resources.Resources.search
        Me.btnFind.Location = New System.Drawing.Point(496, 2)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(30, 24)
        Me.btnFind.TabIndex = 33
        Me.btnFind.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 5)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Quick Search :"
        '
        'txtFind
        '
        Me.txtFind.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFind.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.Location = New System.Drawing.Point(318, 3)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.Size = New System.Drawing.Size(176, 22)
        Me.txtFind.TabIndex = 32
        '
        'cboFields
        '
        Me.cboFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFields.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFields.FormattingEnabled = True
        Me.cboFields.Location = New System.Drawing.Point(103, 3)
        Me.cboFields.Name = "cboFields"
        Me.cboFields.Size = New System.Drawing.Size(179, 22)
        Me.cboFields.TabIndex = 30
        '
        'p_FOOTER
        '
        Me.p_FOOTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.p_FOOTER.Controls.Add(Me.btnRefresh)
        Me.p_FOOTER.Controls.Add(Me.btnFind)
        Me.p_FOOTER.Controls.Add(Me.Label4)
        Me.p_FOOTER.Controls.Add(Me.txtFind)
        Me.p_FOOTER.Controls.Add(Me.PictureBox1)
        Me.p_FOOTER.Controls.Add(Me.cboFields)
        Me.p_FOOTER.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.p_FOOTER.Location = New System.Drawing.Point(0, 494)
        Me.p_FOOTER.Name = "p_FOOTER"
        Me.p_FOOTER.Size = New System.Drawing.Size(954, 30)
        Me.p_FOOTER.TabIndex = 36
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SAIS6._0.My.Resources.Resources.arrow
        Me.PictureBox1.Location = New System.Drawing.Point(290, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(22, 20)
        Me.PictureBox1.TabIndex = 31
        Me.PictureBox1.TabStop = False
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Taxable"
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Multiplier"
        Me.ColumnHeader8.Width = 64
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Has ER Share"
        Me.ColumnHeader6.Width = 84
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Has Unit"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Income"
        Me.ColumnHeader4.Width = 63
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Description"
        Me.ColumnHeader3.Width = 430
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Short Desc"
        Me.ColumnHeader2.Width = 301
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Particular ID"
        Me.ColumnHeader1.Width = 82
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Mandatory"
        Me.ColumnHeader7.Width = 75
        '
        'lvList
        '
        Me.lvList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.lvList.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvList.FullRowSelect = True
        Me.lvList.Location = New System.Drawing.Point(4, 4)
        Me.lvList.Name = "lvList"
        Me.lvList.Size = New System.Drawing.Size(946, 454)
        Me.lvList.TabIndex = 35
        Me.lvList.UseCompatibleStateImageBehavior = False
        Me.lvList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Active"
        '
        'frmParticularQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(954, 524)
        Me.Controls.Add(Me.p_FOOTER)
        Me.Controls.Add(Me.lvList)
        Me.Name = "frmParticularQuery"
        Me.Text = "Query View"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.p_FOOTER.ResumeLayout(False)
        Me.p_FOOTER.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnFind As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtFind As TextBox
    Friend WithEvents cboFields As ComboBox
    Friend WithEvents p_FOOTER As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents lvList As ListView
    Friend WithEvents ColumnHeader10 As ColumnHeader
End Class
