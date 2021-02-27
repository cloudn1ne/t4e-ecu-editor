Imports System.Xml

Public Structure XMLConfiguration    
    Dim calfile_vaddr As Long
    Dim calfile_headersize As Long
    Dim calfile_vin As Long
    Dim calfile_model As Long
    Dim calfile_lockstate As Long
    Dim calfile_checksum As Long
End Structure

' holds <datalogger> elements
Public Structure XMLDatalogDefinition
    Dim num As Integer
    Dim size As Integer
    Dim name As String
    Dim addr As Integer
    Dim calc As String
    Dim unit As String
    Dim cast As String
    Dim precision As Integer
    Dim offset As Integer
    Dim description As String
    Dim value As Integer
    Dim link_name As String
    Dim link_calc As String

    Public Sub SetValue(ByVal value As Integer)
        Me.value = value
    End Sub
End Structure

' holds <map> elements
Public Structure XMLMapDefinition
    Dim num As Integer
    Dim dimension As Integer
    Dim bits As Integer
    Dim xsize As Integer
    Dim shift As Integer
    Dim ysize As Integer
    Dim func As String
    Dim xref_result As String
    Dim lut_r As Long
    Dim lut_a As Long
    Dim lut_b As Long
    Dim input_a As Long
    Dim input_b As Long
    Dim lut_r_name As String
    Dim lut_a_name As String
    Dim lut_b_name As String
    Dim input_a_name As String
    Dim input_a_link_name As String
    Dim input_a_maxval As Integer
    Dim input_a_unit As String
    Dim input_b_name As String
    Dim input_b_unit As String
    Dim input_b_link_name As String
    Dim interpolate As Boolean
    Dim output_name As String
    Dim output_unit As String
    Dim lut_r_calc As String
    Dim lut_r_cast As String
    Dim lut_a_calc As String
    Dim lut_b_calc As String
    Dim lut_r_precision As Integer
    Dim lut_a_precision As Integer
    Dim lut_b_precision As Integer
End Structure

Public Class MapDefinitions
    Private xml_doc As New XmlDocument
    Private Maps As New List(Of XMLMapDefinition)
    Private Datalog80Vars As New List(Of XMLDatalogDefinition)
    Private Datalog50Vars As New List(Of XMLDatalogDefinition)
    Private Config As XMLConfiguration

    Public Function GetDatalog80Vars()
        Return (Datalog80Vars)
    End Function

    Public Function GetDatalog50Vars()
        Return (Datalog50Vars)
    End Function

    Public Function GetMaps()
        Return (Maps)
    End Function

    Public Function GetConfig()
        Return (Config)
    End Function


    '* Load XML file
    '* maps,datalogger vars
    Public Sub LoadFromXML(ByVal filename As String)
        Dim xml_doc As New XmlDocument
        Dim xml_maps As XmlNode
        Dim m As XMLMapDefinition
        Dim d As XMLDatalogDefinition
        Dim c As Integer
        Dim var_offset As Integer        

        ' make sure we clear everything (in case of a 'reload')
        Maps.Clear()
        Datalog80Vars.Clear()
        Datalog50Vars.Clear()
        xml_doc.Load(filename)

        '************************************
        '*** handle configration elements
        '************************************        
        Config.calfile_vaddr = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_vaddr"), 16)
        Config.calfile_headersize = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_headersize"), 16)
        Config.calfile_vin = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_vin"), 16)
        Config.calfile_model = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_model"), 16)
        Config.calfile_lockstate = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_lockstate"), 16)
        Config.calfile_checksum = Convert.ToInt32(GetXMLChild(xml_doc.ChildNodes(0), "calfile_checksum"), 16)
        '************************************
        '*** handle <datalogger><var80>'s
        '************************************   
        xml_maps = xml_doc.SelectSingleNode("/t4e/datalogger")
        c = 0
        var_offset = 0
        Try
            For Each node As XmlNode In xml_maps.ChildNodes
                If (node.Name = "var80") Then
                    ' check if we should ignore this variable
                    Dim ignore_flag As Boolean = False
                    If (node.Attributes.GetNamedItem("ignore") IsNot Nothing) Then
                        If (node.Attributes.GetNamedItem("ignore").Value > 0) Then
                            ignore_flag = True
                        End If
                    End If
                    If (GetXMLChild(node, "link_name") IsNot Nothing) Then
                        d.link_calc = GetXMLChild(node, "link_calc")
                        d.link_name = GetXMLChild(node, "link_name")
                    End If
                    d.size = GetXMLChild(node, "size")
                    If (ignore_flag = False) Then
                        d.num = c
                        d.name = GetXMLChild(node, "name")
                        d.calc = GetXMLChild(node, "calc")
                        If (GetXMLChild(node, "unit") IsNot Nothing) Then
                            d.unit = GetXMLChild(node, "unit")
                        Else
                            d.unit = "?"
                        End If
                        d.precision = GetXMLChild(node, "precision")
                        d.description = GetXMLChild(node, "description")
                        d.cast = GetXMLChild(node, "cast")
                        d.offset = var_offset
                        d.value = 0 ' default 
                    End If
                    var_offset = var_offset + d.size
                    If (ignore_flag = False) Then
                        'Console.WriteLine("datalogger var: '" & d.name & "' (" & d.offset & ")")
                        Datalog80Vars.Add(d)
                        c += 1
                    End If
                End If
            Next
        Catch
        End Try
        '************************************
        '*** handle <datalogger><var50>'s
        '************************************   
        xml_maps = xml_doc.SelectSingleNode("/t4e/datalogger")
        c = 0
        Try
            For Each node As XmlNode In xml_maps.ChildNodes
                If (node.Name = "var50") Then
                    d.size = GetXMLChild(node, "size")
                    d.num = c
                    d.addr = Convert.ToInt32(GetXMLChild(node, "addr"), 16)
                    d.name = GetXMLChild(node, "name")
                    d.calc = GetXMLChild(node, "calc")
                    If (GetXMLChild(node, "unit") IsNot Nothing) Then
                        d.unit = GetXMLChild(node, "unit")
                    Else
                        d.unit = "?"
                    End If
                    d.precision = GetXMLChild(node, "precision")
                    d.description = GetXMLChild(node, "description")
                    d.cast = GetXMLChild(node, "cast")
                    d.value = 0 ' default 
                    var_offset = var_offset + d.size
                    Datalog50Vars.Add(d)
                    c += 1
                End If
            Next
        Catch
        End Try
        '************************************
        '*** handle <maps>
        '************************************
        xml_maps = xml_doc.SelectSingleNode("/t4e/maps")
        c = 0
        For Each node As XmlNode In xml_maps.ChildNodes
            'Console.WriteLine(node.Name)
            If (node.Name = "map") Then
                ' load Map structure from XML
                m.num = c
                m.dimension = GetXMLChild(node, "dimension")
                m.bits = GetXMLChild(node, "bits")
                m.xsize = GetXMLChild(node, "xsize")
                m.shift = GetXMLChild(node, "shift")
                m.ysize = GetXMLChild(node, "ysize")
                m.func = GetXMLChild(node, "function")
                m.xref_result = GetXMLChild(node, "xref_result")
                m.lut_r = Convert.ToInt32(GetXMLChild(node, "lut_r"), 16)
                m.lut_a = Convert.ToInt32(GetXMLChild(node, "lut_a"), 16)
                m.lut_b = Convert.ToInt32(GetXMLChild(node, "lut_b"), 16)
                m.input_a = Convert.ToInt32(GetXMLChild(node, "input_a"), 16)
                m.input_a_maxval = Convert.ToInt32(GetXMLChild(node, "input_a_maxval"), 16)
                m.input_b = Convert.ToInt32(GetXMLChild(node, "input_b"), 16)
                m.lut_r_name = GetXMLChild(node, "lut_r_name")
                m.lut_r_calc = GetXMLChild(node, "lut_r_calc")
                m.lut_r_cast = GetXMLChild(node, "lut_r_cast")
                m.lut_r_precision = GetXMLChild(node, "lut_r_precision")
                m.lut_a_name = GetXMLChild(node, "lut_a_name")
                m.lut_a_calc = GetXMLChild(node, "lut_a_calc")
                m.lut_a_precision = GetXMLChild(node, "lut_a_precision")
                m.lut_b_name = GetXMLChild(node, "lut_b_name")
                m.lut_b_calc = GetXMLChild(node, "lut_b_calc")
                m.lut_b_precision = GetXMLChild(node, "lut_b_precision")
                m.input_a_name = GetXMLChild(node, "input_a_name")
                m.input_a_link_name = GetXMLChild(node, "input_a_link_name")
                m.input_a_unit = GetXMLChild(node, "input_a_unit")
                m.input_b_name = GetXMLChild(node, "input_b_name")
                m.input_b_link_name = GetXMLChild(node, "input_b_link_name")
                m.input_b_unit = GetXMLChild(node, "input_b_unit")
                m.output_name = GetXMLChild(node, "output_name")
                m.output_unit = GetXMLChild(node, "output_unit")
                ' interpolated y/n based on IDA pro map function name
                If m.func.Contains("Interpolate") Then
                    m.interpolate = True
                Else
                    m.interpolate = False
                End If
                ' only load CALRAM maps
                If (m.lut_r > Config.calfile_vaddr) Then
                    Maps.Add(m)
                    c += 1
                End If

            End If
        Next node
    End Sub

    Private Function GetXMLChild(ByVal parent_xml As XmlNode, ByVal name As String)
        For Each cnode As XmlNode In parent_xml
            If (cnode.Name = name) Then
                Return (cnode.InnerText)
            End If
        Next
        Return (Nothing)
    End Function
End Class
