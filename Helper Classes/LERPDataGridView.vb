Public Class LERPDataGridView

    Private dgv As DataGridView
    Private enabled As Boolean
    Private real_xsize As Integer        ' cols
    Private real_ysize As Integer        ' rows
    Private min, max, diff As Double
    Private val As Double

    ' register dgv to be managed by this instance of the class
    Public Sub New(ByVal dgv As DataGridView)
        Me.dgv = dgv
        real_xsize = dgv.Columns.Count - 1
        real_ysize = dgv.Rows.Count - 1
        UpdateMinMax()
        enabled = True
    End Sub

    ' redraw dgv in LERP enabled mode
    Public Sub Enable()
        enabled = True
        UpdateMinMax()
        Redraw()
    End Sub

    ' redraw dgv in plain mode
    Public Sub Disable()
        enabled = False
        UpdateMinMax()
        Redraw()
    End Sub

    Public Sub SetEnableFlag(ByVal flg As Boolean)
        enabled = flg
    End Sub

    Private Sub UpdateMinMax()
        Dim val As Double
        min = 99999999999999999
        max = -999999999999999999
        For y As Integer = 0 To real_ysize
            For x As Integer = 0 To real_xsize
                val = CDbl(dgv.Rows(y).Cells(x).Value)
                If (val < min) Then
                    min = val
                End If
                If (val > max) Then
                    max = val
                End If
            Next x
        Next y
        diff = max - min
    End Sub

    Public Sub RedrawCell(ByVal idx_x As Integer, ByVal idx_y As Integer)
        If (enabled = True) Then
            ' ******************************************
            ' **** RGB VIEW
            ' ******************************************                        
            Dim pct As Double
            
            val = dgv.Rows(idx_y).Cells(idx_x).Value
            val = val - min
            If (diff <> 0) Then
                pct = val / diff * 100 / 100
            Else
                pct = 0
            End If
            dgv.Rows(idx_y).Cells(idx_x).Style.BackColor = Lighten(Lerp(Color.Green, Color.Red, pct), 0.25)
        Else
            ' ******************************************
            ' **** NO RGB VIEW
            ' ******************************************            
            dgv.Rows(idx_y).Cells(idx_x).Style.BackColor = Color.White
        End If
    End Sub

    Public Sub Redraw()      
        If (enabled = True) Then
            ' ******************************************
            ' **** RGB VIEW
            ' ******************************************                        
            Dim pct As Double
            For y As Integer = 0 To real_ysize
                For x As Integer = 0 To real_xsize
                    val = dgv.Rows(y).Cells(x).Value
                    val = val - min
                    If (diff <> 0) Then
                        pct = val / diff * 100 / 100
                    Else
                        pct = 0
                    End If
                    dgv.Rows(y).Cells(x).Style.BackColor = Lighten(Lerp(Color.Green, Color.Red, pct), 0.25)
                    'mapDataGridView.Rows(y).Cells(x).Value = pct                
                Next x
            Next y
        Else
            ' ******************************************
            ' **** NO RGB VIEW
            ' ******************************************
            For y As Integer = 0 To real_ysize
                For x As Integer = 0 To real_xsize
                    dgv.Rows(y).Cells(x).Style.BackColor = Color.White
                Next x
            Next y
        End If
    End Sub


    Private Function Lerp(ByVal color1 As Color, ByVal color2 As Color, ByVal amount As Single) As Color
        Const bitmask As Single = 65536.0!
        Dim n As UInteger = CUInt(Math.Round(CDbl(Math.Max(Math.Min((amount * bitmask), bitmask), 0.0!))))
        Dim r As Integer = (CInt(color1.R) + (((CInt(color2.R) - CInt(color1.R)) * CInt(n)) >> 16))
        Dim g As Integer = (CInt(color1.G) + (((CInt(color2.G) - CInt(color1.G)) * CInt(n)) >> 16))
        Dim b As Integer = (CInt(color1.B) + (((CInt(color2.B) - CInt(color1.B)) * CInt(n)) >> 16))
        Dim a As Integer = (CInt(color1.A) + (((CInt(color2.A) - CInt(color1.A)) * CInt(n)) >> 16))
        Return Color.FromArgb(a, r, g, b)
    End Function

    Private Function Lighten(ByVal inColor As Color, ByVal inAmount As Double) As Color
        Return Color.FromArgb(inColor.A, _
        Math.Min(255, inColor.R + 255 * inAmount), _
        Math.Min(255, inColor.G + 255 * inAmount), _
        Math.Min(255, inColor.B + 255 * inAmount))
    End Function
End Class
