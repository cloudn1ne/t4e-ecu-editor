Imports System
Imports System.Drawing
Imports System.Windows.Forms

Imports Ciloci.Flee
Imports Ciloci.Flee.CalcEngine
Imports System.Math

Public Class MapDataGrid
    Inherits System.Windows.Forms.Form
    Friend WithEvents xLabelPanel As System.Windows.Forms.Panel
    Friend WithEvents yLabelPanel As System.Windows.Forms.Panel
    Friend WithEvents DGVPanel As System.Windows.Forms.Panel
    Friend WithEvents mapDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents xLabel As New Label
    Friend WithEvents yLabel As New VerticalLabel
    Friend WithEvents ToolStrip As New System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel As New System.Windows.Forms.ToolStripLabel    
    Friend WithEvents TSB_Grading As New System.Windows.Forms.ToolStripButton
    Friend WithEvents TSB_Calc As New System.Windows.Forms.ToolStripButton
    Friend WithEvents TSB_LiveData As New System.Windows.Forms.ToolStripButton
    Private map As XMLMapDefinition
    Private Config As XMLConfiguration
    Private Vars80 As List(Of XMLDatalogDefinition)
    Private CALBytes As Byte()
    Private CALFileOffset As Long
    Private LerpEnabled = True
    Private CalcEnabled = True
    Private CFH As CALFileHandler
    Dim CellWidth As Integer = 45
    Dim CellHeight As Integer = 45    
    Friend WithEvents TimerLiveData As New System.Windows.Forms.Timer
    Private a_mark As Integer = -1
    Private a_graphics As System.Drawing.Graphics = Nothing
    Private a_graphics_left As Integer
    Private a_graphics_bottom As Integer
    Private a_graphics_right As Integer
    Private a_val As Double
    Dim gridLinePen = New Pen(Color.DarkGreen, 8)

    ' NEW
    Private LERP As LERPDataGridView
    Private components As System.ComponentModel.IContainer



    Public Sub LoadMap(ByVal Config As XMLConfiguration, ByVal map As XMLMapDefinition, ByVal CFH As CALFileHandler)
        ' Load XML file datalogger definitions
        Main.MD.LoadFromXML(Main.CalFilePath)
        Vars80 = Main.MD.GetDatalog80Vars()
        ' ByVal CALBytes As Byte(), ByVal CALFileOffset As Long)
        Me.map = map
        Me.Config = Config
        Me.CFH = CFH
        Me.CALBytes = CFH.GetCALBytes
        Me.CALFileOffset = Config.calfile_vaddr - Config.calfile_headersize
        SetupLayout()
        PopulateDataGridView()
        LERP = New LERPDataGridView(mapDataGridView)
        LERP.Redraw()
        'LerpDatagridView(LerpEnabled)
        ResizeElements()
        Me.Show()
    End Sub

    Private Sub ResizeElements()
        Dim intWidth As Integer = 0
        Dim intHeight As Integer = 0

        ' Calculate needed size of Form        
        For Each dgvcw As DataGridViewColumn In Me.mapDataGridView.Columns
            intWidth += dgvcw.Width
        Next
        Me.Width = intWidth + Me.mapDataGridView.RowHeadersWidth + Me.yLabelPanel.Width + 10
        For Each dgvrh As DataGridViewRow In Me.mapDataGridView.Rows
            intHeight += dgvrh.Height
        Next
        Dim BorderWidth As Integer = (Me.Width - Me.ClientSize.Width) / 2
        Dim TitlebarHeight As Integer = Me.Height - Me.ClientSize.Height - 2 * BorderWidth
        Me.Height = intHeight + Me.mapDataGridView.ColumnHeadersHeight + Me.xLabelPanel.Height + TitlebarHeight + 10
       

        ' shrink text for xlabel until it fits
        Dim fontsize As Integer = 15
        While (getFullTextWidth(xLabel) > xLabelPanel.Width - 20)
            fontsize -= 1
            Me.xLabel.Font = New Drawing.Font("Arial", fontsize, FontStyle.Bold)
        End While
        ' center the x/y Axis Labels
        xLabel.Location = New System.Drawing.Point(Me.xLabelPanel.Width / 2 - getFullTextWidth(xLabel) / 2, 5 + Me.ToolStrip.Height)

        If (map.dimension = 3) Then
            ' shrink text for ylabel until it fits
            fontsize = 15
            While (getFullTextHeight(yLabel) > yLabelPanel.Height - 20)
                fontsize -= 1
                Me.yLabel.Font = New Drawing.Font("Arial", fontsize, FontStyle.Bold)
            End While
            yLabel.Height = getFullTextHeight(yLabel)
            yLabel.Location = New System.Drawing.Point(5, (Me.yLabelPanel.Height) / 2 - getFullTextHeight(yLabel) / 2)  
        End If
        Me.Show()
    End Sub

    Private Sub SetupLayout()

        Me.Size = New Size(600, 500)

        Me.ToolStrip = New System.Windows.Forms.ToolStrip
        Me.TSB_Grading = New System.Windows.Forms.ToolStripButton
        Me.TSB_Calc = New System.Windows.Forms.ToolStripButton
        Me.TSB_LiveData = New System.Windows.Forms.ToolStripButton
        Me.xLabelPanel = New System.Windows.Forms.Panel
        Me.yLabelPanel = New System.Windows.Forms.Panel
        Me.DGVPanel = New System.Windows.Forms.Panel
        Me.mapDataGridView = New System.Windows.Forms.DataGridView

        Me.ToolStrip.SuspendLayout()
        Me.DGVPanel.SuspendLayout()
        CType(Me.mapDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()

        '
        'ToolStrip
        '
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSB_Grading, Me.TSB_Calc, Me.TSB_LiveData, Me.ToolStripLabel})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(738, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "ToolStrip"

        '
        'xLabel
        '        
        Me.xLabel.Location = New System.Drawing.Point(50, 5 + Me.ToolStrip.Height)
        Me.xLabel.Name = "xLabel"
        Me.xLabel.Font = New Drawing.Font("Arial", 15, FontStyle.Bold)
        Me.xLabel.AutoSize = True

        '
        'yLabel
        '        
        Me.yLabel.Location = New System.Drawing.Point(5, 25)
        Me.yLabel.Font = New Drawing.Font("Arial", 15, FontStyle.Bold)
        Me.yLabel.Name = "yLabel"

        '
        'xLabelPanel
        '
        Me.xLabelPanel.Controls.Add(Me.ToolStrip)
        Me.xLabelPanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.xLabelPanel.Location = New System.Drawing.Point(0, 0)
        Me.xLabelPanel.Name = "xLabelPanel"
        Me.xLabelPanel.Height = 40 + Me.ToolStrip.Height
        Me.xLabelPanel.TabIndex = 0
        '
        'yLabelPanel
        '
        Me.yLabelPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.yLabelPanel.Location = New System.Drawing.Point(0, 100)
        Me.yLabelPanel.Name = "yLabelPanel"
        ' unless we have a Y axis we can make the ypanel's width = 0
        If (map.dimension = 3) Then
            Me.yLabelPanel.Width = 40
        Else
            Me.yLabelPanel.Width = 0
        End If
        Me.yLabelPanel.TabIndex = 1

        '
        'TSB_Grading
        '
        Me.TSB_Grading.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSB_Grading.Image = Global.T4e_ECU_Editor.My.Resources.Resources.colorgradient
        Me.TSB_Grading.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_Grading.Name = "TSB_Grading"
        Me.TSB_Grading.Size = New System.Drawing.Size(23, 22)
        Me.TSB_Grading.Text = "toggle display color grading"

        '
        'TSB_Calc
        '
        Me.TSB_Calc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TSB_Calc.Image = Global.T4e_ECU_Editor.My.Resources.Resources.calculator_icon
        Me.TSB_Calc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_Calc.Name = "TSB_Calc"
        Me.TSB_Calc.Size = New System.Drawing.Size(23, 22)
        Me.TSB_Calc.Text = "toggle calculated values"

        '
        'TSB_LiveData
        '
        Me.TSB_LiveData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image        
        Me.TSB_LiveData.Image = Global.T4e_ECU_Editor.My.Resources.Resources.light_bulb_off
        Me.TSB_LiveData.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_LiveData.Name = "TSB_LiveData"
        Me.TSB_LiveData.Size = New System.Drawing.Size(23, 22)
        Me.TSB_LiveData.Text = "toggle live data view"
        Me.TSB_LiveData.Enabled = False
        ' set enable flag based on if we are connected to the ECU
        If (ECU.Adapter.isConnected) Then
            Me.TSB_LiveData.Enabled = True
        End If

        '
        'ToolStripLabel
        '
        Me.ToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel.Name = "ToolStripLabel"
        Me.ToolStripLabel.Size = New System.Drawing.Size(47, 22)
        Me.ToolStripLabel.Text = CFH.GetModel

        '
        'DGVPanel
        '
        Me.DGVPanel.Controls.Add(Me.mapDataGridView)
        Me.DGVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVPanel.Location = New System.Drawing.Point(200, 100)
        Me.DGVPanel.Name = "DGVPanel"
        'Me.DGVPanel.Size = New System.Drawing.Size(538, 336)
        Me.DGVPanel.TabIndex = 2

        '
        'mapDataGridView
        '
        Me.mapDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.mapDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        'Me.mapDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.mapDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.mapDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.mapDataGridView.Name = "mapDataGridView"
        Me.mapDataGridView.Size = New System.Drawing.Size(538, 336)
        Me.mapDataGridView.TabIndex = 0
        Me.mapDataGridView.ReadOnly = True
        Me.mapDataGridView.BorderStyle = BorderStyle.None
        Me.mapDataGridView.BackgroundColor = Me.yLabelPanel.BackColor
        Me.mapDataGridView.ColumnHeadersDefaultCellStyle.Font = New Drawing.Font("Arial", 9, FontStyle.Bold)
        Me.mapDataGridView.RowHeadersDefaultCellStyle.Font = New Drawing.Font("Arial", 9, FontStyle.Bold)


        ' Add x/y Label
        Me.xLabelPanel.Controls.Add(xLabel)
        Me.yLabelPanel.Controls.Add(yLabel)

        '
        ' Add Controls
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 436)
        Me.Controls.Add(Me.DGVPanel)
        Me.Controls.Add(Me.yLabelPanel)
        Me.Controls.Add(Me.xLabelPanel)

        Me.Name = "MapEditor"

        Me.xLabelPanel.ResumeLayout(False)
        Me.xLabelPanel.PerformLayout()
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.DGVPanel.ResumeLayout(False)
        CType(Me.mapDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private Sub RedrawCellDataGridView(ByVal idx_x As Integer, ByVal idx_y As Integer)
        Dim bytesize As Integer
        Dim size As Integer
        size = map.xsize * map.ysize
        bytesize = map.bits / 8

        If (idx_x <= 0) Then
            Exit Sub
        End If
        If (idx_x >= map.xsize - 1) Then
            Exit Sub
        End If

        Console.WriteLine("RedrawCellDataGridView() " & idx_x & "/" & idx_y)
        'mapDataGridView.Rows(idx_y).Cells(idx_x).Style.ForeColor = Color.Yellow

        If ((map.dimension = 1) And (map.interpolate = True)) Then
            Dim step_size = (1 << (8 - map.shift))
            map.xsize = map.input_a_maxval / step_size + 1
            mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r - CALFileOffset))
        End If

        If ((map.dimension = 1) And (map.interpolate = False)) Then
            Console.WriteLine("1D Flat")
            mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * map.xsize + idx_x + map.xsize / 2 + map.lut_r - CALFileOffset).ToString)
        End If

        If (map.dimension = 2) Then
            If (bytesize = 1) Then
                mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r - CALFileOffset))
            End If
            If (bytesize = 2) Then
                mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r - CALFileOffset) * 256 + CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r + 1 - CALFileOffset))
            End If
        End If

        If (map.dimension = 3) Then
            If (bytesize = 1) Then
                mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r - CALFileOffset))
            End If
            If (bytesize = 2) Then
                mapDataGridView.Rows(idx_y).Cells(idx_x).Value = LookupDataVal(CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r - CALFileOffset) * 256 + CALBytes(idx_y * bytesize * map.xsize + idx_x * bytesize + map.lut_r + 1 - CALFileOffset))
            End If
        End If
    End Sub
    Private Sub PopulateDataGridView()
        Dim bytesize As Integer
        Dim size As Integer
        size = map.xsize * map.ysize

        mapDataGridView.Rows.Clear()
        bytesize = map.bits / 8

        ' *********************************************
        ' *** 1D Interpolate
        ' *********************************************
        If ((map.dimension = 1) And (map.interpolate = True)) Then
            Dim step_size = (1 << (8 - map.shift))
            map.xsize = map.input_a_maxval / step_size + 1
            ' set size of dgv
            mapDataGridView.ColumnCount = map.xsize
            mapDataGridView.RowCount = map.ysize
            ' fill header (depends on size and shift)
            For x As Integer = 0 To map.xsize - 1
                mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA((x * step_size).ToString)
                mapDataGridView.Columns(x).Width = CellWidth
            Next x
            ' fill data 
            For y As Integer = 0 To map.ysize - 1
                For x As Integer = 0 To map.xsize - 1
                    mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r - CALFileOffset))
                Next x
                mapDataGridView.Rows(y).Height = CellHeight
            Next y
        End If

        ' *********************************************
        ' *** 1D FIXED
        ' *********************************************
        If ((map.dimension = 1) And (map.interpolate = False)) Then
            ' set size of dgv
            mapDataGridView.ColumnCount = map.xsize / 2
            mapDataGridView.RowCount = map.ysize
            ' fill header (first half of LUT_R)
            For x As Integer = 0 To map.xsize / 2 - 1
                mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA(CALBytes(x + map.lut_r - CALFileOffset).ToString)
                mapDataGridView.Columns(x).Width = CellWidth
            Next x
            ' fill data (second hald of LUT_R)
            For y As Integer = 0 To map.ysize - 1
                For x As Integer = 0 To map.xsize / 2 - 1
                    mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * map.xsize + x + map.xsize / 2 + map.lut_r - CALFileOffset).ToString)
                Next x
                mapDataGridView.Rows(y).Height = CellHeight
            Next y
        End If
        ' *********************************************
        ' *** 2D 
        ' *********************************************
        If (map.dimension = 2) Then
            ' set size of dgv
            mapDataGridView.ColumnCount = map.xsize
            mapDataGridView.RowCount = map.ysize
            ' fill header (LUT_A)
            For x As Integer = 0 To map.xsize - 1
                If (bytesize = 1) Then
                    mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA(CALBytes(x * bytesize + map.lut_a - CALFileOffset).ToString)
                End If
                If (bytesize = 2) Then
                    mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA(CALBytes(x * bytesize + map.lut_a - CALFileOffset) * 256 + CALBytes(x * bytesize + map.lut_a + 1 - CALFileOffset))
                End If
                mapDataGridView.Columns(x).Width = CellWidth
            Next x
            ' fill data (LUT_R)
            For y As Integer = 0 To map.ysize - 1
                For x As Integer = 0 To map.xsize - 1
                    If (bytesize = 1) Then
                        mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r - CALFileOffset))
                    End If
                    If (bytesize = 2) Then
                        mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r - CALFileOffset) * 256 + CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r + 1 - CALFileOffset))
                    End If
                Next x
                mapDataGridView.Rows(y).Height = CellHeight
            Next y
        End If
        ' *********************************************
        ' *** 3D
        ' *********************************************
        If ((map.dimension = 3)) Then
            ' set size of dgv
            mapDataGridView.ColumnCount = map.xsize
            mapDataGridView.RowCount = map.ysize
            ' fill column header (LUT_A)
            mapDataGridView.ColumnHeadersHeight = CellWidth
            For x As Integer = 0 To map.xsize - 1
                If (bytesize = 1) Then
                    mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA(CALBytes(x * bytesize + map.lut_a - CALFileOffset).ToString)
                End If
                If (bytesize = 2) Then
                    mapDataGridView.Columns(x).HeaderCell.Value = LookupHeaderValA(CALBytes(x * bytesize + map.lut_a - CALFileOffset) * 256 + CALBytes(x * bytesize + map.lut_a + 1 - CALFileOffset))
                End If
            Next x
            ' fill data (LUT_R)
            For y As Integer = 0 To map.ysize - 1
                For x As Integer = 0 To map.xsize - 1

                    If (bytesize = 1) Then
                        mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r - CALFileOffset))
                    End If
                    If (bytesize = 2) Then
                        mapDataGridView.Rows(y).Cells(x).Value = LookupDataVal(CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r - CALFileOffset) * 256 + CALBytes(y * bytesize * map.xsize + x * bytesize + map.lut_r + 1 - CALFileOffset))
                    End If
                    mapDataGridView.Columns(x).Width = CellWidth
                Next x
                mapDataGridView.Rows(y).Height = CellHeight
            Next y
            ' fill row header (LUT_B)        
            mapDataGridView.RowHeadersWidth = 60
            For y As Integer = 0 To map.ysize - 1
                If (bytesize = 1) Then
                    mapDataGridView.Rows(y).HeaderCell.Value = LookupHeaderValB(CALBytes(y * bytesize + map.lut_b - CALFileOffset).ToString)
                End If
                If (bytesize = 2) Then
                    mapDataGridView.Rows(y).HeaderCell.Value = LookupHeaderValB(CALBytes(y * bytesize + map.lut_b - CALFileOffset) * 256 + CALBytes(y * bytesize + map.lut_b + 1 - CALFileOffset))
                End If
            Next y
        End If

        ' print Axis Labels        
        GenerateAxisLabels()

        ' *******************************************************************
        ' *** Align all values in the cells to middle/center
        ' *** Mark all columns not sortable
        ' *******************************************************************        
        Me.mapDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Me.mapDataGridView.RowHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        For c As Integer = 0 To mapDataGridView.ColumnCount - 1
            Me.mapDataGridView.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Me.mapDataGridView.Columns(c).SortMode = DataGridViewColumnSortMode.NotSortable
        Next c
    End Sub

    ' *********************************************
    ' *** Generate Axis Lables
    ' *********************************************
    Private Sub GenerateAxisLabels()
        ' set xLabel
        Dim a_val, b_val, r_val As String
        a_val = ""
        b_val = ""
        r_val = ""
        If (TimerLiveData.Enabled) Then
            a_val = ECU.Poller.GetValueByLinkName(map.input_a_link_name, Vars80) & " "
            Me.xLabel.Text = map.input_a_name & " (" & a_val & map.input_a_unit & ")"
        Else
            If ((map.input_a_unit IsNot Nothing) And CalcEnabled) Then
                Me.xLabel.Text = map.input_a_name & " (" & a_val & map.input_a_unit & ")"
            Else
                Me.xLabel.Text = map.input_a_name & a_val
            End If
        End If
        ' set yLabel
        If (map.dimension = 3) Then
            Me.yLabel.Text = map.input_b_name
            If ((map.input_b_unit IsNot Nothing) And CalcEnabled) Then
                Me.yLabel.Text &= " (" & map.input_b_unit & ")"
            End If
        End If

        ' set Title                
        If (map.output_name IsNot Nothing) Then
            Me.Text = map.output_name
            If ((map.output_unit IsNot Nothing) And CalcEnabled) Then
                Me.Text &= " (" & map.output_unit & ")"
            End If
        Else
            Me.Text = map.lut_r_name
        End If
    End Sub

    Private Function LookupDataVal(ByVal lut_val As Integer)
        Dim retval As Double
        Dim fmt As String

        ' per default we do not process the data
        retval = lut_val

        If (Not CalcEnabled) Then
            Return retval.ToString
        End If

        ' if we have a calc todo do it
        If (map.lut_r_calc IsNot Nothing) Then
            Dim ec As New Ciloci.Flee.ExpressionContext
            Dim ex As IDynamicExpression
            ec.Imports.AddType(GetType(Math))
            If (map.lut_r_precision > 0) Then
                ec.Options.IntegersAsDoubles = True
            End If
            ec.Variables("LUTR") = lut_val
            Try
                ex = ec.CompileDynamic(map.lut_r_calc)
                retval = ex.Evaluate()
            Catch e As Exception
                retval = 0
            End Try
        End If
        If (map.lut_r_cast IsNot Nothing) Then
            If (map.lut_r_cast.Contains("signed")) Then
                If (map.bits = 8) Then
                    retval = IIf(retval < 128, retval, retval - 256)
                End If
                If (map.bits = 16) Then
                    retval = IIf(retval < 32768, retval, retval - 65536)
                End If
            End If
        End If
        If (map.lut_r_precision > 0) Then
            fmt = "F" & map.lut_r_precision
            Return (retval.ToString(fmt))
        Else
            Return (retval.ToString)
        End If
    End Function

    Private Function LookupHeaderValA(ByVal lut_val As Integer)
        Dim retval As Double
        Dim fmt As String

        ' per default we do not process the data
        retval = lut_val

        If (Not CalcEnabled) Then
            Return retval.ToString
        End If

        ' if we have a calc todo do it
        If (map.lut_a_calc IsNot Nothing) Then
            Dim ec As New Ciloci.Flee.ExpressionContext
            Dim ex As IDynamicExpression
            ec.Imports.AddType(GetType(Math))
            If (map.lut_a_precision > 0) Then
                ec.Options.IntegersAsDoubles = True
            End If
            ec.Variables("LUTA") = lut_val
            Try
                ex = ec.CompileDynamic(map.lut_a_calc)
                retval = ex.Evaluate()
            Catch e As Exception
                retval = 0
            End Try
        End If
        If (map.lut_a_precision > 0) Then
            fmt = "F" & map.lut_a_precision
            Return (retval.ToString(fmt))
        Else
            Return (retval.ToString)
        End If
    End Function

    Private Function LookupHeaderValB(ByVal lut_val As Integer)
        Dim retval As Double
        Dim fmt As String

        ' per default we do not process the data
        retval = lut_val
        If (Not CalcEnabled) Then
            Return retval.ToString
        End If

        ' if we have a calc todo do it
        If (map.lut_b_calc IsNot Nothing) Then
            Dim ec As New Ciloci.Flee.ExpressionContext
            Dim ex As IDynamicExpression
            ec.Imports.AddType(GetType(Math))
            If (map.lut_b_precision > 0) Then
                ec.Options.IntegersAsDoubles = True
            End If
            ec.Variables("LUTB") = lut_val
            Try
                ex = ec.CompileDynamic(map.lut_b_calc)
                retval = ex.Evaluate()
            Catch e As Exception
                retval = 0
            End Try
        End If
        If (map.lut_b_precision > 0) Then
            fmt = "F" & map.lut_b_precision
            Return (retval.ToString(fmt))
        Else
            Return (retval.ToString)
        End If
    End Function

    Private Function getFullTextWidth(ByVal lbl As Label)
        Using g As Graphics = lbl.CreateGraphics()
            Return g.MeasureString(lbl.Text, lbl.Font).Width
        End Using
    End Function

    Private Function getFullTextHeight(ByVal lbl As VerticalLabel)
        Using g As Graphics = lbl.CreateGraphics()
            Return g.MeasureString(lbl.Text, lbl.Font).Width
        End Using
    End Function



    Private Sub TSB_Grading_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSB_Grading.Click
        If (LerpEnabled) Then
            LerpEnabled = False
            LERP.Disable()
        Else
            LerpEnabled = True
            LERP.Enable()
        End If
    End Sub

    Private Sub TSB_Calc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSB_Calc.Click
        If (CalcEnabled) Then
            CalcEnabled = False
        Else
            CalcEnabled = True
        End If
        PopulateDataGridView()
        LERP.Redraw()
    End Sub


    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TimerLiveData = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'TimerLiveData
        '
        Me.TimerLiveData.Interval = 200
        '
        'MapDataGrid
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Name = "MapDataGrid"
        Me.ResumeLayout(False)

    End Sub

    Private Sub TSB_LiveData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TSB_LiveData.Click

        ECU.Poller.ClearPollTypes()
        ECU.Poller.AddPollType(ECUPoller.CANPollType.Type80, 0, 0, 0)
        ECU.Poller.SetPollTypeActive(ECUPoller.CANPollType.Type80, 0, 0, 0)

        If (Not ECU.Poller.GetPollThreadStatus) Then
            ECU.Poller.StartPollThread()
            Me.TSB_LiveData.Image = Global.T4e_ECU_Editor.My.Resources.Resources.light_bulb_on
            Console.WriteLine("TSB_LiveData_Click() started poller")
            TimerLiveData.Enabled = True
            TimerLiveData.Interval = 500
            GenerateAxisLabels()
        Else
            ECU.Poller.StopPollThread()
            Me.TSB_LiveData.Image = Global.T4e_ECU_Editor.My.Resources.Resources.light_bulb_off
            Console.WriteLine("TSB_LiveData_Click() stopped poller")
            TimerLiveData.Enabled = False
            GenerateAxisLabels()
        End If
    End Sub

    Private Sub TimerLiveData_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerLiveData.Tick
        Dim a, b As Double

        'Console.WriteLine("TimerLiveData_Tick() reading variables 0x" & Hex(map.input_a) & "  0x" & Hex(map.input_b))
        If (map.dimension = 1) Or (map.dimension = 2) Then
            a_val = ECU.Poller.GetValueByLinkName(map.input_a_link_name, Vars80)
            a_mark = FindXHeaderCellByVal(a_val)
        ElseIf (map.dimension = 3) Then
            a = ECU.Poller.GetValueByLinkName(map.input_a_link_name, Vars80)
            b = ECU.Poller.GetValueByLinkName(map.input_b_link_name, Vars80)
        End If
        'Console.WriteLine("TimerLiveData_Tick() A: " & a & " B: " & b)        
        ' update axis labels with live data
        GenerateAxisLabels()
        InterpolateXMarker()

    End Sub


    Private Sub InterpolateXMarker()
        ' a_mark contains the lower bounds cell for input_a
        ' a_val contains the current value for input_a        

        ' cell_low = lower value
        Dim cell_low = mapDataGridView.Columns(a_mark).HeaderCell.Value
        Dim cell_high = cell_low
        ' cell_high = higher value (or same if upper bounds hit)
        If (a_mark < mapDataGridView.Columns.Count - 1) Then
            cell_high = mapDataGridView.Columns(a_mark + 1).HeaderCell.Value
        End If

        ' get the boundary of the two adject cells
        Dim diff = cell_high - cell_low
        Dim middle = (cell_high - cell_low) / 2

        Dim pixel As Integer = CellWidth / 2
        If (diff > 0) Then
            Dim ValPerPixel As Integer = diff / CellWidth
            pixel = (a_val - cell_low) / ValPerPixel + CellWidth / 2
        End If

        RedrawCellDataGridView(a_mark - 1, 0)
        RedrawCellDataGridView(a_mark, 0)
        RedrawCellDataGridView(a_mark + 1, 0)

        a_graphics_left = mapDataGridView.GetCellDisplayRectangle(a_mark, 0, False).Left
        a_graphics_bottom = mapDataGridView.GetCellDisplayRectangle(a_mark, 0, False).Bottom
        a_graphics = mapDataGridView.CreateGraphics
        a_graphics.DrawLine(gridLinePen, a_graphics_left + pixel, a_graphics_bottom - 4, a_graphics_left + pixel + 5, a_graphics_bottom - 4)
        mapDataGridView.Rows(0).Cells(a_mark).Style.ForeColor = Color.Yellow

        Console.WriteLine("active cell: " & a_mark)
    End Sub

    ' return the idx of the cell matching
    Private Function FindXHeaderCellByVal(ByVal data As Double) As Integer
        ' smaller than first column element ?
        If (data < mapDataGridView.Columns(0).HeaderCell.Value) Then
            Return (0)
        End If
        ' bigger than last column element ?
        If (data > mapDataGridView.Columns(mapDataGridView.Columns.Count - 1).HeaderCell.Value) Then
            Return (mapDataGridView.Columns.Count - 1)
        End If

        ' find the first cell that is larger than our value
        ' return index of previous cell
        For x As Integer = 0 To mapDataGridView.Columns.Count - 2
            If (mapDataGridView.Columns(x).HeaderCell.Value > data) Then
                Return (x - 1)
            End If
        Next
    End Function


    Private Sub MapDataGrid_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        TimerLiveData.Enabled = False
        ECU.Poller.StopPollThread()
        Console.WriteLine("TSB_LiveData_Click() stopped poller")
    End Sub



End Class
