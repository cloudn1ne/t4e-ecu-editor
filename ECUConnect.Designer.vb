<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ECUConnect
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
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.LblAccessType = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.RBSpeedAuto = New System.Windows.Forms.RadioButton
        Me.ChkBECUAutoConnect = New System.Windows.Forms.CheckBox
        Me.TBAdapterVersion = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.BComportListReload = New System.Windows.Forms.Button
        Me.BConnect = New System.Windows.Forms.Button
        Me.RBSpeed1M = New System.Windows.Forms.RadioButton
        Me.RBSpeed500K = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.CBComport = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.LblCalibrationDetail = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LblCalibrationDetail)
        Me.GroupBox1.Controls.Add(Me.LblAccessType)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.RBSpeedAuto)
        Me.GroupBox1.Controls.Add(Me.ChkBECUAutoConnect)
        Me.GroupBox1.Controls.Add(Me.TBAdapterVersion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.BComportListReload)
        Me.GroupBox1.Controls.Add(Me.BConnect)
        Me.GroupBox1.Controls.Add(Me.RBSpeed1M)
        Me.GroupBox1.Controls.Add(Me.RBSpeed500K)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.CBComport)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(378, 203)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CAN adapter settings"
        '
        'LblAccessType
        '
        Me.LblAccessType.AutoSize = True
        Me.LblAccessType.Location = New System.Drawing.Point(96, 136)
        Me.LblAccessType.Name = "LblAccessType"
        Me.LblAccessType.Size = New System.Drawing.Size(80, 13)
        Me.LblAccessType.TabIndex = 12
        Me.LblAccessType.Text = "LblAccessType"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 136)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Access Level:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ECU Software:"
        '
        'RBSpeedAuto
        '
        Me.RBSpeedAuto.AutoSize = True
        Me.RBSpeedAuto.Location = New System.Drawing.Point(241, 67)
        Me.RBSpeedAuto.Name = "RBSpeedAuto"
        Me.RBSpeedAuto.Size = New System.Drawing.Size(74, 17)
        Me.RBSpeedAuto.TabIndex = 10
        Me.RBSpeedAuto.Text = "autosense"
        Me.RBSpeedAuto.UseVisualStyleBackColor = True
        '
        'ChkBECUAutoConnect
        '
        Me.ChkBECUAutoConnect.AutoSize = True
        Me.ChkBECUAutoConnect.Location = New System.Drawing.Point(243, 140)
        Me.ChkBECUAutoConnect.Name = "ChkBECUAutoConnect"
        Me.ChkBECUAutoConnect.Size = New System.Drawing.Size(127, 17)
        Me.ChkBECUAutoConnect.TabIndex = 9
        Me.ChkBECUAutoConnect.Text = "auto connect on start"
        Me.ChkBECUAutoConnect.UseVisualStyleBackColor = True
        '
        'TBAdapterVersion
        '
        Me.TBAdapterVersion.Location = New System.Drawing.Point(99, 101)
        Me.TBAdapterVersion.Name = "TBAdapterVersion"
        Me.TBAdapterVersion.ReadOnly = True
        Me.TBAdapterVersion.Size = New System.Drawing.Size(147, 20)
        Me.TBAdapterVersion.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Adapter Version:"
        '
        'BComportListReload
        '
        Me.BComportListReload.Image = Global.T4e_ECU_Editor.My.Resources.Resources.reload_icon
        Me.BComportListReload.Location = New System.Drawing.Point(316, 29)
        Me.BComportListReload.Name = "BComportListReload"
        Me.BComportListReload.Size = New System.Drawing.Size(28, 21)
        Me.BComportListReload.TabIndex = 6
        Me.BComportListReload.UseVisualStyleBackColor = True
        '
        'BConnect
        '
        Me.BConnect.Image = Global.T4e_ECU_Editor.My.Resources.Resources.plug_connect_icon
        Me.BConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BConnect.Location = New System.Drawing.Point(260, 163)
        Me.BConnect.Name = "BConnect"
        Me.BConnect.Size = New System.Drawing.Size(110, 34)
        Me.BConnect.TabIndex = 5
        Me.BConnect.Text = "Test Connection"
        Me.BConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BConnect.UseVisualStyleBackColor = True
        '
        'RBSpeed1M
        '
        Me.RBSpeed1M.AutoSize = True
        Me.RBSpeed1M.Location = New System.Drawing.Point(168, 67)
        Me.RBSpeed1M.Name = "RBSpeed1M"
        Me.RBSpeed1M.Size = New System.Drawing.Size(69, 17)
        Me.RBSpeed1M.TabIndex = 4
        Me.RBSpeed1M.Text = "1000 kbit"
        Me.RBSpeed1M.UseVisualStyleBackColor = True
        '
        'RBSpeed500K
        '
        Me.RBSpeed500K.AutoSize = True
        Me.RBSpeed500K.Checked = True
        Me.RBSpeed500K.Location = New System.Drawing.Point(99, 67)
        Me.RBSpeed500K.Name = "RBSpeed500K"
        Me.RBSpeed500K.Size = New System.Drawing.Size(63, 17)
        Me.RBSpeed500K.TabIndex = 3
        Me.RBSpeed500K.TabStop = True
        Me.RBSpeed500K.Text = "500 kbit"
        Me.RBSpeed500K.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "CAN Speed:"
        '
        'CBComport
        '
        Me.CBComport.FormattingEnabled = True
        Me.CBComport.Location = New System.Drawing.Point(99, 29)
        Me.CBComport.Name = "CBComport"
        Me.CBComport.Size = New System.Drawing.Size(211, 21)
        Me.CBComport.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "COM Port:"
        '
        'LblCalibrationDetail
        '
        Me.LblCalibrationDetail.AutoSize = True
        Me.LblCalibrationDetail.Location = New System.Drawing.Point(96, 174)
        Me.LblCalibrationDetail.Name = "LblCalibrationDetail"
        Me.LblCalibrationDetail.Size = New System.Drawing.Size(97, 13)
        Me.LblCalibrationDetail.TabIndex = 13
        Me.LblCalibrationDetail.Text = "LblCalibrationDetail"
        '
        'ECUConnect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 225)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ECUConnect"
        Me.Text = "Connect to T4e ECU"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RBSpeed1M As System.Windows.Forms.RadioButton
    Friend WithEvents RBSpeed500K As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CBComport As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BConnect As System.Windows.Forms.Button
    Friend WithEvents BComportListReload As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RBSpeedAuto As System.Windows.Forms.RadioButton
    Friend WithEvents ChkBECUAutoConnect As System.Windows.Forms.CheckBox
    Friend WithEvents TBAdapterVersion As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents LblAccessType As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LblCalibrationDetail As System.Windows.Forms.Label
End Class
