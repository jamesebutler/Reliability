Imports System.data
Imports System.io
Imports Devart.Data.Oracle
Partial Class RI_User_Controls_Common_ucOutageGanttChart
    Inherits System.Web.UI.UserControl
    Private mGanttChartData As String = String.Empty
    Public ReadOnly Property GanttChartData() As String
        Get
            Return mGanttChartData
        End Get

    End Property
    Public Sub GetGanttChart()
        Try

            Dim clsSearch As clsOutageViewSearch
            Dim CallSource As String = String.Empty
            Dim paramCollection As New OracleParameterCollection
            Dim param As New OracleParameter
            Dim drGanttChart As DataTableReader = Nothing
            Dim drGroup As DataTableReader = Nothing
            Dim ds As DataSet = Nothing
            Dim chartStartDate As Date
            Dim chartEndDate As Date
            clsSearch = Session.Item("clsOutageSearch")
            If clsSearch Is Nothing Then Exit Sub

            Try
                param = New OracleParameter
                param.ParameterName = "in_siteid"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.Facility
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Division"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.Division
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_BusinessUnit"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.BusinessUnit
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Area"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.Area
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Line"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.Line
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Title"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.Title
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_StartDate"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.StartDate
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_EndDate"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.EndDate
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_SDCategory"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.SDCategory
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_OutageCoord"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.OutageCoord
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_Annual"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.AnnualFlag
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_OrderBy"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.OrderBy
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "in_AndOr"
                param.OracleDbType = OracleDbType.VarChar
                param.Direction = Data.ParameterDirection.Input
                param.Value = clsSearch.AndOr
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "RSBlock"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                param = New OracleParameter
                param.ParameterName = "RSGroup"
                param.OracleDbType = OracleDbType.Cursor
                param.Direction = Data.ParameterDirection.Output
                paramCollection.Add(param)

                Dim key As String = "GanttOutageView11" '& Facility & "_" & Division & "_" & BusinessUnit & "_" & Area & "_" & Line & "_" & Title & "_" & OutageCoord & "_" & SDCategory & "_" & StartDate & "_" & EndDate & "_" & OrderBy
                ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.Outage.OutageGanttListing", key, 0)
                drGanttChart = ds.Tables(0).CreateDataReader
                drGroup = ds.Tables(1).CreateDataReader
                'dr = RI.SharedFunctions.GetOraclePackageDR(paramCollection, "Reladmin.Outage.OutageGanttListing")
                If drGanttChart IsNot Nothing Then
                    'Dim sbConfiguration As String
                    'Dim sbData As New StringBuilder
                    'Dim sbCategories As New StringBuilder
                    'Dim categoryid As Integer = 0
                    'Dim sbColumn1 As New StringBuilder
                    'Dim sbColumn2 As New StringBuilder
                    'Dim sbColumn3 As New StringBuilder
                    'Dim sbColumn4 As New StringBuilder
                    'Dim Group1 As New StringBuilder
                    'Dim Group2 As New StringBuilder
                    'Dim Group3 As New StringBuilder
                    'Dim Group4 As New StringBuilder
                    Dim printFriendlyHeader As New StringBuilder


                    printFriendlyHeader.Append("<div class='printOnly'><table width='100%' border=0><tr><th colspan=6 align='center'><font size='16px;'>Outage Plans</font><br>Printed on:{0}</th></tr>")
                    printFriendlyHeader.Append("<tr><td>Division:{1}</td><td>Facility:{2}</td><td>Business Unit:{3}</td><td>Area:{4}</td><td>Line:{5}</td><td>Start Date:{6}</td><td>End Date:{7}</td></tr>")
                    printFriendlyHeader.Append("<tr><td colspan='3'>Title/Desc:{8}</td><td colspan='3'>Outage Coordinator:{9}</td></tr>")
                    printFriendlyHeader.Append("</table></div>")
                    Dim printFriendlyHeadertbl As String = String.Format(printFriendlyHeader.ToString, FormatDateTime(Now, DateFormat.GeneralDate), RI.SharedFunctions.DataClean(clsSearch.Division, "All"), RI.SharedFunctions.DataClean(clsSearch.Facility, "All"), RI.SharedFunctions.DataClean(clsSearch.BusinessUnit, "All"), RI.SharedFunctions.DataClean(clsSearch.Area, "All"), RI.SharedFunctions.DataClean(clsSearch.Line, "All"), clsSearch.StartDate, clsSearch.EndDate, clsSearch.Title, clsSearch.OutageCoord)

                    If drGroup IsNot Nothing Then
                        drGroup.Read()
                        If drGroup.HasRows Then
                            chartStartDate = CType(RI.SharedFunctions.DataClean(drGroup.Item("MinStartDate"), clsSearch.StartDate), Date).AddMonths(-1)
                            chartEndDate = CType(RI.SharedFunctions.DataClean(drGroup.Item("MaxEndDate"), clsSearch.EndDate), Date).AddMonths(1)
                            drGroup.Close()
                            drGroup = Nothing
                        End If
                    End If
                    Dim months As Collections.ArrayList = GetMonths(chartStartDate, chartEndDate)
                    'Dim months As Collections.ArrayList = GetWeeksOf(clsSearch.StartDate, clsSearch.EndDate)
                    Dim sbMonthTR As New StringBuilder
                    'sbMonthTR.Append("<tr>")

                    For i As Integer = 0 To months.Count - 1
                        'sbMonthTR.Append("<th width='50px'><font size=1>" & i & "</font></th>")
                        sbMonthTR.Append("<th width='50px'>" & months(i) & "</th>")
                    Next

                    'For i As Integer = 0 To months.Count - 1
                    '    Dim weeks As New StringBuilder
                    '    For j As Integer = 0 To 4
                    '        weeks.Append("<td>" & weekOf((5 * i) + j) & "</td>")
                    '    Next

                    '    sbMonthTR.Append("<th width='50px'><table border=1 cellpadding=2 cellspacing=0 bordercolor='#000000' style='border-collapse: collapse'><tr><th colspan='5'>" & months(i) & "</th></tr><tr>" & weeks.ToString & "</tr></table></th>")
                    'Next

                    'sbMonthTR.Append("</tr>")  

                    Dim Start_Date As DateTime = FormatDateTime(chartStartDate, DateFormat.ShortDate)
                    Dim end_Date As DateTime = FormatDateTime(chartEndDate, DateFormat.ShortDate)
                    'Dim TimeLine As String = "<timeline start_date='{0}' end_date='{1}' start_time='00:00' end_time='00:00'><years level='0' grouping_value='1'/><months level='0' grouping_value='1' caption_type='abbreviature' show_grid='yes' grid_z_index='0' grid_style='grid'/></timeline>"
                    'Dim items As String = "<item id='{0}' start_date='{1}' start_time='00:00' end_date='{2}' end_time='00:00' category_id='{0}' style='item' progress_value='50'><progress type='background' style='background_progress'/><progress type='text' value='{3}' style='text_progress_1'/><progress type='text' value='{4}' style='text_progress_2'/></item>"
                    'Dim category As String = "<category id='{0}' position='{0}' caption='{1}' style='categories_column_line'/>"
                    'Dim sbItems As New StringBuilder
                    'sbItems.Append("<items>")

                    'TimeLine = String.Format(TimeLine, Start_Date.Year & "." & Start_Date.Month & "." & Start_Date.Day, end_Date.Year & "." & end_Date.Month & "." & end_Date.Day)
                    'sbColumn1.Append("<column align='left' title='Duration' position='1' title_style='column_title' style='data_column'><groups>")
                    'sbColumn2.Append("<column align='left' title='Start' position='2' title_style='column_title' style='data_column'><groups>")
                    'sbColumn3.Append("<column align='left' title='Finish' position='3' title_style='column_title' style='data_column'><groups>")
                    'sbColumn4.Append("<column align='left' title='Mill' position='4' title_style='column_title' style='data_column'><groups>")


                    'sbConfiguration = "<?xml version='1.0'?><root><configuration><chart style='chart' items_area_style='chart_items_area'/><styles><style id='chart_items_area'><background enabled='no'/><border enabled='yes' size='0.7' color='Black'/></style><style id='categories_column'><size width='100'/></style><style id='categories_column_line'><size height='40'/><font vertical_align='center'/><border enabled='yes'/></style><style id='data_column'><size width='80'/><font vertical_align='center'/><border enabled='yes'/></style><style id='column_title'><size height='36'/><font align='center' vertical_align='top'/><border enabled='yes'/></style><style id='grid'><line size='0.5' color='Gray'/></style><style id='item'><size vertical_space='30'/><background color='Brown'/></style><style id='background_progress'><background color='Green'/><border enabled='no'/></style><style id='text_progress_1'><position left='-20' top='-4'/><font size='10' type='Verdana'/></style><style id='text_progress_2'><position right='0' placement='right' top='-4'/><font size='10' type='Verdana'/></style></styles></configuration>"
                    'sbData.Append("<data><title text='Outage'/>")
                    'sbCategories.Append("<categories show_grid='yes' scroll_value='10' show_scroller='yes' grid_z_index='1' align='left' position='0' style='categories_column' title_style='column_title' title='Task Name'>")

                    Dim sbTable As New StringBuilder
                    Dim trHeader As String = "<thead><tr class='Border'><th class='sort' width='90px'>Start Date</th><th class='sort' width='90px'>End Date</th><th class='sort' width='80px'>Facility</th><th class='sort' width='175px'>Title</th><th class='sort' width='75px'>Duration</th>{0}</tr></thead><tbody>"
                    trHeader = String.Format(trHeader, sbMonthTR.ToString)
                    Dim trRow As String = "<tr class='Border'><td>{1}</td><td>{2}</td><td>{3}</td><td nowrap><a href='{5}' target=_top>{0}</a></td><td>{4}</td>{6}</tr>"
                    sbTable.Append(printFriendlyHeadertbl)
                    sbTable.Append("<div id='tbl-container'><table id='tbl' width='100%' border='1' cellpadding=2 cellspacing=0 bordercolor='#000000' style='border-collapse: collapse;table-layout: fixed;'>")
                    sbTable.Append(trHeader)
                    Dim imgBar As String = Page.ResolveClientUrl("~/images/blackbar.gif")
                    Dim url As String = Page.ResolveClientUrl("~/Outage/EnterOutage.aspx?OutageNumber=")
                    Do While drGanttChart.Read()
                        Dim itemStart_Date As DateTime = FormatDateTime(drGanttChart.Item("StartDate"), DateFormat.ShortDate)
                        Dim itemend_Date As DateTime = FormatDateTime(drGanttChart.Item("EndDate"), DateFormat.ShortDate)

                        sbTable.Append(String.Format(trRow, drGanttChart.Item("OutageTitle"), FormatDateTime(drGanttChart.Item("StartDate"), DateFormat.ShortDate), FormatDateTime(drGanttChart.Item("EndDate"), DateFormat.ShortDate), drGanttChart.Item("Sitename"), drGanttChart.Item("Duration"), url & drGanttChart.Item("OutageNumber"), Me.GetProgressBar(chartStartDate, chartEndDate, drGanttChart.Item("StartDate"), drGanttChart.Item("EndDate"), months.Count, drGanttChart.Item("SiteID"), drGanttChart.Item("OutageTitle")))) 'months.Count * 4, dr.Item("SiteID") & "--" & dr.Item("OutageTitle")))
                        'categoryid = categoryid + 1
                        'Let's create a table

                    Loop
                    sbTable.Append("</tbody></table><div>")

                    'sbCategories.Append("</categories>")
                    'sbColumn1.Append("</groups></column>")
                    'sbColumn2.Append("</groups></column>")
                    'sbColumn3.Append("</groups></column>")
                    'sbColumn4.Append("</groups></column>")
                    'sbItems.Append("</items>")
                    'Dim Columns As String = "<columns>" & sbColumn1.ToString & sbColumn2.ToString & sbColumn3.ToString & sbColumn4.ToString & "</columns>"

                    'Dim strXML As String = sbConfiguration.ToString & sbData.ToString & sbCategories.ToString & Columns & TimeLine & sbItems.ToString & "</data></root>"

                    'Dim tbl As String = sbTable.ToString
                    'Dim x As String = ""
                    'Dim XML As New FileInfo(Server.MapPath("columns.xml"))
                    mGanttChartData = sbTable.ToString
                    _divGanttChart.InnerHtml = sbTable.ToString
                End If
            Catch ex As Exception
                Throw
            Finally
                If drGanttChart IsNot Nothing Then
                    drGanttChart.Close()
                    drGanttChart = Nothing
                End If
            End Try

            ' Exports a dataset to an .xml file
            'ds.WriteXml(Server.MapPath("amy.xml"), XmlWriteMode.WriteSchema)

            'Data.WriteXml(Stream, XmlWriteMode.IgnoreSchema)
            'Stream.Close()

            ''If HttpRuntime.Cache.Item("GanttOutageViewCT") IsNot Nothing Then
            'If ds IsNot Nothing Then
            '    'ds = CType(HttpRuntime.Cache.Item("GanttOutageViewCT"), DataSet)
            '    Dim myGroupAdapter As SqlDataAdapter
            '    Dim myBlockAdapter As SqlDataAdapter
            '    ds.Relations.Clear()
            '    Dim primaryKey As DataColumn = ds.Tables(0).Columns("groupnum")
            '    Dim ForeignKey As DataColumn = ds.Tables(1).Columns("groupnum")
            '    ' primaryKey.Table.Namespace = "Group"
            '    'ForeignKey.Table.Namespace = "Block"
            '    ds.Tables(0).TableName = "group"
            '    ds.Tables(1).TableName = "block"
            '    Dim relation As New DataRelation("group", primaryKey, ForeignKey)
            '    relation.Nested = True
            '    ds.Relations.Add(relation)

            '    xmlData = ds.GetXml()
            '    Response.Write(xmlData)
            'End If

            ''xmlData = clsSearch.Gantt.ToString
            'EventCalendarControl1.XMLData = xmlData
            'EventCalendarControl1.BlankGifPath = "trans.gif"
            'EventCalendarControl1.Year = 2007
            'EventCalendarControl1.Quarter = 3
            'EventCalendarControl1.BlockColor = "blue"
            'EventCalendarControl1.ToggleColor = "#dcdcdc"
            'EventCalendarControl1.CellHeight = 15
            'EventCalendarControl1.CellWidth = 15

        Catch ex As Exception
            Throw
        End Try
        'Dim url As String = Page.ResolveClientUrl("~/outage/ganttchart.swf") & "?XMLFile=" & Page.ResolveClientUrl("~/Outage/columns.xml")
    End Sub
    Private Function GetTimeLinePosition(ByVal dt As DateTime) As Integer
        Dim mthYear As String = Month(dt) & "/" & Year(dt)
        Dim pos As Integer = 0
        For i As Integer = 0 To timeline.Count - 1
            If mthYear = timeline(i) Then
                pos = i
                Exit For
            End If
            If i = timeline.Count - 1 Then
                pos = i '+ 1
                Exit For
            End If
        Next
        Return pos
    End Function
    Private Function GetTimeLinePosition(ByVal dt As DateTime, ByVal checkWeekOf As Boolean) As Integer
        If checkWeekOf = False Then
            Return GetTimeLinePosition(dt)
        End If
        Dim newDt As DateTime = DateAdd("d", 1 - Weekday(dt), dt)
        Dim weekData As String = MonthName(Month(newDt), True) & "." & Day(newDt) & "." & Year(newDt)

        Dim pos As Integer = 0
        For i As Integer = 0 To weekOf.Count
            If weekData = weekOf(i) Then
                pos = i
                Exit For
            End If
        Next
        Return pos
    End Function
    Public Function GetProgressBar(ByVal startDate As Date, ByVal endDate As Date, ByVal taskStartDate As Date, ByVal taskEndDate As Date, ByVal containerColumns As Integer, ByVal SiteID As String, ByVal TaskDesc As String) As String
        Static count As Integer
        count = count + 1

        If IsDate(startDate) And IsDate(endDate) And IsDate(taskStartDate) And IsDate(taskEndDate) Then
            Dim ProgressTable As String = String.Empty '"<table border='2' cellspacing='0' width='100%' cellpadding='0' height='10px' bgColor='#B5CCFF'><tr>{0}</tr></table>"
            'Dim TimeLineStart As Integer = Month(startDate) '6 - Jun
            'Dim TimeLineEnd As Integer = Month(endDate) '12 - Dec
            Dim EventStart As Integer = (GetTimeLinePosition(taskStartDate)) * 5 + Math.Floor(Day(taskStartDate) / 7)
            Dim EventEnd As Integer = (GetTimeLinePosition(taskEndDate)) * 5 + Math.Floor(Day(taskEndDate) / 7)

            Dim sb As New StringBuilder
            Dim currentCellCount As Integer = 0

            Dim BlocksBeforeBar As Integer = EventStart  '+ 1
            Dim Blocks As Integer = EventEnd - EventStart
            'Dim BlocksAfterBar As Integer = containerColumns - BlocksBeforeBar - Blocks
            Dim BlackBar As String = urlHost & "images/blackbar.gif"
            Dim SpacerBar As String = urlHost & "images/blankbar.gif"

            'Dim blackBarProg As New StringBuilder
            'Dim whiteBarProg As New StringBuilder

            'Dim Bar As String = ("<span style='width:100%;height:10px;'><span style='width:{0}px'>&nbsp;</span><span style='width:2px;margin:5px;font-size:8pt;'>{1}</span><span style='background-color:#000000;width:{2}px;height:10px'>&nbsp;</span><br/><span style='margin:5px;font-size:8pt;'>{3}</span></span>")
            'Dim Bar As String = ("<table width='100%' border=2 height='10px'><tr><td width='{0}px'>&nbsp;</td><td width='2px'>{1}</td><td bgcolor='#000000' width='{2}px'>&nbsp;</td><td nowrap>{3}</td></tr></table>")
            'blackBarProg.Append("<table width='{0}' height='10px'><tr bgcolor='#000000'><td>&nbsp;</td></tr></table>")

            'Dim whiteBar As String = String.Format(whiteBarProg.ToString, BlocksBeforeBar * 20)
            'Dim blackBar As String = String.Format(blackBarProg.ToString, Blocks * 20)
            'blackBarProg.Append("n", Blocks)
            'whiteBarProg.Append("n", BlocksBeforeBar)

            If Blocks = 0 Then Blocks = 1 'And FormatDateTime(taskStartDate, DateFormat.ShortDate) <> FormatDateTime(taskEndDate, DateFormat.ShortDate) Then Blocks = 1
            Dim ProgBar As String = "{0}<span style='font-size:8pt;'>{1}</span>{2}<span style='font-size:8pt;overflow:hidden'>{3}</span>"
            'Dim ProgBar As String = "<span>{0}</span><span style='margin:5px;font-size:8pt;'>{1}</span><span>{2}<span style='margin:5px;font-size:8pt;'>{3}</span>"
            'TaskDesc = String.Empty
            ProgBar = String.Format(ProgBar, GetBar(SpacerBar, BlocksBeforeBar), SiteID, GetBar(SpacerBar, 1) & GetBar(BlackBar, Blocks) & GetBar(SpacerBar, 1), TaskDesc)

            sb.Append("<td nowrap colspan='" & containerColumns & "'>" & ProgBar & "</td>")

            Return sb.ToString
        Else
            Return ""
        End If
    End Function
    Private Function GetBar(ByVal img As String, ByVal count As Integer) As String
        Dim imgFile As String = String.Format("<img src='{0}' border='0' style='width:10px;height:10px;'/>", img)
        Dim imgSB As New StringBuilder
        For i As Integer = 1 To count
            imgSB.Append(imgFile)
        Next
        Return imgSB.ToString
    End Function
    Private urlHost As String = String.Empty
    Private timeline As New Collections.ArrayList
    Private weekOf As New Collections.ArrayList
    'ByVal StartDate As DateTime, ByVal EndDate As DateTime
    Private Sub SetWeeksForAMonth(ByVal dt As DateTime)
        Dim firstDayOfWeek As Integer = Day(DateAdd("d", 1 - Weekday(Now), Now))
        Dim weeksInMonth As Integer = 5
        Dim daysInMonth As Integer = Date.DaysInMonth(Year(dt), Month(dt))

        If daysInMonth = 28 And firstDayOfWeek = 1 Then

        End If
        For i As Integer = 1 To weeksInMonth
            firstDayOfWeek = Day(DateAdd("d", 1 - Weekday(dt), dt))
            weekOf.Add(firstDayOfWeek)
            dt = DateAdd("w", 7, dt)
        Next

    End Sub
    Private Function GetWeeksOf(ByVal StartDate As DateTime, ByVal EndDate As DateTime) As Collections.ArrayList
        Dim newDt As DateTime = DateAdd("d", 1 - Weekday(StartDate), StartDate)
        Dim fDayOfWeek As Integer = Day(DateAdd("d", 1 - Weekday(StartDate), StartDate))
        Dim weekData As String = MonthName(Month(newDt), True) & "." & fDayOfWeek.ToString & "." & Year(newDt)
        Dim dt As DateTime = StartDate
        Dim weeksOfArrayList As New Collections.ArrayList

        weeksOfArrayList.Add(weekData)

        Do While dt < EndDate
            dt = dt.AddDays(7)
            newDt = DateAdd("d", 1 - Weekday(dt), dt)
            fDayOfWeek = Day(newDt)
            weekData = MonthName(Month(newDt), True) & "." & fDayOfWeek.ToString & "." & Year(newDt)
            weeksOfArrayList.Add(weekData)
        Loop
        weekOf = weeksOfArrayList
        Return weeksOfArrayList
    End Function
    Public Function GetMonths(ByVal StartDate As DateTime, ByVal EndDate As DateTime) As System.Collections.ArrayList

        Dim startMonth As Integer = 0
        Dim startYear As Integer = 0
        Dim endMonth As Integer = 0
        Dim endYear As Integer = 0

        startMonth = StartDate.Month
        startYear = StartDate.Year

        endMonth = EndDate.Month
        endYear = EndDate.Year


        Dim asciiCode As Integer = 65

        'Dim monthArrayList As New System.Collections.ArrayList()
        'Dim yearArrayList As New System.Collections.ArrayList()
        Dim monthYearArrayList As New Collections.ArrayList  'New System.Collections.ArrayList()
        monthYearArrayList.Add("&nbsp;")
        timeline.Add("&nbsp;")
        Do
            'monthArrayList.Add(startMonth)
            'yearArrayList.Add(startYear)
            'SetWeeksForAMonth(New Date(startYear, startMonth, 1))
            timeline.Add(startMonth & "/" & startYear)
            monthYearArrayList.Add(MonthName(startMonth, True) & " " & startYear)
            If startMonth = 12 Then
                startMonth = 1
                startYear += 1
            Else
                startMonth += 1
            End If
        Loop While Not (startMonth = endMonth AndAlso startYear = endYear)
        ' Last Value 
        'monthArrayList.Add(startMonth)
        'yearArrayList.Add(startYear)
        monthYearArrayList.Add(MonthName(startMonth, True) & " " & startYear)
        monthYearArrayList.Add("&nbsp;")

        timeline.Add(startMonth & "/" & startYear)
        timeline.Add("&nbsp;")

        'SetWeeksForAMonth(New Date(startYear, startMonth, 1))
        Return monthYearArrayList
    End Function

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.ServerVariables("HTTP_HOST").ToLower.Contains("ridev") Then
            urlHost = "Http://ridev/ri/"
        ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritest") Then
            urlHost = "Http://ritest/ri/"
        ElseIf Request.ServerVariables("HTTP_HOST").ToLower.Contains("ritrain") Then
            urlHost = "Http://ritrain/ri/"
        Else
            urlHost = "Http://ridev/ri/"
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load              
       
    End Sub
    Public Sub Export()
        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", "OutageGantt.doc"))
        HttpContext.Current.Response.ContentType = "application/word"
        Dim sw As StringWriter = New StringWriter
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        '  Create a form to contain the grid
        'Dim table As Table = New Table
        'table.GridLines = gv.GridLines
        ''  add the header row to the table
        'If (Not (gv.HeaderRow) Is Nothing) Then
        '    GridViewExportUtil.PrepareControlForExport(gv.HeaderRow)
        '    table.Rows.Add(gv.HeaderRow)
        'End If
        ''  add each of the data rows to the table
        'For Each row As GridViewRow In gv.Rows
        '    GridViewExportUtil.PrepareControlForExport(row)
        '    table.Rows.Add(row)
        'Next
        ''  add the footer row to the table
        'If (Not (gv.FooterRow) Is Nothing) Then
        '    GridViewExportUtil.PrepareControlForExport(gv.FooterRow)
        '    table.Rows.Add(gv.FooterRow)
        'End If
        '  render the table into the htmlwriter
        Me._divGanttChart.RenderControl(htw)
        'table.RenderControl(htw)
        '  render the htmlwriter into the response
        HttpContext.Current.Response.Write(sw.ToString)
        HttpContext.Current.Response.End()
    End Sub

    Protected Sub _btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _btnExport.Click, _btnExportTop.Click
        Export()
    End Sub
End Class
