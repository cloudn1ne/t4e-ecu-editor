Imports System.Net.Sockets
Imports System.Threading
Imports System.IO

Public Class CAN2WLAN

    Dim Port As Integer = 23
    Dim ip As String = ""
    Dim Client As New TcpClient
    Dim Writer As StreamWriter
    Dim Reader As StreamReader
    Dim VersionString As String
    Dim left_over_str As String = ""
    Private Shared CAN80Bytes As New List(Of Byte)

    Private Structure CANMsg
        Dim can_id As Integer
        Dim len As Integer
        Dim data As List(Of Byte)
    End Structure


    Public Function GetCAN80Bytes()
        Dim CAN80BytesCopy As New List(Of Byte)
        CAN80BytesCopy.Clear()
        SyncLock CAN80Bytes
            'If (CAN80Bytes.Count = 368) Then
            If (CAN80Bytes.Count >= 352) Then
                For i As Integer = 0 To CAN80Bytes.Count - 1
                    CAN80BytesCopy.Add(CAN80Bytes(i))
                Next
            End If
        End SyncLock
        Return (CAN80BytesCopy)
    End Function

    Public Function isConnected() As Boolean
        If (Client IsNot Nothing) Then
            Return (Client.Connected)
        Else
            Return (False)
        End If

    End Function

    Public Sub Disconnect()
        If (Client.Connected) Then
            'Client.GetStream.Close()
            Writer.Dispose()
            Reader.Dispose()
        End If
    End Sub

    Public Sub PollThread()
        left_over_str = ""
        If (Client IsNot Nothing) Then
            While Client.Connected
                Poll()
            End While
        End If
        Console.WriteLine("PollThread() ended")
        Thread.CurrentThread.Abort()
    End Sub

    Public Sub Poll()

        Dim reply_str As String = left_over_str
        Dim reply_data As New List(Of CANMsg)
        Dim c As CANMsg
        ' minimum, maximum, step size of 0x80 CAN replies
        Dim start_can_id = &H200
        'Dim end_can_id = &H2B4
        Dim end_can_id = &H2AC
        Dim step_can_id = &H4
        Dim expected_can_id = start_can_id

        ' (0x80) 0x0 0x1
        'Console.WriteLine("polling CAN 0x80 data")
        Try
            Writer.Write("$S,2,80,0,1" & vbCrLf)
            Writer.Flush()
            Reader = New StreamReader(Client.GetStream())
            Thread.Sleep(200)
            While Reader.Peek > -1
                reply_str = reply_str + Convert.ToChar(Reader.Read()).ToString
            End While
            If (reply_str.IndexOf("$S,OK" & vbCrLf) <> -1) Then
                SyncLock CAN80Bytes
                    CAN80Bytes.Clear()
                End SyncLock
                left_over_str = reply_str
                reply_str.IndexOf(vbCrLf)
                reply_str = reply_str.Remove(0, reply_str.IndexOf("$S,OK" & vbCrLf) + 5 + vbCrLf.Length)
                Dim ansArr() As String = reply_str.Split(vbCrLf)
                For Each a As String In ansArr
                    a = a.Replace(vbCr, "").Replace(vbLf, "")
                    Dim aArr() As String = a.Split(",")
                    ' minimum valid answer length 4 ($F,X,CAN_ID,DATA1,..)
                    If (aArr.Length > 4) Then
                        c.len = aArr(1)
                        Try
                            c.can_id = Convert.ToInt32(aArr(2), 16)
                        Catch ex As Exception
                            Console.WriteLine(ex)
                            Exit Sub
                        End Try
                        If ((c.len > 0) And (expected_can_id = c.can_id) And (c.len = aArr.Length - 3)) Then
                            'Console.WriteLine("can_id: " & Hex(c.can_id) & " len = " & c.len & " aArrLength " & aArr.Length)
                            'Console.WriteLine("line: '" & a & "'")
                            For i As Integer = 3 To aArr.Length - 1
                                'Console.Write(aArr(i) & " ")  
                                Dim v As Integer
                                Try
                                    v = Convert.ToInt32(aArr(i), 16)
                                Catch ex As Exception
                                    Exit Sub
                                End Try

                                SyncLock CAN80Bytes
                                    CAN80Bytes.Add(v)
                                End SyncLock
                            Next
                            'Console.WriteLine()
                            If (end_can_id = c.can_id) Then
                                'Console.WriteLine("===== FULL FRAME RECEIVED =====")
                                'Console.WriteLine("data size read " & CAN80Bytes.Count)
                                left_over_str = ""
                                Exit Sub
                            End If
                            expected_can_id = expected_can_id + step_can_id ' expect next can_id
                        End If
                        'Console.WriteLine("data: '" & a & "'")
                    End If
                Next
                Exit Sub
            Else
                'Console.WriteLine("no acknowlegde received for $S command")
                'Console.WriteLine("DATA FAILED: '" & reply_str & "'")
                Exit Sub
            End If
        Catch ex As Exception
            Console.WriteLine(ex)
            Dim Errorresult As String = ex.Message
            SyncLock CAN80Bytes
                CAN80Bytes.Clear()
            End SyncLock
        End Try
    End Sub

    Public Function GetVersion() As String
        Return (VersionString)
    End Function

    Public Function Connect(ByVal ip As String) As Boolean
        Dim ar As IAsyncResult
        Dim VersionReply As String = ""
        Try
            Me.ip = ip
            If Client.Connected Then
                Disconnect()
            End If
            Client = New TcpClient
            ar = Client.BeginConnect(Me.ip, Me.Port, Nothing, Nothing)
            If Not ar.AsyncWaitHandle.WaitOne(3000, False) Then
                MessageBox.Show("Timeout connecting to device" & vbCrLf & vbCrLf & _
                        "Please Review CAN IP Address and ensure adapter is powered", _
                        "Timeout connecting to device", _
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return (Client.Connected)
                Exit Function
            End If

            Client.ReceiveBufferSize = 1
            Client.ReceiveTimeout = 2000
            Writer = New StreamWriter(Client.GetStream())
            Reader = New StreamReader(Client.GetStream())
            Writer.Write("$VER" & vbCrLf)
            Writer.Flush()
            While Reader.Peek > -1
                VersionReply = VersionReply + Convert.ToChar(Reader.Read()).ToString
            End While
            Client.ReceiveBufferSize = 100
            ' check if we get a valid reply to $VER
            If (VersionReply.StartsWith("$VER,OK,")) Then
                Dim verArr() As String
                VersionReply = VersionReply.Replace(vbCr, "").Replace(vbLf, "")
                verArr = VersionReply.Split(",")
                If (verArr(2) IsNot Nothing) Then
                    VersionString = verArr(2)
                Else
                    MessageBox.Show("Invalid reply for $VER query" & vbCrLf & vbCrLf & _
                      "Please Review CAN IP Address", _
                      "Error connecting to device", _
                      MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return (Client.Connected)
                    Exit Function
                End If
            Else
                MessageBox.Show("Invalid reply for $VER query" & vbCrLf & vbCrLf & _
                       "Please Review CAN IP Address", _
                       "Error connecting to device", _
                       MessageBoxButtons.OK, MessageBoxIcon.Error)

                Return (Client.Connected)
                Exit Function
            End If
        Catch ex As Exception
            'Console.WriteLine(ex)
            Dim Errorresult As String = ex.Message
            '  MessageBox.Show(Errorresult & vbCrLf & vbCrLf & _
            '                  "Please Review CAN IP Address", _
            '                  "Error connecting to device", _
            '                  MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return (Client.Connected)
        End Try
        Return (Client.Connected)
    End Function
End Class
