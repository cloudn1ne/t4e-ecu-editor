<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.TVMain = New System.Windows.Forms.TreeView
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadCalDefinitionMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LoadCalDataMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ECUToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConnectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DataLoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DataLoggerOpenMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenFileDialog_Import_CALDef = New System.Windows.Forms.OpenFileDialog
        Me.ReLoadDef = New System.Windows.Forms.Button
        Me.GroupBoxMD = New System.Windows.Forms.GroupBox
        Me.LblLUTRCalc = New System.Windows.Forms.Label
        Me.LblLUTBCalc = New System.Windows.Forms.Label
        Me.LblLUTACalc = New System.Windows.Forms.Label
        Me.LblMapBits = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.LblXref = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.LblFunc = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LblLUTRName = New System.Windows.Forms.Label
        Me.LblLUTRAddr = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.LblLUTBName = New System.Windows.Forms.Label
        Me.LblLUTBAddr = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.LblLUTAName = New System.Windows.Forms.Label
        Me.LblLUTAAddr = New System.Windows.Forms.Label
        Me.LblLookupA = New System.Windows.Forms.Label
        Me.LblInputBName = New System.Windows.Forms.Label
        Me.LblInputAName = New System.Windows.Forms.Label
        Me.LblInputBVar = New System.Windows.Forms.Label
        Me.LblInputB = New System.Windows.Forms.Label
        Me.LblInputAVar = New System.Windows.Forms.Label
        Me.LblInputA = New System.Windows.Forms.Label
        Me.LblMapSize = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblMapDimension = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileDialog_Import_CalData = New System.Windows.Forms.OpenFileDialog
        Me.GroupBoxCDF = New System.Windows.Forms.GroupBox
        Me.LblCALDataFileName = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.LblChecksum = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.LblECUType = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.LblFWLockState = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.LblModel = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.LblVIN = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.LblSoftwareVersion = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.TSBCalOpen = New System.Windows.Forms.ToolStripButton
        Me.TSBCalDataOpen = New System.Windows.Forms.ToolStripButton
        Me.TSBConnect = New System.Windows.Forms.ToolStripButton
        Me.TSBDatalogger = New System.Windows.Forms.ToolStripButton
        Me.TBSearch = New System.Windows.Forms.TextBox
        Me.BSearch = New System.Windows.Forms.Button
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBoxMD.SuspendLayout()
        Me.GroupBoxCDF.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TVMain
        '
        Me.TVMain.Location = New System.Drawing.Point(0, 82)
        Me.TVMain.Name = "TVMain"
        Me.TVMain.Size = New System.Drawing.Size(546, 467)
        Me.TVMain.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ECUToolStripMenuItem, Me.DataLoggerToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1096, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadCalDefinitionMenuItem, Me.LoadCalDataMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'LoadCalDefinitionMenuItem
        '
        Me.LoadCalDefinitionMenuItem.Name = "LoadCalDefinitionMenuItem"
        Me.LoadCalDefinitionMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.LoadCalDefinitionMenuItem.Text = "Load Cal Definition"
        '
        'LoadCalDataMenuItem
        '
        Me.LoadCalDataMenuItem.Enabled = False
        Me.LoadCalDataMenuItem.Name = "LoadCalDataMenuItem"
        Me.LoadCalDataMenuItem.Size = New System.Drawing.Size(174, 22)
        Me.LoadCalDataMenuItem.Text = "Load Cal Data"
        '
        'ECUToolStripMenuItem
        '
        Me.ECUToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConnectToolStripMenuItem})
        Me.ECUToolStripMenuItem.Name = "ECUToolStripMenuItem"
        Me.ECUToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.ECUToolStripMenuItem.Text = "ECU"
        '
        'ConnectToolStripMenuItem
        '
        Me.ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem"
        Me.ConnectToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.ConnectToolStripMenuItem.Text = "Connect"
        '
        'DataLoggerToolStripMenuItem
        '
        Me.DataLoggerToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataLoggerOpenMenuItem})
        Me.DataLoggerToolStripMenuItem.Name = "DataLoggerToolStripMenuItem"
        Me.DataLoggerToolStripMenuItem.Size = New System.Drawing.Size(78, 20)
        Me.DataLoggerToolStripMenuItem.Text = "Data Logger"
        '
        'DataLoggerOpenMenuItem
        '
        Me.DataLoggerOpenMenuItem.Enabled = False
        Me.DataLoggerOpenMenuItem.Name = "DataLoggerOpenMenuItem"
        Me.DataLoggerOpenMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.DataLoggerOpenMenuItem.Text = "Open"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ReLoadDef
        '
        Me.ReLoadDef.Location = New System.Drawing.Point(10, 253)
        Me.ReLoadDef.Name = "ReLoadDef"
        Me.ReLoadDef.Size = New System.Drawing.Size(142, 23)
        Me.ReLoadDef.TabIndex = 2
        Me.ReLoadDef.Text = "Reload Cal Defintion File"
        Me.ReLoadDef.UseVisualStyleBackColor = True
        '
        'GroupBoxMD
        '
        Me.GroupBoxMD.Controls.Add(Me.LblLUTRCalc)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTBCalc)
        Me.GroupBoxMD.Controls.Add(Me.ReLoadDef)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTACalc)
        Me.GroupBoxMD.Controls.Add(Me.LblMapBits)
        Me.GroupBoxMD.Controls.Add(Me.Label8)
        Me.GroupBoxMD.Controls.Add(Me.LblXref)
        Me.GroupBoxMD.Controls.Add(Me.Label7)
        Me.GroupBoxMD.Controls.Add(Me.LblFunc)
        Me.GroupBoxMD.Controls.Add(Me.Label4)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTRName)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTRAddr)
        Me.GroupBoxMD.Controls.Add(Me.Label6)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTBName)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTBAddr)
        Me.GroupBoxMD.Controls.Add(Me.Label5)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTAName)
        Me.GroupBoxMD.Controls.Add(Me.LblLUTAAddr)
        Me.GroupBoxMD.Controls.Add(Me.LblLookupA)
        Me.GroupBoxMD.Controls.Add(Me.LblInputBName)
        Me.GroupBoxMD.Controls.Add(Me.LblInputAName)
        Me.GroupBoxMD.Controls.Add(Me.LblInputBVar)
        Me.GroupBoxMD.Controls.Add(Me.LblInputB)
        Me.GroupBoxMD.Controls.Add(Me.LblInputAVar)
        Me.GroupBoxMD.Controls.Add(Me.LblInputA)
        Me.GroupBoxMD.Controls.Add(Me.LblMapSize)
        Me.GroupBoxMD.Controls.Add(Me.Label3)
        Me.GroupBoxMD.Controls.Add(Me.LblMapDimension)
        Me.GroupBoxMD.Controls.Add(Me.Label1)
        Me.GroupBoxMD.Enabled = False
        Me.GroupBoxMD.Location = New System.Drawing.Point(552, 233)
        Me.GroupBoxMD.Name = "GroupBoxMD"
        Me.GroupBoxMD.Size = New System.Drawing.Size(536, 282)
        Me.GroupBoxMD.TabIndex = 3
        Me.GroupBoxMD.TabStop = False
        Me.GroupBoxMD.Text = "Map Definition"
        '
        'LblLUTRCalc
        '
        Me.LblLUTRCalc.AutoSize = True
        Me.LblLUTRCalc.Location = New System.Drawing.Point(97, 220)
        Me.LblLUTRCalc.Name = "LblLUTRCalc"
        Me.LblLUTRCalc.Size = New System.Drawing.Size(71, 13)
        Me.LblLUTRCalc.TabIndex = 27
        Me.LblLUTRCalc.Text = "LblLUTRCalc"
        '
        'LblLUTBCalc
        '
        Me.LblLUTBCalc.AutoSize = True
        Me.LblLUTBCalc.Location = New System.Drawing.Point(96, 187)
        Me.LblLUTBCalc.Name = "LblLUTBCalc"
        Me.LblLUTBCalc.Size = New System.Drawing.Size(70, 13)
        Me.LblLUTBCalc.TabIndex = 26
        Me.LblLUTBCalc.Text = "LblLUTBCalc"
        '
        'LblLUTACalc
        '
        Me.LblLUTACalc.AutoSize = True
        Me.LblLUTACalc.Location = New System.Drawing.Point(97, 155)
        Me.LblLUTACalc.Name = "LblLUTACalc"
        Me.LblLUTACalc.Size = New System.Drawing.Size(70, 13)
        Me.LblLUTACalc.TabIndex = 25
        Me.LblLUTACalc.Text = "LblLUTACalc"
        '
        'LblMapBits
        '
        Me.LblMapBits.AutoSize = True
        Me.LblMapBits.Location = New System.Drawing.Point(97, 46)
        Me.LblMapBits.Name = "LblMapBits"
        Me.LblMapBits.Size = New System.Drawing.Size(59, 13)
        Me.LblMapBits.TabIndex = 24
        Me.LblMapBits.Text = "LblMapBits"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Map Bits:"
        '
        'LblXref
        '
        Me.LblXref.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblXref.Location = New System.Drawing.Point(97, 72)
        Me.LblXref.Name = "LblXref"
        Me.LblXref.Size = New System.Drawing.Size(403, 31)
        Me.LblXref.TabIndex = 22
        Me.LblXref.Text = "LblXref"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Cross Ref:"
        '
        'LblFunc
        '
        Me.LblFunc.AutoSize = True
        Me.LblFunc.Location = New System.Drawing.Point(97, 59)
        Me.LblFunc.Name = "LblFunc"
        Me.LblFunc.Size = New System.Drawing.Size(45, 13)
        Me.LblFunc.TabIndex = 20
        Me.LblFunc.Text = "LblFunc"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Lookup Function:"
        '
        'LblLUTRName
        '
        Me.LblLUTRName.AutoSize = True
        Me.LblLUTRName.Location = New System.Drawing.Point(171, 207)
        Me.LblLUTRName.Name = "LblLUTRName"
        Me.LblLUTRName.Size = New System.Drawing.Size(78, 13)
        Me.LblLUTRName.TabIndex = 18
        Me.LblLUTRName.Text = "LblLUTRName"
        '
        'LblLUTRAddr
        '
        Me.LblLUTRAddr.AutoSize = True
        Me.LblLUTRAddr.Location = New System.Drawing.Point(97, 207)
        Me.LblLUTRAddr.Name = "LblLUTRAddr"
        Me.LblLUTRAddr.Size = New System.Drawing.Size(72, 13)
        Me.LblLUTRAddr.TabIndex = 17
        Me.LblLUTRAddr.Text = "LblLUTRAddr"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 207)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Output  R:"
        '
        'LblLUTBName
        '
        Me.LblLUTBName.AutoSize = True
        Me.LblLUTBName.Location = New System.Drawing.Point(170, 174)
        Me.LblLUTBName.Name = "LblLUTBName"
        Me.LblLUTBName.Size = New System.Drawing.Size(77, 13)
        Me.LblLUTBName.TabIndex = 15
        Me.LblLUTBName.Text = "LblLUTBName"
        '
        'LblLUTBAddr
        '
        Me.LblLUTBAddr.AutoSize = True
        Me.LblLUTBAddr.Location = New System.Drawing.Point(96, 174)
        Me.LblLUTBAddr.Name = "LblLUTBAddr"
        Me.LblLUTBAddr.Size = New System.Drawing.Size(71, 13)
        Me.LblLUTBAddr.TabIndex = 14
        Me.LblLUTBAddr.Text = "LblLUTBAddr"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Lookup B:"
        '
        'LblLUTAName
        '
        Me.LblLUTAName.AutoSize = True
        Me.LblLUTAName.Location = New System.Drawing.Point(171, 142)
        Me.LblLUTAName.Name = "LblLUTAName"
        Me.LblLUTAName.Size = New System.Drawing.Size(77, 13)
        Me.LblLUTAName.TabIndex = 12
        Me.LblLUTAName.Text = "LblLUTAName"
        '
        'LblLUTAAddr
        '
        Me.LblLUTAAddr.AutoSize = True
        Me.LblLUTAAddr.Location = New System.Drawing.Point(97, 142)
        Me.LblLUTAAddr.Name = "LblLUTAAddr"
        Me.LblLUTAAddr.Size = New System.Drawing.Size(71, 13)
        Me.LblLUTAAddr.TabIndex = 11
        Me.LblLUTAAddr.Text = "LblLUTAAddr"
        '
        'LblLookupA
        '
        Me.LblLookupA.AutoSize = True
        Me.LblLookupA.Location = New System.Drawing.Point(7, 142)
        Me.LblLookupA.Name = "LblLookupA"
        Me.LblLookupA.Size = New System.Drawing.Size(56, 13)
        Me.LblLookupA.TabIndex = 10
        Me.LblLookupA.Text = "Lookup A:"
        '
        'LblInputBName
        '
        Me.LblInputBName.AutoSize = True
        Me.LblInputBName.Location = New System.Drawing.Point(171, 116)
        Me.LblInputBName.Name = "LblInputBName"
        Me.LblInputBName.Size = New System.Drawing.Size(80, 13)
        Me.LblInputBName.TabIndex = 9
        Me.LblInputBName.Text = "LblInputBName"
        '
        'LblInputAName
        '
        Me.LblInputAName.AutoSize = True
        Me.LblInputAName.Location = New System.Drawing.Point(171, 103)
        Me.LblInputAName.Name = "LblInputAName"
        Me.LblInputAName.Size = New System.Drawing.Size(80, 13)
        Me.LblInputAName.TabIndex = 8
        Me.LblInputAName.Text = "LblInputAName"
        '
        'LblInputBVar
        '
        Me.LblInputBVar.AutoSize = True
        Me.LblInputBVar.Location = New System.Drawing.Point(97, 116)
        Me.LblInputBVar.Name = "LblInputBVar"
        Me.LblInputBVar.Size = New System.Drawing.Size(68, 13)
        Me.LblInputBVar.TabIndex = 7
        Me.LblInputBVar.Text = "LblInputBVar"
        '
        'LblInputB
        '
        Me.LblInputB.AutoSize = True
        Me.LblInputB.Location = New System.Drawing.Point(7, 116)
        Me.LblInputB.Name = "LblInputB"
        Me.LblInputB.Size = New System.Drawing.Size(44, 13)
        Me.LblInputB.TabIndex = 6
        Me.LblInputB.Text = "Input B:"
        '
        'LblInputAVar
        '
        Me.LblInputAVar.AutoSize = True
        Me.LblInputAVar.Location = New System.Drawing.Point(97, 103)
        Me.LblInputAVar.Name = "LblInputAVar"
        Me.LblInputAVar.Size = New System.Drawing.Size(68, 13)
        Me.LblInputAVar.TabIndex = 5
        Me.LblInputAVar.Text = "LblInputAVar"
        '
        'LblInputA
        '
        Me.LblInputA.AutoSize = True
        Me.LblInputA.Location = New System.Drawing.Point(7, 103)
        Me.LblInputA.Name = "LblInputA"
        Me.LblInputA.Size = New System.Drawing.Size(44, 13)
        Me.LblInputA.TabIndex = 4
        Me.LblInputA.Text = "Input A:"
        '
        'LblMapSize
        '
        Me.LblMapSize.AutoSize = True
        Me.LblMapSize.Location = New System.Drawing.Point(97, 33)
        Me.LblMapSize.Name = "LblMapSize"
        Me.LblMapSize.Size = New System.Drawing.Size(62, 13)
        Me.LblMapSize.TabIndex = 3
        Me.LblMapSize.Text = "LblMapSize"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Map Size:"
        '
        'LblMapDimension
        '
        Me.LblMapDimension.AutoSize = True
        Me.LblMapDimension.Location = New System.Drawing.Point(97, 20)
        Me.LblMapDimension.Name = "LblMapDimension"
        Me.LblMapDimension.Size = New System.Drawing.Size(91, 13)
        Me.LblMapDimension.TabIndex = 1
        Me.LblMapDimension.Text = "LblMapDimension"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Map Dimension:"
        '
        'OpenFileDialog_Import_CalData
        '
        Me.OpenFileDialog_Import_CalData.FileName = "OpenFileDialog1"
        '
        'GroupBoxCDF
        '
        Me.GroupBoxCDF.Controls.Add(Me.LblCALDataFileName)
        Me.GroupBoxCDF.Controls.Add(Me.Label14)
        Me.GroupBoxCDF.Controls.Add(Me.LblChecksum)
        Me.GroupBoxCDF.Controls.Add(Me.Label13)
        Me.GroupBoxCDF.Controls.Add(Me.LblECUType)
        Me.GroupBoxCDF.Controls.Add(Me.Label10)
        Me.GroupBoxCDF.Controls.Add(Me.LblFWLockState)
        Me.GroupBoxCDF.Controls.Add(Me.Label12)
        Me.GroupBoxCDF.Controls.Add(Me.LblModel)
        Me.GroupBoxCDF.Controls.Add(Me.Label11)
        Me.GroupBoxCDF.Controls.Add(Me.LblVIN)
        Me.GroupBoxCDF.Controls.Add(Me.Label9)
        Me.GroupBoxCDF.Controls.Add(Me.LblSoftwareVersion)
        Me.GroupBoxCDF.Controls.Add(Me.Label2)
        Me.GroupBoxCDF.Enabled = False
        Me.GroupBoxCDF.Location = New System.Drawing.Point(553, 101)
        Me.GroupBoxCDF.Name = "GroupBoxCDF"
        Me.GroupBoxCDF.Size = New System.Drawing.Size(535, 117)
        Me.GroupBoxCDF.TabIndex = 4
        Me.GroupBoxCDF.TabStop = False
        Me.GroupBoxCDF.Text = "Cal Data File"
        '
        'LblCALDataFileName
        '
        Me.LblCALDataFileName.AutoSize = True
        Me.LblCALDataFileName.Location = New System.Drawing.Point(102, 94)
        Me.LblCALDataFileName.Name = "LblCALDataFileName"
        Me.LblCALDataFileName.Size = New System.Drawing.Size(108, 13)
        Me.LblCALDataFileName.TabIndex = 40
        Me.LblCALDataFileName.Text = "LblCALDataFileName"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(7, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(52, 13)
        Me.Label14.TabIndex = 39
        Me.Label14.Text = "Filename:"
        '
        'LblChecksum
        '
        Me.LblChecksum.AutoSize = True
        Me.LblChecksum.Location = New System.Drawing.Point(102, 81)
        Me.LblChecksum.Name = "LblChecksum"
        Me.LblChecksum.Size = New System.Drawing.Size(71, 13)
        Me.LblChecksum.TabIndex = 38
        Me.LblChecksum.Text = "LblChecksum"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 81)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(60, 13)
        Me.Label13.TabIndex = 37
        Me.Label13.Text = "Checksum:"
        '
        'LblECUType
        '
        Me.LblECUType.AutoSize = True
        Me.LblECUType.Location = New System.Drawing.Point(102, 29)
        Me.LblECUType.Name = "LblECUType"
        Me.LblECUType.Size = New System.Drawing.Size(67, 13)
        Me.LblECUType.TabIndex = 36
        Me.LblECUType.Text = "LblECUType"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "ECU Type:"
        '
        'LblFWLockState
        '
        Me.LblFWLockState.AutoSize = True
        Me.LblFWLockState.Location = New System.Drawing.Point(102, 68)
        Me.LblFWLockState.Name = "LblFWLockState"
        Me.LblFWLockState.Size = New System.Drawing.Size(87, 13)
        Me.LblFWLockState.TabIndex = 34
        Me.LblFWLockState.Text = "LblFWLockState"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 68)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(56, 13)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Unlocked:"
        '
        'LblModel
        '
        Me.LblModel.AutoSize = True
        Me.LblModel.Location = New System.Drawing.Point(102, 55)
        Me.LblModel.Name = "LblModel"
        Me.LblModel.Size = New System.Drawing.Size(50, 13)
        Me.LblModel.TabIndex = 32
        Me.LblModel.Text = "LblModel"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(39, 13)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Model:"
        '
        'LblVIN
        '
        Me.LblVIN.AutoSize = True
        Me.LblVIN.Location = New System.Drawing.Point(102, 42)
        Me.LblVIN.Name = "LblVIN"
        Me.LblVIN.Size = New System.Drawing.Size(39, 13)
        Me.LblVIN.TabIndex = 30
        Me.LblVIN.Text = "LblVIN"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 42)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "VIN (base):"
        '
        'LblSoftwareVersion
        '
        Me.LblSoftwareVersion.AutoSize = True
        Me.LblSoftwareVersion.Location = New System.Drawing.Point(102, 16)
        Me.LblSoftwareVersion.Name = "LblSoftwareVersion"
        Me.LblSoftwareVersion.Size = New System.Drawing.Size(98, 13)
        Me.LblSoftwareVersion.TabIndex = 28
        Me.LblSoftwareVersion.Text = "LblSoftwareVersion"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Software Version:"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.TSBCalOpen, Me.TSBCalDataOpen, Me.TSBConnect, Me.TSBDatalogger})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1096, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'TSBCalOpen
        '
        Me.TSBCalOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBCalOpen.Image = Global.T4e_ECU_Editor.My.Resources.Resources.cal_open_icon
        Me.TSBCalOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBCalOpen.Name = "TSBCalOpen"
        Me.TSBCalOpen.Size = New System.Drawing.Size(23, 22)
        Me.TSBCalOpen.Text = "TSBCalOpen"
        Me.TSBCalOpen.ToolTipText = "Load Calibration Definition File (.xml)"
        '
        'TSBCalDataOpen
        '
        Me.TSBCalDataOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBCalDataOpen.Enabled = False
        Me.TSBCalDataOpen.Image = Global.T4e_ECU_Editor.My.Resources.Resources.binary_open_icon
        Me.TSBCalDataOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBCalDataOpen.Name = "TSBCalDataOpen"
        Me.TSBCalDataOpen.Size = New System.Drawing.Size(23, 22)
        Me.TSBCalDataOpen.Text = "TSBCalDataOpen"
        Me.TSBCalDataOpen.ToolTipText = "Load Calibration Data File (.t4e)"
        '
        'TSBConnect
        '
        Me.TSBConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBConnect.Image = Global.T4e_ECU_Editor.My.Resources.Resources.plug_disconnect_icon
        Me.TSBConnect.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBConnect.Name = "TSBConnect"
        Me.TSBConnect.Size = New System.Drawing.Size(23, 22)
        Me.TSBConnect.Text = "TSBConnect"
        Me.TSBConnect.ToolTipText = "Connect to ECU"
        '
        'TSBDatalogger
        '
        Me.TSBDatalogger.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSBDatalogger.Enabled = False
        Me.TSBDatalogger.Image = Global.T4e_ECU_Editor.My.Resources.Resources.chart
        Me.TSBDatalogger.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBDatalogger.Name = "TSBDatalogger"
        Me.TSBDatalogger.Size = New System.Drawing.Size(23, 22)
        Me.TSBDatalogger.Text = "TSBDatalogger"
        Me.TSBDatalogger.ToolTipText = "Show the Datalogger window"
        '
        'TBSearch
        '
        Me.TBSearch.Location = New System.Drawing.Point(12, 56)
        Me.TBSearch.Name = "TBSearch"
        Me.TBSearch.Size = New System.Drawing.Size(310, 20)
        Me.TBSearch.TabIndex = 6
        '
        'BSearch
        '
        Me.BSearch.Location = New System.Drawing.Point(328, 54)
        Me.BSearch.Name = "BSearch"
        Me.BSearch.Size = New System.Drawing.Size(75, 23)
        Me.BSearch.TabIndex = 7
        Me.BSearch.Text = "Search"
        Me.BSearch.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1096, 549)
        Me.Controls.Add(Me.BSearch)
        Me.Controls.Add(Me.TBSearch)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBoxCDF)
        Me.Controls.Add(Me.GroupBoxMD)
        Me.Controls.Add(Me.TVMain)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "T4e ECU Editor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBoxMD.ResumeLayout(False)
        Me.GroupBoxMD.PerformLayout()
        Me.GroupBoxCDF.ResumeLayout(False)
        Me.GroupBoxCDF.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TVMain As System.Windows.Forms.TreeView
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadCalDefinitionMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog_Import_CALDef As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ReLoadDef As System.Windows.Forms.Button
    Friend WithEvents GroupBoxMD As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LblMapDimension As System.Windows.Forms.Label
    Friend WithEvents LblMapSize As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LblInputBName As System.Windows.Forms.Label
    Friend WithEvents LblInputAName As System.Windows.Forms.Label
    Friend WithEvents LblInputBVar As System.Windows.Forms.Label
    Friend WithEvents LblInputB As System.Windows.Forms.Label
    Friend WithEvents LblInputAVar As System.Windows.Forms.Label
    Friend WithEvents LblInputA As System.Windows.Forms.Label
    Friend WithEvents LblLUTBName As System.Windows.Forms.Label
    Friend WithEvents LblLUTBAddr As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LblLUTAName As System.Windows.Forms.Label
    Friend WithEvents LblLUTAAddr As System.Windows.Forms.Label
    Friend WithEvents LblLookupA As System.Windows.Forms.Label
    Friend WithEvents LblLUTRName As System.Windows.Forms.Label
    Friend WithEvents LblLUTRAddr As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LblXref As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LblFunc As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LblMapBits As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents LblLUTRCalc As System.Windows.Forms.Label
    Friend WithEvents LblLUTBCalc As System.Windows.Forms.Label
    Friend WithEvents LblLUTACalc As System.Windows.Forms.Label
    Friend WithEvents LoadCalDataMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog_Import_CalData As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBoxCDF As System.Windows.Forms.GroupBox
    Friend WithEvents LblSoftwareVersion As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LblVIN As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents LblModel As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents LblFWLockState As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents LblECUType As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LblChecksum As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LblCALDataFileName As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents DataLoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataLoggerOpenMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ECUToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConnectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TSBConnect As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBDatalogger As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBCalOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBCalDataOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TBSearch As System.Windows.Forms.TextBox
    Friend WithEvents BSearch As System.Windows.Forms.Button

End Class
