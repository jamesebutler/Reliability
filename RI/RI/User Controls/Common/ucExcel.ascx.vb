Imports System.Data
Imports System.IO
Imports System.Xml
Imports Devart.Data.Oracle

Partial Class ucExcel
    Inherits System.Web.UI.UserControl

    Public Event DisplayExcel_Click()

    Private mButtonText As String
    Public Property ButtonText() As String
        Get
            Return mButtonText
        End Get
        Set(ByVal value As String)
            mButtonText = value
        End Set
    End Property
    Private mShowButton As Boolean = False
    Public Property ShowButton() As Boolean
        Get
            Return mShowButton
        End Get
        Set(ByVal value As Boolean)
            mShowButton = value
        End Set
    End Property
    Public Sub DisplayExcel(ByRef source As DataSet)

        Try
            Dim ExcelXML As String = WriteExcelXml(source)
            Dim name As String = "DisplayExcelXML" & Today.Second.ToString
            Session.Remove("ExcellXML")
            Session.Add("ExcelXML", ExcelXML)
            Dim excelUrl As String = Page.ResolveClientUrl("~/DisplayExcel.aspx") & "?id=" & Now.ToUniversalTime
            Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, name, "PopupWindow('" & excelUrl & "','ExcelXML',800,600,'yes','no','yes');", True)
        Catch ex As Exception
            Server.ClearError()
        Finally

        End Try
    End Sub

    Public Sub DisplayExcel(ByRef source As OracleDataReader)

        Try
            Dim ExcelXML As String = WriteExcelXml(source)
            Session.Remove("ExcellXML")
            Session.Add("ExcelXML", ExcelXML)
            Dim excelUrl As String = Page.ResolveClientUrl("~/DisplayExcel.aspx")
            Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "ExcelXML", "PopupWindow('" & excelUrl & "','ExcelXML',800,600,'yes','no','yes');", True)
        Catch ex As Exception
            Server.ClearError()
        Finally

        End Try
    End Sub

    Public Sub DisplayExcel(ByRef source As DataTableReader)

        Try
            Dim ExcelXML As String = WriteExcelXml(source)
            Session.Remove("ExcellXML")
            Session.Add("ExcelXML", ExcelXML)
            Dim excelUrl As String = Page.ResolveClientUrl("~/DisplayExcel.aspx")
            Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "ExcelXML", "PopupWindow('" & excelUrl & "','ExcelXML',800,600,'yes','no','yes');", True)
        Catch ex As Exception
            Server.ClearError()
        Finally

        End Try
    End Sub

    Public Sub DisplayExcel(ByVal excelXML As String)

        Try
            Session.Remove("ExcellXML")
            Session.Add("ExcelXML", ExcelXML)
            Dim excelUrl As String = Page.ResolveClientUrl("~/DisplayExcel.aspx")
            Web.UI.ScriptManager.RegisterStartupScript(Me.Page, Page.GetType, "ExcelXML", "PopupWindow('" & excelUrl & "','ExcelXML',800,600,'yes','no','yes');", True)
        Catch ex As Exception
            Server.ClearError()
        Finally

        End Try
    End Sub
    Private Function WriteExcelXml(ByRef source As DataSet) As String

        Dim excelDoc As New StringBuilder

        ' Send output to response stream
        'excelDoc = New System.IO.StreamWriter(resp.OutputStream)

        Dim startExcelXML As String = _
            "<?xml version='1.0' ?>" & vbCrLf & _
            "<Workbook " & _
            "xmlns='urn:schemas-microsoft-com:office:spreadsheet' " & vbCrLf & _
            "xmlns:o='urn:schemas-microsoft-com:office:office' " & vbCrLf & _
            "xmlns:x='urn:schemas-microsoft-com:office:excel' " & vbCrLf & _
            "xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet'>" & vbCrLf & _
            "<Styles>" & vbCrLf & _
            "<Style ss:ID='Default' ss:Name='Normal'>" & vbCrLf & _
            "<Alignment ss:Vertical='Bottom'/>" & vbCrLf & _
            "<Borders/>" & vbCrLf & _
            "<Font/>" & vbCrLf & _
            "<Interior/>" & vbCrLf & _
            "<NumberFormat/>" & vbCrLf & _
            "<Protection/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID='BoldColumn'>" & vbCrLf & _
            "<Font x:Family='Swiss' ss:Bold='1'/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s21"">" & vbCrLf & _
            "<NumberFormat ss:Format=""m/d/yyyy;@""/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s26"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[$-419]d\ mmm\ yy;@""/></Style>" & vbCrLf & _
  "<Style ss:ID=""s28"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[ENG][$-409]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
            "</Styles>" & vbCrLf
        '"<Style ss:ID='StringLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Decimal'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0.0000'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Integer'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='DateLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='mm/dd/yyyy;@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"</Styles>" & vbCrLf

        Dim endExcelXML As String = "</Workbook>"
        Dim rowCount As Integer = 0
        Dim sheetCount As Integer = 0
        Dim newPage As Boolean = True
        Dim maxRows As Integer = 64000
        Dim flushRows As Integer = 100

        ' Write XML header for Excel (changing single to double quotes)
        excelDoc.Append(Replace(startExcelXML, "'", Chr(34)))

        '// Write dataset
        For Each x As DataRow In source.Tables(0).Rows
            rowCount = rowCount + 1

            ' flush every 100 rows
            If ((rowCount Mod flushRows) = 0) Then
                'excelDoc.Flush()
                'If (Not resp.IsClientConnected) Then
                '    Throw New Exception("Client disconnected.")
                'End If
            End If

            'if the number of rows is > 64000 create a new page to continue output
            If (rowCount > maxRows) Then
                rowCount = 0
                newPage = True
            End If

            ' Start a new page?
            If (newPage) Then

                newPage = False

                ' Close out subsequent pages
                If (sheetCount > 0) Then

                    excelDoc.AppendLine("</Table>")
                    excelDoc.AppendLine(" </Worksheet>")

                End If

                ' Start a new worksheet
                sheetCount = sheetCount + 1
                excelDoc.AppendLine("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
                excelDoc.AppendLine("<Table>")
                excelDoc.AppendLine("<Row>")

                '// Write column headers
                Dim z As Integer = 0

                Do While (z < source.Tables(0).Columns.Count)
                    excelDoc.Append("<Cell ss:StyleID=""BoldColumn""><Data ss:Type=""String"">")
                    excelDoc.Append(source.Tables(0).Columns(z).ColumnName)
                    excelDoc.AppendLine("</Data></Cell>")
                    z = z + 1
                Loop

                excelDoc.AppendLine("</Row>")

            End If

            ' Write out a row of data
            excelDoc.AppendLine("<Row>")

            Dim y As Integer = 0
            Dim sb As New System.Text.StringBuilder
            Dim xxx As New XmlTextWriter(New System.IO.StringWriter(sb))

            Do While (y < source.Tables(0).Columns.Count)

                Dim rowType As System.Type
                rowType = x(y).GetType

                Select Case (rowType.ToString)

                    Case "System.String"

                        Dim XMLstring As String = x(y).ToString
                        XMLstring = XMLstring.Trim
                        XMLstring = XMLstring.Replace("&", "&")
                        XMLstring = XMLstring.Replace(">", ">")
                        XMLstring = XMLstring.Replace("<", "<")


                        sb = New System.Text.StringBuilder
                        xxx = New XmlTextWriter(New System.IO.StringWriter(sb))
                        xxx.WriteString(XMLstring)
                        xxx.Flush()
                        XMLstring = sb.ToString()

                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append(XMLstring)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.DateTime"
                        Dim XMLDate As Date = CType(x(y), Date)
                        Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                        Dim yr As String = XMLDate.Year
                        Dim mon As String = XMLDate.Month
                        Dim dy As String = XMLDate.Day
                        If mon.Length <> 2 Then mon = "0" & mon
                        If dy.Length <> 2 Then dy = "0" & dy
                        Dim dt As String = String.Format(dtformat, yr, mon, dy)

                        excelDoc.Append("<Cell ss:StyleID=""s21"">" & _
                            "<Data ss:Type=""DateTime"">")
                        excelDoc.Append(dt)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Boolean"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append(x(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Int16", "System.Int32", "System.Int64", "System.Byte"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(x(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Decimal", "System.Double"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(x(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.DBNull"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append("")
                        excelDoc.AppendLine("</Data></Cell>")

                    Case Else

                        Throw New Exception((rowType.ToString & " not handled."))

                End Select

                y = y + 1

            Loop

            excelDoc.AppendLine("</Row>")

        Next

        ' Close out XML and flush
        excelDoc.AppendLine("</Table>")
        excelDoc.AppendLine("</Worksheet>")
        excelDoc.Append(endExcelXML)
        'excelDoc.Flush()
        'excelDoc.Close()
        Return excelDoc.ToString
    End Function

    Private Function WriteExcelXml(ByRef source As OracleDataReader) As String

        Dim excelDoc As New StringBuilder

        ' Send output to response stream
        'excelDoc = New System.IO.StreamWriter(resp.OutputStream)

        Dim startExcelXML As String = _
            "<?xml version='1.0' ?>" & vbCrLf & _
            "<Workbook " & _
            "xmlns='urn:schemas-microsoft-com:office:spreadsheet' " & vbCrLf & _
            "xmlns:o='urn:schemas-microsoft-com:office:office' " & vbCrLf & _
            "xmlns:x='urn:schemas-microsoft-com:office:excel' " & vbCrLf & _
            "xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet'>" & vbCrLf & _
            "<Styles>" & vbCrLf & _
            "<Style ss:ID='Default' ss:Name='Normal'>" & vbCrLf & _
            "<Alignment ss:Vertical='Bottom'/>" & vbCrLf & _
            "<Borders/>" & vbCrLf & _
            "<Font/>" & vbCrLf & _
            "<Interior/>" & vbCrLf & _
            "<NumberFormat/>" & vbCrLf & _
            "<Protection/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID='BoldColumn'>" & vbCrLf & _
            "<Font x:Family='Swiss' ss:Bold='1'/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s21"">" & vbCrLf & _
            "<NumberFormat ss:Format=""m/d/yyyy;@""/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s26"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[$-419]d\ mmm\ yy;@""/></Style>" & vbCrLf & _
  "<Style ss:ID=""s28"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[ENG][$-409]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
            "</Styles>" & vbCrLf
        '"<Style ss:ID='StringLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Decimal'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0.0000'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Integer'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='DateLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='mm/dd/yyyy;@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"</Styles>" & vbCrLf

        Dim endExcelXML As String = "</Workbook>"
        Dim rowCount As Integer = 0
        Dim sheetCount As Integer = 0
        Dim newPage As Boolean = True
        Dim maxRows As Integer = 64000
        Dim flushRows As Integer = 100

        ' Write XML header for Excel (changing single to double quotes)
        excelDoc.Append(Replace(startExcelXML, "'", Chr(34)))

        '// Write dataset
        Do While source.Read()


            'For Each x As DataRow In source.Item source.Tables(0).Rows
            rowCount = rowCount + 1

            ' flush every 100 rows
            If ((rowCount Mod flushRows) = 0) Then
                'excelDoc.Flush()
                'If (Not resp.IsClientConnected) Then
                '    Throw New Exception("Client disconnected.")
                'End If
            End If

            'if the number of rows is > 64000 create a new page to continue output
            If (rowCount > maxRows) Then
                rowCount = 0
                newPage = True
            End If

            ' Start a new page?
            If (newPage) Then

                newPage = False

                ' Close out subsequent pages
                If (sheetCount > 0) Then

                    excelDoc.AppendLine("</Table>")
                    excelDoc.AppendLine(" </Worksheet>")

                End If

                ' Start a new worksheet
                sheetCount = sheetCount + 1
                excelDoc.AppendLine("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
                excelDoc.AppendLine("<Table>")
                excelDoc.AppendLine("<Row>")

                '// Write column headers
                Dim z As Integer = 0

                Do While (z < source.FieldCount)
                    excelDoc.Append("<Cell ss:StyleID=""BoldColumn""><Data ss:Type=""String"">")
                    excelDoc.Append(source.GetName(z)) 'Tables(0).Columns(z).ColumnName)
                    excelDoc.AppendLine("</Data></Cell>")
                    z = z + 1
                Loop

                excelDoc.AppendLine("</Row>")

            End If

            ' Write out a row of data
            excelDoc.AppendLine("<Row>")

            Dim y As Integer = 0
            Dim sb As New System.Text.StringBuilder
            Dim xxx As New XmlTextWriter(New System.IO.StringWriter(sb))

            Do While (y < source.FieldCount)

                Dim rowType As System.Type

                rowType = source.Item(y).GetType()

                Select Case (rowType.ToString)

                    Case "System.String"

                        Dim XMLstring As String = source.Item(y).ToString
                        XMLstring = XMLstring.Trim
                        XMLstring = XMLstring.Replace("&", "&")
                        XMLstring = XMLstring.Replace(">", ">")
                        XMLstring = XMLstring.Replace("<", "<")


                        sb = New System.Text.StringBuilder
                        xxx = New XmlTextWriter(New System.IO.StringWriter(sb))
                        xxx.WriteString(XMLstring)
                        xxx.Flush()
                        XMLstring = sb.ToString()


                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append(XMLstring)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.DateTime"
                        Dim XMLDate As Date = CType(source.Item(y), Date)
                        Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                        Dim yr As String = XMLDate.Year
                        Dim mon As String = XMLDate.Month
                        Dim dy As String = XMLDate.Day
                        If mon.Length <> 2 Then mon = "0" & mon
                        If dy.Length <> 2 Then dy = "0" & dy
                        Dim dt As String = String.Format(dtformat, yr, mon, dy)
                        Dim styleKey As String = "s21"
                        If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "EN-US" Then
                            styleKey = "s28"
                        ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "RU-RU" Then
                            styleKey = "s26"
                        End If
                        excelDoc.Append("<Cell ss:StyleID=""" & styleKey & """>" & _
                            "<Data ss:Type=""DateTime"">")
                        excelDoc.Append(dt)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Boolean"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Int16", "System.Int32", "System.Int64", "System.Byte"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Decimal", "System.Double"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.DBNull"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append("")
                        excelDoc.AppendLine("</Data></Cell>")

                    Case Else

                        Throw New Exception((rowType.ToString & " not handled."))

                End Select

                y = y + 1

            Loop

            excelDoc.AppendLine("</Row>")

        Loop

        ' Close out XML and flush
        excelDoc.AppendLine("</Table>")
        excelDoc.AppendLine("</Worksheet>")
        excelDoc.Append(endExcelXML)
        Return excelDoc.ToString

    End Function
    Private Function WriteExcelXml(ByRef source As DataTableReader) As String

        Dim excelDoc As New StringBuilder

        ' Send output to response stream
        'excelDoc = New System.IO.StreamWriter(resp.OutputStream)

        Dim startExcelXML As String = _
            "<?xml version='1.0' ?>" & vbCrLf & _
            "<Workbook " & _
            "xmlns='urn:schemas-microsoft-com:office:spreadsheet' " & vbCrLf & _
            "xmlns:o='urn:schemas-microsoft-com:office:office' " & vbCrLf & _
            "xmlns:x='urn:schemas-microsoft-com:office:excel' " & vbCrLf & _
            "xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet'>" & vbCrLf & _
            "<Styles>" & vbCrLf & _
            "<Style ss:ID='Default' ss:Name='Normal'>" & vbCrLf & _
            "<Alignment ss:Vertical='Bottom'/>" & vbCrLf & _
            "<Borders/>" & vbCrLf & _
            "<Font/>" & vbCrLf & _
            "<Interior/>" & vbCrLf & _
            "<NumberFormat/>" & vbCrLf & _
            "<Protection/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID='BoldColumn'>" & vbCrLf & _
            "<Font x:Family='Swiss' ss:Bold='1'/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s21"">" & vbCrLf & _
            "<NumberFormat ss:Format=""m/d/yyyy;@""/>" & vbCrLf & _
            "</Style>" & vbCrLf & _
            "<Style ss:ID=""s26"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[$-419]d\ mmm\ yy;@""/></Style>" & vbCrLf & _
  "<Style ss:ID=""s28"">" & vbCrLf & _
   "<NumberFormat ss:Format=""[ENG][$-409]d\-mmm\-yy;@""/></Style>" & vbCrLf & _
            "</Styles>" & vbCrLf
        '"<Style ss:ID='StringLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Decimal'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0.0000'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='Integer'>" & vbCrLf & _
        '"<NumberFormat ss:Format='0'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"<Style ss:ID='DateLiteral'>" & vbCrLf & _
        '"<NumberFormat ss:Format='mm/dd/yyyy;@'/>" & vbCrLf & _
        '"</Style>" & vbCrLf & _
        '"</Styles>" & vbCrLf

        Dim endExcelXML As String = "</Workbook>"
        Dim rowCount As Integer = 0
        Dim sheetCount As Integer = 0
        Dim newPage As Boolean = True
        Dim maxRows As Integer = 64000
        Dim flushRows As Integer = 100
        Dim iploc As New IP.Bids.Localization.WebLocalization

        ' Write XML header for Excel (changing single to double quotes)
        excelDoc.Append(Replace(startExcelXML, "'", Chr(34)))

        '// Write dataset
        Do While source.Read()


            'For Each x As DataRow In source.Item source.Tables(0).Rows
            rowCount = rowCount + 1

            ' flush every 100 rows
            If ((rowCount Mod flushRows) = 0) Then
                'excelDoc.Flush()
                'If (Not resp.IsClientConnected) Then
                '    Throw New Exception("Client disconnected.")
                'End If
            End If

            'if the number of rows is > 64000 create a new page to continue output
            If (rowCount > maxRows) Then
                rowCount = 0
                newPage = True
            End If

            ' Start a new page?
            If (newPage) Then

                newPage = False

                ' Close out subsequent pages
                If (sheetCount > 0) Then

                    excelDoc.AppendLine("</Table>")
                    excelDoc.AppendLine(" </Worksheet>")

                End If

                ' Start a new worksheet
                sheetCount = sheetCount + 1
                excelDoc.AppendLine("<Worksheet ss:Name=""Sheet" & sheetCount & """>")
                excelDoc.AppendLine("<Table>")
                excelDoc.AppendLine("<Row>")

                '// Write column headers
                Dim z As Integer = 0

                Do While (z < source.FieldCount)
                    excelDoc.Append("<Cell ss:StyleID=""BoldColumn""><Data ss:Type=""String"">")
                    excelDoc.Append(iploc.GetResourceValue(source.GetName(z))) 'Tables(0).Columns(z).ColumnName)
                    excelDoc.AppendLine("</Data></Cell>")
                    z = z + 1
                Loop

                excelDoc.AppendLine("</Row>")

            End If

            ' Write out a row of data
            excelDoc.AppendLine("<Row>")

            Dim y As Integer = 0
            Dim sb As New System.Text.StringBuilder
            Dim xxx As New XmlTextWriter(New System.IO.StringWriter(sb))

            Do While (y < source.FieldCount)

                Dim rowType As System.Type

                rowType = source.Item(y).GetType()

                Select Case (rowType.ToString)

                    Case "System.String"
                        If Not RI.SharedFunctions.isDateValue(source.Item(y).ToString) Or source.GetName(y) = "Functional Location" Or source.GetName(y) = "Line" Or source.GetName(y) = "Downtime" Then
                            Dim XMLstring As String = source.Item(y).ToString
                            XMLstring = XMLstring.Trim
                            XMLstring = XMLstring.Replace("&", "&")
                            XMLstring = XMLstring.Replace(">", ">")
                            XMLstring = XMLstring.Replace("<", "<")


                            sb = New System.Text.StringBuilder
                            xxx = New XmlTextWriter(New System.IO.StringWriter(sb))
                            xxx.WriteString(XMLstring)
                            xxx.Flush()
                            XMLstring = sb.ToString()


                            excelDoc.Append("<Cell>" & _
                                "<Data ss:Type=""String"">")
                            excelDoc.Append(XMLstring)
                            excelDoc.AppendLine("</Data></Cell>")
                        Else
                            Dim XMLDate As Date = CType(source.Item(y), Date)
                            Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                            Dim yr As String = XMLDate.Year
                            Dim mon As String = XMLDate.Month
                            Dim dy As String = XMLDate.Day
                            If mon.Length <> 2 Then mon = "0" & mon
                            If dy.Length <> 2 Then dy = "0" & dy
                            Dim dt As String = String.Format(dtformat, yr, mon, dy)

                            Dim styleKey As String = "s21"
                            If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "EN-US" Then
                                styleKey = "s28"
                            ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "RU-RU" Then
                                styleKey = "s26"
                            End If
                            excelDoc.Append("<Cell ss:StyleID=""" & styleKey & """>" & _
                                "<Data ss:Type=""DateTime"">")
                            excelDoc.Append(dt)
                            excelDoc.AppendLine("</Data></Cell>")
                        End If

                    Case "System.DateTime"
                        Dim XMLDate As Date = CType(source.Item(y), Date)
                        Dim dtformat As String = "{0}-{1}-{2}T00:00:00.000"
                        Dim yr As String = XMLDate.Year
                        Dim mon As String = XMLDate.Month
                        Dim dy As String = XMLDate.Day
                        If mon.Length <> 2 Then mon = "0" & mon
                        If dy.Length <> 2 Then dy = "0" & dy
                        Dim dt As String = String.Format(dtformat, yr, mon, dy)

                        Dim styleKey As String = "s21"
                        If System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "EN-US" Then
                            styleKey = "s28"
                        ElseIf System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToUpper = "RU-RU" Then
                            styleKey = "s26"
                        End If
                        excelDoc.Append("<Cell ss:StyleID=""" & styleKey & """>" & _
                            "<Data ss:Type=""DateTime"">")
                        excelDoc.Append(dt)
                        excelDoc.AppendLine("</Data></Cell>")

                        'excelDoc.Append("<Cell ss:StyleID=""s21"">" & _
                        '    "<Data ss:Type=""DateTime"">")
                        'excelDoc.Append(dt)
                        'excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Boolean"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Int16", "System.Int32", "System.Int64", "System.Byte"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.Decimal", "System.Double"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""Number"">")
                        excelDoc.Append(source.Item(y).ToString)
                        excelDoc.AppendLine("</Data></Cell>")

                    Case "System.DBNull"
                        excelDoc.Append("<Cell>" & _
                            "<Data ss:Type=""String"">")
                        excelDoc.Append("")
                        excelDoc.AppendLine("</Data></Cell>")

                    Case Else

                        Throw New Exception((rowType.ToString & " not handled."))

                End Select

                y = y + 1

            Loop

            excelDoc.AppendLine("</Row>")

        Loop

        ' Close out XML and flush
        excelDoc.AppendLine("</Table>")
        excelDoc.AppendLine("</Worksheet>")
        excelDoc.Append(endExcelXML)
        Return excelDoc.ToString

    End Function
    Protected Sub _btnDisplayExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnDisplayExcel.Click
        RaiseEvent DisplayExcel_Click()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me._btnDisplayExcel.Visible = ShowButton
        Me._btnDisplayExcel.Text = Me.ButtonText
        
    End Sub
End Class
