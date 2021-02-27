Imports System.IO
Imports System.Text.RegularExpressions


Public Class CALFileHandler
    Declare Ansi Function t4e_cal_chksum Lib "t4e_checksum_dll.dll" Alias "t4e_cal_chksum" (ByVal data() As Byte, ByVal offset As UInteger, ByVal size As UInteger) As UInteger
    Private CALBytes As Byte()
    Private Config As XMLConfiguration

    Public Function LoadCALFile(ByVal strFilename As String, ByVal Config As XMLConfiguration) As Byte()
        Try
            Using fsSource As FileStream = New FileStream(strFilename, FileMode.Open, FileAccess.Read)
                ' Read the source file into a byte array.
                Dim bytes() As Byte = New Byte((fsSource.Length) - 1) {}
                Dim numBytesToRead As Integer = CType(fsSource.Length, Integer)
                Dim numBytesRead As Integer = 0

                'tsProgressBar.Minimum = 0
                'tsProgressBar.Maximum = numBytesToRead

                While (numBytesToRead > 0)
                    ' Read may return anything from 0 to numBytesToRead.
                    Dim n As Integer = fsSource.Read(bytes, numBytesRead, _
                        numBytesToRead)
                    ' Break when the end of the file is reached.
                    If (n = 0) Then
                        Exit While
                    End If
                    numBytesRead = (numBytesRead + n)
                    numBytesToRead = (numBytesToRead - n)

                    'tsProgressBar.Value = numBytesRead

                End While
                numBytesToRead = bytes.Length
                Me.CALBytes = bytes
                Me.Config = Config
                Return bytes
            End Using
        Catch ex As Exception
            MsgBox("Error opening file " & strFilename & " " & ex.Message)
        End Try
    End Function

    Public Function GetVersion() As String
        Dim ByteStr(31) As Byte
        If (Config.calfile_headersize > 0) Then
            Array.Copy(CALBytes, Config.calfile_headersize, ByteStr, 0, 32)
            Return System.Text.Encoding.ASCII.GetString(ByteStr)
        End If
        Return ("Unknown")
    End Function

    Public Function GetECUType() As String
        Dim ByteStr(31) As Byte

        Array.Copy(CALBytes, 12, ByteStr, 0, 32)
        Return System.Text.Encoding.ASCII.GetString(ByteStr)

    End Function

    Public Function GetVIN() As String
        Dim ByteStr(16) As Byte
        If (Config.calfile_vin > 0) Then
            Try
                Array.Copy(CALBytes, Config.calfile_vin - Config.calfile_vaddr + Config.calfile_headersize, ByteStr, 0, 17)
                Return System.Text.Encoding.ASCII.GetString(ByteStr)
            Catch ex As Exception
                Return ("Unknown")
            End Try
        End If
        Return ("Unknown")
    End Function

    Public Function GetModel() As String
        Dim ByteStr(31) As Byte
        If (Config.calfile_model > 0) Then
            Try
                Array.Copy(CALBytes, Config.calfile_model - Config.calfile_vaddr + Config.calfile_headersize, ByteStr, 0, 32)
                Return System.Text.Encoding.ASCII.GetString(ByteStr)
            Catch ex As Exception
                Return ("Unknown")
            End Try
        End If
        Return ("Unknown")
    End Function

    Public Function GetModelDateTime() As DateTime
        Dim ByteStr(31) As Byte
        If (Config.calfile_headersize > 0) Then
            Array.Copy(CALBytes, Config.calfile_headersize, ByteStr, 0, 32)
            Dim model As String = System.Text.Encoding.ASCII.GetString(ByteStr)
            Try
                ' reformat date so VB.NET can handle it
                Dim date_str As String = model.Split(" ")(1)
                Dim month_str As String = date_str.Split("-")(0)
                Dim day_str As String = date_str.Split("-")(1)
                Dim year_str As String = date_str.Split("-")(2)
                date_str = year_str & "-" & month_str & "-" & day_str
                Dim time As DateTime = DateTime.Parse(date_str)
                Return (time)
            Catch
                Return (Nothing)
            End Try

        End If
        Return (Nothing)
    End Function

    Public Function GetFWLockState() As Boolean
        Dim ByteStr(3) As Byte
        If (Config.calfile_lockstate > 0) Then
            Try
                Array.Copy(CALBytes, Config.calfile_lockstate - Config.calfile_vaddr + Config.calfile_headersize, ByteStr, 0, 4)
                If (System.Text.Encoding.ASCII.GetString(ByteStr).Contains("WTF?")) Then
                    Return (False)
                End If
                Return (True)
            Catch ex As Exception
                Return (False)
            End Try
        End If
        Return (False)
    End Function

    '********************************************************************
    '* retrieve checksum stored in CALBytes
    '********************************************************************
    Public Function GetStoredChecksum() As UInteger
        Dim ByteStr(1) As Byte
        If (Config.calfile_checksum > 0) Then
            Try
                Array.Copy(CALBytes, Config.calfile_checksum - Config.calfile_vaddr + Config.calfile_headersize, ByteStr, 0, 2)
                ' little vs big endian - we need to swap the bytes
                Dim retval As UInteger = BitConverter.ToInt16(SwapBytes16(ByteStr), 0) And &HFFFF
                Return (retval)
            Catch ex As Exception
                Return (0)
            End Try
        End If
        Return 0
    End Function

    '********************************************************************
    '* calculcate checksum based on the CALBytes
    '********************************************************************
    Public Function GetCalculatedChecksum() As UInteger
        ' calculate checksum from CALBytes unfortunatly that depends on the firmware type (MY ?)
        ' Y/M/D format strings
        Dim calbytes_crc As UInteger
        If DateTime.Compare(GetModelDateTime(), DateTime.Parse("2009-08-01")) < 0 Then
            ' old firmware prior 2009-08-01
            calbytes_crc = t4e_cal_chksum(CALBytes, &H6C, &H3CB4 - &H22)
        Else
            ' newer firmware                     
            calbytes_crc = t4e_cal_chksum(CALBytes, &H6C, &H3CC4 - &H22)
        End If
        Return (calbytes_crc)
    End Function

    Public Function SaveCALFile(ByVal strFilename As String, ByVal bytesToWrite() As Byte) As Boolean
        Using fsNew As FileStream = New FileStream(strFilename, FileMode.Create, FileAccess.Write)
            fsNew.Write(bytesToWrite, 0, bytesToWrite.Length)
        End Using
    End Function

    Public Function GetCALBytes() As Byte()
        Return (Me.CALBytes)
    End Function

    ' swap between big/little endian - 16bit
    Private Function SwapBytes16(ByVal b As Byte()) As Byte()
        Dim b0, b1 As Byte
        b0 = b(0)
        b1 = b(1)
        b(1) = b0
        b(0) = b1
        Return (b)
    End Function
End Class
