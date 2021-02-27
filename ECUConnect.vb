Imports System.Management

Public Class ECUConnect

    ' ECU Access Levels
    Enum ECUAccessLevel
        Unknown = 0
        KLINE_CAN = 1
        CANOnlyLocked = 2
        CANOnlyUnlocked = 3
    End Enum

    Private T4eReg As New T4eRegistry
    Private CANSpeed As Integer = 0
    Private ComPortName As String = ""


    Private Sub BConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BConnect.Click

        If (Not ECU.Adapter.isConnected) Then
            ''''''''''''''''''''''''''''''''''''''''''''''
            ' connect to ECU
            ''''''''''''''''''''''''''''''''''''''''''''''
            Dim oComPortName As GenericListItem(Of String) = CType(CBComport.SelectedItem, GenericListItem(Of String))
            Try
                Me.ComPortName = oComPortName.Value.ToString
            Catch ex As Exception
                MessageBox.Show("You have not selected a COM Port", "Invalid COM Port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End Try
            If Me.ComPortName <> "" Then
                ConnectToECU(Me.ComPortName)
                ReadCalibrationDetail()
                DisconnectFromECU()
            End If
        Else
            ''''''''''''''''''''''''''''''''''''''''''''''
            ' disconnect from ECU
            ''''''''''''''''''''''''''''''''''''''''''''''
            DisconnectFromECU()
        End If
    End Sub

    Private Sub ReadCalibrationDetail()
        Dim caldetail As String = ""
        Dim b() As Byte

        If (ECU.AccessLevel = ECU.ECUAccessLevel.CANOnlyUnlocked) Or (ECU.AccessLevel = ECU.ECUAccessLevel.KLINE_CAN) Then
            For i As Integer = 0 To &H20 Step 4
                b = ECU.Adapter.PollType50Bytes(&H10000 + i, 4)
                caldetail = caldetail & System.Text.Encoding.ASCII.GetString(b)
            Next i
            LblCalibrationDetail.Text = caldetail
        Else
            LblCalibrationDetail.Text = "unable to read"
        End If
    End Sub
    '
    ' close serial port, reset adapter version, and access level
    '
    Public Sub DisconnectFromECU()
        ECU.Adapter.Disconnect()        
    End Sub

    '
    ' open serial port, query adapter version, probe access level
    '
    Public Sub ConnectToECU(ByVal ThisComPortName As String)
        ' reset form elements
        ECU.AccessLevel = ECUAccessLevel.Unknown
        UpdateAccessTypeLabel()
        TBAdapterVersion.Text = ""
        TBAdapterVersion.BackColor = Color.LightGray

        If ThisComPortName <> "" Then
            If (ECU.Adapter.Connect(ThisComPortName) = True) Then
                Dim AdapterVersion = ECU.Adapter.GetVersion()
                If (AdapterVersion IsNot Nothing) Then
                    TBAdapterVersion.Text = AdapterVersion
                    If (ECUProbeLevels()) Then
                        TBAdapterVersion.BackColor = Color.LightGreen
                        ' save CAN speed setting in registry / speed is ok (we could read memory, or OBD)
                        T4eReg.SetECUCANSpeed(CANSpeed)
                    End If
                    ' save comport setting in registry / adaper is ok (we could read version)                    
                    T4eReg.SetECUComPort(ThisComPortName)
                End If
            Else
                Me.Show()
            End If
        Else
            MessageBox.Show("You have not selected a COM Port, or the stored COM Port is not available at this time." _
                            & vbCrLf & "Please make sure that the adapter is connected to this computer, and that the igntion is turned on", _
                            "Invalid COM Port", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ChkBECUAutoConnect.Checked = False
        End If
    End Sub
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Test Access Level to ECU 
    ' *) CAN/KLINE ECU
    ' *) CAN only ECU unlocked
    ' *) CAN only ECU locked
    ' return True if we are certain about the access level
    ' return False if we are not sure, or something failed that shouldnt fail
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Function ECUProbeLevels() As Boolean
        Dim b() As Byte
        Dim l As Long

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' Probe OBD 0x22, 0x211 - ECU Type 'T4E'
        ' only CAN ECUs can do that via CAN2USB if that fails assume KLINE_CAN    
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        b = ECU.Adapter.PollTypeOBD(&H22, &H211)
        If (b IsNot Nothing) Then
            ' we got something via OBD, so it cant be a KLINE_CAN ECU
            ' because those dont speak OBD via CAN
        Else
            ' no OBD so should be KLINE and free to read via CAN to verify this we read 0x10000
            ' reply doesnt matter as long as we get something (=! -1)
            l = ECU.Adapter.PollType50(&H10000, 4)
            If (l <> -1) Then
                ECU.AccessLevel = ECUAccessLevel.KLINE_CAN
                UpdateAccessTypeLabel()
                Return (True)
            End If
            ' OBD not working, but CAN memory read also not working
            ' something is foul here
            ECU.AccessLevel = ECUAccessLevel.Unknown
            UpdateAccessTypeLabel()
            Return (False)
        End If

        ' its not KLINE_CAN check if the ECU is unlocked by reading a well known
        ' address 0xA0C which should contain "T4E" as a string
        l = ECU.Adapter.PollType50(&HA0C, 4)
        If (l = -1) Then
            ECU.AccessLevel = ECUAccessLevel.CANOnlyLocked
            UpdateAccessTypeLabel()
            Return (True)
        Else
            Array.Resize(b, 4)
            b(0) = l >> 24 And &HFF
            b(1) = l >> 16 And &HFF
            b(2) = l >> 8 And &HFF
            b(3) = l And &HFF
            If System.Text.Encoding.ASCII.GetString(b).StartsWith("T4E") Then
                ECU.AccessLevel = ECUAccessLevel.CANOnlyUnlocked
                UpdateAccessTypeLabel()
                Return (True)
            End If
        End If
        ' catchall we never reach this hopefully
        ECU.AccessLevel = ECUAccessLevel.Unknown
        UpdateAccessTypeLabel()
        Return (False)
    End Function

    Private Sub ECUConnect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load                
        ''''''''''''''''''''''''''''''
        ' initialize Form elements
        ''''''''''''''''''''''''''''''
        EnumerateComports()
        LoadSetup()
        LblAccessType.Text = ""
        LblCalibrationDetail.Text = ""
        ToolTip.SetToolTip(Me.RBSpeed500K, "500kBit CAN, used for MY08 and later T4e ECU's which only support CAN")
        ToolTip.SetToolTip(Me.RBSpeed1M, "1MBit CAN, used for MY07 and earlier T4e ECU's which also support K-LINE")
        ToolTip.SetToolTip(Me.RBSpeedAuto, "Try to to autosense CAN Speed")
        ToolTip.SetToolTip(Me.BComportListReload, "re-enumerate all available COM Ports")                
    End Sub

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Load setup from saved registry values
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub LoadSetup()
        '''''''''''''''''''''''''''''''''''''''
        ' set COM Port to stored settings
        '''''''''''''''''''''''''''''''''''''''
        Dim COMPort As String = T4eReg.GetECUComPort()
        CBComport.ValueMember = COMPort
        If (COMPort IsNot "") Then
            For i As Integer = 0 To CBComport.Items.Count - 1
                Dim oComPortName As GenericListItem(Of String) = CType(CBComport.Items.Item(i), GenericListItem(Of String))
                If oComPortName.Value.ToString = COMPort Then
                    CBComport.SelectedIndex = i
                    Me.ComPortName = oComPortName.Value.ToString
                    Exit For
                End If
            Next
        Else
            ' no com port name saved in registry yet
        End If
        '''''''''''''''''''''''''''''''''''''''
        ' set CAN Speed to stored settings
        '''''''''''''''''''''''''''''''''''''''
        If T4eReg.GetECUCANSpeed() = 1000 Then
            RBSpeed1M.Checked = True
        End If
        If T4eReg.GetECUCANSpeed() = 500 Then
            RBSpeed500K.Checked = True
        End If
        If T4eReg.GetECUCANSpeed() = 0 Then
            RBSpeedAuto.Checked = True
        End If
        '''''''''''''''''''''''''''''''''''''''
        ' set Auto Connect to stored settings
        '''''''''''''''''''''''''''''''''''''''
        If T4eReg.GetECUAutoConnect Then
            ChkBECUAutoConnect.Checked = True
        End If
    End Sub
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' get list of COM ports and attached devices and populate CBComport
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Private Sub EnumerateComports()
        CBComport.Text = "<select COM Port>"
        CBComport.Items.Clear()
        Try
            Dim searcher As New Management.ManagementObjectSearcher("root\cimv2", "SELECT * FROM Win32_SerialPort")
            Dim name, comportname As String

            For Each queryObj As Management.ManagementObject In searcher.Get()
                name = queryObj("Name")
                comportname = queryObj("DeviceID")
                CBComport.Items.Add(New GenericListItem(Of String)(name, comportname))
            Next
        Catch ex As ManagementException
            MsgBox("Error while querying for WMI data (COM Ports): " & ex.Message)
        End Try
    End Sub

    Private Sub BComportListReload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BComportListReload.Click
        EnumerateComports()
        LoadSetup()
    End Sub

    Private Sub ChkBECUAutoConnect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkBECUAutoConnect.CheckedChanged
        T4eReg.SetECUAutoConnect(ChkBECUAutoConnect.Checked)
    End Sub

    Private Sub RBSpeed1M_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSpeed1M.CheckedChanged
        CANSpeed = 1000        
    End Sub

    Private Sub RBSpeed500K_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSpeed500K.CheckedChanged
        CANSpeed = 500
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBSpeedAuto.CheckedChanged
        CANSpeed = 0
    End Sub

    ' set text for LblAccessType according to ECU Access Capablities
    Private Sub UpdateAccessTypeLabel()
        If ECU.AccessLevel = ECUAccessLevel.CANOnlyLocked Then
            LblAccessType.Text = "CAN Bus OBD only"
        ElseIf ECU.AccessLevel = ECUAccessLevel.CANOnlyUnlocked Then
            LblAccessType.Text = "CAN Bus full access"
        ElseIf ECU.AccessLevel = ECUAccessLevel.KLINE_CAN Then
            LblAccessType.Text = "KLINE/CAN Bus full access"
        Else
            LblAccessType.Text = "no access"
        End If
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

  
End Class


