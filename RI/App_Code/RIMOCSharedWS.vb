Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports Devart.Data.Oracle
Imports System.Data
Imports System.Text


<WebService(Namespace:="http://RI/")> _
<System.Web.Script.Services.ScriptService()> _
Public Class RIMOCSharedWS
    Inherits System.Web.Services.WebService

    <WebMethod(EnableSession:=True)> _
      Public Function GetEmployeeListBySite(ByVal siteID As String) As String()

        Dim values As New Generic.List(Of String)

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            'param.ParameterName = "in_plantcode"
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            'param.Value = "0627"
            param.Value = siteID
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            'param.ParameterName = "rsResponsibleList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PersonDDL", "MOCPerson_" & siteID, 3)
            ' ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson_" & siteID, 3)

            
            Dim dr As DataTableReader = ds.CreateDataReader


            If dr IsNot Nothing Then
                Do While dr.Read
                    If dr.Item("UserName") IsNot DBNull.Value Then
                        values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Person")))
                        'values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Name")))
                    End If
                Loop
            End If
        Catch ex As Exception
            Throw New DataException("GetPerson", ex)
            Return Nothing
        Finally
            GetEmployeeListBySite = values.ToArray
            If Not ds Is Nothing Then ds = Nothing
        End Try
    End Function
    ' ''' <summary>
    ' ''' Gets the list of values for the Classification List
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    '<WebMethod(EnableSession:=True)> _
    'Public Function GetClassification() As String()

    '    Dim paramCollection As New OracleParameterCollection
    '    Dim param As New OracleParameter
    '    Dim ds As System.Data.DataSet = Nothing

    '    Try

    '        param = New OracleParameter
    '        param.ParameterName = "rsFacility"
    '        param.OracleDbType = OracleDbType.Cursor
    '        param.Direction = ParameterDirection.Output
    '        paramCollection.Add(param)

    '        Dim key As String = "RINEWINCIDENT.FacilityList"
    '        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)
    '        Dim dt As New Data.DataTable

    '        Dim dr As DataTableReader = ds.CreateDataReader
    '        'param = New OracleParameter
    '        'param.ParameterName = "rsClassification"
    '        'param.OracleDbType = OracleDbType.Cursor
    '        'param.Direction = ParameterDirection.Output
    '        'paramCollection.Add(param)

    '        'param = New OracleParameter
    '        'param.ParameterName = "rsCategory"
    '        'param.OracleDbType = OracleDbType.Cursor
    '        'param.Direction = ParameterDirection.Output
    '        'paramCollection.Add(param)

    '        'ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "MOCMaint.GetClassCategoryList", "MOCClassCategory", 8)

    '        ''Dim dr As DataTableReader = ds.CreateDataReader

    '        ''If dr IsNot Nothing Then
    '        ''    Do While dr.Read
    '        ''        If dr.Item("mocclassification_seq_id") IsNot DBNull.Value Then
    '        ''            values.Add(dr.Item("mocclassification_seq_id") & "::" & RI.SharedFunctions.DataClean(dr.Item("mocclassification")))
    '        ''        End If
    '        ''    Loop
    '        ''End If

    '        'Dim dt As New Data.DataTable

    '        'Dim dr As DataTableReader = ds.Tables(0).CreateDataReader

    '        If dr IsNot Nothing Then
    '            dt.Load(dr)
    '        End If

    '        Dim data As String = RI.SharedFunctions.GetJson(dt)
    '        Return data

    '    Catch ex As Exception
    '        Throw New DataException("GetClassification", ex)
    '        Return Nothing
    '    Finally
    '        'GetClassification = values.ToArray
    '        If Not ds Is Nothing Then ds = Nothing
    '    End Try

    'End Function
    <WebMethod(EnableSession:=True)> _
    Public Function DeleteMOCAction(ByVal actionNumber As String) As String

        Dim dr As Devart.Data.Oracle.OracleDataReader = Nothing
        Dim paramCollection As New OracleParameterCollection
        Dim status As String
        Dim userProfile As RI.CurrentUserProfile = Nothing
        userProfile = RI.SharedFunctions.GetUserProfile

        Try
            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_actionid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = actionNumber
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "in_userid"
            param.OracleDbType = OracleDbType.VarChar
            param.Direction = Data.ParameterDirection.Input
            param.Value = userProfile.Username
            paramCollection.Add(param)

            status = RI.SharedFunctions.CallDROraclePackage(paramCollection, "MOCactions.deleteaction")
            Dim ses As System.Web.SessionState.HttpSessionState = HttpContext.Current.Session
            ses.Remove("MOCActionItems")
        Catch ex As Exception
            Throw New DataException("DeleteMOCAction", ex)
        Finally
            If Not dr Is Nothing Then dr = Nothing
        End Try
        Return status
    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function PopulateFacility() As System.Data.DataSet
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FacilityList"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFacility", , ex)
        Finally
            PopulateFacility = ds
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function
    <WebMethod()> _
<Script.Services.ScriptMethod()> _
    Public Function GetDDLData(ByVal plantCode As String, ByVal userControlId As String) As Collections.Generic.List(Of ListItem)
        '    Public Function GetDDLData(ByVal plantCode As String, ByVal userControlId As String) As String()
        Dim values As New Generic.List(Of String)
        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)
        Dim currentUserMode As Integer = 0

        ddlList.Add(New ListItem(userControlId, CStr(0)))

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Dim param As New OracleParameter

        param = New OracleParameter
        param.ParameterName = "in_plantcode"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = plantCode
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson" & plantCode, 0)

        Dim dr As DataTableReader = ds.CreateDataReader

        If dr IsNot Nothing Then
            Do While dr.Read
                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                With ddlList
                    If dr.Item("RoleDescription") <> roleDescription Then 'New Group
                        'No Roleseqid indicates individual
                        Dim roleItem As New ListItem
                        roleDescription = dr.Item("RoleDescription")
                        roleItem.Text = dr.Item("RoleDescription").ToUpper
                        If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                            roleItem.Value = dr.Item("RoleSeqID")
                        End If

                        If ddlList.Count > 0 Then
                            Dim blankItem As New ListItem
                            With blankItem
                                .Attributes.Add("disabled", "true")
                                .Text = ""
                                .Value = -1
                            End With
                            ddlList.Add(blankItem)
                        End If

                        If roleDescription.Length > 0 Then
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                            ddlList.Add(roleItem)
                        Else
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                            roleItem.Attributes.Add("disabled", "true")
                            ddlList.Add(roleItem)
                        End If

                    End If

                    Dim useritem As New ListItem
                    With useritem
                        .Text = Server.HtmlDecode(spaceChar & dr.Item("Name"))
                        .Value = dr.Item("UserName")
                    End With
                    ddlList.Add(useritem)

                End With

            Loop
        End If
        'GetDDLData = values.ToArray
        Return ddlList
    End Function
    <WebMethod()> _
<Script.Services.ScriptMethod()> _
    Public Function GetDDLData2(ByVal plantCode As String, ByVal userControlId As String) As Collections.Generic.List(Of ListItem)
        '    Public Function GetDDLData(ByVal plantCode As String, ByVal userControlId As String) As String()
        Dim values As New Generic.List(Of String)
        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)
        Dim currentUserMode As Integer = 0

        ddlList.Add(New ListItem(userControlId, CStr(0)))

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Dim param As New OracleParameter

        param = New OracleParameter
        param.ParameterName = "in_siteid"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = plantCode
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson" & plantCode, 0)

        Dim dr As DataTableReader = ds.CreateDataReader

        If dr IsNot Nothing Then
            Do While dr.Read
                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                With ddlList
                    If dr.Item("RoleDescription") <> roleDescription Then 'New Group
                        'No Roleseqid indicates individual
                        Dim roleItem As New ListItem
                        roleDescription = dr.Item("RoleDescription")
                        'roleItem.Text = dr.Item("RoleDescription").ToUpper
                        roleItem.Text = RI.SharedFunctions.LocalizeValue(roleDescription).ToUpper
                        If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                            roleItem.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqID")
                            'roleItem.Value = dr.Item("RoleSeqID") '& "/" & dr.Item("UserName")
                        End If

                        'If ddlList.Count > 0 Then
                        '    Dim blankItem As New ListItem
                        '    With blankItem
                        '        .Attributes.Add("disabled", "true")
                        '        .Text = ""
                        '        .Value = -1
                        '    End With
                        '    ddlList.Add(blankItem)
                        'End If

                        If roleDescription.Length > 0 Then
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                            ddlList.Add(roleItem)
                        Else
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                            roleItem.Attributes.Add("disabled", "true")
                            ddlList.Add(roleItem)
                        End If

                    End If

                    Dim useritem As New ListItem
                    With useritem
                        .Text = Server.HtmlDecode(spaceChar & dr.Item("Name"))
                        'If dr.Item("RoleSeqid").ToString <> "" Then
                        '.Value = dr.Item("roleplantcode") & "/" & dr.Item("RoleSeqID")
                        'Else
                        .Value = dr.Item("UserName")
                        'End If
                    End With
                    ddlList.Add(useritem)


                End With

            Loop
        End If

        'GetDDLData = values.ToArray
        Return ddlList
    End Function
    <WebMethod()> _
<Script.Services.ScriptMethod()> _
    Public Function GetMTTResponsible(ByVal plantCode As String, ByVal userControlId As String) As Collections.Generic.List(Of ListItem)
        '    Public Function GetDDLData(ByVal plantCode As String, ByVal userControlId As String) As String()
        Dim values As New Generic.List(Of String)
        Dim roleDescription As String = String.Empty
        Dim ddlList As New Collections.Generic.List(Of ListItem)
        Dim currentUserMode As Integer = 0

        ddlList.Add(New ListItem(userControlId, CStr(0)))

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection

        Dim param As New OracleParameter

        param = New OracleParameter
        param.ParameterName = "in_siteid"
        param.OracleDbType = OracleDbType.VarChar
        param.Value = plantCode
        param.Direction = ParameterDirection.Input
        paramCollection.Add(param)

        param = New OracleParameter
        param.ParameterName = "rsResponsibleList"
        param.OracleDbType = OracleDbType.Cursor
        param.Direction = ParameterDirection.Output
        paramCollection.Add(param)

        ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetResponsibleList", "MOCPerson" & plantCode, 0)

        Dim dr As DataTableReader = ds.CreateDataReader

        If dr IsNot Nothing Then
            Do While dr.Read
                Dim spaceChar As String = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                With ddlList
                    If dr.Item("RoleDescription") <> roleDescription Then 'New Group
                        'No Roleseqid indicates individual
                        Dim roleItem As New ListItem
                        roleDescription = dr.Item("RoleDescription")
                        roleItem.Text = RI.SharedFunctions.LocalizeValue(roleDescription).ToUpper
                        'roleItem.Text = dr.Item("RoleDescription").ToUpper
                        If dr.Item("RoleSeqID") IsNot DBNull.Value Then
                            roleItem.Value = dr.Item("RoleSeqID") '& "/" & dr.Item("UserName")
                        End If

                        If ddlList.Count > 0 Then
                            Dim blankItem As New ListItem
                            With blankItem
                                .Attributes.Add("disabled", "true")
                                .Text = ""
                                .Value = -1
                            End With
                            ddlList.Add(blankItem)
                        End If

                        If roleDescription.Length > 0 Then
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black; font-size:Larger;")
                            ddlList.Add(roleItem)
                        Else
                            roleItem.Attributes.Add("style", "background-color:ActiveBorder; color:black;")
                            roleItem.Attributes.Add("disabled", "true")
                            ddlList.Add(roleItem)
                        End If

                    End If

                    Dim useritem As New ListItem
                    With useritem
                        .Text = Server.HtmlDecode(spaceChar & dr.Item("Name"))
                        .Value = dr.Item("UserName")
                    End With
                    ddlList.Add(useritem)

                End With

            Loop

        End If
        'GetDDLData = values.ToArray
        Return ddlList
    End Function
    '  <WebMethod(EnableSession:=True)> _
    'Public Function GetMOCNotificationBySite(ByVal siteID As String) As String()

    '      Dim values As New Generic.List(Of String)

    '      Dim ds As DataSet = Nothing
    '      Dim paramCollection As New OracleParameterCollection
    '      Try

    '          Dim param As New OracleParameter

    '          param = New OracleParameter
    '          param.ParameterName = "in_siteid"
    '          param.OracleDbType = OracleDbType.VarChar
    '          param.Value = siteID
    '          param.Direction = Data.ParameterDirection.Input
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "in_BusUnit"
    '          param.OracleDbType = OracleDbType.VarChar
    '          param.Value = busunit
    '          param.Direction = Data.ParameterDirection.Input
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "in_area"
    '          param.OracleDbType = OracleDbType.VarChar
    '          param.Value = area
    '          param.Direction = Data.ParameterDirection.Input
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "in_line"
    '          param.OracleDbType = OracleDbType.VarChar
    '          param.Value = line
    '          param.Direction = Data.ParameterDirection.Input
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "rsEndorserList"
    '          param.OracleDbType = OracleDbType.Cursor
    '          param.Direction = Data.ParameterDirection.Output
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "rsL1List"
    '          param.OracleDbType = OracleDbType.Cursor
    '          param.Direction = Data.ParameterDirection.Output
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "rsL2List"
    '          param.OracleDbType = OracleDbType.Cursor
    '          param.Direction = Data.ParameterDirection.Output
    '          paramCollection.Add(param)

    '          param = New OracleParameter
    '          param.ParameterName = "rsL3List"
    '          param.OracleDbType = OracleDbType.Cursor
    '          param.Direction = Data.ParameterDirection.Output
    '          paramCollection.Add(param)

    '          ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "NewMOC.GetBUANotificationList", "GetBUANotificationList", 0)

    '          Dim dr As DataTableReader = ds.CreateDataReader

    '          If dr IsNot Nothing Then
    '              Do While dr.Read
    '                  If dr.Item("UserName") IsNot DBNull.Value Then
    '                      values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Person")))
    '                      'values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Name")))
    '                  End If
    '              Loop
    '          End If
    '      Catch ex As Exception
    '          Throw New DataException("GetMOCNotificationBySite", ex)
    '          Return Nothing
    '      Finally
    '          GetMOCNotificationBySite = values.ToArray
    '          If Not ds Is Nothing Then ds = Nothing
    '      End Try
    '  End Function

    <WebMethod()> _
<Script.Services.ScriptMethod()> _
Public Function GetNotificationType(ByVal facility As String, ByVal businessUnit As String, ByVal Area As String, ByVal lineBreak As String) As String()
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing
        'Dim ActiveFlag As String = String.Empty
        'Dim businessUnit As String = String.Empty
        'Dim Area As String = String.Empty
        'Dim Line As String = String.Empty
        Dim NotificationType As String = String.Empty

        'Check input paramaters
        'Dim userProfile As RI.CurrentUserProfile = Nothing
        'userProfile = RI.SharedFunctions.GetUserProfile
        'Dim username As String = String.Empty
        Dim values As New Generic.List(Of String)
        Try
            'If busArea.Length > 1 Then
            '    businessUnitArea = busArea.Split(CChar("-"))
            '    If businessUnitArea.Length = 2 Then
            '        businessUnit = businessUnitArea(0).Trim
            '        'Area = businessUnitArea(1).Trim
            '    ElseIf businessUnitArea.Length = 1 Then
            '        businessUnit = businessUnitArea(0).Trim
            '    End If
            'End If

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
            param.Value = lineBreak
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsEndorserList"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "NewIncident.GetBUANotificationList" & facility & "_" & businessUnit & "_"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.MOCMAINT.GetBUANotificationList", key, 0)

            Dim dr As DataTableReader = ds.CreateDataReader

            If dr IsNot Nothing Then
                Do While dr.Read
                    If dr.Item("UserName") IsNot DBNull.Value Then
                        values.Add(dr.Item("NotifyType") & "::" & dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Fullname")))
                        'values.Add(dr.Item("UserName") & "::" & RI.SharedFunctions.DataClean(dr.Item("Name")))
                    End If
                Loop
            End If

        Catch ex As Exception
            Throw New DataException("GetNotificationType", ex)
        Finally
            GetNotificationType = values.ToArray
            If ds IsNot Nothing Then
                'ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function

    <WebMethod(EnableSession:=True)> _
    Public Function LoadTable() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = Data.ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FacilityList"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("PopulateFacility", , ex)
        Finally
            LoadTable = ds.GetXml
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function


    'Public Function RetrieveSites(aData As List(Of String)) As List(Of Cars)


    '    Dim li As New List(Of Cars)()
    '    'Dim parameters As New NameValueCollection()

    '    Try
    '        'parameters.Clear()
    '        Dim Cars As String()() = RI.SharedFunctions.CallDROraclePackage()
    '        ._dar.GetDatabaseResults("FMEA.GETSITE", parameters)

    '        li = _dar.BuildSelectOptionList(sites, True)

    '        For Each it As SelectOptionItem In li
    '            it.Selected = (If(it.Value = _userinfo.site, True, False))

    '        Next
    '    Catch ex As Exception
    '        Dim msg As String = String.Format("Error getting site data.{0}{1}", Environment.NewLine, ex.ToString())
    '        '_logger.ErrorFormat(msg)
    '        Throw New Exception(msg)
    '    End Try
    '    Return li
    'End Function
    '*
    <WebMethod(EnableSession:=True)> _
    Public Function GetCustomers() As String

        Dim values As New Generic.List(Of String)

        Dim ds As DataSet = Nothing
        Dim paramCollection As New OracleParameterCollection
        Try

            Dim param As New OracleParameter

            param = New OracleParameter
            param.ParameterName = "in_siteid"
            param.OracleDbType = OracleDbType.VarChar
            param.Value = "GT"
            param.Direction = ParameterDirection.Input
            paramCollection.Add(param)

            param = New OracleParameter
            param.ParameterName = "rsPerson"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "RI.PersonDDL", "Person_", 0)

            Dim dr As DataTableReader = ds.CreateDataReader

            Dim dt As New Data.DataTable
            If dr IsNot Nothing Then
                'If createDataTable = True Then
                dt.Load(dr)

            End If
            ''End If

            'Dim customers As New List(Of ListItem)()
            'If dr IsNot Nothing Then
            '    Do While dr.Read
            '        customers.Add(New ListItem(dr.Item("UserName"), dr.Item("Person")))

            '    Loop
            'End If
            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

            'Return customers
        Catch ex As Exception
            Throw New DataException("GetPerson", ex)
            Return Nothing
        Finally
            'GetCustomers = values.ToArray
            If Not ds Is Nothing Then ds = Nothing
        End Try


    End Function
    <WebMethod(EnableSession:=True)> _
    Public Function GetFacility() As String
        Dim paramCollection As New OracleParameterCollection
        Dim param As New OracleParameter
        Dim ds As System.Data.DataSet = Nothing

        Try

            param = New OracleParameter
            param.ParameterName = "rsFacility"
            param.OracleDbType = OracleDbType.Cursor
            param.Direction = ParameterDirection.Output
            paramCollection.Add(param)

            Dim key As String = "RINEWINCIDENT.FacilityList"
            ds = RI.SharedFunctions.GetDSFromPackage(paramCollection, "Reladmin.RINEWINCIDENT.FacilityList", key, 24)
            Dim dt As New Data.DataTable

            Dim dr As DataTableReader = ds.CreateDataReader
            If dr IsNot Nothing Then
                dt.Load(dr)
            End If

            Dim data As String = RI.SharedFunctions.GetJson(dt)
            Return data

        Catch ex As Exception
            ds = Nothing
            RI.SharedFunctions.HandleError("GetFacility", , ex)
            Return Nothing
        Finally
            If ds IsNot Nothing Then
                ds.Dispose()
                ds = Nothing
            End If

        End Try
    End Function


    ''' <summary>
    ''' Gets the Default Division for the specified Facility
    ''' </summary>
    ''' <param name="SiteId"></param>
    ''' <returns>String - Returns the Default Division for the specified Facility</returns>
    ''' <remarks></remarks>
    <Services.WebMethod(CacheDuration:=0, EnableSession:=True), Script.Services.ScriptMethod()>
    Public Function GetSiteDivision(ByVal SiteId As String) As String
        Dim ds As DataSet = PopulateFacility()
        Dim ret As String = String.Empty
        Dim dr As DataTableReader = Nothing

        Try
            If ds IsNot Nothing Then
                dr = ds.Tables(0).CreateDataReader

                Do While dr.Read
                    If dr.Item("SiteID") IsNot Nothing Then
                        If dr.Item("SiteID") = SiteId Then
                            ret = dr.Item("Division")
                        End If
                    End If
                Loop
            End If
        Catch ex As Exception
            RI.SharedFunctions.HandleError("GetSiteDivision", , ex)
        Finally
            If ds IsNot Nothing Then
                ds = Nothing
            End If
            If dr IsNot Nothing Then
                dr.Close()
                dr = Nothing
            End If
            GetSiteDivision = ret
        End Try
    End Function
End Class
