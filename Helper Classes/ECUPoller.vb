'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' manages polling of data from the ECU 
' * for the datalogger
' * for the map editor (live view)
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Imports System.Threading
Imports Ciloci.Flee
Imports Ciloci.Flee.CalcEngine
Imports System.Math

Public Class ECUPoller

    Private Adapter As Object

    ' poller Thread variables
    Private pThread As Threading.Thread
    Private pThreadRunning As Boolean = False

    ' list of all elements that we need to fetch
    Private Shared PollList() As PollListElement

    ' maximum Polltype 0x80 ID seen
    Private CAN80MaxID As Integer

    ' defines one element on the PollList
    ' (how to poll) and also stores the results fetched
    Public Structure PollListElement
        Dim type As CANPollType
        Dim addr As Integer
        Dim size As Integer
        Dim pid As Integer
        Dim dataval As Integer
        Dim databytes() As Byte
        Dim active As Boolean
    End Structure

    ' possible ways to fetch data from the ECU
    Public Enum CANPollType
        None = 0
        Type80 = 8
        Type50 = 1
        TypeMode1 = 2
        TypeMode22 = 3
    End Enum

    '*****************************************************
    '* link the poller to the right Adapter
    '*****************************************************
    Public Sub New(ByRef adapter As Object)
        Me.Adapter = adapter
    End Sub

    '*****************************************************
    '* return running state of PollThread    
    '*****************************************************
    Public Function GetPollThreadStatus() As Boolean
        Return (pThreadRunning)
    End Function

    '*****************************************************
    '* get a copy of the PollList for the UI thread
    '* fully sync locked
    '*****************************************************
    Public Function GetPollList() As PollListElement()
        Dim CopyOfPollList() As PollListElement

        SyncLock PollList
            ' copy gets same size
            Array.Resize(CopyOfPollList, PollList.Length)
            For i = 0 To PollList.Length - 1
                CopyOfPollList(i) = PollList(i)
            Next
        End SyncLock
        Return (CopyOfPollList)
    End Function

    '*****************************************************
    '* Start the background thread that does the polling    
    '*****************************************************
    Public Sub StartPollThread()
        Me.CAN80MaxID = Adapter.GetHighestCAN80ReplyID()
        pThreadRunning = True
        pThread = New Thread(AddressOf Me.RunPollThread)
        pThread.Name = "T4e Datalogger / Poller"
        pThread.Start()
    End Sub

    '*****************************************************
    '* Stop the background thread that does the polling    
    '*****************************************************
    Public Sub StopPollThread()
        If (pThread IsNot Nothing) Then
            If (pThread.IsAlive) Then
                pThread.Abort()
                pThread.Join()
            End If
            pThread = Nothing
        End If
        pThreadRunning = False
    End Sub


    '*****************************************************
    '* Thread to the do the background polling of selected vars
    '* Calls: Poll80
    '*****************************************************
    Private Sub RunPollThread()
        Dim arLen As Integer = 0
        Dim active As Boolean = False

        Console.WriteLine("RunPollThread() started")
        While Adapter.isConnected() And pThreadRunning
            ' loop PollList see what needs to be queried      
            SyncLock PollList
                arLen = PollList.Length
            End SyncLock

            For i = 0 To arLen - 1
                SyncLock PollList
                    active = PollList(i).active
                End SyncLock
                If (PollList(i).type = CANPollType.Type80) And (active) Then
                    Dim databytes() As Byte = Adapter.PollType80(CAN80MaxID)
                    SyncLock PollList
                        PollList(i).databytes = databytes
                    End SyncLock
                End If
                If (PollList(i).type = CANPollType.Type50) And (active) Then
                    Dim dataval As Integer = Adapter.PollType50(PollList(i).addr, PollList(i).size)
                    SyncLock PollList
                        PollList(i).dataval = dataval
                    End SyncLock
                End If
            Next
        End While
        Console.WriteLine("RunPollThread() ended")
    End Sub

    '*****************************************************
    '* Activate polling of an entry in the PollList    
    '*****************************************************
    Public Sub SetPollTypeActive(ByVal PollType As CANPollType, ByVal addr As Integer, ByVal size As Integer, ByVal pid As Integer)
        Dim arLen As Integer = 0

        SyncLock PollList
            arLen = PollList.Length
        End SyncLock

        For i = 0 To arLen - 1
            If (PollList(i).type = PollType) And (PollType = CANPollType.Type50) And (PollList(i).addr = addr) Then
                SyncLock PollList
                    PollList(i).active = True
                End SyncLock
            End If

            If (PollList(i).type = PollType) And (PollList(i).type = CANPollType.Type80) Then
                SyncLock PollList
                    PollList(i).active = True
                End SyncLock
            End If
        Next
    End Sub

    '*****************************************************
    '* Reset PollList()
    '*****************************************************
    Public Sub ClearPollTypes()
        Array.Resize(PollList, 1)
        PollList(0).type = CANPollType.None
    End Sub

    '*****************************************************
    '* Add new PollType to the PollList
    '*****************************************************
    Public Sub AddPollType(ByVal PollType As CANPollType, ByVal addr As Integer, ByVal size As Integer, ByVal pid As Integer)
        Dim pe As New PollListElement

        pe.type = PollType
        pe.addr = addr
        pe.size = size
        pe.pid = pid
        pe.active = False
        If (PollList Is Nothing) Then
            Array.Resize(PollList, 1)
            PollList(0) = pe
        Else
            Array.Resize(PollList, PollList.Length + 1)
            PollList(PollList.Length - 1) = pe
        End If
    End Sub

    '*****************************************************
    '* return value by address
    '* *) supports Type 0x50 vars only
    '*****************************************************
    Public Function GetValueByAddr(ByVal addr As Integer) As Long
        Dim PollListCopy() As ECUPoller.PollListElement

        Try
            PollListCopy = ECU.Poller.GetPollList()
            For Each pe As ECUPoller.PollListElement In PollListCopy
                If (pe.type = ECUPoller.CANPollType.Type50) And (pe.addr = addr) Then
                    Return (pe.dataval)
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("GetValueByAddr() - " & ex.Message)
        End Try        
        Return (-1)
    End Function

    '*****************************************************
    '* return value by link name
    '* *) supports Type 0x80 vars only
    '*****************************************************
    Public Function GetValueByLinkName(ByVal link_name As String, ByRef Vars As List(Of XMLDatalogDefinition)) As Double
        Dim PollListCopy() As ECUPoller.PollListElement
        Dim CAN80Bytes() As Byte = Nothing
        Dim fmt As String
        Dim retval As Double = -1

        ' get our copy of CAN 0x80 data
        Try
            PollListCopy = ECU.Poller.GetPollList()
            For Each pe As ECUPoller.PollListElement In PollListCopy
                If (pe.type = ECUPoller.CANPollType.Type80) Then
                    CAN80Bytes = pe.databytes
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("GetValueByLinkName() - unable to get Type80 databytes - " & ex.Message)
            Return (retval)
        End Try

        If (CAN80Bytes IsNot Nothing) Then
            For Each dvar As XMLDatalogDefinition In Vars
                If (dvar.link_name = link_name) Then
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
                    If (dvar.link_calc IsNot Nothing) Then
                        Dim ec As New Ciloci.Flee.ExpressionContext
                        Dim ex As IDynamicExpression
                        ec.Imports.AddType(GetType(Math))
                        If (dvar.precision > 0) Then
                            ec.Options.IntegersAsDoubles = True
                        End If
                        ec.Variables("VAR") = val
                        Try
                            ex = ec.CompileDynamic(dvar.link_calc)
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
                End If
            Next
        End If
        Return (-1)
    End Function
End Class
