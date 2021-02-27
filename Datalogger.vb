Imports System.Threading
Imports Ciloci.Flee
Imports Ciloci.Flee.CalcEngine
Imports System.Math

Public Class Datalogger    
    Dim MD As New MapDefinitions
    Dim Vars80 As List(Of XMLDatalogDefinition)
    Dim Vars50 As List(Of XMLDatalogDefinition)
    Dim CAN80Bytes() As Byte    
    Private ChartVals As New List(Of ChartVal)
    Private ChartX As Integer = 0
    Private ChartXMaxSize As Integer = 100
    Private m_OldSelectNode As TreeNode


    Private Structure ChartVal
        Dim node_name As String
        Dim chart_name As String
        Dim enabled As Boolean
    End Structure

    Private Sub Datalogger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing        
        ECU.Poller.StopPollThread()
        TimerVarUpd.Enabled = False
    End Sub

    Private Sub Datalogger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadVarDefinitions()
        ' hide label texts
        LblName.Text = ""
        LblUnit.Text = ""
        LblSize.Text = ""
        LblDesc.Text = ""
        LblValue.Text = ""
        LblAddr.Text = ""
    End Sub

    Private Sub LoadVarDefinitions()
        ' Load XML file datalogger definitions
        MD.LoadFromXML(Main.CalFilePath)
        Vars80 = MD.GetDatalog80Vars()
        Vars50 = MD.GetDatalog50Vars()
        ' fill TreeView with list of variables
        TVDataloggerFill()
    End Sub

    Public Sub TVDataloggerFill()
        Dim TVanker As TreeNode
        Dim Nodename As String = ""
        Dim Nodename_by As String = ""

        ' reset everything (reload case)
        TVDatalogger.Nodes.Clear()
        ECU.Poller.ClearPollTypes()

        ' create root nodes in TV
        TVDatalogger.Nodes.Add("CAN80", "CAN 0x80 Datalogger")
        TVDatalogger.Nodes.Add("CAN50", "CAN 0x50 Datalogger")
        '''''''''''''''''''''''''''''''''''''''''''''
        ' fill TreeView with CAN 0x80 Variables
        ' TV id is the variable number "num" attribute
        '''''''''''''''''''''''''''''''''''''''''''''
        TVanker = TVDatalogger.Nodes.Find("CAN80", True)(0)
        TVanker.Nodes.Clear()
        ' register 0x80 vars with the polling functions (one call for all 0x80 vars)
        ECU.Poller.AddPollType(ECUPoller.CANPollType.Type80, 0, 0, 0)
        For Each dvar As XMLDatalogDefinition In Vars80            
            If (dvar.name IsNot Nothing) Then
                Nodename = dvar.name & " (" & dvar.unit & ")"
            End If
            TVanker.Nodes.Add("CAN80-" & dvar.num, Nodename)
            'Console.WriteLine("TVDataloggerFill() registering 0x80 " & dvar.name)
        Next dvar

        '''''''''''''''''''''''''''''''''''''''''''''
        ' fill TreeView with CAN 0x50 Variables
        ' TV id is the variable number "num" attribute
        '''''''''''''''''''''''''''''''''''''''''''''
        TVanker = TVDatalogger.Nodes.Find("CAN50", True)(0)
        TVanker.Nodes.Clear()
        For Each dvar As XMLDatalogDefinition In Vars50
            If (dvar.name IsNot Nothing) Then
                Nodename = dvar.name & " (" & dvar.unit & ") @0x" & Hex(dvar.addr)
            End If
            ' register this 0x50 var with the polling functions (one call per 0x50 var)
            ECU.Poller.AddPollType(ECUPoller.CANPollType.Type50, dvar.addr, dvar.size, 0)
            TVanker.Nodes.Add("CAN50-" & dvar.num, Nodename)
            'Console.WriteLine("TVDataloggerFill() registering 0x50 " & dvar.name)
        Next dvar

        ' show them all nicely
        TVDatalogger.ExpandAll()
    End Sub


    Private Sub BCanConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCanConnect.Click

        If (ECU.Poller.GetPollThreadStatus) Then
            TimerVarUpd.Enabled = False
            ECU.Poller.StopPollThread()
            BCanConnect.Text = "Start"
        Else
            ECU.Poller.StartPollThread()
            TimerVarUpd.Enabled = True
            BCanConnect.Text = "Stop"
        End If
    End Sub

    Private Function GetCAN80BytesValue(ByVal var_num)
        Dim dvar As XMLDatalogDefinition
        Dim fmt As String
        Dim retval As Double

        Try
            dvar = Vars80(var_num)
            Dim val As Integer = 0
            'Console.WriteLine("size: " & dvar.size)
            If (dvar.size = 1) Then
                val = CAN80Bytes(dvar.offset)
            End If
            If (dvar.size = 2) Then
                val = (CAN80Bytes(dvar.offset) * 256) Or CAN80Bytes(dvar.offset + 1)
            End If
            If (dvar.size = 4) Then
                val = (CAN80Bytes(dvar.offset) * 256 * 256 * 256) Or (CAN80Bytes(dvar.offset + 1) * 256 * 256) Or (CAN80Bytes(dvar.offset + 2) * 256) Or CAN80Bytes(dvar.offset + 3)
            End If
            'For i As Integer = dvar.offset To dvar.offset + dvar.size - 1
            ' Console.Write("0x" & Hex(CAN80Bytes(i)) & " ")
            'Next
            dvar.SetValue(val)
            'Console.WriteLine("name : " & dvar.name & " value: " & Hex(dvar.value))

            retval = val
            If (dvar.calc IsNot Nothing) Then
                Dim ec As New Ciloci.Flee.ExpressionContext
                Dim ex As IDynamicExpression
                ec.Imports.AddType(GetType(Math))
                If (dvar.precision > 0) Then
                    ec.Options.IntegersAsDoubles = True
                End If
                ec.Variables("VAR") = val
                Try
                    ex = ec.CompileDynamic(dvar.calc)
                    retval = ex.Evaluate()
                Catch e As Exception
                    retval = 0
                End Try
            End If
            If (dvar.cast IsNot Nothing) Then
                If (dvar.cast.Contains("signed")) Then
                    If (dvar.size = 1) Then
                        retval = IIf(retval < 128, retval, retval - 256)
                    End If
                    If (dvar.size = 2) Then
                        retval = IIf(retval < 32768, retval, retval - 65536)
                    End If
                End If
            End If
            If (dvar.precision > 0) Then
                fmt = "F" & dvar.precision
                Return (retval.ToString(fmt))
            Else
                Return (retval.ToString)
            End If

        Catch ex As Exception
            Console.WriteLine("GetCAN80BytesValue()" & ex.Message)
            Console.WriteLine("size = " & CAN80Bytes.Count)
            Return (0)
        End Try
    End Function

    Private Sub ChartValAdd(ByVal node_name As String)
        Dim CV As New ChartVal
        Dim chart_name As String = ""

        If (node_name.ToString.StartsWith("CAN50-")) Then
            chart_name = Vars50(node_name.ToString.Split("-")(1)).name
        End If
        If (node_name.ToString.StartsWith("CAN80-")) Then
            chart_name = Vars80(node_name.ToString.Split("-")(1)).name
        End If

        ' check if we are already charting this node_name
        If (Chart.Series.FindByName(chart_name) Is Nothing) Then
            CV.node_name = node_name
            CV.chart_name = chart_name
            CV.enabled = True
            ChartVals.Add(CV)
            Chart.Series.Add(chart_name)
            Chart.Series(chart_name).ChartType = DataVisualization.Charting.SeriesChartType.Line
        End If
    End Sub

    Private Sub ChartValClearData()
        ' update active chart vars
        For Each s As System.Windows.Forms.DataVisualization.Charting.Series In Chart.Series
            Chart.Series(s.Name).Points.Clear()
        Next
        'For Each CV As ChartVal In ChartVals
        'Dim chart_series = Vars80(CV.node_name).name        
        'Chart.Series(chart_series).Points.Clear()
        'Next

        ChartX = 0
    End Sub
    Private Sub TVMakeBold(ByVal tvn As TreeNode)
        Dim old_text As String

        ' TV is broken in .NET
        ' you need to rewrite the text of the node otherwise it gets cropped !
        old_text = tvn.Text
        TVDatalogger.SelectedNode.NodeFont = New Font(TVDatalogger.Font, FontStyle.Bold)
        tvn.Text = old_text
    End Sub

    Private Sub TVDatalogger_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TVDatalogger.AfterSelect
        TVMakeBold(e.Node)
        VarInfosUpdate(e.Node.Name)
    End Sub

    Private Sub VarInfosUpdate(ByVal node_name As String)
        Dim dvar As XMLDatalogDefinition
        Dim PollList() As ECUPoller.PollListElement

        Try
            PollList = ECU.Poller.GetPollList()
            For Each pe As ECUPoller.PollListElement In PollList
                '
                ' directly use pe.databytes ! like we do for 0x50 vals
                '
                If (pe.type = ECUPoller.CANPollType.Type80) Then                                        
                    'Console.WriteLine("CAN80: " & pe.databytes.Count)
                    CAN80Bytes = pe.databytes
                End If             
            Next
        Catch ex As Exception
            Console.WriteLine("VarInfosUpdate() - Fetching from PollThread - " & ex.Message)
        End Try

        Try
            ''''''''''''''''''''''''''''''''''
            ' display CAN 0x80 variables
            ''''''''''''''''''''''''''''''''''
            If (node_name.ToString.StartsWith("CAN80-")) Then
                LblAddr.Visible = False
                LblAddrText.Visible = False
                'Console.WriteLine("VarInfosUpdate() node_name : " & node_name)
                'Console.WriteLine("VarInfosUpdate() node_name : " & node_name.ToString.Split("-")(1))
                dvar = Vars80(node_name.ToString.Split("-")(1))
                LblDesc.Text = dvar.description
                LblName.Text = dvar.name
                LblSize.Text = dvar.size
                LblUnit.Text = dvar.unit
                ECU.Poller.SetPollTypeActive(ECUPoller.CANPollType.Type80, 0, 0, 0)
                LblValue.Text = GetCAN80BytesValue(node_name.ToString.Split("-")(1)) & " " & dvar.unit
            End If

            ''''''''''''''''''''''''''''''''''
            ' display CAN 0x50 variables
            ''''''''''''''''''''''''''''''''''
            If (node_name.ToString.StartsWith("CAN50-")) Then
                LblAddr.Visible = True
                LblAddrText.Visible = True
                'Console.WriteLine("VarInfosUpdate() node_name : " & node_name)
                'Console.WriteLine("VarInfosUpdate() node_name : " & node_name.ToString.Split("-")(1))
                dvar = Vars50(node_name.ToString.Split("-")(1))
                LblDesc.Text = dvar.description
                LblName.Text = dvar.name
                LblSize.Text = dvar.size
                LblUnit.Text = dvar.unit
                LblAddr.Text = "0x" & Hex(dvar.addr)
                LblValue.Text = ECU.Poller.GetValueByAddr(dvar.addr) & " " & dvar.unit
                ECU.Poller.SetPollTypeActive(ECUPoller.CANPollType.Type50, dvar.addr, 0, 0)
            End If

            ''''''''''''''''''''''''''''''''''
            ' chart variables
            ''''''''''''''''''''''''''''''''''
            For Each CV As ChartVal In ChartVals
                If (CV.node_name.StartsWith("CAN80-")) Then
                    Dim chart_series = Vars80(CV.node_name.Split("-")(1)).name
                    Chart.Series(chart_series).Points.AddXY(ChartX, GetCAN80BytesValue(CV.node_name.Split("-")(1)))
                    ChartX = ChartX + 1
                End If
                If (CV.node_name.StartsWith("CAN50-")) Then
                    Dim chart_series = Vars50(CV.node_name.Split("-")(1)).name
                    Chart.Series(chart_series).Points.AddXY(ChartX, ECU.Poller.GetValueByAddr(Vars50(CV.node_name.Split("-")(1)).addr))
                    ECU.Poller.SetPollTypeActive(ECUPoller.CANPollType.Type50, Vars50(CV.node_name.Split("-")(1)).addr, 0, 0)
                    ChartX = ChartX + 1
                End If
            Next

        Catch ex As Exception
            Console.WriteLine("VarInfosUpdate() - Updating variables/charts" & ex.Message)
        End Try
    End Sub

    '******************************************************
    '* Update displayed data based on received CAN Vars
    '******************************************************
    Private Sub TimerVarUpd_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerVarUpd.Tick
        If (ECU.Adapter.isConnected) Then
            If (TVDatalogger.SelectedNode IsNot Nothing) Then
                If (TVDatalogger.SelectedNode.Name.Contains("-")) Then
                    VarInfosUpdate(TVDatalogger.SelectedNode.Name)
                End If
            End If
        Else
            ECU.Poller.StopPollThread()
            TimerVarUpd.Enabled = False
            BCanConnect.Text = "Start Logging"
        End If
    End Sub

    Private Sub ReLoadDef_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReLoadDef.Click
        ECU.Poller.StopPollThread()
        LoadVarDefinitions()
        ECU.Poller.StartPollThread()
    End Sub

    Private Sub BChartRemoveVars_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BChartRemoveVars.Click
        ChartVals.Clear()
        Chart.Series.Clear()
    End Sub

    Private Sub BChartClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BChartClear.Click
        ChartValClearData()
    End Sub

    Private Sub TVDatalogger_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVDatalogger.DoubleClick        
        TVMakeBold(TVDatalogger.SelectedNode)
        ChartValAdd(TVDatalogger.SelectedNode.Name)
    End Sub

    

    

    Private Sub TVDatalogger_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TVDatalogger.MouseUp
        If e.Button = MouseButtons.Right Then

            ' Point where mouse is clicked
            Dim p As Point = New Point(e.X, e.Y)

            ' Go to the node that the user clicked
            Dim node As TreeNode = TVDatalogger.GetNodeAt(p)
            If Not node Is Nothing Then
                ' Highlight the node that the user clicked.
                ' The node is highlighted until the Menu is displayed on the screen
                'm_OldSelectNode = TVDatalogger.SelectedNode
                TVDatalogger.SelectedNode = node
                ' dont add 'root' node
                ContextMenuTV.Show(TVDatalogger, p)
                ' Highlight the selected node
                ' TVDatalogger.SelectedNode = m_OldSelectNode
                m_OldSelectNode = Nothing
            End If
        End If

    End Sub

    Private Sub AddToChartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddToChartToolStripMenuItem.Click        
        TVMakeBold(TVDatalogger.SelectedNode)
        ChartValAdd(TVDatalogger.SelectedNode.Name)
    End Sub
End Class