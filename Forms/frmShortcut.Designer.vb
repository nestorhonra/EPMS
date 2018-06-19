<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShortcut
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
        Me.lvMenu = New System.Windows.Forms.ListView()
        Me.btnTransaction = New System.Windows.Forms.Button()
        Me.btnConfig = New System.Windows.Forms.Button()
        Me.btnReport = New System.Windows.Forms.Button()
        Me.btnUtilities = New System.Windows.Forms.Button()
        Me.btnFileMaintenance = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'lvMenu
        '
        Me.lvMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvMenu.Location = New System.Drawing.Point(166, 2)
        Me.lvMenu.Name = "lvMenu"
        Me.lvMenu.Size = New System.Drawing.Size(397, 249)
        Me.lvMenu.TabIndex = 0
        Me.lvMenu.UseCompatibleStateImageBehavior = False
        '
        'btnTransaction
        '
        Me.btnTransaction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnTransaction.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransaction.Image = Global.SAIS6._0.My.Resources.Resources.transactions
        Me.btnTransaction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTransaction.Location = New System.Drawing.Point(5, 42)
        Me.btnTransaction.Name = "btnTransaction"
        Me.btnTransaction.Size = New System.Drawing.Size(158, 39)
        Me.btnTransaction.TabIndex = 8
        Me.btnTransaction.Text = "&Transactions"
        Me.btnTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTransaction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnTransaction.UseVisualStyleBackColor = True
        '
        'btnConfig
        '
        Me.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConfig.Image = Global.SAIS6._0.My.Resources.Resources.configurations
        Me.btnConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfig.Location = New System.Drawing.Point(5, 82)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(158, 39)
        Me.btnConfig.TabIndex = 9
        Me.btnConfig.Text = "&Configurations"
        Me.btnConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnConfig.UseVisualStyleBackColor = True
        '
        'btnReport
        '
        Me.btnReport.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReport.Image = Global.SAIS6._0.My.Resources.Resources.reports
        Me.btnReport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.Location = New System.Drawing.Point(5, 122)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(158, 39)
        Me.btnReport.TabIndex = 10
        Me.btnReport.Text = "&Reports"
        Me.btnReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'btnUtilities
        '
        Me.btnUtilities.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnUtilities.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnUtilities.Image = Global.SAIS6._0.My.Resources.Resources.utilities
        Me.btnUtilities.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUtilities.Location = New System.Drawing.Point(5, 162)
        Me.btnUtilities.Name = "btnUtilities"
        Me.btnUtilities.Size = New System.Drawing.Size(158, 39)
        Me.btnUtilities.TabIndex = 11
        Me.btnUtilities.Text = "&Utilities"
        Me.btnUtilities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUtilities.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnUtilities.UseVisualStyleBackColor = True
        '
        'btnFileMaintenance
        '
        Me.btnFileMaintenance.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnFileMaintenance.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFileMaintenance.Image = Global.SAIS6._0.My.Resources.Resources.master_file
        Me.btnFileMaintenance.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFileMaintenance.Location = New System.Drawing.Point(5, 2)
        Me.btnFileMaintenance.Name = "btnFileMaintenance"
        Me.btnFileMaintenance.Size = New System.Drawing.Size(158, 39)
        Me.btnFileMaintenance.TabIndex = 7
        Me.btnFileMaintenance.Text = "&File Maintenance"
        Me.btnFileMaintenance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnFileMaintenance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnFileMaintenance.UseVisualStyleBackColor = True
        '
        'frmShortcut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 377)
        Me.Controls.Add(Me.btnUtilities)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnConfig)
        Me.Controls.Add(Me.btnTransaction)
        Me.Controls.Add(Me.btnFileMaintenance)
        Me.Controls.Add(Me.lvMenu)
        Me.Name = "frmShortcut"
        Me.Text = "@Shortcut"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lvMenu As ListView
    Friend WithEvents btnFileMaintenance As Button
    Friend WithEvents btnTransaction As Button
    Friend WithEvents btnConfig As Button
    Friend WithEvents btnReport As Button
    Friend WithEvents btnUtilities As Button
End Class
