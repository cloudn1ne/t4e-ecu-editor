Imports System.Net.Sockets
Imports System.Threading
Imports System.IO
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions


Public Class CAN2USB
    Dim ComPort As IO.Ports.SerialPort = Nothing
    Dim is_open As Boolean = False
    Dim Writer As StreamWriter
    Dim Reader As StreamReader
    Dim VersionString As String        

    ' timeout between sending $S commands, and expecting the valid $F reply
    Private TxTimeoutMillis As Integer = 250

    Private Structure CANMsg
        Dim can_id As Integer
        Dim len As Integer
        Dim data As List(Of Byte)
    End Structure

    Public Function isConnected() As Boolean
        Return (is_open)
    End Function

    '*****************************************************
    '* Shutdown PollThread and then close COM Port
    '*****************************************************
    Public Sub Disconnect()
        If (isConnected()) Then
            is_open = False
            ' give other threads a chance to bail out
            '            While (pThreadRunning)
            ' Thread.Sleep(10)
            ' End While
            ComPort.Close()
        End If
    End Sub


    '********************************************************************
    '* CAN 80 Replies vary by ECU Firmware (number of Variables)
    '* try to find the highest CAN 2XX reply id and return it
    '********************************************************************
    Public Function GetHighestCAN80ReplyID() As Integer
        Dim canline As String
        Dim count As Integer
        Dim this_can_id As Integer
        Dim highest_can_id As Integer = 0

        ComPort.DiscardInBuffer()
        Try
            ComPort.WriteLine("$S,2,80,0,1")
            count = 0
            ' first 100 msgs after the $S request
            ' need to have our highest CAN ID otherwise something is wrong
            While count < 50
                Try
                    canline = ComPort.ReadLine()
                Catch ex As Exception
                    Console.WriteLine("GetHighestCAN80ReplyID() ReadLine: " & ex.Message)
                    Exit Function
                End Try

                If canline Is Nothing Then
                    Exit While
                End If
                'Console.WriteLine("GetHighestCAN80ReplyID RX: '" & canline & "'")

                ' process each received line and find CAN 0x2XX ID's
                canline = canline.Replace(vbCr, "").Replace(vbLf, "")
                Dim aArr() As String = canline.Split(",")
                ' minimum valid answer length 4 ($F,X,CAN_ID,DATA1,..)
                If (aArr.Length > 4) Then
                    Try
                        this_can_id = Convert.ToInt32(aArr(2), 16)
                        If ((this_can_id > highest_can_id) And (this_can_id > &H200) And (this_can_id < &H2FF)) Then
                            highest_can_id = this_can_id
                        End If
                    Catch ex As Exception
                        Console.WriteLine(ex)
                        Exit Function
                    End Try
                End If
                count = count + 1
            End While
            'Console.WriteLine("GetHighestCAN80ReplyID Fetched maximum CAN 0x80 ID " & Hex(Me.CAN80MaxID))
            Return (highest_can_id)
        Catch ex As Exception
            MessageBox.Show("Error while trying to query highest CAN 0x80 reply ID" & vbCrLf & vbCrLf & _
            ex.Message, _
            "GetHighestCAN80ReplyID", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    '*****************************************************
    '* $S,2,80,0,1 - read CAN 0x80 variables
    '* (THREAD)
    '*****************************************************
    Public Function PollType80(ByVal CAN80MaxID As Integer) As Byte()
        'Dim reply_str As String = left_over_str
        Dim reply_str As String = ""
        'Dim reply_data As New List(Of CANMsg)
        Dim c As CANMsg
        Dim count As Integer
        Dim canline As String
        ' minimum, maximum, step size of 0x80 CAN replies
        ' end_can_id is determined by call to SetHighestCAN80ReplyID()
        Dim start_can_id = &H200
        Dim end_can_id = CAN80MaxID
        Dim step_can_id = &H4
        Dim expected_can_id = start_can_id
        Dim CAN80Bytes As New List(Of Byte)
        Dim startTime As Long

        If (Not Me.is_open) Then
            Return (Nothing)
        End If

        'Console.WriteLine("PollType80() - end is: 0x" & Hex(CAN80MaxID))
        Try            
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''        
            ' loop until we get what we want or TxTimeoutMillis is hit
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            startTime = DateTime.Now.Millisecond
            ComPort.DiscardInBuffer()
            ComPort.WriteLine("$S,2,80,0,1")
            While True
                Try
                    canline = ComPort.ReadLine()
                Catch ex As Exception
                    Console.WriteLine("PollType80() ReadLine: " & ex.Message)
                    Return (Nothing)
                End Try
                'Console.WriteLine("PollType80() RX: '" & canline & "'")
                If canline Is Nothing Then
                    Exit While
                End If
                reply_str = reply_str & vbCrLf & canline
                If (startTime + Me.TxTimeoutMillis < DateTime.Now.Millisecond) Then
                    Console.WriteLine("PollType80() TxTimeoutMillis hit ")
                    reply_str = ""
                    Exit While
                End If
                ' highest CAN80 reply id ? then we are done receiving    
                If canline.StartsWith("$F,8," & CAN80MaxID.ToString("X3") & ",") Then
                    'Console.WriteLine("********* END FOUND ***************")
                    Exit While
                End If
            End While

            ' start parsing form the 0x200 msg onwards
            If (reply_str.IndexOf(vbCrLf & "$F,8,200,") <> -1) Then
                SyncLock CAN80Bytes
                    CAN80Bytes.Clear()
                End SyncLock
                ' throw away anything before the $F,8,200 reply
                reply_str = reply_str.Remove(0, reply_str.IndexOf(vbCrLf & "$F,8,200,") + vbCrLf.Length)
                Dim ansArr() As String = reply_str.Split(vbCrLf)
                ' split into lines
                For Each a As String In ansArr
                    a = a.Replace(vbCr, "").Replace(vbLf, "")
                    Dim aArr() As String = a.Split(",")
                    ' minimum valid answer length 4 ($F,X,CAN_ID,DATA1,..)
                    If (aArr.Length > 4) Then
                        Try
                            c.len = Convert.ToInt32(aArr(1), 16)
                        Catch ex As Exception
                            Console.WriteLine("LEN convert - line: '" & a & "'")
                            Console.WriteLine(ex)
                            Return (Nothing)
                        End Try
                        Try
                            c.can_id = Convert.ToInt32(aArr(2), 16)
                        Catch ex As Exception
                            Console.WriteLine("CAN ID convert line: '" & a & "'")
                            Console.WriteLine(ex)
                            Return (Nothing)
                        End Try
                        ' valid len, expected can id found, so we add the databytes to CANXXBytes array
                        If ((c.len > 0) And (expected_can_id = c.can_id) And (c.len = aArr.Length - 3)) Then
                            For i As Integer = 3 To aArr.Length - 1
                                'Console.Write(aArr(i) & " ")  
                                Dim v As Integer
                                Try
                                    v = Convert.ToInt32(aArr(i), 16)
                                Catch ex As Exception
                                    Console.WriteLine("CAN DATA " & i & " convert line: '" & a & "'")
                                    Console.WriteLine(ex)
                                    Return (Nothing)
                                End Try
                                CAN80Bytes.Add(v)
                            Next
                            If (end_can_id = c.can_id) Then
                                'Console.WriteLine("===== FULL FRAME RECEIVED =====")
                                'Console.WriteLine("data size read " & CAN80Bytes.Count)
                                'left_over_str = ""                              
                                Dim retval() As Byte
                                Array.Resize(retval, CAN80Bytes.Count)
                                CAN80Bytes.CopyTo(retval)
                                Return (retval)
                            End If
                            expected_can_id = expected_can_id + step_can_id ' expect next can_id
                        End If
                    End If
                Next
                Return (Nothing)
            Else
                Console.WriteLine("PollType80() RX: no valid reply - timeout hit " & reply_str)
                Return (Nothing)
            End If
        Catch ex As Exception
            Console.WriteLine("PollType80() " & ex.Message)
        End Try
        Return (Nothing)
    End Function


    '*****************************************************
    '* $VER - read adapter version    
    '*****************************************************
    Public Function GetVersion() As String
        Dim VersionReply As String = ""

        While (VersionReply.StartsWith("$VER") = False)
            ComPort.ReadTimeout = 1000
            ComPort.DiscardInBuffer()
            ComPort.WriteLine("$VER")
            Try
                VersionReply = ComPort.ReadLine()
                'Console.WriteLine(VersionReply)
            Catch ex As Exception
                MessageBox.Show("No reply received for $VER query" & vbCrLf & vbCrLf & _
                     "Please Review COM Port", _
                     "Error connecting to device", _
                  MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Disconnect()
                Return ("")
            End Try
        End While

        ' check if we get a valid reply to $VER
        If (VersionReply.StartsWith("$VER,OK,")) Then
            Dim verArr() As String
            VersionReply = VersionReply.Replace(vbCr, "").Replace(vbLf, "")
            verArr = VersionReply.Split(",")
            If (verArr(2) IsNot Nothing) Then
                VersionString = verArr(2)
            Else
                MessageBox.Show("Invalid reply for $VER query" & vbCrLf & vbCrLf & _
                  "Please Review COM Port", _
                  "Error connecting to device", _
                  MessageBoxButtons.OK, MessageBoxIcon.Error)
                Me.Disconnect()
                Return ("")
            End If
        Else
            MessageBox.Show("Invalid reply for $VER query" & vbCrLf & vbCrLf & _
                  "Please Review COM Port", _
                  "Error connecting to device", _
                   MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Disconnect()
            Return ("")
        End If
        Return (VersionString)
    End Function

    '*****************************************************
    '* Connect to USB_CAN adapter
    '*****************************************************
    Public Function Connect(ByVal COMPortName As String) As Boolean
        If (COMPortName Is Nothing) Then
            Return (False)
        End If
        If (Me.is_open) Then
            Me.Disconnect()
        End If
        Try
            ComPort = My.Computer.Ports.OpenSerialPort(COMPortName)
            With ComPort
                .BaudRate = 230400
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .Handshake = IO.Ports.Handshake.None
                .DtrEnable = True ' resets arduino
                '.Encoding = System.Text.Encoding.Default
                '.WriteBufferSize = 1
                '.ReadBufferSize = 1
                .ReceivedBytesThreshold = 1             'threshold: one byte in buffer > event is fired                 
                .NewLine = vbCrLf
                .ReadTimeout = 1000
            End With
            Me.is_open = True
            Thread.Sleep(1500)
        Catch ex As Exception
            MessageBox.Show("Error opening COM Port" & vbCrLf & vbCrLf & _
                  ex.Message, _
                  "Error opening COM Port", _
                  MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return (False)
        End Try
        Return (True)
    End Function

    '*****************************************************
    '* Read from 32bit address
    '* return Long
    '*****************************************************
    Public Function PollType50(ByVal addr As Integer, ByVal size As Integer) As Long
        Dim retval As Long = -1
        Dim len As Integer = 0
        'Dim Read32Reply As String = ""        
        Dim a() As Byte = BitConverter.GetBytes(addr)
        Dim cmd = "$S,4,50," & Hex(a(3)) & "," & Hex(a(2)) & "," & Hex(a(1)) & "," & Hex(a(0))
        Dim canline As String = ""
        Dim startTime As Long

        If (Not Me.is_open) Then
            Return (-1)
        End If

        ComPort.DiscardInBuffer()
        'Console.WriteLine("PollType50() TX: " & cmd)        
        ' first 50 msgs after the $S request
        ' need to have our replies otherwise something is wrong
        startTime = DateTime.Now.Millisecond
        ComPort.WriteLine(cmd)
        While True
            Try
                canline = ComPort.ReadLine()
            Catch ex As Exception
                Console.WriteLine("PollType50() ReadLine: " & ex.Message)
                Return (-1)
            End Try
            '  Console.WriteLine("PollType50: '" & canline & "'")
            If canline Is Nothing Then
                Exit While
            End If
            If (startTime + Me.TxTimeoutMillis < DateTime.Now.Millisecond) Then
                Console.WriteLine("PollType50() TxTimeoutMillis hit ")
                Exit While
            End If
            ' highest CAN80 reply id ? then we are done receiving    
            If canline.StartsWith("$F,4,7A0,") Then
                Exit While
            End If            
        End While
        'Console.WriteLine("PollType50() RX: " & canline)
        ' check if we get a valid reply to $VER
        If (canline.StartsWith("$F,4,7A0,")) Then
            Dim dArr() As String
            canline = canline.Replace(vbCr, "").Replace(vbLf, "")
            dArr = canline.Split(",")
            If (dArr.Length > 4) Then
                len = dArr(1)
                If (len > 0) Then
                    If (size = 4) Then
                        retval = Convert.ToInt32(dArr(3), 16) * 256 * 256 * 256 + _
                             Convert.ToInt32(dArr(4), 16) * 256 * 256 + _
                             Convert.ToInt32(dArr(5), 16) * 256 + _
                             Convert.ToInt32(dArr(6), 16)
                    End If
                    If (size = 2) Then
                        retval = Convert.ToInt32(dArr(3), 16) * 256 + _
                             Convert.ToInt32(dArr(4), 16)
                    End If
                    If (size = 1) Then
                        retval = Convert.ToInt32(dArr(3), 16)
                    End If
                End If
            End If
        Else
            Console.WriteLine("PollType50() RX: no valid reply - timeout hit " & canline)
            Return (-1)
        End If
        Return (retval)
    End Function


    '*****************************************************
    '* Read from 32bit address
    '* return Bytes()
    '*****************************************************
    Public Function PollType50Bytes(ByVal addr As Integer, ByVal size As Integer) As Byte()
        Dim retval() As Byte = Nothing
        Dim len As Integer = 0
        'Dim Read32Reply As String = ""        
        Dim a() As Byte = BitConverter.GetBytes(addr)
        Dim cmd = "$S,4,50," & Hex(a(3)) & "," & Hex(a(2)) & "," & Hex(a(1)) & "," & Hex(a(0))
        Dim canline As String = ""
        Dim startTime As Long

        If (Not Me.is_open) Then
            Return (retval)
        End If

        ComPort.DiscardInBuffer()
        Console.WriteLine("PollType50Bytes() TX: " & cmd)
        ' first 50 msgs after the $S request
        ' need to have our replies otherwise something is wrong
        startTime = DateTime.Now.Millisecond
        ComPort.WriteLine(cmd)
        While True
            Try
                canline = ComPort.ReadLine()
            Catch ex As Exception
                Console.WriteLine("PollType50Bytes() ReadLine: " & ex.Message)
                Return (retval)
            End Try
            '  Console.WriteLine("PollType50: '" & canline & "'")
            If canline Is Nothing Then
                Exit While
            End If
            If (startTime + Me.TxTimeoutMillis < DateTime.Now.Millisecond) Then
                Console.WriteLine("PollType50Bytes() TxTimeoutMillis hit ")
                Exit While
            End If
            ' highest CAN80 reply id ? then we are done receiving    
            If canline.StartsWith("$F,4,7A0,") Then
                Exit While
            End If
        End While
        Console.WriteLine("PollType50Bytes() RX: " & canline)
        ' check if we get a valid reply to $VER
        If (canline.StartsWith("$F,4,7A0,")) Then
            Dim dArr() As String
            canline = canline.Replace(vbCr, "").Replace(vbLf, "")
            dArr = canline.Split(",")
            If (dArr.Length > 4) Then
                len = dArr(1)
                If (len > 0) Then
                    Array.Resize(retval, size)
                    If (size = 4) Then
                        retval(0) = Convert.ToInt32(dArr(3), 16)
                        retval(1) = Convert.ToInt32(dArr(4), 16)
                        retval(2) = Convert.ToInt32(dArr(5), 16)
                        retval(3) = Convert.ToInt32(dArr(6), 16)
                    End If
                    If (size = 2) Then
                        retval(0) = Convert.ToInt32(dArr(3), 16)
                        retval(1) = Convert.ToInt32(dArr(4), 16)                        
                    End If
                    If (size = 1) Then
                        retval(0) = Convert.ToInt32(dArr(3), 16)
                    End If
                End If
            End If
        Else
            Console.WriteLine("PollType50Bytes() RX: no valid reply - timeout hit " & canline)
            Return (retval)
        End If
        Return (retval)
    End Function


    '*****************************************************
    '* Read OBD Mode and Pid
    '*****************************************************
    Public Function PollTypeOBD(ByVal Mode As Integer, ByVal Pid As Integer) As Byte()
        Dim retval() As Byte = Nothing
        Dim can_len As Integer = 0                
        Dim obd_cmd As String
        Dim obd_tx_len As Integer = 0
        Dim canline As String = ""
        Dim startTime As Long
        If (Not Me.is_open) Then
            Return (retval)
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' calculate size of PID to see if we need 1 or 2 bytes
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''
        If (Pid > &HFF) Then
            obd_tx_len = 3
            obd_cmd = "$S," & 1 + obd_tx_len & ",7DF," & Hex(obd_tx_len) & "," & Hex(Mode) & "," & Hex(Pid >> 8) & "," & Hex(Pid And &HFF)
        Else
            obd_tx_len = 2
            obd_cmd = "$S," & 1 + obd_tx_len & ",7DF," & Hex(obd_tx_len) & "," & Hex(Mode) & "," & Hex(Pid)
        End If
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' calculate the regex for the expected successful response
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        Dim rx_regex As Regex = New Regex("^\$F,\d,7E8,")
        Console.WriteLine("PollTypeOBD() TX: " & obd_cmd)

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''        
        ' loop until we get what we want or TxTimeoutMillis is hit
        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        startTime = DateTime.Now.Millisecond
        ComPort.DiscardInBuffer()
        ComPort.WriteLine(obd_cmd)
        While True
            Try
                canline = ComPort.ReadLine()
            Catch ex As Exception
                Console.WriteLine("PollTypeOBD() ReadLine: " & ex.Message)
                Return (retval)
            End Try
            Console.WriteLine("PollTypeOBD: '" & canline & "'")
            If canline Is Nothing Then
                Exit While
            End If
            If (startTime + Me.TxTimeoutMillis < DateTime.Now.Millisecond) Then
                Console.WriteLine("PollTypeOBD() TxTimeoutMillis hit ")
                Exit While
            End If

            Dim lmatch As Match = rx_regex.Match(canline)
            If lmatch.Success Then
                Exit While
            End If            
        End While
        Console.WriteLine("PollTypeOBD() RX: " & canline)
        ''''''''''''''''''''''''''''''''''''''''''''''''
        ' check if we have a valid reply 
        ''''''''''''''''''''''''''''''''''''''''''''''''
        Dim match As Match = rx_regex.Match(canline)
        If match.Success Then
            Dim dArr() As String
            canline = canline.Replace(vbCr, "").Replace(vbLf, "")
            dArr = canline.Split(",")
            If (dArr.Length > 2) Then
                can_len = dArr(1)
                Dim obd_rx_mode As Integer = Convert.ToInt32(dArr(4), 16)
                Dim obd_rx_pid As Integer = 0
                Dim obd_rx_len As Integer = Convert.ToInt32(dArr(3), 16)
                Dim obd_rx_datalen As Integer = obd_rx_len - obd_tx_len
                If (obd_tx_len = 2) Then
                    obd_rx_pid = Convert.ToInt32(dArr(5), 16)
                ElseIf (obd_tx_len = 3) Then
                    obd_rx_pid = Convert.ToInt32(dArr(5), 16) << 8 Or Convert.ToInt32(dArr(6), 16)
                End If
                Console.WriteLine("PollTypeOBD() Reply Mode: 0x" & Hex(obd_rx_mode))
                Console.WriteLine("PollTypeOBD() Reply PID: 0x" & Hex(obd_rx_pid))
                Console.WriteLine("PollTypeOBD() Reply Data Size: 0x" & Hex(obd_rx_datalen))
                If (obd_rx_pid = Pid) And (obd_rx_mode - &H40 = Mode) Then
                    ' successful OBD reply                    
                    Array.Resize(retval, obd_rx_len - obd_tx_len)
                    For i As Integer = 0 To obd_rx_datalen - 1
                        retval(i) = Convert.ToInt32(dArr(4 + obd_tx_len + i), 16)
                    Next
                    Return (retval)
                End If
            End If
        Else
            Console.WriteLine("PollTypeOBD() RX: no valid reply - timeout hit " & canline)
            Return (retval)
        End If
        Return (retval)
    End Function
End Class
