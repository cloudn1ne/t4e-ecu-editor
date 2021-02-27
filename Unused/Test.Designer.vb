<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Test
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
        Me.xLabelPanel = New System.Windows.Forms.Panel
        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.TSB_Grading = New System.Windows.Forms.ToolStripButton
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.yLabelPanel = New System.Windows.Forms.Panel
        Me.DGVPanel = New System.Windows.Forms.Panel
        Me.mapDataGridView = New System.Windows.Forms.DataGridView
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.xLabelPanel.SuspendLayout()
        Me.ToolStrip.SuspendLayout()
        Me.DGVPanel.SuspendLayout()
        CType(Me.mapDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'xLabelPanel
        '
        Me.xLabelPanel.Controls.Add(Me.ToolStrip)
        Me.xLabelPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.xLabelPanel.Location = New System.Drawing.Point(0, 0)
        Me.xLabelPanel.Name = "xLabelPanel"
        Me.xLabelPanel.Size = New System.Drawing.Size(738, 100)
        Me.xLabelPanel.TabIndex = 0
        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSB_Grading, Me.ToolStripLabel1, Me.ToolStripButton1})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(738, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip"
        '
        'TSB_Grading
        '
        Me.TSB_Grading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSB_Grading.Image = Global.T4e_ECU_Editor.My.Resources.Resources.colorgradient
        Me.TSB_Grading.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_Grading.Name = "TSB_Grading"
        Me.TSB_Grading.Size = New System.Drawing.Size(23, 22)
        Me.TSB_Grading.Text = "Display Grading"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(47, 22)
        Me.ToolStripLabel1.Text = "Mapblah"
        '
        'yLabelPanel
        '
        Me.yLabelPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.yLabelPanel.Location = New System.Drawing.Point(0, 100)
        Me.yLabelPanel.Name = "yLabelPanel"
        Me.yLabelPanel.Size = New System.Drawing.Size(200, 336)
        Me.yLabelPanel.TabIndex = 1
        '
        'DGVPanel
        '
        Me.DGVPanel.Controls.Add(Me.mapDataGridView)
        Me.DGVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVPanel.Location = New System.Drawing.Point(200, 100)
        Me.DGVPanel.Name = "DGVPanel"
        Me.DGVPanel.Size = New System.Drawing.Size(538, 336)
        Me.DGVPanel.TabIndex = 2
        '
        'mapDataGridView
        '
        Me.mapDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.mapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.mapDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.mapDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.mapDataGridView.Name = "mapDataGridView"
        Me.mapDataGridView.Size = New System.Drawing.Size(538, 336)
        Me.mapDataGridView.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = Global.T4e_ECU_Editor.My.Resources.Resources.calculator_icon
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 436)
        Me.Controls.Add(Me.DGVPanel)
        Me.Controls.Add(Me.yLabelPanel)
        Me.Controls.Add(Me.xLabelPanel)
        Me.Name = "Test"
        Me.Text = "Test"
        Me.xLabelPanel.ResumeLayout(False)
        Me.xLabelPanel.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.DGVPanel.ResumeLayout(False)
        CType(Me.mapDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents xLabelPanel As System.Windows.Forms.Panel
    Friend WithEvents yLabelPanel As System.Windows.Forms.Panel
    Friend WithEvents DGVPanel As System.Windows.Forms.Panel
    Friend WithEvents mapDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents TSB_Grading As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
