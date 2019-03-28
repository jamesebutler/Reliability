Imports System.Web
Imports System.Web.Services
Imports AjaxControlToolkit
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data
Imports Devart.Data.Oracle

<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class FunctionalLocationLookup
    Inherits System.Web.Services.WebService
    '    Private Shared autoCompleteWordList_FunctionalList As String()
    '    <WebMethod()> _
    '    Public Shared Function GetFunctional(ByVal equipid As String, ByVal listCount As Integer) As String()
    '        If listCount = 0 Then listCount = 10
    '        Dim items As New List(Of String)
    '        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
    '        Dim sql As String = "Select rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid   and rcfaflid<>' ' and  EquipmentId like'{0}%'   order by b.sitename,equipmentid"
    '        sql = String.Format(sql, equipid)
    '        'Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
    '        'If Cache.Item(key) IsNot Nothing Then
    '        'ds = CType(Cache.Item(key), DataSet)
    '        'Else
    '        dr = RI.SharedFunctions.GetOracleDataReader(sql)
    '        If dr IsNot Nothing Then
    '            Do While dr.Read
    '                items.Add(dr.Item("equipmentid") & "  *  " & dr.Item("equipmentdesc"))
    '            Loop
    '        End If

    '        Return items.ToArray
    '    End Function
    '    <WebMethod()> _
    '<Script.Services.ScriptMethod()> _
    '    Public Shared Function GetFunctionalList(ByVal equipID As String, ByVal listCount As Integer) As String()
    '        If listCount = 0 Then listCount = 10
    '        Dim items As New List(Of String)
    '        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
    '        Dim sql As String = "Select rcfaflid,a.siteid,b.sitename,equipmentid,equipmentdesc FROM RefEQUIPMENT a, refsite b WHERE a.siteid = b.siteid  and rcfaflid<>' ' and  EquipmentId like'{0}%'   order by b.sitename,equipmentid"
    '        sql = String.Format(sql, equipID)
    '        'Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)
    '        'If Cache.Item(key) IsNot Nothing Then
    '        'ds = CType(Cache.Item(key), DataSet)
    '        'Else
    '        dr = RI.SharedFunctions.GetOracleDataReader(sql)
    '        If dr IsNot Nothing Then
    '            Do While dr.Read
    '                items.Add(dr.Item("equipmentid") & "  *  " & dr.Item("equipmentdesc"))
    '            Loop
    '        End If

    '        Return items.ToArray()
    '    End Function
    <WebMethod()> _
<Script.Services.ScriptMethod()> _
    Public Function PopulateFunctionalLocation(ByVal SiteId As String, ByVal BusArea As String, ByVal LineBreak As String) As String
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Dim items As String = String.Empty
        Dim busAreaWhere As String = String.Empty
        Dim lineBreakWhere As String = String.Empty
        Try
            Dim sql As String = "Select flid,equipmentdesc, criticality FROM RefSiteArea, refequipment where refsitearea.flid = refequipment.equipmentid and refsitearea.siteid = refequipment.siteid and refsitearea.siteid='{0}' {1} {2}  and FLID is not null order by flid"
            Dim sql2 As String = "Select distinct max(flid) flid FROM RefSiteArea where refsitearea.siteid='{0}' {1} {2}  and FLID is not null"
            If BusArea.Length > 0 Then
                busAreaWhere = String.Format(" and refsitearea.RISuperArea|| ' - ' ||refsitearea.Subarea='{0}'", BusArea)
            End If
            If LineBreak.Length > 0 Then
                If InStr(LineBreak, "-") = 0 Then LineBreak = LineBreak & " - None"
                lineBreakWhere = String.Format(" and refsitearea.area|| ' - ' ||refsitearea.linebreak='{0}'", LineBreak)
            End If
            sql = String.Format(sql, SiteId, busAreaWhere, lineBreakWhere)
            sql2 = String.Format(sql2, SiteId, busAreaWhere, lineBreakWhere)
            Dim key As String = "RI_FUNCTIONAL_LOCATION_" & RI.SharedFunctions.CreateKey(sql)

            ds = RI.SharedFunctions.GetOracleDataSet(sql)

            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    dr.Read()
                    If dr.HasRows Then
                        items = RI.SharedFunctions.DataClean(dr.Item("flid")) & " * " & RI.SharedFunctions.DataClean(dr.Item("equipmentdesc")) & " * " & RI.SharedFunctions.DataClean(dr.Item("criticality"))
                    Else
                        If lineBreakWhere.Length > 0 Then
                            ds = RI.SharedFunctions.GetOracleDataSet(sql2)
                            If ds IsNot Nothing Then
                                dr = ds.Tables(0).CreateDataReader
                                If dr IsNot Nothing Then
                                    dr.Read()
                                    If dr.HasRows Then
                                        items = RI.SharedFunctions.DataClean(dr.Item("flid")) & " * "  '& " * " & RI.SharedFunctions.DataClean(dr.Item("criticality"))
                                    End If
                                End If
                            End If
                        End If
                        End If
                End If
            End If
            Return items
        Catch ex As Exception
            Throw New ApplicationException("Exception Occured")
        Finally

            If ds IsNot Nothing Then ds = Nothing
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Function

    <WebMethod()> _
 <Script.Services.ScriptMethod()> _
  Public Function GetNotificationType(ByVal facility As String, ByVal busArea As String, ByVal lineBreak As String, ByVal userName As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ActiveFlag As String = String.Empty
        Dim businessUnit As String = String.Empty
        Dim Area As String = String.Empty
        Dim Line As String = String.Empty
        Dim businessUnitArea As String()
        Dim LineLineBreak As String()
        Dim NotificationType As String = String.Empty
        'Check input paramaters
        'Dim userProfile As RI.CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String = String.Empty
        Try
            If busArea.Length > 1 Then
                businessUnitArea = busArea.Split(CChar("-"))
                If businessUnitArea.Length = 2 Then
                    businessUnit = businessUnitArea(0).Trim
                    Area = businessUnitArea(1).Trim
                ElseIf businessUnitArea.Length = 1 Then
                    businessUnit = businessUnitArea(0).Trim
                End If
            End If
            If lineBreak.Length > 1 Then
                LineLineBreak = lineBreak.Split(CChar("-"))
                If LineLineBreak.Length = 2 Then
                    Line = LineLineBreak(0).Trim
                ElseIf LineLineBreak.Length = 1 Then
                    Line = LineLineBreak(0).Trim
                End If
            End If


            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_BusUnit"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = businessUnit
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Area"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Area
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_Line"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = Line
            paramCollection.Add(param)


            param = New OracleParameter
            param.ParameterName = "rsNotificationType"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.GetNotificationType" & facility & "_" & busArea & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GetNotificationType", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'rsNotificationType
                    Dim dr As System.Data.DataTableReader = Nothing
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            NotificationType = RI.SharedFunctions.DataClean(dr.Item("NotifyType"))
                        Else
                            NotificationType = String.Empty
                        End If
                    End If
                End If
            End If
            Return NotificationType
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                'ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod()> _
 <Script.Services.ScriptMethod()> _
  Public Function GetDeleteAccess(ByVal facility As String, ByVal userName As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim ret As String = String.Empty
        'Check input paramaters
        'Dim userProfile As RI.CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String = String.Empty
        Try



            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsDeleteAccess"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.GetDeleteAccess" & facility & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.GetDeleteAccess", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'rsNotificationType
                    Dim dr As System.Data.DataTableReader = Nothing
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            ret = RI.SharedFunctions.DataClean(dr.Item("AuthLevelID"))
                        Else
                            ret = String.Empty
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            Server.ClearError()
        Finally
            GetDeleteAccess = ret
            If ds IsNot Nothing Then
                'ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod()> _
 <Script.Services.ScriptMethod()> _
  Public Function GetAuthLevel(ByVal facility As String, ByVal userName As String) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Dim AuthLevel As String = String.Empty
        Dim AuthLevelID As String = String.Empty
        'Check input paramaters
        'Dim userProfile As RI.CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String = String.Empty
        Try

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = facility
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_username"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userName
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsAuthLevel"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.AuthLevel" & facility & "_" & userName
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.AuthLevel", key, 24)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    'rsNotificationType
                    Dim dr As System.Data.DataTableReader = Nothing
                    dr = ds.Tables(0).CreateDataReader
                    If dr IsNot Nothing Then
                        dr.Read()
                        If dr.HasRows Then
                            AuthLevel = RI.SharedFunctions.DataClean(dr.Item("AuthLevel"))
                            AuthLevelID = RI.SharedFunctions.DataClean(dr.Item("AuthLevelid"))
                        End If
                    End If
                End If
            End If
            Dim ret As String = String.Empty
            If AuthLevel.Length > 0 And AuthLevelID.Length > 0 Then ret = AuthLevel & "-" & AuthLevelID
            Return ret
        Catch ex As Exception
            Throw
        Finally
            If ds IsNot Nothing Then
                'ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod()> _
<Script.Services.ScriptMethod()> _
    Public Function RefreshTreeView(ByVal siteid As String, ByVal facility As String) As String        
        Dim sql As String = String.Empty
        Dim sb As New StringBuilder


        sb.Append("<TABLE style=""BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px"" cellSpacing=0 cellPadding=0>")
        sb.Append("<TBODY><TR><TD><A id=ctl00__cphMain__functionalLocationTree__tvFunctionalLocationn0 href=""javascript:TreeView_PopulateNode(ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_Data,0,ctl00__cphMain__functionalLocationTree__tvFunctionalLocationn0,ctl00__cphMain__functionalLocationTree__tvFunctionalLocationt0,null,'-','{0}','{1}','f','','t')""><IMG style=""BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px"" alt=""Expand {0}"" src=""/RI/Images/Plus.gif""></A></TD>")
        sb.Append("<TD class=""TreeView ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_2"" onmouseover=""TreeView_HoverNode(ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_Data, this)"" style=""WHITE-SPACE: nowrap"" onmouseout=TreeView_UnhoverNode(this)><A class=""ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_0 TreeView ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_1"" id=ctl00__cphMain__functionalLocationTree__tvFunctionalLocationt0 style=""FONT-SIZE: 1em; BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none"" href=""javascript:TreeView_PopulateNode(ctl00__cphMain__functionalLocationTree__tvFunctionalLocation_Data,0,ctl00__cphMain__functionalLocationTree__tvFunctionalLocationn0,ctl00__cphMain__functionalLocationTree__tvFunctionalLocationt0,null,'-','{0}','{1}','f','','t')"">{0}</A></TD></TR>")
        sb.Append("<TR style=""HEIGHT: 2px""><TD></TD></TR></TBODY></TABLE>")
        Dim ret As String = String.Format(sb.ToString, facility, GetFLID(siteid))
        Return ret
    End Function

    Public Function GetFLID(ByVal siteid As String) As String
        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim sql As String
        Dim flid As String = String.Empty

        sql = "Select distinct siteid,sitename,rcfaflid, cmmstype FROM refsite a  where rcfaflid<>' ' and siteid='{0}'"
        sql = String.Format(sql, siteid)

        dr = RI.SharedFunctions.GetOracleDataReader(sql)

        If dr IsNot Nothing Then
            If dr.HasRows Then
                Dim parent As Integer = 0
                While dr.Read

                    If RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "SAPPM" Or RI.SharedFunctions.DataClean(dr.Item("cmmstype")) = String.Empty Then
                        flid = CStr(dr.Item("rcfaflid"))
                    ElseIf RI.SharedFunctions.DataClean(dr.Item("cmmstype")).ToUpper = "EXEGETE" Then
                        flid = "*" & CStr(dr.Item("SiteId"))
                    Else
                        flid = "$" & CStr(dr.Item("SiteId"))
                    End If
                End While
            End If
        End If
        Return flid
    End Function

    <WebMethod()> _
 <Script.Services.ScriptMethod()> _
    Public Function PerformEquipmentSearch(ByVal facility As String, ByVal busArea As String, ByVal linelinebreak As String, ByVal criticality As String, ByVal equipclass As String, ByVal equiptype As String, ByVal fl As String, ByVal desc As String, ByVal limit As Integer) As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        Dim sbTable As New StringBuilder
        Dim businessUnit As String = String.Empty
        Dim area As String = String.Empty
        Dim line As String = String.Empty
        Dim linebreak As String = String.Empty

        If busArea.Length > 0 Then
            If busArea.Split("-").Length > 1 Then
                Dim tmp() As String = busArea.Split("-")
                If tmp.Length > 1 Then
                    businessUnit = tmp(0).Trim
                    area = tmp(1).Trim
                End If
            End If
        End If
        If linelinebreak.Length > 0 Then
            If linelinebreak.Split("-").Length > 1 Then
                Dim tmp() As String = linelinebreak.Split("-")
                If tmp.Length > 1 Then
                    line = tmp(0).Trim
                    linebreak = tmp(1).Trim
                End If
            End If
        End If
        Try
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = facility.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_bustype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = businessUnit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_area"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = area
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_line"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = line
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_criticality"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = criticality.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_equipclass"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = equipclass.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_equiptype"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = equiptype.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_fl"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = fl.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_desc"
            param.OracleDbType = OracleDbType.NVarChar
            param.Value = desc.Trim
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_topN"
            param.OracleDbType = OracleDbType.Integer
            param.Value = limit
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rs"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "EquipmentSearchDDL"
            Dim rowCount As Integer = 0
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.equipment_dtl_mill", key, 0)

            If ds IsNot Nothing Then
                If ds.Tables.Count >= 1 Then
                    Dim dr As DataTableReader = ds.Tables(0).CreateDataReader
                    Dim altRow As Integer = 0
                    Dim trClass As String = "Border"
                    sbTable.Append("<Table width='98%' cellspacing=1 cellpadding=2><tr class='LockHeader'><th>Functional Location</th><th>Equipment Desc</th><th>Equipment Class</th><th>EquipmentType</th><th>Criticality</th></tr>")

                    Do While dr.Read
                        rowCount += 1
                        If altRow = 1 Then
                            altRow = 0
                            trClass = "GridStyleAlt"
                        Else
                            trClass = "Border"
                        End If
                        Dim url As String = GetSelectedEquipmentJS(RI.SharedFunctions.DataClean(dr.Item("EquipmentDesc")), RI.SharedFunctions.DataClean(dr.Item("Equipmentid")))
                        Dim tr As String = "<tr class='" & trClass & "'><td>{0}</td><td><a href=""{5}"">{1}</a></td><td>{2}</td><td>{3}</td><td>{4}</td></tr>"
                        sbTable.Append(String.Format(tr, RI.SharedFunctions.DataClean(dr.Item("Equipmentid")), RI.SharedFunctions.DataClean(dr.Item("EquipmentDesc")), RI.SharedFunctions.DataClean(dr.Item("equipclass")), RI.SharedFunctions.DataClean(dr.Item("equiptype")), RI.SharedFunctions.DataClean(dr.Item("Criticality")), url))
                    Loop
                    If rowCount = 0 Then
                        sbTable.Append("<tr><td colspan=5 align=center><h2>" & RI.SharedFunctions.LocalizeValue("NoRecordsFound") & "<h2></td></tr>")
                    End If
                    sbTable.Append("</Table>")
                End If
            End If

        Catch ex As Exception
            Throw
        Finally
            PerformEquipmentSearch = sbTable.ToString
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function
    Public Function GetSelectedEquipmentJS(ByVal Equipment As String, ByVal id As String) As String
        Dim sb As New StringBuilder
        sb.Append("Javascript:SelectedEquipment('")
        sb.Append(Equipment)
        sb.Append("','")
        sb.Append(id.ToString)
        sb.Append("',")
        sb.Append("txtAutoComplete,")
        'sb.Append("','")
        sb.Append("txtAutoComplete2,")
        'sb.Append("','")
        sb.Append("ECClose")
        sb.Append(");")
        Return sb.ToString
    End Function

    Public Function GetBusAreaLineList(ByVal dr As Devart.Data.Oracle.OracleDataReader) As String
        Dim sb As New StringBuilder
        Dim listCount As Integer = -1
        Try
            sb.Append("<table id=""ctl00__cphMain__lbBusinessUnit"" class=""CheckBoxList"" border=""0"" style=""font-weight:bold;white-space: nowrap;"">")
            Do While dr.Read()
                Dim val As String
                If dr.Item("BusUnitArea") IsNot Nothing Then
                    listCount = listCount + 1
                    val = dr.Item("BusUnitArea")
                    sb.Append("<tr><td><span style=""font-weight:bold;""><input id=""ctl00__cphMain__lbBusinessUnit_" & listCount & """ type=""checkbox"" name=""ctl00$_cphMain$_lbBusinessUnit$" & listCount & """ value=""" & val & """ /><label for=""ctl00__cphMain__lbBusinessUnit_" & listCount & """>" & val & "</label></span></td></tr>")
                End If
            Loop
            sb.Append("</table>")
        Catch ex As Exception
            Throw
        Finally
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            If listCount > 0 Then
                GetBusAreaLineList = sb.ToString
            Else
                GetBusAreaLineList = String.Empty
            End If
        End Try
    End Function
    <WebMethod()> _
<Script.Services.ScriptMethod()> _
   Public Function PopulateConstrainedArea(ByVal SiteId As String, ByVal BusArea As String, ByVal LineBreak As String) As String
        Dim dr As DataTableReader = Nothing
        Dim ds As DataSet = Nothing
        Dim items As String = String.Empty
        Dim busAreaWhere As String = String.Empty
        Dim lineBreakWhere As String = String.Empty
        Dim ConstrainedArea As String = String.Empty

        Try
            Dim sql As String = "Select distinct decode(constrainedarea,'Yes','Constrained Area',constrainedarea) constrainedarea FROM RefSiteArea where refsitearea.siteid='{0}' {1} {2}  order by constrainedarea"
            'Dim sql2 As String = "Select constrainedarea FROM RefSiteArea where refsitearea.siteid='{0}' {1} {2}  order by constrainedarea"
            If BusArea.Length > 0 Then
                busAreaWhere = String.Format(" and RISuperArea|| ' - ' ||Subarea='{0}'", BusArea)
            End If
            If LineBreak.Length > 0 Then
                If InStr(LineBreak, "-") = 0 Then LineBreak = LineBreak & " - None"
                lineBreakWhere = String.Format(" and area|| ' - ' ||linebreak='{0}'", LineBreak)
            End If
            sql = String.Format(sql, SiteId, busAreaWhere, lineBreakWhere)
            'sql2 = String.Format(sql2, SiteId, busAreaWhere, lineBreakWhere)
            Dim key As String = "RI_CONSTRAINED_AREA_" & RI.SharedFunctions.CreateKey(sql)

            ds = RI.SharedFunctions.GetOracleDataSet(sql)

            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader
                If dr IsNot Nothing Then
                    dr.Read()
                    If dr.HasRows Then
                        ConstrainedArea = RI.SharedFunctions.DataClean(dr.Item("constrainedarea"), "No")
                    Else
                        ConstrainedArea = ""
                        'If lineBreakWhere.Length > 0 Then
                        '    ds = RI.SharedFunctions.GetOracleDataSet(sql2)
                        '    If ds IsNot Nothing Then
                        '        dr = ds.Tables(0).CreateDataReader
                        '        If dr IsNot Nothing Then
                        '            dr.Read()
                        '            If dr.HasRows Then
                        '                items = RI.SharedFunctions.DataClean(dr.Item("flid")) & " * "  '& " * " & RI.SharedFunctions.DataClean(dr.Item("criticality"))
                        '            End If
                        '        End If
                        '    End If
                        'End If
                    End If
                End If
            End If
            Return ConstrainedArea
        Catch ex As Exception
            Throw New ApplicationException("Exception Occured")
        Finally

            If ds IsNot Nothing Then ds = Nothing
            If dr IsNot Nothing Then dr = Nothing
        End Try
    End Function
End Class
