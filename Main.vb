Imports System.Xml
Imports System.IO



Public Class Main    
    Public CFH As New CALFileHandler
    Public MD As New MapDefinitions
    Public Config As XMLConfiguration
    Dim CALBytes As Byte()
    Public CalFilePath As String
    Dim CALDataFilePath As String
    Dim TVMainSearch As TVSearch

    ' new
    Private T4eReg As New T4eRegistry


    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        HideLabels()

        If (T4eReg.GetECUAutoConnect) Then
            If (Not ECU.Adapter.isConnected) Then
                ECUConnect.ConnectToECU(T4eReg.GetECUComPort)
            End If
        End If

        ' load last saved CALFile
        CalFilePath = T4eReg.GetLastOpenCALFile
        If (CalFilePath <> "") Then
            LoadMapDefintions(CalFilePath)
            LoadCalDataMenuItem.Enabled = True
            TSBCalDataOpen.Enabled = True
            DataLoggerOpenMenuItem.Enabled = True
            TSBDatalogger.Enabled = True
            GroupBoxMD.Enabled = True
        End If
        ' load last saved CAlDataFile
        CALDataFilePath = T4eReg.GetLastOpenCALDataFile
        If (CALDataFilePath <> "") Then
            ' load CAL Data file
            CALBytes = CFH.LoadCALFile(CALDataFilePath, Config)
            ' if successful extract some useful information from it                
            GroupBoxCDF.Enabled = True
            LblECUType.Text = CFH.GetECUType
            LblSoftwareVersion.Text = CFH.GetVersion
            LblVIN.Text = CFH.GetVIN
            LblModel.Text = CFH.GetModel
            If CFH.GetFWLockState Then
                LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Regular)
                LblFWLockState.Text = "locked"
            Else
                LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Bold)
                LblFWLockState.Text = "unlocked"
            End If
            ' compare stored with calculated checksum
            If (CFH.GetCalculatedChecksum() = CFH.GetStoredChecksum()) Then
                LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (matching)"
            Else
                LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (mismatch 0x" & Hex(CFH.GetCalculatedChecksum()) & ")"
            End If
            LblCALDataFileName.Text = Path.GetFileName(CALDataFilePath)
            Me.Text = "T4e ECU Editor - " & CFH.GetModel

            T4eReg.SetLastOpenCALDataFile(CALDataFilePath)
        End If
    End Sub

    ' remove label texts during runtime
    Private Sub HideLabels()
        ' Cal Data File
        LblECUType.Text = ""
        LblFWLockState.Text = ""
        LblSoftwareVersion.Text = ""
        LblModel.Text = ""
        LblVIN.Text = ""
        LblChecksum.Text = ""
        LblCALDataFileName.Text = ""
        ' Cal XML File        
        LblInputAVar.Text = ""
        LblInputBVar.Text = ""
        LblInputAName.Text = ""
        LblInputBName.Text = ""
        LblLUTAName.Text = ""
        LblLUTBName.Text = ""
        LblLUTRName.Text = ""
        LblLUTAAddr.Text = ""
        LblLUTBAddr.Text = ""
        LblLUTRAddr.Text = ""
        LblLUTACalc.Text = ""
        LblLUTBCalc.Text = ""
        LblLUTRCalc.Text = ""
        LblMapBits.Text = ""
        LblMapDimension.Text = ""
        LblMapSize.Text = ""
        LblFunc.Text = ""
        LblXref.Text = ""
    End Sub
    Private Sub ImportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadCalDefinitionMenuItem.Click
        OpenFileDialog_Import_CALDef.Filter = "Calibration Definition (*.xml)|*.xml"
        OpenFileDialog_Import_CALDef.FilterIndex = 1
        If OpenFileDialog_Import_CALDef.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            CalFilePath = OpenFileDialog_Import_CALDef.FileName
            LoadMapDefintions(OpenFileDialog_Import_CALDef.FileName)
            LoadCalDataMenuItem.Enabled = True
            DataLoggerOpenMenuItem.Enabled = True
            TSBDatalogger.Enabled = True
            GroupBoxMD.Enabled = True
            T4eReg.SetLastOpenCALFile(CalFilePath)
        End If
    End Sub

    Private Sub LoadMapDefintions(ByVal filename As String)
        Dim Maps As List(Of XMLMapDefinition)
        Dim TVanker As TreeNode = Nothing
        Dim Nodename As String = ""
        Dim Nodename_by As String = ""

        TVMain.Nodes.Clear()
        TVMain.Nodes.Add("root", "T4e Maps (" & filename & ")")
        TVMain.Nodes.Item("root").Nodes.Add("1d_maps", "1D Maps")
        TVMain.Nodes.Item("root").Nodes.Add("2d_maps", "2D Maps")
        TVMain.Nodes.Item("root").Nodes.Add("3d_maps", "3D Maps")

        MD.LoadFromXML(filename)
        Config = MD.GetConfig
        Maps = MD.GetMaps()

        For Each map As XMLMapDefinition In Maps
            '  Console.WriteLine(map.func)
            If (map.dimension = 1) Then
                TVanker = TVMain.Nodes.Find("1d_maps", True)(0)
            End If
            If (map.dimension = 2) Then
                TVanker = TVMain.Nodes.Find("2d_maps", True)(0)
            End If
            If (map.dimension = 3) Then
                TVanker = TVMain.Nodes.Find("3d_maps", True)(0)
            End If
            If (map.output_name IsNot Nothing) Then
                Nodename = map.output_name & " (" & map.xsize & "x" & map.ysize & ")"
            Else
                If (map.input_a_name IsNot Nothing) Then
                    Nodename_by = "   [" & map.input_a_name & "]"
                End If
                If (map.input_b_name IsNot Nothing) Then
                    Nodename_by &= " X [" & map.input_b_name & "]"
                End If
                Nodename = map.lut_r_name & Nodename_by & "  (" & map.xsize & "x" & map.ysize & ")"
            End If
            If (TVanker IsNot Nothing) Then
                TVanker.Nodes.Add(map.num, Nodename)
            End If
        Next map
        TVMain.ExpandAll()

        ' setup searcher
        TVMainSearch = New TVSearch(TVMain)

    End Sub

    Private Sub TVMain_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TVMain.AfterSelect
        If IsNumeric(e.Node.Name) Then
            MapInfosUpdate(e.Node.Name)
        End If
    End Sub

    Private Sub TVMain_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TVMain.NodeMouseDoubleClick
        Dim MapDG As New MapDataGrid
        Dim Maps As List(Of XMLMapDefinition)
        Dim map As XMLMapDefinition

        If (IsNumeric(e.Node.Name) And CALBytes IsNot Nothing) Then
            Maps = MD.GetMaps()
            map = Maps(e.Node.Name)
            MapDG.LoadMap(Config, map, CFH)
        Else
            MsgBox("To view a map you need to load a Cal Data file first.")
        End If
    End Sub

    Private Sub LoadXML_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReLoadDef.Click
        If (CalFilePath IsNot Nothing) Then
            LoadMapDefintions(CalFilePath)
        End If
    End Sub

    Private Sub MapInfosUpdate(ByVal node_name As Integer)
        Dim Maps As List(Of XMLMapDefinition)
        Dim map As XMLMapDefinition

        If (IsNumeric(node_name)) Then
            Maps = MD.GetMaps()
            map = Maps(node_name)
            LblMapDimension.Text = map.dimension
            LblMapSize.Text = map.xsize & "x" & map.ysize
            LblMapBits.Text = map.bits
            LblFunc.Text = map.func
            LblXref.Text = map.xref_result
            LblInputAVar.Text = "0x" & Hex(map.input_a)
            LblInputAName.Text = map.input_a_name
            ' make potentially unused labels invisible
            LblLUTAAddr.Visible = False
            LblLUTAName.Visible = False
            LblLUTBAddr.Visible = False
            LblLUTBName.Visible = False
            LblInputBName.Visible = False
            LblInputBVar.Visible = False
            LblLUTACalc.Visible = False
            LblLUTBCalc.Visible = False
            LblLUTRCalc.Visible = False
            If (map.lut_a_calc IsNot Nothing) Then
                LblLUTACalc.Text = "Calc: " & map.lut_a_calc
                LblLUTACalc.Visible = True
            End If
            If (map.lut_b_calc IsNot Nothing) Then
                LblLUTBCalc.Text = "Calc: " & map.lut_b_calc
                LblLUTBCalc.Visible = True
            End If
            If (map.lut_r_calc IsNot Nothing) Then
                LblLUTRCalc.Text = "Calc: " & map.lut_r_calc
                LblLUTRCalc.Visible = True
            End If
            If (map.dimension >= 2) Then
                LblLUTAAddr.Text = "0x" & Hex(map.lut_a)
                LblLUTAName.Text = map.lut_a_name
                LblLUTAAddr.Visible = True
                LblLUTAName.Visible = True
            End If
            If (map.dimension = 3) Then
                LblInputBVar.Text = "0x" & Hex(map.input_b)
                LblInputBName.Text = map.input_b_name
                LblLUTBAddr.Text = "0x" & Hex(map.lut_b)
                LblLUTBName.Text = map.lut_b_name
                LblLUTBAddr.Visible = True
                LblLUTBName.Visible = True
                LblInputBName.Visible = True
                LblInputBVar.Visible = True
            End If
            LblLUTRAddr.Text = "0x" & Hex(map.lut_r)
            LblLUTRName.Text = map.lut_r_name
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub LoadCalDataMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadCalDataMenuItem.Click
        OpenFileDialog_Import_CalData.Filter = "Calibration Data (*.t4e)|*.t4e"
        OpenFileDialog_Import_CalData.FilterIndex = 1
        If OpenFileDialog_Import_CalData.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            CALDataFilePath = OpenFileDialog_Import_CalData.FileName
            ' load CAL Data file
            CALBytes = CFH.LoadCALFile(CALDataFilePath, Config)
            ' if successful extract some useful information from it
            GroupBoxCDF.Enabled = False
            If (CALBytes IsNot Nothing) Then

                GroupBoxCDF.Enabled = True
                LblECUType.Text = CFH.GetECUType
                LblSoftwareVersion.Text = CFH.GetVersion
                LblVIN.Text = CFH.GetVIN
                LblModel.Text = CFH.GetModel
                If CFH.GetFWLockState Then
                    LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Regular)
                    LblFWLockState.Text = "locked"
                Else
                    LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Bold)
                    LblFWLockState.Text = "unlocked"
                End If


                ' compare stored with calculated checksum
                If (CFH.GetCalculatedChecksum() = CFH.GetStoredChecksum()) Then
                    LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (matching)"
                Else
                    LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (mismatch 0x" & Hex(CFH.GetCalculatedChecksum()) & ")"
                End If

                LblCALDataFileName.Text = Path.GetFileName(CALDataFilePath)
                Me.Text = "T4e ECU Editor - " & CFH.GetModel

                T4eReg.SetLastOpenCALDataFile(CALDataFilePath)
            Else
                MsgBox("Error loading Calibration Data File")
            End If
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox.Show()
    End Sub

    Private Sub DataLoggerOpenMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataLoggerOpenMenuItem.Click
        If Not ECU.Adapter.isConnected Then
            Dim result As DialogResult = MessageBox.Show("The Datalogger needs a connection to the ECU first." & vbCrLf _
                                                         & "Do you want to connect now ?", _
                                                        "ECU Connection", _
                                                         MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                ECUConnect.ConnectToECU(T4eReg.GetECUComPort)
                Datalogger.Show()
            End If
        Else
            Datalogger.Show()
        End If
    End Sub

    Private Sub ConnectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConnectToolStripMenuItem.Click
        ECUConnect.Show()
    End Sub

    Private Sub TSBConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBConnect.Click
        If (Not ECU.Adapter.isConnected) Then
            ECUConnect.ConnectToECU(T4eReg.GetECUComPort)
        Else
            ECUConnect.DisconnectFromECU()
        End If

        ' update Icon for TSBConnect
        If (ECU.Adapter.isConnected) Then
            TSBConnect.Image = Global.T4e_ECU_Editor.My.Resources.Resources.plug_connect_icon
            TSBConnect.ToolTipText = "Disconnect from ECU"
        Else
            TSBConnect.Image = Global.T4e_ECU_Editor.My.Resources.Resources.plug_disconnect_icon
            TSBConnect.ToolTipText = "Connect to ECU"
        End If
    End Sub

    Private Sub TSBDatalogger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBDatalogger.Click
        If Not ECU.Adapter.isConnected Then
            Dim result As DialogResult = MessageBox.Show("The Datalogger needs a connection to the ECU first." & vbCrLf _
                                                         & "Do you want to connect now ?", _
                                                        "ECU Connection", _
                                                         MessageBoxButtons.YesNo)
            If (result = DialogResult.Yes) Then
                ECUConnect.ConnectToECU(T4eReg.GetECUComPort)
                Datalogger.Show()
            End If
        Else
            Datalogger.Show()
        End If
    End Sub

    Private Sub TSBCalOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBCalOpen.Click
        OpenFileDialog_Import_CALDef.Filter = "Calibration Definition (*.xml)|*.xml"
        OpenFileDialog_Import_CALDef.FilterIndex = 1
        If OpenFileDialog_Import_CALDef.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            CalFilePath = OpenFileDialog_Import_CALDef.FileName
            LoadMapDefintions(OpenFileDialog_Import_CALDef.FileName)
            LoadCalDataMenuItem.Enabled = True
            TSBCalDataOpen.Enabled = True
            DataLoggerOpenMenuItem.Enabled = True
            TSBDatalogger.Enabled = True
            GroupBoxMD.Enabled = True
            T4eReg.SetLastOpenCALFile(CalFilePath)
        End If
    End Sub

    Private Sub TSBCalDataOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBCalDataOpen.Click
        OpenFileDialog_Import_CalData.Filter = "Calibration Data (*.t4e)|*.t4e"
        OpenFileDialog_Import_CalData.FilterIndex = 1
        If OpenFileDialog_Import_CalData.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            CALDataFilePath = OpenFileDialog_Import_CalData.FileName
            ' load CAL Data file
            CALBytes = CFH.LoadCALFile(CALDataFilePath, Config)
            ' if successful extract some useful information from it
            GroupBoxCDF.Enabled = False
            If (CALBytes IsNot Nothing) Then

                GroupBoxCDF.Enabled = True
                LblECUType.Text = CFH.GetECUType
                LblSoftwareVersion.Text = CFH.GetVersion
                LblVIN.Text = CFH.GetVIN
                LblModel.Text = CFH.GetModel
                If CFH.GetFWLockState Then
                    LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Regular)
                    LblFWLockState.Text = "locked"
                Else
                    LblFWLockState.Font = New Font(LblFWLockState.Font, FontStyle.Bold)
                    LblFWLockState.Text = "unlocked"
                End If

               
                ' compare stored with calculated checksum
                If (CFH.GetCalculatedChecksum() = CFH.GetStoredChecksum()) Then
                    LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (matching)"
                Else
                    LblChecksum.Text = "0x" & Hex(CFH.GetStoredChecksum) & " (mismatch 0x" & Hex(CFH.GetCalculatedChecksum()) & ")"
                End If

                LblCALDataFileName.Text = Path.GetFileName(CALDataFilePath)
                Me.Text = "T4e ECU Editor - " & CFH.GetModel

                T4eReg.SetLastOpenCALDataFile(CALDataFilePath)
            Else
                MsgBox("Error loading Calibration Data File")
            End If
        End If
    End Sub

    Private Sub BSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BSearch.Click
        TVMainSearch.FindByText(TBSearch.Text)
    End Sub
End Class
