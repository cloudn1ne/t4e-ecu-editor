<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Datalogger
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
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend
        Me.TVDatalogger = New System.Windows.Forms.TreeView
        Me.TimerVarUpd = New System.Windows.Forms.Timer(Me.components)
        Me.Chart = New System.Windows.Forms.DataVisualization.Charting.Chart
        Me.PanelTV = New System.Windows.Forms.Panel
        Me.PanelContent = New System.Windows.Forms.Panel
        Me.BChartRemoveVars = New System.Windows.Forms.Button
        Me.BChartClear = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ReLoadDef = New System.Windows.Forms.Button
        Me.BCanConnect = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.LblAddr = New System.Windows.Forms.Label
        Me.LblAddrText = New System.Windows.Forms.Label
        Me.LblValue = New System.Windows.Forms.Label
        Me.LblSize = New System.Windows.Forms.Label
        Me.LblDesc = New System.Windows.Forms.Label
        Me.LblUnit = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblName = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.PanelTop = New System.Windows.Forms.Panel
        Me.ContextMenuTV = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AddToChartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTV.SuspendLayout()
        Me.PanelContent.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.PanelTop.SuspendLayout()
        Me.ContextMenuTV.SuspendLayout()
        Me.SuspendLayout()
        '
        'TVDatalogger
        '
        Me.TVDatalogger.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TVDatalogger.Location = New System.Drawing.Point(0, 0)
        Me.TVDatalogger.Name = "TVDatalogger"
        Me.TVDatalogger.Size = New System.Drawing.Size(313, 548)
        Me.TVDatalogger.TabIndex = 0
        '
        'TimerVarUpd
        '
        '
        'Chart
        '
        ChartArea3.Name = "ChartArea1"
        Me.Chart.ChartAreas.Add(ChartArea3)
        Me.Chart.Dock = System.Windows.Forms.DockStyle.Fill
        Legend3.Name = "Default"
        Me.Chart.Legends.Add(Legend3)
        Me.Chart.Location = New System.Drawing.Point(0, 0)
        Me.Chart.Name = "Chart"
        Me.Chart.Size = New System.Drawing.Size(521, 398)
        Me.Chart.TabIndex = 3
        Me.Chart.Text = "Chart"
        '
        'PanelTV
        '
        Me.PanelTV.Controls.Add(Me.TVDatalogger)
        Me.PanelTV.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelTV.Location = New System.Drawing.Point(0, 0)
        Me.PanelTV.Name = "PanelTV"
        Me.PanelTV.Size = New System.Drawing.Size(313, 548)
        Me.PanelTV.TabIndex = 4
        '
        'PanelContent
        '
        Me.PanelContent.Controls.Add(Me.BChartRemoveVars)
        Me.PanelContent.Controls.Add(Me.BChartClear)
        Me.PanelContent.Controls.Add(Me.Chart)
        Me.PanelContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelContent.Location = New System.Drawing.Point(313, 150)
        Me.PanelContent.Name = "PanelContent"
        Me.PanelContent.Size = New System.Drawing.Size(521, 398)
        Me.PanelContent.TabIndex = 5
        '
        'BChartRemoveVars
        '
        Me.BChartRemoveVars.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BChartRemoveVars.Location = New System.Drawing.Point(336, 356)
        Me.BChartRemoveVars.Name = "BChartRemoveVars"
        Me.BChartRemoveVars.Size = New System.Drawing.Size(88, 30)
        Me.BChartRemoveVars.TabIndex = 5
        Me.BChartRemoveVars.Text = "Remove Vars"
        Me.BChartRemoveVars.UseVisualStyleBackColor = True
        '
        'BChartClear
        '
        Me.BChartClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BChartClear.Location = New System.Drawing.Point(430, 356)
        Me.BChartClear.Name = "BChartClear"
        Me.BChartClear.Size = New System.Drawing.Size(88, 30)
        Me.BChartClear.TabIndex = 4
        Me.BChartClear.Text = "Clear Chart"
        Me.BChartClear.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ReLoadDef)
        Me.GroupBox1.Controls.Add(Me.BCanConnect)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(511, 52)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Settings"
        '
        'ReLoadDef
        '
        Me.ReLoadDef.Location = New System.Drawing.Point(363, 19)
        Me.ReLoadDef.Name = "ReLoadDef"
        Me.ReLoadDef.Size = New System.Drawing.Size(142, 23)
        Me.ReLoadDef.TabIndex = 5
        Me.ReLoadDef.Text = "Reload Cal Defintion File"
        Me.ReLoadDef.UseVisualStyleBackColor = True
        '
        'BCanConnect
        '
        Me.BCanConnect.Location = New System.Drawing.Point(6, 19)
        Me.BCanConnect.Name = "BCanConnect"
        Me.BCanConnect.Size = New System.Drawing.Size(136, 23)
        Me.BCanConnect.TabIndex = 2
        Me.BCanConnect.Text = "Start Logging"
        Me.BCanConnect.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LblAddr)
        Me.GroupBox2.Controls.Add(Me.LblAddrText)
        Me.GroupBox2.Controls.Add(Me.LblValue)
        Me.GroupBox2.Controls.Add(Me.LblSize)
        Me.GroupBox2.Controls.Add(Me.LblDesc)
        Me.GroupBox2.Controls.Add(Me.LblUnit)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.LblName)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 61)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(511, 84)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Variable Definition"
        '
        'LblAddr
        '
        Me.LblAddr.AutoSize = True
        Me.LblAddr.Location = New System.Drawing.Point(281, 20)
        Me.LblAddr.Name = "LblAddr"
        Me.LblAddr.Size = New System.Drawing.Size(43, 13)
        Me.LblAddr.TabIndex = 11
        Me.LblAddr.Text = "LblAddr"
        '
        'LblAddrText
        '
        Me.LblAddrText.AutoSize = True
        Me.LblAddrText.Location = New System.Drawing.Point(246, 20)
        Me.LblAddrText.Name = "LblAddrText"
        Me.LblAddrText.Size = New System.Drawing.Size(32, 13)
        Me.LblAddrText.TabIndex = 10
        Me.LblAddrText.Text = "Addr:"
        '
        'LblValue
        '
        Me.LblValue.AutoSize = True
        Me.LblValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblValue.Location = New System.Drawing.Point(298, 46)
        Me.LblValue.Name = "LblValue"
        Me.LblValue.Size = New System.Drawing.Size(136, 33)
        Me.LblValue.TabIndex = 9
        Me.LblValue.Text = "LblValue"
        '
        'LblSize
        '
        Me.LblSize.AutoSize = True
        Me.LblSize.Location = New System.Drawing.Point(75, 59)
        Me.LblSize.Name = "LblSize"
        Me.LblSize.Size = New System.Drawing.Size(41, 13)
        Me.LblSize.TabIndex = 8
        Me.LblSize.Text = "LblSize"
        '
        'LblDesc
        '
        Me.LblDesc.AutoSize = True
        Me.LblDesc.Location = New System.Drawing.Point(75, 33)
        Me.LblDesc.Name = "LblDesc"
        Me.LblDesc.Size = New System.Drawing.Size(46, 13)
        Me.LblDesc.TabIndex = 7
        Me.LblDesc.Text = "LblDesc"
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Location = New System.Drawing.Point(75, 46)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(40, 13)
        Me.LblUnit.TabIndex = 6
        Me.LblUnit.Text = "LblUnit"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Unit:"
        '
        'LblName
        '
        Me.LblName.AutoSize = True
        Me.LblName.Location = New System.Drawing.Point(75, 20)
        Me.LblName.Name = "LblName"
        Me.LblName.Size = New System.Drawing.Size(49, 13)
        Me.LblName.TabIndex = 5
        Me.LblName.Text = "LblName"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(246, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Value:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(30, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Size:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Description:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Name:"
        '
        'PanelTop
        '
        Me.PanelTop.Controls.Add(Me.GroupBox2)
        Me.PanelTop.Controls.Add(Me.GroupBox1)
        Me.PanelTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelTop.Location = New System.Drawing.Point(313, 0)
        Me.PanelTop.Name = "PanelTop"
        Me.PanelTop.Size = New System.Drawing.Size(521, 150)
        Me.PanelTop.TabIndex = 3
        '
        'ContextMenuTV
        '
        Me.ContextMenuTV.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddToChartToolStripMenuItem})
        Me.ContextMenuTV.Name = "ContextMenuStrip1"
        Me.ContextMenuTV.Size = New System.Drawing.Size(145, 26)
        '
        'AddToChartToolStripMenuItem
        '
        Me.AddToChartToolStripMenuItem.Name = "AddToChartToolStripMenuItem"
        Me.AddToChartToolStripMenuItem.Size = New System.Drawing.Size(144, 22)
        Me.AddToChartToolStripMenuItem.Text = "add to chart"
        '
        'Datalogger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 548)
        Me.Controls.Add(Me.PanelContent)
        Me.Controls.Add(Me.PanelTop)
        Me.Controls.Add(Me.PanelTV)
        Me.Name = "Datalogger"
        Me.Text = "Datalogger"
        CType(Me.Chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTV.ResumeLayout(False)
        Me.PanelContent.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.PanelTop.ResumeLayout(False)
        Me.ContextMenuTV.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TVDatalogger As System.Windows.Forms.TreeView

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Friend WithEvents TimerVarUpd As System.Windows.Forms.Timer
    Friend WithEvents Chart As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents PanelTV As System.Windows.Forms.Panel
    Friend WithEvents PanelContent As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ReLoadDef As System.Windows.Forms.Button
    Friend WithEvents BCanConnect As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LblValue As System.Windows.Forms.Label
    Friend WithEvents LblSize As System.Windows.Forms.Label
    Friend WithEvents LblDesc As System.Windows.Forms.Label
    Friend WithEvents LblUnit As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblName As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PanelTop As System.Windows.Forms.Panel
    Friend WithEvents BChartClear As System.Windows.Forms.Button
    Friend WithEvents BChartRemoveVars As System.Windows.Forms.Button
    Friend WithEvents ContextMenuTV As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AddToChartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LblAddr As System.Windows.Forms.Label
    Friend WithEvents LblAddrText As System.Windows.Forms.Label
End Class
